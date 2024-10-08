﻿using CustomMessageBox;
using MidoriValveTest.Forms;
using MidoriValveTest.Properties;
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
            lbOperator.Text = Settings.Default.Operator;

            if (OcultarMostrar)
            {
                this.Size = new Size(500, 333);
                this.StartPosition = FormStartPosition.CenterScreen;
                lbNumOfClycles.Location = new Point(193, 182);
                NumOfCycles.Location = new Point(171, 205);
                btnSetPoint.Visible = false;
                txtSetPoint.Visible = false;
                lbSentSetpoint.Visible = false;
                btnSetPoint.Enabled = false;
                txtSetPoint.Clear();
            }
            else
            {
                //this.Size = new Size(901, 333);
                lbNumOfClycles.Location = new Point(94, 183);
                NumOfCycles.Location = new Point(72, 206);
                //btnSetPoint.Visible = true;
                txtSetPoint.Visible = true;
                lbSentSetpoint.Visible = true;
                btnSetPoint.Enabled = false;
                txtSetPoint.Clear();
                checkFunctionCycleCompare.Checked = false;
                checkFunctionCycleCompare.Enabled = false;
                txtCycle1.Enabled = false;
                txtCycle2.Enabled = false;
                txtCycle3.Enabled = false;
                txtCycle4.Enabled = false;
                txtCycle5.Enabled = false;
                txtCycle6.Enabled = false;
                txtCycle7.Enabled = false;
                txtCycle8.Enabled = false;
                txtCycle9.Enabled = false;
                txtCycle10.Enabled = false;


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
                MessageBoxMaugoncr.Show("Only numbers are allowed", "Important", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
            if (cbSelectTypeCycle.SelectedIndex == 1)
            {
                if (NumOfCycles.Value >= 1 && !string.IsNullOrEmpty(txtSetPoint.Text) && Convert.ToInt32(txtSetPoint.Text) >= 1)
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
                else
                {
                    MessageBoxMaugoncr.Show("You must enter valid cycle quantities and setpoint", "Important", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                if (NumOfCycles.Value >= 1)
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
                else
                {
                    MessageBoxMaugoncr.Show("You must enter valid cycle quantities", "Important", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            
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
                    menssager.SetpointPhase2 = Convert.ToInt32(txtSetPoint.Text);
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

        private void btnOperator_Click(object sender, EventArgs e)
        {
            int valor = 1;

            FrmAskNameReport frm = new FrmAskNameReport(valor);
            DialogResult resultado = frm.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                lbOperator.Text = Settings.Default.Operator;
            }
        }
    }
}
