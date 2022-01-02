using System;
using System.Collections.Generic;

namespace PokemonRPG.Configs
{
    [Serializable]
    public class Pokedex
    {
        public Pokedex()
        {
            PokemonDexList = new List<Pokemon>();
            EvolutionList = new List<EvolutionData>();
            SexList = new List<Gender>();

        }


        public List<Pokemon> PokemonDexList { get; set; }
        public List<EvolutionData> EvolutionList { get; set; }
        public List<Gender> SexList { get; set; }
    }
}