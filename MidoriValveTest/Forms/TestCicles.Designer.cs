
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
            this.label2 = new System.Windows.Forms.Label();
            this.lbl_instruct = new System.Windows.Forms.Label();
            this.btnTestStart = new System.Windows.Forms.Button();
            this.cbSelectTypeCycle = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.NumOfCycles)).BeginInit();
            this.SuspendLayout();
            // 
            // NumOfCycles
            // 
            this.NumOfCycles.Cursor = System.Windows.Forms.Cursors.Default;
            this.NumOfCycles.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NumOfCycles.Location = new System.Drawing.Point(146, 133);
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
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(168, 110);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 20);
            this.label2.TabIndex = 26;
            this.label2.Text = "Num Cycles";
            this.label2.UseWaitCursor = true;
            // 
            // lbl_instruct
            // 
            this.lbl_instruct.AutoSize = true;
            this.lbl_instruct.Cursor = System.Windows.Forms.Cursors.Default;
            this.lbl_instruct.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_instruct.ForeColor = System.Drawing.Color.Black;
            this.lbl_instruct.Location = new System.Drawing.Point(75, 30);
            this.lbl_instruct.Name = "lbl_instruct";
            this.lbl_instruct.Size = new System.Drawing.Size(305, 20);
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
            this.btnTestStart.Location = new System.Drawing.Point(146, 174);
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
            "[2] STABILITY TESTS 500-300-150 TORR",
            "[3] VALVE LEAK TEST"});
            this.cbSelectTypeCycle.Location = new System.Drawing.Point(56, 63);
            this.cbSelectTypeCycle.Name = "cbSelectTypeCycle";
            this.cbSelectTypeCycle.Size = new System.Drawing.Size(343, 28);
            this.cbSelectTypeCycle.TabIndex = 29;
            this.cbSelectTypeCycle.SelectionChangeCommitted += new System.EventHandler(this.cbSelectTypeCycle_SelectionChangeCommitted);
            // 
            // TestCicles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(455, 253);
            this.Controls.Add(this.cbSelectTypeCycle);
            this.Controls.Add(this.NumOfCycles);
            this.Controls.Add(this.lbl_instruct);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnTestStart);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "TestCicles";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cicles test";
            this.Load += new System.EventHandler(this.TestCicles_Load);
            ((System.ComponentModel.ISupportInitialize)(this.NumOfCycles)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnTestStart;
        private System.Windows.Forms.Label lbl_instruct;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown NumOfCycles;
        private System.Windows.Forms.ComboBox cbSelectTypeCycle;
    }
}