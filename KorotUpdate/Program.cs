using System;
using System.IO.Compression;
using System.IO;
using System.Diagnostics;
using System.Reflection;

namespace KorotUpdate
{
    class Program
    {
        static void Main(string[] args)
        {
            string title = Console.Title;
            Console.Title = "Korot Updater";
            string backupFile = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Haltroy\\Korot\\UpdateBackup.hup";
            string newVer = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Haltroy\\Korot\\UpdateTemp\\";
            Console.WriteLine("Korot Update Utility");
            Console.WriteLine("--------------------");
            string appPath = Directory.GetCurrentDirectory();
            Console.Title = "Korot Updater - 0%";

            Console.WriteLine("Creating backup from: " + appPath);
            try
            {
                if (File.Exists(backupFile))
                {
                    try
                    {
                        File.Delete(backupFile);

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error while deleting: " + ex.ToString());
                        Console.WriteLine("Press S to skip job. Press Enter to close.");
                        ConsoleKeyInfo keyInfo = Console.ReadKey();
                        if (keyInfo.Key == ConsoleKey.S) { Console.WriteLine("Skipped job."); }else if (keyInfo.Key == ConsoleKey.Enter) { Console.Title = title; return; }
                    }
                    Console.Title = "Korot Updater - 25%";

                    Console.WriteLine("File already exists. Deleted.");
                }
                ZipFile.CreateFromDirectory(appPath, backupFile);
                Console.WriteLine("Successfully created backup in " + backupFile + " .");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while creating backup: " + ex.ToString());
                Console.WriteLine("Press S to skip job. Press Enter to close.");
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                if (keyInfo.Key == ConsoleKey.S) { Console.WriteLine("Skipped job."); } else if (keyInfo.Key == ConsoleKey.Enter) { Console.Title = title; return; }
            }
            if (args.Length > 0)
            {
                string upgradeFile = args[0];
                Console.Title = "Korot Updater - 50%";
                Console.WriteLine("Upgrading from " + upgradeFile + " ...");
                Console.WriteLine("Unzipping to " + newVer + " ...");
                try
                {
                    if (Directory.Exists(newVer))
                    {
                        try
                        {
                            Directory.Delete(newVer, true);
                        }catch (Exception ex)
                        {
                            Console.WriteLine("Error while deleting: " + ex.ToString());
                            Console.WriteLine("Press S to skip job. Press Enter to close.");
                            ConsoleKeyInfo keyInfo = Console.ReadKey();
                            if (keyInfo.Key == ConsoleKey.S) { Console.WriteLine("Skipped job."); } else if (keyInfo.Key == ConsoleKey.Enter) { Console.Title = title; return; }
                        }
                        Console.WriteLine("Directory already exists. Deleted.");
                    }
                    try
                    {
                        ZipFile.ExtractToDirectory(upgradeFile, newVer);
                    }catch (Exception ex)
                    {
                        Console.WriteLine("Error while unzipping: " + ex.ToString());
                        Console.WriteLine("Press S to skip job. Press Enter to close.");
                        ConsoleKeyInfo keyInfo = Console.ReadKey();
                        if (keyInfo.Key == ConsoleKey.S) { Console.WriteLine("Skipped job."); } else if (keyInfo.Key == ConsoleKey.Enter) { Console.Title = title; return; }
                    }
                    Console.Title = "Korot Updater - 75%";
                    Console.WriteLine("Successfully unzipped. Copying files...");
                    try
                    {
                        Copy(newVer, appPath);
                    }catch (Exception ex)
                    {
                        Console.WriteLine("Error while copying: " + ex.ToString());
                        Console.WriteLine("Press S to skip job. Press Enter to close.");
                        ConsoleKeyInfo keyInfo = Console.ReadKey();
                        if (keyInfo.Key == ConsoleKey.S) { Console.WriteLine("Skipped job."); } else if (keyInfo.Key == ConsoleKey.Enter) { Console.Title = title; return; }
                    }
                    Console.Title = "Korot Updater - 100%";
                    Console.WriteLine("Successfully copied. Closing...");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error while unzipping: " + ex.ToString());
                    Console.WriteLine("Press S to skip job. Press Enter to close.");
                    ConsoleKeyInfo keyInfo = Console.ReadKey();
                    if (keyInfo.Key == ConsoleKey.S) { Console.WriteLine("Skipped job."); } else if (keyInfo.Key == ConsoleKey.Enter) { Console.Title = title; return; }
                }
            }else
            {
                Console.Title = "Korot Updater - 100%";
                Console.WriteLine("No extra arguments found. Closing...");
            }
            Console.Title = title;
            return;
        }
        internal static void Copy(string sourceDirectory, string targetDirectory)
        {
            DirectoryInfo diSource = new DirectoryInfo(sourceDirectory);
            DirectoryInfo diTarget = new DirectoryInfo(targetDirectory);

            CopyAll(diSource, diTarget);
        }

        internal static void CopyAll(DirectoryInfo source, DirectoryInfo target)
        {
            Directory.CreateDirectory(target.FullName);

            // Copy each file into the new directory.
            foreach (FileInfo fi in source.GetFiles())
            {
                string fName = Path.Combine(target.FullName, fi.Name);
                if (File.Exists(fName)) { File.Delete(fName); Console.WriteLine("File exists. Deleted: "+ fName); }
                fi.CopyTo(fName, true);
                Console.WriteLine("File copied: " + fi.Name);
            }

            // Copy each subdirectory using recursion.
            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                DirectoryInfo nextTargetSubDir =
                    target.CreateSubdirectory(diSourceSubDir.Name);
                CopyAll(diSourceSubDir, nextTargetSubDir);
            }
        }
    }
}
