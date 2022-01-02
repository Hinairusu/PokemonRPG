using System;
using System.Collections.Generic;

namespace PokemonRPG.Configs
{
    [Serializable]
    public class Itemdex
    {
        public Itemdex()
        {
            Items = new List<Item>();
            MoveLearningItems = new List<MoveLearningItem>();
            ItemCategories = new List<ItemCategory>();
        }

        public List<Item> Items { get; set; }
        public List<MoveLearningItem> MoveLearningItems { get; set; }
        public List<ItemCategory> ItemCategories { get; set; }
    }
}