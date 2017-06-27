using System;
using System.Collections.Generic;
using System.Text;
using DrumStats.Helpers;

namespace DrumStats.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
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
    }
}
