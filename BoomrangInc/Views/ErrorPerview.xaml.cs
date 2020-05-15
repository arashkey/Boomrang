using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.IO;

namespace BoomrangInc.Views
{
    /// <summary>
    /// Interaction logic for ErrorPerview.xaml
    /// </summary>
    public partial class ErrorPerview : Window
    {
        Exception ex;
        public ErrorPerview()
        {
            InitializeComponent();
        }

        internal static void ShowError(Exception ex)
        {
            var err = new ErrorPerview();

            err.ex = ex;

            err.ShowDialog();
        }
        internal static void ShowError(string message)
        {
            var err = new ErrorPerview();

            err.ex = new Exception(message);

            err.ShowDialog();
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            //Application.Current.Shutdown();
            Close();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            // Configure open file dialog box
            var selectErrorFile = new SaveFileDialog();
            selectErrorFile.FileName = "Error"; // Default file name
            selectErrorFile.DefaultExt = ".txt"; // Default file extension
            selectErrorFile.Filter = "Text file (.txt)|*.txt"; // Filter files by extension 


            bool? isOpend = null;
            try { isOpend = selectErrorFile.ShowDialog(); }
            catch { }
            if (isOpend.HasValue && isOpend.Value)
            {
                SaveError(selectErrorFile.FileName);
            }

        }

        private void SaveError(string errorFilePath)
        {
            if (ex == null) return;

            try
            {
                File.AppendAllText(errorFilePath, GetErrorMessage());
            }
            catch (Exception ex2)
            {
                MessageBox.Show(ex2.Message);
            }
        }

        private string GetErrorMessage()
        {
            if (ex == null) return null;


            var str = new StringBuilder();

            str.AppendLine("Error in:" + DateTime.Now);
            str.AppendFormat
                (
                    "{0}\r\n{1} \r\n --------------------- \r\n Inner Exception:\r\n {2}  \r\n \t <====================> \r\n",
                    ex.Message,
                    ex.StackTrace,
                    ex.InnerException
                );

            return str.ToString();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (ex != null)
                ShowErrorMessage.Content = ex.Message;
        }


    }
}
