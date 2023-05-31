using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MidoriValveTest.Forms
{
    public partial class FrmVisualizadorCrystalReport : Form
    {
        public FrmVisualizadorCrystalReport()
        {
            InitializeComponent();
        }

        private void FrmVisualizadorCrystalReport_Load(object sender, EventArgs e)
        {
           
        }

        private void btnExportPDF_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Archivos PDF (*.pdf)|*.pdf";
            saveFileDialog.Title = "Save Report as PDF";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    ReportDocument reportDocument = crystalReportViewer1.ReportSource as ReportDocument;

                    // Exporta el contenido de CrystalReportViewer a un archivo PDF
                    ExportOptions exportOptions = new ExportOptions();
                    exportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    exportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    exportOptions.DestinationOptions = new DiskFileDestinationOptions { DiskFileName = saveFileDialog.FileName };

                    reportDocument.Export(exportOptions);

                    MessageBox.Show("El reporte se ha exportado exitosamente a PDF.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrió un error al exportar el reporte a PDF: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
