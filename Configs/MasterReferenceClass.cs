using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonRPG.Configs
{
    public class MasterReferenceClass
    {

        
        public Pokedex Pokedex { get; set; }
        public Itemdex ItemDex { get; set; }
        public Naturedex NatureDex { get; set; }
        public Trainerdex TrainerDex { get; set; }
        public Movedex MoveDex { get; set; }
        public Typedex TypeDex { get; set; }


        public Abilitydex AbilityDex { get; set; }

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

        #region Generation of Pokemon

        MapperConfiguration PktoTrPk = new MapperConfiguration(cfg => cfg.CreateMap<Pokemon, TrainerPokemon>());
        MapperConfiguration PktoWiPk = new MapperConfiguration(cfg => cfg.CreateMap<Pokemon, WildPokemon>());

        public TrainerPokemon GenerateTrainerPokemon(WildPokemon Poke)
        {
            return GenerateTrainerPokemon(Poke.UID);
        }
        public TrainerPokemon GenerateTrainerPokemon(Pokemon Poke)
        {
            return GenerateTrainerPokemon(Poke.UID);
        }
        public TrainerPokemon GenerateTrainerPokemon(int UID)
        {
            TrainerPokemon poke = new TrainerPokemon();
            Pokemon pk = new Pokemon();
            var mapper = PktoTrPk.CreateMapper();
            return mapper.Map<TrainerPokemon>(Pokedex.PokemonDexList.Find(s => s.UID.Equals(UID)));
        }
        public WildPokemon GenerateWildPokemon(Pokemon Poke)
        {
            return GenerateWildPokemon(Poke.UID);
        }
        public WildPokemon GenerateWildPokemon(int UID)
        {
            WildPokemon poke = new WildPokemon();
            Pokemon pk = new Pokemon();
            var mapper = PktoWiPk.CreateMapper();
            return mapper.Map<WildPokemon>(Pokedex.PokemonDexList.Find(s => s.UID.Equals(UID)));
        }

        #endregion
    }
}
