using Microsoft.Win32;
using System.Windows;

namespace MakeConfigFile
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MakeConfigFileForm : Window
    {
        public MakeConfigFileForm()
        {
            InitializeComponent();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            var selectConfigFile = new SaveFileDialog();
            selectConfigFile.FileName = "Config"; // Default file name
            selectConfigFile.DefaultExt = ".dat"; // Default file extension
            selectConfigFile.Filter = "Config file (.dat)|*.dat"; // Filter files by extension 


            bool? isOpend = null;
            try { isOpend = selectConfigFile.ShowDialog(); }
            catch { }
            if (isOpend.HasValue && isOpend.Value)
            {
                string conStr = MakeConnectionString();
                Business.Setting.Settings.SaveConnectionString(conStr, selectConfigFile.FileName);
            }
        }

        private string MakeConnectionString()
        {
            if (IsWindows.IsChecked.Value)
                return string.Format("Server = {0}; Database = {1}; Trusted_Connection = True;", Server.Text, Database.Text);
            else
                return string.Format("Server={0};Database={1};User Id={2}; Password={3};", Server.Text, Database.Text, Username.Text, Password.Text);
        }

        private void IsWindows_Checked(object sender, RoutedEventArgs e)
        {
            if (IsWindows.IsChecked.Value)
            {
                Username.Visibility = Password.Visibility = Visibility.Hidden;
                Username.Text = Password.Text = "";
            }
            else
            {

                Username.Visibility = Password.Visibility = Visibility.Visible;
            }
        }
    }
}
