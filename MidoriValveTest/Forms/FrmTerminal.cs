using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Services.Description;
using System.Windows.Forms;

namespace MidoriValveTest
{
    public partial class FrmTerminal : Form
    {
        [DllImport("user32.dll", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        public FrmTerminal()
        {
            InitializeComponent();
        }

        private void CargarInfoUtil()
        {
            string info = 
                "Command To Calibrate MKS\n" +
                "@254ATM!7.19E+2;FF\n" +
                "7.19E+2 Variable from 5.00E+2 to 7.80E+2"
                ;
            txtInformacionUtil.Text = info;
        }

        private void FrmTerminal_Load(object sender, EventArgs e)
        {
            CargarInfoUtil();
            try
            {
                string[] puertos = SerialPort.GetPortNames();
                cboxPort.Items.AddRange(puertos);
                cboxPort.SelectedIndex = 0;
                cbBaudRate.SelectedIndex = 0;
                cbParity.SelectedIndex = 0;
                btnClose.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            btnOpen.Enabled = false;
            btnClose.Enabled = true;
            try
            {
                if (serialPort1.IsOpen)
                {
                    serialPort1.Close();
                    btnOpen.Enabled = true;
                    btnClose.Enabled = false;
                    return;
                }
                serialPort1.PortName = cboxPort.Text;
                serialPort1.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            btnOpen.Enabled = true;
            btnClose.Enabled = false;
            try
            {
                serialPort1.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                if (serialPort1.IsOpen)
                {
                    serialPort1.WriteLine(txtSend.Text.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnReceive_Click(object sender, EventArgs e)
        {
            try
            {
                if (serialPort1.IsOpen)
                {
                    txtReceive.Text = serialPort1.ReadExisting();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                Thread.Sleep(1000);
                string DataIn = serialPort1.ReadExisting();
                if (DataIn != null && DataIn != String.Empty)
                {
                    ReadData(DataIn);
                    serialPort1.DiscardInBuffer();
                }
            }
            catch (Exception)
            {

            }
        }

        private void ReadData(string input)
        { 
            txtReceive.Text += input;
        }

        private void cbParity_SelectedIndexChanged(object sender, EventArgs e)
        {
            //None
            //Odd
            //Even
            //Mark
            //Space
            switch (cbParity.SelectedIndex)
            {
                case 0:
                    serialPort1.Parity = Parity.None;
                    break;
                case 1:
                    serialPort1.Parity = Parity.Odd;
                    break;
                case 2:
                    serialPort1.Parity = Parity.Even;
                    break;
                case 3:
                    serialPort1.Parity = Parity.Mark;
                    break;
                case 4:
                    serialPort1.Parity = Parity.Space;
                    break;
            }
        }

        private void cbBaudRate_SelectedIndexChanged(object sender, EventArgs e)
        {
                //9600
                //19200
                //38400
                //57600
                //115200
            switch (cbBaudRate.SelectedIndex)
            {
                case 0:
                    serialPort1.BaudRate = 9600;
                    break; 
                case 1:
                    serialPort1.BaudRate = 19200;
                    break; 
                case 2:
                    serialPort1.BaudRate = 38400;
                    break;
                case 3:
                    serialPort1.BaudRate = 57600;
                    break;
                case 4:
                    serialPort1.BaudRate = 115200;
                    break;
            }
        }

        private void btnCloseForm_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                serialPort1.Close();
            }
            this.Close();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnClearSend_Click(object sender, EventArgs e)
        {
            txtSend.Clear();
            txtSend.Focus();
        }

        private void btnClearReceive_Click(object sender, EventArgs e)
        {
            txtReceive.Clear();
            txtReceive.Focus();
        }

        private void btnLoadInfo_Click(object sender, EventArgs e)
        {
            CargarInfoUtil();
        }

        private void btnSlide_Click(object sender, EventArgs e)
        {
            if (btnSlide.IconChar == FontAwesome.Sharp.IconChar.CircleChevronRight)
            {
                this.Size = new Size(690, 536);
                btnSlide.IconChar = FontAwesome.Sharp.IconChar.CircleChevronLeft;
            }
            else
            {
                this.Size = new Size(428, 536);
                btnSlide.IconChar = FontAwesome.Sharp.IconChar.CircleChevronRight;
            }
        }

        private void txtReceive_TextChanged(object sender, EventArgs e)
        {
            txtReceive.SelectionStart = txtReceive.Text.Length;
            txtReceive.SelectionLength = 0;
            txtReceive.ScrollToCaret();
        }
    }
}
