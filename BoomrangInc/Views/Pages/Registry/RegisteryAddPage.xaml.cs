using MyUtility;
using BoomrangInc.Classes;
using Business;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Linq;

namespace BoomrangInc.Views.Pages
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class RegisteryAddPage : Page, IBaseInterfaceAdd
    {
        private List<Registry_Pay> pays;
        int registeryId;
        public RegisteryAddPage()
        {
            InitializeComponent();
        }

        #region Method

        public void ClearForm()
        {
            DateOfRegister.SelectedDate = DateTime.Now;
            NumberOfSessionRegister.Clear();
            DescriptionOfSessionRegister.Clear();
            Discount.Clear();
            WorkshopId.SelectedIndex =
                PersonId.SelectedIndex = -1;
            ClearPays(true);
            Edit.Content = (EditText.Edit);
        }

        private void ClearPays(bool isWithGrid)
        {
            if (isWithGrid)
            {
                pays = new List<Registry_Pay>();
                PaysSearchGrid();
            }
            EditPayImage.Source = new BitmapImage(new Uri(@"pack://application:,,,/BoomrangInc;component/Images/ButtonIcons/edit.png"));
            EditPay.ToolTip = EditText.Edit;
            DateOfPay.SelectedDate = null;
            TypeOfPay.SelectedIndex = -1;
            AccountNumber.Clear();
            Price.Clear();
        }
        public void BindData(object data)
        {
            Registry_Registery t = data as Registry_Registery;
            DateOfRegister.SelectedDate = t.DateOfRegister;
            WorkshopId.SelectedValue = t.WorkshopId;
            PersonId.SelectedValue = t.PersonId;
            NumberOfSessionRegister.Text = t.NumberOfSessionRegister.ToString();
            DescriptionOfSessionRegister.Text = t.DescriptionOfSessionRegister;
            Discount.Text = t.Discount.ToString();

            pays = Registry_Pay.Search(new Registry_Pay { RegisterId = t.RegisteryId }).ToList();
            PaysSearchGrid();
        }

        public object GetData(int id)
        {
            return Registry_Registery.GetById(id);
        }

        public bool SaveData(object o)
        {
            registeryId = Registry_Registery.AddOrUpdate(o as Registry_Registery).RegisteryId;
            return registeryId > 0;
        }

        #endregion

        #region Event

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                var number = NumberOfSessionRegister.Text.ToInt();
                if (number > (WorkshopId.SelectedItem as Workshop_Workshop).NumberOfSession)
                {
                    MessageBox.Show("تعداد جلسات انتخابی بیش از کل جلسات کلاس است");
                    return;
                }

                SaveData(new Registry_Registery
                {
                    RegisteryId = Edit.Content.Equals(EditText.Edit) ? 0 : (MasterPage.selectedItemRow as Registry_Registery).RegisteryId,
                    DateOfRegister = DateOfRegister.SelectedDate,
                    WorkshopId = WorkshopId.SelectedValue.ToString().ToInt(),
                    PersonId = PersonId.SelectedValue.ToString().ToInt(),
                    NumberOfSessionRegister = number,
                    DescriptionOfSessionRegister = DescriptionOfSessionRegister.Text,
                    Discount = Discount.Text.ToInt(),
                });
                #region Upadte Pays
                if (registeryId > 0)
                {
                    foreach (var a in Business.Registry_Pay.Search(new Registry_Pay { RegisterId = registeryId }).ToList())
                        Registry_Pay.Remove(a.PayId);

                    if (pays != null)
                        foreach (var a in pays)
                        {
                            a.PayId = 0;
                            a.RegisterId = registeryId;
                            Registry_Pay.AddOrUpdate(a);
                        }
                }
                #endregion
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
            Registry_Registery t = MasterPage.selectedItemRow as Registry_Registery;// this.DataGrid.SelectedItem as Registry_Registery;

            if (t == null)
                return;
            if (MessageBox.Show("آیا از حذف اطمینان دارید؟", "حذف", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                Registry_Registery.Remove(t.RegisteryId);

            ClearForm();
            MasterPage.newFrameGrid.SearchGrid();

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

            PersonId.ItemsSource = PersonInfo_Person.GetAll();
            WorkshopId.ItemsSource = Workshop_Workshop.GetAll();
            DateOfRegister.SelectedDate = DateTime.Now;
            TypeOfPay.ItemsSource = Business.StringEnum.MakeTypeOfPayment();
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

        #region Pays Event
        private void PaysSearchGrid()
        {
            Pays.ItemsSource = null;
            Pays.ItemsSource = pays;
        }
        private void SavePay_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var newPay = new Registry_Pay
                {
                    DateOfPay = DateOfPay.SelectedDate.Value,
                    TypeOfPay = TypeOfPay.SelectedIndex,
                    AccountNumber = AccountNumber.Text.ToString(),
                    Price = Price.Text.ToInt(),

                };
                var obj = Pays.SelectedItem as Registry_Pay;
                if (obj != null && EditPay.ToolTip.ToString() != EditText.Edit)
                {
                    pays[Pays.SelectedIndex] = newPay;
                }
                else
                {
                    if (pays == null)
                        pays = new List<Registry_Pay>();
                    pays.Add(newPay);
                }
                ClearPays(false);
                PaysSearchGrid();

            }
            catch (Exception ex)
            {
                ErrorPerview.ShowError(ex);
            }
        }
        private void DeletePay_Click(object sender, RoutedEventArgs e)
        {
            pays.Remove(Pays.SelectedItem as Registry_Pay);
            PaysSearchGrid();
        }
        private void EditPay_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var obj = Pays.SelectedItem as Registry_Pay;
                if (obj != null)
                {
                    if (EditPay.ToolTip.ToString() == EditText.Edit)
                    {
                        EditPayImage.Source = new BitmapImage(new Uri(@"pack://application:,,,/BoomrangInc;component/Images/ButtonIcons/cancel.png"));
                        EditPay.ToolTip = EditText.Cancel;

                        DateOfPay.SelectedDate = obj.DateOfPay;
                        TypeOfPay.SelectedIndex = obj.TypeOfPay.HasValue ? obj.TypeOfPay.Value : -1;
                        AccountNumber.Text = obj.AccountNumber;
                        Price.Text = obj.Price.ToString();

                    }
                    else {

                        ClearPays(false);
                    }
                }
                else
                {
                    MessageBox.Show("لطفا سطری را برای ویرایش مبلغ انتخاب کنید");
                }
            }
            catch (Exception ex)
            {
                ErrorPerview.ShowError(ex);
            }
        }
        #endregion

        #endregion


    }
}
