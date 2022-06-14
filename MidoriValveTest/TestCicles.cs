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
        public static int counter;
        public static int limit;
        public static bool greenlight = false;


        public TestCicles()
        {
            InitializeComponent();
           

        }

        private void TestCicles_Load(object sender, EventArgs e)
        {
        
    }



private void timer1_Tick(object sender, EventArgs e)
        {
            if (counter < limit)
            {
                counter++;
                txt_cycles.Text = counter.ToString();

                //TODO: verificar con los sensores si todo esta correcto, se mantenga en verde, amarillo, rojo

            }
        }

        // Cycles Reset
        private void button1_Click(object sender, EventArgs e)
        {
            counter = 0;
            greenlight = false;

        }

        private void btnTestStart_Click(object sender, EventArgs e)
        {
            limit = (int)NumOfCycles.Value;
            timer1.Interval = 2500;
            timer1.Start();
            greenlight = true;
           
          

            
            //com_led.Image.Dispose();
            //com_led.Image = MidoriValveTest.Properties.Resources.led_on;

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }


    
}
