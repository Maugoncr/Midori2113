using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Windows.Shapes;
using MidoriValveTest.Properties;
using CustomMessageBox;
using System.Collections.Generic;
using System.Text;

namespace MidoriValveTest.Forms
{
    public partial class FrmChartSepararDataPhase2 : Form
    {
        private bool mousePresionado;
        private Point posicionMouse;
        private int totalCycles;
        private string rutaCompareChartFileFiltered;

        public FrmChartSepararDataPhase2()
        {
            InitializeComponent();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mousePresionado = true;
                posicionMouse = e.Location;
            }
        }

        private void btnImportData_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Archivos de texto (*.txt)|*.txt";
            openFileDialog.Title = "Select Report Data Phase 2";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;

                try
                {
                    // Leer la primera línea del archivo
                    string primeraLinea = File.ReadLines(filePath).First();

                    if (primeraLinea == "# Software Midori II Phase 2")
                    {

                        try
                        {
                            // Leer todas las líneas del archivo
                            string[] lineas = File.ReadAllLines(filePath);

                            // Verificar si hay suficientes líneas para eliminar
                            if (lineas.Length >= 5)
                            {
                                // Eliminar las primeras 5 líneas
                                lineas = lineas.Skip(5).ToArray();
                            }
                            else
                            {
                                // Si no hay suficientes líneas, se deja el archivo sin cambios
                                MessageBoxMaugoncr.Show("Not enough lines to remove.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                            // Filtrar y modificar las líneas
                            for (int i = 0; i < lineas.Length; i++)
                            {
                                // Obtener la línea actual
                                string linea = lineas[i];

                                // Separar los campos por la coma
                                string[] campos = linea.Split(',');

                                if (campos.Length >= 4)
                                {
                                    // Tomar los primeros 3 campos
                                    string nuevaLinea = string.Join(",", campos.Take(3));

                                    // Asignar la nueva línea modificada
                                    lineas[i] = nuevaLinea;
                                }
                            }

                           // Crear el directorio "Archivos Filtrados" si no existe

                            string directorioFiltrado = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(filePath), "Filtered Files");

                            if (!Directory.Exists(directorioFiltrado))
                            {
                                Directory.CreateDirectory(directorioFiltrado);
                            }

                            // Crear el nuevo nombre de archivo con "FILTRADO" al final
                            string nuevoNombreArchivo = System.IO.Path.GetFileNameWithoutExtension(filePath) + "- FILTERED" + System.IO.Path.GetExtension(filePath);

                            // Crear la ruta del nuevo archivo filtrado dentro del directorio "Archivos Filtrados"
                            string nuevaRuta = System.IO.Path.Combine(directorioFiltrado, nuevoNombreArchivo);

                            // Escribir las líneas modificadas en el nuevo archivo filtrado
                            File.WriteAllLines(nuevaRuta, lineas);

                            txtRutaArchivoFiltrado.Text = nuevaRuta;

                            if (lineas.Length > 0)
                            {
                                string ultimaLinea = lineas[lineas.Length - 1];

                                txtTotalCycles.Text = ultimaLinea.Split(',')[0].Trim();
                                totalCycles = Convert.ToInt32(txtTotalCycles.Text);

                            }

                            MessageBoxMaugoncr.Show("Successfully created filtered file.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBoxMaugoncr.Show("Error processing the file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        // La primera línea no coincide con el texto esperado
                        MessageBoxMaugoncr.Show("Incorrect TXT file.","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBoxMaugoncr.Show("Error reading file: " + ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            // Verificar si el mouse está presionado y se está moviendo
            if (mousePresionado)
            {
                // Obtener la posición actual del mouse
                Point nuevaPosicion = e.Location;

                // Calcular el desplazamiento
                int desplazamientoX = nuevaPosicion.X - posicionMouse.X;
                int desplazamientoY = nuevaPosicion.Y - posicionMouse.Y;

                // Actualizar la posición del formulario
                Location = new Point(Location.X + desplazamientoX, Location.Y + desplazamientoY);
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            // Se ha dejado de presionar el botón del mouse
            mousePresionado = false;
        }

        private void IconClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmChartSepararDataPhase2_Load(object sender, EventArgs e)
        {
            CargarFrmResetAll();
        }

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

        private void CargarFrmResetAll()
        {
            EnableBtn(btnImportData);
            DisableBtn(btnShowCyclesC);
            txtRutaArchivoFiltrado.Clear();
            txtTotalCycles.Clear();
            checkFunctionCycleCompare.Checked = false;
            checkFunctionCycleCompare.Enabled = false;
            txtCycle1.Enabled = false;
            txtCycle2.Enabled = false;
            txtCycle3.Enabled = false;
            txtCycle4.Enabled = false;
            txtCycle5.Enabled = false;
            txtCycle6.Enabled = false;
            txtCycle7.Enabled = false;
            txtCycle8.Enabled = false;
            txtCycle9.Enabled = false;
            txtCycle10.Enabled = false;
        }

        private void checkFunctionCycleCompare_CheckedChanged(object sender, EventArgs e)
        {
            if (checkFunctionCycleCompare.Checked)
            {
                // Habilitar los textboxes según el valor de totalCycles
                EnableBtn(btnShowCyclesC);
                for (int i = 1; i <= totalCycles; i++)
                {
                    Control[] controls = Controls.Find("txtCycle" + i, true);
                    if (controls.Length > 0 && controls[0] is TextBox)
                    {
                        TextBox textBox = (TextBox)controls[0];
                        textBox.Enabled = true;
                    }
                }
            }
            else
            {
                // Deshabilitar todos los textboxes
                DisableBtn(btnShowCyclesC);
                foreach (Control control in Controls)
                {
                    if (control is TextBox)
                    {
                        TextBox textBox = (TextBox)control;

                        if (textBox.Name != "txtTotalCycles" && textBox.Name != "txtRutaArchivoFiltrado")
                        {
                            textBox.Enabled = false;
                            textBox.Clear();
                        }
                    }
                }
            }
        }

        private void txtTotalCycles_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtTotalCycles.Text))
            {
                try
                {
                    int num = Convert.ToInt32(txtTotalCycles.Text);
                    if (num >= 1)
                    {
                        checkFunctionCycleCompare.Enabled = true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }


        }

        private void txtCycle1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 32 && e.KeyChar <= 47) || (e.KeyChar >= 58 && e.KeyChar <= 255))
            {
                MessageBoxMaugoncr.Show("Only numbers are allowed", "Important", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void txtCycle2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 32 && e.KeyChar <= 47) || (e.KeyChar >= 58 && e.KeyChar <= 255))
            {
                MessageBoxMaugoncr.Show("Only numbers are allowed", "Important", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void txtCycle3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 32 && e.KeyChar <= 47) || (e.KeyChar >= 58 && e.KeyChar <= 255))
            {
                MessageBoxMaugoncr.Show("Only numbers are allowed", "Important", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void txtCycle4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 32 && e.KeyChar <= 47) || (e.KeyChar >= 58 && e.KeyChar <= 255))
            {
                MessageBoxMaugoncr.Show("Only numbers are allowed", "Important", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void txtCycle5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 32 && e.KeyChar <= 47) || (e.KeyChar >= 58 && e.KeyChar <= 255))
            {
                MessageBoxMaugoncr.Show("Only numbers are allowed", "Important", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void txtCycle6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 32 && e.KeyChar <= 47) || (e.KeyChar >= 58 && e.KeyChar <= 255))
            {
                MessageBoxMaugoncr.Show("Only numbers are allowed", "Important", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void txtCycle7_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 32 && e.KeyChar <= 47) || (e.KeyChar >= 58 && e.KeyChar <= 255))
            {
                MessageBoxMaugoncr.Show("Only numbers are allowed", "Important", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void txtCycle8_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 32 && e.KeyChar <= 47) || (e.KeyChar >= 58 && e.KeyChar <= 255))
            {
                MessageBoxMaugoncr.Show("Only numbers are allowed", "Important", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void txtCycle9_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 32 && e.KeyChar <= 47) || (e.KeyChar >= 58 && e.KeyChar <= 255))
            {
                MessageBoxMaugoncr.Show("Only numbers are allowed", "Important", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void txtCycle10_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 32 && e.KeyChar <= 47) || (e.KeyChar >= 58 && e.KeyChar <= 255))
            {
                MessageBoxMaugoncr.Show("Only numbers are allowed", "Important", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void btnShowCyclesC_Click(object sender, EventArgs e)
        {
            bool desordenado = false;

            for (int i = 2; i <= 10; i++)
            {
                TextBox currentTextBox = Controls.Find("txtCycle" + i, true).FirstOrDefault() as TextBox;
                TextBox previousTextBox = Controls.Find("txtCycle" + (i - 1), true).FirstOrDefault() as TextBox;

                if (currentTextBox.Text.Length > 0 && previousTextBox.Text.Length == 0)
                {
                    desordenado = true;
                    break;
                }
            }

            if (desordenado)
            {
                MessageBox.Show("The textboxes are out of order. You must enter values in the following color order:\n1° Red" +
                    "\n2° Blue\n3° Yellow\n4° Green\n5° Purple\n6° Cyan\n7° Orange\n8° Maroon\n9° Lime\n10° Magenta","Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }

            List<int> valores = new List<int>();

            bool alMenosUnValor = false;
            bool valoresDuplicados = false;
            bool valoresFueraDeRango = false;

            for (int i = 1; i <= totalCycles; i++)
            {
                TextBox textBox = Controls.Find("txtCycle" + i, true).FirstOrDefault() as TextBox;

                if (textBox != null)
                {
                    if (!string.IsNullOrWhiteSpace(textBox.Text))
                    {
                        alMenosUnValor = true;

                        int valor;

                        if (int.TryParse(textBox.Text, out valor))
                        {
                            if (valores.Contains(valor))
                            {
                                valoresDuplicados = true;
                                break;
                            }
                            else if (valor < 1 || valor > totalCycles)
                            {
                                valoresFueraDeRango = true;
                                break;
                            }
                            else
                            {
                                valores.Add(valor);
                            }
                        }
                        else
                        {
                            MessageBox.Show("The value in the TextBox " + textBox.Name + " is not a valid number.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                }
            }

            if (!alMenosUnValor)
            {
                MessageBox.Show("There must be at least one TextBox with a value.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (valoresDuplicados)
            {
                MessageBox.Show("Duplicate values are not allowed in TextBoxes.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (valoresFueraDeRango)
            {
                MessageBox.Show("The values must be in the range of 1 to " + totalCycles + ".", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (valores.Count == 0)
            {
                MessageBox.Show("The list of values is empty.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(txtRutaArchivoFiltrado.Text) || !File.Exists(txtRutaArchivoFiltrado.Text))
            {
                MessageBox.Show("The file path is invalid.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                List<string> lineas = new List<string>();

                using (StreamReader sr = new StreamReader(txtRutaArchivoFiltrado.Text))
                {
                    string linea;

                    while ((linea = sr.ReadLine()) != null)
                    {
                        string[] elementos = linea.Split(',');

                        if (elementos.Length >= 1)
                        {
                            if (int.TryParse(elementos[0], out int valor))
                            {
                                if (valores.Contains(valor))
                                {
                                    lineas.Add(linea);
                                }
                            }
                        }
                    }
                }

                File.WriteAllLines(txtRutaArchivoFiltrado.Text, lineas);

                string sourceFilePath = txtRutaArchivoFiltrado.Text;
                string destinationFilePath = txtRutaArchivoFiltrado.Text;

                try
                {
                    // Lee el archivo de origen
                    string[] lines = File.ReadAllLines(sourceFilePath);

                    // Prepara el contenido convertido
                    StringBuilder convertedContent = new StringBuilder();

                    foreach (string line in lines)
                    {
                        // Divide la línea por las comas
                        string[] values = line.Split(',');

                        if (values.Length == 3)
                        {
                            // Reordena los valores y agrega una nueva línea al contenido convertido
                            string convertedLine = $"{values[1]},{values[2]},{values[0]}";
                            convertedContent.AppendLine(convertedLine);
                        }
                    }

                    // Escribe el contenido convertido en el archivo de destino
                    File.WriteAllText(destinationFilePath, convertedContent.ToString());

                }
                catch (Exception ex)
                {
                    MessageBoxMaugoncr.Show($"Ocurrió un error durante la conversión: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                rutaCompareChartFileFiltered = txtRutaArchivoFiltrado.Text;

                FrmChartComparationPhase2 frm = new FrmChartComparationPhase2(rutaCompareChartFileFiltered);
                frm.ShowDialog();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while processing the file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
