using System;
using System.IO;
using System.Text;
using System.Text.Json;


namespace EasySave.Model
{
    class Log
    {
        //private string Time;
        //private string BackUpName;
        private string filesource;
        private string filetarget;
        private string size;
        private string filetransfer;
        //private BackUp
        public string Time { get; set; }
        public string BackUpName { get; set; }
        public string FileSource { get; set; }
        public string FileTarget { get; set; }
        public long Size { get; set; }
        public int FileTransfer { get; set; }

        private static string Json { get; set; }

        /* public Log(string TimeStamp, string BackUpName, string FileSource, string FileTarget, string Size)
         {
             this.TimeStamp = TimeStamp;
             this.BackUpName = BackUpName;
             this.FileSource = FileSource;
             this.FileTarget,
         }*/
        //public void CreateLog()
        //{

        //    var options = new JsonSerializerOptions { WriteIndented = true };
        //    string JSONresult = JsonConvert.SerializeObject(this, System.Xml.Formatting.Indented);
        //    string path = "C:\\Users\\dsace\\source\\repos\\Logfile\\Logfile\\Fichier log\\log.json";

        //    bool result = File.Exists(path);
        //    if (result == true) // add data if log created before
        //    {
        //        Console.WriteLine("File Found");
        //        using (var tw = new StreamWriter(path, true))
        //        {
        //            tw.WriteLine(JSONresult.ToString());
        //            tw.Close();
        //        }
        //    }
        //    else  // create the log file if it's not create
        //    {
        //        Console.WriteLine("File Not Found");
        //        //string json = JsonSerializer.Serialize(_data);
        //        File.WriteAllText(@"C:\\Users\\dsace\\source\\repos\\Logfile\\Logfile\\Fichier log\\log.json", Json);

        //        using (var tw = new StreamWriter(path, true))
        //        {
        //            tw.WriteLine(JSONresult.ToString());
        //            tw.Close();
        //        }
        //    }
        //}
    }
}
