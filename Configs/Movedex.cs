using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonRPG.Configs
{
    [Serializable]
    public class Movedex
    {
        public List<PokemonMove> MoveList { get; set; }
        public Movedex()
        {
            MoveList = new List<PokemonMove>();
        }
    }

    [Serializable]
    public class PokemonMove
    {
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
        public ContestMoveStats ContestStats {get;set;}
        public PokemonMove()
        {
            Damage = new Dice();
            ContestStats = new ContestMoveStats();
        }
    }

    public class ContestMoveStats
    {
        public ContestType Type { get; set; }
        public Dice Appeal { get; set; }
        public string Effect { get; set; }
        public ContestMoveStats()
        {
            Appeal = new Dice();
        }
    }
    public class ContestType
    {
        public string Name { get; set; }
    }
}
