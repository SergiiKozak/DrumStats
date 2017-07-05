using DrumStats.Models;
using DrumStats.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Linq;
using Xamarin.Forms;

namespace DrumStats.Converters
{
    class PlayerShortDisplayNameConverter : IValueConverter
    {
        private IDataStore<Player> PlayerDataStore => DependencyService.Get<IDataStore<Player>>();
        private IEnumerable<Player> Players;

        public PlayerShortDisplayNameConverter()
        {
            Players = PlayerDataStore.GetItemsAsync().Result;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is string))
                return null;
            var player = Players.FirstOrDefault(p => p.Id == (string)value);
            return player?.ShortDisplayName;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is Player))
                return null;

            return ((Player)value).Id;
        }
    }
}
