using System;
using System.Collections.Generic;

namespace PokemonRPG.Configs
{
    [Serializable]
    public class TrainerClass
    {
        public TrainerClass()
        {
            ClassFeatures = new List<TrainerClassFeature>();
        }

        public int UID { get; set; }
        public string Name { get; set; }
        public List<TrainerClassFeature> ClassFeatures { get; set; }
    }
}