using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySave.Model
{
    class FileCompare : System.Collections.Generic.IEqualityComparer<FileInfo>
    {
        public FileCompare() { }

        public bool Equals(FileInfo File1, FileInfo File2)
        {
            return (File1.Name == File2.Name &&
                    File1.Length == File2.Length &&
                    File1.LastWriteTime == File2.LastWriteTime);
        }
        public int GetHashCode(FileInfo fi)
        {
            string s = $"{fi.Name}{fi.Length}";
            return s.GetHashCode();
        }
    }
}
