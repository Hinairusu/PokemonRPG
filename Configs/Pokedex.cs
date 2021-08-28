using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace PokemonRPG.Configs
{
    [Serializable]
    public class Pokedex
    {

       

        public List<Pokemon> PokemonDexList { get; set; }
        public List<EvolutionData> EvolutionList { get; set; }
        public Pokedex()
        {
            PokemonDexList = new List<Pokemon>();
            EvolutionList = new List<EvolutionData>();
        }

       

    }

    public class Naturedex
    {
        public List<InherentNature> Natures { get; set; }
        public Naturedex()
        {
            Natures = new List<InherentNature>();
        }
    }

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
            PossibleTMMoves = new List<int>();
            Sex = new Gender();
            EvolutionIDs = new List<int>();
            Capability = new List<int>();
        }
    }

    public class WildPokemon : Pokemon
    {
        public int CurrentHP { get; set; }
        public int MaxHP { get; set; }
        public BaseCharacteristics CombatStats { get; set; }
        public BaseCharacteristics ActualStats { get; set; }
        public InherentNature Nature { get; set; }
        public BaseCharacteristics LevelUpPoints { get; set; }
        public BaseCharacteristics Enhancements { get; set; }
        public string ActualSex { get; set; }
        public int Level { get; set; }
        public WildPokemon()
        {
            Level = 1;
            Nature = new InherentNature();
            LevelUpPoints = new BaseCharacteristics();
            Enhancements = new BaseCharacteristics();
            RecalculateStats(); 
            CombatStats = new BaseCharacteristics();
            CombatStats = ActualStats;

            MaxHP = Level + (ActualStats.HP * 3);
            CurrentHP = MaxHP;
        }
        private void RecalculateStats()
        {
            ActualStats = new BaseCharacteristics()
            {
                HP = BaseStats.HP + LevelUpPoints.HP,
                Attack = BaseStats.Attack + LevelUpPoints.Attack,
                Defence = BaseStats.Defence + LevelUpPoints.Defence,
                Speed = BaseStats.Speed + LevelUpPoints.Speed,
                SpecialDefence = BaseStats.SpecialDefence + LevelUpPoints.SpecialDefence,
                SpecialAttack = BaseStats.SpecialAttack + LevelUpPoints.SpecialAttack,
            };
        }
        public void RecalculateCombatStats()
        {
            if (Enhancements.HP > 0)
                CombatStats.HP = ActualStats.HP * (100 + (Enhancements.HP * 25));
            else
                CombatStats.HP = ActualStats.HP * (100 - ((Enhancements.HP * 25) / 2));

            if (Enhancements.Attack > 0)
                CombatStats.Attack = ActualStats.Attack * (100 + (Enhancements.Attack * 25));
            else
                CombatStats.Attack = ActualStats.Attack * (100 - ((Enhancements.Attack * 25) / 2));

            if (Enhancements.Defence > 0)
                CombatStats.Defence = ActualStats.Defence * (100 + (Enhancements.Defence * 25));
            else
                CombatStats.Defence = ActualStats.Defence * (100 - ((Enhancements.Defence * 25) / 2));

            if (Enhancements.SpecialAttack > 0)
                CombatStats.SpecialAttack = ActualStats.SpecialAttack * (100 + (Enhancements.SpecialAttack * 25));
            else
                CombatStats.SpecialAttack = ActualStats.SpecialAttack * (100 - ((Enhancements.SpecialAttack * 25) / 2));

            if (Enhancements.SpecialDefence > 0)
                CombatStats.SpecialDefence = ActualStats.SpecialDefence * (100 + (Enhancements.SpecialDefence * 25));
            else
                CombatStats.SpecialDefence = ActualStats.SpecialDefence * (100 - ((Enhancements.SpecialDefence * 25) / 2));
        }
    }

    public class TrainerPokemon : WildPokemon
    {
        public HiddenCharacteristics IVs { get; set; }
        public HiddenCharacteristics EVs { get; set; }
        public ContestCharacteristics ContestStats { get; set; }
        public string Nickname { get; set; }
        public int Loyalty { get; set; }
        public int CurrentExperience { get; set; }
        public bool Alive { get; set; }
        public int TrainerID { get; set; }
        public int PokemonTID { get; set; }
        public TrainerPokemon()
        {
            IVs = new HiddenCharacteristics();
            EVs = new HiddenCharacteristics();
            ContestStats = new ContestCharacteristics();
            Alive = true;
            RecalculateNewStats();
        }

       

        private void RecalculateNewStats()
        {
            ActualStats = new BaseCharacteristics()
            {
                HP = BaseStats.HP + LevelUpPoints.HP + IVs.HP + (EVs.HP / 4),
                Attack = BaseStats.Attack + LevelUpPoints.Attack + IVs.Attack + (EVs.Attack / 4),
                Defence = BaseStats.Defence + LevelUpPoints.Defence + IVs.Defence + (EVs.Defence / 4),
                Speed = BaseStats.Speed + LevelUpPoints.Speed + IVs.Speed + (EVs.Speed / 4),
                SpecialDefence = BaseStats.SpecialDefence + LevelUpPoints.SpecialDefence + IVs.SpecialDefence + (EVs.SpecialDefence / 4),
                SpecialAttack = BaseStats.SpecialAttack + LevelUpPoints.SpecialAttack + IVs.SpecialAttack + (EVs.SpecialAttack / 4)
            };
        }
        public override string ToString()
        {
            return $"{PokemonTID}";
        }

    }




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

    [Serializable]
    public class EvolutionData
    {
        public int UID { get; set; }
        public int EvolutionID { get; set; }
        public string EvolutionConditions { get; set; }
        public int EvolutionStage { get; set; }
        public bool CanEvolve { get; set; }
        

    }
    [Serializable]
    public class LevelMoves
    {
        public int LevelLearned { get; set; }
        public int MoveID { get; set; }
    }
    [Serializable]
    public class BaseCharacteristics
    {
        public int HP { get; set; }
        public int Attack { get; set; }
        public int Defence { get; set; }
        public int SpecialAttack { get; set; }
        public int SpecialDefence { get; set; }
        public int Speed { get; set; }

        public static BaseCharacteristics operator +(BaseCharacteristics v1, BaseCharacteristics v2)
        {
            BaseCharacteristics newbase = new BaseCharacteristics();
            newbase.HP = v1.HP + v2.HP;
            newbase.Attack = v1.Attack + v2.Attack;
            newbase.Defence = v1.Defence + v2.Defence;
            newbase.Speed = v1.Speed + v2.Speed;
            newbase.SpecialDefence = v1.SpecialDefence + v2.SpecialDefence;
            newbase.SpecialAttack = v1.SpecialAttack + v2.SpecialAttack;
            return newbase;
        }

    }
    [Serializable]
    public class ContestCharacteristics
    {
        public int Cool { get; set; }
        public int Tough { get; set; }
        public int Beauty { get; set; }
        public int Smart { get; set; }
        public int Cute { get; set; }
    }
    [Serializable]
    public class HiddenCharacteristics : BaseCharacteristics
    {
        public int StatTotal { get; set; }
    }
    [Serializable]
    public class Habitat
    {
        public string Location { get; set; }
        public string Rarity { get; set; }
    }
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
    [Serializable]
    public class PokedexEntry
    {
        public string Description { get; set; }
        public int Number { get; set; }
        public string SpeciesType { get; set; }
    }
    
    [Serializable]
    public class Dice
    {
        public int DiceCount { get; set; }
        public int DiceSize { get; set; }
        public int DiceMod { get; set; }
    }
    
    [Serializable]
    public class Gender
    {
        public bool Male { get; set; }
        public bool Female { get; set; }
        public bool Genderless { get; set; }
        public decimal FemaleRatio { get; set; }
    }
    public class InherentNature : BaseCharacteristics
    {
        public string Name { get; set; }
        public string FlavourLike { get; set; }
        public string FlavourHate { get; set; }
    }
}
