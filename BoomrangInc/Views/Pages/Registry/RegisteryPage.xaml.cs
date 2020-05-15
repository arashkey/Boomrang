using BoomrangInc.Classes;
using Business;
using Microsoft.Reporting.WinForms;
using MyUtility;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace BoomrangInc.Views.Pages
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class RegisteryPage : Page, IBaseInterfaceGrid
    {

        public RegisteryPage()
        {
            InitializeComponent();
        }

        #region Method
        public void SearchGrid()
        {
            var items = Registry_Registery.NewSearch(new Registry_Registery
            {
                DateOfRegister = DateOfRegisterFrom.SelectedDate,
                WorkshopId = WorkshopId.SelectedValue.ToInt() > 0 ? WorkshopId.SelectedValue.ToInt() : 0,
                PersonId = PersonId.SelectedValue.ToInt() > 0 ? PersonId.SelectedValue.ToInt() : 0,
                UserChangeId = UserChangeId.SelectedValue.ToInt() > 0 ? UserChangeId.SelectedValue.ToInt() : 0,
            }, new Registry_Registery
            {
                DateOfRegister = DateOfRegisterTo.SelectedDate,
            }, DateOfPayFrom.SelectedDate,
             DateOfPayTo.SelectedDate);

            #region Calculate Sum

            var sum = 0;
            foreach (var item in items)
            {
                foreach (var item1 in item.Registry_Pay)
                {
                    sum += item1.Price;
                }
            }

            Total.Text = sum.ToString();
            #endregion


            if (TypeOfPerson.SelectedIndex >= 0)
            {
                if (TypeOfPerson.SelectedIndex == 0)
                    items = items.Where(x => x.Debt <= 0).ToList();
                else
                    items = items.Where(x => x.Debt > 0).ToList();
            }

            DataGrid.ItemsSource = items;

        }
        public void ClearSearch()
        {
            DateOfPayFrom.SelectedDate =
                DateOfPayTo.SelectedDate =
            DateOfRegisterFrom.SelectedDate = null;
            UserChangeId.SelectedValue =
            WorkshopId.SelectedValue =
            PersonId.SelectedValue =
           TypeOfPerson.SelectedValue = null;

            DateOfRegisterTo.SelectedDate = null;
        }
        #endregion

        #region Event



        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UserChangeId.ItemsSource = Security_User.GetAll();
            WorkshopId.ItemsSource = Workshop_Workshop.GetAll();
            PersonId.ItemsSource = PersonInfo_Person.GetAll();

            var list = new List<Setting_Settings>();
            list.Add(new Setting_Settings { Value = "1", Name = "تسویه کرده" });
            list.Add(new Setting_Settings { Value = "2", Name = "بدهکار" });
            TypeOfPerson.ItemsSource = list;
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

        private void ExportExcel_Click(object sender, RoutedEventArgs e)
        {
            //  var dt = (DataGrid.ItemsSource as List<Registry_Registery>).ToDataTable();

            string reportPath = MyUtility.Basic.AssemblyDirectory + "\\Views\\Pages\\Registry\\Report\\Registry.rdlc";
            var datas = new List<ReportDataSource>();
            datas.Add(new ReportDataSource("DataSetRegistery", DataGrid.ItemsSource as List<Registry_Registery>));

            var objName = "Registry";
            RDLCFileMaker.ShowDialog(RDLCFileMaker.ExportType.Excel, objName, reportPath, datas,
                new ReportParameter[] { new ReportParameter("Sum", Total.Text) });

            //Microsoft.Office.Interop.Excel.Worksheet ws;
            //MyExcel.GetExcelPrameter(DataGrid,   out ws);

            //int j = 0;
            //for (int i = 0; i < DataGrid.Items.Count; i++)
            //{
            //    var registery = ((DataGrid.Items[i]) as Registry_Registery);
            //    j = 0;
            //    ws.Range["A2"].Offset[i, j++].Value = i + 1;
            //    ws.Range["A2"].Offset[i, j++].Value = registery.Person;
            //    ws.Range["A2"].Offset[i, j++].Value = registery.Workshop;
            //    ws.Range["A2"].Offset[i, j++].Value = registery.DateOfRegisterPersian;
            //    ws.Range["A2"].Offset[i, j++].Value = registery.Pays;
            //    ws.Range["A2"].Offset[i, j++].Value = registery.SumPayment;
            //    ws.Range["A2"].Offset[i, j++].Value = registery.Debt;
            //    ws.Range["A2"].Offset[i, j++].Value = registery.NumberOfSessionRegister;
            //    ws.Range["A2"].Offset[i, j++].Value = registery.TeacherPortion;
            //    ws.Range["A2"].Offset[i, j++].Value = registery.UsernameOfChange;

            //}



        }

        private void Print_Click(object sender, RoutedEventArgs e)
        {
            string reportPath = Basic.AssemblyDirectory + "\\Views\\Pages\\Registry\\Report\\Registry.rdlc";
            var datas = new List<ReportDataSource>();
            datas.Add(new ReportDataSource("DataSetRegistery", DataGrid.ItemsSource as List<Registry_Registery>));

            var objName = "Registry";
            RDLCFileMaker.ShowDialog(RDLCFileMaker.ExportType.PDF, objName, reportPath, datas,
                new ReportParameter[] { new ReportParameter("Sum", Total.Text) });
        }

        #endregion





    }
}
