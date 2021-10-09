using System;

namespace PokemonRPG.Configs
{
    [Serializable]
    public class PokedexEntry
    {
        public string Description { get; set; }
        public int Number { get; set; }
        public string SpeciesType { get; set; }
    }
}