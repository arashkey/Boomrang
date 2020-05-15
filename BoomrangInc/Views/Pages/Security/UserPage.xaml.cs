using BoomrangInc.Classes;
using Business;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BoomrangInc.Views.Pages
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class UserPage : Page, IBaseInterfaceGrid
    {
        public UserPage()
        {
            InitializeComponent();
        }

        #region Method
        public void SearchGrid()
        {
            DataGrid.ItemsSource = Security_User.GetAll();
        }
        public void ClearSearch()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Event



        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            SearchGrid();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MasterPage.selectedItemRow = (sender as ListView).SelectedItem;
        }
        #endregion

        private void ExportExcel_Click(object sender, RoutedEventArgs e)
        {
            //  var dt = (DataGrid.ItemsSource as List<Registry_Registery>).ToDataTable();

            //  Microsoft.Office.Interop.Excel.Worksheet ws;
            //MyExcel.GetExcelPrameter(DataGrid,   out ws);

            //int j = 0;
            //for (int i = 0; i < DataGrid.Items.Count; i++)
            //{
            //    var registery = ((DataGrid.Items[i]) as Security_User);
            //    j = 0;
            //    ws.Range["A2"].Offset[i, j++].Value = i + 1;
            //    ws.Range["A2"].Offset[i, j++].Value = registery.Name;
            //    ws.Range["A2"].Offset[i, j++].Value = registery.Username;
            //    ws.Range["A2"].Offset[i, j++].Value = registery.IsActive ? "بله" : "خیر";
            //    ws.Range["A2"].Offset[i, j++].Value = registery.IsSupperAdmin ? "بله" : "خیر";

            //}
            string reportPath = MyUtility.Basic.AssemblyDirectory + "\\Views\\Pages\\Security\\Report\\UserPage.rdlc";
            var datas = new List<ReportDataSource>();
            datas.Add(new ReportDataSource("DataSetUserPage", DataGrid.ItemsSource as List<Security_User>));
            var objName = "UserPage";
            RDLCFileMaker.ShowDialog(RDLCFileMaker.ExportType.Excel, objName, reportPath, datas);
 
 
        }




        private void Print_Click(object sender, RoutedEventArgs e)
        {
            string reportPath = MyUtility.Basic.AssemblyDirectory + "\\Views\\Pages\\Security\\Report\\UserPage.rdlc";
            var datas = new List<ReportDataSource>();
            datas.Add(new ReportDataSource("DataSetUserPage", DataGrid.ItemsSource as List<Security_User>));
            var objName = "UserPage";
            RDLCFileMaker.ShowDialog(RDLCFileMaker.ExportType.PDF, objName, reportPath, datas);
        }
    }
}
