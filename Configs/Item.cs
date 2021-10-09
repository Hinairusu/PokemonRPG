using System;
using System.Collections.Generic;

namespace PokemonRPG.Configs
{
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
}