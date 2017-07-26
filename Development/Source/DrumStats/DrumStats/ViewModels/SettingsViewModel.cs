using System;
using System.Collections.Generic;
using System.Text;
using DrumStats.Helpers;
using System.Linq;

namespace DrumStats.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        public Dictionary<string, Tuple<int, int>> CutOffMap { get; private set; }

        public SettingsViewModel()
        {
            CutOffMap = new Dictionary<string, Tuple<int, int>>()
            {
                {"None", new Tuple<int, int>(0, 0) },
                {"1 Day", new Tuple<int, int>(1, 0) },
                {"1 Week", new Tuple<int, int>(7, 0) },
                {"2 Weeks", new Tuple<int, int>(14, 0) },
                {"1 Month", new Tuple<int, int>(30, 0) },
                {"3 Months", new Tuple<int, int>(90, 0) },
                {"50 Games", new Tuple<int, int>(0, 50) },
                {"100 Games", new Tuple<int, int>(0, 100) },
                {"200 Games", new Tuple<int, int>(0, 200) },
                {"500 Games", new Tuple<int, int>(0, 500) }
            };
        }

        public string[] CutOffKeys
        {
            get
            {
                return CutOffMap.Keys.ToArray();
            }
        }

        public bool IsPlayerSubstitutionEnabled
        {
            get { return Settings.IsPlayerSubstitutionEnabled; }
            set
            {
                if (Settings.IsPlayerSubstitutionEnabled == value)
                    return;

                Settings.IsPlayerSubstitutionEnabled = value;
                OnPropertyChanged();
            }
        }

        public string SelectedCutOff
        {
            get
            {
                var currentCutOff = new Tuple<int, int>(Settings.CutOffDays, Settings.CutOffGames);

                var key = currentCutOff == null ? null : CutOffMap.FirstOrDefault(kvp => kvp.Value.Equals(currentCutOff)).Key;

                return key;
            }

            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    var item = CutOffMap[value];
                    if (item != null)
                    {
                        Settings.CutOffDays = item.Item1;
                        Settings.CutOffGames = item.Item2;
                        OnPropertyChanged();
                    }
                }
            }
        }
    }
}
