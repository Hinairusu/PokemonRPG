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
            ContestMoveList = new List<ContestMoveStats>();
        }

        public List<PokemonMove> MoveList { get; set; }

        public List<ContestMoveStats> ContestMoveList { get; set; }
    }
}