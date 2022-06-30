using AForge.Video;
using AForge.Video.DirectShow;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MidoriValveTest.Forms
{
    public partial class Camara : Form
    {
        private bool HayDispositivos;
        private FilterInfoCollection MisDispositivos;
        private VideoCaptureDevice MiWebCam;
        [DllImport("user32.dll", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        public static int animation = 0;
        public static int aniIMG = 0;
        public static bool offorOn = false;
        public static Image capture;
        

        public Camara()
        {
            InitializeComponent();
        }

        private void Camara_Load(object sender, EventArgs e)
        {
            CargaDiapositivos();
            InitializeSetting();
            TimerAnimation.Start();
        }
        public void InitializeSetting()
        {
            this.FormBorderStyle = FormBorderStyle.None;

        }

        public void CargaDiapositivos() {

            MisDispositivos = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (MisDispositivos.Count > 0)
            {
                HayDispositivos = true;
                cbCamaraSelect.Items.Clear();
                for (int i = 0; i < MisDispositivos.Count; i++)
                {
                    cbCamaraSelect.Items.Add(MisDispositivos[i].Name.ToString());
                    cbCamaraSelect.Text = MisDispositivos[0].Name.ToString();
                }

            }
            else {
                HayDispositivos = false;
            }
        
        }

        private void Capturando(object sender, NewFrameEventArgs eventsArgs) {

            Bitmap Imagen = (Bitmap)eventsArgs.Frame.Clone();
            picCamara.Image = Imagen;
        
        }

        private void IconIniciarCam_Click(object sender, EventArgs e)
        {
            if (TimerAnimation.Enabled == true)
            {
                TimerAnimation.Stop();
                animation = 1;
            }

            if (offorOn == false)
            {
                CerrarWebCam();
                int i = cbCamaraSelect.SelectedIndex;
                string NombreVideo = MisDispositivos[i].MonikerString;
                MiWebCam = new VideoCaptureDevice(NombreVideo);
                MiWebCam.NewFrame += new NewFrameEventHandler(Capturando);
                MiWebCam.Start();
                offorOn = true;
            }
            else if (offorOn == true)
            {
                CerrarWebCam();
                picCamara.Image.Dispose();
                picCamara.Image = MidoriValveTest.Properties.Resources.signal1;
                TimerAnimation.Start();
                animation = 0;
                aniIMG = 1;
                offorOn = false;
            }

          

        }

        private void CerrarWebCam() {

            if (MiWebCam!=null && MiWebCam.IsRunning)
            {
                MiWebCam.SignalToStop();
                MiWebCam = null;
            }
        
        
        }

        private void Camara_FormClosed(object sender, FormClosedEventArgs e)
        {
            CerrarWebCam();
            TimerAnimation.Stop();
            animation = 0;
            aniIMG = 0;
            offorOn = false;
    }

        private void cbCamaraSelect_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void iconMini_Click(object sender, EventArgs e)
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

        private void iconClose_Click(object sender, EventArgs e)
        {
            this.Close();
            CerrarWebCam();
        }

        private void topNav_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void TimerAnimation_Tick(object sender, EventArgs e)
        {
            if (animation == 0)
            {
                
                if (aniIMG == 0)
                {
                    picCamara.Image.Dispose();
                    picCamara.Image = MidoriValveTest.Properties.Resources.signal1;
                    aniIMG++;
                    return;
                }
                if (aniIMG == 1)
                {
                    picCamara.Image.Dispose();
                    picCamara.Image = MidoriValveTest.Properties.Resources.signal2;
                    aniIMG++;
                    return;
                }
                if (aniIMG == 2)
                {
                    picCamara.Image.Dispose();
                    picCamara.Image = MidoriValveTest.Properties.Resources.signal3;
                    aniIMG++;
                    return;
                }
                if (aniIMG == 3)
                {
                    picCamara.Image.Dispose();
                    picCamara.Image = MidoriValveTest.Properties.Resources.signal4;
                    aniIMG++;
                    return;
                }
                if (aniIMG == 4)
                {
                    picCamara.Image.Dispose();
                    picCamara.Image = MidoriValveTest.Properties.Resources.signal5;
                    aniIMG = 0;
                    return;
                }
            }


        }

        private void iconRefresh_Click(object sender, EventArgs e)
        {

            CargaDiapositivos();


        }

        private void iconCapture_Click(object sender, EventArgs e)
        {
            if (MiWebCam != null && MiWebCam.IsRunning)
            {
                capture = picCamara.Image;
                Form frm = Application.OpenForms.Cast<Form>().FirstOrDefault(x => x is CameraCapture);
                if (frm == null)
                {
                    CameraCapture TEST = new CameraCapture();
                    TEST.ShowDialog();
                }
                else
                {
                    frm.BringToFront();
                    return;
                }


            }


        }
    }
}
