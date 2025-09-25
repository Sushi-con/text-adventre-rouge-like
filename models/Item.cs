using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace text_adventer_rouge_like.models
{
    public class Item
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("statEffects")]
        public List<string> StatEffects { get; set; } = new List<string>();
        [JsonPropertyName("price")]
        public int Price {  get; set; }
        [JsonPropertyName("pointBonus")]
        public int PointBonus { get; set; }

        public override string ToString()
        {
            return $"{this.Name}:\nDescription: {this.Description} Price {this.Price}";
        }
    }
}
