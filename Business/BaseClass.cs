using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using Business;
using System.IO;
using Business.MyException;

namespace Business
{
     
    public static class BaseClass
    {
        public static void log(Exception ex)
        {
            var file = MyUtility.Basic.AssemblyDirectory + @"\Error.txt";

            var message = string.Format
                (
                    "Error: {0} {1} {0} {2} {0} {3}_________________\r\n",
                    "--------------\r\n",
                    ex.Message,
                    ex.StackTrace,
                    ex.InnerException
                );

            File.AppendAllText(file, message);
        }
    }

    //public class test : GenericDAO<Shop_Goods>, IGenericDAO<Shop_Goods>

}
