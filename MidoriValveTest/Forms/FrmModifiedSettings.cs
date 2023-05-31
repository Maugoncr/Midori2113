using System;
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
                        txtOperator.Enabled = true;
                        txtPersonOfContact.Enabled = true;
                        txtPurchaseOrder.Enabled = true;

                        btnSelectedPath.Visible = true;
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

            txtClient.Text = Settings.Default.Customer;
            txtNumberProject.Text = Settings.Default.CodeProject;
            txtPersonOfContact.Text = Settings.Default.PersonOfContact;
            txtPurchaseOrder.Text = Settings.Default.PurchaseOrder;
            txtOperator.Text = Settings.Default.Operator;

            if (Settings.Default.PathSaveRecords == "Environment.SpecialFolder.Desktop")
            {
                string rutaEscritorio = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                txtSavePath.Text = rutaEscritorio;
            }
            else
            {
                txtSavePath.Text = Settings.Default.PathSaveRecords;
            }

            txtPassword.Visible = false;

            txtClient.Enabled = false;
            txtNumberProject.Enabled = false;
            txtPersonOfContact.Enabled = false;
            txtPurchaseOrder.Enabled = false;
            txtOperator.Enabled = false;
            txtSavePath.Enabled = false;

            txtPassword.Clear();
            btnSave.Enabled = false;
            btnLock.Visible = false;
            btnReset.Visible = false;
            btnSelectedPath.Visible = false;

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
            if (!string.IsNullOrEmpty(txtClient.Text) && !string.IsNullOrEmpty(txtNumberProject.Text) && !string.IsNullOrEmpty(txtPersonOfContact.Text)
                && !string.IsNullOrEmpty(txtPurchaseOrder.Text) && !string.IsNullOrEmpty(txtOperator.Text))
            {
                Settings.Default.Customer = txtClient.Text;
                Settings.Default.CodeProject = txtNumberProject.Text;
                Settings.Default.PersonOfContact = txtPersonOfContact.Text;
                Settings.Default.PurchaseOrder = txtPurchaseOrder.Text;
                Settings.Default.Operator = txtOperator.Text;
                Settings.Default.PathSaveRecords = txtSavePath.Text;

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

        private void btnSelectedPath_Click(object sender, EventArgs e)
        {
            string rutainicial = txtSavePath.Text;

            using (var folderDialog = new FolderBrowserDialog())
            {
                // Establecer la ruta inicial del explorador de archivos
                if (!string.IsNullOrWhiteSpace(rutainicial))
                {
                    folderDialog.SelectedPath = rutainicial;
                }

                DialogResult result = folderDialog.ShowDialog();
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderDialog.SelectedPath))
                {
                    txtSavePath.Text = folderDialog.SelectedPath;
                }
            }
        }
    }
}
