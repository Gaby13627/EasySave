using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;


namespace EasySave.Model
{
    class DifferentialBackUp : BackUp
    {
        public DifferentialBackUp(string Name, string FileSource, string DestinationPath) : base(Name, FileSource, DestinationPath)
        {
            this._TotalFileSize = 0;
        }

        public override void MakeBackup()
        {
            var SourcePath = this.GetFileSource();
            var TargetPath = this.GetDestinationPath();

            DirectoryInfo SourceDirectory = new DirectoryInfo(SourcePath);
            DirectoryInfo TargetDirectory = new DirectoryInfo(TargetPath);

            // Take a snapshot of the file system.  
            IEnumerable<FileInfo> SourceFileList = SourceDirectory.GetFiles("*.*", SearchOption.AllDirectories);
            IEnumerable<FileInfo> TargetFileList = TargetDirectory.GetFiles("*.*", SearchOption.AllDirectories);

            FileCompare NewFileCompare = new FileCompare();

            bool areIdentical = SourceFileList.SequenceEqual(TargetFileList, NewFileCompare);

            if (areIdentical == true)
            {
                Console.WriteLine("the two folders are the same");
                return;
            }
            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            State NewSate = new State(this);

            var DeletedFilesList = (from file in TargetFileList select file).Except(SourceFileList, NewFileCompare);

            int NumberFiles = 0;

            foreach (var DeletedFile in DeletedFilesList)
            {
                Console.WriteLine(DeletedFile.FullName);
                DeletedFile.Delete();
            }

            var UpdatedFilesList = (from file in SourceFileList select file).Except(TargetFileList, NewFileCompare);
            long FilesSize = 0;
            foreach (var UpdatedFIle in UpdatedFilesList)
            {
                FilesSize += UpdatedFIle.Length;
                NumberFiles++;
            }

            this.SetFilesNumber(NumberFiles);
            this.SetTotalFileSize(FilesSize);

            foreach (string dirPath in Directory.GetDirectories(SourcePath, "*", SearchOption.AllDirectories))
            {
                Directory.CreateDirectory(dirPath.Replace(SourcePath, TargetPath));
            }

            int FileCompleted = 0;
            NewSate.DoState(this.GetFilesNumber(), true);
            foreach (var UpdatedFIle in UpdatedFilesList)
            {
                File.Copy(UpdatedFIle.FullName,UpdatedFIle.FullName.Replace(SourcePath, TargetPath), true);
                FileCompleted++;
                NewSate.DoState(this.GetFilesNumber() - FileCompleted, true);
            }

            LogCreate NewLog = new(this, watch.ElapsedMilliseconds.ToString());
            _ = NewLog.GenLog();

            NewSate.DoState(0, false);
        }
    }
}
