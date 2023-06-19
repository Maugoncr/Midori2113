
namespace MidoriValveTest
{
    partial class TestCicles
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TestCicles));
            this.NumOfCycles = new System.Windows.Forms.NumericUpDown();
            this.lbNumOfClycles = new System.Windows.Forms.Label();
            this.lbl_instruct = new System.Windows.Forms.Label();
            this.btnTestStart = new System.Windows.Forms.Button();
            this.cbSelectTypeCycle = new System.Windows.Forms.ComboBox();
            this.txtSetPoint = new System.Windows.Forms.TextBox();
            this.btnSetPoint = new FontAwesome.Sharp.IconButton();
            this.lbSentSetpoint = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.IconClose = new FontAwesome.Sharp.IconButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.lbOperator = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnOperator = new FontAwesome.Sharp.IconButton();
            this.label3 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.checkFunctionCycleCompare = new System.Windows.Forms.CheckBox();
            this.txtCycle1 = new System.Windows.Forms.TextBox();
            this.txtCycle6 = new System.Windows.Forms.TextBox();
            this.txtCycle2 = new System.Windows.Forms.TextBox();
            this.txtCycle7 = new System.Windows.Forms.TextBox();
            this.txtCycle3 = new System.Windows.Forms.TextBox();
            this.txtCycle8 = new System.Windows.Forms.TextBox();
            this.txtCycle4 = new System.Windows.Forms.TextBox();
            this.txtCycle9 = new System.Windows.Forms.TextBox();
            this.txtCycle5 = new System.Windows.Forms.TextBox();
            this.txtCycle10 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.NumOfCycles)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // NumOfCycles
            // 
            this.NumOfCycles.Cursor = System.Windows.Forms.Cursors.Default;
            this.NumOfCycles.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NumOfCycles.Location = new System.Drawing.Point(72, 206);
            this.NumOfCycles.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.NumOfCycles.Name = "NumOfCycles";
            this.NumOfCycles.Size = new System.Drawing.Size(136, 26);
            this.NumOfCycles.TabIndex = 28;
            this.NumOfCycles.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.NumOfCycles.ValueChanged += new System.EventHandler(this.NumOfCycles_ValueChanged);
            this.NumOfCycles.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NumOfCycles_KeyPress);
            // 
            // lbNumOfClycles
            // 
            this.lbNumOfClycles.AutoSize = true;
            this.lbNumOfClycles.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbNumOfClycles.ForeColor = System.Drawing.Color.Black;
            this.lbNumOfClycles.Location = new System.Drawing.Point(94, 183);
            this.lbNumOfClycles.Name = "lbNumOfClycles";
            this.lbNumOfClycles.Size = new System.Drawing.Size(92, 20);
            this.lbNumOfClycles.TabIndex = 26;
            this.lbNumOfClycles.Text = "Num Cycles";
            // 
            // lbl_instruct
            // 
            this.lbl_instruct.AutoSize = true;
            this.lbl_instruct.Cursor = System.Windows.Forms.Cursors.Default;
            this.lbl_instruct.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.lbl_instruct.ForeColor = System.Drawing.Color.Black;
            this.lbl_instruct.Location = new System.Drawing.Point(75, 102);
            this.lbl_instruct.Name = "lbl_instruct";
            this.lbl_instruct.Size = new System.Drawing.Size(344, 22);
            this.lbl_instruct.TabIndex = 23;
            this.lbl_instruct.Text = "Please select the test you want to perform";
            // 
            // btnTestStart
            // 
            this.btnTestStart.BackColor = System.Drawing.Color.Transparent;
            this.btnTestStart.BackgroundImage = global::MidoriValveTest.Properties.Resources.btnDisa;
            this.btnTestStart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnTestStart.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnTestStart.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnTestStart.FlatAppearance.BorderSize = 0;
            this.btnTestStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTestStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTestStart.ForeColor = System.Drawing.Color.Black;
            this.btnTestStart.Location = new System.Drawing.Point(171, 250);
            this.btnTestStart.Name = "btnTestStart";
            this.btnTestStart.Size = new System.Drawing.Size(136, 44);
            this.btnTestStart.TabIndex = 18;
            this.btnTestStart.Text = "Test Start";
            this.btnTestStart.UseVisualStyleBackColor = false;
            this.btnTestStart.Click += new System.EventHandler(this.btnTestStart_Click);
            // 
            // cbSelectTypeCycle
            // 
            this.cbSelectTypeCycle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbSelectTypeCycle.FormattingEnabled = true;
            this.cbSelectTypeCycle.Items.AddRange(new object[] {
            "[1] PRETEST CALIBRATION",
            "[2] STABILITY TEST TO SETPOINT",
            "[3] VALVE LEAK TEST"});
            this.cbSelectTypeCycle.Location = new System.Drawing.Point(72, 136);
            this.cbSelectTypeCycle.Name = "cbSelectTypeCycle";
            this.cbSelectTypeCycle.Size = new System.Drawing.Size(343, 28);
            this.cbSelectTypeCycle.TabIndex = 29;
            this.cbSelectTypeCycle.SelectionChangeCommitted += new System.EventHandler(this.cbSelectTypeCycle_SelectionChangeCommitted);
            // 
            // txtSetPoint
            // 
            this.txtSetPoint.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSetPoint.Location = new System.Drawing.Point(272, 206);
            this.txtSetPoint.Name = "txtSetPoint";
            this.txtSetPoint.ShortcutsEnabled = false;
            this.txtSetPoint.Size = new System.Drawing.Size(143, 26);
            this.txtSetPoint.TabIndex = 30;
            this.txtSetPoint.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtSetPoint.TextChanged += new System.EventHandler(this.txtSetPoint_TextChanged);
            this.txtSetPoint.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSetPoint_KeyPress);
            // 
            // btnSetPoint
            // 
            this.btnSetPoint.BackColor = System.Drawing.Color.Teal;
            this.btnSetPoint.FlatAppearance.BorderSize = 0;
            this.btnSetPoint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSetPoint.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSetPoint.IconChar = FontAwesome.Sharp.IconChar.CheckCircle;
            this.btnSetPoint.IconColor = System.Drawing.Color.White;
            this.btnSetPoint.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnSetPoint.IconSize = 28;
            this.btnSetPoint.Location = new System.Drawing.Point(5, 303);
            this.btnSetPoint.Name = "btnSetPoint";
            this.btnSetPoint.Size = new System.Drawing.Size(31, 27);
            this.btnSetPoint.TabIndex = 31;
            this.btnSetPoint.UseVisualStyleBackColor = false;
            this.btnSetPoint.Visible = false;
            this.btnSetPoint.Click += new System.EventHandler(this.btnSetPoint_Click);
            // 
            // lbSentSetpoint
            // 
            this.lbSentSetpoint.AutoSize = true;
            this.lbSentSetpoint.Cursor = System.Windows.Forms.Cursors.Default;
            this.lbSentSetpoint.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSentSetpoint.ForeColor = System.Drawing.Color.Black;
            this.lbSentSetpoint.Location = new System.Drawing.Point(268, 182);
            this.lbSentSetpoint.Name = "lbSentSetpoint";
            this.lbSentSetpoint.Size = new System.Drawing.Size(151, 20);
            this.lbSentSetpoint.TabIndex = 32;
            this.lbSentSetpoint.Text = "Send Setpoint [Torr]";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Teal;
            this.panel1.Controls.Add(this.IconClose);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(901, 27);
            this.panel1.TabIndex = 33;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            // 
            // IconClose
            // 
            this.IconClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.IconClose.FlatAppearance.BorderSize = 0;
            this.IconClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IconClose.IconChar = FontAwesome.Sharp.IconChar.TimesRectangle;
            this.IconClose.IconColor = System.Drawing.Color.White;
            this.IconClose.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.IconClose.IconSize = 30;
            this.IconClose.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.IconClose.Location = new System.Drawing.Point(864, 0);
            this.IconClose.Name = "IconClose";
            this.IconClose.Size = new System.Drawing.Size(37, 27);
            this.IconClose.TabIndex = 1;
            this.IconClose.UseVisualStyleBackColor = true;
            this.IconClose.Click += new System.EventHandler(this.IconClose_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Teal;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 27);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(5, 306);
            this.panel2.TabIndex = 34;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Teal;
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(896, 27);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(5, 306);
            this.panel3.TabIndex = 35;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Teal;
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(5, 328);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(891, 5);
            this.panel4.TabIndex = 36;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Cursor = System.Windows.Forms.Cursors.Default;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(11, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(477, 16);
            this.label1.TabIndex = 37;
            this.label1.Text = "Please be aware that the report will be issued under the current operator\'s name";
            // 
            // lbOperator
            // 
            this.lbOperator.Cursor = System.Windows.Forms.Cursors.Default;
            this.lbOperator.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbOperator.ForeColor = System.Drawing.Color.Red;
            this.lbOperator.Location = new System.Drawing.Point(11, 48);
            this.lbOperator.Name = "lbOperator";
            this.lbOperator.Size = new System.Drawing.Size(477, 16);
            this.lbOperator.TabIndex = 38;
            this.lbOperator.Text = "Operator";
            this.lbOperator.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Cursor = System.Windows.Forms.Cursors.Default;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(11, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(284, 16);
            this.label2.TabIndex = 39;
            this.label2.Text = "If you wish to make changes, please click here:";
            // 
            // btnOperator
            // 
            this.btnOperator.BackColor = System.Drawing.Color.Red;
            this.btnOperator.FlatAppearance.BorderSize = 0;
            this.btnOperator.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOperator.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOperator.IconChar = FontAwesome.Sharp.IconChar.User;
            this.btnOperator.IconColor = System.Drawing.Color.White;
            this.btnOperator.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.btnOperator.IconSize = 12;
            this.btnOperator.Location = new System.Drawing.Point(301, 65);
            this.btnOperator.Name = "btnOperator";
            this.btnOperator.Size = new System.Drawing.Size(21, 16);
            this.btnOperator.TabIndex = 40;
            this.btnOperator.UseVisualStyleBackColor = false;
            this.btnOperator.Click += new System.EventHandler(this.btnOperator_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Cursor = System.Windows.Forms.Cursors.Default;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(328, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(151, 16);
            this.label3.TabIndex = 41;
            this.label3.Text = "otherwise, disregard this";
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.Teal;
            this.panel5.Location = new System.Drawing.Point(514, 27);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(4, 303);
            this.panel5.TabIndex = 42;
            // 
            // checkFunctionCycleCompare
            // 
            this.checkFunctionCycleCompare.AutoSize = true;
            this.checkFunctionCycleCompare.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkFunctionCycleCompare.Location = new System.Drawing.Point(600, 33);
            this.checkFunctionCycleCompare.Name = "checkFunctionCycleCompare";
            this.checkFunctionCycleCompare.Size = new System.Drawing.Size(233, 22);
            this.checkFunctionCycleCompare.TabIndex = 43;
            this.checkFunctionCycleCompare.Text = "Function for Cycle Comparison";
            this.checkFunctionCycleCompare.UseVisualStyleBackColor = true;
            // 
            // txtCycle1
            // 
            this.txtCycle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCycle1.Location = new System.Drawing.Point(573, 110);
            this.txtCycle1.Name = "txtCycle1";
            this.txtCycle1.Size = new System.Drawing.Size(123, 26);
            this.txtCycle1.TabIndex = 44;
            // 
            // txtCycle6
            // 
            this.txtCycle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCycle6.Location = new System.Drawing.Point(754, 110);
            this.txtCycle6.Name = "txtCycle6";
            this.txtCycle6.Size = new System.Drawing.Size(123, 26);
            this.txtCycle6.TabIndex = 45;
            // 
            // txtCycle2
            // 
            this.txtCycle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCycle2.Location = new System.Drawing.Point(573, 155);
            this.txtCycle2.Name = "txtCycle2";
            this.txtCycle2.Size = new System.Drawing.Size(123, 26);
            this.txtCycle2.TabIndex = 46;
            // 
            // txtCycle7
            // 
            this.txtCycle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCycle7.Location = new System.Drawing.Point(754, 155);
            this.txtCycle7.Name = "txtCycle7";
            this.txtCycle7.Size = new System.Drawing.Size(123, 26);
            this.txtCycle7.TabIndex = 47;
            // 
            // txtCycle3
            // 
            this.txtCycle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCycle3.Location = new System.Drawing.Point(573, 199);
            this.txtCycle3.Name = "txtCycle3";
            this.txtCycle3.Size = new System.Drawing.Size(123, 26);
            this.txtCycle3.TabIndex = 48;
            // 
            // txtCycle8
            // 
            this.txtCycle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCycle8.Location = new System.Drawing.Point(754, 199);
            this.txtCycle8.Name = "txtCycle8";
            this.txtCycle8.Size = new System.Drawing.Size(123, 26);
            this.txtCycle8.TabIndex = 49;
            // 
            // txtCycle4
            // 
            this.txtCycle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCycle4.Location = new System.Drawing.Point(573, 239);
            this.txtCycle4.Name = "txtCycle4";
            this.txtCycle4.Size = new System.Drawing.Size(123, 26);
            this.txtCycle4.TabIndex = 50;
            // 
            // txtCycle9
            // 
            this.txtCycle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCycle9.Location = new System.Drawing.Point(754, 239);
            this.txtCycle9.Name = "txtCycle9";
            this.txtCycle9.Size = new System.Drawing.Size(123, 26);
            this.txtCycle9.TabIndex = 51;
            // 
            // txtCycle5
            // 
            this.txtCycle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCycle5.Location = new System.Drawing.Point(573, 283);
            this.txtCycle5.Name = "txtCycle5";
            this.txtCycle5.Size = new System.Drawing.Size(123, 26);
            this.txtCycle5.TabIndex = 52;
            // 
            // txtCycle10
            // 
            this.txtCycle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCycle10.Location = new System.Drawing.Point(754, 283);
            this.txtCycle10.Name = "txtCycle10";
            this.txtCycle10.Size = new System.Drawing.Size(123, 26);
            this.txtCycle10.TabIndex = 53;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(548, 118);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(19, 13);
            this.label4.TabIndex = 54;
            this.label4.Text = "    ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Blue;
            this.label5.Location = new System.Drawing.Point(548, 163);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(19, 13);
            this.label5.TabIndex = 55;
            this.label5.Text = "    ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Yellow;
            this.label6.Location = new System.Drawing.Point(548, 207);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(19, 13);
            this.label6.TabIndex = 56;
            this.label6.Text = "    ";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Green;
            this.label7.Location = new System.Drawing.Point(548, 246);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(19, 13);
            this.label7.TabIndex = 57;
            this.label7.Text = "    ";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Purple;
            this.label8.Location = new System.Drawing.Point(548, 291);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(19, 13);
            this.label8.TabIndex = 58;
            this.label8.Text = "    ";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Cyan;
            this.label9.Location = new System.Drawing.Point(729, 118);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(19, 13);
            this.label9.TabIndex = 59;
            this.label9.Text = "    ";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Orange;
            this.label10.Location = new System.Drawing.Point(729, 163);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(19, 13);
            this.label10.TabIndex = 60;
            this.label10.Text = "    ";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Lime;
            this.label11.Location = new System.Drawing.Point(729, 246);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(19, 13);
            this.label11.TabIndex = 61;
            this.label11.Text = "    ";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Maroon;
            this.label12.Location = new System.Drawing.Point(729, 207);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(19, 13);
            this.label12.TabIndex = 62;
            this.label12.Text = "    ";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Magenta;
            this.label13.Location = new System.Drawing.Point(729, 291);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(19, 13);
            this.label13.TabIndex = 63;
            this.label13.Text = "    ";
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(582, 65);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(260, 28);
            this.label14.TabIndex = 64;
            this.label14.Text = "Write the cycle number next to the color you want it to be represented";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TestCicles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(901, 333);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtCycle10);
            this.Controls.Add(this.txtCycle5);
            this.Controls.Add(this.txtCycle9);
            this.Controls.Add(this.txtCycle4);
            this.Controls.Add(this.txtCycle8);
            this.Controls.Add(this.txtCycle3);
            this.Controls.Add(this.txtCycle7);
            this.Controls.Add(this.txtCycle2);
            this.Controls.Add(this.txtCycle6);
            this.Controls.Add(this.txtCycle1);
            this.Controls.Add(this.checkFunctionCycleCompare);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnOperator);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbOperator);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lbSentSetpoint);
            this.Controls.Add(this.btnSetPoint);
            this.Controls.Add(this.txtSetPoint);
            this.Controls.Add(this.cbSelectTypeCycle);
            this.Controls.Add(this.NumOfCycles);
            this.Controls.Add(this.lbl_instruct);
            this.Controls.Add(this.lbNumOfClycles);
            this.Controls.Add(this.btnTestStart);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "TestCicles";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cycles Test";
            this.Load += new System.EventHandler(this.TestCicles_Load);
            ((System.ComponentModel.ISupportInitialize)(this.NumOfCycles)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnTestStart;
        private System.Windows.Forms.Label lbl_instruct;
        private System.Windows.Forms.Label lbNumOfClycles;
        private System.Windows.Forms.NumericUpDown NumOfCycles;
        private System.Windows.Forms.ComboBox cbSelectTypeCycle;
        private System.Windows.Forms.TextBox txtSetPoint;
        private FontAwesome.Sharp.IconButton btnSetPoint;
        private System.Windows.Forms.Label lbSentSetpoint;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private FontAwesome.Sharp.IconButton IconClose;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbOperator;
        private System.Windows.Forms.Label label2;
        private FontAwesome.Sharp.IconButton btnOperator;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.CheckBox checkFunctionCycleCompare;
        private System.Windows.Forms.TextBox txtCycle1;
        private System.Windows.Forms.TextBox txtCycle6;
        private System.Windows.Forms.TextBox txtCycle2;
        private System.Windows.Forms.TextBox txtCycle7;
        private System.Windows.Forms.TextBox txtCycle3;
        private System.Windows.Forms.TextBox txtCycle8;
        private System.Windows.Forms.TextBox txtCycle4;
        private System.Windows.Forms.TextBox txtCycle9;
        private System.Windows.Forms.TextBox txtCycle5;
        private System.Windows.Forms.TextBox txtCycle10;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
    }
}