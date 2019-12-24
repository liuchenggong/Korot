using System;
using System.IO;
using System.Text;

namespace Korot
{
    public class FileSystem2
    {
        public static string ReadFile(string fileLocation, Encoding encode)
        {
            try
            {
                FileStream fs = new FileStream(fileLocation, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                StreamReader sr = new StreamReader(fs, encode);
                string result = sr.ReadToEnd();
                sr.Close();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static bool WriteFile(string fileLocation, string input, Encoding encode)
        {
            try
            {
                var writer = new FileStream(fileLocation, FileMode.Open, FileAccess.Write, FileShare.ReadWrite);
                writer.Write(encode.GetBytes(input), 0, encode.GetBytes(input).Length);
                writer.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}