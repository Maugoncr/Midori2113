using CustomMessageBox;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MidoriValveTest
{
    public partial class PID_Config : Form
    {
        public PID_Config()
        {
            InitializeComponent();
        }


        private void PID_Config_Load(object sender, EventArgs e)
        {
            Comprabar();

        }


        private void checkPID_CheckedChanged(object sender, EventArgs e)
        {
            if (checkPID.Checked)
            {
                ObjetosGlobales.flagPID = true;

            }
            else
            {
                ObjetosGlobales.flagPID = false;
                txtP.Clear();
                txtI.Clear();
                txtD.Clear();
                ObjetosGlobales.P = "x";
                ObjetosGlobales.I = "x";
                ObjetosGlobales.D = "x";

            }
        }
        private void Comprabar()
        {
            if (ObjetosGlobales.flagPID)
            {
                txtP.Text = ObjetosGlobales.P;
                txtI.Text = ObjetosGlobales.I;
                txtD.Text = ObjetosGlobales.D;
                checkPID.Checked = true;
            }

        }
        private void txtP_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 32 && e.KeyChar <= 45) || (e.KeyChar >= 58 && e.KeyChar <= 255) || e.KeyChar == 47)
            {
                MessageBoxMaugoncr.Show("Solo se pueden ingresar números", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void txtI_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 32 && e.KeyChar <= 45) || (e.KeyChar >= 58 && e.KeyChar <= 255) || e.KeyChar == 47)
            {
                MessageBoxMaugoncr.Show("Solo se pueden ingresar números", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void txtD_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 32 && e.KeyChar <= 45) || (e.KeyChar >= 58 && e.KeyChar <= 255) || e.KeyChar == 47)
            {
                MessageBoxMaugoncr.Show("Only numbers are allowed", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void txtP_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtP.Text.Trim()))
            {
                ObjetosGlobales.P = txtP.Text.Trim();
            }
            else
            {
                ObjetosGlobales.P = "x";
            }
        }

        private void txtI_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtI.Text.Trim()))
            {
                ObjetosGlobales.I = txtI.Text.Trim();
            }
            else
            {
                ObjetosGlobales.I = "x";
            }
        }

        private void txtD_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtD.Text.Trim()))
            {
                ObjetosGlobales.D = txtD.Text.Trim();
            }
            else
            {
                ObjetosGlobales.D = "x";
            }
        }

        private void checkPID_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtP.Text.Trim()) || string.IsNullOrEmpty(txtI.Text.Trim())
                || string.IsNullOrEmpty(txtD.Text.Trim()))
            {
                checkPID.Checked = false;
                MessageBoxMaugoncr.Show("Set the PID first", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void PID_Config_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void IconClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSentPID_Click(object sender, EventArgs e)
        {
            if (checkPID.Checked)
            {
                if (!string.IsNullOrEmpty(txtP.Text.Trim()) && !string.IsNullOrEmpty(txtI.Text.Trim()) && !string.IsNullOrEmpty(txtD.Text.Trim()))
                {
                    Midori_PV.EnviarPID = true;
                }
                else
                {
                    MessageBoxMaugoncr.Show("PID aren't complete", "!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                MessageBoxMaugoncr.Show("PID Disable", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
