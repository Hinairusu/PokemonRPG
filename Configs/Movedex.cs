using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PokemonRPG.Configs
{
    [Serializable]
    public class Movedex
    {
        public Movedex()
        {
            MoveList = new List<PokemonMove>();
            ContestMoveList = new List<ContestMoveStats>();
            AdvancementMoveOptions = new ObservableCollection<PokemonMove>();

        }

        public List<PokemonMove> MoveList { get; set; }
        public ObservableCollection<PokemonMove> AdvancementMoveOptions { get; set; }

        public List<ContestMoveStats> ContestMoveList { get; set; }
    }
}