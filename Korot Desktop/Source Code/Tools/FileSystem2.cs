//MIT License
//
//Copyright (c) 2020 Eren "Haltroy" Kanat
//
//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:
//
//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.
//
//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.
using System;
using System.IO;
using System.Text;

namespace Korot
{
    public class FileSystem2
    {
        public static string ImageToBase64(System.Drawing.Image image)
        {
                using (MemoryStream m = new MemoryStream())
                {
                    image.Save(m, image.RawFormat);
                    byte[] imageBytes = m.ToArray();
                    return Convert.ToBase64String(imageBytes);
                }
        }
        public static System.Drawing.Image Base64ToImage(string base64String)
        {
            byte[] imageBytes = Convert.FromBase64String(base64String);
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
            ms.Write(imageBytes, 0, imageBytes.Length);
            System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
            return image;
        }
        public static string ReadFile(string fileLocation, Encoding encode)
        {
            FileStream fs = new FileStream(fileLocation, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            StreamReader sr = new StreamReader(fs, encode);
            string result = sr.ReadToEnd();
            sr.Close();
            return result;

        }
        public static bool WriteFile(string fileLocation, string input, Encoding encode)
        {

            if (!File.Exists(fileLocation))
            {
                File.Create(fileLocation).Dispose();
            }
            var writer = new FileStream(fileLocation, FileMode.Open, FileAccess.Write, FileShare.ReadWrite);
            writer.Write(encode.GetBytes(input), 0, encode.GetBytes(input).Length);
            writer.Close();
            return true;
        }
        public static bool WriteFile(string fileLocation, byte[] input)
        {

            if (!File.Exists(fileLocation))
            {
                File.Create(fileLocation).Dispose();
            }
            var writer = new FileStream(fileLocation, FileMode.Open, FileAccess.Write, FileShare.ReadWrite);
            writer.Write(input, 0, input.Length);
            writer.Close();
            return true;
        }
    }
}