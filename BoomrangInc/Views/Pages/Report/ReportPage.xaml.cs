using BoomrangInc.Classes;
using Business;
using Business.SpecialQuery;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
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
    public partial class ReportPage : Page, IBaseInterfaceGrid
    {
        private Thread trd;
        private bool _isStopThread;
        public string ErrorMessage { get; set; }
        public ReportPage()
        {
            InitializeComponent();
        }

        #region Method
        public void ClearSearch()
        {
            _isStopThread = true;
        }
        public void SearchGrid()
        {
            try
            {
                trd = new Thread(new ThreadStart(this.rptShow));
                trd.IsBackground = true;
                trd.Start();
            }
            catch (Exception ex)
            {
                Message.Text = "خطا در نمایش گزارش";
                ErrorPerview.ShowError(ex);
            }
        }

        private void rptShow()
        {

            _isStopThread = false;
            if (_isStopThread)
                return;

            LocalReport LocalReport = GettingData();

            if (LocalReport == null)
                return;

            string strRptPath = MyUtility.Basic.AssemblyDirectory + "\\Views\\Pages\\Report\\AllReport.rdlc";

            Dispatcher.Invoke(new Action(() =>
            {
                this.progressBar1.Value = 90;
                Message.Text = "آماده سازی داده ها برای نمایش";

                RptVwr.LocalReport.DataSources.Clear();

                foreach (var item in LocalReport.DataSources)
                {

                    RptVwr.LocalReport.DataSources.Add(item);
                }

                RptVwr.LocalReport.ReportPath = strRptPath;
                //RptVwr.LocalReport.SetParameters(lsRptParam);
                RptVwr.LocalReport.Refresh();
                RptVwr.RefreshReport();
                progressBar1.Value = 100;
                Message.Text = "عملیات با موفقیت انجام شد";
            }));
        }
        private LocalReport GettingData()
        {

            Dispatcher.Invoke(new Action(() =>
            {
                progressBar1.Value = 1;
                Message.Text = "آماده سازی داده ها قسمت اول";
            }));

            LocalReport localReport = new LocalReport();


            localReport.DataSources.Add(new ReportDataSource("DataSetReportResult",
               SpecialQuery.GetReportWithWorkshop(MasterPage.selectedItemRow as SearchReport)
            ));


            if (_isStopThread)
                return null;

            Dispatcher.Invoke(new Action(() =>
            {
                progressBar1.Value += 10;
                Message.Text = "آماده سازی داده ها قسمت دوم";
            }));


            return localReport;
        }


        #endregion


    }
}
