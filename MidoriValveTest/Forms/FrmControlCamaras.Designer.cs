namespace MidoriValveTest.Forms
{
    partial class FrmControlCamaras
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmControlCamaras));
            this.panel1 = new System.Windows.Forms.Panel();
            this.IconClose = new FontAwesome.Sharp.IconButton();
            this.iconRefresh = new FontAwesome.Sharp.IconButton();
            this.cbCamaraSelect4 = new System.Windows.Forms.ComboBox();
            this.cbCamaraSelect3 = new System.Windows.Forms.ComboBox();
            this.cbCamaraSelect2 = new System.Windows.Forms.ComboBox();
            this.cbCamaraSelect = new System.Windows.Forms.ComboBox();
            this.IconIniciarCam4 = new FontAwesome.Sharp.IconButton();
            this.IconIniciarCam3 = new FontAwesome.Sharp.IconButton();
            this.IconIniciarCam2 = new FontAwesome.Sharp.IconButton();
            this.IconIniciarCam = new FontAwesome.Sharp.IconButton();
            this.label9 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.IconIniciarCam5 = new FontAwesome.Sharp.IconButton();
            this.cbCamaraSelect5 = new System.Windows.Forms.ComboBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Teal;
            this.panel1.Controls.Add(this.IconClose);
            this.panel1.Controls.Add(this.iconRefresh);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(425, 39);
            this.panel1.TabIndex = 0;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
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
            this.IconClose.Location = new System.Drawing.Point(390, 0);
            this.IconClose.Name = "IconClose";
            this.IconClose.Size = new System.Drawing.Size(35, 39);
            this.IconClose.TabIndex = 40;
            this.IconClose.UseVisualStyleBackColor = true;
            this.IconClose.Click += new System.EventHandler(this.IconClose_Click);
            // 
            // iconRefresh
            // 
            this.iconRefresh.FlatAppearance.BorderSize = 0;
            this.iconRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconRefresh.ForeColor = System.Drawing.Color.White;
            this.iconRefresh.IconChar = FontAwesome.Sharp.IconChar.Sync;
            this.iconRefresh.IconColor = System.Drawing.Color.White;
            this.iconRefresh.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconRefresh.IconSize = 30;
            this.iconRefresh.Location = new System.Drawing.Point(3, 1);
            this.iconRefresh.Name = "iconRefresh";
            this.iconRefresh.Size = new System.Drawing.Size(43, 36);
            this.iconRefresh.TabIndex = 99;
            this.iconRefresh.UseVisualStyleBackColor = true;
            this.iconRefresh.Click += new System.EventHandler(this.iconRefresh_Click);
            // 
            // cbCamaraSelect4
            // 
            this.cbCamaraSelect4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cbCamaraSelect4.BackColor = System.Drawing.Color.White;
            this.cbCamaraSelect4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCamaraSelect4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbCamaraSelect4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCamaraSelect4.FormattingEnabled = true;
            this.cbCamaraSelect4.Location = new System.Drawing.Point(116, 182);
            this.cbCamaraSelect4.Name = "cbCamaraSelect4";
            this.cbCamaraSelect4.Size = new System.Drawing.Size(278, 26);
            this.cbCamaraSelect4.TabIndex = 103;
            // 
            // cbCamaraSelect3
            // 
            this.cbCamaraSelect3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cbCamaraSelect3.BackColor = System.Drawing.Color.White;
            this.cbCamaraSelect3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCamaraSelect3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbCamaraSelect3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCamaraSelect3.FormattingEnabled = true;
            this.cbCamaraSelect3.Location = new System.Drawing.Point(116, 141);
            this.cbCamaraSelect3.Name = "cbCamaraSelect3";
            this.cbCamaraSelect3.Size = new System.Drawing.Size(278, 26);
            this.cbCamaraSelect3.TabIndex = 102;
            // 
            // cbCamaraSelect2
            // 
            this.cbCamaraSelect2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cbCamaraSelect2.BackColor = System.Drawing.Color.White;
            this.cbCamaraSelect2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCamaraSelect2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbCamaraSelect2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCamaraSelect2.FormattingEnabled = true;
            this.cbCamaraSelect2.Location = new System.Drawing.Point(116, 98);
            this.cbCamaraSelect2.Name = "cbCamaraSelect2";
            this.cbCamaraSelect2.Size = new System.Drawing.Size(278, 26);
            this.cbCamaraSelect2.TabIndex = 101;
            // 
            // cbCamaraSelect
            // 
            this.cbCamaraSelect.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cbCamaraSelect.BackColor = System.Drawing.Color.White;
            this.cbCamaraSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCamaraSelect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbCamaraSelect.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCamaraSelect.FormattingEnabled = true;
            this.cbCamaraSelect.Location = new System.Drawing.Point(116, 55);
            this.cbCamaraSelect.Name = "cbCamaraSelect";
            this.cbCamaraSelect.Size = new System.Drawing.Size(278, 26);
            this.cbCamaraSelect.TabIndex = 100;
            // 
            // IconIniciarCam4
            // 
            this.IconIniciarCam4.BackColor = System.Drawing.Color.Transparent;
            this.IconIniciarCam4.FlatAppearance.BorderSize = 0;
            this.IconIniciarCam4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IconIniciarCam4.ForeColor = System.Drawing.Color.White;
            this.IconIniciarCam4.IconChar = FontAwesome.Sharp.IconChar.PowerOff;
            this.IconIniciarCam4.IconColor = System.Drawing.Color.Black;
            this.IconIniciarCam4.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.IconIniciarCam4.IconSize = 20;
            this.IconIniciarCam4.Location = new System.Drawing.Point(82, 182);
            this.IconIniciarCam4.Name = "IconIniciarCam4";
            this.IconIniciarCam4.Size = new System.Drawing.Size(28, 27);
            this.IconIniciarCam4.TabIndex = 107;
            this.IconIniciarCam4.UseVisualStyleBackColor = false;
            this.IconIniciarCam4.Click += new System.EventHandler(this.IconIniciarCam4_Click);
            // 
            // IconIniciarCam3
            // 
            this.IconIniciarCam3.BackColor = System.Drawing.Color.Transparent;
            this.IconIniciarCam3.FlatAppearance.BorderSize = 0;
            this.IconIniciarCam3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IconIniciarCam3.ForeColor = System.Drawing.Color.White;
            this.IconIniciarCam3.IconChar = FontAwesome.Sharp.IconChar.PowerOff;
            this.IconIniciarCam3.IconColor = System.Drawing.Color.Black;
            this.IconIniciarCam3.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.IconIniciarCam3.IconSize = 20;
            this.IconIniciarCam3.Location = new System.Drawing.Point(82, 140);
            this.IconIniciarCam3.Name = "IconIniciarCam3";
            this.IconIniciarCam3.Size = new System.Drawing.Size(28, 27);
            this.IconIniciarCam3.TabIndex = 106;
            this.IconIniciarCam3.UseVisualStyleBackColor = false;
            this.IconIniciarCam3.Click += new System.EventHandler(this.IconIniciarCam3_Click);
            // 
            // IconIniciarCam2
            // 
            this.IconIniciarCam2.BackColor = System.Drawing.Color.Transparent;
            this.IconIniciarCam2.FlatAppearance.BorderSize = 0;
            this.IconIniciarCam2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IconIniciarCam2.ForeColor = System.Drawing.Color.White;
            this.IconIniciarCam2.IconChar = FontAwesome.Sharp.IconChar.PowerOff;
            this.IconIniciarCam2.IconColor = System.Drawing.Color.Black;
            this.IconIniciarCam2.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.IconIniciarCam2.IconSize = 20;
            this.IconIniciarCam2.Location = new System.Drawing.Point(82, 98);
            this.IconIniciarCam2.Name = "IconIniciarCam2";
            this.IconIniciarCam2.Size = new System.Drawing.Size(28, 27);
            this.IconIniciarCam2.TabIndex = 105;
            this.IconIniciarCam2.UseVisualStyleBackColor = false;
            this.IconIniciarCam2.Click += new System.EventHandler(this.IconIniciarCam2_Click);
            // 
            // IconIniciarCam
            // 
            this.IconIniciarCam.BackColor = System.Drawing.Color.Transparent;
            this.IconIniciarCam.FlatAppearance.BorderSize = 0;
            this.IconIniciarCam.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IconIniciarCam.ForeColor = System.Drawing.Color.White;
            this.IconIniciarCam.IconChar = FontAwesome.Sharp.IconChar.PowerOff;
            this.IconIniciarCam.IconColor = System.Drawing.Color.Black;
            this.IconIniciarCam.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.IconIniciarCam.IconSize = 20;
            this.IconIniciarCam.Location = new System.Drawing.Point(82, 55);
            this.IconIniciarCam.Name = "IconIniciarCam";
            this.IconIniciarCam.Size = new System.Drawing.Size(28, 27);
            this.IconIniciarCam.TabIndex = 104;
            this.IconIniciarCam.UseVisualStyleBackColor = false;
            this.IconIniciarCam.Click += new System.EventHandler(this.IconIniciarCam_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(12, 58);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(69, 18);
            this.label9.TabIndex = 108;
            this.label9.Text = "Cam 1#:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 101);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 18);
            this.label1.TabIndex = 109;
            this.label1.Text = "Cam 2#:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 143);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 18);
            this.label2.TabIndex = 110;
            this.label2.Text = "Cam 3#:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 185);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 18);
            this.label3.TabIndex = 111;
            this.label3.Text = "Cam 4#:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 222);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 18);
            this.label4.TabIndex = 114;
            this.label4.Text = "Cam 5#:";
            // 
            // IconIniciarCam5
            // 
            this.IconIniciarCam5.BackColor = System.Drawing.Color.Transparent;
            this.IconIniciarCam5.FlatAppearance.BorderSize = 0;
            this.IconIniciarCam5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IconIniciarCam5.ForeColor = System.Drawing.Color.White;
            this.IconIniciarCam5.IconChar = FontAwesome.Sharp.IconChar.PowerOff;
            this.IconIniciarCam5.IconColor = System.Drawing.Color.Black;
            this.IconIniciarCam5.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.IconIniciarCam5.IconSize = 20;
            this.IconIniciarCam5.Location = new System.Drawing.Point(82, 219);
            this.IconIniciarCam5.Name = "IconIniciarCam5";
            this.IconIniciarCam5.Size = new System.Drawing.Size(28, 27);
            this.IconIniciarCam5.TabIndex = 113;
            this.IconIniciarCam5.UseVisualStyleBackColor = false;
            this.IconIniciarCam5.Click += new System.EventHandler(this.IconIniciarCam5_Click);
            // 
            // cbCamaraSelect5
            // 
            this.cbCamaraSelect5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cbCamaraSelect5.BackColor = System.Drawing.Color.White;
            this.cbCamaraSelect5.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCamaraSelect5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbCamaraSelect5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCamaraSelect5.FormattingEnabled = true;
            this.cbCamaraSelect5.Location = new System.Drawing.Point(116, 219);
            this.cbCamaraSelect5.Name = "cbCamaraSelect5";
            this.cbCamaraSelect5.Size = new System.Drawing.Size(278, 26);
            this.cbCamaraSelect5.TabIndex = 112;
            // 
            // FrmControlCamaras
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(425, 259);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.IconIniciarCam5);
            this.Controls.Add(this.cbCamaraSelect5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.IconIniciarCam4);
            this.Controls.Add(this.IconIniciarCam3);
            this.Controls.Add(this.IconIniciarCam2);
            this.Controls.Add(this.IconIniciarCam);
            this.Controls.Add(this.cbCamaraSelect4);
            this.Controls.Add(this.cbCamaraSelect3);
            this.Controls.Add(this.cbCamaraSelect2);
            this.Controls.Add(this.cbCamaraSelect);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmControlCamaras";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Management Camara";
            this.Load += new System.EventHandler(this.FrmControlCamaras_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private FontAwesome.Sharp.IconButton IconClose;
        private FontAwesome.Sharp.IconButton iconRefresh;
        private FontAwesome.Sharp.IconButton IconIniciarCam4;
        private FontAwesome.Sharp.IconButton IconIniciarCam3;
        private FontAwesome.Sharp.IconButton IconIniciarCam2;
        private FontAwesome.Sharp.IconButton IconIniciarCam;
        private System.Windows.Forms.ComboBox cbCamaraSelect4;
        private System.Windows.Forms.ComboBox cbCamaraSelect3;
        private System.Windows.Forms.ComboBox cbCamaraSelect2;
        private System.Windows.Forms.ComboBox cbCamaraSelect;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private FontAwesome.Sharp.IconButton IconIniciarCam5;
        private System.Windows.Forms.ComboBox cbCamaraSelect5;
    }
}