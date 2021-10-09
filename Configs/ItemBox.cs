using System;
using System.Collections.Generic;

namespace PokemonRPG.Configs
{
    [Serializable]
    public class ItemBox
    {
        public string BoxName { get; set; }
        public int BoxNo { get; set; }
        public List<Item> BoxContents { get; set; }
        public List<MoveLearningItem> BoxedLearningItems { get; set; }
        public ItemBox()
        {
            BoxContents = new List<Item>();
            BoxedLearningItems = new List<MoveLearningItem>();
        }    
    }
}