using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Collections.Specialized;
using System.Windows.Forms;
using System.IO;

namespace SimpleSearch
{
    public partial class SearchInputControl : UserControl
    {
        public event EventHandler StartSearch;
        public event EventHandler StopSearch;

        public SearchInputControl()
        {
            InitializeComponent();
            string[] drives = Directory.GetLogicalDrives();
            string dirs = "";
            foreach (string dir in drives)
            {
                dirs += dir + "; ";
            }
            if (dirs.EndsWith("; ")) dirs = dirs.Substring(0, dirs.Length - 2);
            FoldersText.Text = dirs;
        }

        private void FoldersText_TextChanged(object sender, EventArgs e)
        {
            RefreshFolderList(); // FoldersCollection = FoldersCollection; // reset
        }

        public SearchParams Params
        {
            get
            {
                SearchParams p = new SearchParams();
                p.FileNamePattern = this.FileName.Text;
                p.FileContains = this.FileContains.Text;
                p.SearchFolders = this.SearchFolders;
                p.MatchFolders = FindFolders.Checked;
                p.MatchFiles = true;
                return p;
            }
        }

        private string[] SearchFolders
        {
            get 
            {
                List<string> sl = new List<string>(FoldersText.Text.Split(';'));
                for (int i = 0; i < sl.Count; i++)
                {
                    if (i >= 0)
                    {
                        string s = sl[i];
                        if (s.Trim() == "" || !Directory.Exists(s))
                        {
                            sl.RemoveAt(i);
                            i--;
                        }
                        else
                        {
                            // remove duplicates
                            for (int fi = 0; fi < sl.Count - 1 && fi < i; fi++)
                            {
                                string ff = sl[fi];
                                if (s.EndsWith("\\")) s = s.Substring(0, s.Length - 1);
                                if (ff.EndsWith("\\")) ff = ff.Substring(0, ff.Length - 1);
                                if (ff.ToLower().Trim() == s.ToLower().Trim())
                                {
                                    sl.RemoveAt(fi);
                                    fi--;
                                    i--;
                                }
                            }
                        }
                    }
                }
                return sl.ToArray();
            }
            set
            {
                string s = "";
                foreach (string v in value)
                {
                    s += v;
                }
                if (s.EndsWith(";")) s = s.Substring(0, s.Length - 1);
                FoldersText.Text = s;
                RefreshFolderList();
            }
        }

        private void RefreshFolderList()
        {
            FoldersList.Items.Clear();
            FoldersList.Items.Add("Search Folders:");
            string[] folders = SearchFolders;
            foreach (string f in folders)
            {
                FoldersList.Items.Add(f.Trim());
            }
            if (folders.Length > 0 && folders[0].Trim() != "")
            {
                FoldersList.Visible = true;
            }
            else
            {
                FoldersList.Visible = false;
            }
        }

        private void BrowseFolderButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.SelectedPath = FoldersText.Text;
            if (fbd.ShowDialog(this) != DialogResult.Cancel)
            {
                if (FoldersText.Text != "" && !FoldersText.Text.Trim().EndsWith(";"))
                {
                    FoldersText.Text += ";";
                }
                FoldersText.Text += fbd.SelectedPath;
            }
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            SearchButton.Focus();
            if (this.Searching)
            {
                if (this.StopSearch != null)
                    this.StopSearch(this, new EventArgs());
            }
            else
            {
                if (this.StartSearch != null)
                    this.StartSearch(this, new EventArgs());
            }
        }

        public bool Searching
        {
            get
            {
                if (this.SearchButton.Text == "Search")
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            set
            {
                if (value)
                {
                    this.SearchButton.Text = "Stop";
                }
                else
                {
                    this.SearchButton.Text = "Search";
                }
            }
        }

        private void FileName_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case ':':
                case '\\':
                case '/':
                case '\"':
                case '<':
                case '>':
                case '|':
                    e.Handled = true;
                    break;
            }
        }

        private void Textbox_Entered(object sender, EventArgs e)
        {
            ((TextBox)sender).SelectionStart = 0;
            ((TextBox)sender).SelectionLength = ((TextBox)sender).Text.Length;
        }

        private void SearchInputControl_KeyPress(object sender, KeyPressEventArgs e)
        {
            SearchButton_Click(SearchButton, new EventArgs());
        }

        private void FileName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                SearchButton_Click(SearchButton, new EventArgs());
            }
        }

        private void FoldersText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                SearchButton_Click(SearchButton, new EventArgs());
            }
        }

        private void FileContains_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                SearchButton_Click(SearchButton, new EventArgs());
            }
        }
    }
}
