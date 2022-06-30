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
        public static int selectorIMGdraws = 0;

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

        private void BtnKF40_Click(object sender, EventArgs e)
        {
            selectorIMGdraws = 0;

            Form frm = Application.OpenForms.Cast<Form>().FirstOrDefault(x => x is Drawings);

            if (frm == null)
            {
                Drawings nt = new Drawings();
                nt.ShowDialog();

            }
            else
            {
                frm.BringToFront();
                return;
            }
        }

        private void ChooseDWG_Load(object sender, EventArgs e)
        {

        }
    }
}
