using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask
{
    public class ErrorLogger
    {
        private static readonly string logFilePath = "logs.log";//Путь к файлу логов.
        private static readonly object locker = new object();
        public static void LogError(Exception exep, string additionalInfo = "")
        {
            try
            {
                lock (locker)//Блокируем доступ к файлу, при одновременной записи из разных потоков.
                {
                    using (StreamWriter writer = File.AppendText(logFilePath))
                    {
                        writer.WriteLine($"[{DateTime.Now}] ERROR: {exep.Message}");
                        writer.WriteLine($"StackTrace: {exep.StackTrace}");
                        if (!string.IsNullOrEmpty(additionalInfo))
                        {
                            writer.WriteLine($"Additional Info: {additionalInfo}");
                        }
                        writer.WriteLine(new string('-', 50));//Разделяем записи.
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
