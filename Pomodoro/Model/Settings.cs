
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Pomodoro.ViewModels;

namespace Pomodoro.Model
{
    [Serializable]
    public class Settings : ViewModel
    {
        #region privates
        private int pomodoroTime;
        private int shortBreakTime;
        private int longBreakTime;
        private bool autoStart;
        #endregion

        #region properties
        [XmlElement("PomodoroTime")]
        public int PomodoroTime
        {
            get { return pomodoroTime; }
            set
           {
                if (pomodoroTime != value)
                {
                    pomodoroTime = value;
                    OnPropertyChanged(nameof(PomodoroTime));
                }
            }
        }

        [XmlElement("ShortBreakTime")]
        public int ShortBreakTime
        {
            get { return shortBreakTime; }
            set
            {
                if (shortBreakTime != value)
                {
                    shortBreakTime = value;
                    OnPropertyChanged(nameof(PomodoroTime));
                }
            }
        }

        [XmlElement("LongBreakTime")]
        public int LongBreakTime
        {
            get { return longBreakTime; }
            set
            {
                if (longBreakTime != value)
                {
                    longBreakTime = value;
                    OnPropertyChanged(nameof(LongBreakTime));
                }
            }
        }


        [XmlElement("AutoStart")]
        public bool AutoStart
        {
            get { return autoStart; }
            set
            {
                if (autoStart != value)
                {
                    autoStart = value;
                    OnPropertyChanged(nameof(AutoStart));
                }
            }
        }
        #endregion
    }
}
