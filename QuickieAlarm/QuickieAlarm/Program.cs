using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace QuickieAlarm
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            inputWindow = new InputWindow();
            inputWindow.GoClick += InputWindow_GoClick;
            inputWindow.GoClick += SaveSettingsToRegistry;

            var settings = inputWindow.GetSettings();
            var key = @"HKEY_CURRENT_USER\Software\QuickieAlarm";
            settings.AlarmTime = DateTime.Parse((Microsoft.Win32.Registry
                .GetValue(key, "AlarmTime", settings.AlarmTime) ?? settings.AlarmTime).ToString());
            settings.AlarmType = (AlarmSettings.AlarmTypeEnum)Enum.Parse(typeof(AlarmSettings.AlarmTypeEnum),
                (Microsoft.Win32.Registry.GetValue(key, "AlarmType", settings.AlarmType) ?? settings.AlarmType).ToString());
            settings.SoundFile = (string) Microsoft.Win32.Registry
                .GetValue(key, "SoundFile", settings.SoundFile) ?? settings.SoundFile;

            inputWindow.Load += delegate {inputWindow.SetSettings(settings);};

            Application.Run(inputWindow);
        }

        static void SaveSettingsToRegistry(object sender, EventArgs e)
        {
            var window = (InputWindow) sender;
            var settings = window.GetSettings();
            var key = @"HKEY_CURRENT_USER\Software\QuickieAlarm";
            Microsoft.Win32.Registry.SetValue(key, "AlarmTime", settings.AlarmTime);
            Microsoft.Win32.Registry.SetValue(key, "AlarmType", settings.AlarmType);
            Microsoft.Win32.Registry.SetValue(key, "SoundFile", settings.SoundFile);
        }

        private static InputWindow inputWindow;
        private static Clock clock;

        static void InputWindow_GoClick(object sender, EventArgs e)
        {
            ((InputWindow)sender).Hide();
            clock = new Clock(((InputWindow)sender).GetSettings());
            clock.RequestShowSettings += clock_RequestShowSettings;
            clock.Closed += Clock_Closed;
            clock.Show();
        }

        static void clock_RequestShowSettings(object sender, EventArgs e)
        {
            //((Clock)sender).Hide();
            inputWindow.GoClick -= InputWindow_GoClick;
            inputWindow.GoClick += InputWindow_GoClick2;
            inputWindow.TopMost = true;
            inputWindow.ControlBox = false;
            inputWindow.Show(clock);
        }

        static void InputWindow_GoClick2(object sender, EventArgs e)
        {
            inputWindow.Hide();
            clock.Settings = inputWindow.GetSettings();
        }

        static void Clock_Closed(object sender, EventArgs e)
        {
            inputWindow.Close();
        }
    }
}
