using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Pomodoro.ViewModels
{
    public class MainViewModel : ViewModel
    {

        #region privates
        private TabViewModel tab;
        private ObservableCollection<TabViewModel> tabs;
        private int _selected;
        #endregion

        #region Properties
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
        #endregion
        public MainViewModel()
        {
            CreateTabs();
            Selected = 0;
        }

        private void CreateTabs()
        {
            Tabs = new ObservableCollection<TabViewModel>();
            Tabs.Add(new TabViewModel("Pomodoro", 5, "Its time to focus!!"));
            Tabs.Add(new TabViewModel("Short Break", 5, "Its time to take a short break!!"));
            Tabs.Add(new TabViewModel("Long Break", 5, "Its time to takes a long break!!"));
        }

        private void RefreshTabs()
        {
            foreach(TabViewModel currTab in Tabs)
            {
                currTab.RefreshTime();
            }
        }
    }
}
