using BoomrangInc.Classes;
using Business;
using Microsoft.Reporting.WinForms;
using MyUtility;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Linq;

namespace BoomrangInc.Views.Pages
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class PersonPage : Page, IBaseInterfaceGrid
    {
        public PersonPage()
        {
            InitializeComponent();
        }

        #region Method
        public void SearchGrid()
        {

            var datas = PersonInfo_Person.Search(new PersonInfo_Person
            {
                Name = Name.Text.StringOrNull(),
                Family = Family.Text.StringOrNull(),
                Father = Father.Text.StringOrNull(),
                Mother = Mother.Text.StringOrNull(),

                TypeOfRelation = TypeOfRelation.Text.StringOrNull(),
                IsInViber = IsInViber.SelectedIndex == -1 ? (bool?)null : IsInViber.SelectedIndex == 1,
                NationalNumber = NationalNumber.Text.StringOrNull(),
            }).Where(x => (!DateOfBirthFrom.SelectedDate.HasValue || x.DateOfBirth >= DateOfBirthFrom.SelectedDate.Value)
            && (!DateOfBirthTo.SelectedDate.HasValue || x.DateOfBirth <= DateOfBirthTo.SelectedDate.Value)
           && (Gender.SelectedIndex == -1 || (Gender.SelectedIndex == 1) == x.Gender))
             .OrderBy(x => x.Family);


            DataGrid.ItemsSource = datas.ToList();
            //DateOfBirth = DateOfBirthFrom.SelectedDate.HasValue ? DateOfBirthFrom.SelectedDate.Value : new DateTime(),
            //{
            //}, new PersonInfo_Person
            //{
            //    DateOfBirth = DateOfBirthTo.SelectedDate.HasValue ? DateOfBirthTo.SelectedDate.Value : new DateTime(),
            //});
        }
        public void ClearSearch()
        {
            Name.Clear();
            Family.Clear();
            Father.Clear();
            Mother.Clear();
            TypeOfRelation.Clear();
            UserChangeId.SelectedIndex =
            Gender.SelectedIndex =
            IsInViber.SelectedIndex = -1;
            NationalNumber.Clear();
            DateOfBirthFrom.SelectedDate =
            DateOfBirthTo.SelectedDate = null;
        }
        #endregion

        #region Event



        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UserChangeId.ItemsSource = Security_User.GetAll();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MasterPage.selectedItemRow = (sender as ListView).SelectedItem;
        }
        #endregion

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
            string reportPath = MyUtility.Basic.AssemblyDirectory + "\\Views\\Pages\\PersonInfo\\Report\\PersonInfo.rdlc";
            var datas = new List<ReportDataSource>();
            datas.Add(new ReportDataSource("DataSetPerson", DataGrid.ItemsSource as List<PersonInfo_Person>));
            var objName = "Person";
            RDLCFileMaker.ShowDialog(RDLCFileMaker.ExportType.Excel, objName, reportPath, datas);
        }

        private void Print_Click(object sender, RoutedEventArgs e)
        {
            string reportPath = MyUtility.Basic.AssemblyDirectory + "\\Views\\Pages\\PersonInfo\\Report\\PersonInfo.rdlc";
            var datas = new List<ReportDataSource>();
            datas.Add(new ReportDataSource("DataSetPerson", DataGrid.ItemsSource as List<PersonInfo_Person>));
            var objName = "Person";
            RDLCFileMaker.ShowDialog(RDLCFileMaker.ExportType.PDF, objName, reportPath, datas);
        }


    }
}
