using MidoriValveTest.Forms;
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

        private void btnConfigSettings_Click(object sender, EventArgs e)
        {
            Form frm = Application.OpenForms.Cast<Form>().FirstOrDefault(x => x is FrmModifiedSettings);
            if (frm == null)
            {
                FrmModifiedSettings nt = new FrmModifiedSettings();
                nt.ShowDialog();
            }
            else
            {
                frm.BringToFront();
                return;
            }
        }
    }
}
