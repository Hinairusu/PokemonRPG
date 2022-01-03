using System.Linq;
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

            try
            {

                TrainerPokemon pk = StaticData.PlayerData.OwnedPokemon.Single(s =>
                    s.UID.Equals(StaticData.PlayerData.CurrentParty.Single(d => d.Slot.Equals(0)).PokemonUID));
               
                    Pkmn_One_Nickname.Content = pk.Nickname;
                    Pkmn_One_Primary_Type.Source = GetTypeAsset(pk.PrimaryTypeID);
                    Pkmn_One_Secondary_Type.Source = GetTypeAsset(pk.SecondaryTypeID);
                    Pkmn_One_Species.Content = pk.Name;
                    Pkmn_One_Level.Content = pk.Level;
                    Pkmn_One_Attack_Bar.Width = pk.ActualStats.Attack;
                    Pkmn_One_Defence_Bar.Width = pk.ActualStats.Defence;
                    Pkmn_One_HP_Bar.Width = pk.ActualStats.HP;
                    Pkmn_One_Speed_Bar.Width = pk.ActualStats.Speed;
                    Pkmn_One_Special_Attack_Bar.Width = pk.ActualStats.SpecialAttack;
                    Pkmn_One_Special_Defence_Bar.Width = pk.ActualStats.SpecialDefence;
                    Grid_Pkmn_One.Visibility = Visibility.Visible;
                
            }
            catch{Grid_Pkmn_One.Visibility = Visibility.Hidden;}

            try{
            
                TrainerPokemon pk = StaticData.PlayerData.OwnedPokemon.Single(s =>
                    s.UID.Equals(StaticData.PlayerData.CurrentParty.Single(d => d.Slot.Equals(1)).PokemonUID));
                Pkmn_Two_Nickname.Content =
                    pk.Nickname;
                Pkmn_Two_Primary_Type.Source = GetTypeAsset(pk.PrimaryTypeID);
                Pkmn_Two_Secondary_Type.Source = GetTypeAsset(pk.SecondaryTypeID);
                Pkmn_Two_Species.Content =
                    pk.Name;
                Pkmn_Two_Level.Content = pk.Level;
                Pkmn_Two_Attack_Bar.Width = pk
                    .ActualStats.Attack;
                Pkmn_Two_Defence_Bar.Width = pk
                    .ActualStats.Defence;
                Pkmn_Two_HP_Bar.Width =
                    pk.ActualStats.HP;
                Pkmn_Two_Speed_Bar.Width = pk
                    .ActualStats.Speed;
                Pkmn_Two_Special_Attack_Bar.Width = pk.ActualStats.SpecialAttack;
                Pkmn_Two_Special_Defence_Bar.Width = pk.ActualStats.SpecialDefence;
                Grid_Pkmn_Two.Visibility = Visibility.Visible;
            }
            catch
            {
                Grid_Pkmn_Two.Visibility = Visibility.Hidden;
            }

            try
            {
                TrainerPokemon pk = StaticData.PlayerData.OwnedPokemon.Single(s =>
                    s.UID.Equals(StaticData.PlayerData.CurrentParty.Single(d => d.Slot.Equals(2)).PokemonUID));
                Pkmn_Three_Nickname.Content =
                    pk.Nickname;
                Pkmn_Three_Primary_Type.Source = GetTypeAsset(pk.PrimaryTypeID);
                Pkmn_Three_Secondary_Type.Source = GetTypeAsset(pk.SecondaryTypeID);
                Pkmn_Three_Species.Content =
                    pk.Name;
                Pkmn_Three_Level.Content = pk.Level;
                Pkmn_Three_Attack_Bar.Width = pk
                    .ActualStats.Attack;
                Pkmn_Three_Defence_Bar.Width = pk
                    .ActualStats.Defence;
                Pkmn_Three_HP_Bar.Width =
                    pk.ActualStats.HP;
                Pkmn_Three_Speed_Bar.Width = pk
                    .ActualStats.Speed;
                Pkmn_Three_Special_Attack_Bar.Width = pk.ActualStats.SpecialAttack;
                Pkmn_Three_Special_Defence_Bar.Width = pk.ActualStats.SpecialDefence;
                Grid_Pkmn_Three.Visibility = Visibility.Visible;
            }
            catch
            {
                Grid_Pkmn_Three.Visibility = Visibility.Hidden;
            }

            try
            {
                TrainerPokemon pk = StaticData.PlayerData.OwnedPokemon.Single(s =>
                    s.UID.Equals(StaticData.PlayerData.CurrentParty.Single(d => d.Slot.Equals(3)).PokemonUID));
                Pkmn_Four_Nickname.Content =
                    pk.Nickname;
                Pkmn_Four_Primary_Type.Source = GetTypeAsset(pk.PrimaryTypeID);
                Pkmn_Four_Secondary_Type.Source = GetTypeAsset(pk.SecondaryTypeID);
                Pkmn_Four_Species.Content =
                    pk.Name;
                Pkmn_Four_Level.Content = pk.Level;
                Pkmn_Four_Attack_Bar.Width = pk
                    .ActualStats.Attack;
                Pkmn_Four_Defence_Bar.Width = pk
                    .ActualStats.Defence;
                Pkmn_Four_HP_Bar.Width =
                    pk.ActualStats.HP;
                Pkmn_Four_Speed_Bar.Width = pk
                    .ActualStats.Speed;
                Pkmn_Four_Special_Attack_Bar.Width = pk.ActualStats.SpecialAttack;
                Pkmn_Four_Special_Defence_Bar.Width = pk.ActualStats.SpecialDefence;
                Grid_Pkmn_Four.Visibility = Visibility.Visible;
            }
            catch
            {
                Grid_Pkmn_Four.Visibility = Visibility.Hidden;
            }

            try
            {
                TrainerPokemon pk = StaticData.PlayerData.OwnedPokemon.Single(s =>
                    s.UID.Equals(StaticData.PlayerData.CurrentParty.Single(d => d.Slot.Equals(4)).PokemonUID));
                Pkmn_Five_Nickname.Content =
                    pk.Nickname;
                Pkmn_Five_Primary_Type.Source = GetTypeAsset(pk.PrimaryTypeID);
                Pkmn_Five_Secondary_Type.Source = GetTypeAsset(pk.SecondaryTypeID);
                Pkmn_Five_Species.Content =
                    pk.Name;
                Pkmn_Five_Level.Content = pk.Level;
                Pkmn_Five_Attack_Bar.Width = pk
                    .ActualStats.Attack;
                Pkmn_Five_Defence_Bar.Width = pk
                    .ActualStats.Defence;
                Pkmn_Five_HP_Bar.Width =
                    pk.ActualStats.HP;
                Pkmn_Five_Speed_Bar.Width = pk
                    .ActualStats.Speed;
                Pkmn_Five_Special_Attack_Bar.Width = pk.ActualStats.SpecialAttack;
                Pkmn_Five_Special_Defence_Bar.Width = pk.ActualStats.SpecialDefence;
                Grid_Pkmn_Five.Visibility = Visibility.Visible;
            }
            catch
            {
                Grid_Pkmn_Five.Visibility = Visibility.Hidden;
            }

            try
            {
                TrainerPokemon pk = StaticData.PlayerData.OwnedPokemon.Single(s =>
                    s.UID.Equals(StaticData.PlayerData.CurrentParty.Single(d => d.Slot.Equals(5)).PokemonUID));
                Pkmn_Six_Nickname.Content =
                    pk.Nickname;
                Pkmn_Six_Primary_Type.Source = GetTypeAsset(pk.PrimaryTypeID);
                Pkmn_Six_Secondary_Type.Source = GetTypeAsset(pk.SecondaryTypeID);
                Pkmn_Six_Species.Content =
                    pk.Name;
                Pkmn_Six_Level.Content = pk.Level;
                Pkmn_Six_Attack_Bar.Width = pk
                    .ActualStats.Attack;
                Pkmn_Six_Defence_Bar.Width = pk
                    .ActualStats.Defence;
                Pkmn_Six_HP_Bar.Width =
                    pk.ActualStats.HP;
                Pkmn_Six_Speed_Bar.Width = pk
                    .ActualStats.Speed;
                Pkmn_Six_Special_Attack_Bar.Width = pk.ActualStats.SpecialAttack;
                Pkmn_Six_Special_Defence_Bar.Width = pk.ActualStats.SpecialDefence;
                Grid_Pkmn_Six.Visibility = Visibility.Visible;
            }
            catch
            {
                Grid_Pkmn_Six.Visibility = Visibility.Hidden;
            }

            try
            {
                TrainerPokemon pk = StaticData.PlayerData.OwnedPokemon.Single(s =>
                    s.UID.Equals(StaticData.PlayerData.CurrentParty.Single(d => d.Slot.Equals(6)).PokemonUID));
                Pkmn_Seven_Nickname.Content =
                    pk.Nickname;
                Pkmn_Seven_Primary_Type.Source = GetTypeAsset(pk.PrimaryTypeID);
                Pkmn_Seven_Secondary_Type.Source = GetTypeAsset(pk.SecondaryTypeID);
                Pkmn_Seven_Species.Content =
                    pk.Name;
                Pkmn_Seven_Level.Content = pk.Level;
                Pkmn_Seven_Attack_Bar.Width = pk
                    .ActualStats.Attack;
                Pkmn_Seven_Defence_Bar.Width = pk
                    .ActualStats.Defence;
                Pkmn_Seven_HP_Bar.Width =
                    pk.ActualStats.HP;
                Pkmn_Seven_Speed_Bar.Width = pk
                    .ActualStats.Speed;
                Pkmn_Seven_Special_Attack_Bar.Width = pk.ActualStats.SpecialAttack;
                Pkmn_Seven_Special_Defence_Bar.Width = pk.ActualStats.SpecialDefence;
                Grid_Pkmn_Seven.Visibility = Visibility.Visible;
            }
            catch
            {
                Grid_Pkmn_Seven.Visibility = Visibility.Hidden;
            }

            try
            {
                TrainerPokemon pk = StaticData.PlayerData.OwnedPokemon.Single(s =>
                    s.UID.Equals(StaticData.PlayerData.CurrentParty.Single(d => d.Slot.Equals(7)).PokemonUID));
                Pkmn_Eight_Nickname.Content =
                    pk.Nickname;
                Pkmn_Eight_Primary_Type.Source = GetTypeAsset(pk.PrimaryTypeID);
                Pkmn_Eight_Secondary_Type.Source = GetTypeAsset(pk.SecondaryTypeID);
                Pkmn_Eight_Species.Content =
                    pk.Name;
                Pkmn_Eight_Level.Content = pk.Level;
                Pkmn_Eight_Attack_Bar.Width = pk
                    .ActualStats.Attack;
                Pkmn_Eight_Defence_Bar.Width = pk
                    .ActualStats.Defence;
                Pkmn_Eight_HP_Bar.Width =
                    pk.ActualStats.HP;
                Pkmn_Eight_Speed_Bar.Width = pk
                    .ActualStats.Speed;
                Pkmn_Eight_Special_Attack_Bar.Width = pk.ActualStats.SpecialAttack;
                Pkmn_Eight_Special_Defence_Bar.Width = pk.ActualStats.SpecialDefence;
                Grid_Pkmn_Eight.Visibility = Visibility.Visible;
            }
            catch
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
                new PokemonPage(StaticData.PlayerData.CurrentParty.Single(s => s.Slot.Equals(0)).PokemonUID);
            pkmnpage.Show();
        }
        private void Pkmn_Two_Current_HP_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var pkmnpage =
                new PokemonPage(StaticData.PlayerData.CurrentParty.Single(s => s.Slot.Equals(1)).PokemonUID);
            pkmnpage.Show();
        }
        private void Pkmn_Three_Current_HP_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var pkmnpage =
                new PokemonPage(StaticData.PlayerData.CurrentParty.Single(s => s.Slot.Equals(2)).PokemonUID);
            pkmnpage.Show();
        }
        private void Pkmn_Four_Current_HP_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var pkmnpage =
                new PokemonPage(StaticData.PlayerData.CurrentParty.Single(s => s.Slot.Equals(3)).PokemonUID);
            pkmnpage.Show();
        }
        private void Pkmn_Five_Current_HP_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var pkmnpage =
                new PokemonPage(StaticData.PlayerData.CurrentParty.Single(s => s.Slot.Equals(4)).PokemonUID);
            pkmnpage.Show();
        }
        private void Pkmn_Six_Current_HP_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var pkmnpage =
                new PokemonPage(StaticData.PlayerData.CurrentParty.Single(s => s.Slot.Equals(5)).PokemonUID);
            pkmnpage.Show();
        }
        private void Pkmn_Seven_Current_HP_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var pkmnpage =
                new PokemonPage(StaticData.PlayerData.CurrentParty.Single(s => s.Slot.Equals(6)).PokemonUID);
            pkmnpage.Show();
        }
        private void Pkmn_Eight_Current_HP_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var pkmnpage =
                new PokemonPage(StaticData.PlayerData.CurrentParty.Single(s => s.Slot.Equals(7)).PokemonUID);
            pkmnpage.Show();
        }
    }
}