using System;
using System.Collections.Generic;

namespace PokemonRPG.Configs
{
    [Serializable]
    public class Item
    {
        public Item()
        {
            Flavours = new List<int>();
        }

        public int UID { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public int UnitValue { get; set; }
        public int Category { get; set; }
        public List<int> Flavours { get; set; }
        public string Frequency { get; set; }
        public string Effect { get; set; }
        public string Notes { get; set; }
        public int Rarity { get; set; }
    }
}