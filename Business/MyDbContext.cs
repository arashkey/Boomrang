

// This file was automatically generated.
// Do not make changes directly to this file - edit the template instead.
// 
// The following connection settings were used to generate this file
// 
//     Connection String Name: "MyDbConnectionString"
//     Connection String:      "Data Source=.;Initial Catalog=boomrang;Persist Security Info=True;User ID=sa;Password=123"

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Linq.Dynamic;

namespace Business
{
    // ************************************************************************
    // Database context
    public partial class MyDbContext : DbContext
    {
        public IDbSet<PersonInfo_Address> PersonInfo_Address { get; set; } // Address
        public IDbSet<PersonInfo_Email> PersonInfo_Email { get; set; } // Email
        public IDbSet<PersonInfo_Person> PersonInfo_Person { get; set; } // Person
        public IDbSet<PersonInfo_Phone> PersonInfo_Phone { get; set; } // Phone
        public IDbSet<Registry_Pay> Registry_Pay { get; set; } // Pay
        public IDbSet<Registry_Registery> Registry_Registery { get; set; } // Registery
        public IDbSet<Security_User> Security_User { get; set; } // User
        public IDbSet<Setting_Settings> Setting_Settings { get; set; } // Settings
        public IDbSet<Workshop_Teacher> Workshop_Teacher { get; set; } // Teacher
        public IDbSet<Workshop_Term> Workshop_Term { get; set; } // Term
        public IDbSet<Workshop_Workshop> Workshop_Workshop { get; set; } // Workshop

        static MyDbContext()
        {
            Database.SetInitializer<MyDbContext>(null);
        }

        public MyDbContext()
            : base("Name=MyDbConnectionString")
        {
        }

        public MyDbContext(string connectionString) : base(connectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new PersonInfo_AddressConfiguration());
            modelBuilder.Configurations.Add(new PersonInfo_EmailConfiguration());
            modelBuilder.Configurations.Add(new PersonInfo_PersonConfiguration());
            modelBuilder.Configurations.Add(new PersonInfo_PhoneConfiguration());
            modelBuilder.Configurations.Add(new Registry_PayConfiguration());
            modelBuilder.Configurations.Add(new Registry_RegisteryConfiguration());
            modelBuilder.Configurations.Add(new Security_UserConfiguration());
            modelBuilder.Configurations.Add(new Setting_SettingsConfiguration());
            modelBuilder.Configurations.Add(new Workshop_TeacherConfiguration());
            modelBuilder.Configurations.Add(new Workshop_TermConfiguration());
            modelBuilder.Configurations.Add(new Workshop_WorkshopConfiguration());
        }
    }

    // ************************************************************************
    // POCO classes

    // Address
    public partial class PersonInfo_Address : IBasicClassForInheritance
    {
        public static int Count()
        {
            return MyDbContext.db.PersonInfo_Address.Count();
        }
        public static PersonInfo_Address GetById(int id)
        {
            return MyDbContext.db.PersonInfo_Address.FirstOrDefault(x => x.AddressId == id);
        }
        public static bool Remove(int id)
        {
            try
            {
                MyDbContext.db.PersonInfo_Address.Remove(GetById(id));

                MyDbContext.db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                MyUtility.ExceptionReport.log(ex);
                return false;
            }
        }
        public static PersonInfo_Address AddOrUpdate(PersonInfo_Address item)
        {
            var newItem = GetById(item.AddressId) ?? new PersonInfo_Address();
            newItem.AddressId = item.AddressId;
            newItem.Address = item.Address;
            newItem.PersonId = item.PersonId;
            newItem.ZipCode = item.ZipCode;
            newItem.Description = item.Description;
            newItem.UserChangeId = item.UserChangeId;
            newItem.UserChangeId = GlobalVariable.UserId;

            try
            {
                if (newItem.AddressId <= 0)
                {
                    newItem.AddressId = 0;
                    newItem = MyDbContext.db.PersonInfo_Address.Add(newItem);
                }

                MyDbContext.db.SaveChanges();

                return newItem;
            }
            catch (Exception ex)
            {
                MyUtility.ExceptionReport.log(ex);
                return null;
            }
        }
        public static IQueryable<PersonInfo_Address> Search(PersonInfo_Address search)
        {
            PersonInfo_Address defaultItem = new PersonInfo_Address();
            var result = from x in MyDbContext.db.PersonInfo_Address
                         select x;
            if (search.AddressId != defaultItem.AddressId)
                result = result.Where(x => x.AddressId == search.AddressId);
            if (search.Address != defaultItem.Address)
                result = result.Where(x => x.Address.Contains(search.Address));
            if (search.PersonId != defaultItem.PersonId)
                result = result.Where(x => x.PersonId == search.PersonId);
            if (search.ZipCode != defaultItem.ZipCode)
                result = result.Where(x => x.ZipCode.Contains(search.ZipCode));
            if (search.Description != defaultItem.Description)
                result = result.Where(x => x.Description.Contains(search.Description));
            if (search.UserChangeId != defaultItem.UserChangeId)
                result = result.Where(x => x.UserChangeId == search.UserChangeId);
            return result;
        }
        public static List<PersonInfo_Address> GetData(int maximumRows, int startPageIndex, int startRowIndex)
        {
            return GetData(maximumRows, startPageIndex, startRowIndex, "");
        }
        public static List<PersonInfo_Address> GetData(int maximumRows, int startPageIndex, int startRowIndex, string sortParameter)
        {
            if (string.IsNullOrEmpty(sortParameter))
                sortParameter = "AddressId";
            return MyDbContext.db.PersonInfo_Address.OrderBy(sortParameter).Skip(startRowIndex).Take(maximumRows).ToList();
        }
        public static int GetDataCount(int maximumRows, int startPageIndex, int startRowIndex)
        {
            return MyDbContext.db.PersonInfo_Address.Count();
        }
        public static List<PersonInfo_Address> GetAll()
        {
            return MyDbContext.db.PersonInfo_Address.ToList();
        }

        public int AddressId { get; set; } // AddressId (Primary key)
        public string Address { get; set; } // Address
        public int PersonId { get; set; } // PersonId
        public string ZipCode { get; set; } // ZipCode
        public string Description { get; set; } // Description
        public int UserChangeId { get; set; } // UserChangeId

        // For getting username of last person that chenge the recourd
        public string UsernameOfChange
        { get { return this.UserChangeId <= 0 ? "" : Security_User.GetById(this.UserChangeId).Username; } }

        // Foreign keys
        public virtual PersonInfo_Person PersonInfo_Person { get; set; } //  PersonId - FK_Address_Person
        public virtual Security_User Security_User { get; set; } //  UserChangeId - FK_Address_User
    }

    // Email
    public partial class PersonInfo_Email : IBasicClassForInheritance
    {
        public static int Count()
        {
            return MyDbContext.db.PersonInfo_Email.Count();
        }
        public static PersonInfo_Email GetById(int id)
        {
            return MyDbContext.db.PersonInfo_Email.FirstOrDefault(x => x.EmailId == id);
        }
        public static bool Remove(int id)
        {
            try
            {
                MyDbContext.db.PersonInfo_Email.Remove(GetById(id));

                MyDbContext.db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                MyUtility.ExceptionReport.log(ex);
                return false;
            }
        }
        public static PersonInfo_Email AddOrUpdate(PersonInfo_Email item)
        {
            var newItem = GetById(item.EmailId) ?? new PersonInfo_Email();
            newItem.EmailId = item.EmailId;
            newItem.Email = item.Email;
            newItem.PersonId = item.PersonId;
            newItem.Description = item.Description;
            newItem.UserChangeId = item.UserChangeId;
            newItem.UserChangeId = GlobalVariable.UserId;

            try
            {
                if (newItem.EmailId <= 0)
                {
                    newItem.EmailId = 0;
                    newItem = MyDbContext.db.PersonInfo_Email.Add(newItem);
                }

                MyDbContext.db.SaveChanges();

                return newItem;
            }
            catch (Exception ex)
            {
                MyUtility.ExceptionReport.log(ex);
                return null;
            }
        }
        public static IQueryable<PersonInfo_Email> Search(PersonInfo_Email search)
        {
            PersonInfo_Email defaultItem = new PersonInfo_Email();
            var result = from x in MyDbContext.db.PersonInfo_Email
                         select x;
            if (search.EmailId != defaultItem.EmailId)
                result = result.Where(x => x.EmailId == search.EmailId);
            if (search.Email != defaultItem.Email)
                result = result.Where(x => x.Email.Contains(search.Email));
            if (search.PersonId != defaultItem.PersonId)
                result = result.Where(x => x.PersonId == search.PersonId);
            if (search.Description != defaultItem.Description)
                result = result.Where(x => x.Description.Contains(search.Description));
            if (search.UserChangeId != defaultItem.UserChangeId)
                result = result.Where(x => x.UserChangeId == search.UserChangeId);
            return result;
        }
        public static List<PersonInfo_Email> GetData(int maximumRows, int startPageIndex, int startRowIndex)
        {
            return GetData(maximumRows, startPageIndex, startRowIndex, "");
        }
        public static List<PersonInfo_Email> GetData(int maximumRows, int startPageIndex, int startRowIndex, string sortParameter)
        {
            if (string.IsNullOrEmpty(sortParameter))
                sortParameter = "EmailId";
            return MyDbContext.db.PersonInfo_Email.OrderBy(sortParameter).Skip(startRowIndex).Take(maximumRows).ToList();
        }
        public static int GetDataCount(int maximumRows, int startPageIndex, int startRowIndex)
        {
            return MyDbContext.db.PersonInfo_Email.Count();
        }
        public static List<PersonInfo_Email> GetAll()
        {
            return MyDbContext.db.PersonInfo_Email.ToList();
        }

        public int EmailId { get; set; } // EmailId (Primary key)
        public string Email { get; set; } // Email
        public int PersonId { get; set; } // PersonId
        public string Description { get; set; } // Description
        public int UserChangeId { get; set; } // UserChangeId

        // For getting username of last person that chenge the recourd
        public string UsernameOfChange
        { get { return this.UserChangeId <= 0 ? "" : Security_User.GetById(this.UserChangeId).Username; } }

        // Foreign keys
        public virtual PersonInfo_Person PersonInfo_Person { get; set; } //  PersonId - FK_Email_Person
        public virtual Security_User Security_User { get; set; } //  UserChangeId - FK_Email_User
    }

    // Person
    public partial class PersonInfo_Person : IBasicClassForInheritance
    {
        public static int Count()
        {
            return MyDbContext.db.PersonInfo_Person.Count();
        }
        public static PersonInfo_Person GetById(int id)
        {
            return MyDbContext.db.PersonInfo_Person.FirstOrDefault(x => x.PersonId == id);
        }
        public static bool Remove(int id)
        {
            try
            {
                MyDbContext.db.PersonInfo_Person.Remove(GetById(id));

                MyDbContext.db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                MyUtility.ExceptionReport.log(ex);
                return false;
            }
        }
        public static PersonInfo_Person AddOrUpdate(PersonInfo_Person item)
        {
            var newItem = GetById(item.PersonId) ?? new PersonInfo_Person();
            newItem.PersonId = item.PersonId;
            newItem.Name = item.Name;
            newItem.Family = item.Family;
            newItem.Father = item.Father;
            newItem.Mother = item.Mother;
            newItem.DateOfBirth = item.DateOfBirth;
            newItem.Gender = item.Gender;
            newItem.TypeOfRelation = item.TypeOfRelation;
            newItem.IsInViber = item.IsInViber;
            newItem.NationalNumber = item.NationalNumber;
            newItem.UserChangeId = item.UserChangeId;
            newItem.Picture = item.Picture;
            newItem.LiveWith = item.LiveWith;
            newItem.TakeChild = item.TakeChild;
            newItem.UserChangeId = GlobalVariable.UserId;

            try
            {
                if (newItem.PersonId <= 0)
                {
                    newItem.PersonId = 0;
                    newItem = MyDbContext.db.PersonInfo_Person.Add(newItem);
                }

                MyDbContext.db.SaveChanges();

                return newItem;
            }
            catch (Exception ex)
            {
                MyUtility.ExceptionReport.log(ex);
                return null;
            }
        }
        public static IQueryable<PersonInfo_Person> Search(PersonInfo_Person search)
        {
            PersonInfo_Person defaultItem = new PersonInfo_Person();
            var result = from x in MyDbContext.db.PersonInfo_Person
                         select x;
            if (search.PersonId != defaultItem.PersonId)
                result = result.Where(x => x.PersonId == search.PersonId);
            if (search.Name != defaultItem.Name)
                result = result.Where(x => x.Name.Contains(search.Name));
            if (search.Family != defaultItem.Family)
                result = result.Where(x => x.Family.Contains(search.Family));
            if (search.Father != defaultItem.Father)
                result = result.Where(x => x.Father.Contains(search.Father));
            if (search.Mother != defaultItem.Mother)
                result = result.Where(x => x.Mother.Contains(search.Mother));
            if (search.DateOfBirth != defaultItem.DateOfBirth)
                result = result.Where(x => x.DateOfBirth == search.DateOfBirth);
            if (search.Gender != defaultItem.Gender)
                result = result.Where(x => x.Gender == search.Gender);
            if (search.TypeOfRelation != defaultItem.TypeOfRelation)
                result = result.Where(x => x.TypeOfRelation.Contains(search.TypeOfRelation));
            if (search.IsInViber != defaultItem.IsInViber)
                result = result.Where(x => x.IsInViber == search.IsInViber);
            if (search.NationalNumber != defaultItem.NationalNumber)
                result = result.Where(x => x.NationalNumber.Contains(search.NationalNumber));
            if (search.UserChangeId != defaultItem.UserChangeId)
                result = result.Where(x => x.UserChangeId == search.UserChangeId);
            if (search.Picture != defaultItem.Picture)
                result = result.Where(x => x.Picture == search.Picture);
            if (search.LiveWith != defaultItem.LiveWith)
                result = result.Where(x => x.LiveWith.Contains(search.LiveWith));
            if (search.TakeChild != defaultItem.TakeChild)
                result = result.Where(x => x.TakeChild.Contains(search.TakeChild));
            return result;
        }
        public static List<PersonInfo_Person> GetData(int maximumRows, int startPageIndex, int startRowIndex)
        {
            return GetData(maximumRows, startPageIndex, startRowIndex, "");
        }
        public static List<PersonInfo_Person> GetData(int maximumRows, int startPageIndex, int startRowIndex, string sortParameter)
        {
            if (string.IsNullOrEmpty(sortParameter))
                sortParameter = "PersonId";
            return MyDbContext.db.PersonInfo_Person.OrderBy(sortParameter).Skip(startRowIndex).Take(maximumRows).ToList();
        }
        public static int GetDataCount(int maximumRows, int startPageIndex, int startRowIndex)
        {
            return MyDbContext.db.PersonInfo_Person.Count();
        }
        public static List<PersonInfo_Person> GetAll()
        {
            return MyDbContext.db.PersonInfo_Person.ToList();
        }

        public int PersonId { get; set; } // PersonId (Primary key)
        public string Name { get; set; } // Name
        public string Family { get; set; } // Family
        public string Father { get; set; } // Father
        public string Mother { get; set; } // Mother
        public DateTime DateOfBirth { get; set; } // DateOfBirth
        public bool Gender { get; set; } // Gender
        public string TypeOfRelation { get; set; } // TypeOfRelation
        public bool? IsInViber { get; set; } // IsInViber
        public string NationalNumber { get; set; } // NationalNumber
        public int UserChangeId { get; set; } // UserChangeId
        public byte[] Picture { get; set; } // Picture
        public string LiveWith { get; set; } // LiveWith
        public string TakeChild { get; set; } // TakeChild

        // Reverse navigation
        public virtual ICollection<PersonInfo_Address> PersonInfo_Address { get; set; } // Address.FK_Address_Person;
        public virtual ICollection<PersonInfo_Email> PersonInfo_Email { get; set; } // Email.FK_Email_Person;
        public virtual ICollection<PersonInfo_Phone> PersonInfo_Phone { get; set; } // Phone.FK_Phone_Person;
        public virtual ICollection<Registry_Registery> Registry_Registery { get; set; } // Registery.FK_Registery_Person;

        // For getting username of last person that chenge the recourd
        public string UsernameOfChange
        { get { return this.UserChangeId <= 0 ? "" : Security_User.GetById(this.UserChangeId).Username; } }

        // Foreign keys
        public virtual Security_User Security_User { get; set; } //  UserChangeId - FK_Person_User

        public PersonInfo_Person()
        {
            PersonInfo_Address = new List<PersonInfo_Address>();
            PersonInfo_Email = new List<PersonInfo_Email>();
            PersonInfo_Phone = new List<PersonInfo_Phone>();
            Registry_Registery = new List<Registry_Registery>();
        }
    }

    // Phone
    public partial class PersonInfo_Phone : IBasicClassForInheritance
    {
        public static int Count()
        {
            return MyDbContext.db.PersonInfo_Phone.Count();
        }
        public static PersonInfo_Phone GetById(int id)
        {
            return MyDbContext.db.PersonInfo_Phone.FirstOrDefault(x => x.PhoneId == id);
        }
        public static bool Remove(int id)
        {
            try
            {
                MyDbContext.db.PersonInfo_Phone.Remove(GetById(id));

                MyDbContext.db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                MyUtility.ExceptionReport.log(ex);
                return false;
            }
        }
        public static PersonInfo_Phone AddOrUpdate(PersonInfo_Phone item)
        {
            var newItem = GetById(item.PhoneId) ?? new PersonInfo_Phone();
            newItem.PhoneId = item.PhoneId;
            newItem.PhoneNumber = item.PhoneNumber;
            newItem.Description = item.Description;
            newItem.PersonId = item.PersonId;
            newItem.UserChangeId = item.UserChangeId;
            newItem.UserChangeId = GlobalVariable.UserId;

            try
            {
                if (newItem.PhoneId <= 0)
                {
                    newItem.PhoneId = 0;
                    newItem = MyDbContext.db.PersonInfo_Phone.Add(newItem);
                }

                MyDbContext.db.SaveChanges();

                return newItem;
            }
            catch (Exception ex)
            {
                MyUtility.ExceptionReport.log(ex);
                return null;
            }
        }
        public static IQueryable<PersonInfo_Phone> Search(PersonInfo_Phone search)
        {
            PersonInfo_Phone defaultItem = new PersonInfo_Phone();
            var result = from x in MyDbContext.db.PersonInfo_Phone
                         select x;
            if (search.PhoneId != defaultItem.PhoneId)
                result = result.Where(x => x.PhoneId == search.PhoneId);
            if (search.PhoneNumber != defaultItem.PhoneNumber)
                result = result.Where(x => x.PhoneNumber.Contains(search.PhoneNumber));
            if (search.Description != defaultItem.Description)
                result = result.Where(x => x.Description.Contains(search.Description));
            if (search.PersonId != defaultItem.PersonId)
                result = result.Where(x => x.PersonId == search.PersonId);
            if (search.UserChangeId != defaultItem.UserChangeId)
                result = result.Where(x => x.UserChangeId == search.UserChangeId);
            return result;
        }
        public static List<PersonInfo_Phone> GetData(int maximumRows, int startPageIndex, int startRowIndex)
        {
            return GetData(maximumRows, startPageIndex, startRowIndex, "");
        }
        public static List<PersonInfo_Phone> GetData(int maximumRows, int startPageIndex, int startRowIndex, string sortParameter)
        {
            if (string.IsNullOrEmpty(sortParameter))
                sortParameter = "PhoneId";
            return MyDbContext.db.PersonInfo_Phone.OrderBy(sortParameter).Skip(startRowIndex).Take(maximumRows).ToList();
        }
        public static int GetDataCount(int maximumRows, int startPageIndex, int startRowIndex)
        {
            return MyDbContext.db.PersonInfo_Phone.Count();
        }
        public static List<PersonInfo_Phone> GetAll()
        {
            return MyDbContext.db.PersonInfo_Phone.ToList();
        }

        public int PhoneId { get; set; } // PhoneId (Primary key)
        public string PhoneNumber { get; set; } // PhoneNumber
        public string Description { get; set; } // Description
        public int PersonId { get; set; } // PersonId
        public int UserChangeId { get; set; } // UserChangeId

        // For getting username of last person that chenge the recourd
        public string UsernameOfChange
        { get { return this.UserChangeId <= 0 ? "" : Security_User.GetById(this.UserChangeId).Username; } }

        // Foreign keys
        public virtual PersonInfo_Person PersonInfo_Person { get; set; } //  PersonId - FK_Phone_Person
        public virtual Security_User Security_User { get; set; } //  UserChangeId - FK_Phone_User
    }

    // Pay
    public partial class Registry_Pay : IBasicClassForInheritance
    {
        public static int Count()
        {
            return MyDbContext.db.Registry_Pay.Count();
        }
        public static Registry_Pay GetById(int id)
        {
            return MyDbContext.db.Registry_Pay.FirstOrDefault(x => x.PayId == id);
        }
        public static bool Remove(int id)
        {
            try
            {
                MyDbContext.db.Registry_Pay.Remove(GetById(id));

                MyDbContext.db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                MyUtility.ExceptionReport.log(ex);
                return false;
            }
        }
        public static Registry_Pay AddOrUpdate(Registry_Pay item)
        {
            var newItem = GetById(item.PayId) ?? new Registry_Pay();
            newItem.PayId = item.PayId;
            newItem.RegisterId = item.RegisterId;
            newItem.DateOfPay = item.DateOfPay;
            newItem.TypeOfPay = item.TypeOfPay;
            newItem.AccountNumber = item.AccountNumber;
            newItem.Price = item.Price;
            newItem.UserChangeId = item.UserChangeId;
            newItem.UserChangeId = GlobalVariable.UserId;

            try
            {
                if (newItem.PayId <= 0)
                {
                    newItem.PayId = 0;
                    newItem = MyDbContext.db.Registry_Pay.Add(newItem);
                }

                MyDbContext.db.SaveChanges();

                return newItem;
            }
            catch (Exception ex)
            {
                MyUtility.ExceptionReport.log(ex);
                return null;
            }
        }
        public static IQueryable<Registry_Pay> Search(Registry_Pay search)
        {
            Registry_Pay defaultItem = new Registry_Pay();
            var result = from x in MyDbContext.db.Registry_Pay
                         select x;
            if (search.PayId != defaultItem.PayId)
                result = result.Where(x => x.PayId == search.PayId);
            if (search.RegisterId != defaultItem.RegisterId)
                result = result.Where(x => x.RegisterId == search.RegisterId);
            if (search.DateOfPay != defaultItem.DateOfPay)
                result = result.Where(x => x.DateOfPay == search.DateOfPay);
            if (search.TypeOfPay != defaultItem.TypeOfPay)
                result = result.Where(x => x.TypeOfPay == search.TypeOfPay);
            if (search.AccountNumber != defaultItem.AccountNumber)
                result = result.Where(x => x.AccountNumber.Contains(search.AccountNumber));
            if (search.Price != defaultItem.Price)
                result = result.Where(x => x.Price == search.Price);
            if (search.UserChangeId != defaultItem.UserChangeId)
                result = result.Where(x => x.UserChangeId == search.UserChangeId);
            return result;
        }
        public static List<Registry_Pay> GetData(int maximumRows, int startPageIndex, int startRowIndex)
        {
            return GetData(maximumRows, startPageIndex, startRowIndex, "");
        }
        public static List<Registry_Pay> GetData(int maximumRows, int startPageIndex, int startRowIndex, string sortParameter)
        {
            if (string.IsNullOrEmpty(sortParameter))
                sortParameter = "PayId";
            return MyDbContext.db.Registry_Pay.OrderBy(sortParameter).Skip(startRowIndex).Take(maximumRows).ToList();
        }
        public static int GetDataCount(int maximumRows, int startPageIndex, int startRowIndex)
        {
            return MyDbContext.db.Registry_Pay.Count();
        }
        public static List<Registry_Pay> GetAll()
        {
            return MyDbContext.db.Registry_Pay.ToList();
        }

        public int PayId { get; set; } // PayId (Primary key)
        public int RegisterId { get; set; } // RegisterId
        public DateTime DateOfPay { get; set; } // DateOfPay
        public int? TypeOfPay { get; set; } // TypeOfPay
        public string AccountNumber { get; set; } // AccountNumber
        public int Price { get; set; } // Price
        public int UserChangeId { get; set; } // UserChangeId

        // For getting username of last person that chenge the recourd
        public string UsernameOfChange
        { get { return this.UserChangeId <= 0 ? "" : Security_User.GetById(this.UserChangeId).Username; } }

        // Foreign keys
        public virtual Registry_Registery Registry_Registery { get; set; } //  RegisterId - FK_Pay Fee_Registery
        public virtual Security_User Security_User { get; set; } //  UserChangeId - FK_Pay_User
    }

    // Registery
    public partial class Registry_Registery : IBasicClassForInheritance
    {
        public static int Count()
        {
            return MyDbContext.db.Registry_Registery.Count();
        }
        public static Registry_Registery GetById(int id)
        {
            return MyDbContext.db.Registry_Registery.FirstOrDefault(x => x.RegisteryId == id);
        }
        public static bool Remove(int id)
        {
            try
            {
                MyDbContext.db.Registry_Registery.Remove(GetById(id));

                MyDbContext.db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                MyUtility.ExceptionReport.log(ex);
                return false;
            }
        }
        public static Registry_Registery AddOrUpdate(Registry_Registery item)
        {
            var newItem = GetById(item.RegisteryId) ?? new Registry_Registery();
            newItem.RegisteryId = item.RegisteryId;
            newItem.PersonId = item.PersonId;
            newItem.DateOfRegister = item.DateOfRegister;
            newItem.WorkshopId = item.WorkshopId;
            newItem.UserChangeId = item.UserChangeId;
            newItem.NumberOfSessionRegister = item.NumberOfSessionRegister;
            newItem.DescriptionOfSessionRegister = item.DescriptionOfSessionRegister;
            newItem.Discount = item.Discount;
            newItem.UserChangeId = GlobalVariable.UserId;

            try
            {
                if (newItem.RegisteryId <= 0)
                {
                    newItem.RegisteryId = 0;
                    newItem = MyDbContext.db.Registry_Registery.Add(newItem);
                }

                MyDbContext.db.SaveChanges();

                return newItem;
            }
            catch (Exception ex)
            {
                MyUtility.ExceptionReport.log(ex);
                return null;
            }
        }
        public static IQueryable<Registry_Registery> Search(Registry_Registery search)
        {
            Registry_Registery defaultItem = new Registry_Registery();
            var result = from x in MyDbContext.db.Registry_Registery
                         select x;
            if (search.RegisteryId != defaultItem.RegisteryId)
                result = result.Where(x => x.RegisteryId == search.RegisteryId);
            if (search.PersonId != defaultItem.PersonId)
                result = result.Where(x => x.PersonId == search.PersonId);
            if (search.DateOfRegister != defaultItem.DateOfRegister)
                result = result.Where(x => x.DateOfRegister == search.DateOfRegister);
            if (search.WorkshopId != defaultItem.WorkshopId)
                result = result.Where(x => x.WorkshopId == search.WorkshopId);
            if (search.UserChangeId != defaultItem.UserChangeId)
                result = result.Where(x => x.UserChangeId == search.UserChangeId);
            if (search.NumberOfSessionRegister != defaultItem.NumberOfSessionRegister)
                result = result.Where(x => x.NumberOfSessionRegister == search.NumberOfSessionRegister);
            if (search.DescriptionOfSessionRegister != defaultItem.DescriptionOfSessionRegister)
                result = result.Where(x => x.DescriptionOfSessionRegister.Contains(search.DescriptionOfSessionRegister));
            if (search.Discount != defaultItem.Discount)
                result = result.Where(x => x.Discount == search.Discount);
            return result;
        }
        public static List<Registry_Registery> GetData(int maximumRows, int startPageIndex, int startRowIndex)
        {
            return GetData(maximumRows, startPageIndex, startRowIndex, "");
        }
        public static List<Registry_Registery> GetData(int maximumRows, int startPageIndex, int startRowIndex, string sortParameter)
        {
            if (string.IsNullOrEmpty(sortParameter))
                sortParameter = "RegisteryId";
            return MyDbContext.db.Registry_Registery.OrderBy(sortParameter).Skip(startRowIndex).Take(maximumRows).ToList();
        }
        public static int GetDataCount(int maximumRows, int startPageIndex, int startRowIndex)
        {
            return MyDbContext.db.Registry_Registery.Count();
        }
        public static List<Registry_Registery> GetAll()
        {
            return MyDbContext.db.Registry_Registery.ToList();
        }

        public int RegisteryId { get; set; } // RegisteryId (Primary key)
        public int PersonId { get; set; } // PersonId
        public DateTime? DateOfRegister { get; set; } // DateOfRegister
        public int WorkshopId { get; set; } // WorkshopId
        public int UserChangeId { get; set; } // UserChangeId
        public int NumberOfSessionRegister { get; set; } // NumberOfSessionRegister
        public string DescriptionOfSessionRegister { get; set; } // DescriptionOfSessionRegister
        public int Discount { get; set; } // Discount

        // Reverse navigation
        public virtual ICollection<Registry_Pay> Registry_Pay { get; set; } // Pay.FK_Pay Fee_Registery;

        // For getting username of last person that chenge the recourd
        public string UsernameOfChange
        { get { return this.UserChangeId <= 0 ? "" : Security_User.GetById(this.UserChangeId).Username; } }

        // Foreign keys
        public virtual PersonInfo_Person PersonInfo_Person { get; set; } //  PersonId - FK_Registery_Person
        public virtual Workshop_Workshop Workshop_Workshop { get; set; } //  WorkshopId - FK_Registery_Workshop
        public virtual Security_User Security_User { get; set; } //  UserChangeId - FK_Registery_User

        public Registry_Registery()
        {
            NumberOfSessionRegister = 0;
            Discount = 0;
            Registry_Pay = new List<Registry_Pay>();
        }
    }

    // User
    public partial class Security_User : IBasicClassForInheritance
    {
        public static int Count()
        {
            return MyDbContext.db.Security_User.Count();
        }
        public static Security_User GetById(int id)
        {
            return MyDbContext.db.Security_User.FirstOrDefault(x => x.UserId == id);
        }
        public static bool Remove(int id)
        {
            try
            {
                MyDbContext.db.Security_User.Remove(GetById(id));

                MyDbContext.db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                MyUtility.ExceptionReport.log(ex);
                return false;
            }
        }
        public static Security_User AddOrUpdate(Security_User item)
        {
            var newItem = GetById(item.UserId) ?? new Security_User();
            newItem.UserId = item.UserId;
            newItem.Username = item.Username;
            newItem.Password = item.Password;
            newItem.IsActive = item.IsActive;
            newItem.Name = item.Name;
            newItem.UserChangeId = item.UserChangeId;
            newItem.IsSupperAdmin = item.IsSupperAdmin;
            newItem.UserChangeId = GlobalVariable.UserId;

            try
            {
                if (newItem.UserId <= 0)
                {
                    newItem.UserId = 0;
                    newItem = MyDbContext.db.Security_User.Add(newItem);
                }

                MyDbContext.db.SaveChanges();

                return newItem;
            }
            catch (Exception ex)
            {
                MyUtility.ExceptionReport.log(ex);
                return null;
            }
        }
        public static IQueryable<Security_User> Search(Security_User search)
        {
            Security_User defaultItem = new Security_User();
            var result = from x in MyDbContext.db.Security_User
                         select x;
            if (search.UserId != defaultItem.UserId)
                result = result.Where(x => x.UserId == search.UserId);
            if (search.Username != defaultItem.Username)
                result = result.Where(x => x.Username.Contains(search.Username));
            if (search.Password != defaultItem.Password)
                result = result.Where(x => x.Password.Contains(search.Password));
            if (search.IsActive != defaultItem.IsActive)
                result = result.Where(x => x.IsActive == search.IsActive);
            if (search.Name != defaultItem.Name)
                result = result.Where(x => x.Name.Contains(search.Name));
            if (search.UserChangeId != defaultItem.UserChangeId)
                result = result.Where(x => x.UserChangeId == search.UserChangeId);
            if (search.IsSupperAdmin != defaultItem.IsSupperAdmin)
                result = result.Where(x => x.IsSupperAdmin == search.IsSupperAdmin);
            return result;
        }
        public static List<Security_User> GetData(int maximumRows, int startPageIndex, int startRowIndex)
        {
            return GetData(maximumRows, startPageIndex, startRowIndex, "");
        }
        public static List<Security_User> GetData(int maximumRows, int startPageIndex, int startRowIndex, string sortParameter)
        {
            if (string.IsNullOrEmpty(sortParameter))
                sortParameter = "UserId";
            return MyDbContext.db.Security_User.OrderBy(sortParameter).Skip(startRowIndex).Take(maximumRows).ToList();
        }
        public static int GetDataCount(int maximumRows, int startPageIndex, int startRowIndex)
        {
            return MyDbContext.db.Security_User.Count();
        }
        public static List<Security_User> GetAll()
        {
            return MyDbContext.db.Security_User.ToList();
        }

        public int UserId { get; set; } // UserId (Primary key)
        public string Username { get; set; } // Username
        public string Password { get; set; } // Password
        public bool IsActive { get; set; } // IsActive
        public string Name { get; set; } // Name
        public int? UserChangeId { get; set; } // UserChangeId
        public bool IsSupperAdmin { get; set; } // IsSupperAdmin

        // Reverse navigation
        public virtual ICollection<PersonInfo_Address> PersonInfo_Address { get; set; } // Address.FK_Address_User;
        public virtual ICollection<PersonInfo_Email> PersonInfo_Email { get; set; } // Email.FK_Email_User;
        public virtual ICollection<Registry_Pay> Registry_Pay { get; set; } // Pay.FK_Pay_User;
        public virtual ICollection<PersonInfo_Person> PersonInfo_Person { get; set; } // Person.FK_Person_User;
        public virtual ICollection<PersonInfo_Phone> PersonInfo_Phone { get; set; } // Phone.FK_Phone_User;
        public virtual ICollection<Registry_Registery> Registry_Registery { get; set; } // Registery.FK_Registery_User;
        public virtual ICollection<Setting_Settings> Setting_Settings { get; set; } // Settings.FK_Settings_User;
        public virtual ICollection<Workshop_Teacher> Workshop_Teacher { get; set; } // Teacher.FK_Teacher_User;
        public virtual ICollection<Workshop_Term> Workshop_Term { get; set; } // Term.FK_Term_User;
        public virtual ICollection<Workshop_Workshop> Workshop_Workshop { get; set; } // Workshop.FK_Workshop_User;

        public Security_User()
        {
            Username = "N'_'";
            IsSupperAdmin = false;
            PersonInfo_Address = new List<PersonInfo_Address>();
            PersonInfo_Email = new List<PersonInfo_Email>();
            Registry_Pay = new List<Registry_Pay>();
            PersonInfo_Person = new List<PersonInfo_Person>();
            PersonInfo_Phone = new List<PersonInfo_Phone>();
            Registry_Registery = new List<Registry_Registery>();
            Setting_Settings = new List<Setting_Settings>();
            Workshop_Teacher = new List<Workshop_Teacher>();
            Workshop_Term = new List<Workshop_Term>();
            Workshop_Workshop = new List<Workshop_Workshop>();
        }
    }

    // Settings
    public partial class Setting_Settings : IBasicClassForInheritance
    {
        public static int Count()
        {
            return MyDbContext.db.Setting_Settings.Count();
        }
        public static Setting_Settings GetById(int id)
        {
            return MyDbContext.db.Setting_Settings.FirstOrDefault(x => x.SettingsId == id);
        }
        public static bool Remove(int id)
        {
            try
            {
                MyDbContext.db.Setting_Settings.Remove(GetById(id));

                MyDbContext.db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                MyUtility.ExceptionReport.log(ex);
                return false;
            }
        }
        public static Setting_Settings AddOrUpdate(Setting_Settings item)
        {
            var newItem = GetById(item.SettingsId) ?? new Setting_Settings();
            newItem.SettingsId = item.SettingsId;
            newItem.Name = item.Name;
            newItem.Value = item.Value;
            newItem.UserChangeId = item.UserChangeId;
            newItem.UserChangeId = GlobalVariable.UserId;

            try
            {
                if (newItem.SettingsId <= 0)
                {
                    newItem.SettingsId = 0;
                    newItem = MyDbContext.db.Setting_Settings.Add(newItem);
                }

                MyDbContext.db.SaveChanges();

                return newItem;
            }
            catch (Exception ex)
            {
                MyUtility.ExceptionReport.log(ex);
                return null;
            }
        }
        public static IQueryable<Setting_Settings> Search(Setting_Settings search)
        {
            Setting_Settings defaultItem = new Setting_Settings();
            var result = from x in MyDbContext.db.Setting_Settings
                         select x;
            if (search.SettingsId != defaultItem.SettingsId)
                result = result.Where(x => x.SettingsId == search.SettingsId);
            if (search.Name != defaultItem.Name)
                result = result.Where(x => x.Name.Contains(search.Name));
            if (search.Value != defaultItem.Value)
                result = result.Where(x => x.Value.Contains(search.Value));
            if (search.UserChangeId != defaultItem.UserChangeId)
                result = result.Where(x => x.UserChangeId == search.UserChangeId);
            return result;
        }
        public static List<Setting_Settings> GetData(int maximumRows, int startPageIndex, int startRowIndex)
        {
            return GetData(maximumRows, startPageIndex, startRowIndex, "");
        }
        public static List<Setting_Settings> GetData(int maximumRows, int startPageIndex, int startRowIndex, string sortParameter)
        {
            if (string.IsNullOrEmpty(sortParameter))
                sortParameter = "SettingsId";
            return MyDbContext.db.Setting_Settings.OrderBy(sortParameter).Skip(startRowIndex).Take(maximumRows).ToList();
        }
        public static int GetDataCount(int maximumRows, int startPageIndex, int startRowIndex)
        {
            return MyDbContext.db.Setting_Settings.Count();
        }
        public static List<Setting_Settings> GetAll()
        {
            return MyDbContext.db.Setting_Settings.ToList();
        }

        public int SettingsId { get; set; } // SettingsId (Primary key)
        public string Name { get; set; } // Name
        public string Value { get; set; } // Value
        public int UserChangeId { get; set; } // UserChangeId

        // For getting username of last person that chenge the recourd
        public string UsernameOfChange
        { get { return this.UserChangeId <= 0 ? "" : Security_User.GetById(this.UserChangeId).Username; } }

        // Foreign keys
        public virtual Security_User Security_User { get; set; } //  UserChangeId - FK_Settings_User
    }

    // Teacher
    public partial class Workshop_Teacher : IBasicClassForInheritance
    {
        public static int Count()
        {
            return MyDbContext.db.Workshop_Teacher.Count();
        }
        public static Workshop_Teacher GetById(int id)
        {
            return MyDbContext.db.Workshop_Teacher.FirstOrDefault(x => x.TeacherId == id);
        }
        public static bool Remove(int id)
        {
            try
            {
                MyDbContext.db.Workshop_Teacher.Remove(GetById(id));

                MyDbContext.db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                MyUtility.ExceptionReport.log(ex);
                return false;
            }
        }
        public static Workshop_Teacher AddOrUpdate(Workshop_Teacher item)
        {
            var newItem = GetById(item.TeacherId) ?? new Workshop_Teacher();
            newItem.TeacherId = item.TeacherId;
            newItem.Name = item.Name;
            newItem.Family = item.Family;
            newItem.Field = item.Field;
            newItem.UserChangeId = item.UserChangeId;
            newItem.UserChangeId = GlobalVariable.UserId;

            try
            {
                if (newItem.TeacherId <= 0)
                {
                    newItem.TeacherId = 0;
                    newItem = MyDbContext.db.Workshop_Teacher.Add(newItem);
                }

                MyDbContext.db.SaveChanges();

                return newItem;
            }
            catch (Exception ex)
            {
                MyUtility.ExceptionReport.log(ex);
                return null;
            }
        }
        public static IQueryable<Workshop_Teacher> Search(Workshop_Teacher search)
        {
            Workshop_Teacher defaultItem = new Workshop_Teacher();
            var result = from x in MyDbContext.db.Workshop_Teacher
                         select x;
            if (search.TeacherId != defaultItem.TeacherId)
                result = result.Where(x => x.TeacherId == search.TeacherId);
            if (search.Name != defaultItem.Name)
                result = result.Where(x => x.Name.Contains(search.Name));
            if (search.Family != defaultItem.Family)
                result = result.Where(x => x.Family.Contains(search.Family));
            if (search.Field != defaultItem.Field)
                result = result.Where(x => x.Field.Contains(search.Field));
            if (search.UserChangeId != defaultItem.UserChangeId)
                result = result.Where(x => x.UserChangeId == search.UserChangeId);
            return result;
        }
        public static List<Workshop_Teacher> GetData(int maximumRows, int startPageIndex, int startRowIndex)
        {
            return GetData(maximumRows, startPageIndex, startRowIndex, "");
        }
        public static List<Workshop_Teacher> GetData(int maximumRows, int startPageIndex, int startRowIndex, string sortParameter)
        {
            if (string.IsNullOrEmpty(sortParameter))
                sortParameter = "TeacherId";
            return MyDbContext.db.Workshop_Teacher.OrderBy(sortParameter).Skip(startRowIndex).Take(maximumRows).ToList();
        }
        public static int GetDataCount(int maximumRows, int startPageIndex, int startRowIndex)
        {
            return MyDbContext.db.Workshop_Teacher.Count();
        }
        public static List<Workshop_Teacher> GetAll()
        {
            return MyDbContext.db.Workshop_Teacher.ToList();
        }

        public int TeacherId { get; set; } // TeacherId (Primary key)
        public string Name { get; set; } // Name
        public string Family { get; set; } // Family
        public string Field { get; set; } // Field
        public int UserChangeId { get; set; } // UserChangeId

        // Reverse navigation
        public virtual ICollection<Workshop_Workshop> Workshop_Workshop { get; set; } // Workshop.FK_Workshop_Teacher;

        // For getting username of last person that chenge the recourd
        public string UsernameOfChange
        { get { return this.UserChangeId <= 0 ? "" : Security_User.GetById(this.UserChangeId).Username; } }

        // Foreign keys
        public virtual Security_User Security_User { get; set; } //  UserChangeId - FK_Teacher_User

        public Workshop_Teacher()
        {
            Workshop_Workshop = new List<Workshop_Workshop>();
        }
    }

    // Term
    public partial class Workshop_Term : IBasicClassForInheritance
    {
        public static int Count()
        {
            return MyDbContext.db.Workshop_Term.Count();
        }
        public static Workshop_Term GetById(int id)
        {
            return MyDbContext.db.Workshop_Term.FirstOrDefault(x => x.TermId == id);
        }
        public static bool Remove(int id)
        {
            try
            {
                MyDbContext.db.Workshop_Term.Remove(GetById(id));

                MyDbContext.db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                MyUtility.ExceptionReport.log(ex);
                return false;
            }
        }
        public static Workshop_Term AddOrUpdate(Workshop_Term item)
        {
            var newItem = GetById(item.TermId) ?? new Workshop_Term();
            newItem.TermId = item.TermId;
            newItem.Season = item.Season;
            newItem.Year = item.Year;
            newItem.Name = item.Name;
            newItem.IsCurrentTerm = item.IsCurrentTerm;
            newItem.UserChangeId = item.UserChangeId;
            newItem.UserChangeId = GlobalVariable.UserId;

            try
            {
                if (newItem.TermId <= 0)
                {
                    newItem.TermId = 0;
                    newItem = MyDbContext.db.Workshop_Term.Add(newItem);
                }

                MyDbContext.db.SaveChanges();

                return newItem;
            }
            catch (Exception ex)
            {
                MyUtility.ExceptionReport.log(ex);
                return null;
            }
        }
        public static IQueryable<Workshop_Term> Search(Workshop_Term search)
        {
            Workshop_Term defaultItem = new Workshop_Term();
            var result = from x in MyDbContext.db.Workshop_Term
                         select x;
            if (search.TermId != defaultItem.TermId)
                result = result.Where(x => x.TermId == search.TermId);
            if (search.Season != defaultItem.Season)
                result = result.Where(x => x.Season.Contains(search.Season));
            if (search.Year != defaultItem.Year)
                result = result.Where(x => x.Year == search.Year);
            if (search.Name != defaultItem.Name)
                result = result.Where(x => x.Name.Contains(search.Name));
            if (search.IsCurrentTerm != defaultItem.IsCurrentTerm)
                result = result.Where(x => x.IsCurrentTerm == search.IsCurrentTerm);
            if (search.UserChangeId != defaultItem.UserChangeId)
                result = result.Where(x => x.UserChangeId == search.UserChangeId);
            return result;
        }
        public static List<Workshop_Term> GetData(int maximumRows, int startPageIndex, int startRowIndex)
        {
            return GetData(maximumRows, startPageIndex, startRowIndex, "");
        }
        public static List<Workshop_Term> GetData(int maximumRows, int startPageIndex, int startRowIndex, string sortParameter)
        {
            if (string.IsNullOrEmpty(sortParameter))
                sortParameter = "TermId";
            return MyDbContext.db.Workshop_Term.OrderBy(sortParameter).Skip(startRowIndex).Take(maximumRows).ToList();
        }
        public static int GetDataCount(int maximumRows, int startPageIndex, int startRowIndex)
        {
            return MyDbContext.db.Workshop_Term.Count();
        }
        public static List<Workshop_Term> GetAll()
        {
            return MyDbContext.db.Workshop_Term.ToList();
        }

        public int TermId { get; set; } // TermId (Primary key)
        public string Season { get; set; } // Season
        public int Year { get; set; } // Year
        public string Name { get; set; } // Name
        public bool IsCurrentTerm { get; set; } // IsCurrentTerm
        public int UserChangeId { get; set; } // UserChangeId

        // Reverse navigation
        public virtual ICollection<Workshop_Workshop> Workshop_Workshop { get; set; } // Workshop.FK_Workshop_Term;

        // For getting username of last person that chenge the recourd
        public string UsernameOfChange
        { get { return this.UserChangeId <= 0 ? "" : Security_User.GetById(this.UserChangeId).Username; } }

        // Foreign keys
        public virtual Security_User Security_User { get; set; } //  UserChangeId - FK_Term_User

        public Workshop_Term()
        {
            Workshop_Workshop = new List<Workshop_Workshop>();
        }
    }

    // Workshop
    public partial class Workshop_Workshop : IBasicClassForInheritance
    {
        public static int Count()
        {
            return MyDbContext.db.Workshop_Workshop.Count();
        }
        public static Workshop_Workshop GetById(int id)
        {
            return MyDbContext.db.Workshop_Workshop.FirstOrDefault(x => x.WorkshopId == id);
        }
        public static bool Remove(int id)
        {
            try
            {
                MyDbContext.db.Workshop_Workshop.Remove(GetById(id));

                MyDbContext.db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                MyUtility.ExceptionReport.log(ex);
                return false;
            }
        }
        public static Workshop_Workshop AddOrUpdate(Workshop_Workshop item)
        {
            var newItem = GetById(item.WorkshopId) ?? new Workshop_Workshop();
            newItem.WorkshopId = item.WorkshopId;
            newItem.StartDate = item.StartDate;
            newItem.EndDate = item.EndDate;
            newItem.TermId = item.TermId;
            newItem.Name = item.Name;
            newItem.TeacherId = item.TeacherId;
            newItem.Price = item.Price;
            newItem.UserChangeId = item.UserChangeId;
            newItem.NumberOfSession = item.NumberOfSession;
            newItem.TeacherPortion = item.TeacherPortion;
            newItem.UserChangeId = GlobalVariable.UserId;

            try
            {
                if (newItem.WorkshopId <= 0)
                {
                    newItem.WorkshopId = 0;
                    newItem = MyDbContext.db.Workshop_Workshop.Add(newItem);
                }

                MyDbContext.db.SaveChanges();

                return newItem;
            }
            catch (Exception ex)
            {
                MyUtility.ExceptionReport.log(ex);
                return null;
            }
        }
        public static IQueryable<Workshop_Workshop> Search(Workshop_Workshop search)
        {
            Workshop_Workshop defaultItem = new Workshop_Workshop();
            var result = from x in MyDbContext.db.Workshop_Workshop
                         select x;
            if (search.WorkshopId != defaultItem.WorkshopId)
                result = result.Where(x => x.WorkshopId == search.WorkshopId);
            if (search.StartDate != defaultItem.StartDate)
                result = result.Where(x => x.StartDate == search.StartDate);
            if (search.EndDate != defaultItem.EndDate)
                result = result.Where(x => x.EndDate == search.EndDate);
            if (search.TermId != defaultItem.TermId)
                result = result.Where(x => x.TermId == search.TermId);
            if (search.Name != defaultItem.Name)
                result = result.Where(x => x.Name.Contains(search.Name));
            if (search.TeacherId != defaultItem.TeacherId)
                result = result.Where(x => x.TeacherId == search.TeacherId);
            if (search.Price != defaultItem.Price)
                result = result.Where(x => x.Price == search.Price);
            if (search.UserChangeId != defaultItem.UserChangeId)
                result = result.Where(x => x.UserChangeId == search.UserChangeId);
            if (search.NumberOfSession != defaultItem.NumberOfSession)
                result = result.Where(x => x.NumberOfSession == search.NumberOfSession);
            if (search.TeacherPortion != defaultItem.TeacherPortion)
                result = result.Where(x => x.TeacherPortion == search.TeacherPortion);
            return result;
        }
        public static List<Workshop_Workshop> GetData(int maximumRows, int startPageIndex, int startRowIndex)
        {
            return GetData(maximumRows, startPageIndex, startRowIndex, "");
        }
        public static List<Workshop_Workshop> GetData(int maximumRows, int startPageIndex, int startRowIndex, string sortParameter)
        {
            if (string.IsNullOrEmpty(sortParameter))
                sortParameter = "WorkshopId";
            return MyDbContext.db.Workshop_Workshop.OrderBy(sortParameter).Skip(startRowIndex).Take(maximumRows).ToList();
        }
        public static int GetDataCount(int maximumRows, int startPageIndex, int startRowIndex)
        {
            return MyDbContext.db.Workshop_Workshop.Count();
        }
        public static List<Workshop_Workshop> GetAll()
        {
            return MyDbContext.db.Workshop_Workshop.ToList();
        }

        public int WorkshopId { get; set; } // WorkshopId (Primary key)
        public DateTime StartDate { get; set; } // StartDate
        public DateTime EndDate { get; set; } // EndDate
        public int TermId { get; set; } // TermId
        public string Name { get; set; } // Name
        public int? TeacherId { get; set; } // TeacherId
        public int Price { get; set; } // Price
        public int UserChangeId { get; set; } // UserChangeId
        public int NumberOfSession { get; set; } // NumberOfSession
        public int TeacherPortion { get; set; } // TeacherPortion

        // Reverse navigation
        public virtual ICollection<Registry_Registery> Registry_Registery { get; set; } // Registery.FK_Registery_Workshop;

        // For getting username of last person that chenge the recourd
        public string UsernameOfChange
        { get { return this.UserChangeId <= 0 ? "" : Security_User.GetById(this.UserChangeId).Username; } }

        // Foreign keys
        public virtual Workshop_Term Workshop_Term { get; set; } //  TermId - FK_Workshop_Term
        public virtual Workshop_Teacher Workshop_Teacher { get; set; } //  TeacherId - FK_Workshop_Teacher
        public virtual Security_User Security_User { get; set; } //  UserChangeId - FK_Workshop_User

        public Workshop_Workshop()
        {
            NumberOfSession = 0;
            TeacherPortion = 0;
            Registry_Registery = new List<Registry_Registery>();
        }
    }


    // ************************************************************************
    // POCO Configuration

    // Address
    public partial class PersonInfo_AddressConfiguration : EntityTypeConfiguration<PersonInfo_Address>
    {
        public PersonInfo_AddressConfiguration()
        {
            ToTable("PersonInfo.Address");
            HasKey(x => x.AddressId);

            Property(x => x.AddressId).HasColumnName("AddressId").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Address).HasColumnName("Address").IsRequired();
            Property(x => x.PersonId).HasColumnName("PersonId").IsRequired();
            Property(x => x.ZipCode).HasColumnName("ZipCode").IsOptional().HasMaxLength(50);
            Property(x => x.Description).HasColumnName("Description").IsOptional().HasMaxLength(50);
            Property(x => x.UserChangeId).HasColumnName("UserChangeId").IsRequired();

            // Foreign keys
            HasRequired(a => a.PersonInfo_Person).WithMany(b => b.PersonInfo_Address).HasForeignKey(c => c.PersonId); // FK_Address_Person
            HasRequired(a => a.Security_User).WithMany(b => b.PersonInfo_Address).HasForeignKey(c => c.UserChangeId); // FK_Address_User
        }
    }

    // Email
    public partial class PersonInfo_EmailConfiguration : EntityTypeConfiguration<PersonInfo_Email>
    {
        public PersonInfo_EmailConfiguration()
        {
            ToTable("PersonInfo.Email");
            HasKey(x => x.EmailId);

            Property(x => x.EmailId).HasColumnName("EmailId").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Email).HasColumnName("Email").IsRequired().HasMaxLength(50);
            Property(x => x.PersonId).HasColumnName("PersonId").IsRequired();
            Property(x => x.Description).HasColumnName("Description").IsOptional().HasMaxLength(50);
            Property(x => x.UserChangeId).HasColumnName("UserChangeId").IsRequired();

            // Foreign keys
            HasRequired(a => a.PersonInfo_Person).WithMany(b => b.PersonInfo_Email).HasForeignKey(c => c.PersonId); // FK_Email_Person
            HasRequired(a => a.Security_User).WithMany(b => b.PersonInfo_Email).HasForeignKey(c => c.UserChangeId); // FK_Email_User
        }
    }

    // Person
    public partial class PersonInfo_PersonConfiguration : EntityTypeConfiguration<PersonInfo_Person>
    {
        public PersonInfo_PersonConfiguration()
        {
            ToTable("PersonInfo.Person");
            HasKey(x => x.PersonId);

            Property(x => x.PersonId).HasColumnName("PersonId").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Name).HasColumnName("Name").IsRequired().HasMaxLength(50);
            Property(x => x.Family).HasColumnName("Family").IsRequired().HasMaxLength(50);
            Property(x => x.Father).HasColumnName("Father").IsRequired().HasMaxLength(50);
            Property(x => x.Mother).HasColumnName("Mother").IsRequired().HasMaxLength(50);
            Property(x => x.DateOfBirth).HasColumnName("DateOfBirth").IsRequired();
            Property(x => x.Gender).HasColumnName("Gender").IsRequired();
            Property(x => x.TypeOfRelation).HasColumnName("TypeOfRelation").IsOptional().HasMaxLength(50);
            Property(x => x.IsInViber).HasColumnName("IsInViber").IsOptional();
            Property(x => x.NationalNumber).HasColumnName("NationalNumber").IsRequired().HasMaxLength(10);
            Property(x => x.UserChangeId).HasColumnName("UserChangeId").IsRequired();
            Property(x => x.Picture).HasColumnName("Picture").IsOptional().HasMaxLength(2147483647);
            Property(x => x.LiveWith).HasColumnName("LiveWith").IsOptional().HasMaxLength(500);
            Property(x => x.TakeChild).HasColumnName("TakeChild").IsOptional().HasMaxLength(500);

            // Foreign keys
            HasRequired(a => a.Security_User).WithMany(b => b.PersonInfo_Person).HasForeignKey(c => c.UserChangeId); // FK_Person_User
        }
    }

    // Phone
    public partial class PersonInfo_PhoneConfiguration : EntityTypeConfiguration<PersonInfo_Phone>
    {
        public PersonInfo_PhoneConfiguration()
        {
            ToTable("PersonInfo.Phone");
            HasKey(x => x.PhoneId);

            Property(x => x.PhoneId).HasColumnName("PhoneId").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.PhoneNumber).HasColumnName("PhoneNumber").IsRequired().HasMaxLength(50);
            Property(x => x.Description).HasColumnName("Description").IsOptional().HasMaxLength(50);
            Property(x => x.PersonId).HasColumnName("PersonId").IsRequired();
            Property(x => x.UserChangeId).HasColumnName("UserChangeId").IsRequired();

            // Foreign keys
            HasRequired(a => a.PersonInfo_Person).WithMany(b => b.PersonInfo_Phone).HasForeignKey(c => c.PersonId); // FK_Phone_Person
            HasRequired(a => a.Security_User).WithMany(b => b.PersonInfo_Phone).HasForeignKey(c => c.UserChangeId); // FK_Phone_User
        }
    }

    // Pay
    public partial class Registry_PayConfiguration : EntityTypeConfiguration<Registry_Pay>
    {
        public Registry_PayConfiguration()
        {
            ToTable("Registry.Pay");
            HasKey(x => x.PayId);

            Property(x => x.PayId).HasColumnName("PayId").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.RegisterId).HasColumnName("RegisterId").IsRequired();
            Property(x => x.DateOfPay).HasColumnName("DateOfPay").IsRequired();
            Property(x => x.TypeOfPay).HasColumnName("TypeOfPay").IsOptional();
            Property(x => x.AccountNumber).HasColumnName("AccountNumber").IsOptional().HasMaxLength(50);
            Property(x => x.Price).HasColumnName("Price").IsRequired();
            Property(x => x.UserChangeId).HasColumnName("UserChangeId").IsRequired();

            // Foreign keys
            HasRequired(a => a.Registry_Registery).WithMany(b => b.Registry_Pay).HasForeignKey(c => c.RegisterId); // FK_Pay Fee_Registery
            HasRequired(a => a.Security_User).WithMany(b => b.Registry_Pay).HasForeignKey(c => c.UserChangeId); // FK_Pay_User
        }
    }

    // Registery
    public partial class Registry_RegisteryConfiguration : EntityTypeConfiguration<Registry_Registery>
    {
        public Registry_RegisteryConfiguration()
        {
            ToTable("Registry.Registery");
            HasKey(x => x.RegisteryId);

            Property(x => x.RegisteryId).HasColumnName("RegisteryId").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.PersonId).HasColumnName("PersonId").IsRequired();
            Property(x => x.DateOfRegister).HasColumnName("DateOfRegister").IsOptional();
            Property(x => x.WorkshopId).HasColumnName("WorkshopId").IsRequired();
            Property(x => x.UserChangeId).HasColumnName("UserChangeId").IsRequired();
            Property(x => x.NumberOfSessionRegister).HasColumnName("NumberOfSessionRegister").IsRequired();
            Property(x => x.DescriptionOfSessionRegister).HasColumnName("DescriptionOfSessionRegister").IsOptional().HasMaxLength(50);
            Property(x => x.Discount).HasColumnName("Discount").IsRequired();

            // Foreign keys
            HasRequired(a => a.PersonInfo_Person).WithMany(b => b.Registry_Registery).HasForeignKey(c => c.PersonId); // FK_Registery_Person
            HasRequired(a => a.Workshop_Workshop).WithMany(b => b.Registry_Registery).HasForeignKey(c => c.WorkshopId); // FK_Registery_Workshop
            HasRequired(a => a.Security_User).WithMany(b => b.Registry_Registery).HasForeignKey(c => c.UserChangeId); // FK_Registery_User
        }
    }

    // User
    public partial class Security_UserConfiguration : EntityTypeConfiguration<Security_User>
    {
        public Security_UserConfiguration()
        {
            ToTable("Security.User");
            HasKey(x => x.UserId);

            Property(x => x.UserId).HasColumnName("UserId").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Username).HasColumnName("Username").IsRequired().HasMaxLength(100);
            Property(x => x.Password).HasColumnName("Password").IsRequired().HasMaxLength(100);
            Property(x => x.IsActive).HasColumnName("IsActive").IsRequired();
            Property(x => x.Name).HasColumnName("Name").IsRequired().HasMaxLength(50);
            Property(x => x.UserChangeId).HasColumnName("UserChangeId").IsOptional();
            Property(x => x.IsSupperAdmin).HasColumnName("IsSupperAdmin").IsRequired();
        }
    }

    // Settings
    public partial class Setting_SettingsConfiguration : EntityTypeConfiguration<Setting_Settings>
    {
        public Setting_SettingsConfiguration()
        {
            ToTable("Setting.Settings");
            HasKey(x => x.SettingsId);

            Property(x => x.SettingsId).HasColumnName("SettingsId").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Name).HasColumnName("Name").IsRequired().HasMaxLength(100);
            Property(x => x.Value).HasColumnName("Value").IsRequired().HasMaxLength(100);
            Property(x => x.UserChangeId).HasColumnName("UserChangeId").IsRequired();

            // Foreign keys
            HasRequired(a => a.Security_User).WithMany(b => b.Setting_Settings).HasForeignKey(c => c.UserChangeId); // FK_Settings_User
        }
    }

    // Teacher
    public partial class Workshop_TeacherConfiguration : EntityTypeConfiguration<Workshop_Teacher>
    {
        public Workshop_TeacherConfiguration()
        {
            ToTable("Workshop.Teacher");
            HasKey(x => x.TeacherId);

            Property(x => x.TeacherId).HasColumnName("TeacherId").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Name).HasColumnName("Name").IsOptional().HasMaxLength(50);
            Property(x => x.Family).HasColumnName("Family").IsRequired().HasMaxLength(50);
            Property(x => x.Field).HasColumnName("Field").IsOptional().HasMaxLength(3050);
            Property(x => x.UserChangeId).HasColumnName("UserChangeId").IsRequired();

            // Foreign keys
            HasRequired(a => a.Security_User).WithMany(b => b.Workshop_Teacher).HasForeignKey(c => c.UserChangeId); // FK_Teacher_User
        }
    }

    // Term
    public partial class Workshop_TermConfiguration : EntityTypeConfiguration<Workshop_Term>
    {
        public Workshop_TermConfiguration()
        {
            ToTable("Workshop.Term");
            HasKey(x => x.TermId);

            Property(x => x.TermId).HasColumnName("TermId").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Season).HasColumnName("Season").IsOptional().HasMaxLength(50);
            Property(x => x.Year).HasColumnName("Year").IsRequired();
            Property(x => x.Name).HasColumnName("Name").IsRequired().HasMaxLength(50);
            Property(x => x.IsCurrentTerm).HasColumnName("IsCurrentTerm").IsRequired();
            Property(x => x.UserChangeId).HasColumnName("UserChangeId").IsRequired();

            // Foreign keys
            HasRequired(a => a.Security_User).WithMany(b => b.Workshop_Term).HasForeignKey(c => c.UserChangeId); // FK_Term_User
        }
    }

    // Workshop
    public partial class Workshop_WorkshopConfiguration : EntityTypeConfiguration<Workshop_Workshop>
    {
        public Workshop_WorkshopConfiguration()
        {
            ToTable("Workshop.Workshop");
            HasKey(x => x.WorkshopId);

            Property(x => x.WorkshopId).HasColumnName("WorkshopId").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.StartDate).HasColumnName("StartDate").IsRequired();
            Property(x => x.EndDate).HasColumnName("EndDate").IsRequired();
            Property(x => x.TermId).HasColumnName("TermId").IsRequired();
            Property(x => x.Name).HasColumnName("Name").IsRequired().HasMaxLength(50);
            Property(x => x.TeacherId).HasColumnName("TeacherId").IsOptional();
            Property(x => x.Price).HasColumnName("Price").IsRequired();
            Property(x => x.UserChangeId).HasColumnName("UserChangeId").IsRequired();
            Property(x => x.NumberOfSession).HasColumnName("NumberOfSession").IsRequired();
            Property(x => x.TeacherPortion).HasColumnName("TeacherPortion").IsRequired();

            // Foreign keys
            HasRequired(a => a.Workshop_Term).WithMany(b => b.Workshop_Workshop).HasForeignKey(c => c.TermId); // FK_Workshop_Term
            HasOptional(a => a.Workshop_Teacher).WithMany(b => b.Workshop_Workshop).HasForeignKey(c => c.TeacherId); // FK_Workshop_Teacher
            HasRequired(a => a.Security_User).WithMany(b => b.Workshop_Workshop).HasForeignKey(c => c.UserChangeId); // FK_Workshop_User
        }
    }

}

