﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonRPG.Configs
{
    [Serializable]
    public class Player
    {
        public string Name { get; set; }
        public List<ItemBox> ItemPC { get; set; }
        public List<PokemonBox> PokemonPC { get; set; }
        public List<TrainerPokemon> CurrentParty { get; set; }
        public List<Item> Inventory { get; set; }
        public int Money { get; set; }
        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Constitution { get; set; }
        public int Intelligence { get; set; }
        public int Wisdom { get; set; }
        public int Charisma { get; set; }
        public int TotalOwnedPokemon { get; set; }
        public string Description { get; set; }
        public string Notes { get; set; }
        List<PlayerLevels> Levels { get; set; }
        List<string> GymBadges { get; set; }
        List<string> ContestRibbons { get; set; }
        List<LocationRegion> Regions { get; set; }
        public int GetStatModifider (int value)
        {
            if (value > 10)
                return (value - 10) / 2;
            else
                return value - 10;
        }
        public Player()
        {
            ItemPC = new List<ItemBox>();
            PokemonPC = new List<PokemonBox>();
            Levels = new List<PlayerLevels>();
            GymBadges = new List<string>();
            CurrentParty = new List<TrainerPokemon>();
            Inventory = new List<Item>();
            ContestRibbons = new List<string>();
            Regions = new List<LocationRegion>();
        }
    }
    [Serializable]
    public class PlayerLevels
    {
        public int ClassUID { get; set; }
        public int LevelCount { get; set; }
    }
    [Serializable]
    public class LocationRegion
    {
        public string Name { get; set; }
        public bool DefeatedEliteFour { get; set; }
    }
    
    [Serializable]
    public class ItemBox
    {
        public string BoxName { get; set; }
        public int BoxNo { get; set; }
        public List<Item> BoxContents { get; set; }
        public List<MoveLearningItem> BoxedLearningItems { get; set; }
        public ItemBox()
        {
            BoxContents = new List<Item>();
            BoxedLearningItems = new List<MoveLearningItem>();
        }    
    }
    [Serializable]
    public class PokemonBox
    {
        public string BoxName { get; set; }
        public int BoxNo { get; set; }
        List<Pokemon> BoxContents { get; set; }
        public PokemonBox()
        {
            BoxContents = new List<Pokemon>();
        }
    }
}
