
namespace MidoriValveTest
{
    partial class Terminal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Terminal));
            this.txt_command = new System.Windows.Forms.TextBox();
            this.RTXT_history = new System.Windows.Forms.RichTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txt_response = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblPuerto = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.com_led = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.com_led)).BeginInit();
            this.SuspendLayout();
            // 
            // txt_command
            // 
            this.txt_command.Location = new System.Drawing.Point(6, 49);
            this.txt_command.Name = "txt_command";
            this.txt_command.Size = new System.Drawing.Size(764, 25);
            this.txt_command.TabIndex = 2;
            this.txt_command.Enter += new System.EventHandler(this.textBox1_Enter);
            this.txt_command.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_command_KeyPress);
            // 
            // RTXT_history
            // 
            this.RTXT_history.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RTXT_history.Location = new System.Drawing.Point(6, 19);
            this.RTXT_history.Name = "RTXT_history";
            this.RTXT_history.ReadOnly = true;
            this.RTXT_history.Size = new System.Drawing.Size(764, 229);
            this.RTXT_history.TabIndex = 0;
            this.RTXT_history.TabStop = false;
            this.RTXT_history.Text = "";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txt_command);
            this.groupBox1.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.groupBox1.Location = new System.Drawing.Point(12, 74);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(776, 80);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "COMMAND";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(594, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(176, 18);
            this.label1.TabIndex = 1;
            this.label1.Text = "Press \'Enter\' to send";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txt_response);
            this.groupBox2.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.groupBox2.Location = new System.Drawing.Point(12, 160);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(776, 53);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "RESPONSE";
            // 
            // txt_response
            // 
            this.txt_response.Location = new System.Drawing.Point(6, 19);
            this.txt_response.Name = "txt_response";
            this.txt_response.ReadOnly = true;
            this.txt_response.Size = new System.Drawing.Size(764, 25);
            this.txt_response.TabIndex = 1;
            this.txt_response.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.RTXT_history);
            this.groupBox3.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.groupBox3.Location = new System.Drawing.Point(12, 219);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(776, 265);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "TRANSMISSION HISTORY";
            // 
            // lblPuerto
            // 
            this.lblPuerto.AutoSize = true;
            this.lblPuerto.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPuerto.ForeColor = System.Drawing.Color.Red;
            this.lblPuerto.Location = new System.Drawing.Point(135, 22);
            this.lblPuerto.Name = "lblPuerto";
            this.lblPuerto.Size = new System.Drawing.Size(131, 20);
            this.lblPuerto.TabIndex = 31;
            this.lblPuerto.Text = "Disconnected *";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.DarkCyan;
            this.label6.Location = new System.Drawing.Point(70, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 20);
            this.label6.TabIndex = 30;
            this.label6.Text = "Port:";
            // 
            // com_led
            // 
            this.com_led.Image = global::MidoriValveTest.Properties.Resources.led_on;
            this.com_led.Location = new System.Drawing.Point(747, 33);
            this.com_led.Name = "com_led";
            this.com_led.Size = new System.Drawing.Size(35, 35);
            this.com_led.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.com_led.TabIndex = 32;
            this.com_led.TabStop = false;
            // 
            // Terminal
            // 
            this.AllowDrop = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(800, 497);
            this.Controls.Add(this.lblPuerto);
            this.Controls.Add(this.com_led);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(816, 536);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(816, 536);
            this.Name = "Terminal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Terminal";
            this.Load += new System.EventHandler(this.Terminal_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.com_led)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_command;
        private System.Windows.Forms.RichTextBox RTXT_history;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txt_response;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox com_led;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.Label lblPuerto;
    }
}