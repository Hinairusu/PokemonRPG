using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media.Imaging;

namespace PokemonRPG.Configs
{
    public class TrainerPokemon : Pokemon
    {
        public TrainerPokemon()
        {
            Level = 1;
            Nature = new InherentNature();
            LevelUpPoints = new BaseCharacteristics();
            Enhancements = new BaseCharacteristics();
            NaturalMoves = new List<PokemonBattleMove>();
            CombatStats = new BaseCharacteristics();
            IVs = new HiddenCharacteristics();
            EVs = new HiddenCharacteristics();
            ContestStats = new ContestCharacteristics();
            ArtificalMoves = new List<PokemonBattleMove>();
            ActualStats = new BaseCharacteristics();
            Alive = true;
            MaxHP = Level + ActualStats.HP * 3;
            CurrentHP = MaxHP;
            RecalculateStats();
            RecalculateCombatStats();
        }
        public int BasePokemonUID { get; set; }
        public int CurrentHP { get; set; }
        public int MaxHP { get; set; }
        public BaseCharacteristics CombatStats { get; set; }
        public BaseCharacteristics ActualStats { get; set; }
        public InherentNature Nature { get; set; }
        public BaseCharacteristics LevelUpPoints { get; set; }
        public BaseCharacteristics Enhancements { get; set; }
        public string ActualSex { get; set; }
        public int Level { get; set; }

        public List<PokemonBattleMove> NaturalMoves { get; set; }
        public HiddenCharacteristics IVs { get; set; }
        public HiddenCharacteristics EVs { get; set; }
        public ContestCharacteristics ContestStats { get; set; }
        public string Nickname { get; set; }
        public int Loyalty { get; set; }
        public int CurrentExperience { get; set; }
        public bool Alive { get; set; }
        public int TrainerID { get; set; }
        public int CaptureTrainerID { get; set; }
        public int BreederID { get; set; }
        public int PokemonTID { get; set; }
        public int Parent1UID { get; set; }
        public int Parent2UID { get; set; }
        public string Notes { get; set; }

        public List<PokemonBattleMove> ArtificalMoves { get; set; }


        
        public void RecalculateCombatStats()
        {
            if (Enhancements.HP > 0)
                CombatStats.HP = Convert.ToInt32(ActualStats.HP + ActualStats.HP * (Enhancements.HP * 0.25));
            else
                CombatStats.HP = Convert.ToInt32(ActualStats.HP - ActualStats.HP * (Enhancements.HP * -0.125));

            if (Enhancements.Attack > 0)
                CombatStats.Attack =
                    Convert.ToInt32(ActualStats.Attack + ActualStats.Attack * (Enhancements.Attack * 0.25));
            else
                CombatStats.Attack =
                    Convert.ToInt32(ActualStats.Attack - ActualStats.Attack * (Enhancements.Attack * -0.125));

            if (Enhancements.Defence > 0)
                CombatStats.Defence =
                    Convert.ToInt32(ActualStats.Defence + ActualStats.Defence * (Enhancements.Defence * 0.25));
            else
                CombatStats.Defence =
                    Convert.ToInt32(ActualStats.Defence - ActualStats.Defence * (Enhancements.Defence * -0.125));

            if (Enhancements.SpecialAttack > 0)
                CombatStats.SpecialAttack = Convert.ToInt32(ActualStats.SpecialAttack +
                                                            ActualStats.SpecialAttack *
                                                            (Enhancements.SpecialAttack * 0.25));
            else
                CombatStats.SpecialAttack = Convert.ToInt32(ActualStats.SpecialAttack -
                                                            ActualStats.SpecialAttack *
                                                            (Enhancements.SpecialAttack * -0.125));

            if (Enhancements.SpecialDefence > 0)
                CombatStats.SpecialDefence = Convert.ToInt32(ActualStats.SpecialDefence +
                                                             ActualStats.SpecialDefence *
                                                             (Enhancements.SpecialDefence * 0.25));
            else
                CombatStats.SpecialDefence = Convert.ToInt32(ActualStats.SpecialDefence -
                                                             ActualStats.SpecialDefence *
                                                             (Enhancements.SpecialDefence * -0.125));

            if (Enhancements.Speed > 0)
                CombatStats.Speed =
                    Convert.ToInt32(ActualStats.Speed + ActualStats.Speed * (Enhancements.Speed * 0.25));
            else
                CombatStats.Speed =
                    Convert.ToInt32(ActualStats.Speed - ActualStats.Speed * (Enhancements.Speed * -0.125));
        }
        
        private void RecalculateStats()
        {
            ActualStats = new BaseCharacteristics
            {
                HP = BaseStats.HP + LevelUpPoints.HP + IVs.HP + EVs.HP / 4,
                Attack = BaseStats.Attack + LevelUpPoints.Attack + IVs.Attack + EVs.Attack / 4,
                Defence = BaseStats.Defence + LevelUpPoints.Defence + IVs.Defence + EVs.Defence / 4,
                Speed = BaseStats.Speed + LevelUpPoints.Speed + IVs.Speed + EVs.Speed / 4,
                SpecialDefence = BaseStats.SpecialDefence + LevelUpPoints.SpecialDefence + IVs.SpecialDefence +
                                 EVs.SpecialDefence / 4,
                SpecialAttack = BaseStats.SpecialAttack + LevelUpPoints.SpecialAttack + IVs.SpecialAttack +
                                EVs.SpecialAttack / 4
            };
            MaxHP = Level + ActualStats.HP * 3;
        }

        public override string ToString()
        {
            return $"{Nickname} ({Name}) - Level {Level}";
        }

        public void Advance()
        {
            foreach (var advance in LevelUps)
            {
                Advance(advance);
            }
            RecalculateStats();
            RecalculateCombatStats();
            
        }

        public void Advance(Advancements advance)
        {
             switch (advance.LevelUpUID)
                {
                    case 1:
                    {
                        Level += advance.ValueAdd ? advance.Modifier : -advance.Modifier;
                        break;
                    }
                    case 2:
                    {
                        LevelUpPoints.HP += advance.ValueAdd ?  advance.Modifier : -advance.Modifier;
                        break;
                    }
                    case 3:
                    {
                        LevelUpPoints.Attack += advance.ValueAdd ? advance.Modifier : -advance.Modifier;
                        break;
                    }
                    case 4:
                    {
                        LevelUpPoints.Defence += advance.ValueAdd ? advance.Modifier : -advance.Modifier;
                        break;
                    }
                    case 5:
                    {
                        LevelUpPoints.SpecialAttack += advance.ValueAdd ? advance.Modifier : -advance.Modifier;
                        break;
                    }
                    case 6:
                    {
                        LevelUpPoints.SpecialDefence += advance.ValueAdd ? advance.Modifier : -advance.Modifier;
                        break;
                    }
                    case 7:
                    {
                        LevelUpPoints.Speed += advance.ValueAdd ? advance.Modifier : -advance.Modifier;
                        break;
                    }
                    case 8:
                    {
                        PrimaryTypeID = advance.Modifier;
                        break;
                    }
                    case 9:
                    {
                        SecondaryTypeID = advance.Modifier;
                        break;
                    }
                    case 10:
                    {
                        TertiaryTypeID = advance.Modifier;
                        break;
                    }
                    case 11:
                    {
                        IVs.Attack += advance.ValueAdd ? advance.Modifier : -advance.Modifier;
                        break;
                    }
                    case 12:
                    {
                        IVs.Defence += advance.ValueAdd ? advance.Modifier : -advance.Modifier;
                        break;
                    }
                    case 13:
                    {
                        IVs.SpecialAttack += advance.ValueAdd ? advance.Modifier : -advance.Modifier;
                        break;
                    }
                    case 14:
                    {
                        IVs.SpecialDefence += advance.ValueAdd ? advance.Modifier : -advance.Modifier;
                        break;
                    }
                    case 15:
                    {
                        IVs.Speed += advance.ValueAdd ? advance.Modifier : -advance.Modifier;
                        break;
                    }
                    case 16:
                    {
                        IVs.HP += advance.ValueAdd ? advance.Modifier : -advance.Modifier;
                        break;
                    }
                    case 17:
                    {
                        EVs.HP += advance.ValueAdd ? advance.Modifier : -advance.Modifier;
                        break;
                    }
                    case 18:
                    {
                        EVs.Attack += advance.ValueAdd ? advance.Modifier : -advance.Modifier;
                        break;
                    }
                    case 19:
                    {
                        EVs.Defence += advance.ValueAdd ? advance.Modifier : -advance.Modifier;
                        break;
                    }
                    case 20:
                    {
                        EVs.SpecialAttack += advance.ValueAdd ? advance.Modifier : -advance.Modifier;
                        break;
                    }
                    case 21:
                    {
                        EVs.SpecialDefence += advance.ValueAdd ? advance.Modifier : -advance.Modifier;
                        break;
                    }
                    case 22:
                    {
                        EVs.Speed += advance.ValueAdd ? advance.Modifier : -advance.Modifier;
                        break;
                    }
                    case 23:
                    {
                        Loyalty += advance.ValueAdd ? advance.Modifier : -advance.Modifier;
                        break;
                    }
                    case 24:
                    {
                        int Slot = 0;
                        try
                        {
                            var move = NaturalMoves.Single(s => s.Slot.Equals(Slot));
                            if (move != null)
                                NaturalMoves.Remove(move);
                        }
                        catch{}
                        PokemonBattleMove newMove = new PokemonBattleMove();
                        newMove.Slot = Slot;
                        newMove.Move =
                            StaticData.ReferenceData.MoveDex.MoveList.Single(m => m.MoveID.Equals(advance.Modifier));
                            NaturalMoves.Add(newMove);
                        break;
                    }
                    case 25:
                    {
                        int Slot = 1;
                        try
                        {
                            var move = NaturalMoves.Single(s => s.Slot.Equals(Slot));
                            if (move != null)
                                NaturalMoves.Remove(move);
                        }
                        catch{}
                        PokemonBattleMove newMove = new PokemonBattleMove();
                        newMove.Slot = Slot;
                        newMove.Move =
                            StaticData.ReferenceData.MoveDex.MoveList.Single(m => m.MoveID.Equals(advance.Modifier));
                        NaturalMoves.Add(newMove);
                        break;
                    }
                    case 26:
                    {
                        int Slot = 2;
                        try
                        {
                            var move = NaturalMoves.Single(s => s.Slot.Equals(Slot));
                            if (move != null)
                                NaturalMoves.Remove(move);
                        }
                        catch{}
                        PokemonBattleMove newMove = new PokemonBattleMove();
                        newMove.Slot = Slot;
                        newMove.Move =
                            StaticData.ReferenceData.MoveDex.MoveList.Single(m => m.MoveID.Equals(advance.Modifier));
                        NaturalMoves.Add(newMove);
                        break;
                    }
                    case 27:
                    {
                        int Slot = 3;
                        try
                        {
                            var move = NaturalMoves.Single(s => s.Slot.Equals(Slot));
                            if (move != null)
                                NaturalMoves.Remove(move);
                        }
                        catch{}
                        PokemonBattleMove newMove = new PokemonBattleMove();
                        newMove.Slot = Slot;
                        newMove.Move =
                            StaticData.ReferenceData.MoveDex.MoveList.Single(m => m.MoveID.Equals(advance.Modifier));
                        NaturalMoves.Add(newMove);
                        break;
                    }
                    case 28:
                    {
                        int Slot = 4;
                        try
                        {
                            var move = NaturalMoves.Single(s => s.Slot.Equals(Slot));
                            if (move != null)
                                NaturalMoves.Remove(move);
                        }
                        catch{}
                        PokemonBattleMove newMove = new PokemonBattleMove();
                        newMove.Slot = Slot;
                        newMove.Move =
                            StaticData.ReferenceData.MoveDex.MoveList.Single(m => m.MoveID.Equals(advance.Modifier));
                        NaturalMoves.Add(newMove);
                        break;
                    }
                    case 29:
                    {
                        int Slot = 5;
                        try
                        {
                            var move = NaturalMoves.Single(s => s.Slot.Equals(Slot));
                            if (move != null)
                                NaturalMoves.Remove(move);
                        }
                        catch{}
                        PokemonBattleMove newMove = new PokemonBattleMove();
                        newMove.Slot = Slot;
                        newMove.Move =
                            StaticData.ReferenceData.MoveDex.MoveList.Single(m => m.MoveID.Equals(advance.Modifier));
                        NaturalMoves.Add(newMove);
                        break;
                    }
                    case 30:
                    {
                        int Slot = 6;
                        try
                        {
                            var move = NaturalMoves.Single(s => s.Slot.Equals(Slot));
                            if (move != null)
                                NaturalMoves.Remove(move);
                        }
                        catch{}
                        PokemonBattleMove newMove = new PokemonBattleMove();
                        newMove.Slot = Slot;
                        newMove.Move =
                            StaticData.ReferenceData.MoveDex.MoveList.Single(m => m.MoveID.Equals(advance.Modifier));
                        NaturalMoves.Add(newMove);
                        break;
                    }
                    case 31:
                    {
                        int Slot = 0;
                        try
                        {
                            var move = ArtificalMoves.Single(s => s.Slot.Equals(Slot));
                            if (move != null)
                                ArtificalMoves.Remove(move);
                        }
                        catch{}
                        PokemonBattleMove newMove = new PokemonBattleMove();
                        newMove.Slot = Slot;
                        newMove.Move =
                            StaticData.ReferenceData.MoveDex.MoveList.Single(m => m.MoveID.Equals(advance.Modifier));
                        ArtificalMoves.Add(newMove);
                        break;
                    }
                    case 32:
                    {
                        int Slot = 1;
                        try
                        {
                            var move = ArtificalMoves.Single(s => s.Slot.Equals(Slot));
                            if (move != null)
                                ArtificalMoves.Remove(move);
                        }
                        catch{}
                        PokemonBattleMove newMove = new PokemonBattleMove();
                        newMove.Slot = Slot;
                        newMove.Move =
                            StaticData.ReferenceData.MoveDex.MoveList.Single(m => m.MoveID.Equals(advance.Modifier));
                        ArtificalMoves.Add(newMove);
                        break;
                    }
                    case 33:
                    {
                        int Slot = 2;
                        try
                        {
                            var move = ArtificalMoves.Single(s => s.Slot.Equals(Slot));
                            if (move != null)
                                ArtificalMoves.Remove(move);
                        }
                        catch{}
                        PokemonBattleMove newMove = new PokemonBattleMove();
                        newMove.Slot = Slot;
                        newMove.Move =
                            StaticData.ReferenceData.MoveDex.MoveList.Single(m => m.MoveID.Equals(advance.Modifier));
                        ArtificalMoves.Add(newMove);
                        break;
                    }
                    case 34:
                    {
                        int Slot = 3;
                        try
                        {
                            var move = ArtificalMoves.Single(s => s.Slot.Equals(Slot));
                            if (move != null)
                                ArtificalMoves.Remove(move);
                        }
                        catch{}
                        PokemonBattleMove newMove = new PokemonBattleMove();
                        newMove.Slot = Slot;
                        newMove.Move =
                            StaticData.ReferenceData.MoveDex.MoveList.Single(m => m.MoveID.Equals(advance.Modifier));
                        ArtificalMoves.Add(newMove);
                        break;
                    }
                    case 35:
                    {
                        int Slot = 4;
                        try
                        {
                            var move = ArtificalMoves.Single(s => s.Slot.Equals(Slot));
                            if (move != null)
                                ArtificalMoves.Remove(move);
                        }
                        catch{}
                        PokemonBattleMove newMove = new PokemonBattleMove();
                        newMove.Slot = Slot;
                        newMove.Move =
                            StaticData.ReferenceData.MoveDex.MoveList.Single(m => m.MoveID.Equals(advance.Modifier));
                        ArtificalMoves.Add(newMove);
                        break;
                    }
                    case 36:
                    {
                        int Slot = 5;
                        try
                        {
                            var move = ArtificalMoves.Single(s => s.Slot.Equals(Slot));
                            if (move != null)
                                ArtificalMoves.Remove(move);
                        }
                        catch{}
                        PokemonBattleMove newMove = new PokemonBattleMove();
                        newMove.Slot = Slot;
                        newMove.Move =
                            StaticData.ReferenceData.MoveDex.MoveList.Single(m => m.MoveID.Equals(advance.Modifier));
                        ArtificalMoves.Add(newMove);
                        break;
                    }
                    case 37:
                    {
                        int Slot = 6;
                        try
                        {
                            var move = ArtificalMoves.Single(s => s.Slot.Equals(Slot));
                            if (move != null)
                                ArtificalMoves.Remove(move);
                        }
                        catch{}
                        PokemonBattleMove newMove = new PokemonBattleMove();
                        newMove.Slot = Slot;
                        newMove.Move =
                            StaticData.ReferenceData.MoveDex.MoveList.Single(m => m.MoveID.Equals(advance.Modifier));
                        ArtificalMoves.Add(newMove);
                        break;
                    }
                     case 38:
                    {
                        int Slot = 0;
                        try
                        {
                            var move = NaturalMoves.Single(s => s.Slot.Equals(Slot));
                            if (move != null)
                                NaturalMoves.Remove(move);
                        }
                        catch{}
                        PokemonBattleMove newMove = new PokemonBattleMove();
                        newMove.Slot = Slot;
                        newMove.Move =
                            StaticData.ReferenceData.MoveDex.MoveList.Single(m => m.MoveID.Equals(advance.Modifier));
                            NaturalMoves.Add(newMove);
                        break;
                    }
                    case 39:
                    {
                        int Slot = 1;
                        try
                        {
                            var move = NaturalMoves.Single(s => s.Slot.Equals(Slot));
                            if (move != null)
                                NaturalMoves.Remove(move);
                        }
                        catch{}
                        PokemonBattleMove newMove = new PokemonBattleMove();
                        newMove.Slot = Slot;
                        newMove.Move =
                            StaticData.ReferenceData.MoveDex.MoveList.Single(m => m.MoveID.Equals(advance.Modifier));
                        NaturalMoves.Add(newMove);
                        break;
                    }
                    case 40:
                    {
                        int Slot = 2;
                        try
                        {
                            var move = NaturalMoves.Single(s => s.Slot.Equals(Slot));
                            if (move != null)
                                NaturalMoves.Remove(move);
                        }
                        catch{}
                        PokemonBattleMove newMove = new PokemonBattleMove();
                        newMove.Slot = Slot;
                        newMove.Move =
                            StaticData.ReferenceData.MoveDex.MoveList.Single(m => m.MoveID.Equals(advance.Modifier));
                        NaturalMoves.Add(newMove);
                        break;
                    }
                    case 41:
                    {
                        int Slot = 3;
                        try
                        {
                            var move = NaturalMoves.Single(s => s.Slot.Equals(Slot));
                            if (move != null)
                                NaturalMoves.Remove(move);
                        }
                        catch{}
                        PokemonBattleMove newMove = new PokemonBattleMove();
                        newMove.Slot = Slot;
                        newMove.Move =
                            StaticData.ReferenceData.MoveDex.MoveList.Single(m => m.MoveID.Equals(advance.Modifier));
                        NaturalMoves.Add(newMove);
                        break;
                    }
                    case 42:
                    {
                        int Slot = 4;
                        try
                        {
                            var move = NaturalMoves.Single(s => s.Slot.Equals(Slot));
                            if (move != null)
                                NaturalMoves.Remove(move);
                        }
                        catch{}
                        PokemonBattleMove newMove = new PokemonBattleMove();
                        newMove.Slot = Slot;
                        newMove.Move =
                            StaticData.ReferenceData.MoveDex.MoveList.Single(m => m.MoveID.Equals(advance.Modifier));
                        NaturalMoves.Add(newMove);
                        break;
                    }
                    case 43:
                    {
                        int Slot = 5;
                        try
                        {
                            var move = NaturalMoves.Single(s => s.Slot.Equals(Slot));
                            if (move != null)
                                NaturalMoves.Remove(move);
                        }
                        catch{}
                        PokemonBattleMove newMove = new PokemonBattleMove();
                        newMove.Slot = Slot;
                        newMove.Move =
                            StaticData.ReferenceData.MoveDex.MoveList.Single(m => m.MoveID.Equals(advance.Modifier));
                        NaturalMoves.Add(newMove);
                        break;
                    }
                    case 44:
                    {
                        int Slot = 6;
                        try
                        {
                            var move = NaturalMoves.Single(s => s.Slot.Equals(Slot));
                            if (move != null)
                                NaturalMoves.Remove(move);
                        }
                        catch{}
                        PokemonBattleMove newMove = new PokemonBattleMove();
                        newMove.Slot = Slot;
                        newMove.Move =
                            StaticData.ReferenceData.MoveDex.MoveList.Single(m => m.MoveID.Equals(advance.Modifier));
                        NaturalMoves.Add(newMove);
                        break;
                    }
                    case 45:
                    {
                        int Slot = 0;
                        try
                        {
                            var move = ArtificalMoves.Single(s => s.Slot.Equals(Slot));
                            if (move != null)
                                ArtificalMoves.Remove(move);
                        }
                        catch{}
                        PokemonBattleMove newMove = new PokemonBattleMove();
                        newMove.Slot = Slot;
                        newMove.Move =
                            StaticData.ReferenceData.MoveDex.MoveList.Single(m => m.MoveID.Equals(advance.Modifier));
                        ArtificalMoves.Add(newMove);
                        break;
                    }
                    case 46:
                    {
                        int Slot = 1;
                        try
                        {
                            var move = ArtificalMoves.Single(s => s.Slot.Equals(Slot));
                            if (move != null)
                                ArtificalMoves.Remove(move);
                        }
                        catch{}
                        PokemonBattleMove newMove = new PokemonBattleMove();
                        newMove.Slot = Slot;
                        newMove.Move =
                            StaticData.ReferenceData.MoveDex.MoveList.Single(m => m.MoveID.Equals(advance.Modifier));
                        ArtificalMoves.Add(newMove);
                        break;
                    }
                    case 47:
                    {
                        int Slot = 2;
                        try
                        {
                            var move = ArtificalMoves.Single(s => s.Slot.Equals(Slot));
                            if (move != null)
                                ArtificalMoves.Remove(move);
                        }
                        catch{}
                        PokemonBattleMove newMove = new PokemonBattleMove();
                        newMove.Slot = Slot;
                        newMove.Move =
                            StaticData.ReferenceData.MoveDex.MoveList.Single(m => m.MoveID.Equals(advance.Modifier));
                        ArtificalMoves.Add(newMove);
                        break;
                    }
                    case 48:
                    {
                        int Slot = 3;
                        try
                        {
                            var move = ArtificalMoves.Single(s => s.Slot.Equals(Slot));
                            if (move != null)
                                ArtificalMoves.Remove(move);
                        }
                        catch{}
                        PokemonBattleMove newMove = new PokemonBattleMove();
                        newMove.Slot = Slot;
                        newMove.Move =
                            StaticData.ReferenceData.MoveDex.MoveList.Single(m => m.MoveID.Equals(advance.Modifier));
                        ArtificalMoves.Add(newMove);
                        break;
                    }
                    case 49:
                    {
                        int Slot = 4;
                        try
                        {
                            var move = ArtificalMoves.Single(s => s.Slot.Equals(Slot));
                            if (move != null)
                                ArtificalMoves.Remove(move);
                        }
                        catch{}
                        PokemonBattleMove newMove = new PokemonBattleMove();
                        newMove.Slot = Slot;
                        newMove.Move =
                            StaticData.ReferenceData.MoveDex.MoveList.Single(m => m.MoveID.Equals(advance.Modifier));
                        ArtificalMoves.Add(newMove);
                        break;
                    }
                    case 50:
                    {
                        int Slot = 5;
                        try
                        {
                            var move = ArtificalMoves.Single(s => s.Slot.Equals(Slot));
                            if (move != null)
                                ArtificalMoves.Remove(move);
                        }
                        catch{}
                        PokemonBattleMove newMove = new PokemonBattleMove();
                        newMove.Slot = Slot;
                        newMove.Move =
                            StaticData.ReferenceData.MoveDex.MoveList.Single(m => m.MoveID.Equals(advance.Modifier));
                        ArtificalMoves.Add(newMove);
                        break;
                    }
                    case 51:
                    {
                        int Slot = 6;
                        try
                        {
                            var move = ArtificalMoves.Single(s => s.Slot.Equals(Slot));
                            if (move != null)
                                ArtificalMoves.Remove(move);
                        }
                        catch{}
                        PokemonBattleMove newMove = new PokemonBattleMove();
                        newMove.Slot = Slot;
                        newMove.Move =
                            StaticData.ReferenceData.MoveDex.MoveList.Single(m => m.MoveID.Equals(advance.Modifier));
                        ArtificalMoves.Add(newMove);
                        break;
                    }
                    case 52:
                    {
                        Nickname = advance.Notes;
                        break;
                    }
                    case 53:
                    {
                        try
                        {
                            if (advance.ValueAdd)
                                EggGroup.Add(advance.Modifier);
                            else
                                EggGroup.Remove(advance.Modifier);
                        }
                        catch{}
                        break;
                    }
                    case 54:
                    {
                        try
                        {
                            if (advance.ValueAdd)
                                Capability.Add(advance.Modifier);
                            else
                                Capability.Remove(advance.Modifier);
                        }
                        catch{}
                        break;
                    }
                    case 55:
                    {
                        Nature = StaticData.ReferenceData.NatureDex.Natures.Single(s => s.UID.Equals(advance.Modifier));
                        break;
                    }
                    case 56:
                    {
                        Power += advance.ValueAdd ? advance.Modifier : -advance.Modifier;
                        break;
                    }
                    case 57:
                    {
                        Intellegence += advance.ValueAdd ? advance.Modifier : -advance.Modifier;
                        break;
                    }
                    case 58:
                    {
                        WeightClass += advance.ValueAdd ? advance.Modifier : -advance.Modifier;
                        break;
                    }
                    case 59:
                    {
                        Movements.Overland += advance.ValueAdd ? advance.Modifier : -advance.Modifier;
                        break;
                    }
                    case 60:
                    {
                        Movements.Sky += advance.ValueAdd ? advance.Modifier : -advance.Modifier;
                        break;
                    }
                    case 61:
                    {
                        Movements.Surface += advance.ValueAdd ? advance.Modifier : -advance.Modifier;
                        break;
                    }
                    case 62:
                    {
                        Movements.Jump += advance.ValueAdd ? advance.Modifier : -advance.Modifier;
                        break;
                    }
                    case 63:
                    {
                        Movements.Burrow += advance.ValueAdd ? advance.Modifier : -advance.Modifier;
                        break;
                    }
                    case 64:
                    {
                        Movements.Underwater += advance.ValueAdd ? advance.Modifier : -advance.Modifier;
                        break;
                    }
                    case 65:
                    {
                        BasePokemonUID = advance.Modifier;
                        BaseStats = StaticData.ReferenceData.Pokedex.PokemonDexList
                            .Single(p => p.UID.Equals(BasePokemonUID)).BaseStats;
                        break;
                    }
                    case 66:
                    {
                        ExperinceValue += advance.ValueAdd ? advance.Modifier : -advance.Modifier;
                        break;
                    }
                    case 67:
                    {
                        TrainerID = StaticData.PlayerData.UID;
                        break;
                    }
                    case 68:
                    {
                        LevelUpPoints.HP += advance.ValueAdd ?  advance.Modifier : -advance.Modifier;
                        break;
                    }
                    case 69:
                    {
                        LevelUpPoints.Attack += advance.ValueAdd ? advance.Modifier : -advance.Modifier;
                        break;
                    }
                    case 70:
                    {
                        LevelUpPoints.SpecialAttack += advance.ValueAdd ? advance.Modifier : -advance.Modifier;
                        break;
                    }
                    case 71:
                    {
                        LevelUpPoints.Defence  += advance.ValueAdd ? advance.Modifier : -advance.Modifier;
                        break;
                    }
                    case 72:
                    {
                        LevelUpPoints.SpecialDefence += advance.ValueAdd ? advance.Modifier : -advance.Modifier;
                        break;
                    }
                    case 73:
                    {
                        LevelUpPoints.Speed += advance.ValueAdd ? advance.Modifier : -advance.Modifier;
                        break;
                    }
                    default:
                        break;
                }
        }
    }
}