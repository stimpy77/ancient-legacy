using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;

namespace QuickieAlarm
{
    public partial class InputWindow : Form
    {
        public event EventHandler<EventArgs> GoClick;
        public InputWindow()
        {
            InitializeComponent();
        }

        private void InputWindow_Load(object sender, EventArgs e)
        {
            
        }

        private void AlarmType_SelectedIndexChanged(object sender, EventArgs e)
        {
            SoundFilePicker.Enabled = AlarmType.SelectedItem.ToString() == "Sound File";
            if (SoundFilePicker.Enabled &&
                string.IsNullOrEmpty(SoundFilePicker.Text))
            {
                SoundFilePicker_ButtonPressed(null, null);
            }
        }

        private bool init_sfdiag;
        private void SoundFilePicker_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            if (!init_sfdiag)
            {
                SoundFileDialog.InitialDirectory = Environment.GetEnvironmentVariable("windir") + "\\Media";
                init_sfdiag = true;
            }
            var result = SoundFileDialog.ShowDialog(this);
            if (result != DialogResult.Cancel)
            {
                SoundFilePicker.Text = SoundFileDialog.FileName;
            }
        }

        private void GoButton_Click(object sender, EventArgs e)
        {
            GoClick(this, new EventArgs());
        }

        internal AlarmSettings GetSettings()
        {
            var ret = new AlarmSettings();
            ret.AlarmTime = DateTime.Parse(DateTime.Now.ToShortDateString() + " " + AlarmTime.Text);
            if (ret.AlarmTime < DateTime.Now) ret.AlarmTime = ret.AlarmTime.AddDays(1);
            ret.AlarmType = (AlarmSettings.AlarmTypeEnum)Enum.Parse(typeof(AlarmSettings.AlarmTypeEnum), AlarmType.SelectedItem.ToString().Replace(" ", ""));
            ret.SoundFile = SoundFilePicker.Text;
            return ret;
        }

        private void InputWindow_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r' || e.KeyChar == '\n')
            {
                GoButton_Click(null, null);
            }
        }

        private void AlarmTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r' || e.KeyChar == '\n')
            {
                GoButton.Focus();
                GoButton_Click(null, null);
            }
        }

        private void AlarmType_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r' || e.KeyChar == '\n')
            {
                GoButton.Focus();
                GoButton_Click(null, null);
            }
        }

        private void SoundFilePicker_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r' || e.KeyChar == '\n')
            {
                GoButton.Focus();
                GoButton_Click(null, null);
            }
        }

        internal void SetSettings(AlarmSettings settings)
        {
            this.AlarmTime.Time = settings.AlarmTime;
            this.SoundFilePicker.Text = settings.SoundFile;
            this.AlarmType.SelectedItem = settings.AlarmType.ToString()
                .Replace("SoundFile", "Sound File")
                .Replace("SilentVisual", "Silent Visual");
        }
    }
}
