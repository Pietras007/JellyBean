﻿using System;
using System.Collections.Generic;
using System.Linq;
using Geometric2.Global;
using OpenTK;

namespace Geometric2.Physics
{
    public class ControlFrame
    {
        public ControlPoint[] ControlPoints;

        public ControlPoint[] ControlFramePoints;

        public Spring[] Springs;

        /// <summary>
        /// Springs between vertices of control frame and external vertices of cube
        /// </summary>
        public Spring[] ControlSprings;

        public void Initialize(float mass)
        {
            ControlPoints = GenerateControlPoints(mass);
            ControlFramePoints = GenerateControlFramePoints(ControlPoints);
            Springs = GenerateSprings(ControlPoints);
            ControlSprings = GenerateControlSprings(ControlFramePoints, ControlPoints);
        }

        public void CalculateNextStep(float deltaTime, float springStiffness, float controlSpringStiffness, float frictionCoefficient)
        {
            foreach (var spring in Springs)
            {
                spring.CalculateNextForce(springStiffness);
            }

            //TODO: uncomment later
            //foreach (var spring in ControlSprings)
            //{
            //    spring.CalculateNextForce(controlSpringStiffness);
            //}

            foreach (var point in ControlPoints)
            {
                point.ApplyFriction(frictionCoefficient);
                point.CalculateNextStep(deltaTime);
            }
        }

        public void UpdateMass(float mass)
        {
            foreach (var point in ControlPoints)
            {
                point.Mass = mass;
            }
        }

        public void UpdateControlFramePointsPositions(Vector3[] positions)
        {
            for (int i = 0; i < ControlFramePoints.Length; i++)
            {
                ControlFramePoints[i].Data.Position = positions[i];
            }
        }

        public Vector3[] GetControlPointsPositions()
        {
            return ControlPoints.Select(point => point.Data.Position).ToArray();
        }

        /// <summary>
        /// All external vertex indices of control points
        /// </summary>
        public static readonly int[] ControlFramePointsIndices = { 0, 3, 12, 15, 48, 51, 60, 63 };

        public static ControlPoint[] GenerateControlPoints(float mass)
        {
            var externalVertexPosition = -ConfigurationData.ControlFrameCubeEdgeLength / 2.0f;
            var deltaVertexPosition = ConfigurationData.ControlFrameCubeEdgeLength / 3.0f;
            var controlPoints = new List<ControlPoint>(64);
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    for (int k = 0; k < 4; k++)
                    {
                        var position = new Vector3(
                            externalVertexPosition + i * deltaVertexPosition,
                            externalVertexPosition + j * deltaVertexPosition,
                            externalVertexPosition + k * deltaVertexPosition);
                        var velocity = CalculateInitialVelocity();
                        controlPoints.Add(new ControlPoint(position, velocity, mass));
                    }
                }
            }

            return controlPoints.ToArray();
        }

        public static Vector3 CalculateInitialVelocity()
        {
            return Vector3.Zero;
        }

        public static Spring[] GenerateSprings(ControlPoint[] controlPoints)
        {
            var edgeSpringLength = ConfigurationData.ControlFrameCubeEdgeLength / 3.0f;
            var diagonalSpringLength = edgeSpringLength * (float)Math.Sqrt(2d);
            var springs = new List<Spring>();

            //indices that are on the top of the cube
            var topCubeIndices = new[] { 12, 13, 14, 15, 28, 29, 30, 31, 44, 45, 46, 47, 60, 61, 62, 63 };

            //generate all possible strings for each point
            for (int i = 0; i < controlPoints.Length; i++)
            {
                //p - p+1
                if (i + 1 < controlPoints.Length && i % 4 != 3)
                {
                    springs.Add(new Spring(controlPoints[i], controlPoints[i + 1], edgeSpringLength));
                }

                //p - p+4
                if (i + 4 < controlPoints.Length && !topCubeIndices.Contains(i))
                {
                    springs.Add(new Spring(controlPoints[i], controlPoints[i + 4], edgeSpringLength));
                }

                //p - p+5
                if (i + 5 < controlPoints.Length && i % 4 != 3 && !topCubeIndices.Contains(i))
                {
                    springs.Add(new Spring(controlPoints[i], controlPoints[i + 5], diagonalSpringLength));
                }

                //p - p+16
                if (i + 16 < controlPoints.Length)
                {
                    springs.Add(new Spring(controlPoints[i], controlPoints[i + 16], edgeSpringLength));
                }

                //p - p+17
                if (i + 17 < controlPoints.Length && i % 4 != 3)
                {
                    springs.Add(new Spring(controlPoints[i], controlPoints[i + 17], diagonalSpringLength));
                }

                //p - p+20
                if (i + 20 < controlPoints.Length && !topCubeIndices.Contains(i))
                {
                    springs.Add(new Spring(controlPoints[i], controlPoints[i + 20], diagonalSpringLength));
                }

                //p+1 - p+4
                if (i + 1 < controlPoints.Length && i + 4 < controlPoints.Length && i % 4 != 3 && !topCubeIndices.Contains(i))
                {
                    springs.Add(new Spring(controlPoints[i + 1], controlPoints[i + 4], diagonalSpringLength));
                }

                //p+1 - p+16
                if (i + 1 < controlPoints.Length && i + 16 < controlPoints.Length && i % 4 != 3)
                {
                    springs.Add(new Spring(controlPoints[i + 1], controlPoints[i + 16], diagonalSpringLength));
                }

                //p+4 - p+16
                if (i + 4 < controlPoints.Length && i + 16 < controlPoints.Length && !topCubeIndices.Contains(i))
                {
                    springs.Add(new Spring(controlPoints[i + 4], controlPoints[i + 16], diagonalSpringLength));
                }
            }

            return springs.ToArray();
        }

        public static ControlPoint[] GenerateControlFramePoints(ControlPoint[] controlPoints)
        {
            var controlFramePoints = new ControlPoint[ControlFramePointsIndices.Length];

            for (int i = 0; i < controlFramePoints.Length; i++)
            {
                controlFramePoints[i] = new ControlPoint(controlPoints[ControlFramePointsIndices[i]].LastData.Position, Vector3.Zero, 0f);
            }

            return controlFramePoints;
        }

        public static Spring[] GenerateControlSprings(ControlPoint[] controlFramePoints, ControlPoint[] controlPoints)
        {
            //all external vertex indices of control points 
            var controlSprings = new Spring[ControlFramePointsIndices.Length];

            for (int i = 0; i < controlSprings.Length; i++)
            {
                controlSprings[i] = new Spring(controlFramePoints[i], controlPoints[ControlFramePointsIndices[i]], 0f);
            }

            return controlSprings;
        }
    }
}
