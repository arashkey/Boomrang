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

namespace BoomrangInc.Views.Pages
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class WorkshopPage : Page, IBaseInterfaceGrid
    {
        public WorkshopPage()
        {
            InitializeComponent();
        }

        #region Method
        public void SearchGrid()
        {
            DataGrid.ItemsSource = Workshop_Workshop.NewSearch(new Workshop_Workshop
            {
                Name = Name.Text.Length == 0 ? null : Name.Text,
                Price = PriceFrom.Text.ToInt() == -1 ? 0 : PriceFrom.Text.ToInt(),
                StartDate = StartDateFrom.SelectedDate.HasValue ? StartDateFrom.SelectedDate.Value : new DateTime(),
                EndDate = EndDateFrom.SelectedDate.HasValue ? EndDateFrom.SelectedDate.Value : new DateTime(),
                TeacherId = TeacherId.SelectedValue.ToInt() == -1 ? (int?)null : TeacherId.SelectedValue.ToInt(),
                TermId = TermId.SelectedValue.ToInt() == -1 ? 0 : TermId.SelectedValue.ToInt(),
                UserChangeId = UserChangeId.SelectedValue.ToInt() == -1 ? 0 : UserChangeId.SelectedValue.ToInt(),
            },
            new Workshop_Workshop
            {
                Price = PriceTo.Text.ToInt() == -1 ? 0 : PriceTo.Text.ToInt(),
                StartDate = StartDateTo.SelectedDate.HasValue ? StartDateTo.SelectedDate.Value : new DateTime(),
                EndDate = EndDateTo.SelectedDate.HasValue ? EndDateTo.SelectedDate.Value : new DateTime(),

            });
        }
        public void ClearSearch()
        {
            Name.Text =
            PriceFrom.Text =
            PriceTo.Text = "";
            TeacherId.SelectedIndex =
            TermId.SelectedIndex =
            UserChangeId.SelectedIndex = -1;

            StartDateFrom.SelectedDate =
            EndDateFrom.SelectedDate =
            StartDateTo.SelectedDate =
            EndDateTo.SelectedDate = null;
        }
        #endregion

        #region Event



        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UserChangeId.ItemsSource = Security_User.GetAll();
            TeacherId.ItemsSource = Business.Workshop_Teacher.GetAll();
            TermId.ItemsSource = Business.Workshop_Term.GetAll();

        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MasterPage.selectedItemRow = (sender as ListView).SelectedItem;
        }

        private void ClearSearch1_Click(object sender, RoutedEventArgs e)
        {
            ClearSearch();
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            SearchGrid();
        }
        #endregion

        private void ExportExcel_Click(object sender, RoutedEventArgs e)
        {
            string reportPath = MyUtility.Basic.AssemblyDirectory + "\\Views\\Pages\\Workshop\\Report\\Workshop.rdlc";
            var datas = new List<ReportDataSource>();
            datas.Add(new ReportDataSource("DataSetWorkshop", DataGrid.ItemsSource as List<Workshop_Workshop>));
            var objName = "Workshop";
            RDLCFileMaker.ShowDialog(RDLCFileMaker.ExportType.Excel, objName, reportPath, datas);
            //Microsoft.Office.Interop.Excel.Worksheet ws;
            //MyExcel.GetExcelPrameter(DataGrid, out ws);

            //int j = 0;
            //for (int i = 0; i < DataGrid.Items.Count; i++)
            //{
            //    var registery = ((DataGrid.Items[i]) as Workshop_Workshop);
            //    j = 0;
            //    ws.Range["A2"].Offset[i, j++].Value = i + 1;
            //    ws.Range["A2"].Offset[i, j++].Value = registery.Name;
            //    ws.Range["A2"].Offset[i, j++].Value = registery.StartDatePersian;
            //    ws.Range["A2"].Offset[i, j++].Value = registery.EndDatePersian;
            //    ws.Range["A2"].Offset[i, j++].Value = registery.Teacher;
            //    ws.Range["A2"].Offset[i, j++].Value = registery.Term;
            //    ws.Range["A2"].Offset[i, j++].Value = registery.Price;
            //    ws.Range["A2"].Offset[i, j++].Value = registery.NumberOfSession;
            //    ws.Range["A2"].Offset[i, j++].Value = registery.TeacherPortion;
            //    ws.Range["A2"].Offset[i, j++].Value = registery.UsernameOfChange;

            //}
        }

        private void Print_Click(object sender, RoutedEventArgs e)
        {
            string reportPath = MyUtility.Basic.AssemblyDirectory + "\\Views\\Pages\\Workshop\\Report\\Workshop.rdlc";
            var datas = new List<ReportDataSource>();
            datas.Add(new ReportDataSource("DataSetWorkshop", DataGrid.ItemsSource as List<Workshop_Workshop>));
            var objName = "Workshop";
            RDLCFileMaker.ShowDialog(RDLCFileMaker.ExportType.PDF, objName, reportPath, datas);
        }

    }
}
