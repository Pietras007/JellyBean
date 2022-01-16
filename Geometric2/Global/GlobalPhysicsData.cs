using Geometric2.ModelGeneration;
using OpenTK;
using System.Collections.Generic;
using Geometric2.RasterizationClasses;

namespace Geometric2.Global
{
    public class GlobalPhysicsData
    {
        public Point[] points = new Point[64];
        public Vector3[] controlFramePointsPositions = new Vector3[8];

        public Vector3 Translation = new Vector3(0, 0, 0);
        //Help
        public object lockPathPointsList = new object { };

        //Conditions
        public float integrationStep = 0.001f;

        //Visualization Settings
        public bool displayControlPoints = true;
        public bool displayControlFrame = true;
        public bool displayBox = true;
        public bool displayBezierCube = true;
        public bool displayDisortBox = true;

        //Data from user
        public bool gravityOn = false;

        //Physics data
        public float ControlPointMass = 0.1f;
        public float SpringStiffness = 200.0f;
        public float FrictionCoefficient = 1.0f;
        public float ControlSpringStiffness = 1000.0f;
        public float RandomVelocityScale = 0.0f;

        //Collisions
        public CollisionType CollisionType = CollisionType.Elastic;
        public CollisionModel CollisionModel = CollisionModel.Model1;
        public float CollisionCoefficient = 1.0f;

        public void InitializeControlPoints(Camera camera)
        {
            List<Vector3> controlPoints = new List<Vector3>();
            var x = ConfigurationData.ControlFrameCubeEdgeLength / 2.0f;
            var deltaX = ConfigurationData.ControlFrameCubeEdgeLength / 3.0f;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    for (int k = 0; k < 4; k++)
                    {
                        controlPoints.Add(new Vector3(-x + i * deltaX, -x + j * deltaX, -x + k * deltaX));
                    }
                }
            }

            for (int i = 0; i < controlPoints.Count; i++)
            {
                points[i] = new ModelGeneration.Point(controlPoints[i], camera, i);
            }

            int[] controlFramePointsIndices = { 0, 3, 12, 15, 48, 51, 60, 63 };

            for (int i = 0; i < controlFramePointsIndices.Length; i++)
            {
                controlFramePointsPositions[i] = controlPoints[controlFramePointsIndices[i]];
            }
        }

        public void ResetControlPointsPositions()
        {
            List<Vector3> controlPoints = new List<Vector3>();
            var x = ConfigurationData.ControlFrameCubeEdgeLength / 2.0f;
            var deltaX = ConfigurationData.ControlFrameCubeEdgeLength / 3.0f;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    for (int k = 0; k < 4; k++)
                    {
                        controlPoints.Add(new Vector3(-x + i * deltaX, -x + j * deltaX, -x + k * deltaX));
                    }
                }
            }

            for (int i = 0; i < controlPoints.Count; i++)
            {
                points[i].CenterPosition = controlPoints[i];
            }

            int[] controlFramePointsIndices = { 0, 3, 12, 15, 48, 51, 60, 63 };

            for (int i = 0; i < controlFramePointsIndices.Length; i++)
            {
                controlFramePointsPositions[i] = controlPoints[controlFramePointsIndices[i]];
            }
        }
    }

    public enum CollisionType
    {
        Elastic,
        Inelastic
    }

    public enum CollisionModel
    {
        /// <summary>
        /// When colliding with plane x=0 velocity changes from (v_x, v_y, v_z) to u*(-v_x, v_y, v_z)
        /// </summary>
        Model1,

        /// <summary>
        /// When colliding with plane x=0 velocity changes from (v_x, v_y, v_z) to (-u*v_x, v_y, v_z)
        /// </summary>
        Model2
    }
}
