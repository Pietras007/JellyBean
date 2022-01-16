using Geometric2.ModelGeneration;
using OpenTK;
using System;

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

        //new physics data
        public float SpringStiffness = 1.0f;
        public float ControlSpringStiffness = 10.0f;
        public float FrictionCoefficient = 0.0f;
        public float ControlPointMass = 1.0f;
        public float RandomVelocityScale = 0.0f;
    }
}
