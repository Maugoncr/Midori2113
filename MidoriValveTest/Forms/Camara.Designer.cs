namespace MidoriValveTest.Forms
{
    partial class Camara
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Camara));
            this.panel2 = new System.Windows.Forms.Panel();
            this.picCamara = new System.Windows.Forms.PictureBox();
            this.topNav = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.iconRefresh = new FontAwesome.Sharp.IconButton();
            this.cbCamaraSelect = new System.Windows.Forms.ComboBox();
            this.IconIniciarCam = new FontAwesome.Sharp.IconButton();
            this.iconCapture = new FontAwesome.Sharp.IconButton();
            this.iconMini = new FontAwesome.Sharp.IconButton();
            this.iconClose = new FontAwesome.Sharp.IconButton();
            this.TimerAnimation = new System.Windows.Forms.Timer(this.components);
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCamara)).BeginInit();
            this.topNav.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.picCamara);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 50);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(805, 466);
            this.panel2.TabIndex = 5;
            // 
            // picCamara
            // 
            this.picCamara.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picCamara.Image = global::MidoriValveTest.Properties.Resources.signal1;
            this.picCamara.Location = new System.Drawing.Point(0, 0);
            this.picCamara.Name = "picCamara";
            this.picCamara.Size = new System.Drawing.Size(805, 466);
            this.picCamara.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picCamara.TabIndex = 3;
            this.picCamara.TabStop = false;
            // 
            // topNav
            // 
            this.topNav.BackgroundImage = global::MidoriValveTest.Properties.Resources.image__1_;
            this.topNav.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.topNav.Controls.Add(this.panel1);
            this.topNav.Controls.Add(this.iconMini);
            this.topNav.Controls.Add(this.iconClose);
            this.topNav.Dock = System.Windows.Forms.DockStyle.Top;
            this.topNav.Location = new System.Drawing.Point(0, 0);
            this.topNav.Name = "topNav";
            this.topNav.Size = new System.Drawing.Size(805, 52);
            this.topNav.TabIndex = 4;
            this.topNav.MouseDown += new System.Windows.Forms.MouseEventHandler(this.topNav_MouseDown);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.iconRefresh);
            this.panel1.Controls.Add(this.cbCamaraSelect);
            this.panel1.Controls.Add(this.IconIniciarCam);
            this.panel1.Controls.Add(this.iconCapture);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(455, 52);
            this.panel1.TabIndex = 6;
            // 
            // iconRefresh
            // 
            this.iconRefresh.Dock = System.Windows.Forms.DockStyle.Left;
            this.iconRefresh.FlatAppearance.BorderSize = 0;
            this.iconRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconRefresh.IconChar = FontAwesome.Sharp.IconChar.Sync;
            this.iconRefresh.IconColor = System.Drawing.Color.White;
            this.iconRefresh.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconRefresh.IconSize = 30;
            this.iconRefresh.Location = new System.Drawing.Point(0, 0);
            this.iconRefresh.Name = "iconRefresh";
            this.iconRefresh.Size = new System.Drawing.Size(43, 52);
            this.iconRefresh.TabIndex = 3;
            this.iconRefresh.UseVisualStyleBackColor = true;
            this.iconRefresh.Click += new System.EventHandler(this.iconRefresh_Click);
            // 
            // cbCamaraSelect
            // 
            this.cbCamaraSelect.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cbCamaraSelect.BackColor = System.Drawing.Color.White;
            this.cbCamaraSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCamaraSelect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbCamaraSelect.FormattingEnabled = true;
            this.cbCamaraSelect.Location = new System.Drawing.Point(49, 17);
            this.cbCamaraSelect.Name = "cbCamaraSelect";
            this.cbCamaraSelect.Size = new System.Drawing.Size(278, 21);
            this.cbCamaraSelect.TabIndex = 0;
            this.cbCamaraSelect.SelectedIndexChanged += new System.EventHandler(this.cbCamaraSelect_SelectedIndexChanged);
            // 
            // IconIniciarCam
            // 
            this.IconIniciarCam.BackColor = System.Drawing.Color.Transparent;
            this.IconIniciarCam.Dock = System.Windows.Forms.DockStyle.Right;
            this.IconIniciarCam.FlatAppearance.BorderSize = 0;
            this.IconIniciarCam.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IconIniciarCam.ForeColor = System.Drawing.Color.White;
            this.IconIniciarCam.IconChar = FontAwesome.Sharp.IconChar.PowerOff;
            this.IconIniciarCam.IconColor = System.Drawing.Color.White;
            this.IconIniciarCam.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.IconIniciarCam.IconSize = 30;
            this.IconIniciarCam.Location = new System.Drawing.Point(333, 0);
            this.IconIniciarCam.Name = "IconIniciarCam";
            this.IconIniciarCam.Size = new System.Drawing.Size(61, 52);
            this.IconIniciarCam.TabIndex = 1;
            this.IconIniciarCam.UseVisualStyleBackColor = false;
            this.IconIniciarCam.Click += new System.EventHandler(this.IconIniciarCam_Click);
            // 
            // iconCapture
            // 
            this.iconCapture.BackColor = System.Drawing.Color.Transparent;
            this.iconCapture.Dock = System.Windows.Forms.DockStyle.Right;
            this.iconCapture.FlatAppearance.BorderSize = 0;
            this.iconCapture.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconCapture.ForeColor = System.Drawing.Color.White;
            this.iconCapture.IconChar = FontAwesome.Sharp.IconChar.Camera;
            this.iconCapture.IconColor = System.Drawing.Color.White;
            this.iconCapture.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconCapture.IconSize = 30;
            this.iconCapture.Location = new System.Drawing.Point(394, 0);
            this.iconCapture.Name = "iconCapture";
            this.iconCapture.Size = new System.Drawing.Size(61, 52);
            this.iconCapture.TabIndex = 2;
            this.iconCapture.UseVisualStyleBackColor = false;
            this.iconCapture.Click += new System.EventHandler(this.iconCapture_Click);
            // 
            // iconMini
            // 
            this.iconMini.BackColor = System.Drawing.Color.Transparent;
            this.iconMini.Dock = System.Windows.Forms.DockStyle.Right;
            this.iconMini.FlatAppearance.BorderSize = 0;
            this.iconMini.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconMini.IconChar = FontAwesome.Sharp.IconChar.WindowMinimize;
            this.iconMini.IconColor = System.Drawing.Color.White;
            this.iconMini.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconMini.IconSize = 30;
            this.iconMini.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.iconMini.Location = new System.Drawing.Point(727, 0);
            this.iconMini.Name = "iconMini";
            this.iconMini.Size = new System.Drawing.Size(39, 52);
            this.iconMini.TabIndex = 5;
            this.iconMini.UseVisualStyleBackColor = false;
            this.iconMini.Click += new System.EventHandler(this.iconMini_Click);
            // 
            // iconClose
            // 
            this.iconClose.BackColor = System.Drawing.Color.Transparent;
            this.iconClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.iconClose.FlatAppearance.BorderSize = 0;
            this.iconClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconClose.IconChar = FontAwesome.Sharp.IconChar.WindowClose;
            this.iconClose.IconColor = System.Drawing.Color.White;
            this.iconClose.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconClose.IconSize = 30;
            this.iconClose.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.iconClose.Location = new System.Drawing.Point(766, 0);
            this.iconClose.Name = "iconClose";
            this.iconClose.Size = new System.Drawing.Size(39, 52);
            this.iconClose.TabIndex = 3;
            this.iconClose.UseVisualStyleBackColor = false;
            this.iconClose.Click += new System.EventHandler(this.iconClose_Click);
            // 
            // TimerAnimation
            // 
            this.TimerAnimation.Interval = 1000;
            this.TimerAnimation.Tick += new System.EventHandler(this.TimerAnimation_Tick);
            // 
            // Camara
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(805, 516);
            this.Controls.Add(this.topNav);
            this.Controls.Add(this.panel2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Camara";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Camera";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Camara_FormClosed);
            this.Load += new System.EventHandler(this.Camara_Load);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picCamara)).EndInit();
            this.topNav.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbCamaraSelect;
        private FontAwesome.Sharp.IconButton IconIniciarCam;
        private FontAwesome.Sharp.IconButton iconCapture;
        private System.Windows.Forms.PictureBox picCamara;
        private System.Windows.Forms.Panel topNav;
        private FontAwesome.Sharp.IconButton iconMini;
        private FontAwesome.Sharp.IconButton iconClose;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Timer TimerAnimation;
        private FontAwesome.Sharp.IconButton iconRefresh;
    }
}