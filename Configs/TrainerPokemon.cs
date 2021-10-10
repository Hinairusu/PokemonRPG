using System.Collections.Generic;

namespace PokemonRPG.Configs
{
    public class TrainerPokemon : WildPokemon
    {
        public TrainerPokemon()
        {
            IVs = new HiddenCharacteristics();
            EVs = new HiddenCharacteristics();
            ContestStats = new ContestCharacteristics();
            ArtificalMoves = new List<PokemonMove>();
            Alive = true;
            RecalculateNewStats();
            RecalculateCombatStats();
        }

        public HiddenCharacteristics IVs { get; set; }
        public HiddenCharacteristics EVs { get; set; }
        public ContestCharacteristics ContestStats { get; set; }
        public string Nickname { get; set; }
        public int Loyalty { get; set; }
        public int CurrentExperience { get; set; }
        public bool Alive { get; set; }
        public int TrainerID { get; set; }
        public int PokemonTID { get; set; }

        public List<PokemonMove> ArtificalMoves { get; set; }


        public void LevelUp(int statLocation = 0)
        {
            if (statLocation == 0) statLocation = StaticData.ReferenceData.RandomGenerator.Next(1, 7);

            if (statLocation == 1)
                LevelUpPoints.Attack++;
            else if (statLocation == 2)
                LevelUpPoints.Defence++;
            else if (statLocation == 4)
                LevelUpPoints.SpecialAttack++;
            else if (statLocation == 5)
                LevelUpPoints.SpecialDefence++;
            else if (statLocation == 6)
                LevelUpPoints.Speed++;
            else
                LevelUpPoints.HP++;

            RecalculateNewStats();
            RecalculateCombatStats();
            CheckLevelMoves();
        }

        private void RecalculateNewStats()
        {
            ActualStats = new BaseCharacteristics
            {
                HP = BaseStats.HP + LevelUpPoints.HP + IVs.HP + EVs.HP / 4,
                Attack = BaseStats.Attack + LevelUpPoints.Attack + IVs.Attack + EVs.Attack / 4,
                Defence = BaseStats.Defence + LevelUpPoints.Defence + IVs.Defence + EVs.Defence / 4,
                Speed = BaseStats.Speed + LevelUpPoints.Speed + IVs.Speed + EVs.Speed / 4,
                SpecialDefence = BaseStats.SpecialDefence + LevelUpPoints.SpecialDefence + IVs.SpecialDefence +
                                 EVs.SpecialDefence / 4,
                SpecialAttack = BaseStats.SpecialAttack + LevelUpPoints.SpecialAttack + IVs.SpecialAttack +
                                EVs.SpecialAttack / 4
            };
            MaxHP = Level + ActualStats.HP * 3;
        }

        public override string ToString()
        {
            return $"{PokemonTID}";
        }
    }
}