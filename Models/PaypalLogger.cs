using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace ECommerce.Models
{
    public class PaypalLogger
    {
        public static string LogDirectoryPath = Environment.CurrentDirectory;
        public static void Log(String messages)
        {
            try
            {
                StreamWriter streamWriter = new StreamWriter(LogDirectoryPath + "E:\\PaypalError.log",true);
                streamWriter.WriteLine("{0}----->{1}",DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), messages);
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}