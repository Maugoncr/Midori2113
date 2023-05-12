﻿using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using CustomMessageBox;
using MidoriValveTest.Properties;

namespace MidoriValveTest.Forms
{
    public partial class FrmModifiedSettings : Form
    {
        [DllImport("user32.dll", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        public FrmModifiedSettings()
        {
            InitializeComponent();
        }

        private string secuencia = "MIDRCR";
        private string entrada = "";
        private int indice = 0;


        private void IconClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar))
            {
                if (e.KeyChar == secuencia[indice])
                {
                    entrada += e.KeyChar;
                    indice++;
                    if (indice == secuencia.Length)
                    {
                        btnLock.IconChar = FontAwesome.Sharp.IconChar.LockOpen;
                        txtNumberProject.Enabled = true;
                        txtClient.Enabled = true;
                        btnReset.Visible = true;
                        btnSave.Enabled = true;
                        entrada = "";
                        indice = 0;
                    }
                }
                else
                {
                    entrada = "";
                    indice = 0;
                }
            }
        }

        private void RestablecerConfig()
        { 
            Settings.Default.Reset();
            CargarFormulario();
            MessageBoxMaugoncr.Show("             Reset Complete","Information",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void CargarFormulario()
        {
            entrada = "";
            indice = 0;
            btnLock.IconChar = FontAwesome.Sharp.IconChar.Lock;
            txtClient.Text = Settings.Default.Client;
            txtNumberProject.Text = Settings.Default.CodeProject;
            txtPassword.Visible = false;
            txtClient.Enabled = false;
            txtNumberProject.Enabled = false;
            txtPassword.Clear();
            btnSave.Enabled = false;
            btnLock.Visible = false;
            btnReset.Visible = false;

        }

        private void FrmModifiedSettings_Load(object sender, EventArgs e)
        {
            CargarFormulario();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            RestablecerConfig();
        }

        private void btnLock_Click(object sender, EventArgs e)
        {
            txtPassword.Visible = true;
        }

        private void FrmModifiedSettings_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.L && e.Control && e.Shift)
            {
                btnLock.Visible = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtClient.Text) && !string.IsNullOrEmpty(txtNumberProject.Text))
            {
                Settings.Default.Client = txtClient.Text;
                Settings.Default.CodeProject = txtNumberProject.Text;
                Settings.Default.Save();
                CargarFormulario();
            }
            else
            {
                MessageBoxMaugoncr.Show("          Incomplete Information", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
    }
}
