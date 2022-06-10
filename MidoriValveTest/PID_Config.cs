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
    public partial class PID_Config : Form
    {
        public PID_Config()
        {
            InitializeComponent();
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void PID_Config_Load(object sender, EventArgs e)
        {

            Cb_ControlSelector.SelectedIndex = 0;

            GroupC2.Enabled = false;
            GroupC3.Enabled = false;
            GroupC4.Enabled = false;
            GroupS2.Enabled = false;
            GroupS3.Enabled = false;
            GroupS4.Enabled = false;
            GroupR2.Enabled = false;
            GroupR3.Enabled = false;
            GroupR4.Enabled = false;
            BtnBackGround2.Visible = false;
            BtnBackGround3.Visible = false;
            BtnBackGround4.Visible = false;
            NumD1.Enabled = false;
            NumD2.Enabled = false;
            NumD3.Enabled = false;
            NumD4.Enabled = false;


        }

        private void Cb_ControlSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Cb_ControlSelector.SelectedIndex == 0)
            {
                GroupC1.Enabled = true;
                GroupS1.Enabled = true;
                GroupR1.Enabled = true;
                GroupC2.Enabled = false;
                GroupC3.Enabled = false;
                GroupC4.Enabled = false;
                GroupS2.Enabled = false;
                GroupS3.Enabled = false;
                GroupS4.Enabled = false;
                GroupR2.Enabled = false;
                GroupR3.Enabled = false;
                GroupR4.Enabled = false;

                btnBackGround1.Visible = true;
                BtnBackGround2.Visible = false;
                BtnBackGround3.Visible = false;
                BtnBackGround4.Visible = false;

            }
            else if (Cb_ControlSelector.SelectedIndex == 1)
            {
                GroupC1.Enabled = false;
                GroupS1.Enabled = false;
                GroupR1.Enabled = false;
                GroupC2.Enabled = true;
                GroupC3.Enabled = false;
                GroupC4.Enabled = false;
                GroupS2.Enabled = true;
                GroupS3.Enabled = false;
                GroupS4.Enabled = false;
                GroupR2.Enabled = true;
                GroupR3.Enabled = false;
                GroupR4.Enabled = false;

                btnBackGround1.Visible = false;
                BtnBackGround2.Visible = true;
                BtnBackGround3.Visible = false;
                BtnBackGround4.Visible = false;

            }
            else if (Cb_ControlSelector.SelectedIndex == 2)
            {
                GroupC1.Enabled = false;
                GroupS1.Enabled = false;
                GroupR1.Enabled = false;
                GroupC2.Enabled = false;
                GroupC3.Enabled = true;
                GroupC4.Enabled = false;
                GroupS2.Enabled = false;
                GroupS3.Enabled = true;
                GroupS4.Enabled = false;
                GroupR2.Enabled = false;
                GroupR3.Enabled = true;
                GroupR4.Enabled = false;

                btnBackGround1.Visible = false;
                BtnBackGround2.Visible = false;
                BtnBackGround3.Visible = true;
                BtnBackGround4.Visible = false;


            }
            else if (Cb_ControlSelector.SelectedIndex == 3)
            {
                GroupC1.Enabled = false;
                GroupS1.Enabled = false;
                GroupR1.Enabled = false;
                GroupC2.Enabled = false;
                GroupC3.Enabled = false;
                GroupC4.Enabled = true;
                GroupS2.Enabled = false;
                GroupS3.Enabled = false;
                GroupS4.Enabled = true;
                GroupR2.Enabled = false;
                GroupR3.Enabled = false;
                GroupR4.Enabled = true;

                btnBackGround1.Visible = false;
                BtnBackGround2.Visible = false;
                BtnBackGround3.Visible = false;
                BtnBackGround4.Visible = true;
                


            }
        }


        private void CbAlgo1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (CbAlgo1.SelectedIndex == 1)
            {
                NumD1.Enabled = true;
            }
            else {
                NumD1.Enabled = false;
            }
        }

        private void CbAlgo2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CbAlgo2.SelectedIndex == 1)
            {
                NumD2.Enabled = true;
            }
            else
            {
                NumD2.Enabled = false;
            }
        }

        private void CbAlgo3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CbAlgo3.SelectedIndex == 1)
            {
                NumD3.Enabled = true;
            }
            else
            {
                NumD3.Enabled = false;
            }
        }

        private void CbAlgo4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CbAlgo4.SelectedIndex == 1)
            {
                NumD4.Enabled = true;
            }
            else
            {
                NumD4.Enabled = false;
            }
        }



    }
}
