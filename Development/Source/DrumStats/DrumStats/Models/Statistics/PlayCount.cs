using DrumStats.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrumStats.Models.Statistics
{
    public class PlayCount : ObservableObject
    {
        private string playerId;
        [JsonProperty("Id")]
        public string PlayerId
        {
            get { return playerId; }
            set { SetProperty(ref playerId, value); }
        }

        private int attackPlayCount;
        [JsonProperty("offensePlayCount")]
        public int AttackPlayCount
        {
            get { return attackPlayCount; }
            set { SetProperty(ref attackPlayCount, value); }
        }

        private int defencePlayCount;
        [JsonProperty("defensePlayCount")]
        public int DefencePlayCount
        {
            get { return defencePlayCount; }
            set { SetProperty(ref defencePlayCount, value); }
        }
    }
}
