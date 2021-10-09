﻿using System;
using System.Collections.Generic;

namespace PokemonRPG.Configs
{
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

        public List<PokemonMove> NaturalMoves { get; set; }
        public WildPokemon()
        {
            Level = 1;
            Nature = new InherentNature();
            LevelUpPoints = new BaseCharacteristics();
            Enhancements = new BaseCharacteristics();
            RecalculateStats();
            NaturalMoves = new List<PokemonMove>();
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

        public void WildLevelUp(int statLocation = 0)
        {
            if (statLocation == 0)
            {
                statLocation = StaticData.ReferenceData.RandomGenerator.Next(1, 7);
            }

            if (statLocation == 1)
            {
                LevelUpPoints.Attack++;
            }
            else if (statLocation == 2)
            {
                LevelUpPoints.Defence++;
            }
            else if (statLocation == 4)
            {
                LevelUpPoints.SpecialAttack++;
            }
            else if (statLocation == 5)
            {
                LevelUpPoints.SpecialDefence++;
            }
            else if (statLocation == 6)
            {
                LevelUpPoints.Speed++;
            }
            else
            {
                LevelUpPoints.HP++;
            }

            RecalculateStats();
            CheckLevelMoves();
        }

        public void CheckLevelMoves(int MoveDeleteLocation = 0)
        {
            foreach (LevelMoves move in PossibleLevelupMoves)
            {
                var ActualMove = StaticData.ReferenceData.MoveDex.MoveList.Find(s => s.MoveID.Equals(move.MoveID));
                if (move.LevelLearned <= Level && !NaturalMoves.Contains(ActualMove))
                    NaturalMoves.Add(ActualMove);
                if (NaturalMoves.Count > 7)
                    NaturalMoves.RemoveAt(MoveDeleteLocation);
            }
        }
    }
}