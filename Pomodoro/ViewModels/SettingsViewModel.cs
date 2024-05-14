using System;
using Pomodoro.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using System.Windows.Input;
using Pomodoro.Utils;
using System.Windows;

namespace Pomodoro.ViewModels
{
    public class SettingsViewModel
    {
        #region privates
        private static SettingsViewModel _instance;
        private string fileLocation;
        private Settings mySettings;
        #endregion

        #region Properties

        public string FileLocation
        {
            get { return fileLocation; }
            set
            {
                if (fileLocation != value)
                {
                    fileLocation = value;
                }
            }
        }

        public Settings MySettings
        {
            get { return mySettings; }
            set
            {
                if (mySettings != value)
                {
                    mySettings = value;
                }
            }
        }
        #endregion


        public static SettingsViewModel GetInstance()
        {
            if (_instance == null)
            {
                _instance = new SettingsViewModel();
            }
            return _instance;
        }

        private SettingsViewModel()
        {
            string sCurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            FileLocation = Path.Combine(sCurrentDirectory, "UserConfig.xml");
            if (!File.Exists(FileLocation))
            {
                File.WriteAllText(FileLocation, "<?xml version=\"1.0\" encoding=\"utf-8\"?>\n<Settings xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">\n<PomodoroTime>25</PomodoroTime>\n<ShortBreakTime>5</ShortBreakTime>\n<LongBreakTime>15</LongBreakTime>\n</Settings>");
            }
            LoadSettings();
        }
        #region Commands
        public ICommand SaveCommand { get { return new DelegateCommand(SaveSettings); } }


        #endregion
        #region methods
        private void SaveSettings()
        {
            this.Save();
            MessageBox.Show("Settings Updated");
        }
        public void LoadSettings()
        {
            MySettings = null;
            XmlSerializer serializer = new XmlSerializer(typeof(Settings));
            StreamReader reader = new StreamReader(FileLocation);
            MySettings = (Settings)serializer.Deserialize(reader);
            reader.Close();
        }

        public void Save()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Settings));
            StreamWriter writer = new StreamWriter(FileLocation);
            serializer.Serialize(writer, MySettings);
            writer.Close();
        }
        #endregion
    }
}
