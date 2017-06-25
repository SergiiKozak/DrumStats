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

        private string attackWinRate;
        [JsonProperty("offenseWinRate")]
        public string AttackWinRate
        {
            get { return attackWinRate; }
            set { SetProperty(ref attackWinRate, value); }
        }

        private string defenceWinRate;
        [JsonProperty("defenseWinRate")]
        public string DefenceWinRate
        {
            get { return defenceWinRate; }
            set { SetProperty(ref defenceWinRate, value); }
        }
    }
}
