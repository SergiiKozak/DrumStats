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

        private const string CutOffDaysKey = "cut_off_days";
        private static readonly int CutOffDaysDefault = 0;
        public static int CutOffDays
        {
            get
            {
                return AppSettings.GetValueOrDefault<int>(CutOffDaysKey, CutOffDaysDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue<int>(CutOffDaysKey, value);
            }
        }

        private const string CutOffGamesKey = "cut_off_games";
        private static readonly int CutOffGamesDefault = 0;
        public static int CutOffGames
        {
            get
            {
                return AppSettings.GetValueOrDefault<int>(CutOffGamesKey, CutOffGamesDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue<int>(CutOffGamesKey, value);
            }
        }

    }
}
