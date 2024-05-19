using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pomodoro.Utils
{

    public enum PomodoroTypes{
        POMODORO = 0,
        SHORT = 1,
        LONG = 2 
    }
    public class TimerEventArgs: EventArgs
    {

        public PomodoroTypes NextTab { get; set; }
        public int NextTomato { get; set; }
        public TimerEventArgs(PomodoroTypes nextTab,int nextTomato)
        {
            NextTab = nextTab;
            NextTomato = nextTomato;
        }
    }
}
