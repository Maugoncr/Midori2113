
namespace MidoriValveTest
{
    partial class FrmTerminal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTerminal));
            this.btnReceive = new System.Windows.Forms.Button();
            this.btnSend = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnOpen = new System.Windows.Forms.Button();
            this.cboxPort = new System.Windows.Forms.ComboBox();
            this.txtReceive = new System.Windows.Forms.TextBox();
            this.txtSend = new System.Windows.Forms.TextBox();
            this.cbBaudRate = new System.Windows.Forms.ComboBox();
            this.cbParity = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCloseForm = new FontAwesome.Sharp.IconButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.btnClearSend = new System.Windows.Forms.Button();
            this.btnClearReceive = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnSlide = new FontAwesome.Sharp.IconButton();
            this.txtInformacionUtil = new System.Windows.Forms.RichTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnLoadInfo = new FontAwesome.Sharp.IconButton();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnReceive
            // 
            this.btnReceive.BackColor = System.Drawing.Color.Teal;
            this.btnReceive.FlatAppearance.BorderSize = 0;
            this.btnReceive.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReceive.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReceive.ForeColor = System.Drawing.Color.White;
            this.btnReceive.Location = new System.Drawing.Point(312, 487);
            this.btnReceive.Name = "btnReceive";
            this.btnReceive.Size = new System.Drawing.Size(75, 23);
            this.btnReceive.TabIndex = 14;
            this.btnReceive.Text = "Receive";
            this.btnReceive.UseVisualStyleBackColor = false;
            this.btnReceive.Click += new System.EventHandler(this.btnReceive_Click);
            // 
            // btnSend
            // 
            this.btnSend.BackColor = System.Drawing.Color.Teal;
            this.btnSend.FlatAppearance.BorderSize = 0;
            this.btnSend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSend.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSend.ForeColor = System.Drawing.Color.White;
            this.btnSend.Location = new System.Drawing.Point(312, 309);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 13;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = false;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Teal;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(312, 129);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 12;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnOpen
            // 
            this.btnOpen.BackColor = System.Drawing.Color.Teal;
            this.btnOpen.FlatAppearance.BorderSize = 0;
            this.btnOpen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpen.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpen.ForeColor = System.Drawing.Color.White;
            this.btnOpen.Location = new System.Drawing.Point(213, 129);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(75, 23);
            this.btnOpen.TabIndex = 11;
            this.btnOpen.Text = "Open";
            this.btnOpen.UseVisualStyleBackColor = false;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // cboxPort
            // 
            this.cboxPort.BackColor = System.Drawing.Color.White;
            this.cboxPort.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboxPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboxPort.ForeColor = System.Drawing.Color.Black;
            this.cboxPort.FormattingEnabled = true;
            this.cboxPort.Location = new System.Drawing.Point(16, 68);
            this.cboxPort.Name = "cboxPort";
            this.cboxPort.Size = new System.Drawing.Size(176, 24);
            this.cboxPort.TabIndex = 10;
            // 
            // txtReceive
            // 
            this.txtReceive.BackColor = System.Drawing.Color.Teal;
            this.txtReceive.ForeColor = System.Drawing.Color.White;
            this.txtReceive.Location = new System.Drawing.Point(16, 353);
            this.txtReceive.Multiline = true;
            this.txtReceive.Name = "txtReceive";
            this.txtReceive.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtReceive.Size = new System.Drawing.Size(371, 128);
            this.txtReceive.TabIndex = 9;
            this.txtReceive.TextChanged += new System.EventHandler(this.txtReceive_TextChanged);
            // 
            // txtSend
            // 
            this.txtSend.BackColor = System.Drawing.Color.Teal;
            this.txtSend.ForeColor = System.Drawing.Color.White;
            this.txtSend.Location = new System.Drawing.Point(16, 166);
            this.txtSend.Multiline = true;
            this.txtSend.Name = "txtSend";
            this.txtSend.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtSend.Size = new System.Drawing.Size(371, 137);
            this.txtSend.TabIndex = 8;
            // 
            // cbBaudRate
            // 
            this.cbBaudRate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbBaudRate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbBaudRate.FormattingEnabled = true;
            this.cbBaudRate.Items.AddRange(new object[] {
            "9600",
            "19200",
            "38400",
            "57600",
            "115200"});
            this.cbBaudRate.Location = new System.Drawing.Point(16, 129);
            this.cbBaudRate.Name = "cbBaudRate";
            this.cbBaudRate.Size = new System.Drawing.Size(176, 24);
            this.cbBaudRate.TabIndex = 15;
            this.cbBaudRate.SelectedIndexChanged += new System.EventHandler(this.cbBaudRate_SelectedIndexChanged);
            // 
            // cbParity
            // 
            this.cbParity.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbParity.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbParity.FormattingEnabled = true;
            this.cbParity.Items.AddRange(new object[] {
            "None",
            "Odd",
            "Even",
            "Mark",
            "Space"});
            this.cbParity.Location = new System.Drawing.Point(213, 68);
            this.cbParity.Name = "cbParity";
            this.cbParity.Size = new System.Drawing.Size(174, 24);
            this.cbParity.TabIndex = 16;
            this.cbParity.SelectedIndexChanged += new System.EventHandler(this.cbParity_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Teal;
            this.panel1.Controls.Add(this.btnCloseForm);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(428, 33);
            this.panel1.TabIndex = 17;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            // 
            // btnCloseForm
            // 
            this.btnCloseForm.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCloseForm.FlatAppearance.BorderSize = 0;
            this.btnCloseForm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCloseForm.IconChar = FontAwesome.Sharp.IconChar.TimesRectangle;
            this.btnCloseForm.IconColor = System.Drawing.Color.White;
            this.btnCloseForm.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnCloseForm.IconSize = 30;
            this.btnCloseForm.Location = new System.Drawing.Point(393, 0);
            this.btnCloseForm.Name = "btnCloseForm";
            this.btnCloseForm.Size = new System.Drawing.Size(35, 33);
            this.btnCloseForm.TabIndex = 42;
            this.btnCloseForm.UseVisualStyleBackColor = true;
            this.btnCloseForm.Click += new System.EventHandler(this.btnCloseForm_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 18);
            this.label1.TabIndex = 18;
            this.label1.Text = "COMPort";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(13, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 18);
            this.label2.TabIndex = 19;
            this.label2.Text = "BaudRate";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(210, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 18);
            this.label3.TabIndex = 20;
            this.label3.Text = "Parity";
            // 
            // serialPort1
            // 
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
            // 
            // btnClearSend
            // 
            this.btnClearSend.BackColor = System.Drawing.Color.Teal;
            this.btnClearSend.FlatAppearance.BorderSize = 0;
            this.btnClearSend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearSend.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearSend.ForeColor = System.Drawing.Color.White;
            this.btnClearSend.Location = new System.Drawing.Point(16, 309);
            this.btnClearSend.Name = "btnClearSend";
            this.btnClearSend.Size = new System.Drawing.Size(50, 23);
            this.btnClearSend.TabIndex = 21;
            this.btnClearSend.Text = "Clear";
            this.btnClearSend.UseVisualStyleBackColor = false;
            this.btnClearSend.Click += new System.EventHandler(this.btnClearSend_Click);
            // 
            // btnClearReceive
            // 
            this.btnClearReceive.BackColor = System.Drawing.Color.Teal;
            this.btnClearReceive.FlatAppearance.BorderSize = 0;
            this.btnClearReceive.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearReceive.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearReceive.ForeColor = System.Drawing.Color.White;
            this.btnClearReceive.Location = new System.Drawing.Point(16, 487);
            this.btnClearReceive.Name = "btnClearReceive";
            this.btnClearReceive.Size = new System.Drawing.Size(50, 23);
            this.btnClearReceive.TabIndex = 22;
            this.btnClearReceive.Text = "Clear";
            this.btnClearReceive.UseVisualStyleBackColor = false;
            this.btnClearReceive.Click += new System.EventHandler(this.btnClearReceive_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Teal;
            this.panel2.Controls.Add(this.btnSlide);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(404, 33);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(24, 503);
            this.panel2.TabIndex = 23;
            // 
            // btnSlide
            // 
            this.btnSlide.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSlide.FlatAppearance.BorderSize = 0;
            this.btnSlide.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSlide.IconChar = FontAwesome.Sharp.IconChar.CircleChevronRight;
            this.btnSlide.IconColor = System.Drawing.Color.White;
            this.btnSlide.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnSlide.IconSize = 25;
            this.btnSlide.Location = new System.Drawing.Point(1, 0);
            this.btnSlide.Name = "btnSlide";
            this.btnSlide.Size = new System.Drawing.Size(23, 503);
            this.btnSlide.TabIndex = 0;
            this.btnSlide.UseVisualStyleBackColor = true;
            this.btnSlide.Click += new System.EventHandler(this.btnSlide_Click);
            // 
            // txtInformacionUtil
            // 
            this.txtInformacionUtil.BackColor = System.Drawing.Color.Teal;
            this.txtInformacionUtil.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInformacionUtil.ForeColor = System.Drawing.Color.White;
            this.txtInformacionUtil.Location = new System.Drawing.Point(429, 129);
            this.txtInformacionUtil.Name = "txtInformacionUtil";
            this.txtInformacionUtil.Size = new System.Drawing.Size(217, 352);
            this.txtInformacionUtil.TabIndex = 24;
            this.txtInformacionUtil.Text = "";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(446, 99);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(186, 18);
            this.label4.TabIndex = 25;
            this.label4.Text = "Potential useful information";
            // 
            // btnLoadInfo
            // 
            this.btnLoadInfo.BackColor = System.Drawing.Color.Transparent;
            this.btnLoadInfo.FlatAppearance.BorderSize = 0;
            this.btnLoadInfo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoadInfo.IconChar = FontAwesome.Sharp.IconChar.Repeat;
            this.btnLoadInfo.IconColor = System.Drawing.Color.Teal;
            this.btnLoadInfo.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.btnLoadInfo.IconSize = 25;
            this.btnLoadInfo.Location = new System.Drawing.Point(520, 487);
            this.btnLoadInfo.Name = "btnLoadInfo";
            this.btnLoadInfo.Size = new System.Drawing.Size(33, 23);
            this.btnLoadInfo.TabIndex = 175;
            this.btnLoadInfo.UseVisualStyleBackColor = false;
            this.btnLoadInfo.Click += new System.EventHandler(this.btnLoadInfo_Click);
            // 
            // FrmTerminal
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(428, 536);
            this.Controls.Add(this.btnLoadInfo);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtInformacionUtil);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.btnClearReceive);
            this.Controls.Add(this.btnClearSend);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.cbParity);
            this.Controls.Add(this.cbBaudRate);
            this.Controls.Add(this.btnReceive);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.cboxPort);
            this.Controls.Add(this.txtReceive);
            this.Controls.Add(this.txtSend);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmTerminal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Terminal";
            this.Load += new System.EventHandler(this.FrmTerminal_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnReceive;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.ComboBox cboxPort;
        private System.Windows.Forms.TextBox txtReceive;
        private System.Windows.Forms.TextBox txtSend;
        private System.Windows.Forms.ComboBox cbBaudRate;
        private System.Windows.Forms.ComboBox cbParity;
        private System.Windows.Forms.Panel panel1;
        private FontAwesome.Sharp.IconButton btnCloseForm;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Button btnClearSend;
        private System.Windows.Forms.Button btnClearReceive;
        private System.Windows.Forms.Panel panel2;
        private FontAwesome.Sharp.IconButton btnSlide;
        private System.Windows.Forms.RichTextBox txtInformacionUtil;
        private System.Windows.Forms.Label label4;
        private FontAwesome.Sharp.IconButton btnLoadInfo;
    }
}