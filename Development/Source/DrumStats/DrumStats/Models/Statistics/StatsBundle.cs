using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrumStats.Models.Statistics
{
    public class StatsBundle
    {
        [JsonProperty("winRateAbsolute")]
        public IEnumerable<Rate> WinRatesAbsolute { get; set; }

        [JsonProperty("winRateRelative")]
        public IEnumerable<Rate> WinRatesRelative { get; set; }

        [JsonProperty("goalRate")]
        public IEnumerable<Rate> GoalRate { get; set; }

        [JsonProperty("playCount")]
        public IEnumerable<PlayCount> PlayCounts { get; set; }

        [JsonProperty("bestPartner")]
        public IEnumerable<Person> BestPartners { get; set; }

        [JsonProperty("worstPartner")]
        public IEnumerable<Person> WorstPartners { get; set; }

        [JsonProperty("victim")]
        public IEnumerable<Person> Victims { get; set; }

        [JsonProperty("nemesis")]
        public IEnumerable<Person> Nemeses { get; set; }
    }
}
