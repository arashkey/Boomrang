using System.IO;
using System.Windows;

namespace BoomrangInc
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            if (!File.Exists( Business.Setting.Settings.ConnectionStringFilePath))
            {
                Business.Setting.Settings.SaveConnectionString("Data Source=.\\MSSQLBOOMRANG;Initial Catalog=boomrang;Persist Security Info=True;User ID=sa;Password=@rash@li@na");

            }
        }
    }
}
