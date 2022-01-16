using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Geometric2.Global;
using Geometric2.Helpers;
using Geometric2.MatrixHelpers;
using Geometric2.Models;
using Geometric2.RasterizationClasses;
using OpenTK;
using OpenTK.Graphics.OpenGL4;

namespace Geometric2.ModelGeneration
{
    public class Model : Element
    {
        private float[] cubePoints = null;
        public int cubeVBO, cubeVAO, cubeEBO;
        uint[] indices = null;
        Texture texture;
        Texture specular;
        Texture noise;
        public GlobalPhysicsData first_globalPhysicsData;
        public List<(Vector3, Vector3)> points = new List<(Vector3, Vector3)>();
        Vector3[][][] p = new Vector3[4][][];

        public Model(MeshVertices model_data)
        {
            indices = model_data.Indices;
            List<float> _cubepoints = new List<float>();
            foreach (var vertice in model_data.Vertices)
            {
                points.Add((vertice.Position, vertice.Normal));
                _cubepoints.Add(vertice.Position.X);
                _cubepoints.Add(vertice.Position.Y);
                _cubepoints.Add(vertice.Position.Z);
                //_cubepoints.Add(vertice.Normal.X);
                //_cubepoints.Add(vertice.Normal.Y);
                //_cubepoints.Add(vertice.Normal.Z);
                _cubepoints.Add(0);
                _cubepoints.Add(0);
                _cubepoints.Add(0);
                _cubepoints.Add(0.5f);
                _cubepoints.Add(0.5f);
            }
            cubePoints = _cubepoints.ToArray();
        }

        public override void CreateGlElement(Shader _shader, Shader _shaderLight)
        {
            InitP();
            _shaderLight.Use();
            texture = new Texture("./../../../Resources/wood.jpg");
            specular = new Texture("./../../../Resources/50specular.png");
            noise = new Texture("./../../../Resources/noise.jpg");
            cubeVAO = GL.GenVertexArray();
            cubeVBO = GL.GenBuffer();
            cubeEBO = GL.GenBuffer();
            GL.BindVertexArray(cubeVAO);
            GL.BindBuffer(BufferTarget.ArrayBuffer, cubeVBO);
            GL.BufferData(BufferTarget.ArrayBuffer, cubePoints.Length * sizeof(float), cubePoints, BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, cubeEBO);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);
            var a_Position_Location = _shaderLight.GetAttribLocation("a_Position");
            GL.VertexAttribPointer(a_Position_Location, 3, VertexAttribPointerType.Float, true, 8 * sizeof(float), 0);
            GL.EnableVertexAttribArray(a_Position_Location);
            var aNormal = _shaderLight.GetAttribLocation("aNormal");
            GL.EnableVertexAttribArray(aNormal);
            GL.VertexAttribPointer(aNormal, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 3 * sizeof(float));
            var aTexCoords = _shaderLight.GetAttribLocation("aTexCoords");
            GL.EnableVertexAttribArray(aTexCoords);
            GL.VertexAttribPointer(aTexCoords, 2, VertexAttribPointerType.Float, false, 8 * sizeof(float), 6 * sizeof(float));
        }

        public override void RenderGlElement(Shader _shader, Shader _shaderLight, Vector3 rotationCentre, GlobalPhysicsData globalPhysicsData)
        {
            if (globalPhysicsData.displayDisortBox)
            {
                //RecalculatePatches(globalPhysicsData);
                RecalculateP(globalPhysicsData);
                RecalculatePoints();
                RecalculateGeometry();
                _shaderLight.Use();
                Matrix4 model = ModelMatrix.CreateModelMatrix(new Vector3(1.0f, 1.0f, 1.0f), RotationQuaternion, CenterPosition + Translation, rotationCentre, TempRotationQuaternion);
                _shaderLight.SetMatrix4("model", model);
                _shaderLight.SetInt("transparent", 1);
                GL.BindVertexArray(cubeVAO);
                texture.Use();
                specular.Use(TextureUnit.Texture1);
                noise.Use(TextureUnit.Texture2);
                GL.DrawElements(PrimitiveType.Triangles, cubePoints.Length, DrawElementsType.UnsignedInt, 0);
                GL.BindVertexArray(0);
            }
        }

        private void InitP()
        {
            for (int i = 0; i < 4; i++)
            {
                p[i] = new Vector3[4][];
                for (int j = 0; j < 4; j++)
                {
                    p[i][j] = new Vector3[4];
                }
            }
        }

        private void RecalculateP(GlobalPhysicsData globalPhysicsData)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    for (int k = 0; k < 4; k++)
                    {
                        p[i][j][k] = globalPhysicsData.points[i * 16 + j * 4 + k].Position();
                    }
                }
            }
        }

        private void RecalculatePoints()
        {
            float[] tempPoints = new float[cubePoints.Length];
            Parallel.For(0, points.Count, i =>
            {
                var resultPoints = Evaluate(points[i].Item1);
                tempPoints[8 * i] = (resultPoints.X);
                tempPoints[8 * i + 1] = (resultPoints.Y);
                tempPoints[8 * i + 2] = (resultPoints.Z);

                var resultPointsOfBiggerModel = Evaluate(points[i].Item1 + 0.1f * points[i].Item2);
                var norm = (resultPointsOfBiggerModel - resultPoints).Normalized();
                tempPoints[8 * i + 3] = (norm.X);
                tempPoints[8 * i + 4] = (norm.Y);
                tempPoints[8 * i + 5] = (norm.Z);
                tempPoints[8 * i + 6] = (0.5f);
                tempPoints[8 * i + 7] = (0.5f);
            });

            cubePoints = tempPoints;
        }

        private void RecalculateGeometry()
        {
            GL.BindVertexArray(cubeVAO);
            GL.BindBuffer(BufferTarget.ArrayBuffer, cubeVBO);
            GL.BufferData(BufferTarget.ArrayBuffer, cubePoints.Length * sizeof(float), cubePoints, BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, cubeEBO);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);
        }

        private Vector3 Evaluate(Vector3 uvw)
        {
            float u = uvw.Y;
            float v = uvw.X;
            float w = uvw.Z;
            float u0 = (1.0f - u) * (1.0f - u) * (1.0f - u);
            float u1 = 3.0f * u * (1.0f - u) * (1.0f - u);
            float u2 = 3.0f * u * u * (1.0f - u);
            float u3 = u * u * u;
            float v0 = (1.0f - v) * (1.0f - v) * (1.0f - v);
            float v1 = 3.0f * v * (1.0f - v) * (1.0f - v);
            float v2 = 3.0f * v * v * (1.0f - v);
            float v3 = v * v * v;
            float w0 = (1.0f - w) * (1.0f - w) * (1.0f - w);
            float w1 = 3.0f * w * (1.0f - w) * (1.0f - w);
            float w2 = 3.0f * w * w * (1.0f - w);
            float w3 = w * w * w;

            return w0 *
            (u0 * (v0 * p[0][0][0] + v1 * p[0][0][1] + v2 * p[0][0][2] + v3 * p[0][0][3])
            + u1 * (v0 * p[0][1][0] + v1 * p[0][1][1] + v2 * p[0][1][2] + v3 * p[0][1][3])
            + u2 * (v0 * p[0][2][0] + v1 * p[0][2][1] + v2 * p[0][2][2] + v3 * p[0][2][3])
            + u3 * (v0 * p[0][3][0] + v1 * p[0][3][1] + v2 * p[0][3][2] + v3 * p[0][3][3])) +
            w1 *
            (u0 * (v0 * p[1][0][0] + v1 * p[1][0][1] + v2 * p[1][0][2] + v3 * p[1][0][3])
            + u1 * (v0 * p[1][1][0] + v1 * p[1][1][1] + v2 * p[1][1][2] + v3 * p[1][1][3])
            + u2 * (v0 * p[1][2][0] + v1 * p[1][2][1] + v2 * p[1][2][2] + v3 * p[1][2][3])
            + u3 * (v0 * p[1][3][0] + v1 * p[1][3][1] + v2 * p[1][3][2] + v3 * p[1][3][3])) +
            w2 *
            (u0 * (v0 * p[2][0][0] + v1 * p[2][0][1] + v2 * p[2][0][2] + v3 * p[2][0][3])
            + u1 * (v0 * p[2][1][0] + v1 * p[2][1][1] + v2 * p[2][1][2] + v3 * p[2][1][3])
            + u2 * (v0 * p[2][2][0] + v1 * p[2][2][1] + v2 * p[2][2][2] + v3 * p[2][2][3])
            + u3 * (v0 * p[2][3][0] + v1 * p[2][3][1] + v2 * p[2][3][2] + v3 * p[2][3][3])) +
            w3 *
            (u0 * (v0 * p[3][0][0] + v1 * p[3][0][1] + v2 * p[3][0][2] + v3 * p[3][0][3])
            + u1 * (v0 * p[3][1][0] + v1 * p[3][1][1] + v2 * p[3][1][2] + v3 * p[3][1][3])
            + u2 * (v0 * p[3][2][0] + v1 * p[3][2][1] + v2 * p[3][2][2] + v3 * p[3][2][3])
            + u3 * (v0 * p[3][3][0] + v1 * p[3][3][1] + v2 * p[3][3][2] + v3 * p[3][3][3]));
        }
    }
}
