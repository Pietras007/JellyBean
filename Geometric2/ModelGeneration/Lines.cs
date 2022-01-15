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
        public bool IsControlPoints { get; set; }

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
                GenerateControlFramePoints(null);
            }

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
            if (IsBox && globalPhysicsData.displayBox)
            {
                _shader.Use();
                var cubeSize = (float)globalPhysicsData.InitialConditionsData.cubeEdgeLength;
                Matrix4 model = ModelMatrix.CreateModelMatrix(new Vector3(cubeSize, cubeSize, cubeSize), RotationQuaternion, CenterPosition + Translation, rotationCentre, TempRotationQuaternion);
                _shader.SetMatrix4("model", model);
                GL.BindVertexArray(linesVAO);
                _shader.SetVector3("fragmentColor", ColorHelper.ColorToVector(Color.Black));
                GL.DrawElements(PrimitiveType.Lines, linesIndices.Length, DrawElementsType.UnsignedInt, 0 * sizeof(int));
                GL.BindVertexArray(0);
            }

            if(IsControlFrame && globalPhysicsData.displayControlFrame)
            {
                Translation = globalPhysicsData.Translation;
                GenerateControlFramePoints(globalPhysicsData);
                FillLineGeometry();

                _shader.Use();
                var cubeSize = (float)globalPhysicsData.InitialConditionsData.cubeEdgeLength;
                Matrix4 model = ModelMatrix.CreateModelMatrix(new Vector3(cubeSize, cubeSize, cubeSize), RotationQuaternion, CenterPosition + Translation, rotationCentre, TempRotationQuaternion);
                _shader.SetMatrix4("model", model);
                GL.BindVertexArray(linesVAO);
                _shader.SetVector3("fragmentColor", ColorHelper.ColorToVector(Color.Black));
                GL.DrawElements(PrimitiveType.Lines, linesIndices.Length, DrawElementsType.UnsignedInt, 0 * sizeof(int));
                GL.BindVertexArray(0);
            }

            if (IsControlPoints && globalPhysicsData.displayControlPoints)
            {
                GenerateControlLines(globalPhysicsData);
                FillLineGeometry();
                _shader.Use();
                var cubeSize = (float)globalPhysicsData.InitialConditionsData.cubeEdgeLength;
                Matrix4 model = ModelMatrix.CreateModelMatrix(new Vector3(cubeSize, cubeSize, cubeSize), RotationQuaternion, CenterPosition + Translation, rotationCentre, TempRotationQuaternion);
                _shader.SetMatrix4("model", model);
                GL.BindVertexArray(linesVAO);
                _shader.SetVector3("fragmentColor", ColorHelper.ColorToVector(Color.OrangeRed));
                GL.DrawElements(PrimitiveType.Lines, linesIndices.Length, DrawElementsType.UnsignedInt, 0 * sizeof(int));
                GL.BindVertexArray(0);
            }
        }

        private void FillLineGeometry()
        {
            GL.BindVertexArray(linesVAO);
            GL.BindBuffer(BufferTarget.ArrayBuffer, linesVBO);
            GL.BufferData(BufferTarget.ArrayBuffer, linesPoints.Length * sizeof(float), linesPoints, BufferUsageHint.DynamicDraw);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, linesEBO);
            GL.BufferData(BufferTarget.ElementArrayBuffer, linesIndices.Length * sizeof(uint), linesIndices, BufferUsageHint.DynamicDraw);
        }

        private void GenerateControlFramePoints(GlobalPhysicsData globalPhysicsData)
        {
            if (globalPhysicsData != null)
            {
                linePointsList = new List<Vector3>()
                {
                    new Vector3(-1.5f, -1.5f, -1.5f),
                    new Vector3(-1.5f, -1.5f, 1.5f),
                    new Vector3(1.5f, -1.5f, 1.5f),
                    new Vector3(1.5f, -1.5f, -1.5f),

                    new Vector3(-1.5f, 1.5f, -1.5f),
                    new Vector3(-1.5f, 1.5f, 1.5f),
                    new Vector3(1.5f, 1.5f, 1.5f),
                    new Vector3(1.5f, 1.5f, -1.5f)
                };



                GenerateOnlyPoints();
                linesIndices = new uint[] { 0, 1, 1, 2, 2, 3, 3, 0, 4, 5, 5, 6, 6, 7, 7, 4, 0, 4, 1, 5, 2, 6, 3, 7 };
            }
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

        private void GenerateControlLines(GlobalPhysicsData globalPhysicsData)
        {
            if (globalPhysicsData != null)
            {
                linePointsList.Clear();
                List<uint> indices = new List<uint>();
                foreach (var p in globalPhysicsData.points)
                {
                    linePointsList.Add(p.Position());
                }
                GenerateOnlyPoints();

                for (uint i=0;i<4;i++)
                {
                    for(uint j =0;j<4;j++)
                    {
                        for(uint k = 0; k < 4; k++)
                        {
                            if (k < 3)
                            {
                                indices.Add(i * 16 + j * 4 + k);
                                indices.Add(i * 16 + j * 4 + k + 1);
                            }
                        }
                    }
                }

                for (uint i = 0; i < 16; i++)
                {
                    indices.Add(i);
                    indices.Add(i + 16);
                    indices.Add(i + 16);
                    indices.Add(i + 32);
                    indices.Add(i + 32);
                    indices.Add(i + 48);
                }

                for (uint i = 0; i < 4; i++)
                {
                    for (uint j = 0; j < 4; j++)
                    {
                        uint idx = 16 * i + j;
                        indices.Add(idx);
                        indices.Add(idx + 4);
                        indices.Add(idx + 4);
                        indices.Add(idx + 8);
                        indices.Add(idx + 8);
                        indices.Add(idx + 12);
                    }
                }

                linesIndices = indices.ToArray();
            }
        }
    }
}
