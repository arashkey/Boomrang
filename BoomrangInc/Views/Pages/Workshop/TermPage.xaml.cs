using MyUtility;
using BoomrangInc.Classes;
using Business;
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
using Microsoft.Reporting.WinForms;
using System.Windows.Forms;

namespace BoomrangInc.Views.Pages
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class TermPage : Page, IBaseInterfaceGrid
    {
        public TermPage()
        {
            InitializeComponent();
        }

        #region Method
        public void SearchGrid()
        {
           var datas= Business.Workshop_Term.Search(new Workshop_Term
            {
                //IsCurrentTerm = IsCurrentTerm.SelectedIndex == 1,
                Name = Name.Text,
                Season = Season.Text,
                Year = Year.Text.ToInt(),
                UserChangeId = UserChangeId.SelectedIndex == -1 ? 0 : UserChangeId.SelectedValue.ToInt(),
            }).ToList();
            DataGrid.ItemsSource = datas;
        }
        public void ClearSearch()
        {
            UserChangeId.SelectedIndex = -1;
            Name.Text =
                 Season.Text =
                Year.Text = "";
        }
        #endregion

        #region Event



        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UserChangeId.ItemsSource = Security_User.GetAll();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MasterPage.selectedItemRow = (sender as System.Windows.Controls.ListView).SelectedItem;
        }
        private void Search_Click(object sender, RoutedEventArgs e)
        {
            SearchGrid();
        }

        private void ClearSearch1_Click(object sender, RoutedEventArgs e)
        {
            ClearSearch();

        }

        private void ExportExcel_Click(object sender, RoutedEventArgs e)
        {
            string reportPath = MyUtility.Basic.AssemblyDirectory + "\\Views\\Pages\\Workshop\\Report\\Term.rdlc";
            var datas = new List<ReportDataSource>();
            datas.Add(new ReportDataSource("DataSetTerm", DataGrid.ItemsSource as List<Workshop_Term>));
            var objName = "Term";
            RDLCFileMaker.ShowDialog(RDLCFileMaker.ExportType.Excel, objName, reportPath, datas);

            //RDLCFileMaker.ShowDialog(RDLCFileMaker.ExportType.PDF, objName, reportPath, datas);
            //Microsoft.Office.Interop.Excel.Worksheet ws;
            //MyExcel.GetExcelPrameter(DataGrid, out ws);

            //int j = 0;
            //for (int i = 0; i < DataGrid.Items.Count; i++)
            //{
            //    var registery = ((DataGrid.Items[i]) as Workshop_Term);
            //    j = 0;
            //    ws.Range["A2"].Offset[i, j++].Value = i + 1;
            //    ws.Range["A2"].Offset[i, j++].Value = registery.Name;
            //    ws.Range["A2"].Offset[i, j++].Value = registery.Season;
            //    ws.Range["A2"].Offset[i, j++].Value = registery.Year;
            //    ws.Range["A2"].Offset[i, j++].Value = registery.IsCurrentTerm ? "بله" : "خیر";
            //    ws.Range["A2"].Offset[i, j++].Value = registery.UsernameOfChange;

            //}
        }

        private void Print_Click(object sender, RoutedEventArgs e)
        {
            string reportPath = MyUtility.Basic.AssemblyDirectory + "\\Views\\Pages\\Workshop\\Report\\Term.rdlc";
            var datas = new List<ReportDataSource>();
            datas.Add(new ReportDataSource("DataSetTerm", DataGrid.ItemsSource as List<Workshop_Term>));
            var objName = "Term";
            RDLCFileMaker.ShowDialog(RDLCFileMaker.ExportType.PDF, objName, reportPath, datas);
            //RDLCFileMaker.ShowDialog(RDLCFileMaker.ExportType.Excel, objName, reportPath, datas);

        }


        #endregion

    }
}
