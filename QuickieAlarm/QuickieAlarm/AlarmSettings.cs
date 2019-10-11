using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuickieAlarm
{
    public class AlarmSettings
    {
        public AlarmTypeEnum AlarmType { get; set; }

        public string SoundFile { get; set; }

        public DateTime AlarmTime { get; set; }

        public enum AlarmTypeEnum
        {
            Beep,
            SoundFile,
            SilentVisual
        }
    }
}
