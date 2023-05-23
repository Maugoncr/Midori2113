
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
            this.lbNumOfClycles.UseWaitCursor = true;
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
            this.txtSetPoint.Size = new System.Drawing.Size(107, 26);
            this.txtSetPoint.TabIndex = 30;
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
            this.btnSetPoint.Location = new System.Drawing.Point(384, 206);
            this.btnSetPoint.Name = "btnSetPoint";
            this.btnSetPoint.Size = new System.Drawing.Size(31, 27);
            this.btnSetPoint.TabIndex = 31;
            this.btnSetPoint.UseVisualStyleBackColor = false;
            this.btnSetPoint.Click += new System.EventHandler(this.btnSetPoint_Click);
            // 
            // lbSentSetpoint
            // 
            this.lbSentSetpoint.AutoSize = true;
            this.lbSentSetpoint.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSentSetpoint.ForeColor = System.Drawing.Color.Black;
            this.lbSentSetpoint.Location = new System.Drawing.Point(268, 182);
            this.lbSentSetpoint.Name = "lbSentSetpoint";
            this.lbSentSetpoint.Size = new System.Drawing.Size(151, 20);
            this.lbSentSetpoint.TabIndex = 32;
            this.lbSentSetpoint.Text = "Send Setpoint [Torr]";
            this.lbSentSetpoint.UseWaitCursor = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Teal;
            this.panel1.Controls.Add(this.IconClose);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(500, 27);
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
            this.IconClose.Location = new System.Drawing.Point(463, 0);
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
            this.panel3.Location = new System.Drawing.Point(495, 27);
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
            this.panel4.Size = new System.Drawing.Size(490, 5);
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
            // TestCicles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(500, 333);
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
    }
}