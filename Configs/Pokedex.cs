using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace PokemonRPG.Configs
{
    [Serializable]
    public class Pokedex
    {

       

        public List<Pokemon> PokemonDexList { get; set; }
        public List<EvolutionData> EvolutionList { get; set; }
        public Pokedex()
        {
            PokemonDexList = new List<Pokemon>();
            EvolutionList = new List<EvolutionData>();
        }

       

    }
}
