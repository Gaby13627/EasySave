using System;
using EasySave.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySave.View
{
    class Modify
    {
        public void ModifyBackUp()
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Create a back-up");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\nEnter name : ");
            string Name = Console.ReadLine();
            Console.WriteLine("Enter source : ");
            string Source = Console.ReadLine();
            Console.WriteLine("Enter target : ");
            string Target = Console.ReadLine();
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Choose type");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n(1) Differential execution | (2) Complete execution");
            string TypeBackUp = Console.ReadLine(); 
            if (TypeBackUp == "1")
            {
                DifferentialBackUp NewBackUp = new(Name, Source, Target);
                NewBackUp.MakeBackup();
            }
            else
            {
                CompleteBackUp NewBackUp = new(Name, Source, Target);
                NewBackUp.MakeBackup();
            }
        }

    }
}