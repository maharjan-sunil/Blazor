using System.Text.Json;
using System.Text.Json.Serialization;

namespace Toronto.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [JsonPropertyName("date")]
        public DateTime DateTime { get; set; }

        public decimal Amount { get; set; }
        public int[] Tax { get; set; }

        public override string ToString() => JsonSerializer.Serialize<Product>(this);
    }
}
