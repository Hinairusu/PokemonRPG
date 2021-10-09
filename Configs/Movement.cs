using System;

namespace PokemonRPG.Configs
{
    [Serializable]
    public class Movement
    {
        public int Overland { get; set; }
        public int Sky { get; set; }
        public int Surface { get; set; }
        public int Jump { get; set; }
        public int Burrow { get; set; }
        public int Underwater { get; set; }
    }
}