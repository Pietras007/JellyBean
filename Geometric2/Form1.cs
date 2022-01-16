using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Geometric2.RasterizationClasses;
using Geometric2.ModelGeneration;
using OpenTK;
using Geometric2.Helpers;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Geometric2.Global;
using Geometric2.Physics;

namespace Geometric2
{
    public partial class Form1 : Form
    {
        bool isProgramWorking = true;

        public Form1()
        {
            InitializeComponent();
            Thread thread = new Thread(() =>
            {
                while (isProgramWorking)
                {
                    glControl1.Invalidate();
                    Thread.Sleep(16);
                }
            });

            thread.Start();
            this.glControl1.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.glControl1_MouseWheel);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cameraLightCheckBox.Checked = true;

            InitializePhysicsData();
        }

        private Thread SimulationThread = null;
        private Thread PointListThread = null;
        private InitialConditionsData temporaryConditionsData = new InitialConditionsData();

        private Shader _shaderLight;
        private Shader _shader;
        private Camera _camera;
        private bool cameraLight = true;

        private XyzLines xyzLines = new XyzLines();
        private Cube cube = new Cube();
        private Plane plane = new Plane();
        private List<Element> Elements = new List<Element>();
        private Lines diagonalLine = new Lines();
        private Lines cubeLines = new Lines();
        private Lines pathLines = new Lines();

        private Lines boxLines = new Lines();
        private Lines controlFrameLines = new Lines();
        private Lines controlPointLines = new Lines();

        int prev_xPosMouse = -1, prev_yPosMouse = -1;
        GlobalPhysicsData globalPhysicsData = new GlobalPhysicsData();

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void cameraLightCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            cameraLight = cameraLightCheckBox.Checked;
        }

        private void displayCubeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            globalPhysicsData.displayControlFrame = displayControlFrameCheckBox.Checked;
        }

        private void displayWallsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            globalPhysicsData.displayBezierCube = displayBezierCubeBox.Checked;
        }

        private void displayDiagonalCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            globalPhysicsData.displayDisortBox = displayDistortBlockCheckBox.Checked;
        }

        private void displayBoxCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            globalPhysicsData.displayBox = displayBoxCheckBox.Checked;
        }

        private void displayPlaneCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            globalPhysicsData.displayControlPoints = displayControlPointsCheckBox.Checked;
        }

        private void pathLengthUpDown_ValueChanged(object sender, EventArgs e)
        {
            globalPhysicsData.numberOfPointsToShow = (int)pathLengthUpDown.Value;
        }

        private void gravityOnCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            globalPhysicsData.gravityOn = gravityOnCheckBox.Checked;
        }

        private void startSimulationButton_Click(object sender, EventArgs e)
        {

            ////TODO: insert start here
            //var points = ControlFrame.GenerateControlPoints(1f);
            //var springs = ControlFrame.GenerateSprings(points);
            //var diag = springs.Where(s => s.InitialLength > 1.4f).ToArray();
            //var ones = springs.Where(s => s.InitialLength < 1.4f).ToArray();

            endSimulationButton.Enabled = true;
            startSimulationButton.Enabled = false;
            applyConditionsButton.Enabled = false;
            if (SimulationThread != null)
            {
                SimulationThread.Abort();
            }

            if (PointListThread != null)
            {
                PointListThread.Abort();
            }

            this.GlobalCalculationFunction();
            //this.DrawPath();
        }

        private void endSimulationButton_Click(object sender, EventArgs e)
        {
            endSimulationButton.Enabled = false;
            startSimulationButton.Enabled = true;
            applyConditionsButton.Enabled = true;
            if (SimulationThread != null)
            {
                SimulationThread.Abort();
            }

            if (PointListThread != null)
            {
                PointListThread.Abort();
            }
        }

        private void applyConditionsButton_Click(object sender, EventArgs e)
        {
            globalPhysicsData.InitialConditionsData = temporaryConditionsData;
            temporaryConditionsData = new InitialConditionsData();
            temporaryConditionsData.cubeEdgeLength = (double)cubeEdgeLengthNumericUpDown.Value;
            temporaryConditionsData.cubeDensity = (double)cubeDensityNumericUpDown.Value;
            temporaryConditionsData.cubeDeviationRadian = (Math.PI / 180) * (double)cubeDeviationNumericUpDown.Value;
            temporaryConditionsData.angularVelocityRadian = (Math.PI / 180) * (double)angularVelocityNumericUpDown.Value;
            temporaryConditionsData.integrationStep = (float)integrationStepNumericUpDown.Value;

            InitializePhysicsData();
        }

        private void cubeEdgeLengthNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            temporaryConditionsData.cubeEdgeLength = (double)cubeEdgeLengthNumericUpDown.Value;
        }

        private void cubeDensityNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            temporaryConditionsData.cubeDensity = (double)cubeDensityNumericUpDown.Value;
        }

        private void cubeDeviationNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            temporaryConditionsData.cubeDeviationRadian = (Math.PI / 180) * (double)cubeDeviationNumericUpDown.Value;
        }

        private void angularVelocityNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            temporaryConditionsData.angularVelocityRadian = (Math.PI / 180) * (double)angularVelocityNumericUpDown.Value;
        }

        private void integrationStepNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            temporaryConditionsData.integrationStep = (float)integrationStepNumericUpDown.Value;
        }

        private void InitializePhysicsData()
        {
            controlFrame = new ControlFrame();
            controlFrame.Initialize(globalPhysicsData.ControlPointMass);
        }

        private static PhysicsStepData GetInitialPhysicsStepData(GlobalPhysicsData data)
        {
            PhysicsStepData physicsStepData = new PhysicsStepData();
            physicsStepData.quaternion = Quaterniond.FromEulerAngles(0, 0, data.InitialConditionsData.cubeDeviationRadian).Normalized();//changed order of angles due to some issue
            physicsStepData.angularVelocity = data.InitialConditionsData.angularVelocityRadian * Vector3d.UnitY;

            return physicsStepData;
        }

        private struct PhysicsStepData
        {
            public Quaterniond quaternion;
            public Vector3d angularVelocity;
        }
        private static Vector3d CalculateN(GlobalPhysicsData globalPhysicsData, ref Quaterniond quaternion)
        {
            var N = Vector3d.Zero;
            if (globalPhysicsData.gravityOn)
            {
                var gravityRotated = (Quaterniond.Conjugate(quaternion) * globalPhysicsData.gravitationQuaternion * quaternion).Xyz;
                gravityRotated *= globalPhysicsData.InitialConditionsData.mass;
                N = Vector3d.Cross(globalPhysicsData.InitialConditionsData.massCentre, gravityRotated);
            }
            return N;
        }

        private static Vector3d CalculateW(Vector3d N, Vector3d I, Vector3d W)
        {
            double W_X = ((N.X + (I.Y - I.Z) * W.Y * W.Z) / I.X);
            double W_Y = ((N.Y + (I.Z - I.X) * W.X * W.Z) / I.Y);
            double W_Z = ((N.Z + (I.X - I.Y) * W.X * W.Y) / I.Z);
            return new Vector3d(W_X, W_Y, W_Z);
        }

        private static Quaterniond CalculateQ(Vector3d W, Quaterniond Q)
        {
            return 0.5 * Q * new Quaterniond(W, 0);
        }

        private PhysicsStepData physicsStepData;

        private ControlFrame controlFrame;

        private void GlobalCalculationFunction()
        {
            SimulationThread = new Thread(() =>
            {
                while (isProgramWorking)
                {
                    long nanosecondsToWait = (long)(globalPhysicsData.InitialConditionsData.integrationStep * 1000_000_000);
                    long nanoPrev = 10000L * Stopwatch.GetTimestamp();
                    nanoPrev /= TimeSpan.TicksPerMillisecond;
                    nanoPrev *= 100L;

                    var deltaTime = globalPhysicsData.InitialConditionsData.integrationStep;

                    controlFrame.CalculateNextStep(
                        deltaTime,
                        globalPhysicsData.SpringStiffness,
                        globalPhysicsData.ControlSpringStiffness,
                        globalPhysicsData.FrictionCoefficient);

                    //wait for remaining time to pass
                    long nanoPost;
                    while (isProgramWorking)
                    {
                        nanoPost = 10000L * Stopwatch.GetTimestamp();
                        nanoPost /= TimeSpan.TicksPerMillisecond;
                        nanoPost *= 100L;
                        if (nanoPost - nanoPrev > nanosecondsToWait)
                        {
                            break;
                        }
                    }
                }
            });

            SimulationThread.Start();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            isProgramWorking = false;
        }


        private void DrawPath()
        {
            PointListThread = new Thread(() =>
            {
                var topPoint = new Vector3(0, (float)Math.Sqrt(3), 0);
                var topPointInModelSpace = new Vector4(topPoint, 1.0f) * CreateModelMatrix.CreateMatrixForPoint(globalPhysicsData);
                List<Vector3> newPointList = new List<Vector3>();
                for (int i = 0; i < 1000_000; i++)
                {
                    newPointList.Add(new Vector3(topPointInModelSpace));
                }

                lock (globalPhysicsData.lockPathPointsList)
                {
                    pathLines.linePointsList = newPointList;
                }

                while (isProgramWorking)
                {
                    topPointInModelSpace = new Vector4(topPoint, 1.0f) * CreateModelMatrix.CreateMatrixForPoint(globalPhysicsData);
                    pathLines.linePointsList.Add(new Vector3(topPointInModelSpace));
                    Thread.Sleep(10);
                }
            });

            PointListThread.Start();
        }
    }
}
