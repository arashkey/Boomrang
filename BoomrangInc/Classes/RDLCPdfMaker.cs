using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BoomrangInc.Classes
{
    public static class RDLCFileMaker
    {
        public enum ExportType
        {
            PDF, Excel//, Word
        };
        public static void ShowDialog(ExportType expType, string objName, string reportPath, List<ReportDataSource> datas, ReportParameter[] parameters = null)
        {
            string ext = "";
            switch (expType)
            {
                case ExportType.PDF:
                    ext = "pdf";
                    break;
                case ExportType.Excel:
                    ext = "xls";
                    break;
                //case ExportType.Word:
                //    ext = "doc";
                //    break;
            }
            SaveFileDialog dlg = new SaveFileDialog { Filter = "*." + ext + "|*." + ext, FileName = objName + "." + ext };
            if (dlg.ShowDialog() == DialogResult.OK)
                BoomrangInc.Classes.RDLCFileMaker.Export(expType, dlg.FileName, reportPath, datas, parameters);

        }

        public static void Export(ExportType expType, string pdfFileName, string rdlcReportPath,
            List<ReportDataSource> reportDataSource, ReportParameter[] parameters)
        {
            // Variables
            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;


            // Setup the report viewer object and get the array of bytes
            ReportViewer viewer = new ReportViewer();
            viewer.ProcessingMode = ProcessingMode.Local;
            viewer.LocalReport.ReportPath = rdlcReportPath;

            foreach (var item in reportDataSource)
                viewer.LocalReport.DataSources.Add(item);
            if (parameters != null)
                foreach (var item in parameters)
                {
                    viewer.LocalReport.SetParameters(parameters);
                }



            byte[] bytes = viewer.LocalReport.Render(expType.ToString(), null, out mimeType, out encoding, out extension, out streamIds, out warnings);
            // Now that you have all the bytes representing the PDF report, buffer it and send it to the client.
            var strFileName = Path.GetDirectoryName(pdfFileName) + "\\" + Path.GetFileNameWithoutExtension(pdfFileName) + "." + extension;
            using (FileStream fs = new FileStream(strFileName, FileMode.Create))
            {
                fs.Write(bytes, 0, bytes.Length);
            }


        }
    }
}
