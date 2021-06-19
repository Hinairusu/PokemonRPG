using PokemonRPG.Configs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.DataVisualization.Charting;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PokemonRPG.Windows
{
    /// <summary>
    /// Interaction logic for Party.xaml
    /// </summary>
    public partial class PartyWindow : Window
    {


        TrainerPokemon Pkmn_One = new TrainerPokemon();
        TrainerPokemon Pkmn_Two = new TrainerPokemon();
        TrainerPokemon Pkmn_Three = new TrainerPokemon();
        TrainerPokemon Pkmn_Four = new TrainerPokemon();
        TrainerPokemon Pkmn_Five = new TrainerPokemon();
        TrainerPokemon Pkmn_Six = new TrainerPokemon();
        TrainerPokemon Pkmn_Seven = new TrainerPokemon();
        TrainerPokemon Pkmn_Eight = new TrainerPokemon();
        public PartyWindow()
        {
            InitializeComponent();
            SetAssets();
        }

       private void SetAssets()
        {
            

            if (StaticData.PlayerData.CurrentParty.Count > 0)
            {
                Grid_Pkmn_One.Visibility = Visibility.Visible;
                Pkmn_One = StaticData.PlayerData.CurrentParty[0];
            }
            else
                Grid_Pkmn_One.Visibility = Visibility.Hidden;

            if (StaticData.PlayerData.CurrentParty.Count > 1)
            {
                Grid_Pkmn_Two.Visibility = Visibility.Visible;
                Pkmn_Two = StaticData.PlayerData.CurrentParty[1];
            }
            else
                Grid_Pkmn_Two.Visibility = Visibility.Hidden;

            if (StaticData.PlayerData.CurrentParty.Count > 2)
            {
                Grid_Pkmn_Three.Visibility = Visibility.Visible;
                Pkmn_Three = StaticData.PlayerData.CurrentParty[2];
            }
            else
                Grid_Pkmn_Three.Visibility = Visibility.Hidden;

            if (StaticData.PlayerData.CurrentParty.Count > 3)
            {
                Grid_Pkmn_Four.Visibility = Visibility.Visible;
                Pkmn_Four = StaticData.PlayerData.CurrentParty[3];
            }
            else
                Grid_Pkmn_Four.Visibility = Visibility.Hidden;

            if (StaticData.PlayerData.CurrentParty.Count > 4)
            {
                Grid_Pkmn_Five.Visibility = Visibility.Visible;
                Pkmn_Five = StaticData.PlayerData.CurrentParty[4];
            }
            else
                Grid_Pkmn_Five.Visibility = Visibility.Hidden;

            if (StaticData.PlayerData.CurrentParty.Count > 5)
            {
                Grid_Pkmn_Six.Visibility = Visibility.Visible;
                Pkmn_Six = StaticData.PlayerData.CurrentParty[5];
            }
            else
                Grid_Pkmn_Six.Visibility = Visibility.Hidden;

            if (StaticData.PlayerData.CompanionPokemonParty.Count > 0)
            {
                Grid_Pkmn_Seven.Visibility = Visibility.Visible;
                Pkmn_Seven = StaticData.PlayerData.CompanionPokemonParty[0];
            }
            else
                Grid_Pkmn_Seven.Visibility = Visibility.Hidden;

            if (StaticData.PlayerData.CompanionPokemonParty.Count > 1)
            {
                Grid_Pkmn_Eight.Visibility = Visibility.Visible;
                Pkmn_Eight = StaticData.PlayerData.CompanionPokemonParty[1];
            }
            else
                Grid_Pkmn_Eight.Visibility = Visibility.Hidden;

            BindStats();

        }

        public void BindStats()
        {
            Pkmn_One_Nickname.Content = Pkmn_One.Nickname;
            Pkmn_One_Primary_Type.Source = GetTypeAsset(Pkmn_One.PrimaryTypeID);
            Pkmn_One_Secondary_Type.Source = GetTypeAsset(Pkmn_One.SecondaryTypeID);
            Pkmn_One_Species.Content = Pkmn_One.PokemonFamily;
            Pkmn_One_Level.Content = Pkmn_One.Level;
            Pkmn_One_Attack_Bar.Width = Pkmn_One.ActualStats.Attack;
            Pkmn_One_Defence_Bar.Width = Pkmn_One.ActualStats.Defence;
            Pkmn_One_HP_Bar.Width = Pkmn_One.ActualStats.HP;
            Pkmn_One_Speed_Bar.Width = Pkmn_One.ActualStats.Speed;
            Pkmn_One_Special_Attack_Bar.Width = Pkmn_One.ActualStats.SpecialAttack;
            Pkmn_One_Special_Defence_Bar.Width = Pkmn_One.ActualStats.SpecialDefence;
        }


        public ImageSource GetTypeAsset(int TypeID)
        {
            string TypeName = "Unknown";
            try
            {
                TypeName = StaticData.ReferenceData.TypeDex.TypeList[TypeID].Name;
            }
            catch { }
            string DefaultTypePath = $"Resources/Image/Types/{TypeName}.png";
            ImageSourceConverter c = new ImageSourceConverter();
            return (ImageSource)c.ConvertFrom(DefaultTypePath);
        }
 
    }
}
