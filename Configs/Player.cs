using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PokemonRPG.Configs
{
    [Serializable]
    public class Player : PlayerStub
    {
        [NonSerialized]
        public ObservableCollection<TrainerPokemon> Pkmnlist = new ObservableCollection<TrainerPokemon>();

        public Player()
        {
            ItemPC = new List<ItemBox>();
            Levels = new List<PlayerLevels>();
            GymBadges = new List<string>();
            CurrentParty = new List<PartyPokemon>();
            Inventory = new List<Item>();
            ContestRibbons = new List<string>();
            Regions = new List<LocationRegion>();
            OwnedPokemon = new List<TrainerPokemon>();
        }

        public Player(PlayerStub stub) : this()
        {
            UID = stub.UID;
            FirstName = stub.FirstName;
            LastName = stub.LastName;
            PlayerName = stub.PlayerName;
            Strength = stub.Strength;
            Dexterity = stub.Dexterity;
            Constitution = stub.Constitution;
            Intelligence = stub.Intelligence;
            Wisdom = stub.Wisdom;
            Charisma = stub.Charisma;
            Money = stub.Money;
            CurrentHP = stub.CurrentHP;
            MaxHP = stub.MaxHP;
            Description = stub.Description;
            Notes = stub.Notes;
        }

        public List<ItemBox> ItemPC { get; set; }
        public List<PartyPokemon> CurrentParty { get; set; }
        public List<TrainerPokemon> OwnedPokemon { get; set; }
        public List<Item> Inventory { get; set; }
        
        public int TotalOwnedPokemon
        {
            get { return OwnedPokemon.Count; }
        }
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

        public override string ToString()
        {
            return $"{FirstName}";
        }
    }

    public class PartyPokemon
    {
        public int Slot { get; set; }
        public int PokemonUID { get; set; }
    }
}