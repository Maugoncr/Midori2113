namespace MidoriValveTest
{
    partial class Report
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
            this.PanelMain = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.rtxtTexBoxForEdit = new System.Windows.Forms.RichTextBox();
            this.rtxtContenido = new System.Windows.Forms.RichTextBox();
            this.IconClear = new FontAwesome.Sharp.IconButton();
            this.IconLoad = new FontAwesome.Sharp.IconButton();
            this.IconSave = new FontAwesome.Sharp.IconButton();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.PanelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // PanelMain
            // 
            this.PanelMain.BackColor = System.Drawing.Color.DarkCyan;
            this.PanelMain.Controls.Add(this.label2);
            this.PanelMain.Controls.Add(this.label1);
            this.PanelMain.Controls.Add(this.rtxtTexBoxForEdit);
            this.PanelMain.Controls.Add(this.rtxtContenido);
            this.PanelMain.Controls.Add(this.IconClear);
            this.PanelMain.Controls.Add(this.IconLoad);
            this.PanelMain.Controls.Add(this.IconSave);
            this.PanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelMain.Location = new System.Drawing.Point(0, 0);
            this.PanelMain.Name = "PanelMain";
            this.PanelMain.Size = new System.Drawing.Size(778, 474);
            this.PanelMain.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(29, 233);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 16);
            this.label2.TabIndex = 8;
            this.label2.Text = "Text after Load";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(29, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 16);
            this.label1.TabIndex = 7;
            this.label1.Text = "Text for Save";
            // 
            // rtxtTexBoxForEdit
            // 
            this.rtxtTexBoxForEdit.BackColor = System.Drawing.Color.White;
            this.rtxtTexBoxForEdit.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtxtTexBoxForEdit.Cursor = System.Windows.Forms.Cursors.Default;
            this.rtxtTexBoxForEdit.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtxtTexBoxForEdit.Location = new System.Drawing.Point(32, 252);
            this.rtxtTexBoxForEdit.Name = "rtxtTexBoxForEdit";
            this.rtxtTexBoxForEdit.Size = new System.Drawing.Size(558, 197);
            this.rtxtTexBoxForEdit.TabIndex = 6;
            this.rtxtTexBoxForEdit.Text = "";
            // 
            // rtxtContenido
            // 
            this.rtxtContenido.BackColor = System.Drawing.Color.White;
            this.rtxtContenido.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtxtContenido.Cursor = System.Windows.Forms.Cursors.Default;
            this.rtxtContenido.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtxtContenido.Location = new System.Drawing.Point(32, 33);
            this.rtxtContenido.Name = "rtxtContenido";
            this.rtxtContenido.Size = new System.Drawing.Size(558, 197);
            this.rtxtContenido.TabIndex = 4;
            this.rtxtContenido.Text = "";
            // 
            // IconClear
            // 
            this.IconClear.BackColor = System.Drawing.Color.LightGray;
            this.IconClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IconClear.IconChar = FontAwesome.Sharp.IconChar.Eraser;
            this.IconClear.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.IconClear.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.IconClear.IconSize = 30;
            this.IconClear.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.IconClear.Location = new System.Drawing.Point(629, 276);
            this.IconClear.Name = "IconClear";
            this.IconClear.Size = new System.Drawing.Size(116, 38);
            this.IconClear.TabIndex = 3;
            this.IconClear.Text = "       Clear";
            this.IconClear.UseVisualStyleBackColor = false;
            this.IconClear.Click += new System.EventHandler(this.IconClear_Click);
            // 
            // IconLoad
            // 
            this.IconLoad.BackColor = System.Drawing.Color.LightGray;
            this.IconLoad.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.IconLoad.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IconLoad.IconChar = FontAwesome.Sharp.IconChar.Download;
            this.IconLoad.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.IconLoad.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.IconLoad.IconSize = 30;
            this.IconLoad.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.IconLoad.Location = new System.Drawing.Point(629, 180);
            this.IconLoad.Name = "IconLoad";
            this.IconLoad.Size = new System.Drawing.Size(116, 38);
            this.IconLoad.TabIndex = 2;
            this.IconLoad.Text = "       Load";
            this.IconLoad.UseVisualStyleBackColor = false;
            this.IconLoad.Click += new System.EventHandler(this.IconLoad_Click);
            // 
            // IconSave
            // 
            this.IconSave.BackColor = System.Drawing.Color.LightGray;
            this.IconSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.IconSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IconSave.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.IconSave.IconChar = FontAwesome.Sharp.IconChar.Save;
            this.IconSave.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.IconSave.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.IconSave.IconSize = 30;
            this.IconSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.IconSave.Location = new System.Drawing.Point(629, 83);
            this.IconSave.Name = "IconSave";
            this.IconSave.Size = new System.Drawing.Size(116, 38);
            this.IconSave.TabIndex = 1;
            this.IconSave.Text = "       Save";
            this.IconSave.UseVisualStyleBackColor = false;
            this.IconSave.Click += new System.EventHandler(this.IconSave_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // Report
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(778, 474);
            this.Controls.Add(this.PanelMain);
            this.Name = "Report";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Report";
            this.Load += new System.EventHandler(this.Report_Load);
            this.PanelMain.ResumeLayout(false);
            this.PanelMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel PanelMain;
        private FontAwesome.Sharp.IconButton IconLoad;
        private System.Windows.Forms.RichTextBox rtxtContenido;
        private FontAwesome.Sharp.IconButton IconClear;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.RichTextBox rtxtTexBoxForEdit;
        private FontAwesome.Sharp.IconButton IconSave;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}