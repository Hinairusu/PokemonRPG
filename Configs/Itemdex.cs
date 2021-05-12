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
    [Serializable]
    public class Item
    {
        public int ItemID { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string UnitValue { get; set; }
        public string Category { get; set; }
        public List<string> Flavours { get; set; }
        public string Frequency { get; set; }
        public string Effect { get; set; }
        public string Notes { get; set; }
        public int Rarity { get; set; }
        public Item()
        {
            Flavours = new List<string>();
        }
    }
    [Serializable]
    public class MoveLearningItem : Item
    {
        public string Number { get; set; }
        public string MoveName { get; set; }
        public bool IsHM { get; set; }
        public bool isTM { get; set; }

    }
}
