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
    public partial class TeacherAddPage : Page, IBaseInterfaceAdd
    {
        public TeacherAddPage()
        {
            InitializeComponent();
        }

        #region Method

        public void ClearForm()
        {
            Name.Clear();
            Family.Clear();
            Field.Clear();
            Edit.Content = (EditText.Edit);
        }

        public void BindData(object data)
        {
            Workshop_Teacher t = data as Workshop_Teacher;
            Name.Text = t.Name;
            Family.Text = t.Family;
            Field.Text = t.Field;
        }

        public object GetData(int id)
        {
            return Workshop_Teacher.GetById(id);
        }

        public bool SaveData(object o)
        {
            var id = Workshop_Teacher.AddOrUpdate(o as Workshop_Teacher);
            return id.TeacherId > 0;
        }

        #endregion

        #region Event

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            SaveData(new Workshop_Teacher
            {
                TeacherId = Edit.Content.Equals(EditText.Edit) ? 0 : (MasterPage.selectedItemRow as Workshop_Teacher).TeacherId,
                Name = Name.Text,
                Family = Family.Text,
                Field = Field.Text,
            });
            ClearForm();
            MasterPage.newFrameGrid.SearchGrid();

        }



        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Workshop_Teacher t = MasterPage.selectedItemRow as Workshop_Teacher;// this.DataGrid.SelectedItem as Workshop_Teacher;

            if (t == null)
                return;
            if (MessageBox.Show("آیا از حذف اطمینان دارید؟", "حذف", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                Business.Workshop_Teacher.Remove(t.TeacherId);

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
