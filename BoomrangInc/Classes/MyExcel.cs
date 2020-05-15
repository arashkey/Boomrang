using MyUtility;
using Business;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Controls;

namespace BoomrangInc.Classes
{
    public static class MyExcel
    {

        public static void GetExcelPrameter(ListView DataGrid,   out Microsoft.Office.Interop.Excel.Worksheet ws)
        {
            Microsoft.Office.Interop.Excel.Application excelApp;
            Microsoft.Office.Interop.Excel.Workbook wb;

            GridView view = (GridView)DataGrid.View;

            excelApp = null;
            wb = null;

            object missing = Type.Missing;
            ws = null;
            // Microsoft.Office.Interop.Excel.Range rng = null;

            excelApp = new Microsoft.Office.Interop.Excel.Application();
            //Set global attributes
          


            wb = excelApp.Workbooks.Add();
            ws = (Microsoft.Office.Interop.Excel.Worksheet)wb.ActiveSheet;

            for (int Idx = 0; Idx < view.Columns.Count; Idx++)
            {
                ws.Range["A1"].Offset[0, Idx].Value = view.Columns[Idx].Header;
            }


            //var headerFont = new Font("B Nazanin", 12, FontStyle.Bold);
            //Microsoft.Office.Interop.Excel.Range rng = ws.Cells[1, 1] as Microsoft.Office.Interop.Excel.Range;
            Microsoft.Office.Interop.Excel.Range rng = ws.Cells[1, 1].EntireRow;
            rng.Font.Bold = true;
            rng.Columns.AutoFit();

            excelApp.Visible = true;
            wb.Activate();

            excelApp.StandardFont = "B Nazanin";
            excelApp.StandardFontSize = 10;

            //Microsoft.Office.Interop.Excel.Style style = excelApp.ActiveWorkbook.Styles.Add("NewStyle");

            //style.Font.Name = "B Nazanin";
            //style.Font.Size = 12;
            //style.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
            //style.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Gray);
            //style.Interior.Pattern = Microsoft.Office.Interop.Excel.XlPattern.xlPatternSolid;
         }
        public static void ExportExcel(ListView listView)
        {
            GridView view = (GridView)listView.View;

            Microsoft.Office.Interop.Excel.Application excel = null;
            Microsoft.Office.Interop.Excel.Workbook wb = null;

            object missing = Type.Missing;
            Microsoft.Office.Interop.Excel.Worksheet ws = null;
            // Microsoft.Office.Interop.Excel.Range rng = null;

            excel = new Microsoft.Office.Interop.Excel.Application();
            wb = excel.Workbooks.Add();
            ws = (Microsoft.Office.Interop.Excel.Worksheet)wb.ActiveSheet;

            for (int Idx = 0; Idx < view.Columns.Count; Idx++)
            {
                ws.Range["A1"].Offset[0, Idx].Value = view.Columns[Idx].Header;
            }
            //for (int Idx = 0; Idx < dt.Columns.Count; Idx++)
            //{
            //    ws.Range["A1"].Offset[0, Idx].Value = dt.Columns[Idx].ColumnName;
            //}

            //for (int Idx = 0; Idx < dt.Rows.Count; Idx++)
            //{  // <small>hey! I did not invent this line of code, 
            //    // I found it somewhere on CodeProject.</small> 
            //    // <small>It works to add the whole row at once, pretty cool huh?</small>
            //    ws.Range["A2"].Offset[Idx].Resize[1, dt.Columns.Count].Value = dt.Rows[Idx].ItemArray;
            //}
            for (int i = 0; i < listView.Items.Count; i++)
            {

                for (int Idx = 0; Idx < view.Columns.Count; Idx++)
                {
                    ws.Range["A2"].Offset[i, Idx].Value = ((listView.Items[i]) as Registry_Registery).RegisteryId;
                }
            }

            //for (int i = 0; i < datas.Count; i++)
            //{
            //    ws.Range["A2"].Offset[i].Resize[1, ((GridView)myList.View).Columns.Count].Value =
            //        datas[i];
            //}
            excel.Visible = true;
            wb.Activate();
        }



    }
}
