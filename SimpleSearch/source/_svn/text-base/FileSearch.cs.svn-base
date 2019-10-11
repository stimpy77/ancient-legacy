using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace SimpleSearch
{
    public class FileSearch
    {
        public event EventHandler Starting;
        public event EventHandler Stopped;
        public event FolderEventDelegate SearchingFolder;
        public event FolderEventDelegate FoundFolder;
        public event FileEventHandler FoundFile;

        public FileSearch(SearchParams search_parameters)
        {
            this._SearchParams = search_parameters;
        }

        private bool _Stopped = true;
        private SearchParams _SearchParams;

        public void Start()
        {
            this._Stopped = false;
            if (this.Starting != null)
                this.Starting(this, new EventArgs());
            string[] folders = this._SearchParams.SearchFolders;
            for (int i = 0; i < folders.Length; i++)
            {
                string folder = folders[i].Trim();
                try
                {
                    SearchFolder(folder, this._SearchParams, true);
                }
                catch (System.IO.DirectoryNotFoundException)
                {
                    // ignore
                }
            }
            Stop();
        }

        public void Stop()
        {
            this._Stopped = true;
            if (this.Stopped != null)
                this.Stopped(this, new EventArgs());
        }

        public void SearchFolder(string folder, SearchParams searchParams, bool searchSubfolders)
        {
            string filePattern = searchParams.FileNamePattern;
            if (filePattern == "") filePattern = "*";
            if (!filePattern.Contains("*") ) //&& !filePattern.Contains("."))
                filePattern = "*" + filePattern + "*";
            if (!Directory.Exists(folder)) return;
            DirectoryInfo dirInfo = new DirectoryInfo(folder);

            if (this.SearchingFolder != null)
                this.SearchingFolder(this, new FolderEventArgs(
                    new DirectoryInfo(folder)));
            if (searchParams.MatchFolders && 
                ((dirInfo.Name.Contains(searchParams.FileNamePattern) && searchParams.MatchNameCase) ||
                 (dirInfo.Name.ToLower().Contains(searchParams.FileNamePattern.ToLower()) && !searchParams.MatchNameCase)) //&&
                //this._SearchParams.FileContains.Trim() == ""
                )
            {
                if (this.FoundFolder != null)
                    this.FoundFolder(this, new FolderEventArgs(dirInfo));
            }
            if (searchSubfolders)
            {
                string[] subdirs = Directory.GetDirectories(folder);
                foreach (string subdir in subdirs)
                {
                    System.Windows.Forms.Application.DoEvents();
                    if (_Stopped) return;
                    try
                    {
                        SearchFolder(subdir, searchParams, searchSubfolders);
                    }
                    catch (System.IO.PathTooLongException) { }
                    catch (System.UnauthorizedAccessException) { }
                }
            }

            string[] files = Directory.GetFiles(folder, filePattern, SearchOption.TopDirectoryOnly);
            foreach (string filepath in files)
            {
                if (_Stopped) return;
                bool bMatch = true;
                FileInfo file = new FileInfo(filepath);
                if (this._SearchParams.MatchNameCase)
                {
                    bMatch = file.Name.Contains(filePattern);
                }
                if (bMatch && this._SearchParams.FileContains.Trim() != "")
                {
                    // NOT IMPLEMENTED
                }
                if (bMatch)
                {
                    if (this.FoundFile != null)
                        this.FoundFile(this, new FileEventArgs(file));
                }
                System.Windows.Forms.Application.DoEvents();
            }
        }
    }
}
