using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TireShopParserAdminPanel.Models
{
    [Serializable]
    public class Product
    {
        public Product()
        {
            Specifications = new HashSet<Specification>();
        }
        [JsonPropertyName("id")] public uint? SiteId { get; set; }
        [JsonPropertyName("title")] public string Title { get; set; }
        [JsonPropertyName("price")] public decimal Price { get; set; }
        [JsonPropertyName("quantity")] public int Quantity { get; set; }
        [JsonPropertyName("description")] public string Description { get; set; }
        [JsonPropertyName("product_url")] public string Url { get; set; }
        [JsonPropertyName("image_url")] public string ImageSrc { get; set; }
        [JsonPropertyName("specifications")] public HashSet<Specification> Specifications { get; set; }
    }
}




