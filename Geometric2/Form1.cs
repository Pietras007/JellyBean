using Geometric2.Global;
using Geometric2.ModelGeneration;
using Geometric2.RasterizationClasses;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Geometric2.Models;
using Geometric2.Physics;
using OpenTK;

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

            controlFrame = new ControlFrame(globalPhysicsData.ControlPointMass, globalPhysicsData.RandomVelocityScale);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cameraLightCheckBox.Checked = true;
            StartSimulation();

            var model = ModelReader.ReadObjModelFromFile(@"../../../ModelsData/bunny.obj");
        }

        private Thread SimulationThread = null;

        private Shader _shaderLight;
        private Shader _shader;
        private Camera _camera;
        private bool cameraLight = true;

        private XyzLines xyzLines = new XyzLines();
        private Cube cube = new Cube();
        private List<Element> Elements = new List<Element>();

        private Lines boxLines = new Lines();
        private Lines controlFrameLines = new Lines();
        private Lines controlPointLines = new Lines();

        int prev_xPosMouse = -1, prev_yPosMouse = -1;
        GlobalPhysicsData globalPhysicsData = new GlobalPhysicsData();

        private ControlFrame controlFrame;

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

        private void gravityOnCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            globalPhysicsData.gravityOn = gravityOnCheckBox.Checked;
        }

        private void cubeEdgeLengthNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            globalPhysicsData.ControlPointMass = (float)cubeEdgeLengthNumericUpDown.Value;
            controlFrame?.UpdateMass(globalPhysicsData.ControlPointMass);
        }

        private void cubeDensityNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            globalPhysicsData.SpringStiffness = (float)cubeDensityNumericUpDown.Value;
        }

        private void cubeDeviationNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            globalPhysicsData.FrictionCoefficient = (float)cubeDeviationNumericUpDown.Value;
        }

        private void angularVelocityNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            globalPhysicsData.ControlSpringStiffness = (float)angularVelocityNumericUpDown.Value;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            globalPhysicsData.RandomVelocityScale = (float)randomVelocityScaleUpDown1.Value;
        }

        private void integrationStepNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            globalPhysicsData.integrationStep = (float)integrationStepNumericUpDown.Value;
        }

        private void collisionCoefficientNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            globalPhysicsData.CollisionCoefficient = (float)collisionCoefficientNumericUpDown.Value;
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            EndSimulation();

            globalPhysicsData.Translation = Vector3.Zero;
            globalPhysicsData.ResetControlPointsPositions();
            controlFrame = new ControlFrame(globalPhysicsData.ControlPointMass, globalPhysicsData.RandomVelocityScale);

            StartSimulation();
        }

        private void shakeButton_Click(object sender, EventArgs e)
        {
            controlFrame?.ShakeCube(globalPhysicsData.RandomVelocityScale);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            isProgramWorking = false;
        }

        private void collisionModel_Model1_CheckedChanged(object sender, EventArgs e)
        {
            if (collisionModel_Model1.Checked && globalPhysicsData.CollisionModel != CollisionModel.Model1)
            {
                globalPhysicsData.CollisionModel = CollisionModel.Model1;
            }
        }

        private void collisionModel_Model2_CheckedChanged(object sender, EventArgs e)
        {
            if (collisionModel_Model2.Checked && globalPhysicsData.CollisionModel != CollisionModel.Model2)
            {
                globalPhysicsData.CollisionModel = CollisionModel.Model2;
            }
        }


        private void StartSimulation()
        {
            if (SimulationThread != null)
            {
                SimulationThread.Abort();
            }

            this.GlobalCalculationFunction();
        }

        private void EndSimulation()
        {
            if (SimulationThread != null)
            {
                SimulationThread.Abort();
            }
        }

        private void GlobalCalculationFunction()
        {
            SimulationThread = new Thread(() =>
            {
                while (isProgramWorking)
                {
                    long nanosecondsToWait = (long)(globalPhysicsData.integrationStep * 1000_000_000);
                    long nanoPrev = 10000L * Stopwatch.GetTimestamp();
                    nanoPrev /= TimeSpan.TicksPerMillisecond;
                    nanoPrev *= 100L;

                    controlFrame.UpdateControlFramePointsPositions(globalPhysicsData.controlFramePointsPositions);

                    controlFrame.CalculateNextStep(globalPhysicsData);

                    var positions = controlFrame.GetControlPointsPositions();

                    for (int i = 0; i < globalPhysicsData.points.Length; i++)
                    {
                        globalPhysicsData.points[i].CenterPosition = positions[i];
                    }

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
    }
}
