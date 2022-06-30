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
    public partial class Pressure_Sensor : Form
    {
        public Pressure_Sensor()
        {
            InitializeComponent();
        }

        private void CbDataUnit1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CbDataUnit1.SelectedIndex == 0)
            {
                UpperData1.Text = "Upper Limit Data Value [Pa]";
                LowerData1.Text = "Lower Limit Data Value [Pa]";
            }
            else if (CbDataUnit1.SelectedIndex == 1)
            {
                UpperData1.Text = "Upper Limit Data Value [kPa]";
                LowerData1.Text = "Lower Limit Data Value [kPa]";
            }
            else if (CbDataUnit1.SelectedIndex == 2)
            {
                UpperData1.Text = "Upper Limit Data Value [bar]";
                LowerData1.Text = "Lower Limit Data Value [bar]";
            }
            else if (CbDataUnit1.SelectedIndex == 3)
            {
                UpperData1.Text = "Upper Limit Data Value [mbar]";
                LowerData1.Text = "Lower Limit Data Value [mbar]";
            }
            else if (CbDataUnit1.SelectedIndex == 4)
            {
                UpperData1.Text = "Upper Limit Data Value [Torr]";
                LowerData1.Text = "Lower Limit Data Value [Torr]";
            }
            else if (CbDataUnit1.SelectedIndex == 5)
            {
                UpperData1.Text = "Upper Limit Data Value [mTorr]";
                LowerData1.Text = "Lower Limit Data Value [mTorr]";
            }
            else if (CbDataUnit1.SelectedIndex == 6)
            {
                UpperData1.Text = "Upper Limit Data Value [psia]";
                LowerData1.Text = "Lower Limit Data Value [psia]";
            }
            else if (CbDataUnit1.SelectedIndex == 7)
            {
                UpperData1.Text = "Upper Limit Data Value [psig]";
                LowerData1.Text = "Lower Limit Data Value [psig]";
            }
    


        }

        private void CbDataUnit2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CbDataUnit2.SelectedIndex == 0)
            {
                UpperData2.Text = "Upper Limit Data Value [Pa]";
                LowerData2.Text = "Lower Limit Data Value [Pa]";
            }
            else if (CbDataUnit2.SelectedIndex == 1)
            {
                UpperData2.Text = "Upper Limit Data Value [kPa]";
                LowerData2.Text = "Lower Limit Data Value [kPa]";
            }
            else if (CbDataUnit2.SelectedIndex == 2)
            {
                UpperData2.Text = "Upper Limit Data Value [bar]";
                LowerData2.Text = "Lower Limit Data Value [bar]";
            }
            else if (CbDataUnit2.SelectedIndex == 3)
            {
                UpperData2.Text = "Upper Limit Data Value [mbar]";
                LowerData2.Text = "Lower Limit Data Value [mbar]";
            }
            else if (CbDataUnit2.SelectedIndex == 4)
            {
                UpperData2.Text = "Upper Limit Data Value [Torr]";
                LowerData2.Text = "Lower Limit Data Value [Torr]";
            }
            else if (CbDataUnit2.SelectedIndex == 5)
            {
                UpperData2.Text = "Upper Limit Data Value [mTorr]";
                LowerData2.Text = "Lower Limit Data Value [mTorr]";
            }
            else if (CbDataUnit2.SelectedIndex == 6)
            {
                UpperData2.Text = "Upper Limit Data Value [psia]";
                LowerData2.Text = "Lower Limit Data Value [psia]";
            }
            else if (CbDataUnit2.SelectedIndex == 7)
            {
                UpperData2.Text = "Upper Limit Data Value [psig]";
                LowerData2.Text = "Lower Limit Data Value [psig]";
            }
        }

        private void Pressure_Sensor_Load(object sender, EventArgs e)
        {
            CbDataUnit1.SelectedIndex = 0;
            CbDataUnit2.SelectedIndex = 0;

        }

       
    }
}
