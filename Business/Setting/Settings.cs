using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business;
using System.IO;

namespace Business.Setting
{
    public static class Settings
    {
        public static string ConnectionStringFilePath { get { return MyUtility.Basic.AssemblyDirectory + "\\Config.dat"; } }

        public static bool SaveConnectionString(string connectionString, string filePath = null)
        {
            try
            {
                if (string.IsNullOrEmpty(filePath))
                    filePath = ConnectionStringFilePath;
                File.WriteAllText(filePath, MyUtility.String.Encript(connectionString));

                return true;
            }
            catch (Exception ex)
            {
                BaseClass.log(ex);

                return false;
            }
        }
    }
}
