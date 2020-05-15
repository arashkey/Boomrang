using Business;
using System;
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
using System.Windows.Shapes;

namespace BoomrangInc.Views
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Window
    {
        const int MaxOfTry = 5;
        bool isLogin = false;
        int numberOfTry = 1;
        public LoginPage()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                //if you donot have active user so the program make it!
                if (Security_User.Search(new Business.Security_User { IsActive = true }).Count() == 0)
                {
                    Security_User.AddOrUpdate(new Security_User
                    {
                        Username = "admin",
                        Password = MyUtility.String.Hash("admin"),
                        IsActive = true,
                        IsSupperAdmin = true,
                        Name = "Admin",
                    });
                }

                var searchUser = Security_User.Search(new Business.Security_User
                {
                    Username = txtUserName.Text.ToLower(),
                    Password = MyUtility.String.Hash(txtPassword.Password),
                    IsActive = true,
                }).ToList();
                if (searchUser.Count > 0)
                {
                    Business.GlobalVariable.UserId = searchUser.FirstOrDefault().UserId;
                    isLogin = true;
                    Close();
                }
                else
                {
                    if (numberOfTry > MaxOfTry)
                        Close();
                    MessageBox.Show(string.Format("نام کاربری و یا رمز عبور اشتباه است. {0} بار از {1} بار تلاش ناموفق", numberOfTry, MaxOfTry));
                    numberOfTry++;
                }

            }
            catch (Exception ex)
            {
                ErrorPerview.ShowError(ex);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

            if (!isLogin)
                Application.Current.Shutdown();

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtUserName.Focus();
        }


    }
}
