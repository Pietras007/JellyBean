using System;
using System.Collections.Generic;
using System.Drawing;
using Geometric2.Global;
using Geometric2.Helpers;
using Geometric2.MatrixHelpers;
using Geometric2.RasterizationClasses;
using OpenTK;
using OpenTK.Graphics.OpenGL4;

namespace Geometric2.ModelGeneration
{
    public class Lines : Element
    {
        public bool IsBox { get; set; }
        public bool IsControlFrame { get; set; }
        public bool IsMoveLine { get; set; }
        public List<Vector3> linePointsList = new List<Vector3>();
        public float[] linesPoints = new float[] { };
        public uint[] linesIndices = new uint[] { };
        int linesVBO, linesVAO, linesEBO;
        public bool setEBODirectly = false;


        public override void CreateGlElement(Shader _shader, Shader _shaderLight)
        {
            if (IsBox)
            {
                GenerateOnlyPoints();
            }

            if (IsControlFrame)
            {
                GenerateOnlyPoints();
            }

            //else
            //{
            //    GenerateLines();
            //}

            _shader.Use();
            var a_Position_Location = _shader.GetAttribLocation("a_Position");
            linesVAO = GL.GenVertexArray();
            linesVBO = GL.GenBuffer();
            linesEBO = GL.GenBuffer();
            GL.BindVertexArray(linesVAO);
            GL.BindBuffer(BufferTarget.ArrayBuffer, linesVBO);
            GL.BufferData(BufferTarget.ArrayBuffer, linesPoints.Length * sizeof(float), linesPoints, BufferUsageHint.DynamicDraw);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, linesEBO);
            GL.BufferData(BufferTarget.ElementArrayBuffer, linesIndices.Length * sizeof(uint), linesIndices, BufferUsageHint.DynamicDraw);
            GL.VertexAttribPointer(a_Position_Location, 3, VertexAttribPointerType.Float, true, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(a_Position_Location);
        }

        public override void RenderGlElement(Shader _shader, Shader _shaderLight, Vector3 rotationCentre, GlobalPhysicsData globalPhysicsData)
        {
            if (IsControlFrame)
            {
                Translation = globalPhysicsData.Translation;
            }

            _shader.Use();
            var cubeSize = (float)globalPhysicsData.InitialConditionsData.cubeEdgeLength;
            Matrix4 model = ModelMatrix.CreateModelMatrix(new Vector3(cubeSize, cubeSize, cubeSize), RotationQuaternion, CenterPosition + Translation, rotationCentre, TempRotationQuaternion);
            _shader.SetMatrix4("model", model);
            GL.BindVertexArray(linesVAO);

            _shader.SetVector3("fragmentColor", ColorHelper.ColorToVector(Color.Black));
            GL.DrawElements(PrimitiveType.Lines, linesIndices.Length, DrawElementsType.UnsignedInt, 0 * sizeof(int));

            GL.BindVertexArray(0);
        }

        private void FillLineGeometry()
        {
            GL.BindVertexArray(linesVAO);
            GL.BindBuffer(BufferTarget.ArrayBuffer, linesVBO);
            GL.BufferData(BufferTarget.ArrayBuffer, linesPoints.Length * sizeof(float), linesPoints, BufferUsageHint.DynamicDraw);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, linesEBO);
            GL.BufferData(BufferTarget.ElementArrayBuffer, linesIndices.Length * sizeof(uint), linesIndices, BufferUsageHint.DynamicDraw);
        }

        private void GenerateLines()
        {
            var tempLinesPoints = new float[3 * linePointsList.Count];
            var tempLinesIndices = new uint[linePointsList.Count];
            int idx = 0;
            var tempLinePointsReference = linePointsList;
            foreach (var p in tempLinePointsReference)
            {
                tempLinesPoints[3 * idx] = p.X;
                tempLinesPoints[3 * idx + 1] = p.Y;
                tempLinesPoints[3 * idx + 2] = p.Z;
                tempLinesIndices[idx] = (uint)idx;
                idx++;
            }

            linesPoints = tempLinesPoints;
            linesIndices = tempLinesIndices;
        }

        private void GenerateOnlyPoints()
        {
            linesPoints = new float[3 * linePointsList.Count];
            int idx = 0;
            foreach (var p in linePointsList)
            {
                linesPoints[3 * idx] = p.X;
                linesPoints[3 * idx + 1] = p.Y;
                linesPoints[3 * idx + 2] = p.Z;
                idx++;
            }
        }
    }
}
