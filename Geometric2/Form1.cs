using Geometric2.Global;
using Geometric2.Helpers;
using Geometric2.ModelGeneration;
using Geometric2.RasterizationClasses;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

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
        }

        private Thread SimulationThread = null;
        private Thread PointListThread = null;

        private Shader _shaderLight;
        private Shader _shader;
        private Camera _camera;
        private bool cameraLight = true;

        private XyzLines xyzLines = new XyzLines();
        private Cube cube = new Cube();
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

        private void gravityOnCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            globalPhysicsData.gravityOn = gravityOnCheckBox.Checked;
        }

        private void applyConditionsButton_Click(object sender, EventArgs e)
        {
        }

        private void cubeEdgeLengthNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            globalPhysicsData.pointMass = (float)cubeEdgeLengthNumericUpDown.Value;
        }

        private void cubeDensityNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            globalPhysicsData.resilience_c1 = (float)cubeDensityNumericUpDown.Value;
        }

        private void cubeDeviationNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            globalPhysicsData.tenacityRate_k = (float)cubeDeviationNumericUpDown.Value;
        }

        private void angularVelocityNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            globalPhysicsData.resilience_c2 = (float)angularVelocityNumericUpDown.Value;
        }

        private void integrationStepNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            globalPhysicsData.integrationStep = (float)integrationStepNumericUpDown.Value;
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

                    //var deltaTime = globalPhysicsData.InitialConditionsData.integrationStep;
                    //var I = globalPhysicsData.InitialConditionsData.inertiaTensor;
                    //var W = physicsStepData.angularVelocity;
                    //var Q = physicsStepData.quaternion;

                    //bool rungeKutta = true;

                    ////Rk4
                    //if (rungeKutta)
                    //{
                    //    //k1
                    //    var N_k1 = CalculateN(globalPhysicsData, ref Q);

                    //    var W_k1 = deltaTime * CalculateW(N_k1, I, W);
                    //    var Q_k1 = deltaTime * CalculateQ(W, Q);

                    //    var WW_k1 = W + W_k1 * 0.5;
                    //    var QQ_k1 = (Q + Q_k1 * 0.5).Normalized();
                    //    //k2
                    //    var N_k2 = CalculateN(globalPhysicsData, ref QQ_k1);

                    //    var W_k2 = deltaTime * CalculateW(N_k2, I, WW_k1);
                    //    var Q_k2 = deltaTime * CalculateQ(WW_k1, QQ_k1);

                    //    var WW_k2 = W + W_k2 * 0.5;
                    //    var QQ_k2 = (Q + Q_k2 * 0.5).Normalized();
                    //    //k3
                    //    var N_k3 = CalculateN(globalPhysicsData, ref QQ_k2);

                    //    var W_k3 = deltaTime * CalculateW(N_k3, I, WW_k2);
                    //    var Q_k3 = deltaTime * CalculateQ(WW_k2, QQ_k2);

                    //    var WW_k3 = W + W_k3;
                    //    var QQ_k3 = (Q + Q_k3).Normalized();
                    //    //k4
                    //    var N_k4 = CalculateN(globalPhysicsData, ref QQ_k3);

                    //    var W_k4 = deltaTime * CalculateW(N_k4, I, WW_k3);
                    //    var Q_k4 = deltaTime * CalculateQ(WW_k3, QQ_k3);

                    //    var W_new = W + (1d / 6d) * W_k1 + (1d / 3d) * W_k2 + (1d / 3d) * W_k3 + (1d / 6d) * W_k4;
                    //    var Q_new = Q + (1d / 6d) * Q_k1 + (1d / 3d) * Q_k2 + (1d / 3d) * Q_k3 + (1d / 6d) * Q_k4;

                    //    Q_new.Normalize();

                    //    globalPhysicsData.rotationQuaternion = Q_new.ConvertToQuaternion();

                    //    physicsStepData.angularVelocity = W_new;
                    //    physicsStepData.quaternion = Q_new;
                    //}
                    ////euler
                    //else
                    //{
                    //    var N = CalculateN(globalPhysicsData, ref Q);

                    //    double W_X = ((N.X + (I.Y - I.Z) * W.Y * W.Z) / I.X) * deltaTime + W.X;
                    //    double W_Y = ((N.Y + (I.Z - I.X) * W.X * W.Z) / I.Y) * deltaTime + W.Y;
                    //    double W_Z = ((N.Z + (I.X - I.Y) * W.X * W.Y) / I.Z) * deltaTime + W.Z;

                    //    Quaterniond Q_new = 0.5 * Q * new Quaterniond(W_X, W_Y, W_Z, 0) * deltaTime + Q;

                    //    physicsStepData.angularVelocity = new Vector3d(W_X, W_Y, W_Z);
                    //    physicsStepData.quaternion = Q_new;

                    //    globalPhysicsData.rotationQuaternion = Q_new.ConvertToQuaternion();
                    //}

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
    }
}
