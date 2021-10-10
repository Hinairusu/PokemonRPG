using System;
using AutoMapper;

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

        private MapperConfiguration PktoTrPk = new MapperConfiguration(cfg => cfg.CreateMap<Pokemon, TrainerPokemon>());

        private readonly MapperConfiguration WiPktoTrPk =
            new MapperConfiguration(cfg => cfg.CreateMap<Pokemon, TrainerPokemon>());

        private readonly MapperConfiguration PktoWiPk =
            new MapperConfiguration(cfg => cfg.CreateMap<Pokemon, WildPokemon>());

        public TrainerPokemon GenerateTrainerPokemon(WildPokemon Poke)
        {
            return GenerateTrainerPokemon(Poke.UID);
        }

        public TrainerPokemon GenerateTrainerPokemon(Pokemon Poke)
        {
            return GenerateTrainerPokemon(Poke.UID);
        }

        public TrainerPokemon GenerateTrainerPokemon(int UID, int level = 1)
        {
            var mapper = WiPktoTrPk.CreateMapper();
            var poke = mapper.Map<TrainerPokemon>(GenerateWildPokemon(UID));
            poke.Level = level;
            for (var i = 1; i < level; i++)
            {
                poke.LevelUp();
                if (level > 50)
                    poke.LevelUp();
                if (level > 75)
                    poke.LevelUp();
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

            return poke;
        }

        public WildPokemon GenerateWildPokemon(Pokemon Poke)
        {
            return GenerateWildPokemon(Poke.UID);
        }

        public WildPokemon GenerateWildPokemon(int UID, int level = 0)
        {
            var pk = new Pokemon();
            var mapper = PktoWiPk.CreateMapper();
            var poke = mapper.Map<WildPokemon>(Pokedex.PokemonDexList.Find(s => s.UID.Equals(UID)));
            if (level > 0)
            {
                poke.Level = level;
                for (var i = 1; i < level; i++)
                    poke.WildLevelUp();
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

            return poke;
        }

        #endregion
    }
}