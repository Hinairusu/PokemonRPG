using System;

namespace PokemonRPG.Configs
{
    [Serializable]
    public class PokemonStatus
    {
        public bool Shiney { get; set; }
        public bool Infected { get; set; }
        public bool Posioned { get; set; }
        public bool Toxic { get; set; }
        public bool Sleep { get; set; }
        public bool Paralysed { get; set; }
        public bool Burn { get; set; }
        public bool Frozen { get; set; }
    }
}