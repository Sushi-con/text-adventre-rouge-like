using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace text_adventer_rouge_like.models
{
    public class Enemies
    {
        [JsonPropertyName("id")]
        public int Id {  get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("dammage")]
        public int Dammage { get; set; }
        [JsonPropertyName("hp")]
        public int HP { get; set; }
    }
}
