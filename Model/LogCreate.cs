using System;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;


namespace EasySave.Model
{
    class LogCreate
    {
        private BackUp Data;
        private string Time;
        public LogCreate(BackUp Data, string Time)
        {
            this.Data = Data;
            this.Time = Time;
        }
        public async Task GenLog()
        {
            Log DataLog = new Log
            {
                Time = this.Time,
                BackUpName = this.Data.GetName(),
                FileSource = this.Data.GetFileSource(),
                FileTarget = this.Data.GetDestinationPath(),
                Size = this.Data.GetTotalFileSize(),
                FileTransfer = this.Data.GetFilesNumber(),
            };

            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = System.Text.Json.JsonSerializer.Serialize(DataLog, options);
            string fileName = @"..\..\..\log.json";
            using FileStream createStream = File.Create(fileName);
            await System.Text.Json.JsonSerializer.SerializeAsync(createStream, DataLog);
            await createStream.DisposeAsync();
            Console.WriteLine(File.ReadAllText(fileName));
        }
    }
}
