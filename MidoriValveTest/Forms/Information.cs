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
    public partial class Information : Form
    {
        public Midori_PV interInfo;
        public Information()
        {
            InitializeComponent();
        }


        private void Information_Load(object sender, EventArgs e)
        {
            lbCountGenerateReports.Text = interInfo.ContadorReportes.ToString();
        }

        
    }
}
