using System;

namespace PokemonRPG.Configs
{
    [Serializable]
    public class MoveLearningItem
    {
        public int UID { get; set; }
        public int MoveUID { get; set; }
        public int ItemUID { get; set; }
        public string TMNumber { get; set; }
        public bool IsHM { get; set; }
    }
}