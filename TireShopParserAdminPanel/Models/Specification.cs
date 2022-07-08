using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TireShopParserAdminPanel.Models
{
    [Serializable]
    public class Specification
    {
        public static readonly string Brand = "Brand";
        public static readonly string CarType = "Car type";
        public static readonly string Year = "Year";
        public static readonly string Width = "Width";
        public static readonly string Diameter = "Diameter";
        public static readonly string Profile = "Profile";
        public static readonly string Country = "Counrty";
        public static readonly string SpeedIndex = "Speed index";
        public static readonly string LoadIndex = "Load index";
        public static readonly string Season = "Season";

        [JsonPropertyName("title")] public string Title { get; set; }
        [JsonPropertyName("value")] public string Value { get; set; }
    }
}
