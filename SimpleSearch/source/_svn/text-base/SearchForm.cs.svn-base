using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections.Specialized;
using System.IO;

namespace SimpleSearch
{
    public partial class SearchForm : Form
    {
        public SearchForm()
        {
            InitializeComponent();
            this.SearchInput.StartSearch += new EventHandler(SearchInput_StartSearch);
            this.SearchInput.StopSearch += new EventHandler(SearchInput_StopSearch);
            this.DoubleBuffered = true;
        }

        public FileSearch Search;

        void SearchInput_StopSearch(object sender, EventArgs e)
        {
            Search.Stop();
        }

        void SearchInput_StartSearch(object sender, EventArgs e)
        {

            Search = new FileSearch( SearchInput.Params );
            Search.Starting += new EventHandler(Search_Starting);
            Search.Stopped += new EventHandler(Search_Stopped);
            Search.SearchingFolder += new FolderEventDelegate(Search_SearchingFolder);
            Search.FoundFolder += new FolderEventDelegate(Search_FoundFolder);
            Search.FoundFile += new FileEventHandler(Search_FoundFile);
            Search.Start();
        }

        void Search_FoundFile(object sender, FileEventArgs e)
        {
            int img = 2;
            if (e.FileInfo.Name.ToLower().EndsWith(".exe")) img = 1;
            string dir = e.FileInfo.Directory.FullName;
            ListViewItem item = new ListViewItem(
                new string[] { e.FileInfo.Name, dir }, img);
            item.Tag = e.FileInfo.FullName;
            ResultsView.Items.Add(item);
            if (ResultsView.SelectedIndices.Count == 0) item.EnsureVisible();
        }

        void Search_FoundFolder(object sender, FolderEventArgs e)
        {
            int img = 0;
            string dir = e.DirectoryInfo.Parent.FullName;
            ListViewItem item = new ListViewItem(
                new string[] { e.DirectoryInfo.Name, dir }, img);
            item.Tag = e.DirectoryInfo;
            ResultsView.Items.Add(item);
        }

        void Search_SearchingFolder(object sender, FolderEventArgs e)
        {
            SearchInput.StatusList.Visible = true;
            SearchInput.StatusList.Items.Add(e.DirectoryInfo.FullName);
            SearchInput.StatusList.SelectedIndex = SearchInput.StatusList.Items.Count - 1;
            if (SearchInput.StatusList.Items.Count > 50)
            {
                SearchInput.StatusList.Items.RemoveAt(0);
            }
        }

        void Search_Stopped(object sender, EventArgs e)
        {
            //throw new Exception("The method or operation is not implemented.");
            SearchInput.Searching = false;
            StatusLabel.Text = "Stopped.";
            SearchInput.StatusList.Visible = false;
        }

        void Search_Starting(object sender, EventArgs e)
        {
            SearchInput.Searching = true;
            SearchInput.StatusList.Items.Clear();
            ResultsView.Items.Clear();
            StatusLabel.Text = "Searching ...";
        }

        private void OpenItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem lvi in ResultsView.SelectedItems)
            {
                string filepath = lvi.Tag.ToString();
                //bool isDir = false;
                //if (Directory.Exists(filepath)) isDir = true;
                System.Diagnostics.ProcessStartInfo psi 
                    = new System.Diagnostics.ProcessStartInfo(
                    System.Environment.GetEnvironmentVariable("windir") 
                    + "\\explorer.exe", filepath);
                System.Diagnostics.Process.Start(psi);
            }
        }

        private void OpenParent_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem lvi in ResultsView.SelectedItems)
            {
                FileInfo fi = new FileInfo(lvi.Tag.ToString());
                string parentDir = fi.Directory.FullName;
                System.Diagnostics.ProcessStartInfo psi
                    = new System.Diagnostics.ProcessStartInfo(
                    System.Environment.GetEnvironmentVariable("windir")
                    + "\\explorer.exe", parentDir);
                System.Diagnostics.Process.Start(psi);
            }
        }

        private void ResultsView_DoubleClick(object sender, EventArgs e)
        {
            OpenItem_Click(sender, new EventArgs());
        }

        private void SearchInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

    }
}