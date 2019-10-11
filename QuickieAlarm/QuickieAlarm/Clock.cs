using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Windows.Forms;

namespace QuickieAlarm
{
    public partial class Clock : Form
    {

        public event EventHandler RequestShowSettings;

        public Clock()
        {
            InitializeComponent();
            this.SoundPlayer = new SoundPlayer();
        }

        public Clock(AlarmSettings settings) : this()
        {
            this.Settings = settings;
        }

        public AlarmSettings Settings { get; set; }

        private void TimeTimer_Tick(object sender, EventArgs e)
        {
            TimeLabel.Text = DateTime.Now.ToLongTimeString();


            ExitButton.Visible = 
                ((DateTime.Now - MouseMoveTime) < TimeSpan.FromSeconds(3));
            ShowSettingsButton.Visible = ExitButton.Visible;

            if (!AlarmIsRunning && AlarmIsStopped && this.Settings.AlarmTime > DateTime.Now)
            {
                AlarmIsStopped = false; // reset alarm if user changed to future time
            }

            if (this.Settings.AlarmTime < DateTime.Now && !AlarmIsRunning && !AlarmIsStopped)
            {
                StartAlarm();
            }
            if (AlarmIsRunning && (DateTime.Now - BeepTick) >= TimeSpan.FromSeconds(1))
            {
                BeepTick = DateTime.Now;
                if (Settings.AlarmType == AlarmSettings.AlarmTypeEnum.SoundFile &&
                    (string.IsNullOrEmpty(this.Settings.SoundFile) ||
                    !System.IO.File.Exists(Settings.SoundFile)))
                {
                    Settings.AlarmType = AlarmSettings.AlarmTypeEnum.Beep;
                }
                switch (Settings.AlarmType)
                {
                    case AlarmSettings.AlarmTypeEnum.Beep:
                        // beep if not still beeping
                        Microsoft.VisualBasic.Interaction.Beep();
                        break;
                    case AlarmSettings.AlarmTypeEnum.SoundFile:
                        // playsound if not still playing
                        if (Settings.SoundFile.ToLower().EndsWith(".wav"))
                        {
                            if (string.IsNullOrEmpty(this.SoundPlayer.SoundLocation))
                            {
                                SoundPlayer.SoundLocation = Settings.SoundFile;
                                SoundPlayer.PlayLooping();
                            }
                        } 
                        else if (Settings.SoundFile.ToLower().EndsWith(".mp3"))
                        {
                            // not supported yet (todo)
                            Settings.AlarmType = AlarmSettings.AlarmTypeEnum.Beep;
                        }
                        break;
                    case AlarmSettings.AlarmTypeEnum.SilentVisual:
                        if (this.BackColor == Color.Black)
                            this.BackColor = Color.Wheat;
                        else
                        {
                            this.BackColor = Color.Black;
                        }
                        break;
                }
            }
        }

        private System.Media.SoundPlayer SoundPlayer { get; set; }

        private DateTime? BeepTick { get; set; }

        public bool AlarmIsRunning { get; set; }

        public bool AlarmIsStopped { get; set; }

        private void StartAlarm()
        {
            if (!AlarmIsRunning && !AlarmIsStopped)
            {
                AlarmIsRunning = true;
                AlarmOffButton.Visible = true;
                AlarmOffButton.Focus();
                BeepTick = DateTime.Now.AddSeconds(-1);
            }
        }

        private void AlarmOffButton_Click(object sender, EventArgs e)
        {
            AlarmIsStopped = true;
            AlarmIsRunning = false;
            try {
                SoundPlayer.Stop();
            } catch {}
            SoundPlayer = new SoundPlayer();
            AlarmOffButton.Hide();
        }

        private void Clock_Load(object sender, EventArgs e)
        {
            TimeTimer_Tick(null, null);
            TimeTimer.Enabled = true;
        }

        private void Clock_Shown(object sender, EventArgs e)
        {
            TimeLabel.Left = 0;
            TimeLabel.Width = this.Width;
        }

        public DateTime MouseMoveTime { get; set; }

        private System.Drawing.Point? _mousepos;
        private void ClockLabel_MouseMove(object sender, MouseEventArgs e)
        {
            if (_mousepos == null) _mousepos = e.Location;
            if (e.X > _mousepos.Value.X+5 || e.X < _mousepos.Value.X-5 ||
                e.Y > _mousepos.Value.Y+5 || e.Y < _mousepos.Value.Y-5)
            {
                _mousepos = e.Location;
                MouseMoveTime = DateTime.Now;
            }
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ShowSettingsButton_Click(object sender, EventArgs e)
        {
            RequestShowSettings(this, new EventArgs());
        }
    }
}
