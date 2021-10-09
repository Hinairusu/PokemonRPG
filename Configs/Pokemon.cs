using System;
using System.Collections.Generic;

namespace PokemonRPG.Configs
{
    [Serializable]
    public class Pokemon 
    {
        public int UID { get; set; }
        public string Name { get; set; }
        public string Size { get; set; }
        public int WeightClass { get; set; }
        public int ExperinceValue { get; set; }
        public Gender Sex { get; set; }
        public int PrimaryTypeID { get; set; }
        public int SecondaryTypeID { get; set; }
        public List<string> EggGroup { get; set; }
        public List<Habitat> Habitats { get; set; }
        public Movement Movements { get; set; }
        public int Power { get; set; }
        public int Intellegence { get; set; }
        public List<int> Capability { get; set; }
        public PokedexEntry PokedexResult { get; set; }
        public int HatchRate { get; set; }
        public int HatchTimeRemaining { get; set; }
        public int CatchRate { get; set; }
        public List<LevelMoves> PossibleLevelupMoves { get; set; }
        public List<int> PossibleTMMoves { get; set; }

        public List<int> PossibleTutorMoves { get; set; }
        public List<int> PossibleEggMoves { get; set; }
        public BaseCharacteristics BaseStats { get; set; }
        public List<int> EvolutionIDs { get; set; }
        public string PokemonFamily { get; set; }
        public Pokemon()
        {
            BaseStats = new BaseCharacteristics() { HP = 1, Attack = 1, Defence = 1, SpecialAttack = 1, SpecialDefence = 1, Speed = 1 };
            EggGroup = new List<string>();
            Habitats = new List<Habitat>();
            Movements = new Movement();
            PokedexResult = new PokedexEntry();
            PossibleEggMoves = new List<int>();
            PossibleLevelupMoves = new List<LevelMoves>();
            PossibleTutorMoves = new List<int>();
            PossibleTMMoves = new List<int>();
            Sex = new Gender();
            EvolutionIDs = new List<int>();
            Capability = new List<int>();
        }
    }
}