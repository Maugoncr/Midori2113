namespace MidoriValveTest.Forms
{
    partial class FrmDontTouch
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDontTouch));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnMinus = new FontAwesome.Sharp.IconButton();
            this.btnPlus = new FontAwesome.Sharp.IconButton();
            this.btnUp = new FontAwesome.Sharp.IconButton();
            this.btnDown = new FontAwesome.Sharp.IconButton();
            this.lbBig = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.GifBig = new System.Windows.Forms.PictureBox();
            this.lbMin = new System.Windows.Forms.Label();
            this.GifMin = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GifBig)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GifMin)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DarkOrange;
            this.panel1.Controls.Add(this.btnMinus);
            this.panel1.Controls.Add(this.btnPlus);
            this.panel1.Controls.Add(this.btnUp);
            this.panel1.Controls.Add(this.btnDown);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(400, 31);
            this.panel1.TabIndex = 0;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            // 
            // btnMinus
            // 
            this.btnMinus.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnMinus.FlatAppearance.BorderSize = 0;
            this.btnMinus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinus.IconChar = FontAwesome.Sharp.IconChar.CircleMinus;
            this.btnMinus.IconColor = System.Drawing.Color.White;
            this.btnMinus.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnMinus.IconSize = 28;
            this.btnMinus.Location = new System.Drawing.Point(35, 0);
            this.btnMinus.Name = "btnMinus";
            this.btnMinus.Size = new System.Drawing.Size(35, 31);
            this.btnMinus.TabIndex = 45;
            this.btnMinus.UseVisualStyleBackColor = true;
            this.btnMinus.Click += new System.EventHandler(this.btnMinus_Click);
            // 
            // btnPlus
            // 
            this.btnPlus.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnPlus.FlatAppearance.BorderSize = 0;
            this.btnPlus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlus.IconChar = FontAwesome.Sharp.IconChar.PlusCircle;
            this.btnPlus.IconColor = System.Drawing.Color.White;
            this.btnPlus.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnPlus.IconSize = 28;
            this.btnPlus.Location = new System.Drawing.Point(0, 0);
            this.btnPlus.Name = "btnPlus";
            this.btnPlus.Size = new System.Drawing.Size(35, 31);
            this.btnPlus.TabIndex = 44;
            this.btnPlus.UseVisualStyleBackColor = true;
            this.btnPlus.Click += new System.EventHandler(this.btnPlus_Click);
            // 
            // btnUp
            // 
            this.btnUp.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnUp.FlatAppearance.BorderSize = 0;
            this.btnUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUp.IconChar = FontAwesome.Sharp.IconChar.ChevronCircleUp;
            this.btnUp.IconColor = System.Drawing.Color.White;
            this.btnUp.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnUp.IconSize = 28;
            this.btnUp.Location = new System.Drawing.Point(330, 0);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(35, 31);
            this.btnUp.TabIndex = 43;
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // btnDown
            // 
            this.btnDown.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnDown.FlatAppearance.BorderSize = 0;
            this.btnDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDown.IconChar = FontAwesome.Sharp.IconChar.ChevronCircleDown;
            this.btnDown.IconColor = System.Drawing.Color.White;
            this.btnDown.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnDown.IconSize = 28;
            this.btnDown.Location = new System.Drawing.Point(365, 0);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(35, 31);
            this.btnDown.TabIndex = 42;
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // lbBig
            // 
            this.lbBig.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbBig.Location = new System.Drawing.Point(12, 34);
            this.lbBig.Name = "lbBig";
            this.lbBig.Size = new System.Drawing.Size(373, 58);
            this.lbBig.TabIndex = 2;
            this.lbBig.Text = "A cycle phase is currently in progress. Please do not touch anything!";
            this.lbBig.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // GifBig
            // 
            this.GifBig.Image = global::MidoriValveTest.Properties.Resources.Bigger;
            this.GifBig.Location = new System.Drawing.Point(145, 95);
            this.GifBig.Name = "GifBig";
            this.GifBig.Size = new System.Drawing.Size(90, 90);
            this.GifBig.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.GifBig.TabIndex = 1;
            this.GifBig.TabStop = false;
            // 
            // lbMin
            // 
            this.lbMin.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMin.Location = new System.Drawing.Point(10, 35);
            this.lbMin.Name = "lbMin";
            this.lbMin.Size = new System.Drawing.Size(314, 58);
            this.lbMin.TabIndex = 3;
            this.lbMin.Text = "A cycle phase is currently in progress. Please do not touch anything!";
            this.lbMin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GifMin
            // 
            this.GifMin.Image = global::MidoriValveTest.Properties.Resources.Bigger;
            this.GifMin.Location = new System.Drawing.Point(330, 40);
            this.GifMin.Name = "GifMin";
            this.GifMin.Size = new System.Drawing.Size(45, 45);
            this.GifMin.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.GifMin.TabIndex = 4;
            this.GifMin.TabStop = false;
            // 
            // FrmDontTouch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(400, 203);
            this.Controls.Add(this.GifMin);
            this.Controls.Add(this.lbMin);
            this.Controls.Add(this.lbBig);
            this.Controls.Add(this.GifBig);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmDontTouch";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Don\'t Touch Anything!";
            this.TopMost = true;
            this.Activated += new System.EventHandler(this.FrmDontTouch_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmDontTouch_FormClosing);
            this.Load += new System.EventHandler(this.FrmDontTouch_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GifBig)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GifMin)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox GifBig;
        private System.Windows.Forms.Label lbBig;
        private FontAwesome.Sharp.IconButton btnUp;
        private FontAwesome.Sharp.IconButton btnDown;
        private System.Windows.Forms.Timer timer1;
        private FontAwesome.Sharp.IconButton btnPlus;
        private FontAwesome.Sharp.IconButton btnMinus;
        private System.Windows.Forms.Label lbMin;
        private System.Windows.Forms.PictureBox GifMin;
    }
}