using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using PokemonRPG.Configs;

namespace PokemonRPG.Windows
{
    /// <summary>
    ///     Interaction logic for PokemonPage.xaml
    /// </summary>
    public partial class PokemonPage : Window
    {
        private readonly TrainerPokemon Pkmn;

        public PokemonPage(int pkmn)
        {
            InitializeComponent();
            Pkmn = StaticData.PlayerData.OwnedPokemon.Single(s => s.UID.Equals(pkmn));

            this.Title = $"{Pkmn.Nickname} ({Pkmn.Name}) - Level {Pkmn.Level}";
            lbl_Level.Content = Pkmn.Level.ToString();
            lbl_NickName.Content = Pkmn.Nickname;
            lbl_Nature.Content = Pkmn.Nature.Name;
            lbl_Species.Content = Pkmn.Name;
            lbl_Gender.Content = Pkmn.ActualSex;
            CurrentHP.Content = Pkmn.CurrentHP.ToString();
            MaxHP.Content = Pkmn.MaxHP.ToString();
            SetNaturalMoves();
            SetArtificalMoves();
            SetStats();
        }


        public void SetNaturalMoves()
        {
            try
            {
                PokemonMove Move = Pkmn.NaturalMoves.Single(s => s.Slot.Equals(0)).Move;
                
                NaturalMoveOne.Header = Move.Name;
                PanelNaturalMoveOne.Children.Add(MakeLabel(Move.Frequency));
                PanelNaturalMoveOne.Children.Add(MakeLabel(Move.Range.ToString()));
                PanelNaturalMoveOne.Children.Add(MakeLabel(Move.Damage.ToString()));
                NaturalMoveOne.Visibility = Visibility.Visible;
               
            }
            catch
            {
                NaturalMoveOne.Visibility = Visibility.Hidden;
            }

            try
            {
                PokemonMove Move = Pkmn.NaturalMoves.Single(s => s.Slot.Equals(1)).Move;
                    NaturalMoveTwo.Header = Move.Name;
                    PanelNaturalMoveTwo.Children.Add(MakeLabel(Move.Frequency));
                    PanelNaturalMoveTwo.Children.Add(MakeLabel(Move.Range.ToString()));
                    PanelNaturalMoveTwo.Children.Add(MakeLabel(Move.Damage.ToString()));
                    NaturalMoveTwo.Visibility = Visibility.Visible;
                
            }
            catch
            {
                NaturalMoveTwo.Visibility = Visibility.Hidden;
            }

            try
            {
                PokemonMove Move = Pkmn.NaturalMoves.Single(s => s.Slot.Equals(2)).Move;
                    NaturalMoveThree.Header = Move.Name;
                    PanelNaturalMoveThree.Children.Add(MakeLabel(Move.Frequency));
                    PanelNaturalMoveThree.Children.Add(MakeLabel(Move.Range.ToString()));
                    PanelNaturalMoveThree.Children.Add(MakeLabel(Move.Damage.ToString()));
                    NaturalMoveThree.Visibility = Visibility.Visible;
                
            }
            catch
            {
                NaturalMoveThree.Visibility = Visibility.Hidden;
            }

            try
            {
                PokemonMove Move = Pkmn.NaturalMoves.Single(s => s.Slot.Equals(3)).Move;
                    NaturalMoveFour.Header = Move.Name;
                    PanelNaturalMoveFour.Children.Add(MakeLabel(Move.Frequency));
                    PanelNaturalMoveFour.Children.Add(MakeLabel(Move.Range.ToString()));
                    PanelNaturalMoveFour.Children.Add(MakeLabel(Move.Damage.ToString()));
                    NaturalMoveFour.Visibility = Visibility.Visible;
                
            }
            catch
            {
                NaturalMoveFour.Visibility = Visibility.Hidden;
            }

            try
            {
                PokemonMove Move = Pkmn.NaturalMoves.Single(s => s.Slot.Equals(4)).Move;
                    NaturalMoveFive.Header = Move.Name;
                    PanelNaturalMoveFive.Children.Add(MakeLabel(Move.Frequency));
                    PanelNaturalMoveFive.Children.Add(MakeLabel(Move.Range.ToString()));
                    PanelNaturalMoveFive.Children.Add(MakeLabel(Move.Damage.ToString()));
                    NaturalMoveFive.Visibility = Visibility.Visible;
                
            }
            catch
            {
                NaturalMoveFive.Visibility = Visibility.Hidden;
            }

            try
            {
                PokemonMove Move = Pkmn.NaturalMoves.Single(s => s.Slot.Equals(5)).Move;
                    NaturalMoveSix.Header = Move.Name;
                    PanelNaturalMoveSix.Children.Add(MakeLabel(Move.Frequency));
                    PanelNaturalMoveSix.Children.Add(MakeLabel(Move.Range.ToString()));
                    PanelNaturalMoveSix.Children.Add(MakeLabel(Move.Damage.ToString()));
                    NaturalMoveSix.Visibility = Visibility.Visible;
               
            }
            catch
            {
                NaturalMoveSix.Visibility = Visibility.Hidden;
            }

            try
            {
                PokemonMove Move = Pkmn.NaturalMoves.Single(s => s.Slot.Equals(6)).Move;
                    NaturalMoveSeven.Header = Move.Name;
                    PanelNaturalMoveSeven.Children.Add(MakeLabel(Move.Frequency));
                    PanelNaturalMoveSeven.Children.Add(MakeLabel(Move.Range.ToString()));
                    PanelNaturalMoveSeven.Children.Add(MakeLabel(Move.Damage.ToString()));
                    NaturalMoveSeven.Visibility = Visibility.Visible;
                
            }
            catch
            {
                NaturalMoveSeven.Visibility = Visibility.Hidden;
            }
        }

        public void SetArtificalMoves()
        {
            try
            {
                PokemonMove Move = Pkmn.ArtificalMoves.Single(s => s.Slot.Equals(0)).Move;
                    TutorMoveOne.Header = Move.Name;
                    PanelTutorMoveOne.Children.Add(MakeLabel(Move.Frequency));
                    PanelTutorMoveOne.Children.Add(MakeLabel(Move.Range.ToString()));
                    PanelTutorMoveOne.Children.Add(MakeLabel(Move.Damage.ToString()));
                    TutorMoveOne.Visibility = Visibility.Visible;
                
            }
            catch
            {
                TutorMoveOne.Visibility = Visibility.Hidden;
            }

            try
            {
                PokemonMove Move = Pkmn.ArtificalMoves.Single(s => s.Slot.Equals(1)).Move;
                    TutorMoveTwo.Header = Move.Name;
                    PanelTutorMoveTwo.Children.Add(MakeLabel(Move.Frequency));
                    PanelTutorMoveTwo.Children.Add(MakeLabel(Move.Range.ToString()));
                    PanelTutorMoveTwo.Children.Add(MakeLabel(Move.Damage.ToString()));
                    TutorMoveTwo.Visibility = Visibility.Visible;
               
            }
            catch
            {
                TutorMoveTwo.Visibility = Visibility.Hidden;
            }

            try
            {
                PokemonMove Move = Pkmn.ArtificalMoves.Single(s => s.Slot.Equals(2)).Move;
                    TutorMoveThree.Header = Move.Name;
                    PanelTutorMoveThree.Children.Add(MakeLabel(Move.Frequency));
                    PanelTutorMoveThree.Children.Add(MakeLabel(Move.Range.ToString()));
                    PanelTutorMoveThree.Children.Add(MakeLabel(Move.Damage.ToString()));
                    TutorMoveThree.Visibility = Visibility.Visible;
                
            }
            catch
            {
                TutorMoveThree.Visibility = Visibility.Hidden;
            }

            try
            {
                PokemonMove Move = Pkmn.ArtificalMoves.Single(s => s.Slot.Equals(3)).Move;
                    TutorMoveFour.Header = Move.Name;
                    PanelTutorMoveFour.Children.Add(MakeLabel(Move.Frequency));
                    PanelTutorMoveFour.Children.Add(MakeLabel(Move.Range.ToString()));
                    PanelTutorMoveFour.Children.Add(MakeLabel(Move.Damage.ToString()));
                    TutorMoveFour.Visibility = Visibility.Visible;
               
            }
            catch
            {
                TutorMoveFour.Visibility = Visibility.Hidden;
            }

            try
            {
                PokemonMove Move = Pkmn.ArtificalMoves.Single(s => s.Slot.Equals(4)).Move;
                    TutorMoveFive.Header = Move.Name;
                    PanelTutorMoveFive.Children.Add(MakeLabel(Move.Frequency));
                    PanelTutorMoveFive.Children.Add(MakeLabel(Move.Range.ToString()));
                    PanelTutorMoveFive.Children.Add(MakeLabel(Move.Damage.ToString()));
                    TutorMoveFive.Visibility = Visibility.Visible;
                
            }
            catch
            {
                TutorMoveFive.Visibility = Visibility.Hidden;
            }

            try
            {
                PokemonMove Move = Pkmn.ArtificalMoves.Single(s => s.Slot.Equals(5)).Move;
                TutorMoveSix.Header = Move.Name;
                    PanelTutorMoveSix.Children.Add(MakeLabel(Move.Frequency));
                    PanelTutorMoveSix.Children.Add(MakeLabel(Move.Range.ToString()));
                    PanelTutorMoveSix.Children.Add(MakeLabel(Move.Damage.ToString()));
                    TutorMoveSix.Visibility = Visibility.Visible;
               
            }
            catch
            {
                TutorMoveSix.Visibility = Visibility.Hidden;
            }

            try
            {
                PokemonMove Move = Pkmn.ArtificalMoves.Single(s => s.Slot.Equals(6)).Move;
                    TutorMoveSeven.Header = Move.Name;
                    PanelTutorMoveSeven.Children.Add(MakeLabel(Move.Frequency));
                    PanelTutorMoveSeven.Children.Add(MakeLabel(Move.Range.ToString()));
                    PanelTutorMoveSeven.Children.Add(MakeLabel(Move.Damage.ToString()));
                    TutorMoveSeven.Visibility = Visibility.Visible;
               
            }
            catch
            {
                TutorMoveSeven.Visibility = Visibility.Hidden;
            }
        }

        public void SetStats()
        {
            Lbl_HP.Content = Pkmn.CombatStats.HP;
            Lbl_Atk.Content = Pkmn.CombatStats.Attack;
            Lbl_Def.Content = Pkmn.CombatStats.Defence;
            Lbl_SpAtk.Content = Pkmn.CombatStats.SpecialAttack;
            Lbl_SpDef.Content = Pkmn.CombatStats.SpecialDefence;
            Lbl_Spd.Content = Pkmn.CombatStats.Speed;

            lbl_HPBuff.Content = Pkmn.Enhancements.HP;
            lbl_AtkBuff.Content = Pkmn.Enhancements.Attack;
            lbl_DefBuff.Content = Pkmn.Enhancements.Defence;
            lbl_SpAtkBuff.Content = Pkmn.Enhancements.SpecialAttack;
            lbl_SpDefBuff.Content = Pkmn.Enhancements.SpecialDefence;
            lbl_SpdBuff.Content = Pkmn.Enhancements.Speed;
        }

        public Label MakeLabel(string Content)
        {
            var lbl = new Label();
            lbl.Content = Content;
            return lbl;
        }

        private void ModStat(int Stat, int Increment)
        {
            if (Stat == 1)
                Pkmn.Enhancements.HP += Increment;
            else if (Stat == 2)
                Pkmn.Enhancements.Attack += Increment;
            else if (Stat == 3)
                Pkmn.Enhancements.Defence += Increment;
            else if (Stat == 4)
                Pkmn.Enhancements.SpecialAttack += Increment;
            else if (Stat == 5)
                Pkmn.Enhancements.SpecialDefence += Increment;
            else if (Stat == 6)
                Pkmn.Enhancements.Speed += Increment;

            Pkmn.RecalculateCombatStats();
            SetStats();
        }

        private void btn_HP_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button).Content.Equals("Buff"))
                ModStat(1, 1);
            else
                ModStat(1, -1);
        }

        private void btn_Atk_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button).Content.Equals("Buff"))
                ModStat(2, 1);
            else
                ModStat(2, -1);
        }

        private void btn_Def_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button).Content.Equals("Buff"))
                ModStat(3, 1);
            else
                ModStat(3, -1);
        }

        private void btn_SpAtk_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button).Content.Equals("Buff"))
                ModStat(4, 1);
            else
                ModStat(4, -1);
        }

        private void btn_SpDef_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button).Content.Equals("Buff"))
                ModStat(5, 1);
            else
                ModStat(5, -1);
        }

        private void btn_Spd_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button).Content.Equals("Buff"))
                ModStat(6, 1);
            else
                ModStat(6, -1);
        }
    }
}