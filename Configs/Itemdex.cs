using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonRPG.Configs
{
    [Serializable]
    public class Itemdex
    {
        public List<Item> Items { get; set; }
        public List<MoveLearningItem> MoveLearningItems { get; set; }
        public Itemdex()
        {
            Items = new List<Item>();
            MoveLearningItems = new List<MoveLearningItem>();
        }
    }
}
