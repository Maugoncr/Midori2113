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
    public partial class ChooseDWG : Form
    {
        public ChooseDWG()
        {
            InitializeComponent();
            InitializeSetting();
        }

        public void InitializeSetting()
        {
            this.FormBorderStyle = FormBorderStyle.None;

        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
