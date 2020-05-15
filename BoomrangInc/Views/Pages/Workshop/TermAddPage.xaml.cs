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
    public partial class TermAddPage : Page, IBaseInterfaceAdd
    {
        public TermAddPage()
        {
            InitializeComponent();
        }

        #region Method

        public void ClearForm()
        {
            Name.Clear();
            Season.Clear();
            Year.Clear();
            IsCurrentTerm.IsChecked = false;
            Edit.Content = (EditText.Edit);
        }

        public void BindData(object data)
        {
            Workshop_Term t = data as Workshop_Term;
            Name.Text = t.Name;
            Season.Text = t.Season;
            Year.Text = t.Year.ToString();
            IsCurrentTerm.IsChecked = t.IsCurrentTerm;
        }

        public object GetData(int id)
        {
            return Workshop_Term.GetById(id);
        }

        public bool SaveData(object o)
        {
            var temp = o as Workshop_Term;

            return Workshop_Term.AddOrUpdate(temp).TermId > 0;
        }

        #endregion

        #region Event

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int year;
                int.TryParse(Year.Text, out year);

                SaveData(new Workshop_Term
                {
                    TermId = Edit.Content.Equals(EditText.Edit) ? 0 : (MasterPage.selectedItemRow as Workshop_Term).TermId,
                    Name = Name.Text,
                    Season = Season.Text,
                    Year = year,
                    IsCurrentTerm = IsCurrentTerm.IsChecked.Value,
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
            Workshop_Term t = MasterPage.selectedItemRow as Workshop_Term;// this.DataGrid.SelectedItem as Workshop_Term;

            if (t == null)
                return;
            if (MessageBox.Show("آیا از حذف اطمینان دارید؟", "حذف", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                Business.Workshop_Term.Remove(t.TermId);

            ClearForm();
            MasterPage.newFrameGrid.SearchGrid();
            
        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            
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
