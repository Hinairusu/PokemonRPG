using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;
using AutoMapper;
using Xceed.Wpf.Toolkit.Core.Converters;

namespace PokemonRPG.Configs
{
    public class MasterReferenceClass
    {
        public MasterReferenceClass()
        {
            Pokedex = new Pokedex();
            ItemDex = new Itemdex();
            NatureDex = new Naturedex();
            TrainerDex = new Trainerdex();
            MoveDex = new Movedex();
            TypeDex = new Typedex();
            AbilityDex = new Abilitydex();
        }

        public Random RandomGenerator { get; set; } = new Random();
        public Pokedex Pokedex { get; set; }
        public Itemdex ItemDex { get; set; }
        public Naturedex NatureDex { get; set; }
        public Trainerdex TrainerDex { get; set; }
        public Movedex MoveDex { get; set; }
        public Typedex TypeDex { get; set; }


        public Abilitydex AbilityDex { get; set; }

        #region Generation of Pokemon



        private readonly MapperConfiguration BasePktoTrainterPk =
            new MapperConfiguration(cfg => cfg.CreateMap<Pokemon, TrainerPokemon>());
        
        
        public TrainerPokemon GenerateTrainerPokemon(Pokemon Poke)
        {
            return GenerateRandomTrainerPokemon(Poke.UID);
        }
        public TrainerPokemon GenerateTrainerPokemon(int UID)
        {
            var mapper = BasePktoTrainterPk.CreateMapper();
            var poke = mapper.Map<TrainerPokemon>(GenerateWildPokemon(UID));
            poke.Level = 1;

            return poke;
        }

        public Advancements GenerateRandomAdvancement()
        {
            Advancements advance = new Advancements();
            advance.LevelUpUID = StaticData.ReferenceData.RandomGenerator.Next(2,7);
            advance.Modifier = 1;
            advance.ValueAdd = true;
            advance.DateAdded = DateTime.Now;
            advance.Notes = "Random Advance Generated";

            return advance;

        }
        public TrainerPokemon GenerateRandomTrainerPokemon(int UID, int level = 1)
        {
            var mapper = BasePktoTrainterPk.CreateMapper();
            var poke = mapper.Map<TrainerPokemon>(GenerateWildPokemon(UID));

            poke.Level = level;

            int Moveslot = 0;
            for (var i = 1; i < level; i++)
            {
                poke.LevelUps.Add(GenerateRandomAdvancement());
                if (level > 50)
                    poke.LevelUps.Add(GenerateRandomAdvancement());
                if (level > 75)
                    poke.LevelUps.Add(GenerateRandomAdvancement());
            }
            poke.NaturalMoves = GenerateMoves(poke.PossibleLevelupMoves, level);

            var Ratio = StaticData.ReferenceData.RandomGenerator.NextDouble();
            if (poke.Sex.FemaleRatio > decimal.Parse(Ratio.ToString()) && poke.Sex.Male)
                poke.ActualSex = "Male";
            else if (poke.Sex.Female)
                poke.ActualSex = "Female";
            else
                poke.ActualSex = "Genderless";

            poke.Nature =
                StaticData.ReferenceData.NatureDex.Natures[
                    StaticData.ReferenceData.RandomGenerator.Next(0, StaticData.ReferenceData.NatureDex.Natures.Count)];
            poke.Advance();
            return poke;
        }


        public List<PokemonBattleMove> GenerateMoves(List<LevelMoves> poke, int Level)
        {
            var moves = poke.Where(s => s.LevelLearned <= Level).ToList();

            var Battlemoves = new List<PokemonBattleMove>();
            Random rand = new Random();
            for (int i = 0; i < 7; i++)
            {
                var RandMoveNo = rand.Next(0, moves.Count);
                var MoveIndex = moves[RandMoveNo].MoveID;
                var move = StaticData.ReferenceData.MoveDex.MoveList.Single( m => m.MoveID.Equals(MoveIndex));
                Battlemoves.Add(new PokemonBattleMove()
                    {Move = move, Slot = i});
                try
                {
                    var oldmove = moves.Single(r => r.MoveID.Equals(move.MoveID));
                    moves.Remove(oldmove);
                }
                catch{}
                if (moves.Count < 1)
                    break;
            }

            return Battlemoves;
        }

        public TrainerPokemon GenerateWildPokemon(int UID, int level = 0)
        {
            var pk = new Pokemon();
            var mapper = BasePktoTrainterPk.CreateMapper();
            var poke = mapper.Map<TrainerPokemon>(Pokedex.PokemonDexList.Find(s => s.UID.Equals(UID)));
            if (level > 0)
            {
                int Moveslot = 0;
                poke.Level = level;
                for (var i = 1; i < level; i++)
                {
                    poke.LevelUps.Add(GenerateRandomAdvancement());
                }
                poke.NaturalMoves = GenerateMoves(poke.PossibleLevelupMoves, level);
            }

            var Ratio = StaticData.ReferenceData.RandomGenerator.NextDouble();
            if (poke.Sex.FemaleRatio > decimal.Parse(Ratio.ToString()) && poke.Sex.Male)
                poke.ActualSex = "Male";
            else if (poke.Sex.Female)
                poke.ActualSex = "Female";
            else
                poke.ActualSex = "Genderless";

            poke.Nature =
                StaticData.ReferenceData.NatureDex.Natures[
                    StaticData.ReferenceData.RandomGenerator.Next(0, StaticData.ReferenceData.NatureDex.Natures.Count)];
            poke.Advance();
            return poke;
        }


        public void AdvancePokemon(int PokemonUID, List<Advancements> Advances)
        {

        }
        #endregion
    }
}