using BoomrangInc.Classes;
using Business;
using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
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
    public partial class PersonAddPage : Page, IBaseInterfaceAdd
    {
        private List<PersonInfo_Email> emails;
        private List<PersonInfo_Address> address;
        private List<PersonInfo_Phone> phones;
        byte[] picture;
        int personId;

        public PersonAddPage()
        {
            InitializeComponent();
        }

        #region Method

        public void ClearForm()
        {
            Name.Clear();
            Family.Clear();
            Father.Clear();
            Mother.Clear();
            DateOfBirth.SelectedDate = null;
            Gender.SelectedIndex = 0;
            TypeOfRelation.Clear();
            IsInViber.SelectedIndex = 0;
            NationalNumber.Clear();
            LiveWith.Clear();
            TakeChild.Clear();

            PictureShow.Source = null;
            picture = null;

            ClearAddresss(true);
            ClearPhones(true);
            ClearEmails(true);
            Edit.Content = (EditText.Edit);
        }

        private void ClearEmails(bool isWithGrid)
        {
            if (isWithGrid)
            {
                emails = new List<PersonInfo_Email>();
                EmailsSearchGrid();
            }
            Email.Clear();
            EmailDescription.Clear();
        }

        private void ClearPhones(bool isWithGrid)
        {
            if (isWithGrid)
            {
                phones = new List<PersonInfo_Phone>();
                PhonesSearchGrid();
            }
            Phone.Clear();
            PhoneDescription.Clear();
        }

        private void ClearAddresss(bool isWithGrid)
        {
            if (isWithGrid)
            {
                address = new List<PersonInfo_Address>();
                AddresssSearchGrid();
            }
            Address.Clear();
            ZipCode.Clear();
            AddressDescription.Clear();
        }

        public void BindData(object data)
        {
            PersonInfo_Person t = data as Business.PersonInfo_Person;
            Name.Text = t.Name;
            Family.Text = t.Family;
            Father.Text = t.Father;
            Mother.Text = t.Mother;
            DateOfBirth.SelectedDate = t.DateOfBirth;
            Gender.SelectedIndex = t.Gender ? 1 : 0;
            TypeOfRelation.Text = t.TypeOfRelation;
            IsInViber.SelectedIndex = t.IsInViber.HasValue && t.IsInViber.Value ? 1 : 0;
            NationalNumber.Text = t.NationalNumber;
            LiveWith.Text = t.LiveWith;
            TakeChild.Text = t.TakeChild;
            picture = t.Picture;
            PictureShow.Source = LoadImage(picture);
            emails = PersonInfo_Email.Search(new PersonInfo_Email { PersonId = t.PersonId }).ToList();
            phones = PersonInfo_Phone.Search(new PersonInfo_Phone { PersonId = t.PersonId }).ToList();
            address = PersonInfo_Address.Search(new PersonInfo_Address { PersonId = t.PersonId }).ToList();
            AddresssSearchGrid();
            PhonesSearchGrid();
            EmailsSearchGrid();
        }

        public object GetData(int id)
        {
            return PersonInfo_Person.GetById(id);
        }

        public bool SaveData(object o)
        {
            personId = PersonInfo_Person.AddOrUpdate(o as PersonInfo_Person).PersonId;
            return personId > 0;
        }

        private static void RemoveAllDependentInfo(int personId)
        {
            foreach (var a in Business.PersonInfo_Address.Search(new PersonInfo_Address { PersonId = personId }).ToList())
                PersonInfo_Address.Remove(a.AddressId);

            foreach (var em in Business.PersonInfo_Email.Search(new PersonInfo_Email { PersonId = personId }).ToList())
                PersonInfo_Email.Remove(em.EmailId);

            foreach (var p in Business.PersonInfo_Phone.Search(new PersonInfo_Phone { PersonId = personId }).ToList())
                PersonInfo_Phone.Remove(p.PhoneId);
        }

        private static BitmapImage LoadImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0) return null;
            var image = new BitmapImage();
            using (var mem = new System.IO.MemoryStream(imageData))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();
            return image;
        }
        public static Bitmap ResizeBitmap(System.Drawing.Image source, float scale, InterpolationMode quality)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            // Figure out the new size.
            var width = (int)(source.Width * scale);
            var height = (int)(source.Height * scale);

            // Create the new bitmap.
            // Note that Bitmap has a resize constructor, but you can't control the quality.
            var bmp = new Bitmap(width, height);

            using (var g = Graphics.FromImage(bmp))
            {
                g.InterpolationMode = quality;
                g.DrawImage(source, new System.Drawing.Rectangle(0, 0, width, height));
                g.Save();
            }

            return bmp;
        }
        #endregion

        #region Event

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Name.Text = Name.Text.Trim();
                Family.Text = Family.Text.Trim();

                if (!ValidateInputData())
                    return;
                SaveData(new PersonInfo_Person
                {
                    PersonId = Edit.Content.Equals(EditText.Edit) ? 0 : (MasterPage.selectedItemRow as PersonInfo_Person).PersonId,
                    Name = Name.Text,
                    Family = Family.Text,
                    Father = Father.Text,
                    Mother = Mother.Text,
                    DateOfBirth = DateOfBirth.SelectedDate.Value,
                    Gender = Gender.SelectedIndex == 1,
                    TypeOfRelation = TypeOfRelation.Text,
                    IsInViber = IsInViber.SelectedIndex == 1,
                    NationalNumber = NationalNumber.Text,
                    LiveWith = LiveWith.Text,
                    TakeChild = TakeChild.Text,
                    Picture = picture,
                });

                #region Upadte Email Phone Address
                if (personId > 0)
                {
                    RemoveAllDependentInfo(personId);

                    if (address != null)
                        foreach (var a in address)
                        {
                            a.AddressId = 0;
                            a.PersonId = personId;
                            PersonInfo_Address.AddOrUpdate(a);
                        }

                    if (phones != null)
                        foreach (var a in phones)
                        {
                            a.PhoneId = 0;
                            a.PersonId = personId;
                            PersonInfo_Phone.AddOrUpdate(a);
                        }

                    if (emails != null)
                        foreach (var a in emails)
                        {
                            a.EmailId = 0;
                            a.PersonId = personId;
                            PersonInfo_Email.AddOrUpdate(a);
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

        private bool ValidateInputData()
        {
            if (Edit.Content.Equals(EditText.Edit))
            {
                if (PersonInfo_Person.Search(new PersonInfo_Person { NationalNumber = NationalNumber.Text }).Count() > 0)
                {
                    MessageBox.Show("شخصی با این کد ملی قبلا ثبت شده است");
                    return false;
                }
                var checkPerson = new PersonInfo_Person
                {
                    Name = Name.Text,
                    Family = Family.Text,
                };
                if (PersonInfo_Person.Search(checkPerson).Count() > 0)
                {
                    return MessageBox.Show("شخصی قبلا با این نام و نام خانوادگی ثبت شده، آیا از ثبت اطمینان دارید؟"
                        , "شخص تکراری", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes;

                }
            }
            return true;
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            PersonInfo_Person t = MasterPage.selectedItemRow as PersonInfo_Person;// this.DataGrid.SelectedItem as PersonInfo_Person;

            if (t == null)
                return;
            if (MessageBox.Show("آیا از حذف اطمینان دارید؟", "حذف", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                RemoveAllDependentInfo(t.PersonId);
                PersonInfo_Person.Remove(t.PersonId);
            }
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

        private void Picture_Click(object sender, RoutedEventArgs e)
        {
            var selectImageFile = new OpenFileDialog();
            selectImageFile.FileName = ""; // Default file name
            selectImageFile.DefaultExt = ".jpg"; // Default file extension
            selectImageFile.Filter = "Image file (.jpg)|*.jpg"; // Filter files by extension 


            bool? isOpend = null;
            try { isOpend = selectImageFile.ShowDialog(); }
            catch { }
            if (isOpend.HasValue && isOpend.Value)
            {
                using (var bitmap = System.Drawing.Image.FromFile(selectImageFile.FileName))
                {
                    Bitmap img = ResizeBitmap(bitmap, 100f / bitmap.Width, InterpolationMode.HighQualityBicubic);
                    var bitmapImage = Bitmap2BitmapSource(img);
                    MemoryStream memStream = new MemoryStream();
                    JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
                    encoder.Save(memStream);
                    picture = memStream.GetBuffer();

                    PictureShow.Source = bitmapImage;
                }

            }
        }
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);

        private BitmapSource Bitmap2BitmapSource(Bitmap bitmap)
        {
            IntPtr hBitmap = bitmap.GetHbitmap();
            BitmapSource retval;

            try
            {
                retval = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                             hBitmap,
                             IntPtr.Zero,
                             Int32Rect.Empty,
                             BitmapSizeOptions.FromEmptyOptions());
            }
            finally
            {
                DeleteObject(hBitmap);
            }

            return retval;
        }
        #region Emails Event
        private void EmailsSearchGrid()
        {
            Emails.ItemsSource = null;
            Emails.ItemsSource = emails;
        }
        private void SaveEmail_Click(object sender, RoutedEventArgs e)
        {
            if (emails == null)
                emails = new List<PersonInfo_Email>();
            emails.Add(new PersonInfo_Email { Email = Email.Text, Description = EmailDescription.Text });
            ClearEmails(false);
            EmailsSearchGrid();
        }
        private void DeleteEmail_Click(object sender, RoutedEventArgs e)
        {
            emails.Remove(Emails.SelectedItem as PersonInfo_Email);
            EmailsSearchGrid();
        }

        #endregion

        #region Addresss Event
        private void AddresssSearchGrid()
        {
            Addresss.ItemsSource = null;
            Addresss.ItemsSource = address;
        }
        private void SaveAddress_Click(object sender, RoutedEventArgs e)
        {
            if (address == null)
                address = new List<PersonInfo_Address>();
            address.Add(new PersonInfo_Address
            {
                Address = Address.Text,
                Description = AddressDescription.Text,
                ZipCode = ZipCode.Text,
            });
            AddresssSearchGrid();
            ClearAddresss(false);
        }
        private void DeleteAddress_Click(object sender, RoutedEventArgs e)
        {
            address.Remove(Addresss.SelectedItem as PersonInfo_Address);
            AddresssSearchGrid();
        }
        #endregion

        #region Phones Event
        private void PhonesSearchGrid()
        {
            Phones.ItemsSource = null;
            Phones.ItemsSource = phones;
        }
        private void SavePhone_Click(object sender, RoutedEventArgs e)
        {
            if (phones == null)
                phones = new List<PersonInfo_Phone>();
            phones.Add(new PersonInfo_Phone
            {
                PhoneNumber = Phone.Text,
                Description = PhoneDescription.Text,
            });
            PhonesSearchGrid();
            ClearPhones(false);
        }
        private void DeletePhone_Click(object sender, RoutedEventArgs e)
        {
            phones.Remove(Phones.SelectedItem as PersonInfo_Phone);
            PhonesSearchGrid();
        }
        #endregion


        #endregion


    }
}
