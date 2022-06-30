using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace MidoriValveTest
{
    public partial class Report : Form
    {
        public Report()
        {
            InitializeComponent();
        }

        private void Report_Load(object sender, EventArgs e)
        {
            rtxtTexBoxForEdit.ReadOnly = true;
            rtxtTexBoxForEdit.Enabled = false;
        }

        private void IconSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (rtxtContenido.Text != string.Empty)
                {

                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        if (File.Exists(saveFileDialog1.FileName) && rtxtTexBoxForEdit.Text != string.Empty)
                        {

                            StreamWriter textsave = File.CreateText(saveFileDialog1.FileName);
                            string txtFirst = rtxtContenido.Text;
                            textsave.Write(txtFirst + "\n" + "\n" + rtxtTexBoxForEdit.Text + 
                             "\n" + "Modified: " + DateTime.Now.ToString("MM/dd/yy") + "  At: " + DateTime.Now.ToString("HH:mm:ss"));
                            textsave.Flush();
                            textsave.Close();
                            MessageBox.Show("Change Succesfully");
                            rtxtContenido.Clear();
                            rtxtTexBoxForEdit.Clear();
                            rtxtContenido.ReadOnly = false;
                            rtxtTexBoxForEdit.ReadOnly = true;
                            rtxtTexBoxForEdit.Enabled = false;
                            rtxtContenido.ForeColor = Color.Black;
                            rtxtTexBoxForEdit.ForeColor = Color.Black;

                        }
                        else
                        {
                            // Aqui se guarda normal por primera vez
                            string txt = saveFileDialog1.FileName;
                            StreamWriter textsave = File.CreateText(saveFileDialog1.FileName);
                            textsave.Write(rtxtContenido.Text +
                                "\n" +  "Created: " + DateTime.Now.ToString("MM/dd/yy") + "  At: " + DateTime.Now.ToString("HH:mm:ss"));
                            textsave.Flush();
                            textsave.Close();
                            MessageBox.Show("Save Succesfully");
                            rtxtContenido.Clear();

                        }
                    }
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Failed");;
            }

        }

        private void IconLoad_Click(object sender, EventArgs e)
        {

            rtxtContenido.Clear();
            

            try
            {
                openFileDialog1.Title = "Search your report MIDORI CR";
                openFileDialog1.ShowDialog();

                if (File.Exists(openFileDialog1.FileName))
                {
                    string report = openFileDialog1.FileName;

                    TextReader read = new StreamReader(report);
                    rtxtContenido.Text = read.ReadToEnd();               
                    read.Close();
                    rtxtContenido.ReadOnly = true;
                    rtxtTexBoxForEdit.ReadOnly = false;
                    rtxtTexBoxForEdit.Enabled = true;
                    rtxtContenido.ForeColor = Color.Gray;
                    rtxtTexBoxForEdit.ForeColor = Color.Black;

                }
            }


            catch (Exception)
            {
                MessageBox.Show("Fail");
                throw;
            }

            


        }

        private void IconClear_Click(object sender, EventArgs e)
        {
            rtxtContenido.Clear();
            rtxtTexBoxForEdit.Clear();
            rtxtContenido.ForeColor = Color.Black;
            rtxtTexBoxForEdit.ForeColor = Color.Black;
            rtxtTexBoxForEdit.Enabled = false;
            rtxtTexBoxForEdit.ReadOnly = true;
            rtxtContenido.ReadOnly = false;
            

        }

        
    }
}
