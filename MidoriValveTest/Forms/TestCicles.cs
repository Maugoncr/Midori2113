using CustomMessageBox;
using MidoriValveTest.Forms;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace MidoriValveTest
{
    public partial class TestCicles : Form
    {
       public System.IO.Ports.SerialPort Arduino;
       public Midori_PV menssager;

        [DllImport("user32.dll", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        public TestCicles()
        {
            InitializeComponent();
        }


        private void TestCicles_Load(object sender, EventArgs e)
        {
            MostrarOcultarSetpointPhase2(true);
            cbSelectTypeCycle.SelectedIndex = -1;
            btnTestStart.Enabled = false;
            NumOfCycles.Enabled = false;
        }

        private void cbSelectTypeCycle_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cbSelectTypeCycle.SelectedIndex >= 0)
            {
                NumOfCycles.Enabled = true;
            }
            //[1] PRETEST CALIBRATION
            if (cbSelectTypeCycle.SelectedIndex == 0)
            {
                menssager.TestToRun = 1;
                NumOfCycles.Maximum = 1;
                MostrarOcultarSetpointPhase2(true);
            }
            //[2] STABILITY TEST TO SETPOINT
            else if (cbSelectTypeCycle.SelectedIndex == 1)
            {
                DialogResult result = MessageBoxMaugoncr.Show("𝗣𝗹𝗲𝗮𝘀𝗲 𝗻𝗼𝘁𝗲 𝘁𝗵𝗮𝘁 𝘁𝗵𝗲 𝘀𝗲𝘁𝗽𝗼𝗶𝗻𝘁 𝗺𝘂𝘀𝘁 𝗯𝗲 𝗺𝗮𝗻𝘂𝗮𝗹𝗹𝘆 𝘀𝗲𝗻𝘁 𝗽𝗿𝗶𝗼𝗿 𝘁𝗼 𝘀𝘁𝗮𝗿𝘁𝗶𝗻𝗴 𝘁𝗵𝗶𝘀 𝗽𝗵𝗮𝘀𝗲\n\nIt is crucial to send the setpoint correctly to avoid issues later on. Once the setpoint is sent, it requires a reset on the breadboard each time its value is changed in order to function properly.\n\n\"Failure to do so may result in errors or malfunctions\"",
                    "𝗜𝗠𝗣𝗢𝗥𝗧𝗔𝗡𝗧 𝗙𝗢𝗥 𝗣𝗛𝗔𝗦𝗘 𝟮 𝗦𝗧𝗔𝗕𝗜𝗟𝗜𝗧𝗬 𝗧𝗘𝗦𝗧 𝗧𝗢 𝗦𝗘𝗧𝗣𝗢𝗜𝗡𝗧", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

                if (result == DialogResult.OK)
                {
                    menssager.TestToRun = 2;
                    NumOfCycles.Maximum = 1000000;
                    MostrarOcultarSetpointPhase2(false);

                }
                else if (result == DialogResult.Cancel)
                {
                    cbSelectTypeCycle.SelectedIndex = -1;
                    NumOfCycles.Enabled = false;
                    MostrarOcultarSetpointPhase2(true);
                }

                
            }
            // [3] VALVE LEAK TEST
            else if (cbSelectTypeCycle.SelectedIndex == 2)
            {
                menssager.TestToRun = 3;
                NumOfCycles.Maximum = 1000000;
                MostrarOcultarSetpointPhase2(true);
            }
        }

        private void MostrarOcultarSetpointPhase2(bool OcultarMostrar)
        {
            if (OcultarMostrar)
            {
                lbNumOfClycles.Location = new Point(193, 136);
                NumOfCycles.Location = new Point(171, 159);
                btnSetPoint.Visible = false;
                txtSetPoint.Visible = false;
                lbSentSetpoint.Visible = false;
                btnSetPoint.Enabled = false;
                txtSetPoint.Clear();
            }
            else
            {
                lbNumOfClycles.Location = new Point(94, 136);
                NumOfCycles.Location = new Point(72, 159);
                btnSetPoint.Visible = true;
                txtSetPoint.Visible = true;
                lbSentSetpoint.Visible = true;
                btnSetPoint.Enabled = false;
                txtSetPoint.Clear();
            }
        
        }


        private void NumOfCycles_ValueChanged(object sender, EventArgs e)
        {
            if (NumOfCycles.Value > 0)
            {
                btnTestStart.Enabled = true;
                menssager.NumTest = (int)NumOfCycles.Value;
            }
            else if (NumOfCycles.Value <= 0)
            {
                btnTestStart.Enabled = false;
                menssager.NumTest = 0;
            }
        }

        private void NumOfCycles_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 32 && e.KeyChar <= 47) || (e.KeyChar >= 58 && e.KeyChar <= 255))
            {
                MessageBoxMaugoncr.Show("Only numbers are allowed", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        public void Alert(string msg, Form_Alert.enmType type)
        {
            Form_Alert frm = new Form_Alert();
            frm.showAlert(msg, type);
        }

        private void btnTestStart_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<FrmDontTouch>().Any())
            {
                
            }
            else
            {
                FrmDontTouch miFormulario = new FrmDontTouch();
                miFormulario.Show();
            }
            menssager.timerTemporizador.Start();
            menssager.NewThreadForTest();
            this.Close();
        }

        private void txtSetPoint_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true; // Ignorar la tecla presionada
            }
        }

        private void txtSetPoint_TextChanged(object sender, EventArgs e)
        {
            if (txtSetPoint.Text != String.Empty)
            {
                string text = txtSetPoint.Text;

                // Verificar si el valor es un número válido
                if (int.TryParse(text, out int value))
                {
                    // Limitar el valor al rango de 0 a 760
                    value = Math.Min(760, Math.Max(0, value));
                    // Actualizar el valor del TextBox
                    txtSetPoint.Text = value.ToString();
                    txtSetPoint.SelectionStart = text.Length; // Colocar el cursor al final del texto
                    btnSetPoint.Enabled = true;
                }
                else
                {
                    MessageBoxMaugoncr.Show("𝗧𝗵𝗲 𝘀𝗲𝘁𝗽𝗼𝗶𝗻𝘁 𝗿𝗮𝗻𝗴𝗲 𝗺𝘂𝘀𝘁 𝗯𝗲 𝗯𝗲𝘁𝘄𝗲𝗲𝗻 𝟬 𝘁𝗼 𝟳𝟲𝟬 𝗧𝗼𝗿𝗿",
                        "𝗘𝗥𝗥𝗢𝗥", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtSetPoint.Clear();
                    txtSetPoint.Focus();
                    btnSetPoint.Enabled = false;
                }
            }
            else
            {
                btnSetPoint.Enabled = false;
            }
            
        }

        private void btnSetPoint_Click(object sender, EventArgs e)
        {
            menssager.EnviarSetpointFromTestCycles(txtSetPoint.Text.ToString().Trim());
            btnSetPoint.Enabled = false;
            txtSetPoint.Clear();
            this.Alert("Remember to reset the BB", Form_Alert.enmType.Warning);
        }

        private void IconClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
    }
}
