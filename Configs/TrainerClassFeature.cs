using System;
using System.Collections.Generic;

namespace PokemonRPG.Configs
{
    [Serializable]
    public class TrainerClassFeature
    {
        public string Name { get; set; }
        public string FrequencyBase { get; set; }
        public string FrequenceyIncrement { get; set; }
        public string Description { get; set; }
        public string Target { get; set; }
        public TrainerFeatureTags Tags { get; set; }
        public List<TrainerFeatureRequisites> Requisites { get; set; }
        public TrainerClassFeature()
        {
            Tags = new TrainerFeatureTags();
            Requisites = new List<TrainerFeatureRequisites>();
        }
    }
}