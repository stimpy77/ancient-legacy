using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace SimpleSearch
{
    public delegate void FolderEventDelegate(
        object sender, FolderEventArgs e);

    public class FolderEventArgs : EventArgs
    {
        public FolderEventArgs(System.IO.DirectoryInfo directoryInfo)
        {
            this.DirectoryInfo = directoryInfo;
        }

        public DirectoryInfo DirectoryInfo;
    }
}
