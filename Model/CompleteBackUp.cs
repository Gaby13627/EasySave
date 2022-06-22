using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySave.Model
{
    public class CompleteBackUp : BackUp
    {
        public CompleteBackUp(string Name, string FileSource, string DestinationPath) : base( Name, FileSource, DestinationPath) 
        {
            this._TotalFileSize = 0;
        }

        public override void MakeBackup()
        {
            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            var SourcePath = this.GetFileSource();
            var TargetPath = this.GetDestinationPath();
            State NewSate = new State(this);
            ///We delete all the files and folders that could be present due to an old backup
            DirectoryInfo DirectoryToClean = new DirectoryInfo(TargetPath);

            foreach (FileInfo file in DirectoryToClean.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in DirectoryToClean.GetDirectories())
            {
                dir.Delete(true);
            }

            //Now we recreate the folder structure of the source in the destination
            foreach (string dirPath in Directory.GetDirectories(SourcePath, "*", SearchOption.AllDirectories))
            {
                Directory.CreateDirectory(dirPath.Replace(SourcePath, TargetPath));
            }

            this.SetFilesNumber(Directory.GetFiles(SourcePath, "*.*", SearchOption.AllDirectories).Length);

            int FileCompleted = 0;
            NewSate.DoState(this.GetFilesNumber(), true);
            //Finally we copy all the files and rename them with the same names as in the source folder
            foreach (string newPath in Directory.GetFiles(SourcePath, "*.*", SearchOption.AllDirectories))
            {

                File.Copy(newPath, newPath.Replace(SourcePath, TargetPath), true);
                FileCompleted++;

                NewSate.DoState(this.GetFilesNumber() - FileCompleted, true);

                
            }
            watch.Stop();
            LogCreate NewLog = new(this, watch.ElapsedMilliseconds.ToString());
            _ = NewLog.GenLog();


            NewSate.DoState(0, false);
        }
    }
}
