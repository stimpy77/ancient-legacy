namespace SimpleSearch
{
    partial class SearchInputControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.FileNameLabel = new System.Windows.Forms.Label();
            this.FileName = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.FileContainsLabel = new System.Windows.Forms.Label();
            this.FileContains = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.SearchFoldersLabel = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.SearchButton = new System.Windows.Forms.Button();
            this.FolderPanel = new System.Windows.Forms.Panel();
            this.FoldersText = new System.Windows.Forms.TextBox();
            this.BrowseFolderButton = new System.Windows.Forms.Button();
            this.FoldersList = new System.Windows.Forms.ListBox();
            this.StatusList = new System.Windows.Forms.ListBox();
            this.FindFolders = new System.Windows.Forms.CheckBox();
            this.infoPanel1 = new SimpleSearch.InfoPanel();
            this.panel5.SuspendLayout();
            this.FolderPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // FileNameLabel
            // 
            this.FileNameLabel.AutoSize = true;
            this.FileNameLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.FileNameLabel.Location = new System.Drawing.Point(4, 48);
            this.FileNameLabel.Name = "FileNameLabel";
            this.FileNameLabel.Size = new System.Drawing.Size(57, 13);
            this.FileNameLabel.TabIndex = 1;
            this.FileNameLabel.Text = "File Name:";
            // 
            // FileName
            // 
            this.FileName.Dock = System.Windows.Forms.DockStyle.Top;
            this.FileName.Location = new System.Drawing.Point(4, 61);
            this.FileName.Name = "FileName";
            this.FileName.Size = new System.Drawing.Size(236, 20);
            this.FileName.TabIndex = 0;
            this.FileName.Enter += new System.EventHandler(this.Textbox_Entered);
            this.FileName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FileName_KeyPress);
            this.FileName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FileName_KeyDown);
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(4, 44);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(236, 4);
            this.panel1.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(4, 98);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(236, 4);
            this.panel2.TabIndex = 4;
            // 
            // FileContainsLabel
            // 
            this.FileContainsLabel.AutoSize = true;
            this.FileContainsLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.FileContainsLabel.Location = new System.Drawing.Point(4, 102);
            this.FileContainsLabel.Name = "FileContainsLabel";
            this.FileContainsLabel.Size = new System.Drawing.Size(51, 13);
            this.FileContainsLabel.TabIndex = 5;
            this.FileContainsLabel.Text = "Contains:";
            // 
            // FileContains
            // 
            this.FileContains.Dock = System.Windows.Forms.DockStyle.Top;
            this.FileContains.Enabled = false;
            this.FileContains.Location = new System.Drawing.Point(4, 115);
            this.FileContains.Name = "FileContains";
            this.FileContains.Size = new System.Drawing.Size(236, 20);
            this.FileContains.TabIndex = 1;
            this.FileContains.Text = "[Not implemented.]";
            this.FileContains.Enter += new System.EventHandler(this.Textbox_Entered);
            this.FileContains.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FileContains_KeyDown);
            // 
            // panel3
            // 
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(4, 135);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(236, 4);
            this.panel3.TabIndex = 7;
            // 
            // SearchFoldersLabel
            // 
            this.SearchFoldersLabel.AutoSize = true;
            this.SearchFoldersLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.SearchFoldersLabel.Location = new System.Drawing.Point(4, 139);
            this.SearchFoldersLabel.Name = "SearchFoldersLabel";
            this.SearchFoldersLabel.Size = new System.Drawing.Size(200, 13);
            this.SearchFoldersLabel.TabIndex = 8;
            this.SearchFoldersLabel.Text = "Search Folders (seperate by semicolons):";
            // 
            // panel4
            // 
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(4, 152);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(236, 4);
            this.panel4.TabIndex = 10;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.SearchButton);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(4, 179);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(236, 37);
            this.panel5.TabIndex = 3;
            // 
            // SearchButton
            // 
            this.SearchButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.SearchButton.Location = new System.Drawing.Point(35, 6);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(165, 23);
            this.SearchButton.TabIndex = 4;
            this.SearchButton.Text = "Search";
            this.SearchButton.UseVisualStyleBackColor = true;
            this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // FolderPanel
            // 
            this.FolderPanel.Controls.Add(this.FoldersText);
            this.FolderPanel.Controls.Add(this.BrowseFolderButton);
            this.FolderPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.FolderPanel.Location = new System.Drawing.Point(4, 156);
            this.FolderPanel.Margin = new System.Windows.Forms.Padding(0);
            this.FolderPanel.Name = "FolderPanel";
            this.FolderPanel.Size = new System.Drawing.Size(236, 23);
            this.FolderPanel.TabIndex = 2;
            // 
            // FoldersText
            // 
            this.FoldersText.Dock = System.Windows.Forms.DockStyle.Top;
            this.FoldersText.Location = new System.Drawing.Point(0, 0);
            this.FoldersText.Name = "FoldersText";
            this.FoldersText.Size = new System.Drawing.Size(211, 20);
            this.FoldersText.TabIndex = 0;
            this.FoldersText.Enter += new System.EventHandler(this.Textbox_Entered);
            this.FoldersText.TextChanged += new System.EventHandler(this.FoldersText_TextChanged);
            this.FoldersText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FoldersText_KeyDown);
            // 
            // BrowseFolderButton
            // 
            this.BrowseFolderButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.BrowseFolderButton.Location = new System.Drawing.Point(211, 0);
            this.BrowseFolderButton.Name = "BrowseFolderButton";
            this.BrowseFolderButton.Size = new System.Drawing.Size(25, 23);
            this.BrowseFolderButton.TabIndex = 1;
            this.BrowseFolderButton.Text = "...";
            this.BrowseFolderButton.UseVisualStyleBackColor = true;
            this.BrowseFolderButton.Click += new System.EventHandler(this.BrowseFolderButton_Click);
            // 
            // FoldersList
            // 
            this.FoldersList.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.FoldersList.Dock = System.Windows.Forms.DockStyle.Top;
            this.FoldersList.FormattingEnabled = true;
            this.FoldersList.Location = new System.Drawing.Point(4, 216);
            this.FoldersList.Name = "FoldersList";
            this.FoldersList.Size = new System.Drawing.Size(236, 95);
            this.FoldersList.TabIndex = 13;
            this.FoldersList.Visible = false;
            // 
            // StatusList
            // 
            this.StatusList.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.StatusList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StatusList.FormattingEnabled = true;
            this.StatusList.HorizontalScrollbar = true;
            this.StatusList.Location = new System.Drawing.Point(4, 311);
            this.StatusList.Name = "StatusList";
            this.StatusList.Size = new System.Drawing.Size(236, 108);
            this.StatusList.TabIndex = 14;
            this.StatusList.Visible = false;
            // 
            // FindFolders
            // 
            this.FindFolders.AutoSize = true;
            this.FindFolders.Checked = true;
            this.FindFolders.CheckState = System.Windows.Forms.CheckState.Checked;
            this.FindFolders.Dock = System.Windows.Forms.DockStyle.Top;
            this.FindFolders.Location = new System.Drawing.Point(4, 81);
            this.FindFolders.Name = "FindFolders";
            this.FindFolders.Size = new System.Drawing.Size(236, 17);
            this.FindFolders.TabIndex = 15;
            this.FindFolders.Text = "Find folders (in addition to files)";
            this.FindFolders.UseVisualStyleBackColor = true;
            // 
            // infoPanel1
            // 
            this.infoPanel1.BackColor = System.Drawing.SystemColors.Info;
            this.infoPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.infoPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.infoPanel1.Location = new System.Drawing.Point(4, 4);
            this.infoPanel1.Name = "infoPanel1";
            this.infoPanel1.Padding = new System.Windows.Forms.Padding(4);
            this.infoPanel1.Size = new System.Drawing.Size(236, 40);
            this.infoPanel1.TabIndex = 0;
            this.infoPanel1.TabStop = false;
            // 
            // SearchInputControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.StatusList);
            this.Controls.Add(this.FoldersList);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.FolderPanel);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.SearchFoldersLabel);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.FileContains);
            this.Controls.Add(this.FileContainsLabel);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.FindFolders);
            this.Controls.Add(this.FileName);
            this.Controls.Add(this.FileNameLabel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.infoPanel1);
            this.Name = "SearchInputControl";
            this.Padding = new System.Windows.Forms.Padding(4);
            this.Size = new System.Drawing.Size(244, 433);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SearchInputControl_KeyPress);
            this.panel5.ResumeLayout(false);
            this.FolderPanel.ResumeLayout(false);
            this.FolderPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private InfoPanel infoPanel1;
        private System.Windows.Forms.Label FileNameLabel;
        private System.Windows.Forms.TextBox FileName;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label FileContainsLabel;
        private System.Windows.Forms.TextBox FileContains;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label SearchFoldersLabel;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button SearchButton;
        private System.Windows.Forms.Panel FolderPanel;
        private System.Windows.Forms.Button BrowseFolderButton;
        private System.Windows.Forms.TextBox FoldersText;
        private System.Windows.Forms.ListBox FoldersList;
        public System.Windows.Forms.ListBox StatusList;
        private System.Windows.Forms.CheckBox FindFolders;

    }
}
