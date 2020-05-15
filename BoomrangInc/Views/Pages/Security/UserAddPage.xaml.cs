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
    public partial class UserAddPage : Page, IBaseInterfaceAdd
    {
        public UserAddPage()
        {
            InitializeComponent();
        }

        #region Method

        public void ClearForm()
        {
            Name.Clear();
            Username.Clear();
            Password.Clear();
            IsSupperAdmin.IsChecked =
            IsActive.IsChecked = false;
            Edit.Content = (EditText.Edit);
        }

        public void BindData(object data)
        {
            Security_User t = data as Security_User;
            Name.Text = t.Name;
            Username.Text = t.Username;
            IsActive.IsChecked = t.IsActive;
            IsSupperAdmin.IsChecked = t.IsSupperAdmin;
        }

        public object GetData(int id)
        {
            return Security_User.GetById(id);
        }

        public bool SaveData(object o)
        {
            return Security_User.AddOrUpdate(o as Security_User).UserId > 0;
        }

        #endregion

        #region Event

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (!Security_User.GetById(Business.GlobalVariable.UserId).IsSupperAdmin)
            {
                MessageBox.Show("شما اجازه ویرایش این قسمت را ندارید");
                return;
            }
            try
            {
                var password = "";
                var isHash = false;
                if (!Edit.Content.Equals(EditText.Edit))//زمان ویرایش این قسمت اجرا می شود
                {
                    if (Password.Text.Length == 0)
                    {
                        isHash = true;
                        password = Security_User.GetById((MasterPage.selectedItemRow as Security_User).UserId).Password;
                    }
                }
                else
                {
                    if (Password.Text.Length == 0)//حتما پسورد باید وارد شود
                    {
                        MessageBox.Show("رمز عبور را باید وارد نمایید.");
                        return;
                    }
                }
                if (!isHash)//در صورتی که قبلا هش نشده باشد هش شده و در متغییر مربوطه ذخیره می شود
                    password = MyUtility.String.Hash(Password.Text);

                SaveData(new Security_User
                {
                    UserId = Edit.Content.Equals(EditText.Edit) ? 0 : (MasterPage.selectedItemRow as Security_User).UserId,
                    Name = Name.Text,
                    Username = Username.Text,
                    Password = password,
                    IsActive = IsActive.IsChecked.Value,
                    IsSupperAdmin = IsSupperAdmin.IsChecked.Value,
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
            if (!Security_User.GetById(Business.GlobalVariable.UserId).IsSupperAdmin)
            {
                MessageBox.Show("شما اجازه ویرایش این قسمت را ندارید");
                return;
            }
            Security_User t = MasterPage.selectedItemRow as Security_User;// this.DataGrid.SelectedItem as Security_User;

            if (t == null)
                return;
            if (MessageBox.Show("آیا از حذف اطمینان دارید؟", "حذف", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                Security_User.Remove(t.UserId);

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
