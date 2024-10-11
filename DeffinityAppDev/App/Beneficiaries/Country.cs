using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeffinityAppDev.App.Beneficiaries
{
    // Country.cs
    public class Country
    {
        [JsonProperty("name")]
        public Name Name { get; set; }

        [JsonProperty("idd")]
        public Idd Idd { get; set; }

        // This property will hold the combined dialing code
        public string DialingCode
        {
            get
            {
                if (Idd != null && !string.IsNullOrEmpty(Idd.Root))
                {
                    string suffix = Idd.Suffixes != null && Idd.Suffixes.Count > 0 ? Idd.Suffixes[0] : "";
                    return $"{Idd.Root}{suffix}";
                }
                return "";
            }
        }

        public string DisplayName
        {
            get
            {
                return $"{Name.Common} ({DialingCode})";
            }
        }
    }

    public class Name
    {
        [JsonProperty("common")]
        public string Common { get; set; }

        [JsonProperty("official")]
        public string Official { get; set; }
    }

    public class Idd
    {
        [JsonProperty("root")]
        public string Root { get; set; }

        [JsonProperty("suffixes")]
        public List<string> Suffixes { get; set; }
    }

}