using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Pomodoro.Utils;

namespace Pomodoro.ViewModels
{
    public class MainViewModel : ViewModel
    {

        #region privates
        private TabViewModel tab;
        private ObservableCollection<TabViewModel> tabs;
        private SettingsViewModel settings;
        private int _selected;
        private bool settingsOpen;

        #endregion

        #region Properties
      

        public bool SettingsOpen
        {
            get { return settingsOpen; }
            set
            {
                if (settingsOpen != value)
                {
                    settingsOpen = value;
                    OnPropertyChanged(nameof(SettingsOpen));
                }
            }
        }

        public TabViewModel Tab
        {
            get { return tab; }
            set
            {
                if (tab != value)
                {
                    tab = value;
                    OnPropertyChanged(nameof(Tab));
                }
            }
        }

        public SettingsViewModel Settings
        {
            get { return settings; }
            set
            {
                if (settings != value)
                {
                    settings = value;
                    OnPropertyChanged(nameof(Settings));
                }
            }
        }

        public ObservableCollection<TabViewModel> Tabs
        {
            get { return tabs; }
            set
            {
                if (tabs != value)
                {
                    tabs = value;
                    OnPropertyChanged(nameof(Tabs));
                }
            }
        }

        public int Selected
        {
            get { return _selected; }
            set
            {
                _selected = value;
                RefreshTabs();
                OnPropertyChanged(nameof(Selected));
            }
        }
        #endregion

        #region events
        private void CurrTab_TimerDone(object sender, TimerEventArgs e)
        {
            TabViewModel.RefreshTomato = false;
            Selected = (int)e.NextTab;
            if (Settings.MySettings.AutoStart)
            {
                Tabs[Selected].StartTimer();
            }

            if(e.NextTab == PomodoroTypes.POMODORO)
            {
                foreach (TabViewModel tab in Tabs)
                {
                    tab.CurrentTomato = e.NextTomato;
                }
            }
            TabViewModel.RefreshTomato = true;

        }
        #endregion
        public MainViewModel()
        {
            
            Settings = SettingsViewModel.GetInstance();
            CreateTabs();
            SubscribeToTimerDoneEvent();
            Selected = 0;
            SettingsOpen = false;
        }
        #region Commands
        public ICommand ViewSettingsCommand { get{ return new DelegateCommand(ViewSettings); } }
        public ICommand RefreshCommand { get { return new DelegateCommand(RefreshTabs); }}
        #endregion

        #region methods
        private void ViewSettings(object obj)
        {

            SettingsView settingView = new SettingsView(Settings);
            settingView.Owner = obj as Window;
            settingView.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            settingView.ShowDialog();
            
        }
    
        private void CreateTabs()
        {
            Tabs = new ObservableCollection<TabViewModel>();
            Tabs.Add(new TabViewModel("Pomodoro", Settings.MySettings.PomodoroTime, "Its time to focus!!",PomodoroTypes.POMODORO));
            Tabs.Add(new TabViewModel("Short Break", Settings.MySettings.ShortBreakTime, "Its time to take a short break!!",PomodoroTypes.SHORT));
            Tabs.Add(new TabViewModel("Long Break", Settings.MySettings.LongBreakTime, "Its time to takes a long break!!",PomodoroTypes.LONG));
        }

        private void SubscribeToTimerDoneEvent()
        {
            foreach (TabViewModel currTab in Tabs)
            {
                currTab.TimerDone += CurrTab_TimerDone;
            }
        }



        private void RefreshTabs()
        {
            foreach(TabViewModel currTab in Tabs)
            {
                currTab.RefreshTime();
            }
        }
        #endregion
    }
}
