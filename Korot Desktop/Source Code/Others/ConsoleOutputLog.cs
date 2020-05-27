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

namespace Korot
{
    public class Output
    {
        private readonly string LogDirPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Logs\\";

        private static Output _outputSingleton;
        private static Output OutputSingleton
        {
            get
            {
                if (_outputSingleton == null)
                {
                    _outputSingleton = new Output();
                }
                return _outputSingleton;
            }
        }

        public StreamWriter SW { get; set; }

        public Output()
        {
            EnsureLogDirectoryExists();
            InstantiateStreamWriter();
        }

        ~Output()
        {
            if (SW != null)
            {
                try
                {
                    SW.Dispose();
                }
                catch (ObjectDisposedException) { } // object already disposed - ignore exception
            }
        }

        public static void WriteLine(string str)
        {
            Console.WriteLine(str);
            OutputSingleton.SW.WriteLine(str);
        }

        public static void Write(string str)
        {
            Console.Write(str);
            OutputSingleton.SW.Write(str);
        }

        private void InstantiateStreamWriter()
        {
            string filePath = Path.Combine(LogDirPath, DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")) + ".txt";
            try
            {
                SW = new StreamWriter(filePath)
                {
                    AutoFlush = true
                };
            }
            catch (UnauthorizedAccessException ex)
            {
                throw new ApplicationException(string.Format("Access denied. Could not instantiate StreamWriter using path: {0}.", filePath), ex);
            }
        }

        private void EnsureLogDirectoryExists()
        {
            if (!Directory.Exists(LogDirPath))
            {
                try
                {
                    Directory.CreateDirectory(LogDirPath);
                }
                catch (UnauthorizedAccessException ex)
                {
                    throw new ApplicationException(string.Format("Access denied. Could not create log directory using path: {0}.", LogDirPath), ex);
                }
            }
        }
    }
}
