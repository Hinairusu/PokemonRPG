using System;

namespace PokemonRPG.Configs
{
    [Serializable]
    public class MoveLearningItem : Item
    {
        public string Number { get; set; }
        public string MoveName { get; set; }
        public bool IsHM { get; set; }
        public bool isTM { get; set; }

    }
}