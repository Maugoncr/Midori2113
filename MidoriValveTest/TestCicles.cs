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
    public partial class TestCicles : Form
    {
       public System.IO.Ports.SerialPort Arduino;
        private int counter;
        public TestCicles()
        {
            InitializeComponent();
        }

        private void TestCicles_Load(object sender, EventArgs e)
        {
        
    }



private void timer1_Tick(object sender, EventArgs e)
        {
            counter++;
            txt_cycles.Text = counter.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            counter = 0;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //if(txt_cycles.Text)
            timer1.Interval = 2500;
            timer1.Start();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }


    
}
