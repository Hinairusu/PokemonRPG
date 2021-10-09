using System;

namespace PokemonRPG.Configs
{
    [Serializable]
    public class LevelMoves
    {
        public int LevelLearned { get; set; }
        public int MoveID { get; set; }
    }
}