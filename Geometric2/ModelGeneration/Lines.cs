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


        public override void CreateGlElement(Shader _shader, Shader _shaderLight)
        {
            if (IsBox)
            {
                GenerateOnlyPoints();
            }

            GenerateControlFramePoints(null, new Vector3(0, 0, 0), IsControlFrame);

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
                RenderWithColor(_shader, Color.Black, rotationCentre);
            }

            if (IsControlFrame && globalPhysicsData.displayControlFrame)
            {
                GenerateControlFramePoints(globalPhysicsData, globalPhysicsData.Translation);
                FillLineGeometry();
                RenderWithColor(_shader, Color.Black, rotationCentre);
            }
            else
            {
                GenerateControlFramePoints(globalPhysicsData, globalPhysicsData.Translation, false);
            }

            if (IsControlPoints && globalPhysicsData.displayControlPoints)
            {
                GenerateControlLines(globalPhysicsData);
                FillLineGeometry();
                RenderWithColor(_shader, Color.OrangeRed, rotationCentre);
            }
        }

        private void RenderWithColor(Shader _shader, Color color, Vector3 rotationCentre)
        {
            _shader.Use();
            Matrix4 model = ModelMatrix.CreateModelMatrix(new Vector3(1.0f, 1.0f, 1.0f), RotationQuaternion, CenterPosition + Translation, rotationCentre, TempRotationQuaternion);
            _shader.SetMatrix4("model", model);
            GL.BindVertexArray(linesVAO);
            _shader.SetVector3("fragmentColor", ColorHelper.ColorToVector(color));
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

        private void GenerateControlFramePoints(GlobalPhysicsData globalPhysicsData, Vector3 translation, bool draw = true)
        {
            if (globalPhysicsData != null)
            {
                var x = ConfigurationData.ControlFrameCubeEdgeLength / 2.0f;
                linePointsList = new List<Vector3>()
                {
                    new Vector3(-x, -x, -x) + translation,
                    new Vector3(-x, -x, x) + translation,
                    new Vector3(x, -x, x) + translation,
                    new Vector3(x, -x, -x) + translation,

                    new Vector3(-x, x, -x) + translation,
                    new Vector3(-x, x, x) + translation,
                    new Vector3(x, x, x) + translation,
                    new Vector3(x, x, -x) + translation
                };

                MapControlFramePointsPositions(globalPhysicsData, linePointsList);

                linePointsList.Add(globalPhysicsData.points[0].Position());//8
                linePointsList.Add(globalPhysicsData.points[3].Position());//9
                linePointsList.Add(globalPhysicsData.points[12].Position());//10
                linePointsList.Add(globalPhysicsData.points[15].Position());//11

                linePointsList.Add(globalPhysicsData.points[48].Position());//12
                linePointsList.Add(globalPhysicsData.points[51].Position());//13
                linePointsList.Add(globalPhysicsData.points[60].Position());//14
                linePointsList.Add(globalPhysicsData.points[63].Position());//15

                if (draw)
                {
                    GenerateOnlyPoints();
                    linesIndices = new uint[]
                    {
                        0, 1, 1, 2, 2, 3, 3, 0, 4, 5, 5, 6, 6, 7, 7, 4, 0, 4, 1, 5, 2, 6, 3, 7,
                        0, 8, 1, 9, 4, 10, 5, 11, 2, 13, 3, 12, 6, 15, 7, 14
                    };
                }
            }
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

                for (uint i = 0; i < 4; i++)
                {
                    for (uint j = 0; j < 4; j++)
                    {
                        for (uint k = 0; k < 4; k++)
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

        private void MapControlFramePointsPositions(GlobalPhysicsData globalPhysicsData, List<Vector3> controlFramePointsPositions)
        {
            globalPhysicsData.controlFramePointsPositions[0] = linePointsList[0];
            globalPhysicsData.controlFramePointsPositions[1] = linePointsList[1];
            globalPhysicsData.controlFramePointsPositions[2] = linePointsList[4];
            globalPhysicsData.controlFramePointsPositions[3] = linePointsList[5];
            globalPhysicsData.controlFramePointsPositions[4] = linePointsList[3];
            globalPhysicsData.controlFramePointsPositions[5] = linePointsList[2];
            globalPhysicsData.controlFramePointsPositions[6] = linePointsList[7];
            globalPhysicsData.controlFramePointsPositions[7] = linePointsList[6];
        }
    }
}
