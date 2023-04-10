using CustomMessageBox;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;


namespace MidoriValveTest
{
    public partial class TestCicles : Form
    {
       public System.IO.Ports.SerialPort Arduino;
       public Midori_PV menssager;
       

        public TestCicles()
        {
            InitializeComponent();
        }


        private void TestCicles_Load(object sender, EventArgs e)
        {
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
            }
            //[2] STABILITY TESTS 500 - 300 - 150 TORR
            else if (cbSelectTypeCycle.SelectedIndex == 1)
            {
                menssager.TestToRun = 2;
            }
            // [3] VALVE LEAK TEST
            else if (cbSelectTypeCycle.SelectedIndex == 2)
            {
                menssager.TestToRun = 3;
            }


        }

        private void NumOfCycles_ValueChanged(object sender, EventArgs e)
        {
            if (NumOfCycles.Value > 0)
            {
                btnTestStart.Enabled = true;
            }
            else if (NumOfCycles.Value <= 0)
            {
                btnTestStart.Enabled = false;
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

        private void btnTestStart_Click(object sender, EventArgs e)
        {
            menssager.NewThreadForTest();
        }
    }
}
