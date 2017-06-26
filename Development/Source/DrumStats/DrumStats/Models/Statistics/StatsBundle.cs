using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrumStats.Models.Statistics
{
    public class StatsBundle
    {
        [JsonProperty("winRateAbsolute")]
        public IEnumerable<WinRate> WinRatesAbsolute { get; set; }

        [JsonProperty("winRateRelative")]
        public IEnumerable<WinRate> WinRatesRelative { get; set; }

        [JsonProperty("playCount")]
        public IEnumerable<PlayCount> PlayCounts { get; set; }
    }
}
