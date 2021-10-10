using System.Collections.Generic;
using PokemonRPG.Configs;

namespace PokemonRPG.Windows
{
    public class DexResults
    {
        public DexResults()
        {
            PokeTarget = new Pokemon();
            Nature = new InherentNature();
            MoveList = new List<PokemonMove>();
        }

        public bool StageOne { get; set; } // First Round, Display Primary Type only
        public bool StageTwo { get; set; } // Second Round, Description, Highest Base Stat?
        public bool StageThree { get; set; }
        public Pokemon PokeTarget { get; set; }
        public InherentNature Nature { get; set; }
        public int Level { get; set; }
        public bool ShowLevel { get; set; }
        public bool Shiny { get; set; }
        public List<PokemonMove> MoveList { get; set; }
        public bool Capability { get; set; }
        public int Attitude { get; set; }
    }
}