
namespace Geometric2
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.glControl1 = new OpenTK.GLControl();
            this.loadModelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cameraLightCheckBox = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.collisionModel_Model2 = new System.Windows.Forms.RadioButton();
            this.collisionModel_Model1 = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.collisionCoefficientNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.shakeButton = new System.Windows.Forms.Button();
            this.resetButton = new System.Windows.Forms.Button();
            this.randomVelocityScaleUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.displayBoxCheckBox = new System.Windows.Forms.CheckBox();
            this.displayBezierCubeBox = new System.Windows.Forms.CheckBox();
            this.integrationStepNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.angularVelocityNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.cubeDeviationNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.cubeDensityNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.cubeEdgeLengthNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.integrationstepLabel = new System.Windows.Forms.Label();
            this.resilience2Label = new System.Windows.Forms.Label();
            this.tenacityRateLabel = new System.Windows.Forms.Label();
            this.resilienceLabel = new System.Windows.Forms.Label();
            this.displayControlPointsCheckBox = new System.Windows.Forms.CheckBox();
            this.displayDistortBlockCheckBox = new System.Windows.Forms.CheckBox();
            this.displayControlFrameCheckBox = new System.Windows.Forms.CheckBox();
            this.massLabel = new System.Windows.Forms.Label();
            this.otherLabel = new System.Windows.Forms.Label();
            this.visualizationLabel = new System.Windows.Forms.Label();
            this.initialConditionsLabel = new System.Windows.Forms.Label();
            this.trackBarX = new System.Windows.Forms.TrackBar();
            this.roundLabel = new System.Windows.Forms.Label();
            this.trackBarY = new System.Windows.Forms.TrackBar();
            this.trackBarZ = new System.Windows.Forms.TrackBar();
            this.labelX = new System.Windows.Forms.Label();
            this.labelY = new System.Windows.Forms.Label();
            this.labelZ = new System.Windows.Forms.Label();
            this.menuStrip2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.collisionCoefficientNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.randomVelocityScaleUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.integrationStepNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.angularVelocityNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cubeDeviationNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cubeDensityNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cubeEdgeLengthNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarZ)).BeginInit();
            this.SuspendLayout();
            // 
            // glControl1
            // 
            this.glControl1.BackColor = System.Drawing.Color.Black;
            this.glControl1.Location = new System.Drawing.Point(12, 24);
            this.glControl1.Name = "glControl1";
            this.glControl1.Size = new System.Drawing.Size(1280, 896);
            this.glControl1.TabIndex = 0;
            this.glControl1.VSync = false;
            this.glControl1.Load += new System.EventHandler(this.glControl1_Load);
            this.glControl1.Paint += new System.Windows.Forms.PaintEventHandler(this.glControl1_Paint);
            this.glControl1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.glControl1_KeyDown);
            this.glControl1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.glControl1_KeyPress);
            this.glControl1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.glControl1_KeyUp);
            this.glControl1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.glControl1_MouseDown);
            this.glControl1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.glControl1_MouseMove);
            this.glControl1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.glControl1_MouseUp);
            this.glControl1.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.glControl1_PreviewKeyDown);
            // 
            // loadModelToolStripMenuItem
            // 
            this.loadModelToolStripMenuItem.Name = "loadModelToolStripMenuItem";
            this.loadModelToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // menuStrip2
            // 
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip2.Location = new System.Drawing.Point(0, 0);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(1562, 24);
            this.menuStrip2.TabIndex = 2;
            this.menuStrip2.Text = "menuStrip2";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.clearToolStripMenuItem,
            this.loadToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(101, 22);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(101, 22);
            this.clearToolStripMenuItem.Text = "Clear";
            this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(101, 22);
            this.loadToolStripMenuItem.Text = "Load";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // cameraLightCheckBox
            // 
            this.cameraLightCheckBox.AutoSize = true;
            this.cameraLightCheckBox.Location = new System.Drawing.Point(23, 578);
            this.cameraLightCheckBox.Name = "cameraLightCheckBox";
            this.cameraLightCheckBox.Size = new System.Drawing.Size(88, 17);
            this.cameraLightCheckBox.TabIndex = 4;
            this.cameraLightCheckBox.Text = "Camera Light";
            this.cameraLightCheckBox.UseVisualStyleBackColor = true;
            this.cameraLightCheckBox.CheckedChanged += new System.EventHandler(this.cameraLightCheckBox_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.labelZ);
            this.panel1.Controls.Add(this.labelY);
            this.panel1.Controls.Add(this.labelX);
            this.panel1.Controls.Add(this.trackBarZ);
            this.panel1.Controls.Add(this.trackBarY);
            this.panel1.Controls.Add(this.roundLabel);
            this.panel1.Controls.Add(this.trackBarX);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.collisionCoefficientNumericUpDown);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.shakeButton);
            this.panel1.Controls.Add(this.resetButton);
            this.panel1.Controls.Add(this.randomVelocityScaleUpDown1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.displayBoxCheckBox);
            this.panel1.Controls.Add(this.displayBezierCubeBox);
            this.panel1.Controls.Add(this.integrationStepNumericUpDown);
            this.panel1.Controls.Add(this.angularVelocityNumericUpDown);
            this.panel1.Controls.Add(this.cubeDeviationNumericUpDown);
            this.panel1.Controls.Add(this.cubeDensityNumericUpDown);
            this.panel1.Controls.Add(this.cubeEdgeLengthNumericUpDown);
            this.panel1.Controls.Add(this.integrationstepLabel);
            this.panel1.Controls.Add(this.resilience2Label);
            this.panel1.Controls.Add(this.tenacityRateLabel);
            this.panel1.Controls.Add(this.resilienceLabel);
            this.panel1.Controls.Add(this.displayControlPointsCheckBox);
            this.panel1.Controls.Add(this.displayDistortBlockCheckBox);
            this.panel1.Controls.Add(this.displayControlFrameCheckBox);
            this.panel1.Controls.Add(this.massLabel);
            this.panel1.Controls.Add(this.otherLabel);
            this.panel1.Controls.Add(this.visualizationLabel);
            this.panel1.Controls.Add(this.initialConditionsLabel);
            this.panel1.Controls.Add(this.cameraLightCheckBox);
            this.panel1.Location = new System.Drawing.Point(1298, 27);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(252, 885);
            this.panel1.TabIndex = 6;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.collisionModel_Model2);
            this.panel2.Controls.Add(this.collisionModel_Model1);
            this.panel2.Location = new System.Drawing.Point(3, 347);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(246, 27);
            this.panel2.TabIndex = 37;
            // 
            // collisionModel_Model2
            // 
            this.collisionModel_Model2.AutoSize = true;
            this.collisionModel_Model2.Location = new System.Drawing.Point(134, 4);
            this.collisionModel_Model2.Name = "collisionModel_Model2";
            this.collisionModel_Model2.Size = new System.Drawing.Size(63, 17);
            this.collisionModel_Model2.TabIndex = 1;
            this.collisionModel_Model2.TabStop = true;
            this.collisionModel_Model2.Text = "Model 2";
            this.collisionModel_Model2.UseVisualStyleBackColor = true;
            this.collisionModel_Model2.CheckedChanged += new System.EventHandler(this.collisionModel_Model2_CheckedChanged);
            // 
            // collisionModel_Model1
            // 
            this.collisionModel_Model1.AutoSize = true;
            this.collisionModel_Model1.Checked = true;
            this.collisionModel_Model1.Location = new System.Drawing.Point(30, 4);
            this.collisionModel_Model1.Name = "collisionModel_Model1";
            this.collisionModel_Model1.Size = new System.Drawing.Size(63, 17);
            this.collisionModel_Model1.TabIndex = 0;
            this.collisionModel_Model1.TabStop = true;
            this.collisionModel_Model1.Text = "Model 1";
            this.collisionModel_Model1.UseVisualStyleBackColor = true;
            this.collisionModel_Model1.CheckedChanged += new System.EventHandler(this.collisionModel_Model1_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(77, 321);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 13);
            this.label3.TabIndex = 36;
            this.label3.Text = "Collision model:";
            // 
            // collisionCoefficientNumericUpDown
            // 
            this.collisionCoefficientNumericUpDown.DecimalPlaces = 2;
            this.collisionCoefficientNumericUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.collisionCoefficientNumericUpDown.Location = new System.Drawing.Point(116, 286);
            this.collisionCoefficientNumericUpDown.Maximum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.collisionCoefficientNumericUpDown.Name = "collisionCoefficientNumericUpDown";
            this.collisionCoefficientNumericUpDown.Size = new System.Drawing.Size(120, 20);
            this.collisionCoefficientNumericUpDown.TabIndex = 35;
            this.collisionCoefficientNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.collisionCoefficientNumericUpDown.ValueChanged += new System.EventHandler(this.collisionCoefficientNumericUpDown_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 288);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 13);
            this.label2.TabIndex = 34;
            this.label2.Text = "Collision coefficient:";
            // 
            // shakeButton
            // 
            this.shakeButton.Location = new System.Drawing.Point(137, 574);
            this.shakeButton.Name = "shakeButton";
            this.shakeButton.Size = new System.Drawing.Size(75, 23);
            this.shakeButton.TabIndex = 33;
            this.shakeButton.Text = "Shake Cube";
            this.shakeButton.UseVisualStyleBackColor = true;
            this.shakeButton.Click += new System.EventHandler(this.shakeButton_Click);
            // 
            // resetButton
            // 
            this.resetButton.Location = new System.Drawing.Point(161, 10);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(75, 23);
            this.resetButton.TabIndex = 32;
            this.resetButton.Text = "Reset";
            this.resetButton.UseVisualStyleBackColor = true;
            this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
            // 
            // randomVelocityScaleUpDown1
            // 
            this.randomVelocityScaleUpDown1.DecimalPlaces = 2;
            this.randomVelocityScaleUpDown1.Location = new System.Drawing.Point(137, 205);
            this.randomVelocityScaleUpDown1.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.randomVelocityScaleUpDown1.Name = "randomVelocityScaleUpDown1";
            this.randomVelocityScaleUpDown1.Size = new System.Drawing.Size(99, 20);
            this.randomVelocityScaleUpDown1.TabIndex = 31;
            this.randomVelocityScaleUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 207);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 13);
            this.label1.TabIndex = 30;
            this.label1.Text = "Random Velocity Scale:";
            // 
            // displayBoxCheckBox
            // 
            this.displayBoxCheckBox.AutoSize = true;
            this.displayBoxCheckBox.Checked = true;
            this.displayBoxCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.displayBoxCheckBox.Location = new System.Drawing.Point(20, 460);
            this.displayBoxCheckBox.Name = "displayBoxCheckBox";
            this.displayBoxCheckBox.Size = new System.Drawing.Size(81, 17);
            this.displayBoxCheckBox.TabIndex = 29;
            this.displayBoxCheckBox.Text = "Display Box";
            this.displayBoxCheckBox.UseVisualStyleBackColor = true;
            this.displayBoxCheckBox.CheckedChanged += new System.EventHandler(this.displayBoxCheckBox_CheckedChanged);
            // 
            // displayBezierCubeBox
            // 
            this.displayBezierCubeBox.AutoSize = true;
            this.displayBezierCubeBox.Checked = true;
            this.displayBezierCubeBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.displayBezierCubeBox.Location = new System.Drawing.Point(20, 484);
            this.displayBezierCubeBox.Name = "displayBezierCubeBox";
            this.displayBezierCubeBox.Size = new System.Drawing.Size(120, 17);
            this.displayBezierCubeBox.TabIndex = 28;
            this.displayBezierCubeBox.Text = "Display Bezier Cube";
            this.displayBezierCubeBox.UseVisualStyleBackColor = true;
            this.displayBezierCubeBox.CheckedChanged += new System.EventHandler(this.displayWallsCheckBox_CheckedChanged);
            // 
            // integrationStepNumericUpDown
            // 
            this.integrationStepNumericUpDown.DecimalPlaces = 5;
            this.integrationStepNumericUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.integrationStepNumericUpDown.Location = new System.Drawing.Point(116, 247);
            this.integrationStepNumericUpDown.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.integrationStepNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            327680});
            this.integrationStepNumericUpDown.Name = "integrationStepNumericUpDown";
            this.integrationStepNumericUpDown.Size = new System.Drawing.Size(120, 20);
            this.integrationStepNumericUpDown.TabIndex = 26;
            this.integrationStepNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.integrationStepNumericUpDown.ValueChanged += new System.EventHandler(this.integrationStepNumericUpDown_ValueChanged);
            // 
            // angularVelocityNumericUpDown
            // 
            this.angularVelocityNumericUpDown.DecimalPlaces = 2;
            this.angularVelocityNumericUpDown.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.angularVelocityNumericUpDown.Location = new System.Drawing.Point(116, 165);
            this.angularVelocityNumericUpDown.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.angularVelocityNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.angularVelocityNumericUpDown.Name = "angularVelocityNumericUpDown";
            this.angularVelocityNumericUpDown.Size = new System.Drawing.Size(120, 20);
            this.angularVelocityNumericUpDown.TabIndex = 25;
            this.angularVelocityNumericUpDown.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.angularVelocityNumericUpDown.ValueChanged += new System.EventHandler(this.angularVelocityNumericUpDown_ValueChanged);
            // 
            // cubeDeviationNumericUpDown
            // 
            this.cubeDeviationNumericUpDown.DecimalPlaces = 2;
            this.cubeDeviationNumericUpDown.Location = new System.Drawing.Point(116, 123);
            this.cubeDeviationNumericUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.cubeDeviationNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.cubeDeviationNumericUpDown.Name = "cubeDeviationNumericUpDown";
            this.cubeDeviationNumericUpDown.Size = new System.Drawing.Size(120, 20);
            this.cubeDeviationNumericUpDown.TabIndex = 24;
            this.cubeDeviationNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.cubeDeviationNumericUpDown.ValueChanged += new System.EventHandler(this.cubeDeviationNumericUpDown_ValueChanged);
            // 
            // cubeDensityNumericUpDown
            // 
            this.cubeDensityNumericUpDown.DecimalPlaces = 2;
            this.cubeDensityNumericUpDown.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.cubeDensityNumericUpDown.Location = new System.Drawing.Point(116, 82);
            this.cubeDensityNumericUpDown.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.cubeDensityNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.cubeDensityNumericUpDown.Name = "cubeDensityNumericUpDown";
            this.cubeDensityNumericUpDown.Size = new System.Drawing.Size(120, 20);
            this.cubeDensityNumericUpDown.TabIndex = 23;
            this.cubeDensityNumericUpDown.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.cubeDensityNumericUpDown.ValueChanged += new System.EventHandler(this.cubeDensityNumericUpDown_ValueChanged);
            // 
            // cubeEdgeLengthNumericUpDown
            // 
            this.cubeEdgeLengthNumericUpDown.DecimalPlaces = 2;
            this.cubeEdgeLengthNumericUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.cubeEdgeLengthNumericUpDown.Location = new System.Drawing.Point(116, 45);
            this.cubeEdgeLengthNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.cubeEdgeLengthNumericUpDown.Name = "cubeEdgeLengthNumericUpDown";
            this.cubeEdgeLengthNumericUpDown.Size = new System.Drawing.Size(120, 20);
            this.cubeEdgeLengthNumericUpDown.TabIndex = 22;
            this.cubeEdgeLengthNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.cubeEdgeLengthNumericUpDown.ValueChanged += new System.EventHandler(this.cubeEdgeLengthNumericUpDown_ValueChanged);
            // 
            // integrationstepLabel
            // 
            this.integrationstepLabel.AutoSize = true;
            this.integrationstepLabel.Location = new System.Drawing.Point(11, 249);
            this.integrationstepLabel.Name = "integrationstepLabel";
            this.integrationstepLabel.Size = new System.Drawing.Size(85, 13);
            this.integrationstepLabel.TabIndex = 21;
            this.integrationstepLabel.Text = "Integration Step:";
            // 
            // resilience2Label
            // 
            this.resilience2Label.AutoSize = true;
            this.resilience2Label.Location = new System.Drawing.Point(11, 167);
            this.resilience2Label.Name = "resilience2Label";
            this.resilience2Label.Size = new System.Drawing.Size(74, 13);
            this.resilience2Label.TabIndex = 20;
            this.resilience2Label.Text = "Resilience: c2";
            // 
            // tenacityRateLabel
            // 
            this.tenacityRateLabel.AutoSize = true;
            this.tenacityRateLabel.Location = new System.Drawing.Point(11, 125);
            this.tenacityRateLabel.Name = "tenacityRateLabel";
            this.tenacityRateLabel.Size = new System.Drawing.Size(81, 13);
            this.tenacityRateLabel.TabIndex = 19;
            this.tenacityRateLabel.Text = "Tenacity rate: k";
            // 
            // resilienceLabel
            // 
            this.resilienceLabel.AutoSize = true;
            this.resilienceLabel.Location = new System.Drawing.Point(11, 84);
            this.resilienceLabel.Name = "resilienceLabel";
            this.resilienceLabel.Size = new System.Drawing.Size(74, 13);
            this.resilienceLabel.TabIndex = 18;
            this.resilienceLabel.Text = "Resilience: c1";
            // 
            // displayControlPointsCheckBox
            // 
            this.displayControlPointsCheckBox.AutoSize = true;
            this.displayControlPointsCheckBox.Checked = true;
            this.displayControlPointsCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.displayControlPointsCheckBox.Location = new System.Drawing.Point(20, 414);
            this.displayControlPointsCheckBox.Name = "displayControlPointsCheckBox";
            this.displayControlPointsCheckBox.Size = new System.Drawing.Size(128, 17);
            this.displayControlPointsCheckBox.TabIndex = 16;
            this.displayControlPointsCheckBox.Text = "Display Control Points";
            this.displayControlPointsCheckBox.UseVisualStyleBackColor = true;
            this.displayControlPointsCheckBox.CheckedChanged += new System.EventHandler(this.displayPlaneCheckBox_CheckedChanged);
            // 
            // displayDistortBlockCheckBox
            // 
            this.displayDistortBlockCheckBox.AutoSize = true;
            this.displayDistortBlockCheckBox.Checked = true;
            this.displayDistortBlockCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.displayDistortBlockCheckBox.Location = new System.Drawing.Point(20, 507);
            this.displayDistortBlockCheckBox.Name = "displayDistortBlockCheckBox";
            this.displayDistortBlockCheckBox.Size = new System.Drawing.Size(123, 17);
            this.displayDistortBlockCheckBox.TabIndex = 15;
            this.displayDistortBlockCheckBox.Text = "Display Distort Block";
            this.displayDistortBlockCheckBox.UseVisualStyleBackColor = true;
            this.displayDistortBlockCheckBox.CheckedChanged += new System.EventHandler(this.displayDiagonalCheckBox_CheckedChanged);
            // 
            // displayControlFrameCheckBox
            // 
            this.displayControlFrameCheckBox.AutoSize = true;
            this.displayControlFrameCheckBox.Checked = true;
            this.displayControlFrameCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.displayControlFrameCheckBox.Location = new System.Drawing.Point(20, 437);
            this.displayControlFrameCheckBox.Name = "displayControlFrameCheckBox";
            this.displayControlFrameCheckBox.Size = new System.Drawing.Size(128, 17);
            this.displayControlFrameCheckBox.TabIndex = 13;
            this.displayControlFrameCheckBox.Text = "Display Control Frame";
            this.displayControlFrameCheckBox.UseVisualStyleBackColor = true;
            this.displayControlFrameCheckBox.CheckedChanged += new System.EventHandler(this.displayCubeCheckBox_CheckedChanged);
            // 
            // massLabel
            // 
            this.massLabel.AutoSize = true;
            this.massLabel.Location = new System.Drawing.Point(11, 47);
            this.massLabel.Name = "massLabel";
            this.massLabel.Size = new System.Drawing.Size(46, 13);
            this.massLabel.TabIndex = 11;
            this.massLabel.Text = "Mass: m";
            // 
            // otherLabel
            // 
            this.otherLabel.AutoSize = true;
            this.otherLabel.Location = new System.Drawing.Point(92, 537);
            this.otherLabel.Name = "otherLabel";
            this.otherLabel.Size = new System.Drawing.Size(48, 13);
            this.otherLabel.TabIndex = 9;
            this.otherLabel.Text = "OTHER:";
            // 
            // visualizationLabel
            // 
            this.visualizationLabel.AutoSize = true;
            this.visualizationLabel.Location = new System.Drawing.Point(78, 386);
            this.visualizationLabel.Name = "visualizationLabel";
            this.visualizationLabel.Size = new System.Drawing.Size(91, 13);
            this.visualizationLabel.TabIndex = 8;
            this.visualizationLabel.Text = "VISUALIZATION:";
            // 
            // initialConditionsLabel
            // 
            this.initialConditionsLabel.AutoSize = true;
            this.initialConditionsLabel.Location = new System.Drawing.Point(54, 15);
            this.initialConditionsLabel.Name = "initialConditionsLabel";
            this.initialConditionsLabel.Size = new System.Drawing.Size(77, 13);
            this.initialConditionsLabel.TabIndex = 7;
            this.initialConditionsLabel.Text = "CONDITIONS:";
            // 
            // trackBarX
            // 
            this.trackBarX.Location = new System.Drawing.Point(33, 645);
            this.trackBarX.Maximum = 359;
            this.trackBarX.Name = "trackBarX";
            this.trackBarX.Size = new System.Drawing.Size(195, 45);
            this.trackBarX.TabIndex = 38;
            this.trackBarX.Scroll += new System.EventHandler(this.trackBarX_Scroll);
            // 
            // roundLabel
            // 
            this.roundLabel.AutoSize = true;
            this.roundLabel.Location = new System.Drawing.Point(20, 615);
            this.roundLabel.Name = "roundLabel";
            this.roundLabel.Size = new System.Drawing.Size(42, 13);
            this.roundLabel.TabIndex = 39;
            this.roundLabel.Text = "Round:";
            // 
            // trackBarY
            // 
            this.trackBarY.Location = new System.Drawing.Point(33, 696);
            this.trackBarY.Maximum = 359;
            this.trackBarY.Name = "trackBarY";
            this.trackBarY.Size = new System.Drawing.Size(195, 45);
            this.trackBarY.TabIndex = 40;
            this.trackBarY.Scroll += new System.EventHandler(this.trackBarY_Scroll);
            // 
            // trackBarZ
            // 
            this.trackBarZ.Location = new System.Drawing.Point(33, 747);
            this.trackBarZ.Maximum = 359;
            this.trackBarZ.Name = "trackBarZ";
            this.trackBarZ.Size = new System.Drawing.Size(195, 45);
            this.trackBarZ.TabIndex = 41;
            this.trackBarZ.Scroll += new System.EventHandler(this.trackBarZ_Scroll);
            // 
            // labelX
            // 
            this.labelX.AutoSize = true;
            this.labelX.Location = new System.Drawing.Point(11, 645);
            this.labelX.Name = "labelX";
            this.labelX.Size = new System.Drawing.Size(17, 13);
            this.labelX.TabIndex = 42;
            this.labelX.Text = "X:";
            // 
            // labelY
            // 
            this.labelY.AutoSize = true;
            this.labelY.Location = new System.Drawing.Point(11, 696);
            this.labelY.Name = "labelY";
            this.labelY.Size = new System.Drawing.Size(17, 13);
            this.labelY.TabIndex = 43;
            this.labelY.Text = "Y:";
            // 
            // labelZ
            // 
            this.labelZ.AutoSize = true;
            this.labelZ.Location = new System.Drawing.Point(11, 747);
            this.labelZ.Name = "labelZ";
            this.labelZ.Size = new System.Drawing.Size(17, 13);
            this.labelZ.TabIndex = 44;
            this.labelZ.Text = "Z:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1562, 932);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.glControl1);
            this.Controls.Add(this.menuStrip2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.collisionCoefficientNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.randomVelocityScaleUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.integrationStepNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.angularVelocityNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cubeDeviationNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cubeDensityNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cubeEdgeLengthNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarZ)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private OpenTK.GLControl glControl1;
        private System.Windows.Forms.ToolStripMenuItem loadModelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
        private System.Windows.Forms.CheckBox cameraLightCheckBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label integrationstepLabel;
        private System.Windows.Forms.Label resilience2Label;
        private System.Windows.Forms.Label tenacityRateLabel;
        private System.Windows.Forms.Label resilienceLabel;
        private System.Windows.Forms.CheckBox displayControlPointsCheckBox;
        private System.Windows.Forms.CheckBox displayDistortBlockCheckBox;
        private System.Windows.Forms.CheckBox displayControlFrameCheckBox;
        private System.Windows.Forms.Label massLabel;
        private System.Windows.Forms.Label otherLabel;
        private System.Windows.Forms.Label visualizationLabel;
        private System.Windows.Forms.Label initialConditionsLabel;
        private System.Windows.Forms.NumericUpDown integrationStepNumericUpDown;
        private System.Windows.Forms.NumericUpDown angularVelocityNumericUpDown;
        private System.Windows.Forms.NumericUpDown cubeDeviationNumericUpDown;
        private System.Windows.Forms.NumericUpDown cubeDensityNumericUpDown;
        private System.Windows.Forms.NumericUpDown cubeEdgeLengthNumericUpDown;
        private System.Windows.Forms.CheckBox displayBezierCubeBox;
        private System.Windows.Forms.CheckBox displayBoxCheckBox;
        private System.Windows.Forms.NumericUpDown randomVelocityScaleUpDown1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button resetButton;
        private System.Windows.Forms.Button shakeButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown collisionCoefficientNumericUpDown;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton collisionModel_Model2;
        private System.Windows.Forms.RadioButton collisionModel_Model1;
        private System.Windows.Forms.Label labelZ;
        private System.Windows.Forms.Label labelY;
        private System.Windows.Forms.Label labelX;
        private System.Windows.Forms.TrackBar trackBarZ;
        private System.Windows.Forms.TrackBar trackBarY;
        private System.Windows.Forms.Label roundLabel;
        private System.Windows.Forms.TrackBar trackBarX;
    }
}

