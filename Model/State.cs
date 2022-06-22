using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EasySave.Model
{
    class State
    {
        public string Name  { get; set; }
        public string SourcePath { get; set; }
        public string DestinationPath { get; set; }
        public string BackUpState { get; set; }
        public int TotalFilesToCopy { get; set; }
        public long TotalFilesSize { get; set; }
        public int FilesLeftToDo { get; set; }
        public float Progression { get; set; }
        private int Index { get; set; }

        //--- Constructor ---//
        public State(BackUp LinkedBackUp, int BackUpIndex = 7 )
        {
            this.Name = LinkedBackUp.GetName();
            this.SourcePath = LinkedBackUp.GetFileSource();
            this.DestinationPath = LinkedBackUp.GetDestinationPath();
            this.TotalFilesToCopy = LinkedBackUp.GetFilesNumber();
            this.TotalFilesSize = LinkedBackUp.GetTotalFileSize();

            if (!File.Exists("../../../State.json"))
            {
                CreateStateFile();
            }

            if (BackUpIndex == -1)
            {
                FindIndex();
            }
            else if (BackUpIndex > 5)
            {
                this.Index = PromptLocation();
            }else
            {
                this.Index = BackUpIndex;
            }
        }



        //--- Method :  Create  State.json ---//
        private static void CreateStateFile()
        {
            string str = "{'BackUp': [{'Name': '','SourceFilePath': '','TargetFilePath': '','State': 'END','TotalFilesToCopy': 0,'TotalFilesSize': 0,'NbFilesLeftToDo': 0,'Progression': 0},{{'Name': '','SourceFilePath': '','TargetFilePath': '','State': 'END','TotalFilesToCopy': 0,'TotalFilesSize': 0,'NbFilesLeftToDo': 0,'Progression': 0}},{{'Name': '','SourceFilePath': '','TargetFilePath': '','State': 'END','TotalFilesToCopy': 0,'TotalFilesSize': 0,'NbFilesLeftToDo': 0,'Progression': 0}},{{'Name': '','SourceFilePath': '','TargetFilePath': '','State': 'END','TotalFilesToCopy': 0,'TotalFilesSize': 0,'NbFilesLeftToDo': 0,'Progression': 0}},{{'Name': '','SourceFilePath': '','TargetFilePath': '','State': 'END','TotalFilesToCopy': 0,'TotalFilesSize': 0,'NbFilesLeftToDo': 0,'Progression': 0}}]}"; ;
            JObject json = JObject.Parse(str);
            string jsonIndented = JsonConvert.SerializeObject(json, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(@"../../../State.json", jsonIndented);
        }

        //--- Method : Find and Set Index if the already exist ---//
        private int FindIndex()
        {
            var ReadedJsonArray = GetStateJson();

            for (int i = 0; i<ReadedJsonArray.Length; i++)
            {
                if (ReadedJsonArray[i]["Name"].ToString() == this.Name)
                {
                    this.Index = i;
                    break;
                }
            }

            return 1;
        }

        //--- Method :  returns an array with all the states of the backups  ---//
        private static Newtonsoft.Json.Linq.JObject[] GetStateJson()
        {
            StreamReader Reader = new StreamReader("../../../State.json");
            string ReadedJson = Reader.ReadToEnd();
            var ReadedJsonArray = AllChildren(JObject.Parse(ReadedJson))
                .First(c => c.Type == JTokenType.Array)
                .Children<JObject>().ToArray();
            return ReadedJsonArray;
        }

        //--- Method : Update <state during Backup ---//
        public void DoState(int FilesLeftTooDo, bool Active) 
        {
            this.FilesLeftToDo = FilesLeftTooDo;

            this.BackUpState = Active ? "ACTIVE" : "END";

            this.Progression = Convert.ToInt32(Math.Round((((float)this.TotalFilesToCopy - FilesLeftToDo) / this.TotalFilesToCopy) * 100, 0));

            SaveState();

        }

        //--- Method : Save Updated Backup ---//

        private void SaveState()
        {
            var ReadedJsonArray = GetStateJson();

            ReadedJsonArray[this.Index] = JObject.Parse(System.Text.Json.JsonSerializer.Serialize(this));

            string[] StringData = Array.ConvertAll<object, string>(ReadedJsonArray, o => o.ToString());

            var ConcatenedString = "{'BackUp':[" + string.Join(@",", StringData) + "]}";

            JObject ParsedJson = JObject.Parse(ConcatenedString);

            string JsonToSave = JsonConvert.SerializeObject(ParsedJson, Newtonsoft.Json.Formatting.Indented);

            File.WriteAllText(@"../../../final.json", JsonToSave);
        }

        //--- Method : Ask for the Index of the State ---//
        private int PromptLocation()
        {
            Console.WriteLine(GetStateJson());
            Console.WriteLine("Choose back-up Location 1,2,3,4 or 5");
            int LocationChoice = Convert.ToInt32(Console.ReadLine());
            if (LocationChoice >= 0 && LocationChoice < 6)
            {
                return LocationChoice;
            }
            else return PromptLocation();
            
        }

        //--- Method : Get all Json Children ---//
        private static IEnumerable<JToken> AllChildren(JToken json)
        {
            foreach (var c in json.Children())
            {
                yield return c;
                foreach (var cc in AllChildren(c))
                {
                    yield return cc;
                }
            }
        }
    }
}