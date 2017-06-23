using System;
using DrumStats.Helpers;
using Newtonsoft.Json;

namespace DrumStats.Models
{
    public class BaseDataObject : ObservableObject
    {
        string id = string.Empty;
        [JsonProperty(PropertyName = "_id")]
        public virtual string Id
        {
            get { return id; }
            set { SetProperty(ref id, value); }
        }
    }
}
