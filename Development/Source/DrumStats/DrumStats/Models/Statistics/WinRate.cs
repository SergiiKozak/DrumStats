using DrumStats.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrumStats.Models.Statistics
{
    public class WinRate : ObservableObject
    {
        private string playerId;
        [JsonProperty("Id")]
        public string PlayerId
        {
            get { return playerId; }
            set { SetProperty(ref playerId, value); }
        }

        private decimal attackWinRate;
        [JsonProperty("offenseWinRate")]
        public decimal AttackWinRate
        {
            get { return attackWinRate; }
            set { SetProperty(ref attackWinRate, value); }
        }

        private decimal defenceWinRate;
        [JsonProperty("defenseWinRate")]
        public decimal DefenceWinRate
        {
            get { return defenceWinRate; }
            set { SetProperty(ref defenceWinRate, value); }
        }

        private decimal totalWinRate;
        [JsonProperty("totalWinRate")]
        public decimal TotalWinRate
        {
            get { return totalWinRate; }
            set { SetProperty(ref totalWinRate, value); }
        }
    }
}
