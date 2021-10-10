﻿using System.Linq;
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
        public int PokemonID;

        public PokemonPage(int pkmn)
        {
            InitializeComponent();
            PokemonID = pkmn;
            Pkmn = StaticData.PlayerData.Pkmnlist.Where(s => s.PokemonTID.Equals(pkmn)).First();

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
                NaturalMoveOne.Header = Pkmn.NaturalMoves[0].Name;
                PanelNaturalMoveOne.Children.Add(MakeLabel(Pkmn.NaturalMoves[0].Frequency));
                PanelNaturalMoveOne.Children.Add(MakeLabel(Pkmn.NaturalMoves[0].Range.ToString()));
                PanelNaturalMoveOne.Children.Add(MakeLabel(Pkmn.NaturalMoves[0].Damage.ToString()));
                NaturalMoveOne.Visibility = Visibility.Visible;
            }
            catch
            {
                NaturalMoveOne.Visibility = Visibility.Hidden;
            }

            try
            {
                NaturalMoveTwo.Header = Pkmn.NaturalMoves[1].Name;
                PanelNaturalMoveTwo.Children.Add(MakeLabel(Pkmn.NaturalMoves[1].Frequency));
                PanelNaturalMoveTwo.Children.Add(MakeLabel(Pkmn.NaturalMoves[1].Range.ToString()));
                PanelNaturalMoveTwo.Children.Add(MakeLabel(Pkmn.NaturalMoves[1].Damage.ToString()));
                NaturalMoveTwo.Visibility = Visibility.Visible;
            }
            catch
            {
                NaturalMoveTwo.Visibility = Visibility.Hidden;
            }

            try
            {
                NaturalMoveThree.Header = Pkmn.NaturalMoves[2].Name;
                PanelNaturalMoveThree.Children.Add(MakeLabel(Pkmn.NaturalMoves[2].Frequency));
                PanelNaturalMoveThree.Children.Add(MakeLabel(Pkmn.NaturalMoves[2].Range.ToString()));
                PanelNaturalMoveThree.Children.Add(MakeLabel(Pkmn.NaturalMoves[2].Damage.ToString()));
                NaturalMoveThree.Visibility = Visibility.Visible;
            }
            catch
            {
                NaturalMoveThree.Visibility = Visibility.Hidden;
            }

            try
            {
                NaturalMoveFour.Header = Pkmn.NaturalMoves[3].Name;
                PanelNaturalMoveFour.Children.Add(MakeLabel(Pkmn.NaturalMoves[3].Frequency));
                PanelNaturalMoveFour.Children.Add(MakeLabel(Pkmn.NaturalMoves[3].Range.ToString()));
                PanelNaturalMoveFour.Children.Add(MakeLabel(Pkmn.NaturalMoves[3].Damage.ToString()));
                NaturalMoveFour.Visibility = Visibility.Visible;
            }
            catch
            {
                NaturalMoveFour.Visibility = Visibility.Hidden;
            }

            try
            {
                NaturalMoveFive.Header = Pkmn.NaturalMoves[4].Name;
                PanelNaturalMoveFive.Children.Add(MakeLabel(Pkmn.NaturalMoves[4].Frequency));
                PanelNaturalMoveFive.Children.Add(MakeLabel(Pkmn.NaturalMoves[4].Range.ToString()));
                PanelNaturalMoveFive.Children.Add(MakeLabel(Pkmn.NaturalMoves[4].Damage.ToString()));
                NaturalMoveFive.Visibility = Visibility.Visible;
            }
            catch
            {
                NaturalMoveFive.Visibility = Visibility.Hidden;
            }

            try
            {
                NaturalMoveSix.Header = Pkmn.NaturalMoves[5].Name;
                PanelNaturalMoveSix.Children.Add(MakeLabel(Pkmn.NaturalMoves[5].Frequency));
                PanelNaturalMoveSix.Children.Add(MakeLabel(Pkmn.NaturalMoves[5].Range.ToString()));
                PanelNaturalMoveSix.Children.Add(MakeLabel(Pkmn.NaturalMoves[5].Damage.ToString()));
                NaturalMoveSix.Visibility = Visibility.Visible;
            }
            catch
            {
                NaturalMoveSix.Visibility = Visibility.Hidden;
            }

            try
            {
                NaturalMoveSeven.Header = Pkmn.NaturalMoves[6].Name;
                PanelNaturalMoveSeven.Children.Add(MakeLabel(Pkmn.NaturalMoves[6].Frequency));
                PanelNaturalMoveSeven.Children.Add(MakeLabel(Pkmn.NaturalMoves[6].Range.ToString()));
                PanelNaturalMoveSeven.Children.Add(MakeLabel(Pkmn.NaturalMoves[6].Damage.ToString()));
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
                TutorMoveOne.Header = Pkmn.ArtificalMoves[0].Name;
                PanelTutorMoveOne.Children.Add(MakeLabel(Pkmn.ArtificalMoves[0].Frequency));
                PanelTutorMoveOne.Children.Add(MakeLabel(Pkmn.ArtificalMoves[0].Range.ToString()));
                PanelTutorMoveOne.Children.Add(MakeLabel(Pkmn.ArtificalMoves[0].Damage.ToString()));
                TutorMoveOne.Visibility = Visibility.Visible;
            }
            catch
            {
                TutorMoveOne.Visibility = Visibility.Hidden;
            }

            try
            {
                TutorMoveTwo.Header = Pkmn.ArtificalMoves[1].Name;
                PanelTutorMoveTwo.Children.Add(MakeLabel(Pkmn.ArtificalMoves[1].Frequency));
                PanelTutorMoveTwo.Children.Add(MakeLabel(Pkmn.ArtificalMoves[1].Range.ToString()));
                PanelTutorMoveTwo.Children.Add(MakeLabel(Pkmn.ArtificalMoves[1].Damage.ToString()));
                TutorMoveTwo.Visibility = Visibility.Visible;
            }
            catch
            {
                TutorMoveTwo.Visibility = Visibility.Hidden;
            }

            try
            {
                TutorMoveThree.Header = Pkmn.ArtificalMoves[2].Name;
                PanelTutorMoveThree.Children.Add(MakeLabel(Pkmn.ArtificalMoves[2].Frequency));
                PanelTutorMoveThree.Children.Add(MakeLabel(Pkmn.ArtificalMoves[2].Range.ToString()));
                PanelTutorMoveThree.Children.Add(MakeLabel(Pkmn.ArtificalMoves[2].Damage.ToString()));
                TutorMoveThree.Visibility = Visibility.Visible;
            }
            catch
            {
                TutorMoveThree.Visibility = Visibility.Hidden;
            }

            try
            {
                TutorMoveFour.Header = Pkmn.ArtificalMoves[3].Name;
                PanelTutorMoveFour.Children.Add(MakeLabel(Pkmn.ArtificalMoves[3].Frequency));
                PanelTutorMoveFour.Children.Add(MakeLabel(Pkmn.ArtificalMoves[3].Range.ToString()));
                PanelTutorMoveFour.Children.Add(MakeLabel(Pkmn.ArtificalMoves[3].Damage.ToString()));
                TutorMoveFour.Visibility = Visibility.Visible;
            }
            catch
            {
                TutorMoveFour.Visibility = Visibility.Hidden;
            }

            try
            {
                TutorMoveFive.Header = Pkmn.ArtificalMoves[4].Name;
                PanelTutorMoveFive.Children.Add(MakeLabel(Pkmn.ArtificalMoves[4].Frequency));
                PanelTutorMoveFive.Children.Add(MakeLabel(Pkmn.ArtificalMoves[4].Range.ToString()));
                PanelTutorMoveFive.Children.Add(MakeLabel(Pkmn.ArtificalMoves[4].Damage.ToString()));
                TutorMoveFive.Visibility = Visibility.Visible;
            }
            catch
            {
                TutorMoveFive.Visibility = Visibility.Hidden;
            }

            try
            {
                TutorMoveSix.Header = Pkmn.ArtificalMoves[5].Name;
                PanelTutorMoveSix.Children.Add(MakeLabel(Pkmn.ArtificalMoves[5].Frequency));
                PanelTutorMoveSix.Children.Add(MakeLabel(Pkmn.ArtificalMoves[5].Range.ToString()));
                PanelTutorMoveSix.Children.Add(MakeLabel(Pkmn.ArtificalMoves[5].Damage.ToString()));
                TutorMoveSix.Visibility = Visibility.Visible;
            }
            catch
            {
                TutorMoveSix.Visibility = Visibility.Hidden;
            }

            try
            {
                TutorMoveSeven.Header = Pkmn.ArtificalMoves[6].Name;
                PanelTutorMoveSeven.Children.Add(MakeLabel(Pkmn.ArtificalMoves[6].Frequency));
                PanelTutorMoveSeven.Children.Add(MakeLabel(Pkmn.ArtificalMoves[6].Range.ToString()));
                PanelTutorMoveSeven.Children.Add(MakeLabel(Pkmn.ArtificalMoves[6].Damage.ToString()));
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