using EasySave.Model;
using EasySave.View;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace EasySave
{
    class Program
    {
        static void Main()
        {
            DisplayMenu NewMenu = new();

            NewMenu.EasySave();
            //var Lang = NewMenu.SelectLanguage();
            //Language LanguageVariables = JsonSerializer.Deserialize<Language>(Lang);
            //NewMenu.ClearScreen(LanguageVariables);
            NewMenu.Menu();

        }
    }
}