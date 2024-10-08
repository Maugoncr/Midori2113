﻿/// <summary>
/// Midori valve software
/// </summary>
/// using AForge.Video;
using AForge.Video.DirectShow;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.IO;
using System.IO.Ports;
using System.Threading;
using System.Runtime.InteropServices;
using System.Diagnostics;
using CustomMessageBox;
using MidoriValveTest.Forms;
using AForge.Video;
using CrystalDecisions.CrystalReports.Engine;
using MidoriValveTest.Properties;
using CrystalDecisions.Windows.Forms;
using System.Drawing.Drawing2D;

namespace MidoriValveTest
{


    public partial class Midori_PV : Form
    {
        //------------------- Work variable ------------
        Stopwatch oSW = new Stopwatch();
        bool record = false;                          // flag for record data
        public int precision_aperture = 0;           // apperture
        int base_value = 0;                         // ranges for apperture 
        bool InicioStartPID = true;                 // Flag for btnStarPID sent P or T
        bool connect = false;                       // flag for connect status
        public static bool EnviarPID = false;       // flag for Sent the PID
        double rt = 0;                              // Time X from chart
        double temp = 0;                            // Time in ms
        bool MostrarSetPoint = false;

        bool PedirMKS1 = false;
        bool PedirMKS2 = false;
        bool PedirMKS3 = false;

        DateTime star_record = new DateTime();
        DateTime end_record = new DateTime();

        //--------------- (Temp LIST for record data) -----------------
        private List<string> times = new List<string>();
        private List<string> apertures = new List<string>();
        private List<string> pressures = new List<string>();
        private List<string> datetimes = new List<string>();

        //var for menu

        private const int widthSlide = 200;
        private const int widthSlideIcon = 46;
        [DllImport("user32.dll", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);


        // funcion de cosntruccion de clase (inicio automatico), inicializa los componentes visuales, (no es recomendado agregar mas funcionamiento a este)
        public Midori_PV()
        {
            InitializeComponent();
            InitializeSetting();
            InicializarComboboxes();


            Control.CheckForIllegalCrossThreadCalls = false;
        }

        public void InitializeSetting()
        {
            this.FormBorderStyle = FormBorderStyle.None;
            // For Torr Units
            s_inicial = 755;
            s_final = 760;
            trackBar2A.Maximum = 760;
            lbl_T_0.Text = "0";
            lbl_T_1.Text = "84.44";
            lbl_T_2.Text = "168.88";
            lbl_T_3.Text = "253.32";
            lbl_T_4.Text = "337.76";
            lbl_T_5.Text = "422.2";
            lbl_T_6.Text = "506.64";
            lbl_T_7.Text = "591.08";
            lbl_T_8.Text = "675.52";
            lbl_T_9.Text = "760";
            lbl_units_track.Text = "Torr";
            lbl_P_unit_top.Text = "Torr";
            lbl_presure_chart.Text = "[Torr]";
        }

        private List<ComboBox> comboBoxes;
        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox selectedComboBox = (ComboBox)sender;
            bool isItemSelectedInOtherComboBox = false;
            foreach (ComboBox comboBox in comboBoxes)
            {
                if (comboBox != selectedComboBox && comboBox.SelectedItem != null && comboBox.SelectedItem.Equals(selectedComboBox.SelectedItem))
                {
                    isItemSelectedInOtherComboBox = true;
                    break;
                }
            }
            if (isItemSelectedInOtherComboBox)
            {
                MessageBoxMaugoncr.Show("Cannot select the same item in multiple ComboBoxes.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                selectedComboBox.SelectedIndex = -1;
            }
            else
            {
                selectedComboBox = comboBoxes.Find(comboBox => comboBox.SelectedItem != null && comboBox.SelectedItem.Equals(selectedComboBox.SelectedItem));
                if (selectedComboBox != null)
                {
                    if (selectedComboBox == cbMKS1)
                    {
                        ActivarConnectMKS(1);
                    }
                    else if (selectedComboBox == cbMKS2)
                    {
                        ActivarConnectMKS(2);
                    }
                    else if (selectedComboBox == cbMKS3)
                    {
                        ActivarConnectMKS(3);
                    }
                    else if (selectedComboBox == cbSelectionCOM)
                    {
                        EnableBtn(btnConnect);
                    }
                }
            }

        }

        private void InicializarComboboxes() 
        {
            comboBoxes = new List<ComboBox> { cbMKS1, cbMKS2, cbMKS3, cbSelectionCOM };

            // Suscribe al evento SelectedIndexChanged para todos los ComboBox
            foreach (ComboBox comboBox in comboBoxes)
            {
                comboBox.SelectedIndexChanged += ComboBox_SelectedIndexChanged;
            }
        }

        

        public void Alert(string msg, Form_Alert.enmType type)
        {
            Form_Alert frm = new Form_Alert();
            frm.showAlert(msg, type);
        }




        // Funcion de carga de procedimientos iniciales (inicio automatico). 
        private void Form1_Load(object sender, EventArgs e)
        {
            bool firstTime = Settings.Default.FirstTimeOpen;

            if (firstTime)
            {
                FrmAskNameReport frm = new FrmAskNameReport();
                DialogResult resultado = frm.ShowDialog();

                if (resultado == DialogResult.OK)
                {
                    Settings.Default.FirstTimeOpen = false;
                    Settings.Default.Save();
                }
                else
                {
                    Application.Exit();
                }
            }

            CrearArchivoContadorReportesGenerados();
            OffEverything();
            timer1.Enabled = true;
            TimerAnimation.Start();



            //if (cbSelectionCOM.Items.Count > 0)     // exist ports com
            //{
            //    cbSelectionCOM.SelectedIndex = 0;
            //    EnableBtn(btnConnect);
            //}
        }

        private void OffEverything()
        {
            lbClientSettings.Text = Settings.Default.CodeProject + " " + Settings.Default.Customer;

            if (serialPort1.IsOpen)
            {
                serialPort1.Close();
            }
            if (serialPortMKS1.IsOpen)
            {
                serialPortMKS1.Close();
            }
            if (serialPortMKS2.IsOpen)
            {
                serialPortMKS2.Close();
            }

            //Timers
            timerTemporizador.Stop();
            timerForData.Stop();

            PedirMKS1 = false;
            PedirMKS2 = false;
            PedirMKS3 = false;

            //MarathonTEST
            // Hace que siempre al realizar un reset la variable haga que cualquier secuencia mal detenida se apague por completo!
            runTimer = false;
            stillRunning = false;
            stopChrono = false;
            generateReport = false;
            SetpointPhase2 = 0;

            ResetVariablesPhases();

            btnStopMarathon.Enabled = false;
            btnStopMarathon.BackgroundImage = Resources.StopDisable;

            // Return all the variables from ObjetosGlobales to default
            ObjetosGlobales.P = "x";
            ObjetosGlobales.I = "x";
            ObjetosGlobales.D = "x";
            ObjetosGlobales.flagPID = false;
            ObjetosGlobales.ApperCali = 90;

            //Return all variables to default from MIDORI_PV

            record = false;
            InicioStartPID = true;
            i = false;
            AutocalibracionPrendida = false;
            base_value = 0;
            precision_aperture = 0;
            times = new List<string>();
            apertures = new List<string>();
            pressures = new List<string>();
            datetimes = new List<string>();
            EnviarPID = false;
            MostrarSetPoint = false;
            Manual = true;
            Auto = false;
            AxisY2Maximo = 1000;

            //Reset for Camaras
            Animation1 = true;
            Animation2 = true;
            Animation3 = true;
            Animation4 = true;
            Animation5 = true;

            timerAnimation1 = true;
            timerAnimation2 = true;
            timerAnimation3 = true;
            timerAnimation4 = true;
            timerAnimation5 = true;

            CerrarWebCam();
            CerrarWebCam2();
            CerrarWebCam3();
            CerrarWebCam4();
            CerrarWebCam5();

            animation = 0;
            aniIMG = 0;
            offorOn = false;

            animation2 = 0;
            aniIMG2 = 0;
            offorOn2 = false;

            animation3 = 0;
            aniIMG3 = 0;
            offorOn3 = false;

            animation4 = 0;
            aniIMG4 = 0;
            offorOn4 = false;

            animation5 = 0;
            aniIMG5 = 0;
            offorOn5 = false;


            // Return all labels to default text

            LblEstado.Text = "Disconnected *";
            LblEstado.ForeColor = Color.FromArgb(15, 60, 89);
            lblPuerto.ForeColor = Color.FromArgb(15, 60, 89);
            lblPuerto.Text = "Disconnected *";
            lbl_estado.Text = "OFF";
            lbl_record.Text = "OFF";
            Current_aperture.Text = "0°";
            lb_Temperature.Text = " 0 °C";
            lbl_pressure.Text = "0";
            lbSetPointPressure.Text = "---";

            //Return texts btn to default

            btnSetApertura.Text = "Set Apperture";
            btnSetPresion.Text = "Set Target Pressure";
            btnStartPID.Text = "Start PID";
            btnAutoCalibrate.Text = "Autocalibration";
            txtSetPresion.Clear();

            // Load COM
            cbSelectionCOM.Enabled = true;
            cbMKS1.Enabled = true;
            cbMKS2.Enabled = true;
            cbMKS3.Enabled = true;

            string[] ports = SerialPort.GetPortNames();

            cbSelectionCOM.Items.Clear();
            cbMKS1.Items.Clear();
            cbMKS2.Items.Clear();
            cbMKS3.Items.Clear();

            cbSelectionCOM.Items.AddRange(ports);
            cbMKS1.Items.AddRange(ports);
            cbMKS2.Items.AddRange(ports);
            cbMKS3.Items.AddRange(ports);


            //Enable Buttons

            //Disable Buttons
            btnConexionMKS.Enabled = true;

            btnConnectMKS1.Enabled = false;
            btnConnectMKS1.BackgroundImage.Dispose();
            btnConnectMKS1.BackgroundImage = Resources.TurnOnDisable;

            btnDisconnectMKS1.Enabled = false;
            btnDisconnectMKS1.BackgroundImage.Dispose();
            btnDisconnectMKS1.BackgroundImage = Resources.TurnOffDisable;

            btnConnectMKS2.Enabled = false;
            btnConnectMKS2.BackgroundImage.Dispose();
            btnConnectMKS2.BackgroundImage = Resources.TurnOnDisable;

            btnDisconnectMKS2.Enabled = false;
            btnDisconnectMKS2.BackgroundImage.Dispose();
            btnDisconnectMKS2.BackgroundImage = Resources.TurnOffDisable;

            btnConnectMKS3.Enabled = false;
            btnConnectMKS3.BackgroundImage.Dispose();
            btnConnectMKS3.BackgroundImage = Resources.TurnOnDisable;

            btnDisconnectMKS3.Enabled = false;
            btnDisconnectMKS3.BackgroundImage.Dispose();
            btnDisconnectMKS3.BackgroundImage = Resources.TurnOffDisable;

            lbStatusMKS1.Text = "* Disconnected";
            lbStatusMKS2.Text = "* Disconnected";
            lbStatusMKS3.Text = "* Disconnected";
            lbMKS1.Text = "---";
            lbMKS2.Text = "---";
            lbMKS3.Text = "---";

            DisableBtn(btnOpenGate);
            DisableBtn(btnCloseGate);
            DisableBtn(btnSetApertura);
            DisableBtn(btnSetPresion);
            DisableBtn(btnStartPID);
            DisableBtn(btnStartRecord);
            DisableBtn(btnStopRecord);
            DisableBtn(btnChartArchiveAnalyzer);
            DisableBtn(btnCompareChart);
            DisableBtn(btnPIDAnalisis);
            DisableBtn(btnAutoCalibrate);
            DisableBtn(btnEMO);
            DisableBtn(btnConnect);
            DisableBtn(btnInfo);
            DisableBtn(btn_valveTest);

            // Man Val Controls
            DisableBtn(btnOffMANValve);
            DisableBtn(btnOffMANValve2);
            DisableBtn(btnOnMANValve);
            DisableBtn(btnOnMANValve2);

            // Pump Controls

            DisableBtn(btnOffPump);
            DisableBtn(btnOnPump);
            lbPumpStatus.Text = "OFF";

            //DisableBtn(btnPlay);
            //DisableBtn(btn_Stop);
            //DisableBtn(btn_Pause);

            iconPID.Enabled = false;
            txtSetPresion.Enabled = false;

            //Buttons for Degrees
            DisableBtn(btn_90);
            DisableBtn(btn_80);
            DisableBtn(btn_70);
            DisableBtn(btn_60);
            DisableBtn(btn_50);
            DisableBtn(btn_40);
            DisableBtn(btn_30);
            DisableBtn(btn_20);
            DisableBtn(btn_10);
            DisableBtn(btn_0);

            //Disable Trackbars
            trackBar1A.Enabled = false;
            trackBar2A.Enabled = false;
            trackBar2A.Value = 0;
            trackBar1A.Value = 0;

            // Visual Valve IMG's
            picture_frontal.Image.Dispose();
            picture_frontal.Image = MidoriValveTest.Properties.Resources.Front0;
            picture_plane.Image.Dispose();
            picture_plane.Image = MidoriValveTest.Properties.Resources.Verti0B;

            //Led status
            com_led.Image.Dispose();
            com_led.Image = MidoriValveTest.Properties.Resources.led_off;

            //Led status Man V

            ManVOpen1.Image.Dispose();
            ManVOpen1.Image = Properties.Resources.led_off_green;
            ManVClose1.Image.Dispose();
            ManVClose1.Image = Properties.Resources.led_off_red;
            ManVOpen2.Image.Dispose();
            ManVOpen2.Image = Properties.Resources.led_off_green;
            ManVClose2.Image.Dispose();
            ManVClose2.Image = Properties.Resources.led_off_red;

            // Chart
            chart1.Series["Aperture value"].Points.Clear();
            chart1.Series["Pressure"].Points.Clear();
            ChartArea CA = chart1.ChartAreas[0];
            CA.CursorX.AutoScroll = true;

            // Limpiar todo sobre TEST
            LimpiarTestArea(true);

        }


        // Metodo para cambiar para remplazar el enable disable button y usar uno general

        private void DisableBtn(Button btn)
        {

            btn.BackgroundImage.Dispose();
            btn.BackgroundImage = Resources.btnDisa2;
            btn.Enabled = false;
            btn.ForeColor = Color.White;

        }

        private void EnableBtn(Button btn)
        {
            btn.BackgroundImage.Dispose();
            btn.BackgroundImage = Resources.btnNor;
            btn.Enabled = true;
        }

        private void EnableBtnEMO(Button btn)
        {
            btn.BackgroundImage.Dispose();
            btn.BackgroundImage = Resources.btnEMOYellow;
            btn.Enabled = true;
            btn.ForeColor = Color.Red;
        }


        //Maugoncr// Nos permite comprobar que en caso de que al iniciar la carga del form no habia ningun com para reconocer, en caso de reconocerse luego de esta
        // ser capaces de activar el boton Connect


      

        //Maugoncr// 
        // Reboot the whole system as when it started up

        private void btnRestart_Click(object sender, EventArgs e)
        {
            if (t != null && t.IsAlive)
            {
                t.Abort();
            }
            OffEverything();
            this.Alert("Successfully restarted", Form_Alert.enmType.Success);
        }


        // Accion en boton "CONNECT" en la seccion "COM SELECT" 
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (reconocer_arduino(cbSelectionCOM.SelectedItem.ToString()))// Funcion para establecer conexion COM con la valvula. 
                {
                    timerForData.Start();
                    txtSetPresion.Enabled = true;
                    com_led.Image.Dispose();
                    com_led.Image = MidoriValveTest.Properties.Resources.led_on_green;
                    ManVClose1.Image.Dispose();
                    ManVClose2.Image.Dispose();
                    ManVClose1.Image = Properties.Resources.led_on_red;
                    ManVClose2.Image = Properties.Resources.led_on_red;

                    EnableBtn(btnOpenGate);
                    btn_P_conf.Enabled = true;
                    EnableBtn(btn_valveTest);
                    cbSelectionCOM.Enabled = false;
                    DisableBtn(btnConnect);
                    EnableBtn(btnStartPID);
                    EnableBtn(btnOnMANValve);
                    EnableBtn(btnOnMANValve2);
                    EnableBtn(btnOnPump);


                    // Menu settings
                    btnMenu.Enabled = true;
                    iconTerminal.Enabled = true;
                    iconPID.Enabled = true;
                    IconSensor.Enabled = true;
                    IconTrace.Enabled = true;
                    IconReport.Enabled = true;

                    trackBar1A.Enabled = true;
                    trackBar2A.Enabled = true;

                    btnAutoCalibrate.Enabled = true;
                    btnPIDAnalisis.Enabled = true;
                    btnInfo.Enabled = true;
                    btnConexionMKS.Enabled = false;

                    EnableBtn(btn_90);
                    EnableBtn(btn_80);
                    EnableBtn(btn_70);
                    EnableBtn(btn_60);
                    EnableBtn(btn_50);
                    EnableBtn(btn_40);
                    EnableBtn(btn_30);
                    EnableBtn(btn_20);
                    EnableBtn(btn_10);
                    EnableBtn(btn_0);
                    EnableBtn(btnStartRecord);
                    EnableBtn(btnChartArchiveAnalyzer);
                    EnableBtn(btnCompareChart);
                    EnableBtn(btnAutoCalibrate);
                    EnableBtn(btnPIDAnalisis);
                    EnableBtnEMO(btnEMO);
                   

                    // Se propone realizar una apertura y cierre total de todas las valvulas principalmente BCV para evitar problemas
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool reconocerCOMMKS(string COMM, int which)
        {
            if (which == 1)
            {
                try
                {
                    if (serialPortMKS1.IsOpen)
                    {
                        serialPortMKS1.Close();
                        return false;
                    }

                    serialPortMKS1.PortName = COMM;
                    serialPortMKS1.Open();
                    lbStatusMKS1.Text = "* Connected";

                    return true;
                }
                catch (UnauthorizedAccessException)
                {
                    lbStatusMKS1.Text = "* Disconnected";
                    serialPortMKS1.Close();

                    cbMKS1.Enabled = true;

                    MessageBoxMaugoncr.Show("Access to " + COMM + " port denied", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                catch (Exception ex)
                {
                    lbStatusMKS1.Text = "* Disconnected";
                    serialPortMKS1.Close();

                    cbMKS1.Enabled = true;

                    MessageBox.Show(ex.Message);
                    return false;
                }
            }
            else
            {
                try
                {
                    if (serialPortMKS2.IsOpen)
                    {
                        serialPortMKS2.Close();
                        return false;
                    }

                    serialPortMKS2.PortName = COMM;
                    serialPortMKS2.Open();
                    lbStatusMKS2.Text = "* Connected";

                    return true;
                }
                catch (UnauthorizedAccessException)
                {
                    lbStatusMKS2.Text = "* Disconnected";
                    serialPortMKS2.Close();

                    cbMKS2.Enabled = true;

                    MessageBoxMaugoncr.Show("Access to " + COMM + " port denied", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                catch (Exception ex)
                {
                    lbStatusMKS2.Text = "* Disconnected";
                    serialPortMKS2.Close();

                    cbMKS2.Enabled = true;

                    MessageBox.Show(ex.Message);
                    return false;
                }
            }

        }

        public bool reconocer_arduino(string COMM)
        {
            try
            {
                if (serialPort1.IsOpen)
                {
                    serialPort1.Close();
                    return false;
                }
                // Remember Baud Rate
                serialPort1.PortName = COMM;
                serialPort1.Open();
                LblEstado.Text = "Connected";
                lblPuerto.Text = COMM;
                connect = true;

                string validarData = serialPort1.ReadExisting();

                if (validarData == null || validarData == "")
                {
                    LblEstado.Text = "Disconnected *";
                    lblPuerto.Text = "Disconnected *";
                    serialPort1.Close();

                    MessageBoxMaugoncr.Show("Data is not being received correctly. The program will not start until this is fixed.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                return true;
            }
            catch (UnauthorizedAccessException)
            {
                MessageBoxMaugoncr.Show("Access to " + COMM + " port denied", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LblEstado.Text = "Disconnected *";
                lblPuerto.Text = "Disconnected *";
                cbSelectionCOM.Enabled = true;
                cbMKS1.Enabled = true;
                cbMKS2.Enabled = true;
                return false;
            }
            catch (Exception ex)
            {
                LblEstado.Text = "Disconnected *";
                lblPuerto.Text = "Disconnected *";
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        private void btn_encender_Click(object sender, EventArgs e)
        {
            trackBar1A.Enabled = true;
            trackBar2A.Enabled = true;

            //Maugoncr// Valide default degrees
            if (trackBar1A.Value != 0)
            {
                precision_aperture = trackBar1A.Value;
            }
            else
            {
                precision_aperture = 90;
                picture_frontal.Image.Dispose();
                picture_frontal.Image = MidoriValveTest.Properties.Resources.Front90;
                picture_plane.Image.Dispose();
                picture_plane.Image = MidoriValveTest.Properties.Resources.Verti90B;
            }

            serialPort1.Write(precision_aperture.ToString());
            Thread.Sleep(50);

            Current_aperture.Text = precision_aperture + "°";
            lbl_estado.ForeColor = Color.Red;
            lbl_estado.Text = "Open";
            DisableBtn(btnOpenGate);
            EnableBtn(btnCloseGate);

            EnableBtn(btn_90);
            EnableBtn(btn_80);
            EnableBtn(btn_70);
            EnableBtn(btn_60);
            EnableBtn(btn_50);
            EnableBtn(btn_40);
            EnableBtn(btn_30);
            EnableBtn(btn_20);
            EnableBtn(btn_10);
            EnableBtn(btn_0);


            EnableBtn(btnSetApertura);
            EnableBtn(btnInfo);
            EnableBtn(btnChartArchiveAnalyzer);
            EnableBtnEMO(btnEMO);
            EnableBtn(btnCompareChart);
            //stop
            EnableBtn(btnStopRecord);
            // grabar
            EnableBtn(btnStartRecord);

        }

        private void btn_apagar_Click(object sender, EventArgs e)
        {

            serialPort1.Write("0");
            Thread.Sleep(50);

            precision_aperture = 0;
            Current_aperture.Text = precision_aperture + "°";
            picture_frontal.Image.Dispose();
            picture_frontal.Image = Resources.Front0;
            picture_plane.Image.Dispose();
            picture_plane.Image = Resources.Verti0B;

            trackBar1A.Value = 0;

            lbl_estado.ForeColor = Color.Red;
            lbl_estado.Text = "Close";
            btnSetPresion.Text = "Set Target Pressure";
            btnSetApertura.Text = "Set Apperture";

            EnableBtn(btnOpenGate);
            DisableBtn(btnCloseGate);
        }

        private void btn_valveTest_Click(object sender, EventArgs e)
        {
            stillRunning = false;

            Form frm = Application.OpenForms.Cast<Form>().FirstOrDefault(x => x is TestCicles);
            if (frm == null)
            {
                TestCicles TEST = new TestCicles();
                TEST.menssager = this;
                TEST.Show();
            }
            else
            {
                frm.BringToFront();
                return;
            }
        }
        private void ContarCycle()
        {
            int x = Convert.ToInt32(lbCountCycles.Text);
            x++;
            lbCountCycles.Text = x.ToString();
            lb_CounterTest.Text = x.ToString();
        }

        private void EncenderPump()
        {
            lbPumpStatus.Text = "ON";
            EnableBtn(btnOffPump);
            DisableBtn(btnOnPump);
        }

        private void ApagarPump()
        {
            lbPumpStatus.Text = "OFF";
            EnableBtn(btnOnPump);
            DisableBtn(btnOffPump);
        }

        private void AbrirSolenoid_1()
        {
            EnableBtn(btnOffMANValve);
            DisableBtn(btnOnMANValve);
            ManVOpen1.Image.Dispose();
            ManVOpen1.Image = Properties.Resources.led_on_green;
            ManVClose1.Image.Dispose();
            ManVClose1.Image = Properties.Resources.led_off_red;
        }

        private void CerrarSolenoid_1()
        {
            EnableBtn(btnOnMANValve);
            DisableBtn(btnOffMANValve);
            ManVOpen1.Image.Dispose();
            ManVOpen1.Image = Properties.Resources.led_off_green;
            ManVClose1.Image.Dispose();
            ManVClose1.Image = Properties.Resources.led_on_red;
        }

        private void AbrirSolenoid_2()
        {
            EnableBtn(btnOffMANValve2);
            DisableBtn(btnOnMANValve2);
            ManVOpen2.Image.Dispose();
            ManVOpen2.Image = Properties.Resources.led_on_green;
            ManVClose2.Image.Dispose();
            ManVClose2.Image = Properties.Resources.led_off_red;
        }

        private void CerrarSolenoid_2()
        {
            EnableBtn(btnOnMANValve2);
            DisableBtn(btnOffMANValve2);
            ManVOpen2.Image.Dispose();
            ManVOpen2.Image = Properties.Resources.led_off_green;
            ManVClose2.Image.Dispose();
            ManVClose2.Image = Properties.Resources.led_on_red;
        }

        private void VisualCerrarMainV()
        {
            Current_aperture.Text = "0°";
            lbl_estado.Text = "Close";
            picture_frontal.Image.Dispose();
            picture_plane.Image.Dispose();
            picture_frontal.Image = Properties.Resources.Verti0B;
            picture_plane.Image = Properties.Resources.Front0;
            base_value = 0;
            trackBar1A.Value = 0;
            precision_aperture = 0;
            DisableBtn(btnCloseGate);
            EnableBtn(btnOpenGate);
        }

        private void VisualAbrirMainV()
        {
            Current_aperture.Text = "90°";
            lbl_estado.Text = "Open";
            picture_frontal.Image.Dispose();
            picture_plane.Image.Dispose();
            picture_frontal.Image = Properties.Resources.Verti90B;
            picture_plane.Image = Properties.Resources.Front90;
            base_value = 90;
            trackBar1A.Value = 90;
            precision_aperture = 90;
            DisableBtn(btnOpenGate);
            EnableBtn(btnCloseGate);
        }

        public int TestToRun = 0;
        public int NumTest = 1;
        public int SetpointPhase2 = 0;
        Thread t;

        public void NewThreadForTest()
        {
            LimpiarTestArea();
            btnStopMarathon.Enabled = true;
            btnStopMarathon.BackgroundImage = Resources.Stop;
            btnLimpiarTestArea.Enabled = false;
            InicioChrono = DateTime.Now;
            lbGoalCycles.Text = NumTest.ToString();
            stillRunning = false;

            ResetVariablesPhases();

            t = new Thread(new ThreadStart(EjecutarTest));
            t.Start();
            DisableBtn(btn_valveTest);
        }

        private void EjecutarTest()
        {
            //Q - Abrir solenoid 1
            //W - Cerrar solenoid 1
            //E - Abrir solenoid 2
            //R - Cerrar solenoid 2
            //Y - Encender Pump
            //U - Apagar Pump

            try
            {
                if (TestToRun == 1)
                {
                    DateStartedTest.Text = DateTime.Now.ToString("MM/dd/yy || hh:mm:ss tt");

                    // Apagar Pump
                    serialPort1.Write("U");
                    Thread.Sleep(50);
                    ApagarPump();

                    // Abrir Solenoid 1
                    serialPort1.Write("Q");
                    Thread.Sleep(50);
                    AbrirSolenoid_1();

                    // Abrir Solenoid 2
                    serialPort1.Write("E");
                    Thread.Sleep(50);
                    AbrirSolenoid_2();

                    // Abrir Valvula Main
                    serialPort1.Write("90");
                    Thread.Sleep(50);
                    VisualAbrirMainV();

                    if (stillRunning)
                    {
                        stopChrono = true;
                        return;
                    }

                    for (int i = 1; i <= NumTest; i++)
                    {
                        // #1
                        serialPort1.Write("0");
                        lbStepForTest.Text = "Close [BCV40]";
                        VisualCerrarMainV();

                        tiempoSeleccionado = TimeSpan.ParseExact("00:02", @"mm\:ss", null);
                        lbTemporizadorStepByStep.Text = tiempoSeleccionado.ToString(@"mm\:ss");
                        runTimer = true;

                        for (int j = 0; j < 2; j++)
                        {
                            Thread.Sleep(1000);
                            if (stillRunning)
                            {
                                stopChrono = true;
                                return;
                            }
                        }

                        // #2
                        serialPort1.Write("R");
                        lbStepForTest.Text = "Close [PN ISO-V2]";
                        CerrarSolenoid_2();

                        tiempoSeleccionado = TimeSpan.ParseExact("00:02", @"mm\:ss", null);
                        lbTemporizadorStepByStep.Text = tiempoSeleccionado.ToString(@"mm\:ss");
                        runTimer = true;

                        for (int j = 0; j < 2; j++)
                        {
                            Thread.Sleep(1000);
                            if (stillRunning)
                            {
                                stopChrono = true;
                                return;
                            }
                        }

                        // #3
                        serialPort1.Write("Y");
                        lbStepForTest.Text = "On [PUMP]";
                        EncenderPump();

                        tiempoSeleccionado = TimeSpan.ParseExact("00:02", @"mm\:ss", null);
                        lbTemporizadorStepByStep.Text = tiempoSeleccionado.ToString(@"mm\:ss");
                        runTimer = true;

                        for (int j = 0; j < 2; j++)
                        {
                            Thread.Sleep(1000);
                            if (stillRunning)
                            {
                                stopChrono = true;
                                return;
                            }
                        }

                        // #4
                        lbStepForTest.Text = "Waiting down to 1 torr";

                        tiempoSeleccionado = TimeSpan.ParseExact("02:00", @"mm\:ss", null);
                        lbTemporizadorStepByStep.Text = tiempoSeleccionado.ToString(@"mm\:ss");
                        runTimer = true;

                        for (int j = 0; j < 120; j++)
                        {
                            Thread.Sleep(1000);
                            if (stillRunning)
                            {
                                stopChrono = true;
                                return;
                            }
                        }

                        // #5
                        serialPort1.Write("W");
                        lbStepForTest.Text = "Close [PN ISO-V1]";
                        CerrarSolenoid_1();

                        tiempoSeleccionado = TimeSpan.ParseExact("00:02", @"mm\:ss", null);
                        lbTemporizadorStepByStep.Text = tiempoSeleccionado.ToString(@"mm\:ss");
                        runTimer = true;

                        for (int j = 0; j < 2; j++)
                        {
                            Thread.Sleep(1000);
                            if (stillRunning)
                            {
                                stopChrono = true;
                                return;
                            }
                        }

                        // #6
                        serialPort1.Write("U");
                        lbStepForTest.Text = "Off [PUMP]";
                        ApagarPump();

                        tiempoSeleccionado = TimeSpan.ParseExact("00:02", @"mm\:ss", null);
                        lbTemporizadorStepByStep.Text = tiempoSeleccionado.ToString(@"mm\:ss");
                        runTimer = true;

                        for (int j = 0; j < 2; j++)
                        {
                            Thread.Sleep(1000);
                            if (stillRunning)
                            {
                                stopChrono = true;
                                return;
                            }
                        }

                        // #7
                        lbStepForTest.Text = "Verify leak for 5 min";

                        tiempoSeleccionado = TimeSpan.ParseExact("05:00", @"mm\:ss", null);
                        lbTemporizadorStepByStep.Text = tiempoSeleccionado.ToString(@"mm\:ss");
                        runTimer = true;

                        for (int j = 0; j < 300; j++)
                        {
                            Thread.Sleep(1000);
                            if (stillRunning)
                            {
                                stopChrono = true;
                                return;
                            }
                        }

                        // #8
                        serialPort1.Write("Q");
                        lbStepForTest.Text = "Open [PN ISO-V1]";
                        AbrirSolenoid_1();

                        tiempoSeleccionado = TimeSpan.ParseExact("00:02", @"mm\:ss", null);
                        lbTemporizadorStepByStep.Text = tiempoSeleccionado.ToString(@"mm\:ss");
                        runTimer = true;

                        for (int j = 0; j < 2; j++)
                        {
                            Thread.Sleep(1000);
                            if (stillRunning)
                            {
                                stopChrono = true;
                                return;
                            }
                        }

                        // #9
                        serialPort1.Write("E");
                        lbStepForTest.Text = "Open [PN ISO-V2]";
                        AbrirSolenoid_2();

                        tiempoSeleccionado = TimeSpan.ParseExact("00:04", @"mm\:ss", null);
                        lbTemporizadorStepByStep.Text = tiempoSeleccionado.ToString(@"mm\:ss");
                        runTimer = true;

                        for (int j = 0; j < 4; j++)
                        {
                            Thread.Sleep(1000);
                            if (stillRunning)
                            {
                                stopChrono = true;
                                return;
                            }
                        }

                        // #10
                        serialPort1.Write("90");
                        lbStepForTest.Text = "Open [BCV40]";
                        VisualAbrirMainV();

                        tiempoSeleccionado = TimeSpan.ParseExact("00:02", @"mm\:ss", null);
                        lbTemporizadorStepByStep.Text = tiempoSeleccionado.ToString(@"mm\:ss");
                        runTimer = true;

                        for (int j = 0; j < 2; j++)
                        {
                            Thread.Sleep(1000);
                            if (stillRunning)
                            {
                                stopChrono = true;
                                return;
                            }
                        }

                        ContarCycle();
                    }
                    // TERMINA CORRECTAMENTE!!
                    generateReport = true;
                    stopChrono = true;

                    DateEndedTest.Text = DateTime.Now.ToString("MM/dd/yy || hh:mm:ss tt");
                    lbStepForTest.Text = "Phase 1 Finished";
                }
                else if (TestToRun == 2)
                {

                    phase2Setpoint = SetpointPhase2;

                    DateStartedTest.Text = DateTime.Now.ToString("MM/dd/yy || hh:mm:ss tt");

                    // Apagar Pump
                    serialPort1.Write("U");
                    Thread.Sleep(50);
                    ApagarPump();
                    // Abre Valvula Main
                    serialPort1.Write("90");
                    Thread.Sleep(50);
                    VisualAbrirMainV();

                    // Abre Solenoid 1
                    serialPort1.Write("Q");
                    Thread.Sleep(50);
                    AbrirSolenoid_1();

                    // Abre Solenoid 2
                    serialPort1.Write("E");
                    Thread.Sleep(50);
                    AbrirSolenoid_2();

                    if (stillRunning)
                    {
                        stopChrono = true;
                        return;
                    }
                    // FIN INICIAL STATE

                    // #1
                    serialPort1.Write("0");
                    lbStepForTest.Text = "Close [BCV40]";
                    VisualCerrarMainV();

                    tiempoSeleccionado = TimeSpan.ParseExact("00:02", @"mm\:ss", null);
                    lbTemporizadorStepByStep.Text = tiempoSeleccionado.ToString(@"mm\:ss");
                    runTimer = true;

                    for (int j = 0; j < 2; j++)
                    {
                        Thread.Sleep(1000);
                        if (stillRunning)
                        {
                            stopChrono = true;
                            return;
                        }
                    }

                    // #2
                    serialPort1.Write("R");
                    lbStepForTest.Text = "Close [PN ISO-V2]";
                    CerrarSolenoid_2();

                    tiempoSeleccionado = TimeSpan.ParseExact("00:02", @"mm\:ss", null);
                    lbTemporizadorStepByStep.Text = tiempoSeleccionado.ToString(@"mm\:ss");
                    runTimer = true;

                    for (int j = 0; j < 2; j++)
                    {
                        Thread.Sleep(1000);
                        if (stillRunning)
                        {
                            stopChrono = true;
                            return;
                        }
                    }

                    // #3
                    serialPort1.Write("Y");
                    lbStepForTest.Text = "On [PUMP]";
                    EncenderPump();

                    tiempoSeleccionado = TimeSpan.ParseExact("00:02", @"mm\:ss", null);
                    lbTemporizadorStepByStep.Text = tiempoSeleccionado.ToString(@"mm\:ss");
                    runTimer = true;

                    for (int j = 0; j < 2; j++)
                    {
                        Thread.Sleep(1000);
                        if (stillRunning)
                        {
                            stopChrono = true;
                            return;
                        }
                    }

                    // #4
                    lbStepForTest.Text = "Waiting down to 1 torr";

                    tiempoSeleccionado = TimeSpan.ParseExact("02:00", @"mm\:ss", null);
                    lbTemporizadorStepByStep.Text = tiempoSeleccionado.ToString(@"mm\:ss");
                    runTimer = true;

                    for (int j = 0; j < 120; j++)
                    {
                        Thread.Sleep(1000);
                        if (stillRunning)
                        {
                            stopChrono = true;
                            return;
                        }
                    }

                    // #5
                    serialPort1.Write("S" + SetpointPhase2 + ",0.1,0.1,0.1");
                    Thread.Sleep(50);
                    serialPort1.Write("P");

                    lbStepForTest.BackColor = Color.Yellow;
                    lbStepForTest.Text = "Waiting 30s to Manual Reset";

                    tiempoSeleccionado = TimeSpan.ParseExact("00:30", @"mm\:ss", null);
                    lbTemporizadorStepByStep.Text = tiempoSeleccionado.ToString(@"mm\:ss");
                    runTimer = true;

                    for (int j = 0; j < 30; j++)
                    {
                        Thread.Sleep(1000);
                        if (stillRunning)
                        {
                            stopChrono = true;
                            return;
                        }
                    }

                    lbStepForTest.BackColor = Color.Transparent;

                    for (int i = 1; i <= NumTest; i++)
                    {
                        // #6
                        lbStepForTest.Text = "STABILITY TEST";

                        tiempoSeleccionado = TimeSpan.ParseExact("05:00", @"mm\:ss", null);
                        lbTemporizadorStepByStep.Text = tiempoSeleccionado.ToString(@"mm\:ss");
                        runTimer = true;

                        timeForChartCompareRecordPhase2 = 0;
                        tempForChartCompareRecordPhase2 = 0;
                        grabarPresionPhase2 = true;

                        for (int j = 0; j < 300; j++)
                        {
                            Thread.Sleep(1000);
                            double pressureChartDinamic = Convert.ToDouble(presionChart);
                            if (capturarPresionPhase2 == false && pressureChartDinamic >= phase2Setpoint-6 && pressureChartDinamic <= phase2Setpoint+6)
                            {
                                capturarPresionPhase2 = true;
                            }
                            if (j == 120 && capturarPresionPhase2 == false)
                            {
                                capturarPresionPhase2 = true;
                            }
                            if (stillRunning)
                            {
                                stopChrono = true;
                                return;
                            }
                        }

                        grabarPresionPhase2 = false;
                        capturarPresionPhase2 = false;

                        // #7
                        serialPort1.Write("E");
                        lbStepForTest.Text = "Open [PN ISO-V2]";
                        AbrirSolenoid_2();

                        tiempoSeleccionado = TimeSpan.ParseExact("00:04", @"mm\:ss", null);
                        lbTemporizadorStepByStep.Text = tiempoSeleccionado.ToString(@"mm\:ss");
                        runTimer = true;

                        for (int j = 0; j < 4; j++)
                        {
                            Thread.Sleep(1000);
                            if (stillRunning)
                            {
                                stopChrono = true;
                                return;
                            }
                        }

                        // #8
                        serialPort1.Write("R");
                        lbStepForTest.Text = "Close [PN ISO-V2]";
                        CerrarSolenoid_2();

                        tiempoSeleccionado = TimeSpan.ParseExact("00:02", @"mm\:ss", null);
                        lbTemporizadorStepByStep.Text = tiempoSeleccionado.ToString(@"mm\:ss");
                        runTimer = true;

                        for (int j = 0; j < 2; j++)
                        {
                            Thread.Sleep(1000);
                            if (stillRunning)
                            {
                                stopChrono = true;
                                return;
                            }
                        }

                        ContarCycle();

                        if (lbCountCycles.Text == "1")
                        {
                            CalcularPhase2PerCycle(true);
                        }
                        else
                        {
                            CalcularPhase2PerCycle();
                        }
                    }

                    // #9
                    serialPort1.Write("U");
                    lbStepForTest.Text = "Off [PUMP]";
                    ApagarPump();

                    tiempoSeleccionado = TimeSpan.ParseExact("00:02", @"mm\:ss", null);
                    lbTemporizadorStepByStep.Text = tiempoSeleccionado.ToString(@"mm\:ss");
                    runTimer = true;

                    for (int j = 0; j < 2; j++)
                    {
                        Thread.Sleep(1000);
                        if (stillRunning)
                        {
                            stopChrono = true;
                            return;
                        }
                    }

                    // #10
                    serialPort1.Write("E");
                    lbStepForTest.Text = "Open [PN ISO-V2]";
                    AbrirSolenoid_2();

                    tiempoSeleccionado = TimeSpan.ParseExact("00:04", @"mm\:ss", null);
                    lbTemporizadorStepByStep.Text = tiempoSeleccionado.ToString(@"mm\:ss");
                    runTimer = true;

                    for (int j = 0; j < 4; j++)
                    {
                        Thread.Sleep(1000);
                        if (stillRunning)
                        {
                            stopChrono = true;
                            return;
                        }
                    }

                    // #11
                    serialPort1.Write("90");
                    lbStepForTest.Text = "Open [BCV40]";
                    VisualAbrirMainV();

                    tiempoSeleccionado = TimeSpan.ParseExact("00:02", @"mm\:ss", null);
                    lbTemporizadorStepByStep.Text = tiempoSeleccionado.ToString(@"mm\:ss");
                    runTimer = true;

                    for (int j = 0; j < 2; j++)
                    {
                        Thread.Sleep(1000);
                        if (stillRunning)
                        {
                            stopChrono = true;
                            return;
                        }
                    }

                    // #12 Stop PID NEED RESET
                    serialPort1.Write("T");
                    lbStepForTest.Text = "Waiting 20s to Manual Reset";

                    tiempoSeleccionado = TimeSpan.ParseExact("00:20", @"mm\:ss", null);
                    lbTemporizadorStepByStep.Text = tiempoSeleccionado.ToString(@"mm\:ss");
                    runTimer = true;

                    for (int j = 0; j < 20; j++)
                    {
                        Thread.Sleep(1000);
                        if (stillRunning)
                        {
                            stopChrono = true;
                            return;
                        }
                    }

                    generateReport = true;
                    stopChrono = true;

                    DateEndedTest.Text = DateTime.Now.ToString("MM/dd/yy || hh:mm:ss tt");
                    lbStepForTest.Text = "Phase 2 Finished";

                    // Save CSV File 
                    GuardarGrabacionPhase2();

                }
                else if (TestToRun == 3)
                {
                    DateStartedTest.Text = DateTime.Now.ToString("MM/dd/yy || hh:mm:ss tt");

                    // Apagar Pump
                    serialPort1.Write("U");
                    Thread.Sleep(50);
                    ApagarPump();
                    // Abre Valvula Main
                    serialPort1.Write("90");
                    Thread.Sleep(50);
                    VisualAbrirMainV();
                    // Abre Solenoid 1
                    serialPort1.Write("Q");
                    Thread.Sleep(50);
                    AbrirSolenoid_1();
                    // Abre Solenoid 2
                    serialPort1.Write("E");
                    Thread.Sleep(50);
                    AbrirSolenoid_2();

                    if (stillRunning)
                    {
                        stopChrono = true;
                        return;
                    }

                    for (int i = 1; i <= NumTest; i++)
                    {
                        // #1
                        serialPort1.Write("0");
                        lbStepForTest.Text = "Close [BCV40]";
                        VisualCerrarMainV();

                        tiempoSeleccionado = TimeSpan.ParseExact("00:02", @"mm\:ss", null);
                        lbTemporizadorStepByStep.Text = tiempoSeleccionado.ToString(@"mm\:ss");
                        runTimer = true;

                        for (int j = 0; j < 2; j++)
                        {
                            Thread.Sleep(1000);
                            if (stillRunning)
                            {
                                stopChrono = true;
                                return;
                            }
                        }

                        // #2
                        serialPort1.Write("R");
                        lbStepForTest.Text = "Close [PN ISO-V2]";
                        CerrarSolenoid_2();

                        tiempoSeleccionado = TimeSpan.ParseExact("00:02", @"mm\:ss", null);
                        lbTemporizadorStepByStep.Text = tiempoSeleccionado.ToString(@"mm\:ss");
                        runTimer = true;

                        for (int j = 0; j < 2; j++)
                        {
                            Thread.Sleep(1000);
                            if (stillRunning)
                            {
                                stopChrono = true;
                                return;
                            }
                        }

                        // #3
                        serialPort1.Write("Y");
                        lbStepForTest.Text = "On [PUMP]";
                        EncenderPump();

                        tiempoSeleccionado = TimeSpan.ParseExact("00:02", @"mm\:ss", null);
                        lbTemporizadorStepByStep.Text = tiempoSeleccionado.ToString(@"mm\:ss");
                        runTimer = true;

                        for (int j = 0; j < 2; j++)
                        {
                            Thread.Sleep(1000);
                            if (stillRunning)
                            {
                                stopChrono = true;
                                return;
                            }
                        }

                        // #4
                        lbStepForTest.Text = "Waiting down to 1 torr";

                        tiempoSeleccionado = TimeSpan.ParseExact("02:00", @"mm\:ss", null);
                        lbTemporizadorStepByStep.Text = tiempoSeleccionado.ToString(@"mm\:ss");
                        runTimer = true;

                        for (int j = 0; j < 120; j++)
                        {
                            Thread.Sleep(1000);
                            if (stillRunning)
                            {
                                stopChrono = true;
                                return;
                            }
                        }

                        // #5
                        serialPort1.Write("W");
                        lbStepForTest.Text = "Close [PN ISO-V1]";
                        CerrarSolenoid_1();

                        tiempoSeleccionado = TimeSpan.ParseExact("00:02", @"mm\:ss", null);
                        lbTemporizadorStepByStep.Text = tiempoSeleccionado.ToString(@"mm\:ss");
                        runTimer = true;

                        for (int j = 0; j < 2; j++)
                        {
                            Thread.Sleep(1000);
                            if (stillRunning)
                            {
                                stopChrono = true;
                                return;
                            }
                        }

                        // #6
                        serialPort1.Write("U");
                        lbStepForTest.Text = "Off [PUMP]";
                        ApagarPump();

                        tiempoSeleccionado = TimeSpan.ParseExact("00:02", @"mm\:ss", null);
                        lbTemporizadorStepByStep.Text = tiempoSeleccionado.ToString(@"mm\:ss");
                        runTimer = true;

                        for (int j = 0; j < 2; j++)
                        {
                            Thread.Sleep(1000);
                            if (stillRunning)
                            {
                                stopChrono = true;
                                return;
                            }
                        }

                        // #7
                        lbStepForTest.Text = "Verify leak for 1 min";

                        tiempoSeleccionado = TimeSpan.ParseExact("01:00", @"mm\:ss", null);
                        lbTemporizadorStepByStep.Text = tiempoSeleccionado.ToString(@"mm\:ss");
                        runTimer = true;

                        //Reset de las variables para cada ciclo evitar problemas!
                        pressureDinamicMax = 0;
                        pressureDinamicMin = double.MaxValue;
                        capturarPresionMaxMinPhase3 = true;
                        grabarPresionPhase3 = true;

                        for (int j = 0; j < 60; j++)
                        {
                            Thread.Sleep(1000);
                            if (stillRunning)
                            {
                                stopChrono = true;
                                return;
                            }
                        }

                        capturarPresionMaxMinPhase3 = false;
                        grabarPresionPhase3 = false;

                        // #8
                        serialPort1.Write("Q");
                        lbStepForTest.Text = "Open [PN ISO-V1]";
                        AbrirSolenoid_1();

                        tiempoSeleccionado = TimeSpan.ParseExact("00:02", @"mm\:ss", null);
                        lbTemporizadorStepByStep.Text = tiempoSeleccionado.ToString(@"mm\:ss");
                        runTimer = true;

                        for (int j = 0; j < 2; j++)
                        {
                            Thread.Sleep(1000);
                            if (stillRunning)
                            {
                                stopChrono = true;
                                return;
                            }
                        }

                        // #9
                        serialPort1.Write("E");
                        lbStepForTest.Text = "Open [PN ISO-V2]";
                        AbrirSolenoid_2();

                        tiempoSeleccionado = TimeSpan.ParseExact("00:04", @"mm\:ss", null);
                        lbTemporizadorStepByStep.Text = tiempoSeleccionado.ToString(@"mm\:ss");
                        runTimer = true;

                        for (int j = 0; j < 4; j++)
                        {
                            Thread.Sleep(1000);
                            if (stillRunning)
                            {
                                stopChrono = true;
                                return;
                            }
                        }

                        // #10
                        serialPort1.Write("90");
                        lbStepForTest.Text = "Open [BCV40]";
                        VisualAbrirMainV();

                        tiempoSeleccionado = TimeSpan.ParseExact("00:02", @"mm\:ss", null);
                        lbTemporizadorStepByStep.Text = tiempoSeleccionado.ToString(@"mm\:ss");
                        runTimer = true;

                        for (int j = 0; j < 2; j++)
                        {
                            Thread.Sleep(1000);
                            if (stillRunning)
                            {
                                stopChrono = true;
                                return;
                            }
                        }

                        ContarCycle();

                        if (lbCountCycles.Text == "1")
                        {
                            CalcularPhase3PerCycle(true);
                        }
                        else
                        {
                            CalcularPhase3PerCycle();
                        }
                    }

                    generateReport = true;
                    stopChrono = true;
                    DateEndedTest.Text = DateTime.Now.ToString("MM/dd/yy || hh:mm:ss tt");
                    lbStepForTest.Text = "Phase 3 Finished";

                    // Función que guarde en csv los datos recopilados!
                    GuardarGrabacionPhase3();
                }
            }
            catch (Exception ex)
            {
                string message = "    The marathon test ended abruptly\n             Exception Message:\n           " + ex.Message;
                MessageBoxMaugoncr.Show(message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }
        }

        private void GuardarGrabacionPhase2() 
        {
            string rutaCompleta;

            if (Directory.Exists(Settings.Default.PathSaveRecords))
            {
                string ruta = Settings.Default.PathSaveRecords;
                string nameFile = "Report Data Phase 2 Created at " + (DateTime.Now.ToString("MM-dd-yy  HH-mm-ss"));
                string extension = ".txt";
                rutaCompleta = Path.Combine(ruta, nameFile + extension);
            }
            else
            {
                string ruta = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string saveFolderPath = Path.Combine(ruta, "MIDORI II REPORT DATA");
                Directory.CreateDirectory(saveFolderPath);
                string nameFile = "Report Data Phase 2 Created at " + (DateTime.Now.ToString("MM-dd-yy  HH-mm-ss"));
                string extension = ".txt";
                rutaCompleta = Path.Combine(saveFolderPath, nameFile + extension);
            }

            try
            {
                File.WriteAllText(rutaCompleta, string.Empty);
                using (StreamWriter writer = new StreamWriter(rutaCompleta))
                {
                    writer.WriteLine("# Software Midori II Phase 2");
                    writer.WriteLine("# Start Test Time " + DateStartedTest.Text);
                    writer.WriteLine("# End Test Time " + DateEndedTest.Text);
                    writer.WriteLine("# Operator Test " + Settings.Default.Operator);
                    writer.WriteLine("Cycle Number , Pressure , TimeChartComparation, Time , DateTime");

                    for (int i = 0; i < timesPressurePhase2L.Count; i++)
                    {
                        writer.WriteLine(numberCyclePhase2L[i] + "," + pressuresPhase2L[i] +","+ timesChartComparePressurePhase2L[i] +"," + timesPressurePhase2L[i] + "," + datetimesPhase2L[i]);
                    }
                }

                pressuresPhase2L.Clear();
                timesPressurePhase2L.Clear();
                timesChartComparePressurePhase2L.Clear();
                numberCyclePhase2L.Clear();
                datetimesPhase2L.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void GuardarGrabacionPhase3() 
        {
            string rutaCompleta;

            if (Directory.Exists(Settings.Default.PathSaveRecords))
            {
                string ruta = Settings.Default.PathSaveRecords;
                string nameFile = "Report Data Phase 3 Created at " + (DateTime.Now.ToString("MM-dd-yy  HH-mm-ss"));
                string extension = ".txt";
                rutaCompleta = Path.Combine(ruta, nameFile + extension);
            }
            else
            {
                string ruta = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string saveFolderPath = Path.Combine(ruta, "MIDORI II REPORT DATA");
                Directory.CreateDirectory(saveFolderPath);
                string nameFile = "Report Data Phase 3 Created at " + (DateTime.Now.ToString("MM-dd-yy  HH-mm-ss"));
                string extension = ".txt";
                rutaCompleta = Path.Combine(saveFolderPath, nameFile + extension);
            }

            try
            {
                File.WriteAllText(rutaCompleta, string.Empty);
                using (StreamWriter writer = new StreamWriter(rutaCompleta))
                {
                    writer.WriteLine("# Software Midori II Phase 3");
                    writer.WriteLine("# Start Test Time " + DateStartedTest.Text);
                    writer.WriteLine("# End Test Time " + DateEndedTest.Text);
                    writer.WriteLine("# Operator Test " + Settings.Default.Operator);
                    writer.WriteLine("Cycle Number , Pressure , Time , DateTime");

                    for (int i = 0; i < timesPressurePhase3L.Count; i++)
                    {
                        writer.WriteLine(numberCyclePhase3L[i] + "," + pressuresPhase3L[i] + "," + timesPressurePhase3L[i] + "," + datetimesPhase3L[i]);
                    }
                }

                pressuresPhase3L.Clear();
                timesPressurePhase3L.Clear();
                numberCyclePhase3L.Clear();
                datetimesPhase3L.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_0_Click(object sender, EventArgs e)
        {
            picture_frontal.Image.Dispose();
            picture_plane.Image.Dispose();
            picture_frontal.Image = MidoriValveTest.Properties.Resources.Front0;
            picture_plane.Image = MidoriValveTest.Properties.Resources.Verti0B;
            base_value = 0;
            trackBar1A.Value = 0;
            Current_aperture.Text = trackBar1A.Value + "°";
            btnSetApertura.Text = "Set Aperture";
            if (lbl_estado.Text == "Open")
            {
                EnableBtn(btnSetApertura);
            }

        }

        private void btn_10_Click(object sender, EventArgs e)
        {
            picture_frontal.Image.Dispose();
            picture_plane.Image.Dispose();
            picture_frontal.Image = MidoriValveTest.Properties.Resources.Front10;
            picture_plane.Image = MidoriValveTest.Properties.Resources.Verti10B;
            base_value = 10;
            trackBar1A.Value = 10;
            Current_aperture.Text = trackBar1A.Value + "°";

            if (lbl_estado.Text == "Open")
            {
                EnableBtn(btnSetApertura);
                btnSetApertura.Text = "Set Aperture in 10";
            }
        }

        private void btn_20_Click(object sender, EventArgs e)
        {
            picture_frontal.Image.Dispose();
            picture_plane.Image.Dispose();
            picture_frontal.Image = MidoriValveTest.Properties.Resources.Front20;
            picture_plane.Image = MidoriValveTest.Properties.Resources.Verti20B;
            base_value = 20;
            trackBar1A.Value = 20;
            Current_aperture.Text = trackBar1A.Value + "°";

            if (lbl_estado.Text == "Open")
            {
                EnableBtn(btnSetApertura);
                btnSetApertura.Text = "Set Aperture in 20";
            }
        }

        private void btn_30_Click(object sender, EventArgs e)
        {
            picture_frontal.Image.Dispose();
            picture_plane.Image.Dispose();
            picture_frontal.Image = MidoriValveTest.Properties.Resources.Front30;
            picture_plane.Image = MidoriValveTest.Properties.Resources.Verti30B;
            base_value = 30;
            trackBar1A.Value = 30;
            Current_aperture.Text = trackBar1A.Value + "°";
            if (lbl_estado.Text == "Open")
            {
                EnableBtn(btnSetApertura);
                btnSetApertura.Text = "Set Aperture in 30";

            }
        }

        private void btn_40_Click(object sender, EventArgs e)
        {
            picture_frontal.Image.Dispose();
            picture_plane.Image.Dispose();
            picture_frontal.Image = MidoriValveTest.Properties.Resources.Front40;
            picture_plane.Image = MidoriValveTest.Properties.Resources.Verti40B;
            base_value = 40;
            trackBar1A.Value = 40;
            Current_aperture.Text = trackBar1A.Value + "°";
            if (lbl_estado.Text == "Open")
            {
                EnableBtn(btnSetApertura);
                btnSetApertura.Text = "Set Aperture in 40";
            }
        }

        private void btn_50_Click(object sender, EventArgs e)
        {
            picture_frontal.Image.Dispose();
            picture_plane.Image.Dispose();
            picture_frontal.Image = MidoriValveTest.Properties.Resources.Front50;
            picture_plane.Image = MidoriValveTest.Properties.Resources.Verti50B;
            base_value = 50;
            trackBar1A.Value = 50;
            Current_aperture.Text = trackBar1A.Value + "°";

            if (lbl_estado.Text == "Open")
            {
                EnableBtn(btnSetApertura);
                btnSetApertura.Text = "Set Aperture in 50";
            }

        }

        private void btn_60_Click(object sender, EventArgs e)
        {
            picture_frontal.Image.Dispose();
            picture_plane.Image.Dispose();
            picture_frontal.Image = MidoriValveTest.Properties.Resources.Front60;
            picture_plane.Image = MidoriValveTest.Properties.Resources.Verti60B;
            base_value = 60;
            trackBar1A.Value = 60;

            Current_aperture.Text = trackBar1A.Value + "°";
            if (lbl_estado.Text == "Open")
            {
                EnableBtn(btnSetApertura);
                btnSetApertura.Text = "Set Aperture in 60";
            }
        }

        private void btn_70_Click(object sender, EventArgs e)
        {
            picture_frontal.Image.Dispose();
            picture_plane.Image.Dispose();
            picture_frontal.Image = MidoriValveTest.Properties.Resources.Front70;
            picture_plane.Image = MidoriValveTest.Properties.Resources.Verti70B;
            base_value = 70;
            trackBar1A.Value = 70;
            Current_aperture.Text = trackBar1A.Value + "°";
            if (lbl_estado.Text == "Open")
            {
                EnableBtn(btnSetApertura);
                btnSetApertura.Text = "Set Aperture in 70";
            }
        }

        private void btn_80_Click(object sender, EventArgs e)
        {
            picture_frontal.Image.Dispose();
            picture_plane.Image.Dispose();
            picture_frontal.Image = MidoriValveTest.Properties.Resources.Front80;
            picture_plane.Image = MidoriValveTest.Properties.Resources.Verti80B;
            base_value = 80;
            trackBar1A.Value = 80;
            Current_aperture.Text = trackBar1A.Value + "°";
            if (lbl_estado.Text == "Open")
            {
                EnableBtn(btnSetApertura);
                btnSetApertura.Text = "Set Aperture in 80";
            }

        }

        private void btn_90_Click(object sender, EventArgs e)
        {
            picture_frontal.Image.Dispose();
            picture_plane.Image.Dispose();
            picture_frontal.Image = MidoriValveTest.Properties.Resources.Front90;
            picture_plane.Image = MidoriValveTest.Properties.Resources.Verti90B;
            base_value = 90;
            trackBar1A.Value = 90;
            Current_aperture.Text = trackBar1A.Value + "°";
            if (lbl_estado.Text == "Open")
            {
                btnSetApertura.Enabled = true;
                btnSetApertura.Text = "Set Aperture in 90";
            }

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            int pos = trackBar1A.Value;


            switch (base_value)
            {
                case 0:
                    if (pos > 9)
                    {
                        trackBar1A.Value = 9;

                    }
                    break;
                case 10:
                    if (pos < 10)
                    {
                        trackBar1A.Value = 10;
                    }
                    else if (pos > 19)
                    {
                        trackBar1A.Value = 19;
                    }
                    break;
                case 20:
                    if (pos < 20)
                    {
                        trackBar1A.Value = 20;
                    }
                    else if (pos > 29)
                    {
                        trackBar1A.Value = 29;
                    }
                    break;
                case 30:
                    if (pos < 30)
                    {
                        trackBar1A.Value = 30;
                    }
                    else if (pos > 39)
                    {
                        trackBar1A.Value = 39;
                    }
                    break;
                case 40:
                    if (pos < 40)
                    {
                        trackBar1A.Value = 40;
                    }
                    else if (pos > 49)
                    {
                        trackBar1A.Value = 49;
                    }
                    break;
                case 50:
                    if (pos < 50)
                    {
                        trackBar1A.Value = 50;
                    }
                    else if (pos > 59)
                    {
                        trackBar1A.Value = 59;
                    }
                    break;
                case 60:
                    if (pos < 60)
                    {
                        trackBar1A.Value = 60;
                    }
                    else if (pos > 69)
                    {
                        trackBar1A.Value = 69;
                    }
                    break;
                case 70:
                    if (pos < 70)
                    {
                        trackBar1A.Value = 70;
                    }
                    else if (pos > 79)
                    {
                        trackBar1A.Value = 79;
                    }
                    break;
                case 80:
                    if (pos < 80)
                    {
                        trackBar1A.Value = 80;
                    }
                    else if (pos > 89)
                    {
                        trackBar1A.Value = 89;
                    }
                    break;
                case 90:
                    if (pos < 90)
                    {
                        trackBar1A.Value = 90;
                    }
                    break;


            }


            //btn_set.Enabled = true;
            btnSetApertura.Text = "Set Aperture in " + trackBar1A.Value + "°";
            //precision_aperture = trackBar1.Value;


        }

        private void ObtenerData(string full)
        {
            string test = full;
            test.Trim();
            bool firtIn = false;
            bool secondIn = false;
            bool thirdIn = false;
            string Temp = "";
            string Pressure = "";
            string PressureSetPoint = "";
            // A120,250J180$
            for (int i = 0; i < test.Length; i++)
            {
                if (test.Substring(i, 1).Equals("$"))
                {
                    break;
                }
                if (thirdIn == true)
                {
                    PressureSetPoint += test.Substring(i, 1);
                }
                if (test.Substring(i, 1).Equals("J"))
                {
                    secondIn = false;
                    thirdIn = true;
                }
                if (secondIn == true)
                {
                    Pressure += test.Substring(i, 1);
                }
                if (test.Substring(i, 1).Equals(","))
                {
                    firtIn = false;
                    secondIn = true;
                }
                if (firtIn == true)
                {
                    Temp += test.Substring(i, 1);
                }
                if (test.Substring(i, 1).Equals("A"))
                {
                    firtIn = true;
                }
            }
            Temp.Replace("A", "");
            Pressure.Replace("$", "");
            PressureSetPoint.Replace("J", "");
            temperaturaLabel = Temp;
            presionSetPoint = PressureSetPoint;
            try
            {
                switch (lbl_P_unit_top.Text)
                {
                    case "PSI":
                        if (Pressure != "")
                        {
                            double presionPSI = Math.Round(Convert.ToDouble(Pressure) / 51.715, 4);
                            presionChart = presionPSI.ToString();
                        }
                        break;
                    case "mbar":
                        if (Pressure != "")
                        {
                            double presionMBAR = Math.Round(Convert.ToDouble(Pressure) * 1.33322, 4);
                            presionChart = presionMBAR.ToString();
                        }
                        break;
                    case "ATM":
                        if (Pressure != "")
                        {
                            double presionATM = Math.Round(Convert.ToDouble(Pressure) / 760, 4);
                            presionChart = presionATM.ToString();
                        }
                        break;
                    case "Torr":
                        presionChart = Pressure;
                        break;
                }
            }
            catch (Exception)
            {
            }
        }


        private readonly Random _random = new Random();
        double final = 0.0;
        public decimal pressure_get;
        DateTime n = new DateTime();

        public double s_inicial = 13.5555;
        public double s_final = 14.6959;
        //Maugoncr// Aqui es donde se algoritman las lineas de manera random 

        //Maugoncr// Boton de iniciar la grabacion del chart
        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to start recording?, The real time graph will be reset to start recording.", "Midori Valve", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                chart1.Series["Aperture value"].Points.Clear();
                chart1.Series["Pressure"].Points.Clear();
                record = true;
                rt = 0;
                star_record = DateTime.Now;
                DisableBtn(btnStartRecord);
                EnableBtn(btnStopRecord);
                lbl_record.Text = "Recording...";
            }
        }

        //Maugoncr// Boton de stop para la grabación
        private void button2_Click(object sender, EventArgs e)
        {
            if (record == true)
            {
                record = false;
                end_record = DateTime.Now;
                saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                saveFileDialog1.FilterIndex = 2;
                saveFileDialog1.RestoreDirectory = true;
                saveFileDialog1.InitialDirectory = @"C:\";
                saveFileDialog1.FileName = "VALVE_RECORD_" + end_record.AddMilliseconds(-40).ToString("yyyy_MM_dd-hh_mm_ss");
                saveFileDialog1.ShowDialog();

                if (saveFileDialog1.FileName != "")
                {


                    // Saves the Image via a FileStream created by the OpenFile method.

                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"" + saveFileDialog1.FileName + ".txt"))
                    {
                        file.WriteLine("** MIDORI VALVE **");
                        file.WriteLine("#------------------------------------------------------------------");
                        file.WriteLine("#Datetime: " + star_record.ToString("yyyy/MM/dd - hh:mm:ss:ff tt"));
                        file.WriteLine("#Data Time range: [" + star_record.ToString(" hh:mm:ss:ff tt") + " - " + end_record.ToString(" hh:mm:ss:ff tt") + "]");
                        file.WriteLine("#Data |Time,seconds,[s],ChartAxisX ");
                        file.WriteLine("#Data |Apperture,grades,[°],ChartAxisY1 ");
                        file.WriteLine("#Data |Pressure,pounds per square inch,[psi],ChartAxisY2 ");
                        file.WriteLine("#------------------------------------------------------------------");
                        file.WriteLine("#PARAMETER    |Chart Type = valve record");
                        file.WriteLine("#PARAMETER    |Valve serie =");
                        file.WriteLine("#PARAMETER    |Valve Software Version =");
                        file.WriteLine("#PARAMETER    |Valve Firmware Version =");
                        file.WriteLine("#PARAMETER    |Position Unit = 0 - 90 =");

                        file.WriteLine("#------------------------------------------------------------------");
                        file.WriteLine("-|-  Time  -|-  Apperture  -|-  Pressure  -|-  DateTime  -|-");

                        file.WriteLine("#------------------------------------------------------------------");
                        for (int i = 0; i < times.Count; i++)
                        {

                            file.WriteLine(times[i] + " , " + apertures[i] + " , " + pressures[i] + " , " + datetimes[i]);

                        }
                        file.WriteLine("#------------------------------------------------------------------");
                    }
                }
                DisableBtn(btnStopRecord);
                EnableBtn(btnStartRecord);
                times.Clear();
                apertures.Clear();
                pressures.Clear();
                datetimes.Clear();
                lbl_record.Text = "OFF";
            }
            else
            {
                MessageBox.Show("The recording has not started", "Midori Valve", MessageBoxButtons.OK);
            }
        }


        //Chart Analicer
        private void button7_Click(object sender, EventArgs e)
        {

            Chart_Analyzer_File cd = new Chart_Analyzer_File();
            string[] line_in_depure;
            List<string> times_1 = new List<string>();
            List<string> apertures_1 = new List<string>();
            List<string> pressures_1 = new List<string>();
            List<string> datetimes_1 = new List<string>();

            OpenFileDialog OpenFile = new OpenFileDialog();
            OpenFile.Filter = "Texto | *.txt";

            int initial_line = 0;
            string range = "";
            string[] times;
            if (OpenFile.ShowDialog() == DialogResult.OK)
            {
                string FileToRead = OpenFile.FileName;
                cd.archivo = FileToRead;
                using (StreamReader sr = new StreamReader(FileToRead))
                {
                    if (System.IO.Path.GetExtension(FileToRead).ToLower() == ".txt")
                    {
                        //Dado el caso, verifico que exista el archivo..
                        if (System.IO.File.Exists(FileToRead))
                        {
                            //Lo ejecuto.
                            //System.Diagnostics.Process.Start(FileToRead);
                            // Creating string array  
                            string[] lines = File.ReadAllLines(FileToRead);
                            for (int i = 0; i < lines.Length; i++)
                            {


                                using (StreamReader tr = new StreamReader(FileToRead))
                                {
                                    cd.richTextBox1.Text = tr.ReadToEnd();
                                }

                                if (lines[i].Contains("#Data Time range: ["))
                                {
                                    //MessageBox.Show(lines[i]);

                                    range = lines[i].Replace("#Data Time range: [", string.Empty);
                                    MessageBox.Show(range);
                                    //MessageBox.Show(lines[i].Replace("#Data Time range:", string.Empty));
                                    range = range.Remove(range.Length - 1);
                                    //MessageBox.Show(range);
                                    // range = range.Remove(0);
                                    // MessageBox.Show(range);
                                    times = range.Split('-');
                                    //MessageBox.Show(times[0]);

                                    //MessageBox.Show(times[1]);
                                    cd.ini_range = times[0];
                                    cd.end_range = times[1];


                                }

                                if (lines[i] == "-|-  Time  -|-  Apperture  -|-  Pressure  -|-  DateTime  -|-" && lines[i + 1] == "#------------------------------------------------------------------")
                                {
                                    initial_line = i + 2;

                                    // MessageBox.Show((initial_line).ToString());
                                    //break;
                                }


                            }
                            for (int y = initial_line; y < lines.Length - 1; y++)
                            {
                                line_in_depure = lines[y].Split(',');
                                times_1.Add(line_in_depure[0]);
                                apertures_1.Add(line_in_depure[1]);
                                pressures_1.Add(line_in_depure[2]);
                                datetimes_1.Add(line_in_depure[3]);
                                Console.WriteLine(String.Join(Environment.NewLine, line_in_depure[0] + " " + line_in_depure[1] + " " + line_in_depure[2] + " " + line_in_depure[3]));

                            }


                            cd.times = times_1;
                            cd.apertures = apertures_1;
                            cd.pressures = pressures_1;
                            cd.datetimes = datetimes_1;
                            cd.Show();




                        }
                        else
                        {
                            MessageBox.Show("Doesnt exist.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalided format");
                    }
                }
            }
        }




        private void pictureBox3_Click(object sender, EventArgs e)
        {
            LateralNav.Size = new Size(0, 1019);
        }



        private void Midori_PV_MouseClick(object sender, MouseEventArgs e)
        {
            if (LateralNav.Width != 0)
            {
                LateralNav.Size = new Size(0, 1019);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            PID_Config nt = new PID_Config();

            nt.ShowDialog();

        }

        private void btn_P_conf_Click(object sender, EventArgs e)
        {
            unit_form un = new unit_form();
            un.ob = this;
            un.ShowDialog();
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {

            EnableBtn(btnSetPresion);
            float nivel = trackBar2A.Value;

            switch (lbl_P_unit_top.Text)
            {
                case "PSI":
                    btnSetPresion.Text = "Set target pressure in " + nivel / 10000;
                    break;
                case "ATM":
                    btnSetPresion.Text = "Set target pressure in " + nivel / 1000;
                    break;
                case "mbar":
                    btnSetPresion.Text = "Set target pressure in " + nivel / 100;
                    break;
                case "Torr":
                    btnSetPresion.Text = "Set target pressure in " + nivel;
                    txtSetPresion.Text = trackBar2A.Value.ToString();
                    break;
            }
        }


        //Maugoncr// Set clic de la apertura AZUL ESTE SIRVE
        private void btn_set_Click(object sender, EventArgs e)
        {
            precision_aperture = trackBar1A.Value;
            Current_aperture.Text = precision_aperture + "°";
            btnSetApertura.Text = "Set Aperture";
            DisableBtn(btnSetApertura);
            lbl_estado.ForeColor = Color.Red;
            lbl_estado.Text = "Open";
            serialPort1.Write(precision_aperture.ToString());

        }


        //Maugoncr// Set clic de la presión VERDE
        private void btn_S_pressure_Click_1(object sender, EventArgs e)
        {
            double presion = trackBar2A.Value;


            switch (lbl_P_unit_top.Text)
            {
                case "PSI":
                    if (presion <= 146959 && presion > 130624)
                    {
                        s_inicial = 130624 / 10000;
                        s_final = 146959 / 10000;
                    }
                    else if (presion <= 130624 && presion > 114296)
                    {
                        s_inicial = 114296 / 10000;
                        s_final = 130624 / 10000;
                    }
                    else if (presion <= 114296 && presion > 97968)
                    {
                        s_inicial = 97968 / 10000;
                        s_final = 114296 / 10000;
                    }
                    else if (presion <= 97968 && presion > 81640)
                    {
                        s_inicial = 81640 / 10000;
                        s_final = 97968 / 10000;
                    }
                    else if (presion <= 81640 && presion > 65312)
                    {
                        s_inicial = 65312 / 10000;
                        s_final = 81640 / 10000;
                    }
                    else if (presion <= 65312 && presion > 48984)
                    {
                        s_inicial = 48984 / 10000;
                        s_final = 65312 / 10000;
                    }
                    else if (presion <= 48984 && presion > 32656)
                    {
                        s_inicial = 32656 / 10000;
                        s_final = 48954 / 10000;
                    }
                    else if (presion <= 32656 && presion > 16328)
                    {
                        s_inicial = 16328 / 10000;
                        s_final = 32656 / 10000;
                    }
                    else if (presion <= 16328 && presion > 0)
                    {
                        s_inicial = 0;
                        s_final = 16328 / 10000;
                    };


                    break;

                case "ATM":

                    presion = presion / 1000;

                    if (presion <= 1 && presion > 0.88)
                    {
                        s_inicial = 0.88;
                        s_final = 1;
                    }
                    else if (presion <= 0.88 && presion > 0.77)
                    {
                        s_inicial = 0.77;
                        s_final = 0.88;
                    }
                    else if (presion <= 0.77 && presion > 0.66)
                    {
                        s_inicial = 0.66;
                        s_final = 0.77;
                    }
                    else if (presion <= 0.66 && presion > 0.55)
                    {
                        s_inicial = 0.55;
                        s_final = 0.66;
                    }
                    else if (presion <= 0.55 && presion > 0.44)
                    {
                        s_inicial = 0.44;
                        s_final = 0.55;
                    }
                    else if (presion <= 0.44 && presion > 0.33)
                    {
                        s_inicial = 0.33;
                        s_final = 0.44;
                    }
                    else if (presion <= 0.33 && presion > 0.22)
                    {
                        s_inicial = 0.22 / 1000;
                        s_final = 0.33 / 1000;
                    }
                    else if (presion <= 0.22 && presion > 0.11)
                    {
                        s_inicial = 0.11;
                        s_final = 0.22;
                    }
                    else if (presion <= 0.11 && presion > 0)
                    {
                        s_inicial = 0;
                        s_final = 0.11;
                    };



                    break;
                case "mbar":

                    presion = presion / 100;

                    if (presion <= 1013.25 && presion > 900.6664)
                    {
                        s_inicial = 900.6664;
                        s_final = 1013.25;
                    }
                    else if (presion <= 900.6664 && presion > 788.0831)
                    {
                        s_inicial = 788.0831;
                        s_final = 900.6664;
                    }
                    else if (presion <= 788.0831 && presion > 675.4998)
                    {
                        s_inicial = 675.4998;
                        s_final = 788.0831;
                    }
                    else if (presion <= 675.4998 && presion > 562.9165)
                    {
                        s_inicial = 562.9165;
                        s_final = 675.4998;
                    }
                    else if (presion <= 562.9165 && presion > 450.3332)
                    {
                        s_inicial = 450.3332;
                        s_final = 562.9165;
                    }
                    else if (presion <= 450.3332 && presion > 337.7499)
                    {
                        s_inicial = 337.7499;
                        s_final = 450.3332;
                    }
                    else if (presion <= 337.7499 && presion > 225.1666)
                    {
                        s_inicial = 225.1666;
                        s_final = 337.7499;
                    }
                    else if (presion <= 225.1666 && presion > 112.5833)
                    {
                        s_inicial = 112.5833;
                        s_final = 225.1666;
                    }
                    else if (presion <= 112.5833 && presion > 0)
                    {
                        s_inicial = 0;
                        s_final = 112.5833;
                    };



                    break;


                case "Torr":


                    //Send to arduino
                    // THIS FORMAT S120,0.123,0.456,1.789
                    // S120,x,x,x
                    //string envioConFormato = "S" + presion.ToString() + "," + ObjetosGlobales.P + ","
                    //    +ObjetosGlobales.I + "," + ObjetosGlobales.D;
                    string envioConFormato = "S" + presion.ToString() + ",x,x,x";
                    lbSendPID.Text = envioConFormato;
                    serialPort1.Write(envioConFormato);
                    lbSetPointPressure.Text = presion.ToString() + " Torr";
                    break;
            }

        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            lbClientSettings.Text = Settings.Default.CodeProject + " " + Settings.Default.Customer; 

            string fecha = DateTime.Now.ToString("dddd, MM/dd/yyyy");
            lblhora.Text = DateTime.Now.ToString("hh:mm:ss tt");
            lblfecha.Text = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(fecha);

            if (EnviarPID)
            {
                EnviarPID = false;
                string PIDFormat = "Sx," + ObjetosGlobales.P + "," + ObjetosGlobales.I + "," + ObjetosGlobales.D;
                lbPIDSent.Text = PIDFormat;
                //ENVIAR
                serialPort1.Write(PIDFormat);

            }
        }



        private void IconClose_Click(object sender, EventArgs e)
        {
            FrmDontTouch permitirCerrarFormulario = Application.OpenForms.OfType<FrmDontTouch>().FirstOrDefault();
            if (permitirCerrarFormulario != null)
            {
                permitirCerrarFormulario.permitirCerrar = true;
                permitirCerrarFormulario.Close();
            }

            serialPort1.Close();
            serialPortMKS1.Close();
            serialPortMKS2.Close();
            CerrarWebCam();
            CerrarWebCam2();
            CerrarWebCam3();
            CerrarWebCam4();
            if (t != null && t.IsAlive)
            {
                t.Abort();
            }

            Application.Exit();
        }

        private void iconBar_Click(object sender, EventArgs e)
        {
            if (PanelSideNav.Width != widthSlideIcon)
            {
                PanelSideNav.Width = widthSlideIcon;
            }
            else
            {
                PanelSideNav.Width = widthSlide;
            }

        }

        private void iconTerminal_Click(object sender, EventArgs e)
        {
            Form frm = Application.OpenForms.Cast<Form>().FirstOrDefault(x => x is FrmTerminal);

            if (frm == null)
            {
                FrmTerminal nt = new FrmTerminal();
                nt.ShowDialog();
            }
            else
            {
                frm.BringToFront();
                return;
            }
        }

        private void iconPID_Click(object sender, EventArgs e)
        {
            Form frm = Application.OpenForms.Cast<Form>().FirstOrDefault(x => x is PID_Config);

            if (frm == null)
            {
                PID_Config nt = new PID_Config();

                nt.Show();
            }
            else
            {
                frm.BringToFront();
                return;
            }

        }



        //Maugoncr//
        //Evento que nos permitira mover el form
        private void PanelNav_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void IconMaxin_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                WindowState = FormWindowState.Maximized;
            }
            else if (WindowState == FormWindowState.Maximized)
            {
                WindowState = FormWindowState.Normal;
            }

        }

        private void IconMinima_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                WindowState = FormWindowState.Minimized;
            }
            else if (WindowState == FormWindowState.Maximized)
            {
                WindowState = FormWindowState.Minimized;
            }
        }

        private void IconTrace_Click(object sender, EventArgs e)
        {

            Form frm = Application.OpenForms.Cast<Form>().FirstOrDefault(x => x is Trace_Log);

            if (frm == null)
            {
                Trace_Log nt = new Trace_Log();

                nt.Show();
            }
            else
            {
                frm.BringToFront();
                return;
            }

        }

        private void IconSensor_Click(object sender, EventArgs e)
        {
            Form frm = Application.OpenForms.Cast<Form>().FirstOrDefault(x => x is Pressure_Sensor);

            if (frm == null)
            {
                Pressure_Sensor nt = new Pressure_Sensor();

                nt.Show();
            }
            else
            {
                frm.BringToFront();
                return;
            }

        }

        private void IconReport_Click(object sender, EventArgs e)
        {
            GenerateNewReportVersion();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (t != null && t.IsAlive)
            {
                t.Abort();
            }
            OffEverything();
            this.Alert("Successfully stoped", Form_Alert.enmType.Success);
        }

        private void btnInfo_Click(object sender, EventArgs e)
        {
            Form frm = Application.OpenForms.Cast<Form>().FirstOrDefault(x => x is ChooseDWG);

            if (frm == null)
            {
                ChooseDWG nt = new ChooseDWG();
                nt.ShowDialog();

            }
            else
            {
                frm.BringToFront();
                return;
            }
        }

        private void CrearArchivoContadorReportesGenerados()
        {
            string rutaArchivo = Path.Combine(rutaProyecto, "archivo.txt");

            if (File.Exists(rutaArchivo))
            {
                StreamReader lector = new StreamReader(rutaArchivo);
                string linea = lector.ReadLine(); // Leemos la línea del archivo
                lector.Close(); // Cerramos el lector
                ContadorReportes = int.Parse(linea);
            }
            else
            {
                using (StreamWriter escritor = new StreamWriter(rutaArchivo))
                {
                    escritor.Write(ContadorReportes.ToString());
                }
            }
        }

        private void AumentarContadorReportesGenerados()
        {
            string rutaArchivo = Path.Combine(rutaProyecto, "archivo.txt");

            if (File.Exists(rutaArchivo))
            {
                ContadorReportes++;
                StreamWriter escritor = new StreamWriter(rutaArchivo);
                escritor.WriteLine(ContadorReportes.ToString());
                escritor.Close();
            }
        }

        string rutaProyecto = Path.Combine(Environment.CurrentDirectory);
        public int ContadorReportes = 0;
        private void IconInfo_Click(object sender, EventArgs e)
        {

            Form frm = Application.OpenForms.Cast<Form>().FirstOrDefault(x => x is Information);
            if (frm == null)
            {
                Information nt = new Information();
                nt.interInfo = this;
                nt.ShowDialog();
            }
            else
            {
                frm.BringToFront();
                return;
            }

        }

        private void EnterBtn(Button btn)
        {
            if (btn.Enabled == true)
            {
                btn.BackgroundImage.Dispose();
                btn.BackgroundImage = MidoriValveTest.Properties.Resources.btnPress;
                btn.ForeColor = Color.White;
            }

        }

        private void LeftBtn(Button btn)
        {
            if (btn.Enabled == true)
            {
                btn.BackgroundImage.Dispose();
                btn.BackgroundImage = MidoriValveTest.Properties.Resources.btnNor;
                btn.ForeColor = Color.White;
            }
            else
            {
                btn.BackgroundImage.Dispose();
                btn.BackgroundImage = MidoriValveTest.Properties.Resources.btnDisa2;
                btn.ForeColor = Color.White;
            }
        }

        private void button3_MouseEnter(object sender, EventArgs e)
        {
            EnterBtn(btnConnect);
        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
            LeftBtn(btnConnect);
        }

        private void btnRestart_MouseEnter(object sender, EventArgs e)
        {
            EnterBtn(btnRestart);
        }

        private void btnRestart_MouseLeave(object sender, EventArgs e)
        {
            LeftBtn(btnRestart);
        }

        private void btnStop_MouseEnter(object sender, EventArgs e)
        {
            EnterBtn(btnStop);
        }

        private void btnStop_MouseLeave(object sender, EventArgs e)
        {
            LeftBtn(btnStop);
        }

        private void btn_encender_MouseEnter(object sender, EventArgs e)
        {
            EnterBtn(btnOpenGate);
        }

        private void btn_encender_MouseLeave(object sender, EventArgs e)
        {
            LeftBtn(btnOpenGate);
        }

        private void btn_apagar_MouseEnter(object sender, EventArgs e)
        {
            EnterBtn(btnCloseGate);
        }

        private void btn_apagar_MouseLeave(object sender, EventArgs e)
        {
            LeftBtn(btnCloseGate);
        }

        private void btn_valveTest_MouseEnter(object sender, EventArgs e)
        {
            EnterBtn(btn_valveTest);
        }

        private void btn_valveTest_MouseLeave(object sender, EventArgs e)
        {
            LeftBtn(btn_valveTest);
        }

        private void btnInfo_MouseEnter(object sender, EventArgs e)
        {
            EnterBtn(btnInfo);
        }

        private void btnInfo_MouseLeave(object sender, EventArgs e)
        {
            LeftBtn(btnInfo);
        }

        private void btn_set_MouseEnter(object sender, EventArgs e)
        {
            EnterBtn(btnSetApertura);
        }

        private void btn_set_MouseLeave(object sender, EventArgs e)
        {
            LeftBtn(btnSetApertura);
        }

        private void btn_S_pressure_MouseEnter(object sender, EventArgs e)
        {
            EnterBtn(btnSetPresion);
        }

        private void btn_S_pressure_MouseLeave(object sender, EventArgs e)
        {
            LeftBtn(btnSetPresion);
        }

        private void btn_90_MouseEnter(object sender, EventArgs e)
        {
            EnterBtn(btn_90);
        }

        private void btn_90_MouseLeave(object sender, EventArgs e)
        {
            LeftBtn(btn_90);
        }

        private void btn_80_MouseEnter(object sender, EventArgs e)
        {
            EnterBtn(btn_80);
        }

        private void btn_80_MouseLeave(object sender, EventArgs e)
        {
            LeftBtn(btn_80);
        }

        private void btn_70_MouseEnter(object sender, EventArgs e)
        {
            EnterBtn(btn_70);
        }

        private void btn_70_MouseLeave(object sender, EventArgs e)
        {
            LeftBtn(btn_70);
        }

        private void btn_60_MouseEnter(object sender, EventArgs e)
        {
            EnterBtn(btn_60);
        }

        private void btn_60_MouseLeave(object sender, EventArgs e)
        {
            LeftBtn(btn_60);
        }

        private void btn_50_MouseEnter(object sender, EventArgs e)
        {
            EnterBtn(btn_50);
        }

        private void btn_50_MouseLeave(object sender, EventArgs e)
        {
            LeftBtn(btn_50);
        }

        private void btn_40_MouseEnter(object sender, EventArgs e)
        {
            EnterBtn(btn_40);
        }

        private void btn_40_MouseLeave(object sender, EventArgs e)
        {
            LeftBtn(btn_40);
        }

        private void btn_30_MouseEnter(object sender, EventArgs e)
        {
            EnterBtn(btn_30);
        }

        private void btn_30_MouseLeave(object sender, EventArgs e)
        {
            LeftBtn(btn_30);
        }

        private void btn_20_MouseEnter(object sender, EventArgs e)
        {
            EnterBtn(btn_20);
        }

        private void btn_20_MouseLeave(object sender, EventArgs e)
        {
            LeftBtn(btn_20);
        }

        private void btn_10_MouseEnter(object sender, EventArgs e)
        {
            EnterBtn(btn_10);
        }

        private void btn_10_MouseLeave(object sender, EventArgs e)
        {
            LeftBtn(btn_10);
        }

        private void btn_0_MouseEnter(object sender, EventArgs e)
        {
            EnterBtn(btn_0);
        }

        private void btn_0_MouseLeave(object sender, EventArgs e)
        {
            LeftBtn(btn_0);
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            EnterBtn(btnStartRecord);
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            LeftBtn(btnStartRecord);
        }

        private void button2_MouseEnter(object sender, EventArgs e)
        {
            EnterBtn(btnStopRecord);
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            LeftBtn(btnStopRecord);
        }

        private void button7_MouseEnter(object sender, EventArgs e)
        {
            EnterBtn(btnChartArchiveAnalyzer);
        }

        private void button7_MouseLeave(object sender, EventArgs e)
        {
            LeftBtn(btnChartArchiveAnalyzer);
        }

        private void button6_MouseEnter(object sender, EventArgs e)
        {
            // EnterBtn(btnEMO);
        }

        private void button6_MouseLeave(object sender, EventArgs e)
        {
            //LeftBtn(btnEMO);
        }

        private void button5_MouseEnter(object sender, EventArgs e)
        {
            EnterBtn(btnCompareChart);
        }

        private void button5_MouseLeave(object sender, EventArgs e)
        {
            LeftBtn(btnCompareChart);
        }


        private void iconCamera_Click(object sender, EventArgs e)
        {
            Form frm = Application.OpenForms.Cast<Form>().FirstOrDefault(x => x is Forms.Camara);

            if (frm == null)
            {
                Forms.Camara nt = new Forms.Camara();
                nt.Show();

            }
            else
            {
                frm.BringToFront();
                return;
            }
        }
        private void trackBar1A_Scroll(object sender, EventArgs e)
        {
            int pos = trackBar1A.Value;

            switch (base_value)
            {
                case 0:
                    if (pos > 9)
                    {
                        trackBar1A.Value = 9;

                    }
                    break;
                case 10:
                    if (pos < 10)
                    {
                        trackBar1A.Value = 10;
                    }
                    else if (pos > 19)
                    {
                        trackBar1A.Value = 19;
                    }
                    break;
                case 20:
                    if (pos < 20)
                    {
                        trackBar1A.Value = 20;
                    }
                    else if (pos > 29)
                    {
                        trackBar1A.Value = 29;
                    }
                    break;
                case 30:
                    if (pos < 30)
                    {
                        trackBar1A.Value = 30;
                    }
                    else if (pos > 39)
                    {
                        trackBar1A.Value = 39;
                    }
                    break;
                case 40:
                    if (pos < 40)
                    {
                        trackBar1A.Value = 40;
                    }
                    else if (pos > 49)
                    {
                        trackBar1A.Value = 49;
                    }
                    break;
                case 50:
                    if (pos < 50)
                    {
                        trackBar1A.Value = 50;
                    }
                    else if (pos > 59)
                    {
                        trackBar1A.Value = 59;
                    }
                    break;
                case 60:
                    if (pos < 60)
                    {
                        trackBar1A.Value = 60;
                    }
                    else if (pos > 69)
                    {
                        trackBar1A.Value = 69;
                    }
                    break;
                case 70:
                    if (pos < 70)
                    {
                        trackBar1A.Value = 70;
                    }
                    else if (pos > 79)
                    {
                        trackBar1A.Value = 79;
                    }
                    break;
                case 80:
                    if (pos < 80)
                    {
                        trackBar1A.Value = 80;
                    }
                    else if (pos > 89)
                    {
                        trackBar1A.Value = 89;
                    }
                    break;
                case 90:
                    if (pos < 90)
                    {
                        trackBar1A.Value = 90;
                    }
                    break;


            }


            //btn_set.Enabled = true;
            btnSetApertura.Text = "Set Aperture in " + trackBar1A.Value + "°";
            //precision_aperture = trackBar1.Value;
        }


        private void trackBar2A_Scroll(object sender, EventArgs e)
        {
            EnableBtn(btnSetPresion);
            float nivel = trackBar2A.Value;


            switch (lbl_P_unit_top.Text)
            {
                case "PSI":
                    btnSetPresion.Text = "Set target pressure in " + nivel / 100;
                    break;
                case "ATM":
                    btnSetPresion.Text = "Set target pressure in " + nivel / 1000;
                    break;
                case "mbar":
                    btnSetPresion.Text = "Set target pressure in " + nivel;
                    break;
                case "Torr":
                    btnSetPresion.Text = "Set target pressure in " + nivel;
                    break;
            }
        }


        //Reset this flag
        Boolean i = false;
        string capturadatos;
        public string presionChart;
        public string temperaturaLabel;
        public string presionSetPoint;

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (i == false)
            {
                rt = 0;
                i = true;
            }
            try
            {
                if (!serialPort1.ReadLine().Contains("-"))
                {
                    if (serialPort1.ReadLine().Contains("$"))
                    {
                        //lbl_Test.Invoke(new Action(() => lbl_Test.Text = serialPort1.ReadLine().ToString()));
                        label18.Text = serialPort1.ReadLine();
                        capturadatos = serialPort1.ReadLine();
                        ObtenerData(capturadatos);
                        serialPort1.DiscardInBuffer();
                    }
                }
            }
            catch (Exception)
            {


            }
        }

        private void ResetVariablesPhases()
        {
            // Variables PHASE 2
            capturarPresionPhase2 = false;

            numOMax = 0;
            numOMin = 0;

            numUMax = 0;
            numUMin = 0;

            numDMaxP2 = 0;
            numDMinP2 = 0;

            phase2Setpoint = 0;

            overshootMaxDelta = 0;
            overshootMinDelta = 0;

            undershootMaxDelta = 0;
            undershootMinDelta = 0;

            overshootMax = 0;
            overshootMin = 0;

            undershootMax = 0;
            undershootMin = 0;

            deltaMaxP2 = 0;
            deltaMinP2 = 0;

            ptgOMax = 0;
            ptgOMin = 0;

            ptgUMax = 0;
            ptgUMin = 0;

            pressureDOMax = Double.NaN;
            pressureDOMin = Double.NaN;

            pressureDUMax = Double.NaN;
            pressureDUMin = Double.NaN;


            // Variables PHASE 3
            capturarPresionMaxMinPhase3 = false;

            cycleDeltaMax = 0;
            cycleDeltaMin = 0;
            deltaMaxPhase3 = 0;
            deltaMinPhase3 = 0;

            pressureHigh1Phase3 = 0;
            pressureLow1Phase3 = 0;

            pressureHigh2Phase3 = 0;
            pressureLow2Phase3 = 0;

            leakRate1Phase3 = 0;
            leakRate2Phase3 = 0;

            pressureDinamicMax = 0;
            pressureDinamicMin = double.MaxValue;

            grabarPresionPhase3 = false;
            timeForRecordPhase3 = 0;
            tempForRecordPhase3 = 0;

            pressuresPhase3L.Clear();
            timesPressurePhase3L.Clear();
            datetimesPhase3L.Clear();
            numberCyclePhase3L.Clear();
    }


        // Variables para cubrir todas las fases e información para el reporte!

        // Variables Phase 2
        bool capturarPresionPhase2 = false;

        int numOMax = 0;
        int numOMin = 0;

        int numUMax = 0;
        int numUMin = 0;

        int numDMaxP2 = 0;
        int numDMinP2 = 0;

        double phase2Setpoint = 0;

        double overshootMaxDelta = 0;
        double overshootMinDelta = 0;

        double undershootMaxDelta = 0;
        double undershootMinDelta = 0;

        double overshootMax = 0;
        double overshootMin = 0;

        double undershootMax = 0;
        double undershootMin = 0;

        double deltaMaxP2 = 0;
        double deltaMinP2 = 0;

        double ptgOMax = 0;
        double ptgOMin = 0;

        double ptgUMax = 0;
        double ptgUMin = 0;

        double pressureDOMax = Double.NaN;
        double pressureDOMin = Double.NaN;
       
        double pressureDUMax = Double.NaN;
        double pressureDUMin = Double.NaN;

        // Variables Phase 2 Grabaciones

        bool grabarPresionPhase2 = false;
        double timeForRecordPhase2 = 0;
        double tempForRecordPhase2 = 0;

        double timeForChartCompareRecordPhase2 = 0;
        double tempForChartCompareRecordPhase2 = 0;
       

        private List<string> pressuresPhase2L = new List<string>();
        private List<string> timesPressurePhase2L = new List<string>();
        private List<string> timesChartComparePressurePhase2L = new List<string>();
        private List<string> datetimesPhase2L = new List<string>();
        private List<string> numberCyclePhase2L = new List<string>();

        // Variables Phase 3
        bool capturarPresionMaxMinPhase3 = false;

        int cycleDeltaMax = 0;
        int cycleDeltaMin = 0;

        double deltaMaxPhase3 = 0;
        double deltaMinPhase3 = 0;

        double pressureHigh1Phase3 = 0;
        double pressureLow1Phase3 = 0;

        double pressureHigh2Phase3 = 0;
        double pressureLow2Phase3 = 0;

        double leakRate1Phase3 = 0;
        double leakRate2Phase3 = 0;

        double pressureDinamicMax = 0;
        double pressureDinamicMin = double.MaxValue;

        // Variables Phase 3 Grabaciones

        bool grabarPresionPhase3 = false;
        double timeForRecordPhase3 = 0;
        double tempForRecordPhase3 = 0;

        private List<string> pressuresPhase3L = new List<string>();
        private List<string> timesPressurePhase3L = new List<string>();
        private List<string> datetimesPhase3L = new List<string>();
        private List<string> numberCyclePhase3L = new List<string>();

        private void CalcularPhase2PerCycle(bool primerCiclo = false) 
        {
            if (primerCiclo)
            {
                numOMax = Convert.ToInt32(lbCountCycles.Text);
                numOMin = Convert.ToInt32(lbCountCycles.Text); 
                numUMax = Convert.ToInt32(lbCountCycles.Text);
                numUMin = Convert.ToInt32(lbCountCycles.Text);
                numDMaxP2 = Convert.ToInt32(lbCountCycles.Text);
                numDMinP2 = Convert.ToInt32(lbCountCycles.Text);
                overshootMax = Math.Round(pressureDOMax, 2);
                overshootMin = Math.Round(pressureDOMin, 2);
                undershootMax = Math.Round(pressureDUMax, 2);
                undershootMin = Math.Round(pressureDUMin, 2);
                deltaMaxP2 = Math.Round((overshootMax - undershootMax), 2);
                overshootMaxDelta = overshootMax;
                undershootMaxDelta = undershootMax;
                deltaMinP2 = Math.Round((overshootMin - undershootMin), 2);
                overshootMinDelta = overshootMin;
                undershootMinDelta = undershootMin;
                ptgOMax = Math.Round(((overshootMax - phase2Setpoint) / phase2Setpoint) * 100, 2);
                ptgOMin = Math.Round(((overshootMin - phase2Setpoint) / phase2Setpoint) * 100, 2);
                ptgUMax = Math.Round(((phase2Setpoint - undershootMax) / phase2Setpoint) * 100, 2);
                ptgUMin = Math.Round(((phase2Setpoint - undershootMin) / phase2Setpoint) * 100, 2);
            }
            else
            {
                double deltaDMaxP2 = Math.Round((pressureDOMax - pressureDUMax), 2);

                if (deltaDMaxP2 > deltaMaxP2 || !double.IsNaN(deltaDMaxP2))
                {
                    deltaMaxP2 = deltaDMaxP2;
                    numDMaxP2 = Convert.ToInt32(lbCountCycles.Text);
                    overshootMaxDelta = pressureDOMax;
                    undershootMaxDelta = pressureDUMax;

                
                }
                double deltaDMinP2 = Math.Round((pressureDOMin - pressureDUMin), 2);
                
                if (deltaDMinP2 < deltaMinP2 || !double.IsNaN(deltaDMinP2))
                {
                    deltaMinP2 = deltaDMinP2;
                    numDMinP2 = Convert.ToInt32(lbCountCycles.Text);
                    overshootMinDelta = pressureDOMin;
                    undershootMinDelta = pressureDUMin;
                }

                // Over and Under shooting

                if (pressureDOMax > overshootMax || !double.IsNaN(pressureDOMax) && pressureDOMax > overshootMax || !double.IsNaN(pressureDOMax) && double.IsNaN(overshootMax))
                {
                    overshootMax = pressureDOMax;
                    ptgOMax = Math.Round(((overshootMax - phase2Setpoint) / phase2Setpoint) * 100, 2);
                    numOMax = Convert.ToInt32(lbCountCycles.Text);
                }
                if (pressureDOMin < overshootMin || !double.IsNaN(pressureDOMin) && pressureDOMin > overshootMin || !double.IsNaN(pressureDOMin) && double.IsNaN(overshootMin))
                {
                    overshootMin = pressureDOMin;
                    ptgOMin = Math.Round(((overshootMin - phase2Setpoint) / phase2Setpoint) * 100, 2);
                    numOMin = Convert.ToInt32(lbCountCycles.Text);
                }
                if (pressureDUMax < undershootMax || !double.IsNaN(pressureDUMax) && pressureDUMax < undershootMax || !double.IsNaN(pressureDUMax) && double.IsNaN(undershootMax))
                {
                    undershootMax = pressureDUMax;
                    ptgUMax = Math.Round(((phase2Setpoint - undershootMax) / phase2Setpoint) * 100, 2);
                    numUMax = Convert.ToInt32(lbCountCycles.Text);
                }
                if (pressureDUMin > undershootMin || !double.IsNaN(pressureDUMin) && pressureDUMin > undershootMin || !double.IsNaN(pressureDUMin) && double.IsNaN(undershootMin))
                {
                    undershootMin = pressureDUMin;
                    ptgUMin = Math.Round(((phase2Setpoint - undershootMin) / phase2Setpoint) * 100, 2);
                    numUMin = Convert.ToInt32(lbCountCycles.Text);
                }
            }
        }

        private void CalcularPhase3PerCycle(bool primerCiclo = false) 
        {
            if (primerCiclo)
            {
                cycleDeltaMax = Convert.ToInt32(lbCountCycles.Text);
                cycleDeltaMin = Convert.ToInt32(lbCountCycles.Text);

                pressureHigh1Phase3 = Math.Round(pressureDinamicMax,2);
                pressureHigh2Phase3 = Math.Round(pressureDinamicMax,2);

                pressureLow1Phase3 = Math.Round(pressureDinamicMin,2);
                pressureLow2Phase3 = Math.Round(pressureDinamicMin, 2);

                deltaMaxPhase3 = Math.Round((pressureDinamicMax - pressureDinamicMin), 2);
                deltaMinPhase3 = Math.Round((pressureDinamicMax - pressureDinamicMin), 2);

                leakRate1Phase3 = deltaMaxPhase3 / 60000;
                leakRate2Phase3 = deltaMinPhase3 / 60000;
            }
            else
            {
                double deltaDinamica = Math.Round((pressureDinamicMax - pressureDinamicMin),2);

                if (deltaDinamica > deltaMaxPhase3)
                {
                    cycleDeltaMax = Convert.ToInt32(lbCountCycles.Text);

                    pressureHigh1Phase3 = Math.Round(pressureDinamicMax, 2);

                    pressureLow1Phase3 = Math.Round(pressureDinamicMin, 2);

                    deltaMaxPhase3 = deltaDinamica;

                    leakRate1Phase3 = deltaMaxPhase3 / 60000;
                }

                if (deltaDinamica < deltaMinPhase3)
                {
                    cycleDeltaMin = Convert.ToInt32(lbCountCycles.Text);

                    pressureHigh2Phase3 = Math.Round(pressureDinamicMax, 2);

                    pressureLow2Phase3 = Math.Round(pressureDinamicMin, 2);

                    deltaMinPhase3 = deltaDinamica;

                    leakRate2Phase3 = deltaMinPhase3 / 60000;
                }
            }
        }

        private void TimerForData_Tick(object sender, EventArgs e)
        {
            if (capturarPresionPhase2)
            {
                double pressureDinamic = Convert.ToDouble(presionChart);

                if (pressureDinamic > phase2Setpoint)
                {
                    if (double.IsNaN(pressureDOMax) || pressureDinamic > pressureDOMax)
                    {
                        pressureDOMax = pressureDinamic;
                    }
                    if (double.IsNaN(pressureDOMin) || pressureDinamic < pressureDOMin)
                    {
                        pressureDOMin = pressureDinamic;
                    }
                }
                else if (pressureDinamic < phase2Setpoint)
                {
                    if (double.IsNaN(pressureDUMax) || pressureDinamic < pressureDUMax)
                    {
                        pressureDUMax = pressureDinamic;
                    }
                    if (double.IsNaN(pressureDUMin) || pressureDinamic > pressureDUMin)
                    {
                        pressureDUMin = pressureDinamic;
                    }
                }
            }

            if (grabarPresionPhase2)
            {
                double pressureDinamicPhase2 = Convert.ToDouble(presionChart);
                int numCyclePhase2 = Convert.ToInt32(lbCountCycles.Text) + 1;

                timeForRecordPhase2 = timeForRecordPhase2 + 100;
                tempForRecordPhase2 = timeForRecordPhase2 / 1000;

                timeForChartCompareRecordPhase2 = timeForChartCompareRecordPhase2 + 100;
                tempForChartCompareRecordPhase2 = timeForChartCompareRecordPhase2 / 1000;

                pressuresPhase2L.Add(pressureDinamicPhase2.ToString());

                timesPressurePhase2L.Add(tempForRecordPhase2.ToString());
                timesChartComparePressurePhase2L.Add(tempForChartCompareRecordPhase2.ToString());

                numberCyclePhase2L.Add(numCyclePhase2.ToString());
                datetimesPhase2L.Add(DateTime.Now.ToString("MM/dd/yyyy  HH:mm:ss:ff"));
            }


            if (capturarPresionMaxMinPhase3)
            {
                double pressureDinamic = Convert.ToDouble(presionChart);

                if (pressureDinamic > pressureDinamicMax)
                {
                    pressureDinamicMax = pressureDinamic;
                }
                if (pressureDinamic < pressureDinamicMin)
                {
                    pressureDinamicMin = pressureDinamic;
                }

                if (grabarPresionPhase3)
                {
                    int numCycle = Convert.ToInt32(lbCountCycles.Text) + 1;

                    timeForRecordPhase3 = timeForRecordPhase3 + 100;
                    tempForRecordPhase3 = timeForRecordPhase3 / 1000;

                    pressuresPhase3L.Add(pressureDinamic.ToString());
                    timesPressurePhase3L.Add(tempForRecordPhase3.ToString());
                    numberCyclePhase3L.Add(numCycle.ToString());
                    datetimesPhase3L.Add(DateTime.Now.ToString("MM/dd/yyyy  HH:mm:ss:ff"));
                }
            }
            

            rt = rt + 100;
            temp = rt / 1000;

            if (serialPortMKS1.IsOpen && PedirMKS1)
            {
                serialPortMKS1.DiscardOutBuffer();
                serialPortMKS1.WriteLine("@254PR1?;FF");
            }

            if (serialPortMKS2.IsOpen && PedirMKS2)
            {
                serialPortMKS2.DiscardOutBuffer();
                serialPortMKS2.WriteLine("@254PR1?;FF");
            }

            if (serialPortMKS3.IsOpen && PedirMKS3)
            {
                serialPortMKS3.DiscardOutBuffer();
                serialPortMKS3.WriteLine("@254PR1?;FF");
            }


            if (serialPort1.IsOpen && i == true && presionChart != null && temperaturaLabel != null)
            {
                chart1.Series["Aperture value"].Points.AddXY(temp.ToString(), precision_aperture.ToString());
                chart1.Series["Pressure"].Points.AddXY(temp.ToString(), presionChart.ToString());

                lbl_pressure.Text = (presionChart);
                lb_Temperature.Text = temperaturaLabel + " °C";

                if (!string.IsNullOrEmpty(presionSetPoint))
                {
                    lbSetPointPressure.Text = presionSetPoint;
                }

                if (Auto)
                {
                    chart1.ChartAreas[0].AxisY2.Maximum = Double.NaN;
                    chart1.ChartAreas[0].AxisY2.Minimum = Double.NaN;
                    chart1.ChartAreas[0].RecalculateAxesScale();
                }
                if (Manual)
                {
                    chart1.ChartAreas[0].AxisY.Minimum = 0;
                    chart1.ChartAreas[0].AxisY.Maximum = 100;
                    chart1.ChartAreas[0].AxisY2.Minimum = 0;
                    chart1.ChartAreas[0].AxisY2.Maximum = AxisY2Maximo;
                }

            }

            if (chart1.Series["Aperture value"].Points.Count == 349)
            {

                chart1.Series["Aperture value"].Points.RemoveAt(0);
                chart1.Series["Pressure"].Points.RemoveAt(0);
            }

            if (record == true)
            {
                if (AutocalibracionPrendida == true)
                {
                    times.Add(temp.ToString());
                    apertures.Add(precision_aperture.ToString());
                    pressures.Add(presionChart.ToString());
                    datetimes.Add(DateTime.Now.ToString("hh:mm:ss:ff tt"));
                    lbl_record.Text = "Calibrating. " + "[" + temp.ToString() + "]";
                }
                else
                {
                    times.Add(temp.ToString());
                    apertures.Add(precision_aperture.ToString());
                    pressures.Add(presionChart.ToString());
                    datetimes.Add(DateTime.Now.ToString("hh:mm:ss:ff tt"));
                    lbl_record.Text = "Recording. " + "[" + temp.ToString() + "]";
                }
            }
        }

        private void btnPIDAnalisis_Click(object sender, EventArgs e)
        {
            PIDAnalize MiPidAnalisis = new PIDAnalize();
            string[] line_in_depure;
            List<string> times_1 = new List<string>();
            List<string> apertures_1 = new List<string>();
            List<string> pressures_1 = new List<string>();
            List<string> datetimes_1 = new List<string>();

            OpenFileDialog OpenFile = new OpenFileDialog();
            OpenFile.Filter = "Texto | *.txt";

            int initial_line = 0;
            string range = "";
            string[] times;
            if (OpenFile.ShowDialog() == DialogResult.OK)
            {
                string FileToRead = OpenFile.FileName;
                MiPidAnalisis.archivo = FileToRead;
                using (StreamReader sr = new StreamReader(FileToRead))
                {
                    if (Path.GetExtension(FileToRead).ToLower() == ".txt")
                    {
                        //Dado el caso, verifico que exista el archivo..
                        if (File.Exists(FileToRead))
                        {
                            //Lo ejecuto.
                            //System.Diagnostics.Process.Start(FileToRead);
                            // Creating string array  
                            string[] lines = File.ReadAllLines(FileToRead);
                            for (int i = 0; i < lines.Length; i++)
                            {

                                // Creo no lo voy a necesitar
                                //using (StreamReader tr = new StreamReader(FileToRead))
                                //{
                                //    cd.richTextBox1.Text = tr.ReadToEnd();
                                //}

                                if (lines[i].Contains("#Data Time range: ["))
                                {
                                    //MessageBox.Show(lines[i]);

                                    range = lines[i].Replace("#Data Time range: [", string.Empty);
                                    //MessageBox.Show(range);
                                    //MessageBox.Show(lines[i].Replace("#Data Time range:", string.Empty));
                                    range = range.Remove(range.Length - 1);
                                    //MessageBox.Show(range);
                                    // range = range.Remove(0);
                                    // MessageBox.Show(range);
                                    times = range.Split('-');
                                    //MessageBox.Show(times[0]);

                                    //MessageBox.Show(times[1]);
                                    MiPidAnalisis.ini_range = times[0];
                                    MiPidAnalisis.end_range = times[1];


                                }

                                if (lines[i] == "-|-  Time  -|-  Apperture  -|-  Pressure  -|-  DateTime  -|-" && lines[i + 1] == "#------------------------------------------------------------------")
                                {
                                    initial_line = i + 2;

                                    // MessageBox.Show((initial_line).ToString());
                                    //break;
                                }


                            }
                            for (int y = initial_line; y < lines.Length - 1; y++)
                            {
                                line_in_depure = lines[y].Split(',');
                                times_1.Add(line_in_depure[0]);
                                apertures_1.Add(line_in_depure[1]);
                                pressures_1.Add(line_in_depure[2]);
                                datetimes_1.Add(line_in_depure[3]);
                                Console.WriteLine(String.Join(Environment.NewLine, line_in_depure[0] + " " + line_in_depure[1] + " " + line_in_depure[2] + " " + line_in_depure[3]));

                            }


                            MiPidAnalisis.times = times_1;
                            MiPidAnalisis.apertures = apertures_1;
                            MiPidAnalisis.pressures = pressures_1;
                            MiPidAnalisis.datetimes = datetimes_1;
                            MiPidAnalisis.Show();




                        }
                        else
                        {
                            //Caso que la ruta tenga la extensión correcta, pero el archivo
                            //no exista en el disco
                            MessageBoxMaugoncr.Show("File doesn't exist.", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        //Caso de que la extensión sea incorrecta.
                        MessageBoxMaugoncr.Show("Invalid Format.", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private void btnPIDAnalisis_MouseEnter(object sender, EventArgs e)
        {
            EnterBtn(btnPIDAnalisis);
        }

        private void btnPIDAnalisis_MouseLeave(object sender, EventArgs e)
        {
            LeftBtn(btnPIDAnalisis);
        }
        public void AutoCalibrarANDRecord()
        {
            if (AutocalibracionPrendida == false)
            {
                if (MessageBoxMaugoncr.Show("Do you want to start autocalibration?, The real time graph will be reset to start.", "!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    chart1.Series["Aperture value"].Points.Clear();
                    chart1.Series["Pressure"].Points.Clear();
                    record = true;
                    rt = 0;
                    star_record = DateTime.Now;

                    precision_aperture = 0;
                    serialPort1.Write(precision_aperture.ToString());
                    base_value = 0;
                    trackBar1A.Value = 0;
                    Current_aperture.Text = trackBar1A.Value + "°";
                    lbl_record.Text = "Calibrating...";
                    AutocalibracionPrendida = true;
                    btnAutoCalibrate.Text = "Stop";
                    //ENVIA AL ARDUINO ORDEN DE ABRIR VALVULA
                    lbl_estado.ForeColor = Color.Red;
                    lbl_estado.Text = "Open";
                    //TEMAS ESTETICOS DEL SISTEMA
                    picture_frontal.Image.Dispose();
                    picture_plane.Image.Dispose();
                    picture_frontal.Image = Properties.Resources.Front0;
                    picture_plane.Image = Properties.Resources.Verti0B;

                    if (MessageBoxMaugoncr.Show("PRESS OK TO START", "!", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                    {
                        precision_aperture = 90;
                        //ENVIA AL ARDUINO ORDEN DE ABRIR VALVULA
                        serialPort1.Write(precision_aperture.ToString());
                        base_value = 90;
                        trackBar1A.Value = 90;
                        Current_aperture.Text = trackBar1A.Value + "°";
                        lbl_estado.ForeColor = Color.Red;
                        lbl_estado.Text = "Open";
                        //TEMAS ESTETICOS DEL SISTEMA
                        picture_frontal.Image.Dispose();
                        picture_plane.Image.Dispose();
                        picture_frontal.Image = Properties.Resources.Front90;
                        picture_plane.Image = Properties.Resources.Verti90B;
                    }


                }
            }
            else
            {
                precision_aperture = 0;
                Current_aperture.Text = precision_aperture + "°";
                serialPort1.Write("0");
                trackBar2A.Enabled = false;
                trackBar2A.Value = 0;
                trackBar1A.Value = 0;
                picture_frontal.Image.Dispose();
                picture_frontal.Image = Properties.Resources.Front0;
                picture_plane.Image.Dispose();
                picture_plane.Image = Properties.Resources.Verti0B;
                lbl_estado.ForeColor = Color.Red;
                lbl_estado.Text = "Close";
                btnSetPresion.Text = "Set Target Pressure";
                btnSetApertura.Text = "Set Apperture";
                EnableBtn(btnOpenGate);
                DisableBtn(btnCloseGate);
                DisableBtn(btnSetApertura);

                if (record == true)
                {
                    record = false;
                    end_record = DateTime.Now;
                    saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                    saveFileDialog1.FilterIndex = 2;
                    saveFileDialog1.RestoreDirectory = true;
                    saveFileDialog1.InitialDirectory = @"C:\";
                    saveFileDialog1.FileName = "VALVE_CALIBRATION_" + end_record.AddMilliseconds(-40).ToString("yyyy_MM_dd-hh_mm_ss");
                    saveFileDialog1.ShowDialog();

                    if (saveFileDialog1.FileName != "")
                    {
                        // Saves the Image via a FileStream created by the OpenFile method.

                        using (StreamWriter file = new StreamWriter(@"" + saveFileDialog1.FileName + ".txt"))
                        {
                            file.WriteLine("** MIDORI VALVE **");
                            file.WriteLine("#------------------------------------------------------------------");
                            file.WriteLine("#Datetime: " + star_record.ToString("yyyy/MM/dd - hh:mm:ss:ff tt"));
                            file.WriteLine("#Data Time range: [" + star_record.ToString(" hh:mm:ss:ff tt") + " - " + end_record.ToString(" hh:mm:ss:ff tt") + "]");
                            file.WriteLine("#Data |Time,seconds,[s],ChartAxisX ");
                            file.WriteLine("#Data |Apperture,grades,[°],ChartAxisY1 ");
                            file.WriteLine("#Data |Pressure,pounds per square inch,[psi],ChartAxisY2 ");
                            file.WriteLine("#------------------------------------------------------------------");
                            file.WriteLine("#PARAMETER    |Chart Type = valve record");
                            file.WriteLine("#PARAMETER    |Valve serie =");
                            file.WriteLine("#PARAMETER    |Valve Software Version =");
                            file.WriteLine("#PARAMETER    |Valve Firmware Version =");
                            file.WriteLine("#PARAMETER    |Position Unit = 0 - 90 =");

                            file.WriteLine("#------------------------------------------------------------------");
                            file.WriteLine("-|-  Time  -|-  Apperture  -|-  Pressure  -|-  DateTime  -|-");

                            file.WriteLine("#------------------------------------------------------------------");
                            for (int i = 0; i < times.Count; i++)
                            {

                                file.WriteLine(times[i] + " , " + apertures[i] + " , " + pressures[i] + " , " + datetimes[i]);

                            }
                            file.WriteLine("#------------------------------------------------------------------");
                        }
                    }

                    lbl_record.Text = "OFF";
                    AutocalibracionPrendida = false;
                    btnAutoCalibrate.Text = "Autocalibration";
                    MessageBoxMaugoncr.Show("Autocalibration data successfully saved", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
        }

        public static bool AutocalibracionPrendida = false;


        bool Inicio = true;
        private void btnStartPID_Click(object sender, EventArgs e)
        {
            if (Inicio)
            {
                serialPort1.Write("P");
                btnStartPID.Text = "Stop PID";
                Inicio = false;
            }
            else
            {
                serialPort1.Write("T");
                btnStartPID.Text = "Start PID";
                Inicio = true;
            }
        }

        private void btnStartPID_MouseEnter(object sender, EventArgs e)
        {
            EnterBtn(btnStartPID);
        }

        private void btnAutoCalibrate_Click(object sender, EventArgs e)
        {
            AutoCalibrarANDRecord();
        }

        int KnowWhichManVal = 0;
        private void btnOnMANValve_Click(object sender, EventArgs e)
        {
            //Q - Abrir solenoid 1
            //W - Cerrar solenoid 1
            //E - Abrir solenoid 2
            //R - Cerrar solenoid 2
                this.Alert("Successfully opened", Form_Alert.enmType.Success);

                //Abrir la 1 y cerrar la 2
                serialPort1.Write("Q");
               
                ManVOpen1.Image.Dispose();
                ManVOpen1.Image = Properties.Resources.led_on_green;
                ManVClose1.Image.Dispose();
                ManVClose1.Image = Properties.Resources.led_off_red;

                DisableBtn(btnOnMANValve);
                EnableBtn(btnOffMANValve);
        }

        private void btnOffMANValve_Click(object sender, EventArgs e)
        {
                this.Alert("Successfully closed", Form_Alert.enmType.Success);
                // Cerrar la 1
                serialPort1.Write("W");
                ManVOpen1.Image.Dispose();
                ManVOpen1.Image = Properties.Resources.led_off_green;
                ManVClose1.Image.Dispose();
                ManVClose1.Image = Properties.Resources.led_on_red;

                DisableBtn(btnOffMANValve);
                EnableBtn(btnOnMANValve);
        }

        private void EncenderBTN(Button btn)
        {
            if (btn.Enabled == true)
            {
                if (btn.Name == "btnOnMANValve" || btn.Name == "btnOnPump" || btn.Name == "btnOnMANValve2")
                {
                    btn.ForeColor = Color.White;
                    btnOnMANValve.IconColor = Color.White;
                    btn.BackgroundImage.Dispose();
                    btn.BackgroundImage = Properties.Resources.btnOn;
                }
                else
                {
                    btn.ForeColor = Color.White;
                    btnOffMANValve.IconColor = Color.White;
                    btn.BackgroundImage.Dispose();
                    btn.BackgroundImage = Properties.Resources.btnOff;
                }
            }
        }

        private void btnOnMANValve_MouseEnter(object sender, EventArgs e)
        {
            EncenderBTN(btnOnMANValve);
        }

        private void btnOnMANValve_MouseLeave(object sender, EventArgs e)
        {
            LeftBtn(btnOnMANValve);
        }

        private void btnOffMANValve_MouseEnter(object sender, EventArgs e)
        {
            EncenderBTN(btnOffMANValve);
        }

        private void btnOffMANValve_MouseLeave(object sender, EventArgs e)
        {
            LeftBtn(btnOffMANValve);
        }

        private void txtSetPresion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (lbl_P_unit_top.Text == "Torr")
            {
                if ((e.KeyChar >= 32 && e.KeyChar <= 47) || (e.KeyChar >= 58 && e.KeyChar <= 255))
                {
                    MessageBoxMaugoncr.Show("Only numbers are allowed", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    e.Handled = true;
                    return;
                }
            }
            else
            {
                if ((e.KeyChar >= 32 && e.KeyChar <= 45) || (e.KeyChar >= 58 && e.KeyChar <= 255) || e.KeyChar == 47)
                {
                    MessageBoxMaugoncr.Show("Only numbers are allowed", "Important", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    e.Handled = true;
                    return;
                }
            }
        }

        private void txtSetPresion_TextChanged(object sender, EventArgs e)
        {
            switch (lbl_P_unit_top.Text)
            {
                case "PSI":
                    //
                    break;
                case "ATM":
                    //
                    break;
                case "mbar":
                    //
                    break;
                case "Torr":
                    if (!string.IsNullOrEmpty(txtSetPresion.Text.Trim()))
                    {
                        int txtTorrUnit = Convert.ToInt32(txtSetPresion.Text.Trim());
                        if (txtTorrUnit <= 760)
                        {
                            trackBar2A.Value = txtTorrUnit;
                            btnSetPresion.Text = "Set target pressure in " + txtTorrUnit;
                            EnableBtn(btnSetPresion);
                        }
                        else
                        {
                            MessageBoxMaugoncr.Show("Invalide Number", "Important", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            txtSetPresion.Clear();
                            trackBar2A.Value = 0;
                            btnSetPresion.Text = "Set Target Pressure";
                            DisableBtn(btnSetPresion);
                        }
                    }
                    break;
            }
        }

        // Logica de las CAMARAS!!!

        private FilterInfoCollection MisDispositivos;

        // Duplicar como tantas camaras quieras
        private VideoCaptureDevice MiWebCam;
        public static bool offorOn = false;
        private VideoCaptureDevice MiWebCam2;
        public static bool offorOn2 = false;
        private VideoCaptureDevice MiWebCam3;
        public static bool offorOn3 = false;
        private VideoCaptureDevice MiWebCam4;
        public static bool offorOn4 = false;
        private VideoCaptureDevice MiWebCam5;
        public static bool offorOn5 = false;

        // Animation Variables
        public static int animation = 0;
        public static int aniIMG = 0;
        public static int animation2 = 0;
        public static int aniIMG2 = 0;
        public static int animation3 = 0;
        public static int aniIMG3 = 0;
        public static int animation4 = 0;
        public static int aniIMG4 = 0;
        public static int animation5 = 0;
        public static int aniIMG5 = 0;

        // Para capturas no creo usarla.
        public static Image capture;
      
        private bool Animation1 = true;
        private bool Animation2 = true;
        private bool Animation3 = true;
        private bool Animation4 = true;
        private bool Animation5 = true;

        public bool timerAnimation1 = false;
        public bool timerAnimation2 = false;
        public bool timerAnimation3 = false;
        public bool timerAnimation4 = false;
        public bool timerAnimation5 = false;


        private void TimerAnimation_Tick(object sender, EventArgs e)
        {
            if (timerAnimation1)
            {
                if (animation == 0)
                {
                    if (aniIMG == 0 && Animation1)
                    {
                        picCamara1.Image.Dispose();
                        picCamara1.Image = Properties.Resources.signal1;
                        aniIMG++;
                        Animation1 = false;
                    }
                    if (aniIMG == 1 && Animation1)
                    {
                        picCamara1.Image.Dispose();
                        picCamara1.Image = Properties.Resources.signal2;
                        aniIMG++;
                        Animation1 = false;
                    }
                    if (aniIMG == 2 && Animation1)
                    {
                        picCamara1.Image.Dispose();
                        picCamara1.Image = Properties.Resources.signal3;
                        aniIMG++;
                        Animation1 = false;
                    }
                    if (aniIMG == 3 && Animation1)
                    {
                        picCamara1.Image.Dispose();
                        picCamara1.Image = Properties.Resources.signal4;
                        aniIMG++;
                        Animation1 = false;
                    }
                    if (aniIMG == 4 && Animation1)
                    {
                        picCamara1.Image.Dispose();
                        picCamara1.Image = Properties.Resources.signal5;
                        aniIMG = 0;
                        Animation1 = false;
                    }
                }
            }
            Animation1 = true;
            if (timerAnimation2)
            {
                if (animation2 == 0)
                {
                    if (aniIMG2 == 0 && Animation2)
                    {
                        picCamara2.Image.Dispose();
                        picCamara2.Image = Properties.Resources.signal1;
                        aniIMG2++;
                        Animation2 = false;
                    }
                    if (aniIMG2 == 1 && Animation2)
                    {
                        picCamara2.Image.Dispose();
                        picCamara2.Image = Properties.Resources.signal2;
                        aniIMG2++;
                        Animation2 = false;
                    }
                    if (aniIMG2 == 2 && Animation2)
                    {
                        picCamara2.Image.Dispose();
                        picCamara2.Image = Properties.Resources.signal3;
                        aniIMG2++;
                        Animation2 = false;
                    }
                    if (aniIMG2 == 3 && Animation2)
                    {
                        picCamara2.Image.Dispose();
                        picCamara2.Image = Properties.Resources.signal4;
                        aniIMG2++;
                        Animation2 = false;
                    }
                    if (aniIMG2 == 4 && Animation2)
                    {
                        picCamara2.Image.Dispose();
                        picCamara2.Image = Properties.Resources.signal5;
                        aniIMG2 = 0;
                        Animation2 = false;
                    }
                }
            }
            Animation2 = true;
            if (timerAnimation3)
            {
                if (animation3 == 0 )
                {
                    if (aniIMG3 == 0 && Animation3)
                    {
                        picCamara3.Image.Dispose();
                        picCamara3.Image = Properties.Resources.signal1;
                        aniIMG3++;
                        Animation3 = false;
                    }
                    if (aniIMG3 == 1 && Animation3)
                    {
                        picCamara3.Image.Dispose();
                        picCamara3.Image = Properties.Resources.signal2;
                        aniIMG3++;
                        Animation3 = false;
                    }
                    if (aniIMG3 == 2 && Animation3)
                    {
                        picCamara3.Image.Dispose();
                        picCamara3.Image = Properties.Resources.signal3;
                        aniIMG3++;
                        Animation3 = false;
                    }
                    if (aniIMG3 == 3 && Animation3)
                    {
                        picCamara3.Image.Dispose();
                        picCamara3.Image = Properties.Resources.signal4;
                        aniIMG3++;
                        Animation3 = false;
                    }
                    if (aniIMG3 == 4 && Animation3)
                    {
                        picCamara3.Image.Dispose();
                        picCamara3.Image = Properties.Resources.signal5;
                        aniIMG3 = 0;
                        Animation3 = false;
                    }
                }
            }
            Animation3 = true;
            if (timerAnimation4)
            {
                if (animation4 == 0)
                {
                    if (aniIMG4 == 0 && Animation4)
                    {
                        picCamara4.Image.Dispose();
                        picCamara4.Image = Properties.Resources.signal1;
                        aniIMG4++;
                        Animation4 = false;
                    }
                    if (aniIMG4 == 1 && Animation4)
                    {
                        picCamara4.Image.Dispose();
                        picCamara4.Image = Properties.Resources.signal2;
                        aniIMG4++;
                        Animation4 = false;
                    }
                    if (aniIMG4 == 2 && Animation4)
                    {
                        picCamara4.Image.Dispose();
                        picCamara4.Image = Properties.Resources.signal3;
                        aniIMG4++;
                        Animation4 = false;
                    }
                    if (aniIMG4 == 3 && Animation4)
                    {
                        picCamara4.Image.Dispose();
                        picCamara4.Image = Properties.Resources.signal4;
                        aniIMG4++;
                        Animation4 = false;
                    }
                    if (aniIMG4 == 4 && Animation4)
                    {
                        picCamara4.Image.Dispose();
                        picCamara4.Image = Properties.Resources.signal5;
                        aniIMG4 = 0;
                        Animation4 = false;
                    }
                }
            }
            Animation4 = true;
            if (timerAnimation5)
            {
                if (animation5 == 0)
                {
                    if (aniIMG5 == 0 && Animation5)
                    {
                        picCamara5.Image.Dispose();
                        picCamara5.Image = Properties.Resources.signal1;
                        aniIMG5++;
                        Animation5 = false;
                    }
                    if (aniIMG5 == 1 && Animation5)
                    {
                        picCamara5.Image.Dispose();
                        picCamara5.Image = Properties.Resources.signal2;
                        aniIMG5++;
                        Animation5 = false;
                    }
                    if (aniIMG5 == 2 && Animation5)
                    {
                        picCamara5.Image.Dispose();
                        picCamara5.Image = Properties.Resources.signal3;
                        aniIMG5++;
                        Animation5 = false;
                    }
                    if (aniIMG5 == 3 && Animation5)
                    {
                        picCamara5.Image.Dispose();
                        picCamara5.Image = Properties.Resources.signal4;
                        aniIMG5++;
                        Animation5 = false;
                    }
                    if (aniIMG5 == 4 && Animation5)
                    {
                        picCamara5.Image.Dispose();
                        picCamara5.Image = Properties.Resources.signal5;
                        aniIMG5 = 0;
                        Animation5 = false;
                    }
                }
            }
            Animation5 = true;
        }

     

        public void CargaDiapositivosInter()
        {
            MisDispositivos = new FilterInfoCollection(FilterCategory.VideoInputDevice);
        }

        public void ActivarCam5(int cb)
        {
            if (TimerAnimation.Enabled == true)
            {
                timerAnimation5 = false;
                animation5 = 1;
            }

            if (offorOn5 == false)
            {
                CerrarWebCam5();
                int i = cb;
                string NombreVideo = MisDispositivos[i].MonikerString;
                MiWebCam5 = new VideoCaptureDevice(NombreVideo);
                MiWebCam5.NewFrame += new NewFrameEventHandler(Capturando5);
                MiWebCam5.Start();
                offorOn5 = true;
            }
            else if (offorOn5 == true)
            {
                CerrarWebCam5();
                picCamara5.Image.Dispose();
                picCamara5.Image = Properties.Resources.signal1;
                timerAnimation5 = true;
                animation5 = 0;
                aniIMG5 = 1;
                offorOn5 = false;
            }
        }

        public void ActivarCam4(int cb)
        {
            if (TimerAnimation.Enabled == true)
            {
                timerAnimation4 = false;
                animation4 = 1;
            }

            if (offorOn4 == false)
            {
                CerrarWebCam4();
                int i = cb;
                string NombreVideo = MisDispositivos[i].MonikerString;
                MiWebCam4 = new VideoCaptureDevice(NombreVideo);
                MiWebCam4.NewFrame += new NewFrameEventHandler(Capturando4);
                MiWebCam4.Start();
                offorOn4 = true;
            }
            else if (offorOn4 == true)
            {
                CerrarWebCam4();
                picCamara4.Image.Dispose();
                picCamara4.Image = Properties.Resources.signal1;
                timerAnimation4 = true;
                animation4 = 0;
                aniIMG4 = 1;
                offorOn4 = false;
            }
        }
        public void ActivarCam3(int cb)
        {
            if (TimerAnimation.Enabled == true)
            {
                timerAnimation3 = false;
                animation3 = 1;
            }
            if (offorOn3 == false)
            {
                CerrarWebCam3();
                int i = cb;
                string NombreVideo = MisDispositivos[i].MonikerString;
                MiWebCam3 = new VideoCaptureDevice(NombreVideo);
                MiWebCam3.NewFrame += new NewFrameEventHandler(Capturando3);
                MiWebCam3.Start();
                offorOn3 = true;
            }
            else if (offorOn3 == true)
            {
                CerrarWebCam3();
                picCamara3.Image.Dispose();
                picCamara3.Image = Properties.Resources.signal1;
                timerAnimation3 = true;
                animation3 = 0;
                aniIMG3 = 1;
                offorOn3 = false;
            }
        }

        public void ActivarCam2(int cb)
        {
            if (TimerAnimation.Enabled == true)
            {
                timerAnimation2 = false;
                animation2 = 1;
            }

            if (offorOn2 == false)
            {
                CerrarWebCam2();
                int i = cb;
                string NombreVideo = MisDispositivos[i].MonikerString;
                MiWebCam2 = new VideoCaptureDevice(NombreVideo);
                MiWebCam2.NewFrame += new NewFrameEventHandler(Capturando2);
                MiWebCam2.Start();
                offorOn2 = true;
            }
            else if (offorOn2 == true)
            {
                CerrarWebCam2();
                picCamara2.Image.Dispose();
                picCamara2.Image = Properties.Resources.signal1;
                timerAnimation2 = true;
                animation2 = 0;
                aniIMG2 = 1;
                offorOn2 = false;
            }
        }

        public void ActivarCam1(int cb)
        {
            if (TimerAnimation.Enabled == true)
            {
                timerAnimation1 = false;
                animation = 1;
            }

            if (offorOn == false)
            {
                CerrarWebCam();
                int i = cb;
                string NombreVideo = MisDispositivos[i].MonikerString;
                MiWebCam = new VideoCaptureDevice(NombreVideo);
                MiWebCam.NewFrame += new NewFrameEventHandler(Capturando);
                MiWebCam.Start();
                offorOn = true;
            }
            else if (offorOn == true)
            {
                CerrarWebCam();
                picCamara1.Image.Dispose();
                picCamara1.Image = Properties.Resources.signal1;
                timerAnimation1 = true;
                animation = 0;
                aniIMG = 1;
                offorOn = false;
            }
        }

        private void CerrarWebCam()
        {
            if (MiWebCam != null && MiWebCam.IsRunning)
            {
                MiWebCam.SignalToStop();
                MiWebCam = null;
            }
        }

        private void CerrarWebCam2()
        {
            if (MiWebCam2 != null && MiWebCam2.IsRunning)
            {
                MiWebCam2.SignalToStop();
                MiWebCam2 = null;
            }
        }

        private void CerrarWebCam3()
        {
            if (MiWebCam3 != null && MiWebCam3.IsRunning)
            {
                MiWebCam3.SignalToStop();
                MiWebCam3 = null;
            }
        }

        private void CerrarWebCam4()
        {
            if (MiWebCam4 != null && MiWebCam4.IsRunning)
            {
                MiWebCam4.SignalToStop();
                MiWebCam4 = null;
            }
        }

        private void CerrarWebCam5()
        {
            if (MiWebCam5 != null && MiWebCam5.IsRunning)
            {
                MiWebCam5.SignalToStop();
                MiWebCam5 = null;
            }
        }

        private void Capturando(object sender, NewFrameEventArgs eventsArgs)
        {
            Bitmap Imagen = (Bitmap)eventsArgs.Frame.Clone();
            picCamara1.Image = Imagen;
        }
        private void Capturando2(object sender, NewFrameEventArgs eventsArgs)
        {
            Bitmap Imagen = (Bitmap)eventsArgs.Frame.Clone();
            picCamara2.Image = Imagen;
        }

        private void Capturando3(object sender, NewFrameEventArgs eventsArgs)
        {
            Bitmap Imagen = (Bitmap)eventsArgs.Frame.Clone();
            picCamara3.Image = Imagen;
        }

        private void Capturando4(object sender, NewFrameEventArgs eventsArgs)
        {
            Bitmap Imagen = (Bitmap)eventsArgs.Frame.Clone();
            picCamara4.Image = Imagen;
        }
        private void Capturando5(object sender, NewFrameEventArgs eventsArgs)
        {
            Bitmap Imagen = (Bitmap)eventsArgs.Frame.Clone();
            picCamara5.Image = Imagen;
        }


        int AxisY2Maximo = 1000;
        bool Auto = false;
        bool Manual = false;

        private void torrToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Auto = false;
            Manual = true;
            AxisY2Maximo = 1000;
        }

        private void torrToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Auto = false;
            Manual = true;
            AxisY2Maximo = 500;
        }

        private void torrToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Auto = false;
            Manual = true;
            AxisY2Maximo = 100;
        }

        private void scaleAutoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Auto = true;
            Manual = false;
            AxisY2Maximo = 1000;
        }

        private void camarasSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmControlCamaras form1 = new FrmControlCamaras(this);
            form1.ShowDialog();
        }

        private void btnPIDAnalisis_MouseEnter_1(object sender, EventArgs e)
        {
            EnterBtn(btnPIDAnalisis);
        }

        private void btnPIDAnalisis_MouseLeave_1(object sender, EventArgs e)
        {
            LeftBtn(btnPIDAnalisis);
        }

        private void btnAutoCalibrate_MouseEnter(object sender, EventArgs e)
        {
            EnterBtn(btnAutoCalibrate);
        }

        private void btnAutoCalibrate_MouseLeave(object sender, EventArgs e)
        {
            LeftBtn(btnAutoCalibrate);
        }

        bool stopChrono = false;
        bool generateReport = false;
        private DateTime InicioChrono;

        //Temporizador Step by Step
        private TimeSpan tiempoSeleccionado;
        private bool runTimer = false;

        private void timerTemporizador_Tick(object sender, EventArgs e)
        {
            if (stopChrono)
            {
                EnableBtn(btn_valveTest);
                stopChrono = false;
                timerTemporizador.Stop();
                btnStopMarathon.Enabled= false;
                btnStopMarathon.BackgroundImage = Resources.StopDisable;

                FrmDontTouch permitirCerrarFormulario = Application.OpenForms.OfType<FrmDontTouch>().FirstOrDefault();
                if (permitirCerrarFormulario != null)
                {
                    permitirCerrarFormulario.permitirCerrar = true;
                    permitirCerrarFormulario.Close();
                }

                if (generateReport)
                {
                    generateReport = false;
                    GenerateNewReportVersion(TestToRun);
                    //GenerarReporte("Automatically created");
                }

                btnLimpiarTestArea.Enabled = true;
            }
            else
            {
                TimeSpan tiempoTranscurrido = DateTime.Now - InicioChrono;
                // Formateamos el tiempo transcurrido en formato de días, horas, minutos y segundos
                string tiempoFormateado = string.Format("{0:00}:{1:00}:{2:00}:{3:00}",
                    tiempoTranscurrido.Days,
                    tiempoTranscurrido.Hours,
                    tiempoTranscurrido.Minutes,
                    tiempoTranscurrido.Seconds);
                //Actualizamos el textbox con el tiempo formateado
                txtCrono.Text = tiempoFormateado;
                
                // Temporizador Step by Step

                if (runTimer)
                {
                    tiempoSeleccionado = tiempoSeleccionado.Subtract(TimeSpan.FromSeconds(1));
                    lbTemporizadorStepByStep.Text = tiempoSeleccionado.ToString(@"mm\:ss");

                    if (tiempoSeleccionado.TotalSeconds <= 0)
                    {
                        runTimer = false;
                    }
                }

            }
        }





        private void GenerateNewReportVersion(int WhoIam = 0)
        {
            AumentarContadorReportesGenerados();

            ReportDocument MiReporte = new ReportDocument();
            FrmVisualizadorCrystalReport Visualizador = new FrmVisualizadorCrystalReport();

            // Reporte creado desde fuera de las fases, intermedio o etc..
            if (WhoIam == 0)
            {
                GenerarReporte(Settings.Default.Operator);
            }
            // Reporte creado desde Fase 1
            if (WhoIam == 1)
            {

            }
            // Reporte creado desde Fase 2, PID Problem...
            if (WhoIam == 2)
            {
                MiReporte.Load("../../Reportes/RptTableMarathon.rpt");

                MiReporte.SetParameterValue("customerName", Settings.Default.Customer);
                MiReporte.SetParameterValue("personContact", Settings.Default.PersonOfContact);
                MiReporte.SetParameterValue("projectCode", Settings.Default.CodeProject);
                MiReporte.SetParameterValue("operatorName", Settings.Default.Operator);
                MiReporte.SetParameterValue("purchaseOrder", Settings.Default.PurchaseOrder);

                MiReporte.SetParameterValue("reportNumber", "TBD");

                MiReporte.SetParameterValue("startDate", DateStartedTest.Text);
                MiReporte.SetParameterValue("endDate", DateEndedTest.Text);

                // Debe verificarse que el ultimo ciclo se haya contado correctamente!
                MiReporte.SetParameterValue("totalCycles", lbCountCycles.Text);

                //Traer las variables requeridas para los demás espacios PHASE 2!

                MiReporte.SetParameterValue("phase2Setpoint",phase2Setpoint.ToString("0.00"));
                MiReporte.SetParameterValue("overshootMax", overshootMax.ToString("0.00")+" (+"+ (overshootMax - phase2Setpoint).ToString("0.0")+")");
                MiReporte.SetParameterValue("overshootMin",overshootMin.ToString("0.00") + " (+" + (overshootMin - phase2Setpoint).ToString("0.0") + ")");
                MiReporte.SetParameterValue("undershootMax",undershootMax.ToString("0.00") + " (-" + (phase2Setpoint - undershootMax).ToString("0.0") + ")");
                MiReporte.SetParameterValue("undershootMin",undershootMin.ToString("0.00") + " (-" + (phase2Setpoint - undershootMin).ToString("0.0") + ")");
                if (double.IsNaN(deltaMaxP2))
                {
                    MiReporte.SetParameterValue("deltaMaxP2", deltaMaxP2.ToString("0.00"));
                }
                else
                {
                    MiReporte.SetParameterValue("deltaMaxP2", deltaMaxP2.ToString("0.00") + " (" + overshootMaxDelta + " - " + undershootMaxDelta + ")");
                }
                if (double.IsNaN(deltaMinP2))
                {
                    MiReporte.SetParameterValue("deltaMinP2", deltaMinP2.ToString("0.00"));
                }
                else
                {
                    MiReporte.SetParameterValue("deltaMinP2", deltaMinP2.ToString("0.00") + " (" + overshootMinDelta + " - " + undershootMinDelta + ")");
                }
                MiReporte.SetParameterValue("numOMax","#"+numOMax);
                MiReporte.SetParameterValue("numOMin","#"+numOMin);
                MiReporte.SetParameterValue("numUMax","#"+numUMax);
                MiReporte.SetParameterValue("numUMin","#"+numUMin);
                MiReporte.SetParameterValue("numDMaxP2","#"+numDMaxP2);
                MiReporte.SetParameterValue("numDMinP2","#"+numDMinP2);
                MiReporte.SetParameterValue("ptgOMax","+" + ptgOMax.ToString("0.00")+"%");
                MiReporte.SetParameterValue("ptgOMin","+" + ptgOMin.ToString("0.00")+"%");
                MiReporte.SetParameterValue("ptgUMax","-" + ptgUMax.ToString("0.00")+"%");
                MiReporte.SetParameterValue("ptgUMin","-" + ptgUMin.ToString("0.00")+"%");

                MiReporte.SetParameterValue("cyclesPhase2", lbCountCycles.Text);

                MiReporte.SetParameterValue("cyclesFailP2", "Not Calculated");

                // Campos muertos por que es phase 2

                MiReporte.SetParameterValue("APMaxPhase3", "~");
                MiReporte.SetParameterValue("pressureHigh1Phase3", "~");
                MiReporte.SetParameterValue("pressureLow1Phase3", "~");
                MiReporte.SetParameterValue("leakRate1Phase3", "~");
                MiReporte.SetParameterValue("NumCyclePhase3MaxDelta", "~");
                MiReporte.SetParameterValue("APMinPhase3", "~");
                MiReporte.SetParameterValue("pressureHigh2Phase3", "~");
                MiReporte.SetParameterValue("pressureLow2Phase3", "~");
                MiReporte.SetParameterValue("leakRate2Phase3", "~");
                MiReporte.SetParameterValue("NumCyclePhase3MinDelta", "~");
                MiReporte.SetParameterValue("cyclesPhase3", "~");
                MiReporte.SetParameterValue("cyclesFailP3", "~");

                Visualizador.crystalReportViewer1.ReportSource = MiReporte;
                Visualizador.crystalReportViewer1.Zoom(85);
                Visualizador.Show();

            }
            //Reporte creado desde Fase 3, Leak test!
            if (WhoIam == 3)
            {
                MiReporte.Load("../../Reportes/RptTableMarathon.rpt");

                MiReporte.SetParameterValue("customerName", Settings.Default.Customer);
                MiReporte.SetParameterValue("personContact", Settings.Default.PersonOfContact);
                MiReporte.SetParameterValue("projectCode", Settings.Default.CodeProject);
                MiReporte.SetParameterValue("operatorName", Settings.Default.Operator);
                MiReporte.SetParameterValue("purchaseOrder", Settings.Default.PurchaseOrder);

                MiReporte.SetParameterValue("reportNumber", "TBD");

                MiReporte.SetParameterValue("startDate", DateStartedTest.Text);
                MiReporte.SetParameterValue("endDate", DateEndedTest.Text);

                // Debe verificarse que el ultimo ciclo se haya contado correctamente!
                MiReporte.SetParameterValue("totalCycles", lbCountCycles.Text);

                //Traer las variables requeridas para los demás espacios PHASE 3!

                MiReporte.SetParameterValue("APMaxPhase3", deltaMaxPhase3.ToString("0.00"));
                MiReporte.SetParameterValue("pressureHigh1Phase3", pressureHigh1Phase3.ToString("0.00"));
                MiReporte.SetParameterValue("pressureLow1Phase3", pressureLow1Phase3.ToString("0.00"));

                MiReporte.SetParameterValue("leakRate1Phase3", leakRate1Phase3.ToString("0.00E+0"));

                MiReporte.SetParameterValue("NumCyclePhase3MaxDelta", "#" + cycleDeltaMax.ToString());

                MiReporte.SetParameterValue("APMinPhase3", deltaMinPhase3.ToString("0.00"));
                MiReporte.SetParameterValue("pressureHigh2Phase3", pressureHigh2Phase3.ToString("0.00"));
                MiReporte.SetParameterValue("pressureLow2Phase3", pressureLow2Phase3.ToString("0.00"));

                MiReporte.SetParameterValue("leakRate2Phase3", leakRate2Phase3.ToString("0.00E+0"));

                MiReporte.SetParameterValue("NumCyclePhase3MinDelta", "#" + cycleDeltaMin.ToString());

                MiReporte.SetParameterValue("cyclesPhase3", lbCountCycles.Text);
                MiReporte.SetParameterValue("cyclesFailP3", "Not Calculated");


                // Variables a rellenar que no se utilizan

                MiReporte.SetParameterValue("phase2Setpoint", "~");
                MiReporte.SetParameterValue("overshootMax", "~");
                MiReporte.SetParameterValue("overshootMin", "~");
                MiReporte.SetParameterValue("undershootMax", "~");
                MiReporte.SetParameterValue("undershootMin", "~");
                MiReporte.SetParameterValue("deltaMaxP2", "~");
                MiReporte.SetParameterValue("deltaMinP2", "~");
                MiReporte.SetParameterValue("numOMax", "~");
                MiReporte.SetParameterValue("numOMin", "~");
                MiReporte.SetParameterValue("numUMax", "~");
                MiReporte.SetParameterValue("numUMin", "~");
                MiReporte.SetParameterValue("numDMaxP2", "~");
                MiReporte.SetParameterValue("numDMinP2", "~");
                MiReporte.SetParameterValue("ptgOMax", "~");
                MiReporte.SetParameterValue("ptgOMin", "~");
                MiReporte.SetParameterValue("ptgUMax", "~");
                MiReporte.SetParameterValue("ptgUMin", "~");
                MiReporte.SetParameterValue("cyclesPhase2", "~");
                MiReporte.SetParameterValue("cyclesFailP2", "~");


                Visualizador.crystalReportViewer1.ReportSource = MiReporte;
                Visualizador.crystalReportViewer1.Zoom(85);
                Visualizador.Show();
            }

        }

        private void GenerarReporte(string NombreReport) 
        {
            // Llamar el aumentador del contador
            AumentarContadorReportesGenerados();

            Bitmap screenCapture = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Graphics g = Graphics.FromImage(screenCapture);
            g.CopyFromScreen(0, 0, 0, 0, screenCapture.Size);
            string tempPath = Path.Combine(Path.GetTempPath(), "screenshot.jpg");
            screenCapture.Save(tempPath, System.Drawing.Imaging.ImageFormat.Jpeg);
            ReportDocument MiReporte = new ReportDocument();
            FrmVisualizadorCrystalReport Visualizador = new FrmVisualizadorCrystalReport();

            MiReporte.Load("../../Reportes/RptCyclesComplete.rpt");
            MiReporte.SetParameterValue("CompleteCycles", lbCountCycles.Text);
            MiReporte.SetParameterValue("GoalCycles", lbGoalCycles.Text);
            MiReporte.SetParameterValue("NameReport", NombreReport);
            MiReporte.SetParameterValue("ImagePath", tempPath);

            string k = DateStartedTest.Text;
            string j = k.Replace("\n", " - ");

            MiReporte.SetParameterValue("DateTimeStartedTest",j);

            k = DateEndedTest.Text;
            j = k.Replace("\n", " - ");

            MiReporte.SetParameterValue("DateTimeFinishTest", j);

            if (TestToRun == 1)
            {
                MiReporte.SetParameterValue("PhaseName", "[1] Pretest System Calibration");
            }
            else if (TestToRun == 2)
            {
                MiReporte.SetParameterValue("PhaseName", "[2] Stability Test");
            }
            else if (TestToRun == 3)
            {
                MiReporte.SetParameterValue("PhaseName", "[3] Valve Leak Test");
            }
            else
            {
                MiReporte.SetParameterValue("PhaseName", "Phase not selected yet");
            }

            Visualizador.crystalReportViewer1.ReportSource = MiReporte;
            Visualizador.crystalReportViewer1.Zoom(85);
            Visualizador.Show();
        }

        private void takeScreenshotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap captura = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Graphics graficos = Graphics.FromImage(captura);
            graficos.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0, captura.Size);

            // Mostrar el diálogo para guardar la captura
            SaveFileDialog dialogoGuardar = new SaveFileDialog();
            dialogoGuardar.Filter = "Imágenes PNG|*.png";
            dialogoGuardar.Title = "Guardar captura de pantalla";
            dialogoGuardar.ShowDialog();

            // Guardar la captura de pantalla en el archivo seleccionado
            if (dialogoGuardar.FileName != "")
            {
                captura.Save(dialogoGuardar.FileName, System.Drawing.Imaging.ImageFormat.Png);
            }
        }

   

        private void btnOnPump_Click(object sender, EventArgs e)
        {
            this.Alert("Pump On", Form_Alert.enmType.Success);
            serialPort1.Write("Y");
            DisableBtn(btnOnPump);
            EnableBtn(btnOffPump);
            lbPumpStatus.Text = "ON";
        }

        private void btnOffPump_Click(object sender, EventArgs e)
        {
            this.Alert("Pump Off", Form_Alert.enmType.Success);
            serialPort1.Write("U");
            DisableBtn(btnOffPump);
            EnableBtn(btnOnPump);
            lbPumpStatus.Text = "OFF";

        }

       
        private void btnOnPump_MouseEnter(object sender, EventArgs e)
        {
            EncenderBTN(btnOnPump);
        }

        private void btnOnPump_MouseLeave(object sender, EventArgs e)
        {
            LeftBtn(btnOnPump);
        }

        private void btnOffPump_MouseEnter(object sender, EventArgs e)
        {
            EncenderBTN(btnOffPump);
        }

        private void btnOffPump_MouseLeave(object sender, EventArgs e)
        {
            LeftBtn(btnOffPump);
        }

        private void btnConexionMKS_Click(object sender, EventArgs e)
        {
            int centerX = Screen.PrimaryScreen.Bounds.Width / 2;
            int centerY = Screen.PrimaryScreen.Bounds.Height / 2;
            Point centerScreen = new Point(centerX, centerY);

            panelConexionMKS.Location = new Point(centerScreen.X - panelConexionMKS.Size.Width / 2, centerScreen.Y - panelConexionMKS.Size.Height / 2);

            panelConexionMKS.Visible = true;

        }

        public void IniciarConexionMKS1(string COM)
        {
            try
            {
                if (serialPortMKS1.IsOpen)
                {
                    serialPortMKS1.Close();
                }
                serialPortMKS1.PortName = COM;
                serialPortMKS1.Open();


            }
            catch (Exception)
            {
            }
        }

        public void IniciarConexionMKS2(string COM)
        {
            try
            {
                if (serialPortMKS2.IsOpen)
                {
                    serialPortMKS2.Close();
                }

                serialPortMKS2.PortName = COM;
                serialPortMKS2.Open();

              

            }
            catch (Exception)
            {
            }
        }

      

      

        private void serialPortMKS1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (serialPortMKS1.IsOpen)
            {
                try
                {
                    Thread.Sleep(25);
                    string DataIn = serialPortMKS1.ReadExisting();
                    if (DataIn != null && DataIn != String.Empty && DataIn.Contains("F"))
                    {
                        ReadData1(DataIn);
                        serialPortMKS1.DiscardInBuffer();
                    }
                }
                catch (Exception)
                {
                }
            }
        }
        private void serialPortMKS2_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (serialPortMKS2.IsOpen)
            {
                try
                {
                    Thread.Sleep(25);
                    string DataIn = serialPortMKS2.ReadExisting();
                    if (DataIn != null && DataIn != String.Empty && DataIn.Contains("F"))
                    {
                        ReadData2(DataIn);
                        serialPortMKS2.DiscardInBuffer();
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        private void ReadData1(string t)
        {
            string Format = t;
            string Out = "";

            int startIndex = Format.IndexOf('K');
            int endIndex = Format.IndexOf(';', startIndex);

            if (startIndex >= 0 && endIndex >= 0)
            {
                Out = Format.Substring(startIndex + 1, endIndex - startIndex - 1);
            }

            double result = double.Parse(Out, System.Globalization.NumberStyles.Float);

            if (result > 10.0)
            {
                lbMKS1.Text = result.ToString("0.0");
            }
            else
            {
                lbMKS1.Text = result.ToString("0.00");
            }
        }

        private void ReadData2(string t)
        {
            string Format = t;
            string Out = "";

            int startIndex = Format.IndexOf('K');
            int endIndex = Format.IndexOf(';', startIndex);

            if (startIndex >= 0 && endIndex >= 0)
            {
                Out = Format.Substring(startIndex + 1, endIndex - startIndex - 1);
            }

            double result = double.Parse(Out, System.Globalization.NumberStyles.Float);

            if (result > 10.0)
            {
                lbMKS2.Text = result.ToString("0.0");
            }
            else
            {
                lbMKS2.Text = result.ToString("0.00");
            }
        }

        private void ReadData3(string t)
        {
            string Format = t;
            string Out = "";

            int startIndex = Format.IndexOf('K');
            int endIndex = Format.IndexOf(';', startIndex);

            if (startIndex >= 0 && endIndex >= 0)
            {
                Out = Format.Substring(startIndex + 1, endIndex - startIndex - 1);
            }

            double result = double.Parse(Out, System.Globalization.NumberStyles.Float);

            if (result > 10.0)
            {
                lbMKS3.Text = result.ToString("0.0");
            }
            else
            {
                lbMKS3.Text = result.ToString("0.00");
            }
        }

        private void btnCloseMKSConexion_Click(object sender, EventArgs e)
        {
            panelConexionMKS.Visible = false;
        }

        private void DesactivarConnectMKS(int wich) 
        {
            if (wich == 1)
            {
                btnConnectMKS1.Enabled = false;
                btnConnectMKS1.BackgroundImage.Dispose();
                btnConnectMKS1.BackgroundImage = Resources.TurnOnDisable;

                btnDisconnectMKS1.Enabled = true;
                btnDisconnectMKS1.BackgroundImage.Dispose();
                btnDisconnectMKS1.BackgroundImage = Resources.TurnOffEnable;
            }
            else if (wich == 2)
            {
                btnConnectMKS2.Enabled = false;
                btnConnectMKS2.BackgroundImage.Dispose();
                btnConnectMKS2.BackgroundImage = Resources.TurnOnDisable;

                btnDisconnectMKS2.Enabled = true;
                btnDisconnectMKS2.BackgroundImage.Dispose();
                btnDisconnectMKS2.BackgroundImage = Resources.TurnOffEnable;
            }
            else if (wich == 3)
            {
                btnConnectMKS3.Enabled = false;
                btnConnectMKS3.BackgroundImage.Dispose();
                btnConnectMKS3.BackgroundImage = Resources.TurnOnDisable;

                btnDisconnectMKS3.Enabled = true;
                btnDisconnectMKS3.BackgroundImage.Dispose();
                btnDisconnectMKS3.BackgroundImage = Resources.TurnOffEnable;
            }
        }

        private void ActivarConnectMKS(int wich)
        {
            if (wich == 1)
            {
                btnConnectMKS1.Enabled = true;
                btnConnectMKS1.BackgroundImage.Dispose();
                btnConnectMKS1.BackgroundImage = Resources.TurnOnEnable;

                btnDisconnectMKS1.Enabled = false;
                btnDisconnectMKS1.BackgroundImage.Dispose();
                btnDisconnectMKS1.BackgroundImage = Resources.TurnOffDisable;
            }
            else if (wich == 2)
            {
                btnConnectMKS2.Enabled = true;
                btnConnectMKS2.BackgroundImage.Dispose();
                btnConnectMKS2.BackgroundImage = Resources.TurnOnEnable;

                btnDisconnectMKS2.Enabled = false;
                btnDisconnectMKS2.BackgroundImage.Dispose();
                btnDisconnectMKS2.BackgroundImage = Resources.TurnOffDisable;
            }
            else if (wich == 3)
            {
                btnConnectMKS3.Enabled = true;
                btnConnectMKS3.BackgroundImage.Dispose();
                btnConnectMKS3.BackgroundImage = Resources.TurnOnEnable;

                btnDisconnectMKS3.Enabled = false;
                btnDisconnectMKS3.BackgroundImage.Dispose();
                btnDisconnectMKS3.BackgroundImage = Resources.TurnOffDisable;
            }
        }


        private void btnDisconnectMKS1_Click(object sender, EventArgs e)
        {
            cbMKS1.Enabled = true;

            string[] ports = SerialPort.GetPortNames();

            cbMKS1.Items.Clear();

            cbMKS1.Items.AddRange(ports);

            cbMKS1.SelectedIndex = -1;

            if (serialPortMKS1.IsOpen)
            {
                serialPortMKS1.Close();
            }

            lbStatusMKS1.Text = "* Disconnected";
            PedirMKS1 = false;

            btnConnectMKS1.Enabled = false;
            btnDisconnectMKS1.Enabled = false;

            btnConnectMKS1.BackgroundImage.Dispose();
            btnDisconnectMKS1.BackgroundImage.Dispose();

            btnConnectMKS1.BackgroundImage = Resources.TurnOnDisable;
            btnDisconnectMKS1.BackgroundImage = Resources.TurnOffDisable;
        }

        private void btnDisconnectMKS2_Click(object sender, EventArgs e)
        {
            cbMKS2.Enabled = true;

            string[] ports = SerialPort.GetPortNames();

            cbMKS2.Items.Clear();

            cbMKS2.Items.AddRange(ports);

            cbMKS2.SelectedIndex = -1;

            if (serialPortMKS2.IsOpen)
            {
                serialPortMKS2.Close();
            }

            lbStatusMKS2.Text = "* Disconnected";
            PedirMKS2 = false;

            btnConnectMKS2.Enabled = false;
            btnDisconnectMKS2.Enabled = false;

            btnConnectMKS2.BackgroundImage.Dispose();
            btnDisconnectMKS2.BackgroundImage.Dispose();

            btnConnectMKS2.BackgroundImage = Resources.TurnOnDisable;
            btnDisconnectMKS2.BackgroundImage = Resources.TurnOffDisable;
        }

        private void btnConnectMKS1_Click(object sender, EventArgs e)
        {
            if (cbMKS1.SelectedIndex >= 0 && btnConnectMKS1.Enabled == true)
            {
                string port = cbMKS1.SelectedItem.ToString();

                try
                {
                    if (serialPortMKS1.IsOpen)
                    {
                        serialPortMKS1.Close();

                        cbMKS1.Enabled = true;

                        string[] ports = SerialPort.GetPortNames();

                        cbMKS1.Items.Clear();

                        cbMKS1.Items.AddRange(ports);

                        cbMKS1.SelectedIndex = -1;

                        lbStatusMKS1.Text = "* Disconnected";
                        PedirMKS1 = false;

                        btnConnectMKS1.Enabled = false;
                        btnDisconnectMKS1.Enabled = false;

                        btnConnectMKS1.BackgroundImage.Dispose(); btnConnectMKS1.BackgroundImage = Resources.TurnOnDisable;
                        btnDisconnectMKS1.BackgroundImage.Dispose(); btnDisconnectMKS1.BackgroundImage = Resources.TurnOffDisable;

                        return;
                    }
                    else
                    {
                        cbMKS1.Enabled = false;

                        serialPortMKS1.PortName = port;
                        serialPortMKS1.Open();
                        lbStatusMKS1.Text = "* Connected";
                        PedirMKS1 = true;

                        DesactivarConnectMKS(1);
                    }
                }
                catch (Exception ex)
                {

                    MessageBoxMaugoncr.Show("An error occurred:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    serialPortMKS1.Close();

                    cbMKS1.Enabled = true;

                    string[] ports = SerialPort.GetPortNames();

                    cbMKS1.Items.Clear();

                    cbMKS1.Items.AddRange(ports);

                    cbMKS1.SelectedIndex = -1;

                    lbStatusMKS1.Text = "* Disconnected";
                    PedirMKS1 = false;

                    btnConnectMKS1.Enabled = false;
                    btnDisconnectMKS1.Enabled = false;

                    btnConnectMKS1.BackgroundImage.Dispose(); btnConnectMKS1.BackgroundImage = Resources.TurnOnDisable;
                    btnDisconnectMKS1.BackgroundImage.Dispose(); btnDisconnectMKS1.BackgroundImage = Resources.TurnOffDisable;
                }
            }
        }

        private void btnConnectMKS2_Click(object sender, EventArgs e)
        {
            if (cbMKS2.SelectedIndex >= 0 && btnConnectMKS2.Enabled == true)
            {
                string port = cbMKS2.SelectedItem.ToString();

                try
                {
                    if (serialPortMKS2.IsOpen)
                    {
                        serialPortMKS2.Close();

                        cbMKS2.Enabled = true;

                        string[] ports = SerialPort.GetPortNames();

                        cbMKS2.Items.Clear();

                        cbMKS2.Items.AddRange(ports);

                        cbMKS2.SelectedIndex = -1;

                        lbStatusMKS2.Text = "* Disconnected";
                        PedirMKS2 = false;

                        btnConnectMKS2.Enabled = false;
                        btnDisconnectMKS2.Enabled = false;

                        btnConnectMKS2.BackgroundImage.Dispose(); btnConnectMKS2.BackgroundImage = Resources.TurnOnDisable;
                        btnDisconnectMKS2.BackgroundImage.Dispose(); btnDisconnectMKS2.BackgroundImage = Resources.TurnOffDisable;

                        return;
                    }
                    else
                    {
                        cbMKS2.Enabled = false;

                        serialPortMKS2.PortName = port;
                        serialPortMKS2.Open();
                        lbStatusMKS2.Text = "* Connected";
                        PedirMKS2 = true;

                        DesactivarConnectMKS(2);
                    }
                }
                catch (Exception ex)
                {

                    MessageBoxMaugoncr.Show("An error occurred:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    serialPortMKS2.Close();

                    cbMKS2.Enabled = true;

                    string[] ports = SerialPort.GetPortNames();

                    cbMKS2.Items.Clear();

                    cbMKS2.Items.AddRange(ports);

                    cbMKS2.SelectedIndex = -1;

                    lbStatusMKS2.Text = "* Disconnected";
                    PedirMKS2 = false;

                    btnConnectMKS2.Enabled = false;
                    btnDisconnectMKS2.Enabled = false;

                    btnConnectMKS2.BackgroundImage.Dispose(); btnConnectMKS2.BackgroundImage = Resources.TurnOnDisable;
                    btnDisconnectMKS2.BackgroundImage.Dispose(); btnDisconnectMKS2.BackgroundImage = Resources.TurnOffDisable;
                }
            }
        }

        private void btnOnMANValve2_Click(object sender, EventArgs e)
        {
            this.Alert("Successfully opened", Form_Alert.enmType.Success);

            //Abrir la 2 y cerrar la 1
            serialPort1.Write("E");

            ManVOpen2.Image.Dispose();
            ManVOpen2.Image = Properties.Resources.led_on_green;
            ManVClose2.Image.Dispose();
            ManVClose2.Image = Properties.Resources.led_off_red;

            DisableBtn(btnOnMANValve2);
            EnableBtn(btnOffMANValve2);
        }

        private void btnOffMANValve2_Click(object sender, EventArgs e)
        {
            this.Alert("Successfully closed", Form_Alert.enmType.Success);
            // Cerrar la 2
            serialPort1.Write("R");
            ManVOpen2.Image.Dispose();
            ManVOpen2.Image = Properties.Resources.led_off_green;
            ManVClose2.Image.Dispose();
            ManVClose2.Image = Properties.Resources.led_on_red;

            DisableBtn(btnOffMANValve2);
            EnableBtn(btnOnMANValve2);
        }

        private void btnOnMANValve2_MouseEnter(object sender, EventArgs e)
        {
            EncenderBTN(btnOnMANValve2);
        }

        private void btnOnMANValve2_MouseLeave(object sender, EventArgs e)
        {
            LeftBtn(btnOnMANValve2);
        }

        private void btnOffMANValve2_MouseEnter(object sender, EventArgs e)
        {
            EncenderBTN(btnOffMANValve2);
        }

        private void btnOffMANValve2_MouseLeave(object sender, EventArgs e)
        {
            LeftBtn(btnOffMANValve2);
        }

        public string NombreReporte = "Name Unregistered";

        private string ObtenerNombreReport()
        {
            Form frm = Application.OpenForms.Cast<Form>().FirstOrDefault(x => x is FrmAskNameReport);
            if (frm == null)
            {
                FrmAskNameReport Mensajero = new FrmAskNameReport();
                Mensajero.inter = this;
                Mensajero.ShowDialog();

                if (Mensajero.DialogResult == DialogResult.OK)
                {
                    return NombreReporte;
                }
                else if (Mensajero.DialogResult == DialogResult.Cancel)
                {
                    return NombreReporte;
                }
            }

            return NombreReporte;
        }

        private (int, int) GetPositionForMouseMove(Button btn) 
        {
            Point mousePositionInForm = this.PointToClient(Control.MousePosition);
            // Obtiene la posición del mouse en relación con el botón
            Point mousePositionInButton = btn.PointToClient(mousePositionInForm);
            // Agrega un desplazamiento adicional si es necesario
            int x = mousePositionInButton.X + 10;
            int y = mousePositionInButton.Y - 10;

            return (x, y);
        }

        private void IconMinima_MouseMove(object sender, MouseEventArgs e)
        {
            Point mousePositionInForm = this.PointToClient(Control.MousePosition);
            Point mousePositionInButton = IconMinima.PointToClient(mousePositionInForm);
            int x = mousePositionInButton.X - 20;
            int y = mousePositionInButton.Y + 20;
            toolTip1.Show("Minimize", IconMinima, x,y);
        }

        private void IconMinima_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(this);
        }

        private void IconMaxin_MouseMove(object sender, MouseEventArgs e)
        {
            Point mousePositionInForm = this.PointToClient(Control.MousePosition);
            Point mousePositionInButton = IconMaxin.PointToClient(mousePositionInForm);
            int x = mousePositionInButton.X - 20;
            int y = mousePositionInButton.Y + 20;
            toolTip1.Show("Maximize", IconMaxin, x, y);
        }

        private void IconMaxin_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(this);
        }

        private void IconClose_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(this);
        }

        private void IconClose_MouseMove(object sender, MouseEventArgs e)
        {
            Point mousePositionInForm = this.PointToClient(Control.MousePosition);
            Point mousePositionInButton = IconClose.PointToClient(mousePositionInForm);
            int x = mousePositionInButton.X - 20;
            int y = mousePositionInButton.Y + 20;
            toolTip1.Show("Exit", IconClose, x, y);
        }

        private void iconTerminal_MouseMove(object sender, MouseEventArgs e)
        {
            var result = GetPositionForMouseMove(iconTerminal);
            int x = result.Item1;
            int y = result.Item2;

            toolTip1.Show("Terminal", iconTerminal, x, y);
        }

        private void iconTerminal_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(this);
        }

        private void iconPID_MouseMove(object sender, MouseEventArgs e)
        {
            var result = GetPositionForMouseMove(iconPID);
            int x = result.Item1;
            int y = result.Item2;

            toolTip1.Show("PID Config", iconPID, x, y);
        }

        private void iconPID_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(this);
        }

        private void IconReport_MouseMove(object sender, MouseEventArgs e)
        {
            var result = GetPositionForMouseMove(IconReport);
            int x = result.Item1;
            int y = result.Item2;

            toolTip1.Show("Reports", IconReport, x, y);
        }

        private void IconReport_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(this);
        }

        private void btnConexionMKS_MouseMove(object sender, MouseEventArgs e)
        {
            var result = GetPositionForMouseMove(btnConexionMKS);
            int x = result.Item1;
            int y = result.Item2;

            toolTip1.Show("Connection", btnConexionMKS, x, y);
        }

        private void btnConexionMKS_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(this);
        }

        private void btnMenu_MouseMove(object sender, MouseEventArgs e)
        {
            var result = GetPositionForMouseMove(btnMenu);
            int x = result.Item1;
            int y = result.Item2;

            toolTip1.Show("Display menu", btnMenu, x, y);
        }

        private void btnMenu_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(this);
        }

        private void IconInfo_MouseMove(object sender, MouseEventArgs e)
        {
            Point mousePositionInForm = this.PointToClient(Control.MousePosition);
            Point mousePositionInButton = IconInfo.PointToClient(mousePositionInForm);
            int x = mousePositionInButton.X + 5;
            int y = mousePositionInButton.Y - 35;
            toolTip1.Show("Information", IconInfo, x, y);
        }

        private void IconInfo_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(this);
        }

        private void iconCamera_MouseMove(object sender, MouseEventArgs e)
        {
            var result = GetPositionForMouseMove(iconCamera);
            int x = result.Item1;
            int y = result.Item2;
            toolTip1.Show("Camera", iconCamera, x, y);
        }

        private void iconCamera_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(this);
        }

        private void btn_P_conf_MouseMove(object sender, MouseEventArgs e)
        {
            var result = GetPositionForMouseMove(btn_P_conf);
            int x = result.Item1;
            int y = result.Item2;
            toolTip1.Show("Configure unit of measurement", btn_P_conf, x, y);
        }

        private void btn_P_conf_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(this);
        }

        public void EnviarSetpointFromTestCycles(string Setpoint) 
        {
            try
            {
                if (serialPort1.IsOpen)
                {
                    serialPort1.Write("S" + Setpoint + ",x,x,x");
                    lbSetPointPressure.Text = Setpoint+" Torr";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            

        }

        private void LimpiarTestArea(bool LimpiarPopUp = false) 
        {
            lbGoalCycles.Text = "0";
            lbTemporizadorStepByStep.Text = "00:00";
            lbCountCycles.Text = "0";
            txtCrono.Text = "00:00:00:00";
            DateEndedTest.Text = "-/-/-";
            DateStartedTest.Text = "-/-/-";
            lbStepForTest.BackColor = Color.WhiteSmoke;
            lbStepForTest.Text = "In non-execution";
            lb_CounterTest.Text = "0";

            if (LimpiarPopUp)
            {
                FrmDontTouch permitirCerrarFormulario = Application.OpenForms.OfType<FrmDontTouch>().FirstOrDefault();
                if (permitirCerrarFormulario != null)
                {
                    permitirCerrarFormulario.permitirCerrar = true;
                    permitirCerrarFormulario.Close();
                }
            }
          
        }

        private void btnLimpiarTestArea_Click(object sender, EventArgs e)
        {
            LimpiarTestArea(true);
            btnLimpiarTestArea.Enabled = false;
        }

        // Boton que detendrá TODO!!
        bool stillRunning = false;
        private void btnStopMarathon_Click(object sender, EventArgs e)
        {

            // Posible Change, use .Abort

            generateReport = false;

            //

            stillRunning = true;
            btnStopMarathon.Enabled = false;
            btnStopMarathon.BackgroundImage = Resources.StopDisable;
            btnLimpiarTestArea.Enabled = true;
            lbStepForTest.BackColor = Color.Red;
            lbStepForTest.Text = "TEST STOPPED";

            FrmDontTouch permitirCerrarFormulario = Application.OpenForms.OfType<FrmDontTouch>().FirstOrDefault();
            if (permitirCerrarFormulario != null)
            {
                permitirCerrarFormulario.permitirCerrar = true;
                permitirCerrarFormulario.Close();
            }

            if (serialPort1.IsOpen)
            {
                // Apagar Pump
                serialPort1.Write("U");
                Thread.Sleep(50);
                ApagarPump();

                // Cerrar Solenoid 1
                serialPort1.Write("W");
                Thread.Sleep(50);
                CerrarSolenoid_1();

                // Cerrar Solenoid 2
                serialPort1.Write("R");
                Thread.Sleep(50);
                CerrarSolenoid_2();

                // Abrir Valvula Main
                serialPort1.Write("0");
                Thread.Sleep(50);
                VisualCerrarMainV();

                // Abrir Solenoid 1
                serialPort1.Write("Q");
                Thread.Sleep(50);
                AbrirSolenoid_1();

                // Abrir Solenoid 2
                serialPort1.Write("E");
                Thread.Sleep(50);
                AbrirSolenoid_2();

                // Abrir Valvula Main
                serialPort1.Write("90");
                Thread.Sleep(50);
                VisualAbrirMainV();
            }


        }

        private void btnReloadMKS1_Click(object sender, EventArgs e)
        {

            cbMKS1.Enabled = true;

            string[] ports = SerialPort.GetPortNames();

            cbMKS1.Items.Clear();

            cbMKS1.Items.AddRange(ports);

            cbMKS1.SelectedIndex = -1;

            if (serialPortMKS1.IsOpen)
            {
                serialPortMKS1.Close();
            }

            lbStatusMKS1.Text = "* Disconnected";
            PedirMKS1 = false;

            btnConnectMKS1.Enabled = false;
            btnDisconnectMKS1.Enabled = false;

            btnConnectMKS1.BackgroundImage.Dispose();
            btnDisconnectMKS1.BackgroundImage.Dispose();

            btnConnectMKS1.BackgroundImage = Resources.TurnOnDisable;
            btnDisconnectMKS1.BackgroundImage = Resources.TurnOffDisable;

        }

        private void btnReloadMKS2_Click(object sender, EventArgs e)
        {

            cbMKS2.Enabled = true;

            string[] ports = SerialPort.GetPortNames();

            cbMKS2.Items.Clear();

            cbMKS2.Items.AddRange(ports);

            cbMKS2.SelectedIndex = -1;

            if (serialPortMKS2.IsOpen)
            {
                serialPortMKS2.Close();
            }

            lbStatusMKS2.Text = "* Disconnected";
            PedirMKS2 = false;

            btnConnectMKS2.Enabled = false;
            btnDisconnectMKS2.Enabled = false;

            btnConnectMKS2.BackgroundImage.Dispose();
            btnDisconnectMKS2.BackgroundImage.Dispose();

            btnConnectMKS2.BackgroundImage = Resources.TurnOnDisable;
            btnDisconnectMKS2.BackgroundImage = Resources.TurnOffDisable;

        }

        private void btnCompareChart_Click(object sender, EventArgs e)
        {
            Form frm = Application.OpenForms.Cast<Form>().FirstOrDefault(x => x is FrmChartSepararDataPhase2);

            if (frm == null)
            {
                FrmChartSepararDataPhase2 nt = new FrmChartSepararDataPhase2();

                nt.Show();
            }
            else
            {
                frm.BringToFront();
                return;
            }
        }

        private void serialPortMKS3_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (serialPortMKS3.IsOpen)
            {
                try
                {
                    Thread.Sleep(25);
                    string DataIn = serialPortMKS3.ReadExisting();
                    if (DataIn != null && DataIn != String.Empty && DataIn.Contains("F"))
                    {
                        ReadData3(DataIn);
                        serialPortMKS3.DiscardInBuffer();
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        private void btnReloadMKS3_Click(object sender, EventArgs e)
        {
            cbMKS3.Enabled = true;

            string[] ports = SerialPort.GetPortNames();

            cbMKS3.Items.Clear();

            cbMKS3.Items.AddRange(ports);

            cbMKS3.SelectedIndex = -1;

            if (serialPortMKS3.IsOpen)
            {
                serialPortMKS3.Close();
            }

            lbStatusMKS3.Text = "* Disconnected";
            PedirMKS3 = false;

            btnConnectMKS3.Enabled = false;
            btnDisconnectMKS3.Enabled = false;

            btnConnectMKS3.BackgroundImage.Dispose();
            btnDisconnectMKS3.BackgroundImage.Dispose();

            btnConnectMKS3.BackgroundImage = Resources.TurnOnDisable;
            btnDisconnectMKS3.BackgroundImage = Resources.TurnOffDisable;
        }

        private void btnConnectMKS3_Click(object sender, EventArgs e)
        {
            if (cbMKS3.SelectedIndex >= 0 && btnConnectMKS3.Enabled == true)
            {
                string port = cbMKS3.SelectedItem.ToString();

                try
                {
                    if (serialPortMKS3.IsOpen)
                    {
                        serialPortMKS3.Close();

                        cbMKS3.Enabled = true;

                        string[] ports = SerialPort.GetPortNames();

                        cbMKS3.Items.Clear();

                        cbMKS3.Items.AddRange(ports);

                        cbMKS3.SelectedIndex = -1;

                        lbStatusMKS3.Text = "* Disconnected";
                        PedirMKS3 = false;

                        btnConnectMKS3.Enabled = false;
                        btnDisconnectMKS3.Enabled = false;

                        btnConnectMKS3.BackgroundImage.Dispose(); btnConnectMKS3.BackgroundImage = Resources.TurnOnDisable;
                        btnDisconnectMKS3.BackgroundImage.Dispose(); btnDisconnectMKS3.BackgroundImage = Resources.TurnOffDisable;

                        return;
                    }
                    else
                    {
                        cbMKS3.Enabled = false;

                        serialPortMKS3.PortName = port;
                        serialPortMKS3.Open();
                        lbStatusMKS3.Text = "* Connected";
                        PedirMKS3 = true;

                        DesactivarConnectMKS(3);
                    }
                }
                catch (Exception ex)
                {

                    MessageBoxMaugoncr.Show("An error occurred:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    serialPortMKS2.Close();

                    cbMKS2.Enabled = true;

                    string[] ports = SerialPort.GetPortNames();

                    cbMKS2.Items.Clear();

                    cbMKS2.Items.AddRange(ports);

                    cbMKS2.SelectedIndex = -1;

                    lbStatusMKS2.Text = "* Disconnected";
                    PedirMKS2 = false;

                    btnConnectMKS2.Enabled = false;
                    btnDisconnectMKS2.Enabled = false;

                    btnConnectMKS2.BackgroundImage.Dispose(); btnConnectMKS2.BackgroundImage = Resources.TurnOnDisable;
                    btnDisconnectMKS2.BackgroundImage.Dispose(); btnDisconnectMKS2.BackgroundImage = Resources.TurnOffDisable;
                }
            }
        }

        private void btnDisconnectMKS3_Click(object sender, EventArgs e)
        {
            cbMKS3.Enabled = true;

            string[] ports = SerialPort.GetPortNames();

            cbMKS3.Items.Clear();

            cbMKS3.Items.AddRange(ports);

            cbMKS3.SelectedIndex = -1;

            if (serialPortMKS3.IsOpen)
            {
                serialPortMKS3.Close();
            }

            lbStatusMKS3.Text = "* Disconnected";
            PedirMKS3 = false;

            btnConnectMKS3.Enabled = false;
            btnDisconnectMKS3.Enabled = false;

            btnConnectMKS3.BackgroundImage.Dispose();
            btnDisconnectMKS3.BackgroundImage.Dispose();

            btnConnectMKS3.BackgroundImage = Resources.TurnOnDisable;
            btnDisconnectMKS3.BackgroundImage = Resources.TurnOffDisable;
        }

      
    }
}
