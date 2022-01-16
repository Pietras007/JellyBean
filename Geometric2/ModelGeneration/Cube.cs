using System;
using System.Collections.Generic;
using System.Drawing;
using Geometric2.Global;
using Geometric2.Helpers;
using Geometric2.MatrixHelpers;
using Geometric2.Models;
using Geometric2.RasterizationClasses;
using OpenTK;
using OpenTK.Graphics.OpenGL4;

namespace Geometric2.ModelGeneration
{
    public class Cube : Element
    {
        private float[] cubePoints = {
             // Positions          Normals              Texture coords
            -0.5f, -0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  0.0f, 0.0f,
             0.5f, -0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  1.0f, 0.0f,
             0.5f,  0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  1.0f, 1.0f,
             0.5f,  0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  1.0f, 1.0f,
            -0.5f,  0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  0.0f, 1.0f,
            -0.5f, -0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  0.0f, 0.0f,

            -0.5f, -0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  0.0f, 0.0f,
             0.5f, -0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  1.0f, 0.0f,
             0.5f,  0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  1.0f, 1.0f,
             0.5f,  0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  1.0f, 1.0f,
            -0.5f,  0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  0.0f, 1.0f,
            -0.5f, -0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  0.0f, 0.0f,

            -0.5f,  0.5f,  0.5f, -1.0f,  0.0f,  0.0f,  1.0f, 0.0f,
            -0.5f,  0.5f, -0.5f, -1.0f,  0.0f,  0.0f,  1.0f, 1.0f,
            -0.5f, -0.5f, -0.5f, -1.0f,  0.0f,  0.0f,  0.0f, 1.0f,
            -0.5f, -0.5f, -0.5f, -1.0f,  0.0f,  0.0f,  0.0f, 1.0f,
            -0.5f, -0.5f,  0.5f, -1.0f,  0.0f,  0.0f,  0.0f, 0.0f,
            -0.5f,  0.5f,  0.5f, -1.0f,  0.0f,  0.0f,  1.0f, 0.0f,

             0.5f,  0.5f,  0.5f,  1.0f,  0.0f,  0.0f,  1.0f, 0.0f,
             0.5f,  0.5f, -0.5f,  1.0f,  0.0f,  0.0f,  1.0f, 1.0f,
             0.5f, -0.5f, -0.5f,  1.0f,  0.0f,  0.0f,  0.0f, 1.0f,
             0.5f, -0.5f, -0.5f,  1.0f,  0.0f,  0.0f,  0.0f, 1.0f,
             0.5f, -0.5f,  0.5f,  1.0f,  0.0f,  0.0f,  0.0f, 0.0f,
             0.5f,  0.5f,  0.5f,  1.0f,  0.0f,  0.0f,  1.0f, 0.0f,

            -0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f,  0.0f, 1.0f,
             0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f,  1.0f, 1.0f,
             0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f,  1.0f, 0.0f,
             0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f,  1.0f, 0.0f,
            -0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f,  0.0f, 0.0f,
            -0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f,  0.0f, 1.0f,

            -0.5f,  0.5f, -0.5f,  0.0f,  1.0f,  0.0f,  0.0f, 1.0f,
             0.5f,  0.5f, -0.5f,  0.0f,  1.0f,  0.0f,  1.0f, 1.0f,
             0.5f,  0.5f,  0.5f,  0.0f,  1.0f,  0.0f,  1.0f, 0.0f,
             0.5f,  0.5f,  0.5f,  0.0f,  1.0f,  0.0f,  1.0f, 0.0f,
            -0.5f,  0.5f,  0.5f,  0.0f,  1.0f,  0.0f,  0.0f, 0.0f,
            -0.5f,  0.5f, -0.5f,  0.0f,  1.0f,  0.0f,  0.0f, 1.0f
        };

        public int cubeVBO, cubeVAO, cubeEBO;
        uint[] indices;
        Texture texture;
        Texture specular;
        Texture noise;
        public GlobalPhysicsData first_globalPhysicsData;
        public CubeData CubeData { get; set; }

        public Cube()
        {
            indices = new uint[36];
            for (int i = 0; i < indices.Length; i++)
            {
                indices[i] = (uint)i;
            }
        }

        public override void CreateGlElement(Shader _shader, Shader _shaderLight)
        {
            GeneratePatches(first_globalPhysicsData);
            _shaderLight.Use();
            texture = new Texture("./../../../Resources/wood.jpg");
            specular = new Texture("./../../../Resources/50specular.png");
            noise = new Texture("./../../../Resources/noise.jpg");
            cubeVAO = GL.GenVertexArray();
            cubeVBO = GL.GenBuffer();
            cubeEBO = GL.GenBuffer();
            GL.BindVertexArray(cubeVAO);
            GL.BindBuffer(BufferTarget.ArrayBuffer, cubeVBO);
            GL.BufferData(BufferTarget.ArrayBuffer, cubePoints.Length * sizeof(float), cubePoints, BufferUsageHint.DynamicDraw);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, cubeEBO);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.DynamicDraw);
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
            if (globalPhysicsData.displayBezierCube)
            {
                GeneratePatches(globalPhysicsData);
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

        private void RecalculateGeometry()
        {
            GL.BindVertexArray(cubeVAO);
            GL.BindBuffer(BufferTarget.ArrayBuffer, cubeVBO);
            GL.BufferData(BufferTarget.ArrayBuffer, cubePoints.Length * sizeof(float), cubePoints, BufferUsageHint.DynamicDraw);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, cubeEBO);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.DynamicDraw);
        }

        private void GeneratePatches(GlobalPhysicsData globalPhysicsData)
        {
            List<uint> indices = new List<uint>();
            List<Vector3> points = new List<Vector3>();
            var commonIndices = GenerateIndices();
            uint numberOfIndicesInPatch = (uint)commonIndices.Length;
            var patches = GetC0Patches(globalPhysicsData);
            uint idx = 0;
            foreach (var patch in patches)
            {
                Vector3[] patchPoints = GeneratePatchPoints(patch);
                points.AddRange(patchPoints);
                uint[] patchIndices = new uint[numberOfIndicesInPatch];
                for (int i = 0; i < numberOfIndicesInPatch; i++)
                {
                    indices.Add(commonIndices[i] + idx * numberOfIndicesInPatch);
                }
            }
            GenerateOnlyPoints(points);
        }

        private void GenerateOnlyPoints(List<Vector3> points)
        {
            cubePoints = new float[3 * points.Count];
            int idx = 0;
            foreach (var p in points)
            {
                cubePoints[3 * idx] = p.X;
                cubePoints[3 * idx + 1] = p.Y;
                cubePoints[3 * idx + 2] = p.Z;
                idx++;
            }
        }

        private List<Vector3[]> GetC0Patches(GlobalPhysicsData globalPhysicsData)
        {
            var points = globalPhysicsData.points;
            var pointsIndicesForPatches = new int[6][];
            for(int i = 0; i < 6; i++)
            {
                pointsIndicesForPatches[i] = new int[16];
            }

            for (int i = 0; i < 16; i++)
            {
                pointsIndicesForPatches[0][i] = i;
            }

            for (int i = 0; i < 16; i++)
            {
                pointsIndicesForPatches[1][i] = pointsIndicesForPatches[0][i] + 48;
            }

            pointsIndicesForPatches[2] = new int[] { 0,1,2,3,16,17,18,19,32,33,34,35,48,49,50,51};

            for (int i = 0; i < 16; i++)
            {
                pointsIndicesForPatches[3][i] = pointsIndicesForPatches[2][i] + 12;
            }

           
            pointsIndicesForPatches[4] = new int[] { 0,4,8,12,16,20,24,28,32,36,40,44,48,52,56,60};

            for (int i = 0; i < 16; i++)
            {
                pointsIndicesForPatches[5][i] = pointsIndicesForPatches[4][i] + 3;
            }

            List<Vector3[]> result = new List<Vector3[]>();
            for(int i = 0; i < 6; i++)
            {
                Vector3[] patchPoints = new Vector3[16];
                for(int j = 0; j < 16; j++)
                {
                    patchPoints[j] = points[pointsIndicesForPatches[i][j]].Position();
                }

                result.Add(patchPoints);
            }

            return result;
        }

        private Vector3[] GeneratePatchPoints(Vector3[] points)
        {
            Vector3[] allPatchPoints = new Vector3[(ConfigurationData.BezierBoxDivisions + 1) * (ConfigurationData.BezierBoxDivisions + 1)];
            for(float u = 0; u <= 1; u += 1.0f / ConfigurationData.BezierBoxDivisions)
            {
                Vector3 four1 = new Vector3();
                Vector3 four2 = new Vector3();
                Vector3 four3 = new Vector3();
                Vector3 four4 = new Vector3();

                four1.X = HelpFunctions.DeKastilio(new float[] { points[0].X, points[1].X, points[2].X, points[3].X }, u, 4);
                four1.Y = HelpFunctions.DeKastilio(new float[] { points[0].Y, points[1].Y, points[2].Y, points[3].Y }, u, 4);
                four1.Z = HelpFunctions.DeKastilio(new float[] { points[0].Z, points[1].Z, points[2].Z, points[3].Z }, u, 4);

                four2.X = HelpFunctions.DeKastilio(new float[] { points[4 + 0].X, points[4 + 1].X, points[4 + 2].X, points[4 + 3].X }, u, 4);
                four2.Y = HelpFunctions.DeKastilio(new float[] { points[4 + 0].Y, points[4 + 1].Y, points[4 + 2].Y, points[4 + 3].Y }, u, 4);
                four2.Z = HelpFunctions.DeKastilio(new float[] { points[4 + 0].Z, points[4 + 1].Z, points[4 + 2].Z, points[4 + 3].Z }, u, 4);

                four3.X = HelpFunctions.DeKastilio(new float[] { points[8 + 0].X, points[8 + 1].X, points[8 + 2].X, points[8 + 3].X }, u, 4);
                four3.Y = HelpFunctions.DeKastilio(new float[] { points[8 + 0].Y, points[8 + 1].Y, points[8 + 2].Y, points[8 + 3].Y }, u, 4);
                four3.Z = HelpFunctions.DeKastilio(new float[] { points[8 + 0].Z, points[8 + 1].Z, points[8 + 2].Z, points[8 + 3].Z }, u, 4);

                four4.X = HelpFunctions.DeKastilio(new float[] { points[12 + 0].X, points[12 + 1].X, points[12 + 2].X, points[12 + 3].X }, u, 4);
                four4.Y = HelpFunctions.DeKastilio(new float[] { points[12 + 0].Y, points[12 + 1].Y, points[12 + 2].Y, points[12 + 3].Y }, u, 4);
                four4.Z = HelpFunctions.DeKastilio(new float[] { points[12 + 0].Z, points[12 + 1].Z, points[12 + 2].Z, points[12 + 3].Z }, u, 4);

                for (float v = 0; v <= 1; v += 1.0f / ConfigurationData.BezierBoxDivisions)
                {
                    Vector3 resultPoint = new Vector3();
                    resultPoint.X = HelpFunctions.DeKastilio(new float[] { four1.X, four2.X, four3.X, four4.X }, v, 4);
                    resultPoint.Y = HelpFunctions.DeKastilio(new float[] { four1.Y, four2.Y, four3.Y, four4.Y }, v, 4);
                    resultPoint.Z = HelpFunctions.DeKastilio(new float[] { four1.Z, four2.Z, four3.Z, four4.Z }, v, 4);
                    allPatchPoints[(int)(u * ConfigurationData.BezierBoxDivisions) + (int)(v * ConfigurationData.BezierBoxDivisions)] = resultPoint;
                }
            }

            return allPatchPoints;
        }

        private uint[] GenerateIndices()
        {
            List<uint> uints = new List<uint>();
            for(uint i=0;i< ConfigurationData.BezierBoxDivisions; i++)
            {
                for (uint j = 0; j < ConfigurationData.BezierBoxDivisions; j++)
                {
                    uint shift = j * 51;
                    uints.Add(i + shift);
                    uints.Add(i + 1 + shift);
                    uints.Add(i + 51 + shift);

                    uints.Add(i + shift);
                    uints.Add(i + 1 + shift);
                    uints.Add(i + 52 + shift);
                }
            }

            return uints.ToArray();
        }
    }
}
