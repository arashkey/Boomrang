using BoomrangInc.Classes;
using Business;
using Business.SpecialQuery;
using MyUtility;
using System.Windows;
using System.Windows.Controls;

namespace BoomrangInc.Views.Pages
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class ReportAddPage : Page, IBaseInterfaceAdd
    {
        public ReportAddPage()
        {
            InitializeComponent();
        }

        #region Method

        public void ClearForm()
        {
            Name.Clear();
            Family.Clear();
            NationalNumber.Clear();
            TypeOfRelation.Clear();
            WorkshopId.SelectedIndex =
            TermId.SelectedIndex =
            AgeRangeId.SelectedIndex =
            IsInViber.SelectedIndex =-1;
            MasterPage.newFrameGrid.ClearSearch();

        }

        public void BindData(object data)
        {

        }

        public object GetData(int id)
        {
            return null;
        }

        public bool SaveData(object o)
        {
            return false;
        }
        #endregion

        #region Event



        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            
            WorkshopId.ItemsSource = Workshop_Workshop.GetAll();
            TermId.ItemsSource = Workshop_Term.GetAll();
            AgeRangeId.ItemsSource = SearchReport.GetRange();
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            MasterPage.selectedItemRow = new SearchReport
            {
                Name = Name.Text,
                Family = Family.Text,
                NationalNumber = NationalNumber.Text,
             
                TypeOfRelation = TypeOfRelation.Text,
                WorkshopId = WorkshopId.SelectedValue.ToInt(),
                TermId = TermId.SelectedValue.ToInt(),
                AgeRangeId = AgeRangeId.SelectedValue.ToInt(),
                //اگر انتخاب نشده بود نال وگرنه برای مرد درست و برای زن غلط بر می گردونه
                IsInViber = IsInViber.SelectedIndex < 0 ? (bool?)null : IsInViber.SelectedIndex == 1,
            };
            MasterPage.newFrameGrid.SearchGrid();
        }
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            ClearForm();

        }
        #endregion

     





    }
}
