using BoomrangInc.Classes;
using Microsoft.Windows.Controls.Ribbon;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace BoomrangInc.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MasterPage : Window
    {
        //   public static RoutedCommand CustomRoutedCommand = new RoutedCommand();
        public static MasterPage mainWindow;
        public static IBaseInterfaceGrid newFrameGrid;
        public static IBaseInterfaceAdd newFrameAdd;
        public static object selectedItemRow { get; set; }

        public MasterPage()
        {
            InitializeComponent();
            #region For persian datepicker
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(1065);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
            #endregion
        }
        private void ButtonShow_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as RibbonButton;

            if (btn.Tag == null)
                ErrorPerview.ShowError(new Exception("تگ مربوط به " + btn.Content + " ثبت نشده"));
            else
            {
                NavigateToPage(btn.Tag.ToString());
                PageName.Text = ": مدیریت " + btn.Label;
            }
        }
        public void NavigateToPage(string pageName)
        {
            if (mainWindow == null)
                mainWindow = this;

            MasterPage.selectedItemRow = null;
            //ساخت قسمت گرید
            Type t = Type.GetType("BoomrangInc.Views.Pages." + pageName + "AddPage");//برای نمایش صفحه مربوطه اول نوعش رو می گیریم
            if (t == null)//اگه پیداش نکنه خطا می دیم
            {
                ErrorPerview.ShowError(new Exception("صفحه Add " + pageName + " طراحی نشده است"));
                return;
            }
            try
            {
                newFrameAdd = Activator.CreateInstance(t) as IBaseInterfaceAdd;//یک نمونه از روی پیج مربوطه می سازیم
                AddFrame.NavigationService.Navigate(newFrameAdd); //نمایشش می دیم
            }
            catch (Exception ex)
            {
                ErrorPerview.ShowError(ex);
            }

            //ساخت قسمت اضافه کردن
            t = Type.GetType("BoomrangInc.Views.Pages." + pageName + "Page");//برای نمایش صفحه مربوطه اول نوعش رو می گیریم
            if (t == null)//اگه پیداش نکنه خطا می دیم
            {
                ErrorPerview.ShowError(new Exception("صفحه " + pageName + " طراحی نشده است"));
                return;
            }
            try
            {
                newFrameGrid = Activator.CreateInstance(t) as IBaseInterfaceGrid;//یک نمونه از روی پیج مربوطه می سازیم
                GridViewFrame.NavigationService.Navigate(newFrameGrid); //نمایشش می دیم
            }
            catch (Exception ex)
            {
                ErrorPerview.ShowError(ex);
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
#if DEBUG
            Business.GlobalVariable.UserId = 1;
#else
            (new LoginPage()).ShowDialog();
#endif
        }
    }
}
