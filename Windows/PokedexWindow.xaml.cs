using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using PokemonRPG.Configs;

namespace PokemonRPG.Windows
{
    /// <summary>
    ///     Interaction logic for PokedexWindow.xaml
    /// </summary>
    public partial class PokedexWindow : Window
    {
        public PokedexWindow()
        {
            InitializeComponent();
            SetAssets();
            ResetValues();
        }


        public DexResults Dex { get; set; } = new DexResults();

        public void SetAssets()
        {
            Background = new ImageBrush(new BitmapImage(
                new Uri($"{Environment.CurrentDirectory}/Resources/Image/Pokedex Images/Pokedex Background.png")));
            MainGrid.Background =
                new ImageBrush(new BitmapImage(
                    new Uri($"{Environment.CurrentDirectory}/Resources/Image/Pokedex Images/Annotations.png")));
            lb_Habitats.Background =
                new ImageBrush(
                    new BitmapImage(new Uri($"{Environment.CurrentDirectory}/Resources/Image/Pokedex Images/Box.png")));
            lb_Capabilities.Background =
                new ImageBrush(
                    new BitmapImage(new Uri($"{Environment.CurrentDirectory}/Resources/Image/Pokedex Images/Box.png")));
            lb_EggGroups.Background =
                new ImageBrush(
                    new BitmapImage(new Uri($"{Environment.CurrentDirectory}/Resources/Image/Pokedex Images/Box.png")));
            lbl_Pokemon.Background =
                new ImageBrush(
                    new BitmapImage(new Uri($"{Environment.CurrentDirectory}/Resources/Image/Pokedex Images/Box.png")));
            tb_PokemonCode.Background =
                new ImageBrush(
                    new BitmapImage(new Uri($"{Environment.CurrentDirectory}/Resources/Image/Pokedex Images/Box.png")));
            btn_DexSearch.Background = new ImageBrush(new BitmapImage(new Uri(
                $"{Environment.CurrentDirectory}/Resources/Image/Pokedex Images/Search Button Highlight.png")));
        }

        public void ResetValues()
        {
            var DefaultTypePath = "Resources/Image/Types/Unknown.png";
            var c = new ImageSourceConverter();
            img_PrimaryType.Source = (ImageSource) c.ConvertFrom(DefaultTypePath);
            img_SecondaryType.Source = (ImageSource) c.ConvertFrom(DefaultTypePath);
            img_Pkmn.Source =
                new BitmapImage(
                    new Uri($"{Environment.CurrentDirectory}/Resources/Image/Pokedex Images/Whose That Pokemon.png"));
            lbl_Stat.Content = "Unknown";
            tbx_Desc.Text = "Unknown";

            img_SecondaryType.Source =
                new BitmapImage(new Uri($"{Environment.CurrentDirectory}/Resources/Image/Types/Unknown.png"));
            lbl_Pokemon.Content = "";
            lbl_Power.Content = 00;
            lbl_Intelligence.Content = 00;
            lbl_Weight.Content = 00;

            lbl_Overland.Content = 00;
            lbl_Sky.Content = 00;
            lbl_Surface.Content = 00;
            lbl_Jump.Content = 00;
            lbl_Burrow.Content = 00;
            lbl_Underwater.Content = 00;
            lb_Habitats.Items.Clear();
            lb_EggGroups.Items.Clear();
            lb_Capabilities.Items.Clear();
        }

        private void btn_DexSearch_Click(object sender, RoutedEventArgs e)
        {
            // FORMAT
            // 3 characters = pokemon code
            // 2 characters = nature
            // 2 characters = Level
            // 1 character = Shiny
            // 2 character = Show moves
            // 1 character = Show Capabilities
            // 1 character = show attitude
            try
            {
                ResetValues();
                FetchDexData();
                if (Dex.StageOne)
                {
                    if (!StaticData.ReferenceData.TypeDex.TypeList[Dex.PokeTarget.PrimaryTypeID].Name
                        .Equals("None", StringComparison.OrdinalIgnoreCase))
                    {
                        var Path =
                            $@"Resources/Image/Types/{StaticData.ReferenceData.TypeDex.TypeList[Dex.PokeTarget.PrimaryTypeID].Name}.png";
                        var c = new ImageSourceConverter();
                        img_PrimaryType.Source = (ImageSource) c.ConvertFrom(Path);
                    }
                    else
                    {
                        img_PrimaryType.Source = null;
                    }
                }

                if (Dex.StageTwo)
                {
                    lbl_Stat.Content = FindHighestStat();
                    tbx_Desc.Text = Dex.PokeTarget.PokedexResult.Description;
                }

                if (Dex.StageThree)
                {
                    if (!StaticData.ReferenceData.TypeDex.TypeList[Dex.PokeTarget.SecondaryTypeID].Name
                        .Equals("None", StringComparison.OrdinalIgnoreCase))
                    {
                        var Path =
                            $@"{Environment.CurrentDirectory}\Resources\Image\Types\{StaticData.ReferenceData.TypeDex.TypeList[Dex.PokeTarget.SecondaryTypeID].Name}.png";
                        var c = new ImageSourceConverter();
                        img_SecondaryType.Source = (ImageSource) c.ConvertFrom(Path);
                    }
                    else
                    {
                        img_SecondaryType.Source = null;
                    }

                    lbl_Pokemon.Content = Dex.PokeTarget.Name;
                    lbl_Power.Content = Dex.PokeTarget.Power;
                    lbl_Intelligence.Content = Dex.PokeTarget.Intellegence;
                    lbl_Weight.Content = Dex.PokeTarget.WeightClass;

                    #region Movements

                    lbl_Overland.Content = Dex.PokeTarget.Movements.Overland;
                    lbl_Sky.Content = Dex.PokeTarget.Movements.Sky;
                    lbl_Surface.Content = Dex.PokeTarget.Movements.Surface;
                    lbl_Jump.Content = Dex.PokeTarget.Movements.Jump;
                    lbl_Burrow.Content = Dex.PokeTarget.Movements.Burrow;
                    lbl_Underwater.Content = Dex.PokeTarget.Movements.Underwater;

                    #endregion

                    foreach (var value in Dex.PokeTarget.Habitats)
                        lb_Habitats.Items.Add(value.Location);

                    foreach (var value in Dex.PokeTarget.EggGroup)
                        lb_EggGroups.Items.Add(value);

                    foreach (var value in Dex.PokeTarget.Capability)
                        lb_Capabilities.Items.Add(StaticData.ReferenceData.AbilityDex.CapabilityList[value].Name);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public string FindHighestStat()
        {
            var Stats = new List<Tuple<string, int>>();

            Stats.Add(Tuple.Create("HP", Dex.PokeTarget.BaseStats.HP));
            Stats.Add(Tuple.Create("Attack", Dex.PokeTarget.BaseStats.Attack));
            Stats.Add(Tuple.Create("Defence", Dex.PokeTarget.BaseStats.Defence));
            Stats.Add(Tuple.Create("Special Attack", Dex.PokeTarget.BaseStats.SpecialAttack));
            Stats.Add(Tuple.Create("Special Defence", Dex.PokeTarget.BaseStats.SpecialDefence));
            Stats.Add(Tuple.Create("Speed", Dex.PokeTarget.BaseStats.Speed));


            Stats = Stats.OrderBy(s => s.Item2).ToList();
            return Stats[0].Item1;
        }

        public void FetchDexData()
        {
            Dex = new DexResults();
            var PokeCode = tb_PokemonCode.Text.ToCharArray().ToList();
            PokeCode.RemoveAll(s => s.Equals('-'));

            if (PokeCode.Count >= 4)
            {
                Dex.StageOne = true;
                for (var i = PokeCode.Count; i < 12; i++)
                    PokeCode.Add('0');
            }

            if (PokeCode.Count >= 8) Dex.StageTwo = true;
            if (PokeCode.Count == 12) Dex.StageThree = true;

            for (var i = PokeCode.Count; i < 12; i++)
                PokeCode.Add('0');

            PokeTextConverter(PokeCode);

            if (!Dex.StageOne)
                throw new Exception("Invalid Code. You should enter a minimum of 4 characters");


            var HexDexNo = $"{PokeCode[0]}{PokeCode[1]}{PokeCode[2]}";
            var DexNo = Convert.ToInt32(HexDexNo, 16);
            if (DexNo > StaticData.ReferenceData.Pokedex.PokemonDexList.Count)
                throw new Exception("Invalid Code");

            Dex.PokeTarget = StaticData.ReferenceData.Pokedex.PokemonDexList[DexNo];

            var NatureDexNo = $"{PokeCode[3]}{PokeCode[4]}";
            var NatureNo = Convert.ToInt32(NatureDexNo, 16);
            if (NatureNo > StaticData.ReferenceData.NatureDex.Natures.Count)
                throw new Exception("Invalid Code");

            Dex.Nature = StaticData.ReferenceData.NatureDex.Natures[NatureNo];

            var LevelNo = $"{PokeCode[5]}{PokeCode[6]}";
            var Level = Convert.ToInt32(LevelNo, 16);
            if (Level > 201)
                throw new Exception("Invalid Code");

            if (Level < 101)
            {
                Dex.Level = Level;
                Dex.ShowLevel = true;
            }
            else
            {
                Dex.Level = Level - 100;
            }

            if (PokeCode[7].Equals(1))
                Dex.Shiny = true;

            var MoveDexNo = $"{PokeCode[8]}{PokeCode[9]}";
            var MoveNo = Convert.ToInt32(MoveDexNo, 16);
            if (MoveNo > 0)
            {
                var mvlist = new List<LevelMoves>();
                foreach (var move in Dex.PokeTarget.PossibleLevelupMoves)
                    if (move.LevelLearned <= Dex.Level)
                        mvlist.Add(move);
                foreach (var move in mvlist)
                    if (Dex.MoveList.Count < MoveNo)
                        Dex.MoveList.Add(StaticData.ReferenceData.MoveDex.MoveList[move.MoveID]);
            }

            var CapabilityNo = $"{PokeCode[10]}";
            var Cap = Convert.ToInt32(CapabilityNo, 16);
            if (Cap > 0)
                Dex.Capability = true;

            var AttitudeNo = $"{PokeCode[11]}";
            var Attitude = Convert.ToInt32(AttitudeNo, 16);
            if (Attitude > 0)
                Dex.Attitude = Attitude;
        }

        private void PokeTextConverter(List<char> pokeCode)
        {
            var PokeText = new StringBuilder();
            PokeText.Append($"{pokeCode[0]}");
            PokeText.Append($"{pokeCode[1]}");
            PokeText.Append($"{pokeCode[2]}");
            PokeText.Append($"{pokeCode[3]}");
            PokeText.Append("-");
            PokeText.Append($"{pokeCode[4]}");
            PokeText.Append($"{pokeCode[5]}");
            PokeText.Append($"{pokeCode[6]}");
            PokeText.Append($"{pokeCode[7]}");
            PokeText.Append("-");
            PokeText.Append($"{pokeCode[8]}");
            PokeText.Append($"{pokeCode[9]}");
            PokeText.Append($"{pokeCode[10]}");
            PokeText.Append($"{pokeCode[11]}");
            tb_PokemonCode.Text = PokeText.ToString();
        }

        private void btn_StyleToggle_Click(object sender, MouseButtonEventArgs e)
        {
            var WindowStyle = new Style(typeof(Label));
            var setter = new Setter(ForegroundProperty, Brushes.Black);
            WindowStyle.Setters.Add(setter);
            //this.Style = WindowStyle;
            Resources.Clear();
            Resources.Add("Label", WindowStyle);

            tb_PokemonCode.Foreground = Brushes.Black;
            lb_Capabilities.Foreground = Brushes.Black;
            lb_EggGroups.Foreground = Brushes.Black;
            lb_Habitats.Foreground = Brushes.Black;
            tbx_Desc.Foreground = Brushes.Black;
            lbl_PokemonCodeRequest.Foreground = Brushes.Black;
        }
    }
}