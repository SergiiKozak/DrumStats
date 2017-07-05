using DrumStats.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrumStats.Models.Statistics
{
    public class Rate : ObservableObject
    {
        private string playerId;
        [JsonProperty("Id")]
        public string PlayerId
        {
            get { return playerId; }
            set { SetProperty(ref playerId, value); }
        }

        private decimal attackRate;
        [JsonProperty("offenseRate")]
        public decimal AttackRate
        {
            get { return attackRate; }
            set { SetProperty(ref attackRate, value); }
        }

        private decimal defenceRate;
        [JsonProperty("defenseRate")]
        public decimal DefenceRate
        {
            get { return defenceRate; }
            set { SetProperty(ref defenceRate, value); }
        }

        private decimal totalRate;
        [JsonProperty("totalRate")]
        public decimal TotalRate
        {
            get { return totalRate; }
            set { SetProperty(ref totalRate, value); }
        }
    }
}
