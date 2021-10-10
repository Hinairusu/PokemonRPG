using System;
using System.Collections.Generic;

namespace PokemonRPG.Configs
{
    [Serializable]
    public class Movedex
    {
        public Movedex()
        {
            MoveList = new List<PokemonMove>();
        }

        public List<PokemonMove> MoveList { get; set; }
    }
}