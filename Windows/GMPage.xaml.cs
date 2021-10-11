using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using PokemonRPG.Configs;

namespace PokemonRPG.Windows
{
    /// <summary>
    /// Interaction logic for GMPage.xaml
    /// </summary>
    public partial class GMPage : Window
    {
        public GMPage()
        {
            InitializeComponent();
        }
        
        private void btn_Pkmn_Encounter_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(tb_PokemonLevel.Text, out int PkmnLevel) && PkmnLevel > 0 && PkmnLevel < 151)
            {
                if (StaticData.PlayerData.Pkmnlist.Count > 0)
                    StaticData.PlayerData.Pkmnlist.RemoveAt(0);
                StaticData.PlayerData.Pkmnlist.Add(StaticData.ReferenceData.GenerateTrainerPokemon(
                    StaticData.ReferenceData.RandomGenerator.Next(0,
                        StaticData.ReferenceData.Pokedex.PokemonDexList.Count),
                    StaticData.ReferenceData.RandomGenerator.Next(1, PkmnLevel + 1)));
                StaticData.PlayerData.Pkmnlist[0].CurrentHP = StaticData.PlayerData.Pkmnlist[0].MaxHP;
                var page = new PokemonPage(0);
                page.Show();
            }
            else
            {
                MessageBox.Show("Invalid Level in box");
            }
        }
    }
}
