namespace MidoriValveTest.Forms
{
    partial class FrmAskNameReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAskNameReport));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnOk = new FontAwesome.Sharp.IconButton();
            this.txtNameReport = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnCloseMKSConexion = new FontAwesome.Sharp.IconButton();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnOk);
            this.panel1.Controls.Add(this.txtNameReport);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(600, 200);
            this.panel1.TabIndex = 0;
            // 
            // btnOk
            // 
            this.btnOk.BackColor = System.Drawing.Color.Teal;
            this.btnOk.FlatAppearance.BorderSize = 0;
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOk.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOk.ForeColor = System.Drawing.Color.White;
            this.btnOk.IconChar = FontAwesome.Sharp.IconChar.CheckCircle;
            this.btnOk.IconColor = System.Drawing.Color.White;
            this.btnOk.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnOk.IconSize = 28;
            this.btnOk.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnOk.Location = new System.Drawing.Point(254, 143);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(76, 32);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "OK";
            this.btnOk.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // txtNameReport
            // 
            this.txtNameReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNameReport.Location = new System.Drawing.Point(107, 97);
            this.txtNameReport.MaxLength = 100;
            this.txtNameReport.Name = "txtNameReport";
            this.txtNameReport.Size = new System.Drawing.Size(391, 29);
            this.txtNameReport.TabIndex = 2;
            this.txtNameReport.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(103, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(395, 24);
            this.label1.TabIndex = 1;
            this.label1.Text = "Please enter your name to generate the report";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Teal;
            this.panel2.Controls.Add(this.btnCloseMKSConexion);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(600, 33);
            this.panel2.TabIndex = 0;
            this.panel2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel2_MouseDown);
            // 
            // btnCloseMKSConexion
            // 
            this.btnCloseMKSConexion.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCloseMKSConexion.FlatAppearance.BorderSize = 0;
            this.btnCloseMKSConexion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCloseMKSConexion.IconChar = FontAwesome.Sharp.IconChar.TimesRectangle;
            this.btnCloseMKSConexion.IconColor = System.Drawing.Color.White;
            this.btnCloseMKSConexion.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnCloseMKSConexion.IconSize = 30;
            this.btnCloseMKSConexion.Location = new System.Drawing.Point(565, 0);
            this.btnCloseMKSConexion.Name = "btnCloseMKSConexion";
            this.btnCloseMKSConexion.Size = new System.Drawing.Size(35, 33);
            this.btnCloseMKSConexion.TabIndex = 42;
            this.btnCloseMKSConexion.UseVisualStyleBackColor = true;
            this.btnCloseMKSConexion.Click += new System.EventHandler(this.btnCloseMKSConexion_Click);
            // 
            // FrmAskNameReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 200);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmAskNameReport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Please Type your Name";
            this.Load += new System.EventHandler(this.FrmAskNameReport_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private FontAwesome.Sharp.IconButton btnCloseMKSConexion;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNameReport;
        private FontAwesome.Sharp.IconButton btnOk;
    }
}