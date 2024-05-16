﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Pomodoro.Utils;

namespace Pomodoro.ViewModels
{
    public class TabViewModel : ViewModel
    {
        #region Privates
        private string name;
        private string visibleTime;
        private CurrentTime currentTime;
        private DispatcherTimer timer;
        private Visibility startVisibilty;
        private Visibility stopVisibilty;
        private string message;
        private int currentTomato;
        #endregion


        #region Properties


        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        public string Message
        {
            get { return message; }
            set
            {
                if (message != value)
                {
                    message = value;
                    OnPropertyChanged(nameof(Message));
                }
            }
        }


        public int CurrentTomato
        {
            get { return currentTomato; }
            set
            {
                if (currentTomato != value)
                {
                    currentTomato = value;
                    OnPropertyChanged(nameof(CurrentTomato));
                }
            }
        }




        public Visibility StartVisibility
        {
            get { return startVisibilty; }
            set
            {
                if (startVisibilty != value)
                {
                    startVisibilty = value;
                    OnPropertyChanged(nameof(StartVisibility));
                }
            }
        }

        public Visibility StopVisibility
        {
            get { return stopVisibilty; }
            set
            {
                if (stopVisibilty != value)
                {
                    stopVisibilty = value;
                    OnPropertyChanged(nameof(StopVisibility));
                }
            }
        }


        public string VisibleTime
        {
            get { return visibleTime; }
            set
            {
                if (visibleTime != value)
                {
                    visibleTime = value;
                    OnPropertyChanged(nameof(VisibleTime));
                }
            }
        }

        public DispatcherTimer Timer
        {
            get { return timer; }
            set
            {
                if (timer != value)
                {
                    timer = value;
                    OnPropertyChanged(nameof(Timer));
                }
            }
        }


        public CurrentTime CurrentTime
        {
            get { return currentTime; }
            set
            {
                if (currentTime != value)
                {
                    currentTime = value;
                    OnPropertyChanged(nameof(Timer));
                }
            }
        }
        #endregion


        #region ctor
        public TabViewModel(string name ,int numOfMinutes,string message)
        {
            CurrentTime = new CurrentTime(numOfMinutes);
            VisibleTime = CurrentTime.ToString();
            Message = message;
            Name = name;
            Timer = new DispatcherTimer(DispatcherPriority.DataBind)
            {
                Interval = TimeSpan.FromSeconds(1)
        
            };
            Timer.Tick += DispatherTimer_Tick;
            StartVisibility = Visibility.Visible;
            StopVisibility = Visibility.Collapsed;
        }
        #endregion


        #region events
        private void DispatherTimer_Tick(object sender, EventArgs e)
        {
            if (!CurrentTime.IsOver())
            {
                CurrentTime.DecreaseTime();
                VisibleTime = CurrentTime.ToString();
            }
            else
            {
                OnTimerDone();
            }

        }

        public delegate void TimerDoneEventHandler(object sender, EventArgs e);
        public event TimerDoneEventHandler TimerDone;
        #endregion


        #region Commands
        public ICommand StartCommand { get { return new DelegateCommand(StartTimer); } }
        public ICommand StopCommand { get { return new DelegateCommand(StopTimer); } }
        #endregion


        #region methods

        public void StopTimer()
        {
            StopVisibility = Visibility.Collapsed;
            StartVisibility = Visibility.Visible;
            Timer.Stop();
        }

         protected virtual void OnTimerDone()
        {
            if(TimerDone != null)
            {
                TimerDone(this, EventArgs.Empty);
            }
        }
        public void StartTimer()
        {
            if (!IsRunning())
            {
                StartVisibility = Visibility.Collapsed;
                StopVisibility = Visibility.Visible;
            }

            Timer.Start();
        }

        public bool IsRunning()
        {
            return Timer.IsEnabled;
        }


        public void RefreshTime()
        {
            CurrentTime.RefreshTime();
            StopTimer();
            VisibleTime = CurrentTime.ToString();
        }
        #endregion

    }
}
