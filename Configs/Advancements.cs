using System;
using System.Linq;

namespace PokemonRPG.Configs
{
    public class Advancements
    {
        public int UID { get; set; }
        public int LevelUpUID { get; set; }
        public int Modifier { get; set; }
        public bool ValueAdd { get; set; }
        public string Notes { get; set; }
        public DateTime DateAdded { get; set; }
        public override string ToString()
        {
            return $"{StaticData.ReferenceData.TrainerDex.Advances.Single(s => s.UID.Equals(LevelUpUID))}";
        }
    }
}