using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using Business.MyException;
using System.IO;
using MyUtility;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;

namespace Business
{
    public class IBasicClassForInheritance
    {
        public static int Count()
        {
            throw new NotImplementedException();
        }
        //public static PersonInfo_Phone GetById(int id);
        //public static bool Remove(int id);
        //public static PersonInfo_Phone AddOrUpdate(PersonInfo_Phone item);
        //public static List<PersonInfo_Phone> Search(PersonInfo_Phone search);
        //public static List<PersonInfo_Phone> GetAll();
    }
    public static class GlobalVariable
    {
        public static int UserId { get; set; }

    }
    public partial class MyDbContext : DbContext
    {
        static string _connectionString;
        static MyDbContext _db;

        public static string ConnectionString
        {
            get
            {
                if (_connectionString != null) return _connectionString;

                if (File.Exists(Business.Setting.Settings.ConnectionStringFilePath))
                {
                    _connectionString = MyUtility.String.Decript(File.ReadAllText(Business.Setting.Settings.ConnectionStringFilePath));

                    return _connectionString;
                }



                _connectionString = ConfigurationSettings.AppSettings["MyDbConnection"];


                if (_connectionString != null)
                    return _connectionString;

                throw new ConfigFileNotFoundException("Config File Not Found!");
            }
        }

        public static MyDbContext db
        {
            get
            {
                if (_db == null) _db = new MyDbContext(ConnectionString);

                return _db;
            }
        }

        public static void ExecuteSql(string sql)
        {
            //ObjectContext c;
            //var entityConnection = (System.Data.EntityClient.EntityConnection)c.Connection;
            var conn = new SqlConnection(MyDbContext.ConnectionString);
            ConnectionState initialState = conn.State;
            try
            {
                if (initialState != ConnectionState.Open)
                    conn.Open();  // open connection if not already open
                using (DbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                }
            }
            finally
            {
                if (initialState != ConnectionState.Open)
                    conn.Close(); // only close connection if not initially open
            }
        }

    }
    /*
    public partial class Security_User
    {
        public static Security_User CheckUser(string username, string password)
        {
            return MyDbContext.db.Security_User.FirstOrDefault(x =>
                x.Username.Equals(username, StringComparison.OrdinalIgnoreCase) && x.Password == password);
        }

        public static Security_User CheckUserEmailOrUsername(string userOrEmail)
        {
            return MyDbContext.db.Security_User.FirstOrDefault(x =>
                x.Username.Equals(userOrEmail, StringComparison.OrdinalIgnoreCase) ||
                x.Username.Equals(userOrEmail, StringComparison.OrdinalIgnoreCase));
        }
    }

    public partial class Security_RoleUser
    {
        public static bool IsInRole(int userId, string rolesName)
        {
            var roles = rolesName.Split(',');

            var checkRole =
                MyDbContext.db.Security_RoleUser.FirstOrDefault(x => x.UserId == userId && roles.Any(y => y.Equals(x.Security_Role.Name, StringComparison.OrdinalIgnoreCase)));

            return !checkRole.IsNull();
        }
    }
    */
    public partial class Setting_Settings
    {
        public static Setting_Settings GetSetting(string settingName)
        {
            var setting = MyDbContext.db.Setting_Settings.FirstOrDefault(x => x.Name == settingName);

            return setting;
        }
    }
    public partial class Workshop_Teacher
    {
        public string FullName
        {
            get
            {
                return this.Name + " " + this.Family;
            }
        }
    }
    public partial class Workshop_Workshop
    {
        public string StartDatePersian
        {
            get { return MyUtility.Date.GetPersianDate(this.StartDate); }
        }
        public string EndDatePersian
        {
            get { return MyUtility.Date.GetPersianDate(this.EndDate); }
        }
        public string Teacher
        {
            get
            {
                if (!this.TeacherId.HasValue)
                    return "";

                var t = Workshop_Teacher.GetById(this.TeacherId.Value);
                return t.Name + " " + t.Family;
            }
        }
        public string Term
        {
            get
            {
                var t = Workshop_Term.GetById(this.TermId);
                return t.Name;
            }
        }
    }
    public partial class PersonInfo_Person
    {
        public string FullName
        {
            get
            {
                return this.Name + " " + this.Family + " (" + this.NationalNumber + ")";
            }
        }
        public string Addresses
        {
            get
            {
                var addresses = Business.PersonInfo_Address.Search(new PersonInfo_Address { PersonId = this.PersonId }).ToList();
                return string.Join("\r\n",
                    addresses.Select(x => string.Format
                        (
                            "{0} [ {1} کدپستی:({2}) ]",
                            x.Description,
                            x.Address,
                            x.ZipCode
                        )).ToArray());
            }

        }
        public string Emails
        {
            get
            {
                var emails = Business.PersonInfo_Email.Search(new PersonInfo_Email { PersonId = this.PersonId }).ToList();
                return string.Join("\r\n",
                       emails.Select(x => string.Format
                        (
                            "{0} [ {1} ]",
                            x.Description,
                            x.Email
                        )).ToArray());
            }
        }
        public string Phones
        {
            get
            {
                var phones = Business.PersonInfo_Phone.Search(new PersonInfo_Phone { PersonId = this.PersonId }).ToList();
                return string.Join("\r\n",
                    phones.Select(x => string.Format
                        (
                            "{0} [ {1} ]",
                            x.Description,
                            x.PhoneNumber
                        )).ToArray());
            }
        }

        public string DateOfBirthPersian
        {
            get
            {
                return MyUtility.Date.GetPersianDate(this.DateOfBirth);
            }
        }
        public string GenderPersian
        {
            get
            {
                return this.Gender ? "مرد" : "زن";
            }
        }
    }
    public partial class Registry_Pay
    {
        public string DateOfPayPersian
        {
            get
            {
                return MyUtility.Date.GetPersianDate(this.DateOfPay);
            }
        }
        public string TypeOfPayPersian
        {
            get
            {
                if (this.TypeOfPay.HasValue)
                    return StringEnum.GetTypeOfPayment(this.TypeOfPay.Value);
                return null;
            }
        }

    }

    public partial class Registry_Registery
    {
        public static List<Registry_Registery> NewSearch(Registry_Registery search1, Registry_Registery search2, DateTime? dateOfPayFrom, DateTime? dateOfPayTo)
        {
            Registry_Registery defaultItem = new Registry_Registery();
            var result = from x in MyDbContext.db.Registry_Registery
                         select x;
            if (search1.RegisteryId != defaultItem.RegisteryId)
                result = result.Where(x => x.RegisteryId == search1.RegisteryId);
            if (search1.PersonId != defaultItem.PersonId)
                result = result.Where(x => x.PersonId == search1.PersonId);

            if (search1.DateOfRegister != defaultItem.DateOfRegister)
                result = result.Where(x => x.DateOfRegister >= search1.DateOfRegister);
            if (search2.DateOfRegister != defaultItem.DateOfRegister)
                result = result.Where(x => x.DateOfRegister <= search2.DateOfRegister);

            if (search1.WorkshopId != defaultItem.WorkshopId)
                result = result.Where(x => x.WorkshopId == search1.WorkshopId);
            if (search1.UserChangeId != defaultItem.UserChangeId)
                result = result.Where(x => x.UserChangeId == search1.UserChangeId);

            if (dateOfPayFrom.HasValue)
            {
                result = result.Where(y => y.Registry_Pay.Any(x => x.DateOfPay >= dateOfPayFrom.Value));
            }
            if (dateOfPayTo.HasValue)
            {
                result = result.Where(y => y.Registry_Pay.Any(x => x.DateOfPay <= dateOfPayTo.Value));
            }

            return result.ToList();
        }

        public string DateOfRegisterPersian
        {
            get
            {
                return MyUtility.Date.GetPersianDate(this.DateOfRegister);
            }
        }
        public string Person
        {
            get
            {
                var t = PersonInfo_Person.GetById(this.PersonId);
                return t.Name + " " + t.Family;
            }
        }
        public string Workshop
        {
            get
            {
                var t = Workshop_Workshop.GetById(this.WorkshopId);
                return t.Name;
            }
        }
        private List<Registry_Pay> _paysOfRegister;
        private List<Registry_Pay> PaysOfRegister
        {
            get
            {
                if (_paysOfRegister == null)
                    _paysOfRegister = Business.Registry_Pay.Search(new Registry_Pay { RegisterId = this.RegisteryId }).ToList();
                return _paysOfRegister;
            }
        }
        public string Pays
        {
            get
            {
                return string.Join("\r\n",
                   PaysOfRegister.Select(x => string.Format
                       (
                           "در تاریخ: {0} به شکل: {1} با کد پرداخت: {2} مبلغ: {3} پرداخت شده",
                           x.DateOfPayPersian,
                           x.TypeOfPayPersian,
                           x.AccountNumber,
                           x.Price
                       )).ToArray());
            }
        }
        public string TeacherPortion
        {
            get
            {
                var sum = this.Workshop_Workshop.Price * this.NumberOfSessionRegister / this.Workshop_Workshop.NumberOfSession;
                int tp = this.Workshop_Workshop.TeacherPortion;

                return (sum * tp / 100.0).ToString("0");
            }
        }
        public int SumPayment
        {
            get
            {

                return (this.Registry_Pay.Sum(x => x.Price));

            }
        }
        public int Debt
        {
            get
            {

                return this.Workshop_Workshop.Price * this.NumberOfSessionRegister / this.Workshop_Workshop.NumberOfSession - this.SumPayment - this.Discount;

            }
        }
        public string ColorOfRow//Coloring row if somebody have to pay mony
        {
            get
            {
                var color = "White";
                if (this.Debt > 0)
                    color = "#f15555";
                return color;
            }
        }
        public string ColorOfRowSelect
        {
            get
            {
                var color = "#eeeeee";
                if (this.Debt > 0)
                    color = "#f15555";
                return color;
            }
        }
    }
    public static class StringEnum
    {
        private static readonly Dictionary<int, string> _typeOfPayment = new Dictionary<int, string>();
        public static string GetTypeOfPayment(int id)
        {
            MakeTypeOfPayment();
            string value = null;
            _typeOfPayment.TryGetValue(id, out value);
            return value;
        }

        public static List<string> MakeTypeOfPayment()
        {
            if (_typeOfPayment.Count == 0)
            {
                _typeOfPayment.Add(0, "نقدی");
                _typeOfPayment.Add(1, "کارت خوان");
                _typeOfPayment.Add(2, "کارت به کارت");
            }
            return _typeOfPayment.Values.ToList();
        }
    }
    public partial class Security_User
    {
        public string FullName
        {
            get
            {
                return this.Username;
            }
        }
    }
    public partial class Workshop_Workshop
    {
        public static List<Workshop_Workshop> NewSearch(Workshop_Workshop search1, Workshop_Workshop search2)
        {
            Workshop_Workshop defaultItem = new Workshop_Workshop();
            var result = from x in MyDbContext.db.Workshop_Workshop
                         select x;
            if (search1.WorkshopId != defaultItem.WorkshopId)
                result = result.Where(x => x.WorkshopId == search1.WorkshopId);

            if (search1.StartDate != defaultItem.StartDate)
                result = result.Where(x => x.StartDate >= search1.StartDate);
            if (search2.StartDate != defaultItem.StartDate)
                result = result.Where(x => x.StartDate <= search2.StartDate);

            if (search1.EndDate != defaultItem.EndDate)
                result = result.Where(x => x.EndDate >= search1.EndDate);
            if (search2.EndDate != defaultItem.EndDate)
                result = result.Where(x => x.EndDate <= search2.EndDate);

            if (search1.TermId != defaultItem.TermId)
                result = result.Where(x => x.TermId == search1.TermId);
            if (search1.Name != defaultItem.Name)
                result = result.Where(x => x.Name.Contains(search1.Name));
            if (search1.TeacherId != defaultItem.TeacherId)
                result = result.Where(x => x.TeacherId == search1.TeacherId);

            if (search1.Price != defaultItem.Price)
                result = result.Where(x => x.Price >= search1.Price);
            if (search2.Price != defaultItem.Price)
                result = result.Where(x => x.Price <= search2.Price);

            if (search1.UserChangeId != defaultItem.UserChangeId)
                result = result.Where(x => x.UserChangeId == search1.UserChangeId);

            return result.ToList();
        }
    }

    //public partial class PersonInfo_Person
    //{
    //    //public static List<PersonInfo_Person> NewSearch(PersonInfo_Person search1, PersonInfo_Person search2)
    //    //{
    //    //    PersonInfo_Person defaultItem = new PersonInfo_Person();
    //    //    var result = from x in MyDbContext.db.PersonInfo_Person
    //    //                 select x;
    //    //    if (search1.PersonId != defaultItem.PersonId)
    //    //        result = result.Where(x => x.PersonId == search1.PersonId);
    //    //    if (search1.Name != defaultItem.Name)
    //    //        result = result.Where(x => x.Name.Contains(search1.Name));
    //    //    if (search1.Family != defaultItem.Family)
    //    //        result = result.Where(x => x.Family.Contains(search1.Family));
    //    //    if (search1.Father != defaultItem.Father)
    //    //        result = result.Where(x => x.Father.Contains(search1.Father));
    //    //    if (search1.Mother != defaultItem.Mother)
    //    //        result = result.Where(x => x.Mother.Contains(search1.Mother));

    //    //    if (search1.DateOfBirth != defaultItem.DateOfBirth)
    //    //        result = result.Where(x => x.DateOfBirth >= search1.DateOfBirth);
    //    //    if (search2.DateOfBirth != defaultItem.DateOfBirth)
    //    //        result = result.Where(x => x.DateOfBirth <= search2.DateOfBirth);

    //    //    if (search1.Gender != defaultItem.Gender)
    //    //        result = result.Where(x => x.Gender == search1.Gender);
    //    //    if (search1.TypeOfRelation != defaultItem.TypeOfRelation)
    //    //        result = result.Where(x => x.TypeOfRelation.Contains(search1.TypeOfRelation));
    //    //    if (search1.IsInViber != defaultItem.IsInViber)
    //    //        result = result.Where(x => x.IsInViber == search1.IsInViber);
    //    //    if (search1.NationalNumber != defaultItem.NationalNumber)
    //    //        result = result.Where(x => x.NationalNumber.Contains(search1.NationalNumber));
    //    //    if (search1.UserChangeId != defaultItem.UserChangeId)
    //    //        result = result.Where(x => x.UserChangeId == search1.UserChangeId);
    //    //    return result.ToList();
    //    //}

    //}

}

namespace Business.SpecialQuery
{
    public class AgeRange
    {
        public int Id { get; set; }
        public string Name
        {
            get
            {
                return string.Format("{0} تا {1} سال", FromAge, ToAge);
            }
        }
        public int FromAge { get; set; }
        public int ToAge { get; set; }
    }
    public class SearchReport
    {
        public string Name { get; set; }
        public string Family { get; set; }
        public string NationalNumber { get; set; }
        public string TypeOfRelation { get; set; }
        public int WorkshopId { get; set; }
        public int TermId { get; set; }
        public int AgeRangeId { get; set; }
        //اگر انتخاب نشده بود نال وگرنه برای مرد درست و برای زن غلط بر می گردونه
        public bool? IsInViber { get; set; }

        public static List<AgeRange> GetRange()
        {
            var output = new List<AgeRange>();
            output.Add(new AgeRange
            {
                Id = 1,
                FromAge = 3,
                ToAge = 5,
            });
            output.Add(new AgeRange
            {
                Id = 2,
                FromAge = 5,
                ToAge = 6,
            });
            output.Add(new AgeRange
            {
                Id = 3,
                FromAge = 6,
                ToAge = 8,
            });
            output.Add(new AgeRange
            {
                Id = 4,
                FromAge = 8,
                ToAge = 12,
            });
            output.Add(new AgeRange
            {
                Id = 5,
                FromAge = 12,
                ToAge = 16,
            });
            output.Add(new AgeRange
            {
                Id = 6,
                FromAge = 14,
                ToAge = 17,
            });
            output.Add(new AgeRange
            {
                Id = 7,
                FromAge = 17,
                ToAge = 60,
            });
            return output;
        }
    }
    public class ReportResult
    {
        public int PersonId { get; set; }
        public string Name { get; set; } // Name
        public string Family { get; set; } // Family
        public string Father { get; set; } // Father
        public string Mother { get; set; } // Mother
        public string DateOfBirthPersian { get; set; } // DateOfBirth
        public string GenderPersian { get; set; } // Gender
                                                  //  public string TypeOfRelation { get; set; } // TypeOfRelation
                                                  //  public bool? IsInViber { get; set; } // IsInViber
        public string NationalNumber { get; set; } // NationalNumber
                                                   // public byte[] Picture { get; set; } // Picture
                                                   //    public string LiveWith { get; set; } // LiveWith
                                                   //  public string TakeChild { get; set; } // TakeChild
        public int WorkshopId { get; set; } // WorkshopId (Primary key)
        public string StartDatePersian { get; set; } // StartDate
                                                     //  public string EndDatePersian { get; set; } // EndDate
        public int TermId { get; set; } // TermId
        public string WorkshopName { get; set; } // Name
                                                 //  public int? TeacherId { get; set; } // TeacherId
        public int Price { get; set; } // Price
        public int NumberOfSession { get; set; } // NumberOfSession
        public string Term { get; set; }
        public int Debt { get; set; }
    }
    public static class SpecialQuery
    {
        //public static List<ReportResult> GetReport(SearchReport searchParameter)
        //{
        //    var rang = SearchReport.GetRange().FirstOrDefault(x => x.Id == searchParameter.AgeRangeId);
        //    var rangFrom = rang == null ? DateTime.Now : DateTime.Now.AddYears(-1 * rang.FromAge);
        //    var rangTo = rang == null ? DateTime.Now : DateTime.Now.AddYears(-1 * rang.ToAge);

        //    var result = (from x in MyDbContext.db.PersonInfo_Person
        //                  join y in MyDbContext.db.Registry_Registery on x.PersonId equals y.PersonId
        //                  join z in MyDbContext.db.Workshop_Workshop on y.WorkshopId equals z.WorkshopId
        //                  join k in MyDbContext.db.Workshop_Term on z.TermId equals k.TermId
        //                  where
        //                  (searchParameter.AgeRangeId == -1 || (x.DateOfBirth <= rangFrom && x.DateOfBirth >= rangTo))
        //                  && (searchParameter.Name.Length == 0 || x.Name.Contains(searchParameter.Name))
        //                  && (searchParameter.Family.Length == 0 || x.Family.Contains(searchParameter.Family))
        //                  && (searchParameter.TypeOfRelation.Length == 0 || x.TypeOfRelation.Contains(searchParameter.TypeOfRelation))
        //                  && (searchParameter.NationalNumber.Length == 0 || x.NationalNumber.Contains(searchParameter.NationalNumber))
        //                  && (searchParameter.IsInViber == null || x.IsInViber == searchParameter.IsInViber.Value)
        //                  && (searchParameter.WorkshopId == -1 || z.WorkshopId == searchParameter.WorkshopId)

        //                  select new { x, y, z, k }).ToList();
        //    return result.Select(x => new ReportResult
        //    {
        //        PersonId = x.x.PersonId,
        //        Name = x.x.Name,
        //        DateOfBirthPersian = x.x.DateOfBirthPersian,
        //        EndDatePersian = x.z.EndDatePersian,
        //        Family = x.x.Family,
        //        Father = x.x.Father,
        //        GenderPersian = x.x.GenderPersian,
        //        IsInViber = x.x.IsInViber,
        //        LiveWith = x.x.LiveWith,
        //        Mother = x.x.Mother,
        //        NationalNumber = x.x.NationalNumber,
        //        NumberOfSession = x.z.NumberOfSession,
        //        Picture = x.x.Picture,
        //        Price = x.z.Price,
        //        StartDatePersian = x.z.StartDatePersian,
        //        TakeChild = x.x.TakeChild,
        //        TeacherId = x.z.TeacherId,
        //        TermId = x.z.TermId,
        //        TypeOfRelation = x.x.TypeOfRelation,
        //        WorkshopId = x.z.WorkshopId,
        //        WorkshopName = x.z.Name,
        //        Term = x.k.Name,
        //        Debt = x.y.Debt,
        //    }).ToList();
        //}

        public static List<ReportResult> GetReportWithWorkshop(SearchReport searchParameter)
        {
            var rang = SearchReport.GetRange().FirstOrDefault(x => x.Id == searchParameter.AgeRangeId);
            var rangFrom = rang == null ? DateTime.Now : DateTime.Now.AddYears(-1 * rang.FromAge);
            var rangTo = rang == null ? DateTime.Now : DateTime.Now.AddYears(-1 * rang.ToAge);

            var result = (from person in MyDbContext.db.PersonInfo_Person
                          join registery in MyDbContext.db.Registry_Registery on person.PersonId equals registery.PersonId
                          join workshop in MyDbContext.db.Workshop_Workshop on registery.WorkshopId equals workshop.WorkshopId
                          join term in MyDbContext.db.Workshop_Term on workshop.TermId equals term.TermId
                          where
                          (searchParameter.AgeRangeId == -1 || (person.DateOfBirth <= rangFrom && person.DateOfBirth >= rangTo))
                          && (searchParameter.Name.Length == 0 || person.Name.Contains(searchParameter.Name))
                          && (searchParameter.Family.Length == 0 || person.Family.Contains(searchParameter.Family))
                          && (searchParameter.TypeOfRelation.Length == 0 || person.TypeOfRelation.Contains(searchParameter.TypeOfRelation))
                          && (searchParameter.NationalNumber.Length == 0 || person.NationalNumber.Contains(searchParameter.NationalNumber))
                          && (searchParameter.IsInViber == null || person.IsInViber == searchParameter.IsInViber.Value)
                          && (searchParameter.WorkshopId == -1 || workshop.WorkshopId == searchParameter.WorkshopId)
                          && (searchParameter.TermId == -1 || term.TermId == searchParameter.TermId)

                          select new { person, registery, workshop, term }).ToList();
            return result.Select(r => new ReportResult
            {
                PersonId = r.person.PersonId,
                Name = r.person.Name,
                Family = r.person.Family,
                Father = r.person.Father,
                Mother = r.person.Mother,
                DateOfBirthPersian = r.person.DateOfBirthPersian,
                GenderPersian = r.person.GenderPersian,
                NationalNumber = r.person.NationalNumber,
                TermId = r.term.TermId,
                NumberOfSession = r.workshop.NumberOfSession,
                Price = r.workshop.Price,
                StartDatePersian = r.workshop.StartDatePersian,
                WorkshopId = r.workshop.WorkshopId,
                WorkshopName = r.workshop.Name,
                Term = r.term.Name,
                Debt = r.registery.Debt,
            }).ToList();
        }

    }
}