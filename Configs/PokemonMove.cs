using System;

namespace PokemonRPG.Configs
{
    [Serializable]
    public class PokemonMove
    {
        public PokemonMove()
        {
            Damage = new Dice();
        }

        public int MoveID { get; set; }
        public string Name { get; set; }
        public int TypeID { get; set; }
        public string Frequency { get; set; }
        public int Accuracy { get; set; }
        public int Range { get; set; }
        public Dice Damage { get; set; }
        public string Notes { get; set; }
        public string Target { get; set; }
        public string AttackStat { get; set; }
        public int ContestStats { get; set; }

        public override string ToString()
        {
            return $"{Name} - {Damage}";
        }
    }
}