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
    public partial class Drawings : Form
    {
        public Drawings()
        {
            InitializeComponent();
            InitializeSetting();
        }

        public void InitializeSetting()
        {
            this.FormBorderStyle = FormBorderStyle.None;

        }

        private void IconRight_Click(object sender, EventArgs e)
        {

            if (ChooseDWG.selectorIMGdraws == 0)
            {
                ChooseDWG.selectorIMGdraws = 1;
                imgDraws.Image.Dispose();
                imgDraws.Image = MidoriValveTest.Properties.Resources.E401;
                IconLeft.Enabled = true;
            }
            else if (ChooseDWG.selectorIMGdraws == 1)
            {
                ChooseDWG.selectorIMGdraws = 2;
                imgDraws.Image.Dispose();
                imgDraws.Image = MidoriValveTest.Properties.Resources.E402;
                IconRight.Enabled = false;
            }
           

        }

        private void IconClose_Click(object sender, EventArgs e)
        {
            this.Close();
            ChooseDWG.selectorIMGdraws = 0;
        }

        private void IconLeft_Click(object sender, EventArgs e)
        {
            if (ChooseDWG.selectorIMGdraws == 1)
            {
                ChooseDWG.selectorIMGdraws = 0;
                imgDraws.Image.Dispose();
                imgDraws.Image = MidoriValveTest.Properties.Resources.A40;
                IconLeft.Enabled = false;

            }
            else if (ChooseDWG.selectorIMGdraws == 2)
            {
                ChooseDWG.selectorIMGdraws = 1;
                imgDraws.Image.Dispose();
                imgDraws.Image = MidoriValveTest.Properties.Resources.E401;
                IconRight.Enabled = true;
            }


        }

        private void Drawings_Load(object sender, EventArgs e)
        {
            ChooseDWG.selectorIMGdraws = 0;
            imgDraws.Image = MidoriValveTest.Properties.Resources.A40;
            IconLeft.Enabled = false;

        }
    }
}
