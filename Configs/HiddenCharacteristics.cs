using System;

namespace PokemonRPG.Configs
{
    [Serializable]
    public class HiddenCharacteristics : BaseCharacteristics
    {
        public int StatTotal { get; set; }
    }
}