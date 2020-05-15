using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.MyException
{
    public class ConfigFileNotFoundException : Exception
    {
        public string ErrorMessage { get; private set; }

        public ConfigFileNotFoundException(string errorMessage)
        {
            ErrorMessage = errorMessage; 
        }
        public override string ToString()
        {
            return ErrorMessage;
        }
    }
}
