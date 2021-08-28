using PokemonRPG.Configs;
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

namespace PokemonRPG.Windows
{
    /// <summary>
    /// Interaction logic for PokemonPage.xaml
    /// </summary>
    public partial class PokemonPage : Window
    {
        public int PokemonID;
        public PokemonPage(int pkmn)
        {
            InitializeComponent();
            PokemonID = pkmn;
           TrainerPokemon it =  StaticData.PlayerData.Pkmnlist.Where(s => s.PokemonTID.Equals(pkmn)).First();
        }



    }
}
