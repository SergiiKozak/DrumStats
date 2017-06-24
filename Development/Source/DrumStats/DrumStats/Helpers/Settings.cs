using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrumStats.Helpers
{
    public static class Settings
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        private const string IsPlayerSubstitutionEnabledKey = "is_player_substitution_enabled";
        private static readonly bool IsPlayerSubstitutionEnabledDefault = true;
        public static bool IsPlayerSubstitutionEnabled
        {
            get
            {
                return AppSettings.GetValueOrDefault<bool>(IsPlayerSubstitutionEnabledKey, IsPlayerSubstitutionEnabledDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue<bool>(IsPlayerSubstitutionEnabledKey, value);
            }
        }

    }
}
