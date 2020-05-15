using MyUtility;
using BoomrangInc.Classes;
using Business;
using System;
using System.Collections;
using System.Collections.Generic;
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
    public partial class WorkshopAddPage : Page, IBaseInterfaceAdd
    {
        public WorkshopAddPage()
        {
            InitializeComponent();
        }

        #region Method

        public void ClearForm()
        {
            Name.Clear();
            TeacherPortion.Clear();
            StartDate.SelectedDate = null;
            EndDate.SelectedDate = null;
            Price.Clear();
            NumberOfSession.Clear();
            TermId.SelectedIndex = -1;
            TeacherId.SelectedIndex = -1;
            Edit.Content = (EditText.Edit);
        }

        public void BindData(object data)
        {
            Workshop_Workshop t = data as Workshop_Workshop;
            Name.Text = t.Name;
            StartDate.SelectedDate = t.StartDate;
            EndDate.SelectedDate = t.EndDate;
            TermId.SelectedValue = t.TermId;
            TeacherId.SelectedValue = t.TeacherId;
            Price.Text = t.Price.ToString();
            TeacherPortion.Text = t.TeacherPortion.ToString();
            NumberOfSession.Text = t.NumberOfSession.ToString();
        }

        public object GetData(int id)
        {
            return Workshop_Workshop.GetById(id);
        }

        public bool SaveData(object o)
        {
            var temp = o as Workshop_Workshop;

            return Workshop_Workshop.AddOrUpdate(temp).WorkshopId > 0;
        }

        #endregion

        #region Event

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int price;
                int.TryParse(Price.Text, out price);

                SaveData(new Workshop_Workshop
                {
                    WorkshopId = Edit.Content.Equals(EditText.Edit) ? 0 : (MasterPage.selectedItemRow as Workshop_Workshop).WorkshopId,
                    Name = Name.Text,
                    StartDate = StartDate.SelectedDate.Value,
                    EndDate = EndDate.SelectedDate.Value,
                    TermId = TermId.SelectedValue.ToString().ToInt(),
                    TeacherId = TeacherId.SelectedValue.ToString().ToInt() > 0 ?
                            (int?)TeacherId.SelectedValue.ToString().ToInt() : null,
                    Price = price,
                    NumberOfSession = NumberOfSession.Text.ToInt(),
                    TeacherPortion = TeacherPortion.Text.ToInt(),
                });
                ClearForm();
                MasterPage.newFrameGrid.SearchGrid();

            }
            catch (Exception ex)
            {
                ErrorPerview.ShowError(ex);
            }
        }



        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Workshop_Workshop t = MasterPage.selectedItemRow as Workshop_Workshop;// this.DataGrid.SelectedItem as Workshop_Workshop;

            if (t == null)
                return;
            if (MessageBox.Show("آیا از حذف اطمینان دارید؟", "حذف", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                Workshop_Workshop.Remove(t.WorkshopId);

            ClearForm();
            MasterPage.newFrameGrid.SearchGrid();

        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

            TermId.ItemsSource = Workshop_Term.GetAll();
            TeacherId.ItemsSource = Workshop_Teacher.GetAll();
        }
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (Edit.Content.Equals(EditText.Edit))//edit event(prepeare for editing)
            {
                if (MasterPage.selectedItemRow == null)
                {
                    MessageBox.Show("سطری انتخاب نشده است!");
                    return;
                }
                BindData(MasterPage.selectedItemRow);
                Edit.Content = EditText.Cancel;
            }
            else//Cancle button click
            {
                ClearForm();
                Edit.Content = EditText.Edit;
            }
        }
        #endregion



    }
}
