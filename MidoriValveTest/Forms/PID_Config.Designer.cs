
namespace MidoriValveTest
{
    partial class PID_Config
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PID_Config));
            this.RamEnable1 = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.Type1 = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.StartV1 = new System.Windows.Forms.ComboBox();
            this.Slope1 = new System.Windows.Forms.NumericUpDown();
            this.Time1 = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.Mode1 = new System.Windows.Forms.ComboBox();
            this.Cb_ControlSelector = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.GroupC1 = new System.Windows.Forms.GroupBox();
            this.CbAlgo1 = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.CbDirec1 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.NumP1 = new System.Windows.Forms.NumericUpDown();
            this.NumI1 = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.NumD1 = new System.Windows.Forms.NumericUpDown();
            this.GroupR1 = new System.Windows.Forms.GroupBox();
            this.GroupS1 = new System.Windows.Forms.GroupBox();
            this.btnBackGround1 = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtD = new System.Windows.Forms.TextBox();
            this.txtI = new System.Windows.Forms.TextBox();
            this.txtP = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.checkPID = new System.Windows.Forms.CheckBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.btnSentPID = new FontAwesome.Sharp.IconButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.IconClose = new FontAwesome.Sharp.IconButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.Slope1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Time1)).BeginInit();
            this.GroupC1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumP1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumI1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumD1)).BeginInit();
            this.GroupR1.SuspendLayout();
            this.GroupS1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // RamEnable1
            // 
            this.RamEnable1.AutoSize = true;
            this.RamEnable1.Location = new System.Drawing.Point(27, 32);
            this.RamEnable1.Name = "RamEnable1";
            this.RamEnable1.Size = new System.Drawing.Size(59, 17);
            this.RamEnable1.TabIndex = 13;
            this.RamEnable1.Text = "Enable";
            this.RamEnable1.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(24, 203);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(31, 13);
            this.label10.TabIndex = 10;
            this.label10.Text = "Type";
            // 
            // Type1
            // 
            this.Type1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Type1.FormattingEnabled = true;
            this.Type1.Items.AddRange(new object[] {
            "Exponentrial",
            "Linear",
            "Logarithmic"});
            this.Type1.Location = new System.Drawing.Point(154, 200);
            this.Type1.Name = "Type1";
            this.Type1.Size = new System.Drawing.Size(149, 21);
            this.Type1.TabIndex = 11;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(24, 168);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(58, 13);
            this.label9.TabIndex = 8;
            this.label9.Text = "Start value";
            // 
            // StartV1
            // 
            this.StartV1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.StartV1.FormattingEnabled = true;
            this.StartV1.Items.AddRange(new object[] {
            "Actual value",
            "Previus Ramp Value"});
            this.StartV1.Location = new System.Drawing.Point(154, 165);
            this.StartV1.Name = "StartV1";
            this.StartV1.Size = new System.Drawing.Size(149, 21);
            this.StartV1.TabIndex = 9;
            // 
            // Slope1
            // 
            this.Slope1.Location = new System.Drawing.Point(154, 97);
            this.Slope1.Name = "Slope1";
            this.Slope1.Size = new System.Drawing.Size(149, 20);
            this.Slope1.TabIndex = 7;
            this.Slope1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Slope1.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // Time1
            // 
            this.Time1.Location = new System.Drawing.Point(154, 62);
            this.Time1.Name = "Time1";
            this.Time1.Size = new System.Drawing.Size(149, 20);
            this.Time1.TabIndex = 4;
            this.Time1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Time1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Time";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(24, 99);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(34, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "Slope";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(24, 133);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(34, 13);
            this.label8.TabIndex = 4;
            this.label8.Text = "Mode";
            // 
            // Mode1
            // 
            this.Mode1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Mode1.FormattingEnabled = true;
            this.Mode1.Items.AddRange(new object[] {
            "Ramp Slope",
            "Ramp Time"});
            this.Mode1.Location = new System.Drawing.Point(154, 133);
            this.Mode1.Name = "Mode1";
            this.Mode1.Size = new System.Drawing.Size(149, 21);
            this.Mode1.TabIndex = 4;
            // 
            // Cb_ControlSelector
            // 
            this.Cb_ControlSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Cb_ControlSelector.FormattingEnabled = true;
            this.Cb_ControlSelector.Items.AddRange(new object[] {
            "Controller 1",
            "Controller 2",
            "Controller 3",
            "Controller 4"});
            this.Cb_ControlSelector.Location = new System.Drawing.Point(129, 45);
            this.Cb_ControlSelector.Name = "Cb_ControlSelector";
            this.Cb_ControlSelector.Size = new System.Drawing.Size(149, 21);
            this.Cb_ControlSelector.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(11, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Controller Selector";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkCyan;
            this.label1.Location = new System.Drawing.Point(8, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 18);
            this.label1.TabIndex = 9;
            this.label1.Text = "PID Controller";
            // 
            // GroupC1
            // 
            this.GroupC1.BackColor = System.Drawing.SystemColors.Control;
            this.GroupC1.Controls.Add(this.CbAlgo1);
            this.GroupC1.Controls.Add(this.label11);
            this.GroupC1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GroupC1.ForeColor = System.Drawing.Color.Black;
            this.GroupC1.Location = new System.Drawing.Point(514, 72);
            this.GroupC1.Name = "GroupC1";
            this.GroupC1.Size = new System.Drawing.Size(327, 479);
            this.GroupC1.TabIndex = 14;
            this.GroupC1.TabStop = false;
            this.GroupC1.Text = "Controller 1";
            // 
            // CbAlgo1
            // 
            this.CbAlgo1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CbAlgo1.FormattingEnabled = true;
            this.CbAlgo1.Items.AddRange(new object[] {
            "PI",
            "PID",
            "Soft Pump",
            "P"});
            this.CbAlgo1.Location = new System.Drawing.Point(160, 27);
            this.CbAlgo1.Name = "CbAlgo1";
            this.CbAlgo1.Size = new System.Drawing.Size(149, 21);
            this.CbAlgo1.TabIndex = 1;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.SystemColors.InfoText;
            this.label11.Location = new System.Drawing.Point(17, 30);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(86, 13);
            this.label11.TabIndex = 0;
            this.label11.Text = "Control Algorithm";
            // 
            // CbDirec1
            // 
            this.CbDirec1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CbDirec1.FormattingEnabled = true;
            this.CbDirec1.Items.AddRange(new object[] {
            "Downstream",
            "Upstream"});
            this.CbDirec1.Location = new System.Drawing.Point(154, 127);
            this.CbDirec1.Name = "CbDirec1";
            this.CbDirec1.Size = new System.Drawing.Size(149, 21);
            this.CbDirec1.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 127);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Control Direction";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 67);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "I- Gain";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(24, 32);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(39, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "P-Gain";
            // 
            // NumP1
            // 
            this.NumP1.DecimalPlaces = 2;
            this.NumP1.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.NumP1.Location = new System.Drawing.Point(154, 30);
            this.NumP1.Name = "NumP1";
            this.NumP1.Size = new System.Drawing.Size(149, 20);
            this.NumP1.TabIndex = 4;
            this.NumP1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.NumP1.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            // 
            // NumI1
            // 
            this.NumI1.DecimalPlaces = 2;
            this.NumI1.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.NumI1.Location = new System.Drawing.Point(154, 65);
            this.NumI1.Name = "NumI1";
            this.NumI1.Size = new System.Drawing.Size(149, 20);
            this.NumI1.TabIndex = 7;
            this.NumI1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.NumI1.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(24, 99);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(40, 13);
            this.label12.TabIndex = 8;
            this.label12.Text = "D-Gain";
            // 
            // NumD1
            // 
            this.NumD1.DecimalPlaces = 2;
            this.NumD1.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.NumD1.Location = new System.Drawing.Point(154, 97);
            this.NumD1.Name = "NumD1";
            this.NumD1.Size = new System.Drawing.Size(149, 20);
            this.NumD1.TabIndex = 9;
            this.NumD1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.NumD1.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            // 
            // GroupR1
            // 
            this.GroupR1.Controls.Add(this.Cb_ControlSelector);
            this.GroupR1.Controls.Add(this.label2);
            this.GroupR1.Controls.Add(this.label1);
            this.GroupR1.Controls.Add(this.RamEnable1);
            this.GroupR1.Controls.Add(this.label10);
            this.GroupR1.Controls.Add(this.Type1);
            this.GroupR1.Controls.Add(this.label9);
            this.GroupR1.Controls.Add(this.StartV1);
            this.GroupR1.Controls.Add(this.Slope1);
            this.GroupR1.Controls.Add(this.Time1);
            this.GroupR1.Controls.Add(this.label3);
            this.GroupR1.Controls.Add(this.label7);
            this.GroupR1.Controls.Add(this.label8);
            this.GroupR1.Controls.Add(this.Mode1);
            this.GroupR1.Location = new System.Drawing.Point(520, 313);
            this.GroupR1.Name = "GroupR1";
            this.GroupR1.Size = new System.Drawing.Size(315, 232);
            this.GroupR1.TabIndex = 13;
            this.GroupR1.TabStop = false;
            this.GroupR1.Text = "Ramp";
            // 
            // GroupS1
            // 
            this.GroupS1.Controls.Add(this.NumD1);
            this.GroupS1.Controls.Add(this.label12);
            this.GroupS1.Controls.Add(this.NumI1);
            this.GroupS1.Controls.Add(this.NumP1);
            this.GroupS1.Controls.Add(this.label6);
            this.GroupS1.Controls.Add(this.label5);
            this.GroupS1.Controls.Add(this.label4);
            this.GroupS1.Controls.Add(this.CbDirec1);
            this.GroupS1.Location = new System.Drawing.Point(520, 137);
            this.GroupS1.Name = "GroupS1";
            this.GroupS1.Size = new System.Drawing.Size(315, 160);
            this.GroupS1.TabIndex = 12;
            this.GroupS1.TabStop = false;
            this.GroupS1.Text = "Controller Settings";
            // 
            // btnBackGround1
            // 
            this.btnBackGround1.BackColor = System.Drawing.Color.DarkCyan;
            this.btnBackGround1.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnBackGround1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBackGround1.Location = new System.Drawing.Point(505, 63);
            this.btnBackGround1.Name = "btnBackGround1";
            this.btnBackGround1.Size = new System.Drawing.Size(343, 498);
            this.btnBackGround1.TabIndex = 24;
            this.btnBackGround1.Text = "button1";
            this.btnBackGround1.UseVisualStyleBackColor = false;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.Black;
            this.label13.Location = new System.Drawing.Point(28, 54);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(178, 29);
            this.label13.TabIndex = 35;
            this.label13.Text = "PID Controller";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.DimGray;
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.checkPID);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(66, 93);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(393, 227);
            this.groupBox1.TabIndex = 37;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Controller 1";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.DimGray;
            this.groupBox2.Controls.Add(this.txtD);
            this.groupBox2.Controls.Add(this.txtI);
            this.groupBox2.Controls.Add(this.txtP);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.White;
            this.groupBox2.Location = new System.Drawing.Point(11, 38);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(361, 171);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Controller Settings";
            // 
            // txtD
            // 
            this.txtD.Location = new System.Drawing.Point(154, 114);
            this.txtD.Name = "txtD";
            this.txtD.Size = new System.Drawing.Size(182, 24);
            this.txtD.TabIndex = 12;
            this.txtD.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtD_KeyPress);
            // 
            // txtI
            // 
            this.txtI.Location = new System.Drawing.Point(154, 79);
            this.txtI.Name = "txtI";
            this.txtI.Size = new System.Drawing.Size(182, 24);
            this.txtI.TabIndex = 11;
            this.txtI.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtI_KeyPress);
            // 
            // txtP
            // 
            this.txtP.Location = new System.Drawing.Point(154, 44);
            this.txtP.Name = "txtP";
            this.txtP.Size = new System.Drawing.Size(182, 24);
            this.txtP.TabIndex = 10;
            this.txtP.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtP_KeyPress);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(24, 114);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(55, 18);
            this.label14.TabIndex = 8;
            this.label14.Text = "D-Gain";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(24, 47);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(54, 18);
            this.label15.TabIndex = 6;
            this.label15.Text = "P-Gain";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(24, 82);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(51, 18);
            this.label16.TabIndex = 5;
            this.label16.Text = "I- Gain";
            // 
            // checkPID
            // 
            this.checkPID.AutoSize = true;
            this.checkPID.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkPID.ForeColor = System.Drawing.Color.White;
            this.checkPID.Location = new System.Drawing.Point(300, 18);
            this.checkPID.Name = "checkPID";
            this.checkPID.Size = new System.Drawing.Size(72, 22);
            this.checkPID.TabIndex = 13;
            this.checkPID.Text = "Enable";
            this.checkPID.UseVisualStyleBackColor = true;
            this.checkPID.CheckedChanged += new System.EventHandler(this.checkPID_CheckedChanged);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Controller 1",
            "Controller 2",
            "Controller 3",
            "Controller 4"});
            this.comboBox1.Location = new System.Drawing.Point(187, 113);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(149, 21);
            this.comboBox1.TabIndex = 36;
            this.comboBox1.Visible = false;
            // 
            // btnSentPID
            // 
            this.btnSentPID.BackColor = System.Drawing.Color.DimGray;
            this.btnSentPID.FlatAppearance.BorderSize = 0;
            this.btnSentPID.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSentPID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSentPID.ForeColor = System.Drawing.Color.White;
            this.btnSentPID.IconChar = FontAwesome.Sharp.IconChar.Calculator;
            this.btnSentPID.IconColor = System.Drawing.Color.White;
            this.btnSentPID.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnSentPID.IconSize = 28;
            this.btnSentPID.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSentPID.Location = new System.Drawing.Point(208, 335);
            this.btnSentPID.Name = "btnSentPID";
            this.btnSentPID.Size = new System.Drawing.Size(117, 36);
            this.btnSentPID.TabIndex = 38;
            this.btnSentPID.Text = "       Send PID";
            this.btnSentPID.UseVisualStyleBackColor = false;
            this.btnSentPID.Click += new System.EventHandler(this.btnSentPID_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DimGray;
            this.panel1.Controls.Add(this.IconClose);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.pictureBox2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(511, 41);
            this.panel1.TabIndex = 39;
            // 
            // IconClose
            // 
            this.IconClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.IconClose.FlatAppearance.BorderSize = 0;
            this.IconClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IconClose.IconChar = FontAwesome.Sharp.IconChar.WindowClose;
            this.IconClose.IconColor = System.Drawing.Color.White;
            this.IconClose.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.IconClose.IconSize = 30;
            this.IconClose.Location = new System.Drawing.Point(476, 0);
            this.IconClose.Name = "IconClose";
            this.IconClose.Size = new System.Drawing.Size(35, 41);
            this.IconClose.TabIndex = 40;
            this.IconClose.UseVisualStyleBackColor = true;
            this.IconClose.Click += new System.EventHandler(this.IconClose_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.pictureBox1.Location = new System.Drawing.Point(50, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(80, 29);
            this.pictureBox1.TabIndex = 28;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox2.BackgroundImage")));
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.pictureBox2.Location = new System.Drawing.Point(6, 3);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(38, 29);
            this.pictureBox2.TabIndex = 29;
            this.pictureBox2.TabStop = false;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.DimGray;
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(0, 41);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(10, 375);
            this.panel3.TabIndex = 40;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.DimGray;
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(10, 406);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(501, 10);
            this.panel4.TabIndex = 41;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.DimGray;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(501, 41);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(10, 365);
            this.panel2.TabIndex = 42;
            // 
            // PID_Config
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(511, 416);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnSentPID);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.GroupR1);
            this.Controls.Add(this.GroupS1);
            this.Controls.Add(this.GroupC1);
            this.Controls.Add(this.btnBackGround1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "PID_Config";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PID_Config";
            this.Load += new System.EventHandler(this.PID_Config_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Slope1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Time1)).EndInit();
            this.GroupC1.ResumeLayout(false);
            this.GroupC1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumP1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumI1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumD1)).EndInit();
            this.GroupR1.ResumeLayout(false);
            this.GroupR1.PerformLayout();
            this.GroupS1.ResumeLayout(false);
            this.GroupS1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox Type1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox StartV1;
        private System.Windows.Forms.NumericUpDown Slope1;
        private System.Windows.Forms.NumericUpDown Time1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox Mode1;
        private System.Windows.Forms.ComboBox Cb_ControlSelector;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox RamEnable1;
        private System.Windows.Forms.GroupBox GroupC1;
        private System.Windows.Forms.ComboBox CbAlgo1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox CbDirec1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown NumP1;
        private System.Windows.Forms.NumericUpDown NumI1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.NumericUpDown NumD1;
        private System.Windows.Forms.GroupBox GroupR1;
        private System.Windows.Forms.GroupBox GroupS1;
        private System.Windows.Forms.Button btnBackGround1;
        private FontAwesome.Sharp.IconButton btnSentPID;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtD;
        private System.Windows.Forms.TextBox txtI;
        private System.Windows.Forms.TextBox txtP;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.CheckBox checkPID;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Panel panel1;
        private FontAwesome.Sharp.IconButton IconClose;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel2;
    }
}