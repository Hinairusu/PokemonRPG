using System;

namespace PokemonRPG.Configs
{
    [Serializable]
    public class EvolutionData
    {
        public int UID { get; set; }
        public int EvolutionID { get; set; }
        public string EvolutionConditions { get; set; }
        public int EvolutionStage { get; set; }
        public bool CanEvolve { get; set; }
    }
}