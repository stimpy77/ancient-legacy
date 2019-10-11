namespace QuickieAlarm
{
    partial class Clock
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.TimeTimer = new System.Windows.Forms.Timer(this.components);
            this.TimeLabel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.AlarmOffButton = new DevExpress.XtraEditors.SimpleButton();
            this.ExitButton = new DevExpress.XtraEditors.SimpleButton();
            this.ShowSettingsButton = new DevExpress.XtraEditors.SimpleButton();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TimeTimer
            // 
            this.TimeTimer.Interval = 50;
            this.TimeTimer.Tick += new System.EventHandler(this.TimeTimer_Tick);
            // 
            // TimeLabel
            // 
            this.TimeLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TimeLabel.Font = new System.Drawing.Font("Tahoma", 120F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TimeLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.TimeLabel.Location = new System.Drawing.Point(0, 0);
            this.TimeLabel.Name = "TimeLabel";
            this.TimeLabel.Size = new System.Drawing.Size(925, 664);
            this.TimeLabel.TabIndex = 2;
            this.TimeLabel.Text = "12:00 AM";
            this.TimeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.TimeLabel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ClockLabel_MouseMove);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.AlarmOffButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 602);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(350, 0, 350, 15);
            this.panel1.Size = new System.Drawing.Size(925, 62);
            this.panel1.TabIndex = 3;
            // 
            // AlarmOffButton
            // 
            this.AlarmOffButton.Appearance.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AlarmOffButton.Appearance.Options.UseFont = true;
            this.AlarmOffButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.AlarmOffButton.Location = new System.Drawing.Point(350, 15);
            this.AlarmOffButton.Name = "AlarmOffButton";
            this.AlarmOffButton.Size = new System.Drawing.Size(225, 32);
            this.AlarmOffButton.TabIndex = 2;
            this.AlarmOffButton.Text = "Alarm Off";
            this.AlarmOffButton.Visible = false;
            this.AlarmOffButton.Click += new System.EventHandler(this.AlarmOffButton_Click);
            // 
            // ExitButton
            // 
            this.ExitButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ExitButton.Location = new System.Drawing.Point(899, 0);
            this.ExitButton.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Style3D;
            this.ExitButton.LookAndFeel.UseDefaultLookAndFeel = false;
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(26, 26);
            this.ExitButton.TabIndex = 4;
            this.ExitButton.Text = "X";
            this.ExitButton.Visible = false;
            this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // ShowSettingsButton
            // 
            this.ShowSettingsButton.Location = new System.Drawing.Point(0, 0);
            this.ShowSettingsButton.LookAndFeel.UseDefaultLookAndFeel = false;
            this.ShowSettingsButton.Name = "ShowSettingsButton";
            this.ShowSettingsButton.Size = new System.Drawing.Size(75, 23);
            this.ShowSettingsButton.TabIndex = 5;
            this.ShowSettingsButton.Text = "Settings ...";
            this.ShowSettingsButton.Visible = false;
            this.ShowSettingsButton.Click += new System.EventHandler(this.ShowSettingsButton_Click);
            // 
            // Clock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(925, 664);
            this.ControlBox = false;
            this.Controls.Add(this.ShowSettingsButton);
            this.Controls.Add(this.ExitButton);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.TimeLabel);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.Color.Red;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Clock";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Clock";
            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Clock_Load);
            this.Shown += new System.EventHandler(this.Clock_Shown);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer TimeTimer;
        private System.Windows.Forms.Label TimeLabel;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.SimpleButton AlarmOffButton;
        private DevExpress.XtraEditors.SimpleButton ExitButton;
        private DevExpress.XtraEditors.SimpleButton ShowSettingsButton;
    }
}