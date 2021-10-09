using System;

namespace PokemonRPG.Configs
{
    [Serializable]
    public class TrainerFeatureTags
    {
        public bool Extended { get; set; }
        public bool LeagueLegal { get; set; }
        public bool Static { get; set; }
        public bool FreeAction { get; set; }
        public bool Interrupt { get; set; }
        public bool NonPossession { get; set; }
    }
}