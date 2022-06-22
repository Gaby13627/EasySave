using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySave.Model
{
    public abstract class BackUp
    {
        protected string _Name;
        protected string _FileSource;
        protected string _DestinationPath;
        protected long _TotalFileSize;
        protected int _FilesNumber;

        public BackUp(string Name, string FileSource, string DestinationPath)
        {
            this._Name = Name;
            this._FileSource = FileSource;
            this._DestinationPath = DestinationPath;
        }

        public string GetName()
        {
            return this._Name;
        }
        public string GetFileSource()
        {
            return this._FileSource;
        }
        public string GetDestinationPath()
        {
            return this._DestinationPath;
        }

        public long GetTotalFileSize()
        {
            return this._TotalFileSize;
        }

        public void SetTotalFileSize(long TotalFileSize)
        {
            this._TotalFileSize = TotalFileSize;
        }

        public int GetFilesNumber()
        {
            return this._FilesNumber;
        }

        public void SetFilesNumber(int FilesNumber)
        {
            this._FilesNumber = FilesNumber;
        }

        public abstract void MakeBackup();
    }
}
