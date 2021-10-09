using System;
using System.Collections.Generic;

namespace PokemonRPG.Configs
{
    [Serializable]

    public class TrainerClass
    {
        public int UID { get; set; }
        public string Name { get; set; }
        public List<TrainerClassFeature> ClassFeatures { get; set; }
        public TrainerClass()
        {
            ClassFeatures = new List<TrainerClassFeature>();
        }
    }
}