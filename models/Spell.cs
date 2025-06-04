using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace text_adventer_rouge_like.models
{
    public class Spell
    {
        [JsonPropertyName("id")]
        public int id;
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("tag")]
        public string Tag { get; set; }

        public override string ToString()
        {
            return $"{this.Name}: {this.Description}";
        }
    }
}
