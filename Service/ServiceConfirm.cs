using System;
using EasySave.View;

namespace ServiceConfirm
{
    public class Confirm
    {
        public void ConfirmChoice()
        {
            //Exit or return to the main menue
            
                switch (Console.ReadLine())
                {
                    case "yes":
                        Environment.Exit(0);
                        break;
                    case "no":
                        Console.Clear();
                        DisplayMenu displayMenu = new DisplayMenu();
                        displayMenu.Menu();
                        break;
                }
            }
        }
    }

