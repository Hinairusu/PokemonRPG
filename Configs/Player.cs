using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace PokemonRPG.Configs
{
    [Serializable]
    public class Player : PlayerStub
    {

        public Player()
        {
            ItemPC = new ObservableCollection<ItemBox>();
            Levels = new ObservableCollection<PlayerLevels>();
            GymBadges = new ObservableCollection<string>();
            CurrentParty = new ObservableCollection<PartyPokemon>();
            Inventory = new ObservableCollection<Item>();
            ContestRibbons = new ObservableCollection<string>();
            Regions = new ObservableCollection<LocationRegion>();
            OwnedPokemon = new ObservableCollection<TrainerPokemon>();
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

        public ObservableCollection<ItemBox> ItemPC { get; set; }
        public ObservableCollection<PartyPokemon> CurrentParty { get; set; }
        public ObservableCollection<TrainerPokemon> OwnedPokemon { get; set; }
        public ObservableCollection<Item> Inventory { get; set; }
        
        public int TotalOwnedPokemon
        {
            get { return OwnedPokemon.Count; }
        }
        private ObservableCollection<PlayerLevels> Levels { get; set; }
        private ObservableCollection<string> GymBadges { get; set; }
        private ObservableCollection<string> ContestRibbons { get; set; }
        private ObservableCollection<LocationRegion> Regions { get; set; }

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

        public override string ToString()
        {
            var pkmn = StaticData.PlayerData.OwnedPokemon.Single(s => s.UID.Equals(PokemonUID));
            

            return $"{pkmn.Nickname} ({pkmn.Name}) {Environment.NewLine} - Level {pkmn.Level} {Environment.NewLine} HP: {pkmn.CurrentHP} / {pkmn.MaxHP}";
        }
    }
}