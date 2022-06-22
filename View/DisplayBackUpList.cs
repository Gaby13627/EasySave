using System;
using System.Collections.Generic;
using System.IO;
using ServiceConfirm;
using EasySave.View;
namespace EasySave.View
{
    public class DisplayBackUpList
    {
        public void BackUpList()
        {

            DisplayMenu displayMenu = new DisplayMenu();
            displayMenu.EasySave();
            Console.ForegroundColor = ConsoleColor.White;
            List<string> BackupList = new List<string>();
            Console.WriteLine("Enter path : ");
            string Path = Console.ReadLine();
            DirectoryInfo DI = new DirectoryInfo(Path);
            FileInfo[] Files = DI.GetFiles("*.txt");
            Console.WriteLine("\t=================== Back-up List ===================");
            Console.WriteLine("\t");
            int Index = 0;
            foreach (FileInfo File in Files)
            {
                Console.Write(Index);
                Console.Write("  ");
                Console.WriteLine(File.Name);
                Console.WriteLine("----------------");
                Index++;

            }

            Console.WriteLine("\t________________________");
            Console.WriteLine("Options (1) Delete | (2) Modify | (3) Exit");
            switch (Console.ReadLine())
            {
                case "1":
                    Console.WriteLine("Enter back-up name, split with <,>");
                    string Select = Console.ReadLine();
                    string[] SelectedBackup = Select.Split(",");
                    List<string> DeleteBackupList = new List<string>();
                    foreach (string S in SelectedBackup)
                    {
                        File.Delete(Path + S + ".txt");
                    }

                    Console.WriteLine("End, press a key");
                    Console.ReadKey();
                    Console.Clear();
                    displayMenu.Menu();
                    break;
                case "2":
                    Console.Clear();
                    Modify modify = new Modify();
                    modify.ModifyBackUp();
                    break;
                case "3":
                    Confirm serviceConfirm = new Confirm();
                    serviceConfirm.ConfirmChoice();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Incorrect input, press enter");
                    Console.ReadKey();
                    Console.Clear();
                    displayMenu.Menu();
                    break;

            }

        }
    }
}
