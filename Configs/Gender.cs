using System;

namespace PokemonRPG.Configs
{
    [Serializable]
    public class Gender
    {
        public bool Male { get; set; }
        public bool Female { get; set; }
        public bool Genderless { get; set; }
        public decimal FemaleRatio { get; set; }
    }
}