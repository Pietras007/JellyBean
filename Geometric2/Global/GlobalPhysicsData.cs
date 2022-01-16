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
        public float pointMass;
        public float resilience_c1;
        public float resilience_c2;
        public float tenacityRate_k;
        public float integrationStep = 0.001f;

        //Visualization Settings
        public bool displayControlPoints = true;
        public bool displayControlFrame = true;
        public bool displayBox = true;
        public bool displayBezierCube = true;
        public bool displayDisortBox = true;

        //Data from user
        public bool gravityOn = false;

        //Visualization Data
        public double diagonalRoundInRadianX = 0.0;
        public double diagonalRoundInRadianY = 0.0;
        public double diagonalRoundInRadianZ = 0.0;
        public double yRoundInRadian = 0.0;
        public Quaternion rotationQuaternion = Quaternion.Identity;
        public Quaternion rotationQuaternionInitial = Quaternion.Identity;

        //physics data
        public Vector3d gravitation = new Vector3d(0, -9.81, 0);
        public Quaterniond gravitationQuaternion = new Quaterniond(0, -9.81, 0, 0);


        //new physics data
        public float SpringStiffness = 1.0f;
        public float ControlSpringStiffness = 10.0f;
        public float FrictionCoefficient = 1.0f;
        public float ControlPointMass = 1.0f;
        public float RandomVelocityScale = 0.0f;
    }
}
