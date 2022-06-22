using System;
using System.Collections.Generic;
using System.IO;
using ServiceConfirm;
using EasySave.Model;
namespace EasySave.View
{
    class DisplayMenu
    {
            //Display ASCII ART title 
            public void EasySave()
            {
                Console.Title = "EasySave program";
                string Title = @" 
 ______                      _____
|  ____|                    / ____|
| |__     __ _  ___  _   _ | (___    __ _ __   __  ___
|  __|   / _` |/ __|| | | | \___ \  / _` |\ \ / / / _ \
| |____ | (_| |\__ \| |_| | ____) || (_| | \ V / |  __/
|______| \__,_||___/ \__, ||_____/  \__,_|  \_/   \___|
                      __/ |
                     |___/

                                                                 ";

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(Title);
            }

        //Clear screen when user press a key
        public void ClearScreen(Language LData)
        {
            Console.WriteLine(LData.Continue);
            Console.ReadKey();
            Console.Clear();
        }

        public string SelectLanguage()
        {
            string FileName;
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Select language : ");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("1 - English ");
            Console.WriteLine("2 - Francais");
            string input = Console.ReadLine();
            if (input == "1")
            {


                Console.Clear();
                FileName = @"..\..\..\Translate\English.json";
            }else if (input == "2")
            {
                Console.Clear();
                FileName = @"..\..\..\Translate\French.json";
            }else
            {
                FileName = "";
                Console.Clear();
                Console.WriteLine("Incorrect input, press enter");
                Console.ReadKey();
                Console.Clear();
                SelectLanguage();
            }
            string jsonString = File.ReadAllText(FileName);
            return jsonString;
        }


        //Display main menu
        public void Menu()
            {
                // Options
                EasySave();
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(
                @"=================== Home Page ===================

    Choose an option from the following list:
    1 - Create back-up
    2 - Display back-up list
    3 - Exit

====================================================
Enter your option : ");

            switch (Console.ReadLine())
                    {
                        case "1":
                            Console.Clear();
                            Console.Clear();
                            Modify modify = new Modify();
                            modify.ModifyBackUp();
                        break;
                        case "2":
                            Console.Clear();
                            DisplayBackUpList displayBackupList = new DisplayBackUpList();
                            displayBackupList.BackUpList();
                            break;
                        case "3":
                            Console.Clear();
                            Console.WriteLine("Do you want to stop the program ?");
                            Console.WriteLine("yes | no");
                            Confirm serviceConfirm = new Confirm();
                            serviceConfirm.ConfirmChoice();
                            
                            Console.Clear();
                            break;
                            default:
                            Console.Clear();
                            Console.WriteLine("Incorrect input, press enter");
                            Console.ReadKey();
                            Console.Clear();
                            Menu();
                            break;
                    }
            }

            
    }
}