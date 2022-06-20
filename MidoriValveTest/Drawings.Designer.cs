namespace MidoriValveTest
{
    partial class Drawings
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
            this.NavTop = new System.Windows.Forms.Panel();
            this.NavLeft = new System.Windows.Forms.Panel();
            this.NavRight = new System.Windows.Forms.Panel();
            this.imgDraws = new System.Windows.Forms.PictureBox();
            this.IconLeft = new FontAwesome.Sharp.IconButton();
            this.IconRight = new FontAwesome.Sharp.IconButton();
            this.IconClose = new FontAwesome.Sharp.IconButton();
            this.NavTop.SuspendLayout();
            this.NavLeft.SuspendLayout();
            this.NavRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgDraws)).BeginInit();
            this.SuspendLayout();
            // 
            // NavTop
            // 
            this.NavTop.BackColor = System.Drawing.Color.DarkCyan;
            this.NavTop.Controls.Add(this.IconClose);
            this.NavTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.NavTop.Location = new System.Drawing.Point(0, 0);
            this.NavTop.Name = "NavTop";
            this.NavTop.Size = new System.Drawing.Size(1904, 30);
            this.NavTop.TabIndex = 0;
            // 
            // NavLeft
            // 
            this.NavLeft.BackColor = System.Drawing.Color.DarkCyan;
            this.NavLeft.Controls.Add(this.IconLeft);
            this.NavLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.NavLeft.Location = new System.Drawing.Point(0, 30);
            this.NavLeft.Name = "NavLeft";
            this.NavLeft.Size = new System.Drawing.Size(30, 1011);
            this.NavLeft.TabIndex = 1;
            // 
            // NavRight
            // 
            this.NavRight.BackColor = System.Drawing.Color.DarkCyan;
            this.NavRight.Controls.Add(this.IconRight);
            this.NavRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.NavRight.Location = new System.Drawing.Point(1874, 30);
            this.NavRight.Name = "NavRight";
            this.NavRight.Size = new System.Drawing.Size(30, 1011);
            this.NavRight.TabIndex = 2;
            // 
            // imgDraws
            // 
            this.imgDraws.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imgDraws.Location = new System.Drawing.Point(30, 30);
            this.imgDraws.Name = "imgDraws";
            this.imgDraws.Size = new System.Drawing.Size(1844, 1011);
            this.imgDraws.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgDraws.TabIndex = 3;
            this.imgDraws.TabStop = false;
            // 
            // IconLeft
            // 
            this.IconLeft.Dock = System.Windows.Forms.DockStyle.Top;
            this.IconLeft.FlatAppearance.BorderSize = 0;
            this.IconLeft.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IconLeft.IconChar = FontAwesome.Sharp.IconChar.ArrowLeft;
            this.IconLeft.IconColor = System.Drawing.Color.White;
            this.IconLeft.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.IconLeft.IconSize = 30;
            this.IconLeft.Location = new System.Drawing.Point(0, 0);
            this.IconLeft.Name = "IconLeft";
            this.IconLeft.Size = new System.Drawing.Size(30, 1008);
            this.IconLeft.TabIndex = 0;
            this.IconLeft.UseVisualStyleBackColor = true;
            this.IconLeft.Click += new System.EventHandler(this.IconLeft_Click);
            // 
            // IconRight
            // 
            this.IconRight.Dock = System.Windows.Forms.DockStyle.Top;
            this.IconRight.FlatAppearance.BorderSize = 0;
            this.IconRight.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IconRight.IconChar = FontAwesome.Sharp.IconChar.ArrowRight;
            this.IconRight.IconColor = System.Drawing.Color.White;
            this.IconRight.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.IconRight.IconSize = 30;
            this.IconRight.Location = new System.Drawing.Point(0, 0);
            this.IconRight.Name = "IconRight";
            this.IconRight.Size = new System.Drawing.Size(30, 1008);
            this.IconRight.TabIndex = 0;
            this.IconRight.UseVisualStyleBackColor = true;
            this.IconRight.Click += new System.EventHandler(this.IconRight_Click);
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
            this.IconClose.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.IconClose.Location = new System.Drawing.Point(1829, 0);
            this.IconClose.Name = "IconClose";
            this.IconClose.Size = new System.Drawing.Size(75, 30);
            this.IconClose.TabIndex = 0;
            this.IconClose.UseVisualStyleBackColor = true;
            this.IconClose.Click += new System.EventHandler(this.IconClose_Click);
            // 
            // Drawings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1904, 1041);
            this.Controls.Add(this.imgDraws);
            this.Controls.Add(this.NavRight);
            this.Controls.Add(this.NavLeft);
            this.Controls.Add(this.NavTop);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Drawings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Drawings";
            this.NavTop.ResumeLayout(false);
            this.NavLeft.ResumeLayout(false);
            this.NavRight.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imgDraws)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel NavTop;
        private System.Windows.Forms.Panel NavLeft;
        private System.Windows.Forms.Panel NavRight;
        private FontAwesome.Sharp.IconButton IconLeft;
        private System.Windows.Forms.PictureBox imgDraws;
        private FontAwesome.Sharp.IconButton IconClose;
        private FontAwesome.Sharp.IconButton IconRight;
    }
}