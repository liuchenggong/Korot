/*

Copyright © 2020 Eren "Haltroy" Kanat

Use of this source code is governed by an MIT License that can be found in github.com/Haltroy/Korot/blob/master/LICENSE

*/

using System;
using System.IO;

namespace Korot
{
    public class Output
    {
        private readonly string LogDirPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + (string.IsNullOrWhiteSpace(SafeFileSettingOrganizedClass.LastUser) ? "OOBE" : SafeFileSettingOrganizedClass.LastUser) + "\\Logs\\";

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
                    AutoFlush = true,
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