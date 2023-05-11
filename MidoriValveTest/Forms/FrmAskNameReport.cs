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
        public FrmAskNameReport()
        {
            InitializeComponent();
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnCloseMKSConexion_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            inter.NombreReporte = "Name Unregistered";
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (Regex.IsMatch(txtNameReport.Text, @"^[a-zA-ZñÑáÁéÉíÍóÓúÚ\s]+$")) // Verificar si el nombre sólo contiene letras
            {
                string nombreUsuario = txtNameReport.Text;
                inter.NombreReporte = nombreUsuario;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Please enter a valid name");
            }
        }

        private void FrmAskNameReport_Load(object sender, EventArgs e)
        {
            this.ActiveControl = txtNameReport;

            Point textBoxLocation = txtNameReport.PointToScreen(new Point(0, 0));

            // Mover el cursor del mouse a las coordenadas del TextBox
            Cursor.Position = new Point(textBoxLocation.X + (txtNameReport.Width / 2), textBoxLocation.Y + (txtNameReport.Height / 2));
        }
    }
}
