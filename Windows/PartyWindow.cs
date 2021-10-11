using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using PokemonRPG.Configs;

namespace PokemonRPG.Windows
{
    /// <summary>
    ///     Interaction logic for Party.xaml
    /// </summary>
    public partial class PartyWindow : Window
    {
        public PartyWindow()
        {
            InitializeComponent();
            SetAssets();
        }

        private void SetAssets()
        {
            BindStats();
        }

        public void BindStats()
        {
            if (StaticData.PlayerData.CurrentParty.Count > 0)
            {
                Pkmn_One_Nickname.Content =
                    StaticData.PlayerData.Pkmnlist[StaticData.PlayerData.CurrentParty[0]].Nickname;
                Pkmn_One_Primary_Type.Source = GetTypeAsset(StaticData.PlayerData
                    .Pkmnlist[StaticData.PlayerData.CurrentParty[0]].PrimaryTypeID);
                Pkmn_One_Secondary_Type.Source = GetTypeAsset(StaticData.PlayerData
                    .Pkmnlist[StaticData.PlayerData.CurrentParty[0]].SecondaryTypeID);
                Pkmn_One_Species.Content =
                    StaticData.PlayerData.Pkmnlist[StaticData.PlayerData.CurrentParty[0]].Name;
                Pkmn_One_Level.Content = StaticData.PlayerData.Pkmnlist[StaticData.PlayerData.CurrentParty[0]].Level;
                Pkmn_One_Attack_Bar.Width = StaticData.PlayerData.Pkmnlist[StaticData.PlayerData.CurrentParty[0]]
                    .ActualStats.Attack;
                Pkmn_One_Defence_Bar.Width = StaticData.PlayerData.Pkmnlist[StaticData.PlayerData.CurrentParty[0]]
                    .ActualStats.Defence;
                Pkmn_One_HP_Bar.Width =
                    StaticData.PlayerData.Pkmnlist[StaticData.PlayerData.CurrentParty[0]].ActualStats.HP;
                Pkmn_One_Speed_Bar.Width = StaticData.PlayerData.Pkmnlist[StaticData.PlayerData.CurrentParty[0]]
                    .ActualStats.Speed;
                Pkmn_One_Special_Attack_Bar.Width = StaticData.PlayerData
                    .Pkmnlist[StaticData.PlayerData.CurrentParty[0]].ActualStats.SpecialAttack;
                Pkmn_One_Special_Defence_Bar.Width = StaticData.PlayerData
                    .Pkmnlist[StaticData.PlayerData.CurrentParty[0]].ActualStats.SpecialDefence;
                Grid_Pkmn_One.Visibility = Visibility.Visible;
            }
            else
            {
                Grid_Pkmn_One.Visibility = Visibility.Hidden;
            }

            if (StaticData.PlayerData.CurrentParty.Count > 1)
            {
                Pkmn_Two_Nickname.Content =
                    StaticData.PlayerData.Pkmnlist[StaticData.PlayerData.CurrentParty[1]].Nickname;
                Pkmn_Two_Primary_Type.Source = GetTypeAsset(StaticData.PlayerData
                    .Pkmnlist[StaticData.PlayerData.CurrentParty[1]].PrimaryTypeID);
                Pkmn_Two_Secondary_Type.Source = GetTypeAsset(StaticData.PlayerData
                    .Pkmnlist[StaticData.PlayerData.CurrentParty[1]].SecondaryTypeID);
                Pkmn_Two_Species.Content =
                    StaticData.PlayerData.Pkmnlist[StaticData.PlayerData.CurrentParty[1]].Name;
                Pkmn_Two_Level.Content = StaticData.PlayerData.Pkmnlist[StaticData.PlayerData.CurrentParty[1]].Level;
                Pkmn_Two_Attack_Bar.Width = StaticData.PlayerData.Pkmnlist[StaticData.PlayerData.CurrentParty[1]]
                    .ActualStats.Attack;
                Pkmn_Two_Defence_Bar.Width = StaticData.PlayerData.Pkmnlist[StaticData.PlayerData.CurrentParty[1]]
                    .ActualStats.Defence;
                Pkmn_Two_HP_Bar.Width =
                    StaticData.PlayerData.Pkmnlist[StaticData.PlayerData.CurrentParty[1]].ActualStats.HP;
                Pkmn_Two_Speed_Bar.Width = StaticData.PlayerData.Pkmnlist[StaticData.PlayerData.CurrentParty[1]]
                    .ActualStats.Speed;
                Pkmn_Two_Special_Attack_Bar.Width = StaticData.PlayerData
                    .Pkmnlist[StaticData.PlayerData.CurrentParty[1]].ActualStats.SpecialAttack;
                Pkmn_Two_Special_Defence_Bar.Width = StaticData.PlayerData
                    .Pkmnlist[StaticData.PlayerData.CurrentParty[1]].ActualStats.SpecialDefence;
                Grid_Pkmn_Two.Visibility = Visibility.Visible;
            }
            else
            {
                Grid_Pkmn_Two.Visibility = Visibility.Hidden;
            }

            if (StaticData.PlayerData.CurrentParty.Count > 2)
            {
                Pkmn_Three_Nickname.Content =
                    StaticData.PlayerData.Pkmnlist[StaticData.PlayerData.CurrentParty[2]].Nickname;
                Pkmn_Three_Primary_Type.Source = GetTypeAsset(StaticData.PlayerData
                    .Pkmnlist[StaticData.PlayerData.CurrentParty[2]].PrimaryTypeID);
                Pkmn_Three_Secondary_Type.Source = GetTypeAsset(StaticData.PlayerData
                    .Pkmnlist[StaticData.PlayerData.CurrentParty[2]].SecondaryTypeID);
                Pkmn_Three_Species.Content =
                    StaticData.PlayerData.Pkmnlist[StaticData.PlayerData.CurrentParty[2]].Name;
                Pkmn_Three_Level.Content = StaticData.PlayerData.Pkmnlist[StaticData.PlayerData.CurrentParty[2]].Level;
                Pkmn_Three_Attack_Bar.Width = StaticData.PlayerData.Pkmnlist[StaticData.PlayerData.CurrentParty[2]]
                    .ActualStats.Attack;
                Pkmn_Three_Defence_Bar.Width = StaticData.PlayerData.Pkmnlist[StaticData.PlayerData.CurrentParty[2]]
                    .ActualStats.Defence;
                Pkmn_Three_HP_Bar.Width =
                    StaticData.PlayerData.Pkmnlist[StaticData.PlayerData.CurrentParty[2]].ActualStats.HP;
                Pkmn_Three_Speed_Bar.Width = StaticData.PlayerData.Pkmnlist[StaticData.PlayerData.CurrentParty[2]]
                    .ActualStats.Speed;
                Pkmn_Three_Special_Attack_Bar.Width = StaticData.PlayerData
                    .Pkmnlist[StaticData.PlayerData.CurrentParty[2]].ActualStats.SpecialAttack;
                Pkmn_Three_Special_Defence_Bar.Width = StaticData.PlayerData
                    .Pkmnlist[StaticData.PlayerData.CurrentParty[2]].ActualStats.SpecialDefence;
                Grid_Pkmn_Three.Visibility = Visibility.Visible;
            }
            else
            {
                Grid_Pkmn_Three.Visibility = Visibility.Hidden;
            }

            if (StaticData.PlayerData.CurrentParty.Count > 3)
            {
                Pkmn_Four_Nickname.Content =
                    StaticData.PlayerData.Pkmnlist[StaticData.PlayerData.CurrentParty[3]].Nickname;
                Pkmn_Four_Primary_Type.Source = GetTypeAsset(StaticData.PlayerData
                    .Pkmnlist[StaticData.PlayerData.CurrentParty[3]].PrimaryTypeID);
                Pkmn_Four_Secondary_Type.Source = GetTypeAsset(StaticData.PlayerData
                    .Pkmnlist[StaticData.PlayerData.CurrentParty[3]].SecondaryTypeID);
                Pkmn_Four_Species.Content =
                    StaticData.PlayerData.Pkmnlist[StaticData.PlayerData.CurrentParty[3]].Name;
                Pkmn_Four_Level.Content = StaticData.PlayerData.Pkmnlist[StaticData.PlayerData.CurrentParty[3]].Level;
                Pkmn_Four_Attack_Bar.Width = StaticData.PlayerData.Pkmnlist[StaticData.PlayerData.CurrentParty[3]]
                    .ActualStats.Attack;
                Pkmn_Four_Defence_Bar.Width = StaticData.PlayerData.Pkmnlist[StaticData.PlayerData.CurrentParty[3]]
                    .ActualStats.Defence;
                Pkmn_Four_HP_Bar.Width =
                    StaticData.PlayerData.Pkmnlist[StaticData.PlayerData.CurrentParty[3]].ActualStats.HP;
                Pkmn_Four_Speed_Bar.Width = StaticData.PlayerData.Pkmnlist[StaticData.PlayerData.CurrentParty[3]]
                    .ActualStats.Speed;
                Pkmn_Four_Special_Attack_Bar.Width = StaticData.PlayerData
                    .Pkmnlist[StaticData.PlayerData.CurrentParty[3]].ActualStats.SpecialAttack;
                Pkmn_Four_Special_Defence_Bar.Width = StaticData.PlayerData
                    .Pkmnlist[StaticData.PlayerData.CurrentParty[3]].ActualStats.SpecialDefence;
                Grid_Pkmn_Four.Visibility = Visibility.Visible;
            }
            else
            {
                Grid_Pkmn_Four.Visibility = Visibility.Hidden;
            }

            if (StaticData.PlayerData.CurrentParty.Count > 4)
            {
                Pkmn_Five_Nickname.Content =
                    StaticData.PlayerData.Pkmnlist[StaticData.PlayerData.CurrentParty[4]].Nickname;
                Pkmn_Five_Primary_Type.Source = GetTypeAsset(StaticData.PlayerData
                    .Pkmnlist[StaticData.PlayerData.CurrentParty[4]].PrimaryTypeID);
                Pkmn_Five_Secondary_Type.Source = GetTypeAsset(StaticData.PlayerData
                    .Pkmnlist[StaticData.PlayerData.CurrentParty[4]].SecondaryTypeID);
                Pkmn_Five_Species.Content =
                    StaticData.PlayerData.Pkmnlist[StaticData.PlayerData.CurrentParty[4]].Name;
                Pkmn_Five_Level.Content = StaticData.PlayerData.Pkmnlist[StaticData.PlayerData.CurrentParty[4]].Level;
                Pkmn_Five_Attack_Bar.Width = StaticData.PlayerData.Pkmnlist[StaticData.PlayerData.CurrentParty[4]]
                    .ActualStats.Attack;
                Pkmn_Five_Defence_Bar.Width = StaticData.PlayerData.Pkmnlist[StaticData.PlayerData.CurrentParty[4]]
                    .ActualStats.Defence;
                Pkmn_Five_HP_Bar.Width =
                    StaticData.PlayerData.Pkmnlist[StaticData.PlayerData.CurrentParty[4]].ActualStats.HP;
                Pkmn_Five_Speed_Bar.Width = StaticData.PlayerData.Pkmnlist[StaticData.PlayerData.CurrentParty[4]]
                    .ActualStats.Speed;
                Pkmn_Five_Special_Attack_Bar.Width = StaticData.PlayerData
                    .Pkmnlist[StaticData.PlayerData.CurrentParty[4]].ActualStats.SpecialAttack;
                Pkmn_Five_Special_Defence_Bar.Width = StaticData.PlayerData
                    .Pkmnlist[StaticData.PlayerData.CurrentParty[4]].ActualStats.SpecialDefence;
                Grid_Pkmn_Five.Visibility = Visibility.Visible;
            }
            else
            {
                Grid_Pkmn_Five.Visibility = Visibility.Hidden;
            }

            if (StaticData.PlayerData.CurrentParty.Count > 5)
            {
                Pkmn_Six_Nickname.Content =
                    StaticData.PlayerData.Pkmnlist[StaticData.PlayerData.CurrentParty[5]].Nickname;
                Pkmn_Six_Primary_Type.Source = GetTypeAsset(StaticData.PlayerData
                    .Pkmnlist[StaticData.PlayerData.CurrentParty[5]].PrimaryTypeID);
                Pkmn_Six_Secondary_Type.Source = GetTypeAsset(StaticData.PlayerData
                    .Pkmnlist[StaticData.PlayerData.CurrentParty[5]].SecondaryTypeID);
                Pkmn_Six_Species.Content =
                    StaticData.PlayerData.Pkmnlist[StaticData.PlayerData.CurrentParty[5]].Name;
                Pkmn_Six_Level.Content = StaticData.PlayerData.Pkmnlist[StaticData.PlayerData.CurrentParty[5]].Level;
                Pkmn_Six_Attack_Bar.Width = StaticData.PlayerData.Pkmnlist[StaticData.PlayerData.CurrentParty[5]]
                    .ActualStats.Attack;
                Pkmn_Six_Defence_Bar.Width = StaticData.PlayerData.Pkmnlist[StaticData.PlayerData.CurrentParty[5]]
                    .ActualStats.Defence;
                Pkmn_Six_HP_Bar.Width =
                    StaticData.PlayerData.Pkmnlist[StaticData.PlayerData.CurrentParty[5]].ActualStats.HP;
                Pkmn_Six_Speed_Bar.Width = StaticData.PlayerData.Pkmnlist[StaticData.PlayerData.CurrentParty[5]]
                    .ActualStats.Speed;
                Pkmn_Six_Special_Attack_Bar.Width = StaticData.PlayerData
                    .Pkmnlist[StaticData.PlayerData.CurrentParty[5]].ActualStats.SpecialAttack;
                Pkmn_Six_Special_Defence_Bar.Width = StaticData.PlayerData
                    .Pkmnlist[StaticData.PlayerData.CurrentParty[5]].ActualStats.SpecialDefence;
                Grid_Pkmn_Six.Visibility = Visibility.Visible;
            }
            else
            {
                Grid_Pkmn_Six.Visibility = Visibility.Hidden;
            }

            if (StaticData.PlayerData.CurrentParty.Count > 6)
            {
                Pkmn_Seven_Nickname.Content =
                    StaticData.PlayerData.Pkmnlist[StaticData.PlayerData.CurrentParty[6]].Nickname;
                Pkmn_Seven_Primary_Type.Source = GetTypeAsset(StaticData.PlayerData
                    .Pkmnlist[StaticData.PlayerData.CurrentParty[6]].PrimaryTypeID);
                Pkmn_Seven_Secondary_Type.Source = GetTypeAsset(StaticData.PlayerData
                    .Pkmnlist[StaticData.PlayerData.CurrentParty[6]].SecondaryTypeID);
                Pkmn_Seven_Species.Content =
                    StaticData.PlayerData.Pkmnlist[StaticData.PlayerData.CurrentParty[6]].Name;
                Pkmn_Seven_Level.Content = StaticData.PlayerData.Pkmnlist[StaticData.PlayerData.CurrentParty[6]].Level;
                Pkmn_Seven_Attack_Bar.Width = StaticData.PlayerData.Pkmnlist[StaticData.PlayerData.CurrentParty[6]]
                    .ActualStats.Attack;
                Pkmn_Seven_Defence_Bar.Width = StaticData.PlayerData.Pkmnlist[StaticData.PlayerData.CurrentParty[6]]
                    .ActualStats.Defence;
                Pkmn_Seven_HP_Bar.Width =
                    StaticData.PlayerData.Pkmnlist[StaticData.PlayerData.CurrentParty[6]].ActualStats.HP;
                Pkmn_Seven_Speed_Bar.Width = StaticData.PlayerData.Pkmnlist[StaticData.PlayerData.CurrentParty[6]]
                    .ActualStats.Speed;
                Pkmn_Seven_Special_Attack_Bar.Width = StaticData.PlayerData
                    .Pkmnlist[StaticData.PlayerData.CurrentParty[6]].ActualStats.SpecialAttack;
                Pkmn_Seven_Special_Defence_Bar.Width = StaticData.PlayerData
                    .Pkmnlist[StaticData.PlayerData.CurrentParty[6]].ActualStats.SpecialDefence;
                Grid_Pkmn_Seven.Visibility = Visibility.Visible;
            }
            else
            {
                Grid_Pkmn_Seven.Visibility = Visibility.Hidden;
            }

            if (StaticData.PlayerData.CurrentParty.Count > 7)
            {
                Pkmn_Eight_Nickname.Content =
                    StaticData.PlayerData.Pkmnlist[StaticData.PlayerData.CurrentParty[7]].Nickname;
                Pkmn_Eight_Primary_Type.Source = GetTypeAsset(StaticData.PlayerData
                    .Pkmnlist[StaticData.PlayerData.CurrentParty[7]].PrimaryTypeID);
                Pkmn_Eight_Secondary_Type.Source = GetTypeAsset(StaticData.PlayerData
                    .Pkmnlist[StaticData.PlayerData.CurrentParty[7]].SecondaryTypeID);
                Pkmn_Eight_Species.Content =
                    StaticData.PlayerData.Pkmnlist[StaticData.PlayerData.CurrentParty[7]].Name;
                Pkmn_Eight_Level.Content = StaticData.PlayerData.Pkmnlist[StaticData.PlayerData.CurrentParty[7]].Level;
                Pkmn_Eight_Attack_Bar.Width = StaticData.PlayerData.Pkmnlist[StaticData.PlayerData.CurrentParty[7]]
                    .ActualStats.Attack;
                Pkmn_Eight_Defence_Bar.Width = StaticData.PlayerData.Pkmnlist[StaticData.PlayerData.CurrentParty[7]]
                    .ActualStats.Defence;
                Pkmn_Eight_HP_Bar.Width =
                    StaticData.PlayerData.Pkmnlist[StaticData.PlayerData.CurrentParty[7]].ActualStats.HP;
                Pkmn_Eight_Speed_Bar.Width = StaticData.PlayerData.Pkmnlist[StaticData.PlayerData.CurrentParty[7]]
                    .ActualStats.Speed;
                Pkmn_Eight_Special_Attack_Bar.Width = StaticData.PlayerData
                    .Pkmnlist[StaticData.PlayerData.CurrentParty[7]].ActualStats.SpecialAttack;
                Pkmn_Eight_Special_Defence_Bar.Width = StaticData.PlayerData
                    .Pkmnlist[StaticData.PlayerData.CurrentParty[7]].ActualStats.SpecialDefence;
                Grid_Pkmn_Eight.Visibility = Visibility.Visible;
            }
            else
            {
                Grid_Pkmn_Eight.Visibility = Visibility.Hidden;
            }
        }


        public ImageSource GetTypeAsset(int TypeID)
        {
            var TypeName = "Unknown";
            try
            {
                TypeName = StaticData.ReferenceData.TypeDex.TypeList[TypeID].Name;
                var DefaultTypePath = $"Resources/Image/Types/{TypeName}.png";
                var c = new ImageSourceConverter();
                return (ImageSource)c.ConvertFrom(DefaultTypePath);
            }
            catch
            {
            }

            return null;

        }

        private void Pkmn_One_Current_HP_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var pkmnpage =
                new PokemonPage(StaticData.PlayerData.CurrentParty[0]);
            pkmnpage.Show();
        }
        private void Pkmn_Two_Current_HP_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var pkmnpage =
                new PokemonPage(StaticData.PlayerData.CurrentParty[1]);
            pkmnpage.Show();
        }
        private void Pkmn_Three_Current_HP_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var pkmnpage =
                new PokemonPage(StaticData.PlayerData.CurrentParty[2]);
            pkmnpage.Show();
        }
        private void Pkmn_Four_Current_HP_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var pkmnpage =
                new PokemonPage(StaticData.PlayerData.CurrentParty[3]);
            pkmnpage.Show();
        }
        private void Pkmn_Five_Current_HP_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var pkmnpage =
                new PokemonPage(StaticData.PlayerData.CurrentParty[4]);
            pkmnpage.Show();
        }
        private void Pkmn_Six_Current_HP_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var pkmnpage =
                new PokemonPage(StaticData.PlayerData.CurrentParty[5]);
            pkmnpage.Show();
        }
        private void Pkmn_Seven_Current_HP_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var pkmnpage =
                new PokemonPage(StaticData.PlayerData.CurrentParty[6]);
            pkmnpage.Show();
        }
        private void Pkmn_Eight_Current_HP_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var pkmnpage =
                new PokemonPage(StaticData.PlayerData.CurrentParty[7]);
            pkmnpage.Show();
        }
    }
}