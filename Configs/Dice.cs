using System;

namespace PokemonRPG.Configs
{
    [Serializable]
    public class Dice
    {
        public int DiceCount { get; set; }
        public int DiceSize { get; set; }
        public int DiceMod { get; set; }

        public override string ToString()
        {
            string Returnval = Returnval = $"{DiceCount}d{DiceSize}";
            if (DiceMod > 0)
                Returnval += $"+{DiceMod}";

            if (Returnval.Equals("0d0"))
                return "None";
            return Returnval;
        }
    }
}