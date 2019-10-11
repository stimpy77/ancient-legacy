using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleSearch
{
    public delegate void FileEventHandler(object sender, FileEventArgs e);

    public class FileEventArgs : EventArgs
    {
        public FileEventArgs(System.IO.FileInfo fileInfo)
        {
            this.FileInfo = fileInfo;
        }

        public System.IO.FileInfo FileInfo;
    }
}
