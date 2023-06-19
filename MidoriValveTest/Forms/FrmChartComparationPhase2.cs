using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Runtime.InteropServices;

namespace MidoriValveTest.Forms
{
    public partial class FrmChartComparationPhase2 : Form
    {
        private string rutaArchivo;
        private List<double> presion;
        private List<double> tiempo;
        private List<int> whichCycle;
        private Dictionary<int, Color> cycleColors;
        private List<CheckBox> checkBoxesList;
        private string rutaCompareChartFileFiltered;

        [DllImport("user32.dll", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        public FrmChartComparationPhase2(string rutaCompareChartFileFiltered)
        {
            InitializeComponent();
            this.rutaCompareChartFileFiltered = rutaCompareChartFileFiltered;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void FrmChartComparationPhase2_Load(object sender, EventArgs e)
        {
            checkBoxesList = new List<CheckBox>()
            {
                checkBox1,
                checkBox2,
                checkBox3,
                checkBox4,
                checkBox5,
                checkBox6,
                checkBox7,
                checkBox8,
                checkBox9,
                checkBox10
            };
            BringToFront();
            ShowOpenFileDialog();
        }

        private void ShowOpenFileDialog()
        {
            rutaArchivo = rutaCompareChartFileFiltered;
            LoadChartData();
        }

        private void LoadChartData()
        {
            presion = new List<double>();
            tiempo = new List<double>();
            whichCycle = new List<int>();

            using (StreamReader sr = new StreamReader(rutaArchivo))
            {
                string linea;
                while ((linea = sr.ReadLine()) != null)
                {
                    string[] partes = linea.Split(',');

                    if (partes.Length == 3)
                    {
                        double valorPresion;
                        double valorTiempo;
                        int valorWhichCycle;

                        if (double.TryParse(partes[0], out valorPresion) &&
                            double.TryParse(partes[1], out valorTiempo) &&
                            int.TryParse(partes[2], out valorWhichCycle))
                        {
                            presion.Add(valorPresion);
                            tiempo.Add(valorTiempo);
                            whichCycle.Add(valorWhichCycle);
                        }
                    }
                }
            }

            FillChart();
        }

        private void FillChart()
        {
            chart1.Series.Clear();
            List<int> ciclosDistintos = whichCycle.Distinct().ToList();

            // Configurar el gráfico para habilitar el zoom y el desplazamiento
            chart1.ChartAreas.Clear();
            ChartArea chartArea = chart1.ChartAreas.Add("MainArea");
            chartArea.CursorX.IsUserEnabled = true;
            chartArea.CursorX.IsUserSelectionEnabled = true;
            chartArea.CursorY.IsUserEnabled = true;
            chartArea.CursorY.IsUserSelectionEnabled = true;

            chartArea.AxisX.ScaleView.Zoomable = true;
            chartArea.AxisX.ScrollBar.IsPositionedInside = false;
            chartArea.AxisX.ScrollBar.Enabled = true;

            // Configurar el aspecto del gráfico
            chart1.BackColor = Color.Teal;
            chartArea.BackColor = Color.CadetBlue;
            chartArea.AxisY.TitleForeColor = Color.White;
            chartArea.AxisY.TitleFont = new Font(chartArea.AxisY.TitleFont, FontStyle.Bold);
            chartArea.AxisX.TitleFont = new Font(chartArea.AxisY.TitleFont, FontStyle.Bold);
            chartArea.AxisY.LabelStyle.ForeColor = Color.White;
            chartArea.AxisX.LabelStyle.ForeColor = Color.White;
            chartArea.AxisX.LineColor = Color.White;
            chartArea.AxisY.LineColor = Color.White;
            chartArea.AxisX.MajorGrid.LineColor = Color.White;
            chartArea.AxisY.MajorGrid.LineColor = Color.White;
            chartArea.AxisX.MajorTickMark.LineColor = Color.White;
            chartArea.AxisY.MajorTickMark.LineColor = Color.White;

            chartArea.AxisY.Minimum = 0; // Establecer el valor mínimo del eje Y a 0
            chartArea.AxisX.Minimum = 0;

            chartArea.AxisX.LineWidth = 3;
            chartArea.AxisY.LineWidth = 3;

            chartArea.AxisX.MajorTickMark.LineWidth = 1; // Aumentar el grosor de la línea de la escala en el eje X
            chartArea.AxisY.MajorTickMark.LineWidth = 1; // Aumentar el grosor de la línea de la escala en el eje Y

            // Definir el diccionario de colores para los ciclos
            cycleColors = new Dictionary<int, Color>()
            {
                { 1, Color.Red },
                { 2, Color.Blue },
                { 3, Color.Yellow },
                { 4, Color.Green },
                { 5, Color.Purple },
                { 6, Color.Cyan },
                { 7, Color.Orange },
                { 8, Color.Maroon },
                { 9, Color.Lime},
                { 10, Color.Magenta}
            };

            foreach (int ciclo in ciclosDistintos)
            {
                string serieName = "Cycle #" + ciclo;
                Series serie = chart1.Series.Add(serieName);
                serie.ChartType = SeriesChartType.Spline;
                // Configurar el grosor de la línea de la serie
                serie.BorderWidth = 2; // Ajusta el grosor según tus necesidades
                // Configurar el color de la línea de la serie
                serie.Color = cycleColors[ciclo];

                for (int i = 0; i < presion.Count; i++)
                {
                    if (whichCycle[i] == ciclo)
                    {
                        serie.Points.AddXY(tiempo[i], presion[i]);
                    }
                }
            }

            Legend legend = chart1.Legends[0];
            legend.BackColor = Color.CadetBlue;
            legend.ForeColor = Color.White;
            legend.Font = new Font("Microsoft Sans Serif", 8);
            legend.Font = new Font(legend.Font, FontStyle.Bold);


            int cantidadSeries = chart1.Series.Count;

            // Recorrer los RadioButton
            for (int i = 0; i < checkBoxesList.Count; i++)
            {
                CheckBox checkBox = checkBoxesList[i];

                if (i < cantidadSeries)
                {
                    // Mostrar y habilitar el CheckBox correspondiente
                    Series series = chart1.Series[i];
                    checkBox.Visible = true;
                    checkBox.Enabled = true;
                    checkBox.Text = series.Name;
                    checkBox.Checked = true;
                }
                else
                {
                    // Ocultar y deshabilitar los CheckBox que no corresponden a ninguna serie
                    checkBox.Visible = false;
                    checkBox.Enabled = false;
                    checkBox.Text = string.Empty;
                }
            }


        }
        private void btnCloseFrm_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LogicRadio(CheckBox btn)
        {
            if (btn.Checked)
            {
                chart1.Series[btn.Text].Enabled = true;
            }
            else
            {
                chart1.Series[btn.Text].Enabled = false;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            LogicRadio(checkBox);
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            LogicRadio(checkBox);
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            LogicRadio(checkBox);
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            LogicRadio(checkBox);
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            LogicRadio(checkBox);
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            LogicRadio(checkBox);
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            LogicRadio(checkBox);
        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            LogicRadio(checkBox);
        }

        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            LogicRadio(checkBox);
        }

        private void checkBox10_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            LogicRadio(checkBox);
        }
    }
}
