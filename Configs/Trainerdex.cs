using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonRPG.Configs
{
    public class Trainerdex
    {
        public List<TrainerClass> Classes { get; set; }
        public Trainerdex()
        {
            Classes = new List<TrainerClass>();
        }
    }
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
    [Serializable]
    public class TrainerFeatureRequisites
    {
        public string Requisite { get; set; }
    }
    [Serializable]
    public class TrainerFeatureTags
    {
        public bool Extended { get; set; }
        public bool LeagueLegal { get; set; }
        public bool Static { get; set; }
        public bool FreeAction { get; set; }
        public bool Interrupt { get; set; }
        public bool NonPossession { get; set; }
    }
}
