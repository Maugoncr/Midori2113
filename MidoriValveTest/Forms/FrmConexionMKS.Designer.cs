namespace MidoriValveTest.Forms
{
    partial class FrmConexionMKS
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmConexionMKS));
            this.panel1 = new System.Windows.Forms.Panel();
            this.IconClose = new FontAwesome.Sharp.IconButton();
            this.iconRefresh = new FontAwesome.Sharp.IconButton();
            this.cbMKS2 = new System.Windows.Forms.ComboBox();
            this.cbMKS1 = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbStatusMKS1 = new System.Windows.Forms.Label();
            this.lbStatusMKS2 = new System.Windows.Forms.Label();
            this.btnConnectMKS1 = new FontAwesome.Sharp.IconButton();
            this.btnConnectMKS2 = new FontAwesome.Sharp.IconButton();
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
            this.panel1.Size = new System.Drawing.Size(570, 39);
            this.panel1.TabIndex = 0;
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
            this.IconClose.Location = new System.Drawing.Point(535, 0);
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
            this.iconRefresh.IconChar = FontAwesome.Sharp.IconChar.ArrowsRotate;
            this.iconRefresh.IconColor = System.Drawing.Color.White;
            this.iconRefresh.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconRefresh.IconSize = 30;
            this.iconRefresh.Location = new System.Drawing.Point(3, 1);
            this.iconRefresh.Name = "iconRefresh";
            this.iconRefresh.Size = new System.Drawing.Size(34, 35);
            this.iconRefresh.TabIndex = 99;
            this.iconRefresh.UseVisualStyleBackColor = true;
            this.iconRefresh.Click += new System.EventHandler(this.iconRefresh_Click);
            // 
            // cbMKS2
            // 
            this.cbMKS2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cbMKS2.BackColor = System.Drawing.Color.White;
            this.cbMKS2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMKS2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbMKS2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbMKS2.FormattingEnabled = true;
            this.cbMKS2.Location = new System.Drawing.Point(64, 96);
            this.cbMKS2.Name = "cbMKS2";
            this.cbMKS2.Size = new System.Drawing.Size(278, 26);
            this.cbMKS2.TabIndex = 101;
            // 
            // cbMKS1
            // 
            this.cbMKS1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cbMKS1.BackColor = System.Drawing.Color.White;
            this.cbMKS1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMKS1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbMKS1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbMKS1.FormattingEnabled = true;
            this.cbMKS1.Location = new System.Drawing.Point(64, 55);
            this.cbMKS1.Name = "cbMKS1";
            this.cbMKS1.Size = new System.Drawing.Size(278, 26);
            this.cbMKS1.TabIndex = 100;
            this.cbMKS1.SelectedIndexChanged += new System.EventHandler(this.cbMKS1_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(12, 58);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(46, 18);
            this.label9.TabIndex = 108;
            this.label9.Text = "EC-1:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 101);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 18);
            this.label1.TabIndex = 109;
            this.label1.Text = "EC-2:";
            // 
            // lbStatusMKS1
            // 
            this.lbStatusMKS1.AutoSize = true;
            this.lbStatusMKS1.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbStatusMKS1.Location = new System.Drawing.Point(408, 61);
            this.lbStatusMKS1.Name = "lbStatusMKS1";
            this.lbStatusMKS1.Size = new System.Drawing.Size(122, 18);
            this.lbStatusMKS1.TabIndex = 112;
            this.lbStatusMKS1.Text = "* Disconnected";
            // 
            // lbStatusMKS2
            // 
            this.lbStatusMKS2.AutoSize = true;
            this.lbStatusMKS2.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbStatusMKS2.Location = new System.Drawing.Point(408, 104);
            this.lbStatusMKS2.Name = "lbStatusMKS2";
            this.lbStatusMKS2.Size = new System.Drawing.Size(122, 18);
            this.lbStatusMKS2.TabIndex = 113;
            this.lbStatusMKS2.Text = "* Disconnected";
            // 
            // btnConnectMKS1
            // 
            this.btnConnectMKS1.BackColor = System.Drawing.Color.Transparent;
            this.btnConnectMKS1.FlatAppearance.BorderSize = 0;
            this.btnConnectMKS1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConnectMKS1.IconChar = FontAwesome.Sharp.IconChar.ToggleOff;
            this.btnConnectMKS1.IconColor = System.Drawing.Color.Black;
            this.btnConnectMKS1.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.btnConnectMKS1.Location = new System.Drawing.Point(350, 55);
            this.btnConnectMKS1.Name = "btnConnectMKS1";
            this.btnConnectMKS1.Size = new System.Drawing.Size(52, 33);
            this.btnConnectMKS1.TabIndex = 110;
            this.btnConnectMKS1.UseVisualStyleBackColor = false;
            this.btnConnectMKS1.Click += new System.EventHandler(this.btnConnectMKS1_Click);
            // 
            // btnConnectMKS2
            // 
            this.btnConnectMKS2.BackColor = System.Drawing.Color.Transparent;
            this.btnConnectMKS2.FlatAppearance.BorderSize = 0;
            this.btnConnectMKS2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConnectMKS2.IconChar = FontAwesome.Sharp.IconChar.ToggleOff;
            this.btnConnectMKS2.IconColor = System.Drawing.Color.Black;
            this.btnConnectMKS2.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.btnConnectMKS2.Location = new System.Drawing.Point(350, 94);
            this.btnConnectMKS2.Name = "btnConnectMKS2";
            this.btnConnectMKS2.Size = new System.Drawing.Size(52, 33);
            this.btnConnectMKS2.TabIndex = 111;
            this.btnConnectMKS2.UseVisualStyleBackColor = false;
            this.btnConnectMKS2.Click += new System.EventHandler(this.btnConnectMKS2_Click);
            // 
            // FrmConexionMKS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(570, 153);
            this.Controls.Add(this.lbStatusMKS2);
            this.Controls.Add(this.lbStatusMKS1);
            this.Controls.Add(this.btnConnectMKS2);
            this.Controls.Add(this.btnConnectMKS1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.cbMKS2);
            this.Controls.Add(this.cbMKS1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmConexionMKS";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Connection EC-1&2";
            this.Load += new System.EventHandler(this.FrmConexionMKS_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private FontAwesome.Sharp.IconButton IconClose;
        private FontAwesome.Sharp.IconButton iconRefresh;
        private System.Windows.Forms.ComboBox cbMKS2;
        private System.Windows.Forms.ComboBox cbMKS1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbStatusMKS1;
        private System.Windows.Forms.Label lbStatusMKS2;
        private FontAwesome.Sharp.IconButton btnConnectMKS1;
        private FontAwesome.Sharp.IconButton btnConnectMKS2;
    }
}