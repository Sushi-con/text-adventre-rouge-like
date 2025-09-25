using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace text_adventer_rouge_like.models
{
    public class Store
    {
        private List<Item> Items { get; set; } = new List<Item>();

        public Store(List<Item> Itmes) { this.Items = Itmes; }
        public Item Item1 { get; set; }
        public Item Item2 { get; set; }
        public Item Item3 { get; set; }
        public void GenerateStore()
        {
            Random random = new Random();
            int item1 = random.Next(1, this.Items.Count);
            int item2 = random.Next(1, this.Items.Count);
            int item3 = random.Next(1, this.Items.Count);

            this.Item1 = this.Items[item1];
            this.Item2 = this.Items[item2];
            this.Item3 = this.Items[item3];
        }
        public override string ToString()
        {
            return $"1. {Item1.ToString()}\n2. {Item2.ToString()}\n3. {Item3.ToString()}";
        }
    }
}
