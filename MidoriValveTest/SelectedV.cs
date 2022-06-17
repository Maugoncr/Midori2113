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
    public partial class SelectedV : Form
    {

        public static int Reference = 0;

        public SelectedV()
        {
            InitializeComponent();
            InitializeSetting();
        }

        public void InitializeSetting()
        {
            this.FormBorderStyle = FormBorderStyle.None;

        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Reference = 40;



        }


    }
}
