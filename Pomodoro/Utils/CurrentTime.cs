using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pomodoro.Utils
{
    public class CurrentTime
    {
        private readonly int MaxMinutes;
        private int minute;
        private int seconds;

        public CurrentTime(int minutes)
        {
            MaxMinutes = minutes;
            minute = minutes;
            seconds = 0;
        }
        public bool IsOver()
        {
            return minute == 0 && seconds == 0;
        }

        public void DecreaseTime()
        {
            if(minute != 0)
            {
                if(seconds == 0)
                {
                    minute--;
                    seconds = 59;
                }
                else
                {
                    seconds--;
                }
            }
            else
            {
                if(seconds > 0)
                {
                    seconds--;
                }
            }
        }


        public override string ToString()
        {
            string min = minute < 10 ? string.Format("0{0}", minute) : minute.ToString();
            string sec = seconds < 10 ? string.Format("0{0}", seconds) : seconds.ToString();

            return string.Format("{0}:{1}", min,sec);
        }

        public void RefreshTime()
        {
            minute = MaxMinutes;
            seconds = 0;
        }
    }
}
