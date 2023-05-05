/// <summary>
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
using System.Drawing.Imaging;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

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

        public void Alert(string msg, Form_Alert.enmType type)
        {
            Form_Alert frm = new Form_Alert();
            frm.showAlert(msg, type);
        }




        // Funcion de carga de procedimientos iniciales (inicio automatico). 
        private void Form1_Load(object sender, EventArgs e)
        {
            OffEverything();
            timer1.Enabled = true;
            TimerAnimation.Start();
            TimerAnimation2.Start();
            TimerAnimation3.Start();
            TimerAnimation4.Start();
            TimerAnimation5.Start();
            if (cbSelectionCOM.Items.Count > 0)     // exist ports com
            {
                cbSelectionCOM.SelectedIndex = 0;
                EnableBtn(btnConnect);
            }
        }

        private void OffEverything()
        {
            if (serialPort1.IsOpen)
            {
                serialPort1.Close();
            }

            //Timers
            timerForData.Stop();

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
            string[] ports = SerialPort.GetPortNames();
            cbSelectionCOM.Items.Clear();
            cbSelectionCOM.Items.AddRange(ports);

            //Enable Buttons

            //Disable Buttons
            DisableBtn(btnOpenGate);
            DisableBtn(btnCloseGate);
            DisableBtn(btnSetApertura);
            DisableBtn(btnSetPresion);
            DisableBtn(btnStartPID);
            DisableBtn(btnStartRecord);
            DisableBtn(btnStopRecord);
            DisableBtn(btnChartArchiveAnalyzer);
            DisableBtn(btnAnalyze);
            DisableBtn(btnPIDAnalisis);
            DisableBtn(btnAutoCalibrate);
            DisableBtn(btnEMO);
            DisableBtn(btnConnect);
            DisableBtn(btnInfo);
            DisableBtn(btn_valveTest);

            // Man Val Controls
            DisableBtn(btnOffMANValve);
            DisableBtn(btnOnMANValve);
            cbManValve.Enabled = false;
            cbManValve.SelectedIndex = -1;

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

        }


        // Metodo para cambiar para remplazar el enable disable button y usar uno general

        private void DisableBtn(Button btn)
        {

            btn.BackgroundImage.Dispose();
            btn.BackgroundImage = MidoriValveTest.Properties.Resources.btnDisa2;
            btn.Enabled = false;
            btn.ForeColor = Color.White;

        }

        private void EnableBtn(Button btn)
        {
            btn.BackgroundImage.Dispose();
            btn.BackgroundImage = MidoriValveTest.Properties.Resources.btnNor;
            btn.Enabled = true;
        }

        private void EnableBtnEMO(Button btn)
        {
            btn.BackgroundImage.Dispose();
            btn.BackgroundImage = Properties.Resources.btnOff;
            btn.Enabled = true;
        }


        //Maugoncr// Nos permite comprobar que en caso de que al iniciar la carga del form no habia ningun com para reconocer, en caso de reconocerse luego de esta
        // ser capaces de activar el boton Connect


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbSelectionCOM.SelectedIndex >= 0)
            {
                EnableBtn(btnConnect);

            }
            else
            {
                DisableBtn(btnConnect);
            }
        }



        //Maugoncr// 
        // Reboot the whole system as when it started up

        private void btnRestart_Click(object sender, EventArgs e)
        {
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
                    cbManValve.Enabled = true;
                    cbManValve.SelectedIndex = 0;

                    EnableBtn(btnOpenGate);
                    btn_P_conf.Enabled = true;
                    EnableBtn(btn_valveTest);
                    cbSelectionCOM.Enabled = false;
                    DisableBtn(btnConnect);
                    EnableBtn(btnStartPID);
                    EnableBtn(btnOnMANValve);
                    EnableBtn(btnOffMANValve);
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
                    EnableBtn(btnAnalyze);
                    EnableBtn(btnAutoCalibrate);
                    EnableBtn(btnPIDAnalisis);
                    EnableBtnEMO(btnEMO);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
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
            catch (Exception)
            {
                LblEstado.Text = "Disconnected *";
                lblPuerto.Text = "Disconnected *";
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
            EnableBtn(btnEMO);
            EnableBtn(btnAnalyze);
            //stop
            EnableBtn(btnStopRecord);
            // grabar
            EnableBtn(btnStartRecord);

        }

        private void btn_apagar_Click(object sender, EventArgs e)
        {

            serialPort1.Write("0");
            Thread.Sleep(50);


            //esperamos la señal de movimeinto de partura
            //while (respuesta != "B")
            //{
            //    respuesta = Arduino.ReadExisting(); //MessageBox.Show(respuesta);
            //    Thread.Sleep(50);
            //}
            //trackBar1.Enabled = false;
            trackBar2A.Enabled = false;
            trackBar2A.Value = 0;
            trackBar1A.Value = 0;
            precision_aperture = 0;
            Current_aperture.Text = precision_aperture + "°";
            picture_frontal.Image.Dispose();
            picture_frontal.Image = MidoriValveTest.Properties.Resources._0_2;
            picture_plane.Image.Dispose();
            picture_plane.Image = MidoriValveTest.Properties.Resources._0_GRADOS2;
            precision_aperture = 0;
            lbl_estado.ForeColor = Color.Red;
            lbl_estado.Text = "Close";
            btnSetPresion.Text = "Set Target Pressure";
            btnSetApertura.Text = "Set Apperture";
            //btn_encender.Enabled = true;
            EnableBtn(btnOpenGate);

            //btn_apagar.Enabled = false;
            DisableBtn(btnCloseGate);

            //btn_90.Enabled = false;
            //btn_80.Enabled = false;
            //btn_70.Enabled = false;
            //btn_60.Enabled = false;
            //btn_50.Enabled = false;
            //btn_40.Enabled = false;
            //btn_30.Enabled = false;
            //btn_20.Enabled = false;
            //btn_10.Enabled = false;
            //btn_0.Enabled = false;

            //btn_set.Enabled=false;
            DisableBtn(btnSetApertura);

        }

        private void btn_valveTest_Click(object sender, EventArgs e)
        {
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

        public int TestToRun = 0;
        public int NumTest = 1;

        public void NewThreadForTest()
        {
            InicioChrono = DateTime.Now;

            if (TestToRun == 1)
            {
                minutos = 7;
                segundos = 14;
                //minutos = 0;
                //segundos = 10;
            }
            else if (TestToRun == 2)
            {
                minutos = 5;
                segundos = 10;
            }
            else if (true)
            {
                minutos = 2;
                segundos = 12;
            }

            lbGoalCycles.Text = NumTest.ToString();

            timerTemporizador.Start();

            Thread t = new Thread(new ThreadStart(EjecutarTest));
            t.Start();
            DisableBtn(btn_valveTest);
        }

        private void AbrirSolenoid_1() 
        {
            ManVOpen1.Image.Dispose();
            ManVOpen1.Image = Properties.Resources.led_on_green;
            ManVClose1.Image.Dispose();
            ManVClose1.Image = Properties.Resources.led_off_red;
        }

        private void CerrarSolenoid_1()
        {
            ManVOpen1.Image.Dispose();
            ManVOpen1.Image = Properties.Resources.led_off_green;
            ManVClose1.Image.Dispose();
            ManVClose1.Image = Properties.Resources.led_on_red;
        }

        private void AbrirSolenoid_2()
        {
            ManVOpen2.Image.Dispose();
            ManVOpen2.Image = Properties.Resources.led_on_green;
            ManVClose2.Image.Dispose();
            ManVClose2.Image = Properties.Resources.led_off_red;
        }

        private void CerrarSolenoid_2()
        {
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
            picture_frontal.Image = MidoriValveTest.Properties.Resources.Verti0B;
            picture_plane.Image = MidoriValveTest.Properties.Resources.Front0;
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
            picture_frontal.Image = MidoriValveTest.Properties.Resources.Verti90B;
            picture_plane.Image = MidoriValveTest.Properties.Resources.Front90;
            base_value = 90;
            trackBar1A.Value = 90;
            precision_aperture = 90;
            DisableBtn(btnOpenGate);
            EnableBtn(btnCloseGate);
        }

        private void EjecutarTest()
        {
            //Q - Abrir solenoid 1
            //W - Cerrar solenoid 1
            //E - Abrir solenoid 2
            //R - Cerrar solenoid 2
            //Y - Encender Pump
            //U - Apagar Pump

            if (TestToRun == 1)
            {
                // Apagar Pump
                serialPort1.Write("U");
                Thread.Sleep(50);

                // Cierra Solenoid 1
                serialPort1.Write("W");
                Thread.Sleep(50);
                CerrarSolenoid_1();

                // Cierra Solenoid 2
                serialPort1.Write("R");
                Thread.Sleep(50);
                CerrarSolenoid_2();

                // Cierra Valvula Main
                serialPort1.Write("90");
                Thread.Sleep(50);
                VisualAbrirMainV();

                DateStartedTest.Text = DateTime.Now.ToString("MM/dd/yy\nhh:mm:ss tt");
                for (int i = 1; i <= NumTest; i++)
                {

                    serialPort1.Write("0");
                    lbStepForTest.Text = "Open [BCV40]";
                    VisualCerrarMainV();
                    Thread.Sleep(2000);

                    serialPort1.Write("Q");
                    lbStepForTest.Text = "Open [PN ISO-V1]";
                    AbrirSolenoid_1();
                    Thread.Sleep(2000);

                    serialPort1.Write("Y");
                    lbStepForTest.Text = "On [PUMP]";
                    Thread.Sleep(2000);

                    lbStepForTest.Text = "Waiting down to 1 torr";
                    Thread.Sleep(120000);


                    serialPort1.Write("W");
                    lbStepForTest.Text = "Close [PN ISO-V1]";
                    CerrarSolenoid_1();
                    Thread.Sleep(2000);

                    serialPort1.Write("U");
                    lbStepForTest.Text = "Off [PUMP]";
                    Thread.Sleep(2000);

                    lbStepForTest.Text = "Verify leak for 5 min";
                    Thread.Sleep(300000);

                    serialPort1.Write("Q");
                    lbStepForTest.Text = "Open [PN ISO-V1]";
                    AbrirSolenoid_1();
                    Thread.Sleep(2000);

                    serialPort1.Write("E");
                    lbStepForTest.Text = "Open [PN ISO-V2]";
                    AbrirSolenoid_2();
                    Thread.Sleep(2000);
                    

                }
                DateEndedTest.Text = DateTime.Now.ToString("MM/dd/yy\nhh:mm:ss tt");
                lbStepForTest.Text = "Phase 1 Finished";
            }
            else if (TestToRun == 2)
            {

                DateStartedTest.Text = DateTime.Now.ToString("MM/dd/yy\nhh:mm:ss tt");

                // Apagar Pump
                serialPort1.Write("U");
                Thread.Sleep(50);
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

                for (int i = 1; i <= NumTest; i++)
                {
                    // Cierra Valvula Main
                    serialPort1.Write("0");
                    lbStepForTest.Text = "Close [BCV40]";
                    VisualCerrarMainV();
                    Thread.Sleep(2000);

                    serialPort1.Write("R");
                    lbStepForTest.Text = "Close [PN ISO-V2]";
                    CerrarSolenoid_2();
                    Thread.Sleep(2000);

                    serialPort1.Write("Y");
                    lbStepForTest.Text = "On [PUMP]";
                    Thread.Sleep(2000);

                    //Esperamos down to 1 Torr
                    lbStepForTest.Text = "Waiting down to 1 torr";
                    Thread.Sleep(120000);

                    // Viene el dilema del PID
                    // Ya debe tener el preset de la presion 500 tor...
                    //Activa el PID y espera 3 min
                    serialPort1.Write("P");
                    lbStepForTest.Text = "STABILITY TEST";
                    Thread.Sleep(180000);

                    //Apaga el Pump
                    serialPort1.Write("U");
                    lbStepForTest.Text = "Off [PUMP]";
                    Thread.Sleep(2000);

                    serialPort1.Write("E");
                    lbStepForTest.Text = "Open [PN ISO-V2]";
                    AbrirSolenoid_2();
                    Thread.Sleep(2000);

                }
                DateEndedTest.Text = DateTime.Now.ToString("MM/dd/yy\nhh:mm:ss tt");
                lbStepForTest.Text = "Phase 2 Finished";
            }
            else if (TestToRun == 3)
            {
                DateStartedTest.Text = DateTime.Now.ToString("MM/dd/yy\nhh:mm:ss tt");

                // Apagar Pump
                serialPort1.Write("U");
                Thread.Sleep(50);
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

                for (int i = 0; i <= NumTest; i++)
                {
                    //Cierra la valvula normal
                    serialPort1.Write("0");
                    lbStepForTest.Text = "Close [BCV40]";
                    VisualCerrarMainV();
                    Thread.Sleep(2000);

                    //Cierra solenoid 2
                    serialPort1.Write("R");
                    lbStepForTest.Text = "Close [PN ISO-V2]";
                    CerrarSolenoid_2();
                    Thread.Sleep(2000);

                    //Enciende el pump
                    serialPort1.Write("Y");
                    lbStepForTest.Text = "On [PUMP]";
                    Thread.Sleep(2000);

                    //Esperamos down to 1 Torr
                    lbStepForTest.Text = "Waiting down to 1 torr";
                    Thread.Sleep(120000);

                    // Cerrar Solenoid 1
                    serialPort1.Write("W");
                    lbStepForTest.Text = "Open [PN ISO-V1]";
                    CerrarSolenoid_1();
                    Thread.Sleep(2000);

                    //Se apaga el pump
                    serialPort1.Write("U");
                    lbStepForTest.Text = "Off [PUMP]";
                    Thread.Sleep(2000);

                    //Se verifica el Leak por 1 minuto
                    lbStepForTest.Text = "Verify leak for 1 min";
                    Thread.Sleep(60000);

                    //Se abre todo nuevamente
                    // Abre solenoid 1
                    serialPort1.Write("Q");
                    lbStepForTest.Text = "Open [PN ISO-V1]";
                    AbrirSolenoid_1();
                    Thread.Sleep(2000);

                    //Abre solenoid 2
                    serialPort1.Write("E");
                    lbStepForTest.Text = "Open [PN ISO-V2]";
                    AbrirSolenoid_2();
                    Thread.Sleep(2000);

                    //Abre valvula 90°
                    serialPort1.Write("90");
                    lbStepForTest.Text = "Open [BCV40]";
                    VisualAbrirMainV();
                    Thread.Sleep(2000);

                }
                DateEndedTest.Text = DateTime.Now.ToString("MM/dd/yy\nhh:mm:ss tt");
                lbStepForTest.Text = "Phase 3 Finished";
            }
            EnableBtn(btn_valveTest);
        }


        private void btn_0_Click(object sender, EventArgs e)
        {
            picture_frontal.Image.Dispose();
            picture_plane.Image.Dispose();
            picture_frontal.Image = MidoriValveTest.Properties.Resources.Verti0B;
            picture_plane.Image = MidoriValveTest.Properties.Resources.Front0;
            base_value = 0;
            trackBar1A.Value = 0;
            // precision_aperture = 0;
            Current_aperture.Text = trackBar1A.Value + "°";
            btnSetApertura.Text = "Set Aperture";
            //btn_set.Enabled = true;
            //lbl_estado.ForeColor = Color.Red;
            //lbl_estado.Text = "Close";
            if (lbl_estado.Text == "Open")
            {
                //btn_set.Enabled = true;
                EnableBtn(btnSetApertura);
            }

        }

        private void btn_10_Click(object sender, EventArgs e)
        {
            picture_frontal.Image.Dispose();
            picture_plane.Image.Dispose();
            picture_frontal.Image = MidoriValveTest.Properties.Resources.Verti10B;
            picture_plane.Image = MidoriValveTest.Properties.Resources.Front10;
            base_value = 10;
            trackBar1A.Value = 10;
            // precision_aperture = 10;
            Current_aperture.Text = trackBar1A.Value + "°";

            //btn_set.Enabled = true;
            //lbl_estado.ForeColor = Color.Green;
            //lbl_estado.Text = "Open";
            if (lbl_estado.Text == "Open")
            {
                //btn_set.Enabled = true;
                EnableBtn(btnSetApertura);
                btnSetApertura.Text = "Set Aperture in 10";
            }
        }

        private void btn_20_Click(object sender, EventArgs e)
        {
            picture_frontal.Image.Dispose();
            picture_plane.Image.Dispose();
            picture_frontal.Image = MidoriValveTest.Properties.Resources.Verti20B;
            picture_plane.Image = MidoriValveTest.Properties.Resources.Front20;
            base_value = 20;
            trackBar1A.Value = 20;
            // precision_aperture = 20;
            Current_aperture.Text = trackBar1A.Value + "°";

            //btn_set.Enabled = true;
            //lbl_estado.ForeColor = Color.Green;
            //lbl_estado.Text = "Open";

            if (lbl_estado.Text == "Open")
            {
                //btn_set.Enabled = true;
                EnableBtn(btnSetApertura);
                btnSetApertura.Text = "Set Aperture in 20";
            }
        }

        private void btn_30_Click(object sender, EventArgs e)
        {
            picture_frontal.Image.Dispose();
            picture_plane.Image.Dispose();
            picture_frontal.Image = MidoriValveTest.Properties.Resources.Verti30B;
            picture_plane.Image = MidoriValveTest.Properties.Resources.Front30;
            base_value = 30;
            trackBar1A.Value = 30;
            //precision_aperture = 30;
            Current_aperture.Text = trackBar1A.Value + "°";

            //btn_set.Enabled = true;
            //lbl_estado.ForeColor = Color.Green;
            //lbl_estado.Text = "Open";
            if (lbl_estado.Text == "Open")
            {
                //btn_set.Enabled = true;
                EnableBtn(btnSetApertura);
                btnSetApertura.Text = "Set Aperture in 30";

            }
        }

        private void btn_40_Click(object sender, EventArgs e)
        {
            picture_frontal.Image.Dispose();
            picture_plane.Image.Dispose();
            picture_frontal.Image = MidoriValveTest.Properties.Resources.Verti40B;
            picture_plane.Image = MidoriValveTest.Properties.Resources.Front40;
            base_value = 40;
            trackBar1A.Value = 40;
            //precision_aperture = 40;
            Current_aperture.Text = trackBar1A.Value + "°";

            //btn_set.Enabled = true;
            //lbl_estado.ForeColor = Color.Green;
            //lbl_estado.Text = "Open";
            if (lbl_estado.Text == "Open")
            {
                //btn_set.Enabled = true;
                EnableBtn(btnSetApertura);
                btnSetApertura.Text = "Set Aperture in 40";
            }
        }

        private void btn_50_Click(object sender, EventArgs e)
        {
            picture_frontal.Image.Dispose();
            picture_plane.Image.Dispose();
            picture_frontal.Image = MidoriValveTest.Properties.Resources.Verti50B;
            picture_plane.Image = MidoriValveTest.Properties.Resources.Front50;
            base_value = 50;
            trackBar1A.Value = 50;
            //precision_aperture = 50;
            Current_aperture.Text = trackBar1A.Value + "°";

            //btn_set.Enabled = true;
            //lbl_estado.ForeColor = Color.Green;
            //lbl_estado.Text = "Open";
            if (lbl_estado.Text == "Open")
            {
                //btn_set.Enabled = true;
                EnableBtn(btnSetApertura);
                btnSetApertura.Text = "Set Aperture in 50";
            }

        }

        private void btn_60_Click(object sender, EventArgs e)
        {
            picture_frontal.Image.Dispose();
            picture_plane.Image.Dispose();
            picture_frontal.Image = MidoriValveTest.Properties.Resources.Verti60B;
            picture_plane.Image = MidoriValveTest.Properties.Resources.Front60;
            base_value = 60;
            trackBar1A.Value = 60;
            //precision_aperture = 60;
            Current_aperture.Text = trackBar1A.Value + "°";

            //btn_set.Enabled = true;
            //lbl_estado.ForeColor = Color.Green;
            //lbl_estado.Text = "Open";

            if (lbl_estado.Text == "Open")
            {
                //btn_set.Enabled = true;
                EnableBtn(btnSetApertura);
                btnSetApertura.Text = "Set Aperture in 60";
            }
        }

        private void btn_70_Click(object sender, EventArgs e)
        {
            picture_frontal.Image.Dispose();
            picture_plane.Image.Dispose();
            picture_frontal.Image = MidoriValveTest.Properties.Resources.Verti70B;
            picture_plane.Image = MidoriValveTest.Properties.Resources.Front70;
            base_value = 70;
            trackBar1A.Value = 70;
            //precision_aperture = 70;
            Current_aperture.Text = trackBar1A.Value + "°";

            //btn_set.Enabled = true;
            //lbl_estado.ForeColor = Color.Green;
            //lbl_estado.Text = "Open";

            if (lbl_estado.Text == "Open")
            {
                //btn_set.Enabled = true;
                EnableBtn(btnSetApertura);
                btnSetApertura.Text = "Set Aperture in 70";
            }
        }

        private void btn_80_Click(object sender, EventArgs e)
        {
            picture_frontal.Image.Dispose();
            picture_plane.Image.Dispose();
            picture_frontal.Image = MidoriValveTest.Properties.Resources.Verti80B;
            picture_plane.Image = MidoriValveTest.Properties.Resources.Front80;
            base_value = 80;
            trackBar1A.Value = 80;
            //precision_aperture = 80;
            Current_aperture.Text = trackBar1A.Value + "°";

            //btn_set.Enabled = true;
            //lbl_estado.ForeColor = Color.Green;
            //lbl_estado.Text = "Open";
            if (lbl_estado.Text == "Open")
            {
                //btn_set.Enabled = true;
                EnableBtn(btnSetApertura);
                btnSetApertura.Text = "Set Aperture in 80";
            }

        }

        private void btn_90_Click(object sender, EventArgs e)
        {
            picture_frontal.Image.Dispose();
            picture_plane.Image.Dispose();
            picture_frontal.Image = MidoriValveTest.Properties.Resources.Verti90B;
            picture_plane.Image = MidoriValveTest.Properties.Resources.Front90;
            base_value = 90;
            trackBar1A.Value = 90;
            //precision_aperture = 90;
            Current_aperture.Text = trackBar1A.Value + "°";


            //lbl_estado.ForeColor = Color.Green;
            //lbl_estado.Text = "Open";
            //btn_set.Enabled = true;

            if (lbl_estado.Text == "Open")
            {
                btnSetApertura.Enabled = true;
                btnSetApertura.Text = "Set Aperture in 90";
            }

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

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

        private void button5_Click(object sender, EventArgs e)
        {

            Chart_Analyzer ca = new Chart_Analyzer();
            ca.final_time = final;
            ca.date = n;
            for (int i = 0; i < chart1.Series["Aperture value"].Points.Count; i++)
            {
                ca.chart1.Series["Aperture value"].Points.Add(chart1.Series["Aperture value"].Points[i]);
                ca.chart1.Series["Pressure"].Points.Add(chart1.Series["Pressure"].Points[i]);
            }
            ca.ShowDialog();

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

        private void button8_Click(object sender, EventArgs e)
        {
            Terminal nt = new Terminal();
            LateralNav.Size = new Size(0, 1019);
            nt.lblPuerto.Text = "Connected";
            nt.mvt = this;
            nt.Arduino = serialPort1;
            nt.ShowDialog();

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
                    lbSetPointPressure.Text = presion.ToString();
                    break;
            }


            //pressure_get = trackBar2.Value;
            //lbl_pressure.Text = "Current Pressure:" + pressure_get + "°";
            //btn_S_pressure.Text = "Set Pressure";
            //btn_S_pressure.Enabled = false;
            //DisableBtn(btn_S_pressure);
            //// lbl_estado.ForeColor = Color.Green;
            //// lbl_estado.Text = "Open";
            //Arduino.Write(pressure_get.ToString());


        }


        private void timer1_Tick(object sender, EventArgs e)
        {
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
            serialPort1.Close();
            Application.Exit();
            CerrarWebCam();
            CerrarWebCam2();
            CerrarWebCam3();
            CerrarWebCam4();
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
            Form frm = Application.OpenForms.Cast<Form>().FirstOrDefault(x => x is Terminal);

            if (frm == null)
            {
                Terminal nt = new Terminal();
                nt.lblPuerto.Text = "Connected";
                nt.mvt = this;
                //  nt.Arduino = Arduino;
                nt.Show();
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
            GenerarReporte();

            //Form frm = Application.OpenForms.Cast<Form>().FirstOrDefault(x => x is Report);
            //if (frm == null)
            //{
            //    Report nt = new Report();
            //    nt.Show();
            //}
            //else
            //{
            //    frm.BringToFront();
            //    return;
            //}
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
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

        private void IconInfo_Click(object sender, EventArgs e)
        {
            Form frm = Application.OpenForms.Cast<Form>().FirstOrDefault(x => x is Information);

            if (frm == null)
            {
                Information nt = new Information();
                nt.Show();

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
            EnterBtn(btnAnalyze);
        }

        private void button5_MouseLeave(object sender, EventArgs e)
        {
            LeftBtn(btnAnalyze);
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


        private void TimerForData_Tick(object sender, EventArgs e)
        {
            rt = rt + 100;
            temp = rt / 1000;


            if (serialPort1.IsOpen && i == true && presionChart != null && temperaturaLabel != null)
            {
                chart1.Series["Aperture value"].Points.AddXY(temp.ToString(), precision_aperture.ToString());
                chart1.Series["Pressure"].Points.AddXY(temp.ToString(), presionChart.ToString());

                lbl_pressure.Text = (presionChart);
                lb_Temperature.Text = temperaturaLabel + " °C";

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

            if (KnowWhichManVal == 1)
            {
                this.Alert("Successfully opened", Form_Alert.enmType.Success);

                //Abrir la 1 y cerrar la 2
                serialPort1.Write("Q");
               
                ManVOpen1.Image.Dispose();
                ManVOpen1.Image = Properties.Resources.led_on_green;
                ManVClose1.Image.Dispose();
                ManVClose1.Image = Properties.Resources.led_off_red;
                ManVOpen2.Image.Dispose();
                ManVOpen2.Image = Properties.Resources.led_off_green;
                ManVClose2.Image.Dispose();
                ManVClose2.Image = Properties.Resources.led_on_red;

                Thread.Sleep(50);

                serialPort1.Write("R");

            }
            else if (KnowWhichManVal == 2)
            {

                this.Alert("Successfully opened", Form_Alert.enmType.Success);

                //Abrir la 2 y cerrar la 1
                serialPort1.Write("E");

                ManVOpen1.Image.Dispose();
                ManVOpen1.Image = Properties.Resources.led_off_green;
                ManVClose1.Image.Dispose();
                ManVClose1.Image = Properties.Resources.led_on_red;
                ManVOpen2.Image.Dispose();
                ManVOpen2.Image = Properties.Resources.led_on_green;
                ManVClose2.Image.Dispose();
                ManVClose2.Image = Properties.Resources.led_off_red;
                Thread.Sleep(50);
                serialPort1.Write("W");
            }


        }

        private void btnOffMANValve_Click(object sender, EventArgs e)
        {
            if (KnowWhichManVal == 1)
            {
                this.Alert("Successfully closed", Form_Alert.enmType.Success);
                // Cerrar la 1
                serialPort1.Write("W");
                ManVOpen1.Image.Dispose();
                ManVOpen1.Image = Properties.Resources.led_off_green;
                ManVClose1.Image.Dispose();
                ManVClose1.Image = Properties.Resources.led_on_red;
            }
            else if (KnowWhichManVal == 2)
            {
                this.Alert("Successfully closed", Form_Alert.enmType.Success);
                // Cerrar la 2
                serialPort1.Write("R");
                ManVOpen2.Image.Dispose();
                ManVOpen2.Image = Properties.Resources.led_off_green;
                ManVClose2.Image.Dispose();
                ManVClose2.Image = Properties.Resources.led_on_red;
            }
        }

        private void EncenderBTN(Button btn)
        {
            if (btn.Enabled == true)
            {
                if (btn.Name == "btnOnMANValve" || btn.Name == "btnOnPump")
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

        private void TimerAnimation_Tick(object sender, EventArgs e)
        {
            if (animation == 0)
            {

                if (aniIMG == 0)
                {
                    picCamara1.Image.Dispose();
                    picCamara1.Image = Properties.Resources.signal1;
                    aniIMG++;
                    return;
                }
                if (aniIMG == 1)
                {
                    picCamara1.Image.Dispose();
                    picCamara1.Image = Properties.Resources.signal2;
                    aniIMG++;
                    return;
                }
                if (aniIMG == 2)
                {
                    picCamara1.Image.Dispose();
                    picCamara1.Image = Properties.Resources.signal3;
                    aniIMG++;
                    return;
                }
                if (aniIMG == 3)
                {
                    picCamara1.Image.Dispose();
                    picCamara1.Image = Properties.Resources.signal4;
                    aniIMG++;
                    return;
                }
                if (aniIMG == 4)
                {
                    picCamara1.Image.Dispose();
                    picCamara1.Image = Properties.Resources.signal5;
                    aniIMG = 0;
                    return;
                }
            }
        }

        private void TimerAnimation2_Tick(object sender, EventArgs e)
        {
            if (animation2 == 0)
            {

                if (aniIMG2 == 0)
                {
                    picCamara2.Image.Dispose();
                    picCamara2.Image = Properties.Resources.signal1;
                    aniIMG2++;
                    return;
                }
                if (aniIMG2 == 1)
                {
                    picCamara2.Image.Dispose();
                    picCamara2.Image = Properties.Resources.signal2;
                    aniIMG2++;
                    return;
                }
                if (aniIMG2 == 2)
                {
                    picCamara2.Image.Dispose();
                    picCamara2.Image = Properties.Resources.signal3;
                    aniIMG2++;
                    return;
                }
                if (aniIMG2 == 3)
                {
                    picCamara2.Image.Dispose();
                    picCamara2.Image = Properties.Resources.signal4;
                    aniIMG2++;
                    return;
                }
                if (aniIMG2 == 4)
                {
                    picCamara2.Image.Dispose();
                    picCamara2.Image = Properties.Resources.signal5;
                    aniIMG2 = 0;
                    return;
                }
            }
        }

        private void TimerAnimation3_Tick(object sender, EventArgs e)
        {
            if (animation3 == 0)
            {

                if (aniIMG3 == 0)
                {
                    picCamara3.Image.Dispose();
                    picCamara3.Image = Properties.Resources.signal1;
                    aniIMG3++;
                    return;
                }
                if (aniIMG3 == 1)
                {
                    picCamara3.Image.Dispose();
                    picCamara3.Image = Properties.Resources.signal2;
                    aniIMG3++;
                    return;
                }
                if (aniIMG3 == 2)
                {
                    picCamara3.Image.Dispose();
                    picCamara3.Image = Properties.Resources.signal3;
                    aniIMG3++;
                    return;
                }
                if (aniIMG3 == 3)
                {
                    picCamara3.Image.Dispose();
                    picCamara3.Image = Properties.Resources.signal4;
                    aniIMG3++;
                    return;
                }
                if (aniIMG3 == 4)
                {
                    picCamara3.Image.Dispose();
                    picCamara3.Image = Properties.Resources.signal5;
                    aniIMG3 = 0;
                    return;
                }
            }
        }

        private void TimerAnimation4_Tick(object sender, EventArgs e)
        {
            if (animation4 == 0)
            {
                if (aniIMG4 == 0)
                {
                    picCamara4.Image.Dispose();
                    picCamara4.Image = Properties.Resources.signal1;
                    aniIMG4++;
                    return;
                }
                if (aniIMG4 == 1)
                {
                    picCamara4.Image.Dispose();
                    picCamara4.Image = Properties.Resources.signal2;
                    aniIMG4++;
                    return;
                }
                if (aniIMG4 == 2)
                {
                    picCamara4.Image.Dispose();
                    picCamara4.Image = Properties.Resources.signal3;
                    aniIMG4++;
                    return;
                }
                if (aniIMG4 == 3)
                {
                    picCamara4.Image.Dispose();
                    picCamara4.Image = Properties.Resources.signal4;
                    aniIMG4++;
                    return;
                }
                if (aniIMG4 == 4)
                {
                    picCamara4.Image.Dispose();
                    picCamara4.Image = Properties.Resources.signal5;
                    aniIMG4 = 0;
                    return;
                }
            }
        }

        public void CargaDiapositivosInter()
        {
            MisDispositivos = new FilterInfoCollection(FilterCategory.VideoInputDevice);
        }

        public void ActivarCam5(int cb)
        {
            if (TimerAnimation5.Enabled == true)
            {
                TimerAnimation5.Stop();
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
                TimerAnimation5.Start();
                animation5 = 0;
                aniIMG5 = 1;
                offorOn5 = false;
            }
        }

        public void ActivarCam4(int cb)
        {
            if (TimerAnimation4.Enabled == true)
            {
                TimerAnimation4.Stop();
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
                TimerAnimation4.Start();
                animation4 = 0;
                aniIMG4 = 1;
                offorOn4 = false;
            }
        }
        public void ActivarCam3(int cb)
        {
            if (TimerAnimation3.Enabled == true)
            {
                TimerAnimation3.Stop();
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
                TimerAnimation3.Start();
                animation3 = 0;
                aniIMG3 = 1;
                offorOn3 = false;
            }
        }

        public void ActivarCam2(int cb)
        {
            if (TimerAnimation2.Enabled == true)
            {
                TimerAnimation2.Stop();
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
                TimerAnimation2.Start();
                animation2 = 0;
                aniIMG2 = 1;
                offorOn2 = false;
            }
        }

        public void ActivarCam1(int cb)
        {
            if (TimerAnimation.Enabled == true)
            {
                TimerAnimation.Stop();
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
                TimerAnimation.Start();
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

        private void cbManValve_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbManValve.SelectedIndex == 0)
            {
                KnowWhichManVal = 1;
            }
            else if (cbManValve.SelectedIndex == 1)
            {
                KnowWhichManVal = 2;
            }
            else
            {
                KnowWhichManVal = 0;
            }
        }

        int minutos = 0;
        int segundos = 0;
        bool NoesUltimoTick = true;

        private void timer2_Tick(object sender, EventArgs e)
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

            if (minutos == 0 && segundos == 0)
                {
                    int x = Convert.ToInt32(lbCountCycles.Text);
                    x++;
                    lbCountCycles.Text = x.ToString();
                    lb_CounterTest.Text = x.ToString();
                    if (TestToRun == 1)
                    {
                            minutos = 7;
                            segundos = 14;
                            //minutos = 0;
                            //segundos = 10;
                        if (lbCountCycles.Text == lbGoalCycles.Text)
                        {
                            timerTemporizador.Stop();
                            NoesUltimoTick = false;
                            minutos = 0;
                            segundos = 0;
                            MessageBoxMaugoncr.Show("[1] PRETEST CALIBRATION FINISHED", "Phase 1", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else if (TestToRun == 2)
                    {
                        minutos = 5;
                        segundos = 10;
                        //minutos = 0;
                        //segundos = 10;
                        if (lbCountCycles.Text == lbGoalCycles.Text)
                        {
                            timerTemporizador.Stop();
                            NoesUltimoTick = false;
                            minutos = 0;
                            segundos = 0;
                            MessageBoxMaugoncr.Show("[2] STABILITY TEST FINISHED", "Phase 2", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else if (TestToRun == 3)
                    {
                        minutos = 2;
                        segundos = 12;
                        if (lbCountCycles.Text == lbGoalCycles.Text)
                        {
                            timerTemporizador.Stop();
                            NoesUltimoTick = false;
                            minutos = 0;
                            segundos = 0;
                            MessageBoxMaugoncr.Show("[3] VALVE LEAK TEST", "Phase 3", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }

            if (NoesUltimoTick)
            {
                if (minutos != 0 || segundos != 0)
                {
                    if (segundos != 0)
                    {
                        segundos--;
                        if (segundos < 10)
                        {
                            if (minutos < 10)
                            {
                                txtTemporizador.Text = "0" + minutos.ToString() + ":0" + segundos.ToString();
                            }
                            else
                            {
                                txtTemporizador.Text = minutos.ToString() + ":0" + segundos.ToString();
                            }
                        }
                        else
                        {
                            if (minutos < 10)
                            {
                                txtTemporizador.Text = "0" + minutos.ToString() + ":" + segundos.ToString();
                            }
                            else
                            {
                                txtTemporizador.Text = minutos.ToString() + ":" + segundos.ToString();
                            }
                        }
                    }
                    else
                    {
                        if (minutos != 0)
                        {
                            minutos--;
                            segundos = 59;
                            if (minutos < 10)
                            {
                                if (segundos < 10)
                                {
                                    txtTemporizador.Text = "0" + minutos.ToString() + ":0" + segundos.ToString();
                                }
                                else
                                {
                                    txtTemporizador.Text = "0" + minutos.ToString() + ":" + segundos.ToString();
                                }
                            }
                            else
                            {
                                if (segundos < 10)
                                {
                                    txtTemporizador.Text = minutos.ToString() + ":0" + segundos.ToString();
                                }
                                else
                                {
                                    txtTemporizador.Text = minutos.ToString() + ":" + segundos.ToString();
                                }
                            }

                        }
                    }
                }
            }
            
        }

        private DateTime InicioChrono;

        private void GenerarReporte() 
        {
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
            Visualizador.ShowDialog();
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

        private void TimerAnimation5_Tick(object sender, EventArgs e)
        {
            if (animation5 == 0)
            {
                if (aniIMG5 == 0)
                {
                    picCamara5.Image.Dispose();
                    picCamara5.Image = Properties.Resources.signal1;
                    aniIMG5++;
                    return;
                }
                if (aniIMG5 == 1)
                {
                    picCamara5.Image.Dispose();
                    picCamara5.Image = Properties.Resources.signal2;
                    aniIMG5++;
                    return;
                }
                if (aniIMG5 == 2)
                {
                    picCamara5.Image.Dispose();
                    picCamara5.Image = Properties.Resources.signal3;
                    aniIMG5++;
                    return;
                }
                if (aniIMG5 == 3)
                {
                    picCamara5.Image.Dispose();
                    picCamara5.Image = Properties.Resources.signal4;
                    aniIMG5++;
                    return;
                }
                if (aniIMG5 == 4)
                {
                    picCamara5.Image.Dispose();
                    picCamara5.Image = Properties.Resources.signal5;
                    aniIMG5 = 0;
                    return;
                }
            }
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

      
    }
}
