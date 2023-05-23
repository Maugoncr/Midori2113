using CustomMessageBox;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MidoriValveTest.Forms
{
    public partial class FrmAskNameReport : Form
    {

        [DllImport("user32.dll", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        public Midori_PV inter;
        private int parametro;

        public FrmAskNameReport(int valor = 0)
        {
            InitializeComponent();
            parametro = valor;
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }


        private void btnOk_Click(object sender, EventArgs e)
        {


            if (!string.IsNullOrEmpty(txtNameReport.Text.Trim()))
            {
                if (Regex.IsMatch(txtNameReport.Text, @"^[a-zA-ZñÑáÁéÉíÍóÓúÚ\s-]+$")) // Verificar si el nombre sólo contiene letras
                {
                    Properties.Settings.Default.Operator = txtNameReport.Text;
                    Properties.Settings.Default.Save();

                    if (parametro == 1)
                    {
                        MessageBoxMaugoncr.Show("Thank you, your name was successfully changed", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBoxMaugoncr.Show("Thank you, your name was successfully registered", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBoxMaugoncr.Show("Please enter a valid name", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBoxMaugoncr.Show("Please enter your name", "Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
            
        }

        private void FrmAskNameReport_Load(object sender, EventArgs e)
        {
            if (parametro == 1)
            {
                label1.Text = "Here, you can easily modify your name";
                label1.Location = new Point(133, 47);
                btnOk.Text = "Confirm";
                btnOk.Location = new Point(250, 172);
                btnOk.Size = new Size(107, 32);

                txtNameReport.Text = Properties.Settings.Default.Operator;

            }
            else
            {
                this.ActiveControl = txtNameReport;
                Point textBoxLocation = txtNameReport.PointToScreen(new Point(0, 0));
                Cursor.Position = new Point(textBoxLocation.X + (txtNameReport.Width / 2), textBoxLocation.Y + (txtNameReport.Height / 2));
            }
        }

     
    }
}
