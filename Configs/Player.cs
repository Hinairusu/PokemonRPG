using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PokemonRPG.Configs
{
    [Serializable]
    public class Player
    {
        [NonSerialized]
        public ObservableCollection<TrainerPokemon> Pkmnlist = new ObservableCollection<TrainerPokemon>();

        public Player()
        {
            ItemPC = new List<ItemBox>();
            Levels = new List<PlayerLevels>();
            GymBadges = new List<string>();
            CurrentParty = new List<int>();
            Inventory = new List<Item>();
            ContestRibbons = new List<string>();
            Regions = new List<LocationRegion>();
            OwnedPokemon = new List<TrainerPokemon>();
        }

        public string Name { get; set; }
        public List<ItemBox> ItemPC { get; set; }
        public List<int> CurrentParty { get; set; }
        public List<TrainerPokemon> OwnedPokemon { get; set; }

        public List<Item> Inventory { get; set; }
        public int Money { get; set; }
        public int CurrentHP { get; set; }
        public int MaxHP { get; set; }
        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Constitution { get; set; }
        public int Intelligence { get; set; }
        public int Wisdom { get; set; }
        public int Charisma { get; set; }

        public int TotalOwnedPokemon
        {
            get { return OwnedPokemon.Count; }
        }
        public string Description { get; set; }
        public string Notes { get; set; }
        private List<PlayerLevels> Levels { get; set; }
        private List<string> GymBadges { get; set; }
        private List<string> ContestRibbons { get; set; }
        private List<LocationRegion> Regions { get; set; }

        public int GetStatModifider(int value)
        {
            if (value > 10)
                return (value - 10) / 2;
            return value - 10;
        }
    }
}