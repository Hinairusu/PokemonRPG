using PokemonRPG.Configs;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for PokedexWindow.xaml
    /// </summary>
    public partial class PokedexWindow : Window
    {
        public PokedexWindow(MasterReferenceClass MRef, Player PRef)
        {
            InitializeComponent();
            SetAssets();
            ReferenceData = MRef;
            PlayerData = PRef;
            ResetValues();

           
        }

       public void SetAssets()
        {
            this.Background = new ImageBrush(new BitmapImage(new Uri($"{Environment.CurrentDirectory}/Resources/Image/Pokedex Images/Pokedex Background.png")));
            this.MainGrid.Background = new ImageBrush(new BitmapImage(new Uri($"{Environment.CurrentDirectory}/Resources/Image/Pokedex Images/Annotations.png")));
            this.lb_Habitats.Background = new ImageBrush(new BitmapImage(new Uri($"{Environment.CurrentDirectory}/Resources/Image/Pokedex Images/Box.png")));
            this.lb_Capabilities.Background = new ImageBrush(new BitmapImage(new Uri($"{Environment.CurrentDirectory}/Resources/Image/Pokedex Images/Box.png")));
            this.lb_EggGroups.Background = new ImageBrush(new BitmapImage(new Uri($"{Environment.CurrentDirectory}/Resources/Image/Pokedex Images/Box.png")));
            this.lbl_Pokemon.Background = new ImageBrush(new BitmapImage(new Uri($"{Environment.CurrentDirectory}/Resources/Image/Pokedex Images/Box.png")));
            this.tb_PokemonCode.Background = new ImageBrush(new BitmapImage(new Uri($"{Environment.CurrentDirectory}/Resources/Image/Pokedex Images/Box.png")));
            this.btn_DexSearch.Background = new ImageBrush(new BitmapImage(new Uri($"{Environment.CurrentDirectory}/Resources/Image/Pokedex Images/Search Button Highlight.png")));
        }

        public MasterReferenceClass ReferenceData { get; set; }
        public Player PlayerData { get; set; }

        public DexResults Dex { get; set; } = new DexResults();
        public void ResetValues()
        {

            string DefaultTypePath = "Resources/Image/Types/Unknown.png";
            ImageSourceConverter c = new ImageSourceConverter();
            img_PrimaryType.Source = (ImageSource)c.ConvertFrom(DefaultTypePath);
            img_SecondaryType.Source = (ImageSource)c.ConvertFrom(DefaultTypePath);
            img_Pkmn.Source = new BitmapImage(new Uri($"{Environment.CurrentDirectory}/Resources/Image/Pokedex Images/Whose That Pokemon.png"));
            lbl_Stat.Content = "Unknown";
            tbx_Desc.Text = "Unknown";

            img_SecondaryType.Source = new BitmapImage(new Uri($"{Environment.CurrentDirectory}/Resources/Image/Types/Unknown.png"));
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
                    if (!ReferenceData.TypeDex.TypeList[Dex.PokeTarget.PrimaryTypeID].Name.Equals("None", StringComparison.OrdinalIgnoreCase))
                    {
                        string Path = $@"Resources/Image/Types/{ReferenceData.TypeDex.TypeList[Dex.PokeTarget.PrimaryTypeID].Name}.png";
                        ImageSourceConverter c = new ImageSourceConverter();
                        img_PrimaryType.Source = (ImageSource)c.ConvertFrom(Path);
                    }
                    else
                        img_PrimaryType.Source = null;
                }
                if (Dex.StageTwo)
                {
                    lbl_Stat.Content = FindHighestStat();
                    tbx_Desc.Text = Dex.PokeTarget.PokedexResult.Description;
                }
                if (Dex.StageThree)
                {
                    if (!ReferenceData.TypeDex.TypeList[Dex.PokeTarget.SecondaryTypeID].Name.Equals("None", StringComparison.OrdinalIgnoreCase))
                    {
                        string Path = $@"{Environment.CurrentDirectory}\Resources\Image\Types\{ReferenceData.TypeDex.TypeList[Dex.PokeTarget.SecondaryTypeID].Name}.png";
                        ImageSourceConverter c = new ImageSourceConverter();
                        img_SecondaryType.Source = (ImageSource)c.ConvertFrom(Path);
                    }
                    else
                        img_SecondaryType.Source = null;

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
                        lb_Capabilities.Items.Add(ReferenceData.AbilityDex.CapabilityList[value].Name);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public string FindHighestStat()
        {
            List<Tuple<string, int>> Stats = new List<Tuple<string, int>>();

            Stats.Add(Tuple.Create<string, int>("HP", Dex.PokeTarget.BaseStats.HP));
            Stats.Add(Tuple.Create<string, int>("Attack", Dex.PokeTarget.BaseStats.Attack));
            Stats.Add(Tuple.Create<string, int>("Defence", Dex.PokeTarget.BaseStats.Defence));
            Stats.Add(Tuple.Create<string, int>("Special Attack", Dex.PokeTarget.BaseStats.SpecialAttack));
            Stats.Add(Tuple.Create<string, int>("Special Defence", Dex.PokeTarget.BaseStats.SpecialDefence));
            Stats.Add(Tuple.Create<string, int>("Speed", Dex.PokeTarget.BaseStats.Speed));


            Stats = Stats.OrderBy(s => s.Item2).ToList();
            return Stats[0].Item1;
        }

        public void FetchDexData()
        {
            Dex = new DexResults();
            List<char> PokeCode = tb_PokemonCode.Text.ToCharArray().ToList();
            PokeCode.RemoveAll(s => s.Equals('-'));

            if (PokeCode.Count >= 4)
            {
                Dex.StageOne = true;
                for (int i = PokeCode.Count; i < 12; i++)
                    PokeCode.Add('0');
            }
            if (PokeCode.Count >= 8)
            {
                Dex.StageTwo = true;
                
            }
            if (PokeCode.Count == 12)
            {
                Dex.StageThree = true;
            }

            for (int i = PokeCode.Count; i < 12; i++)
                    PokeCode.Add('0');

            PokeTextConverter(PokeCode);

            if (!Dex.StageOne)
                throw new Exception("Invalid Code. You should enter a minimum of 4 characters");

            

            string HexDexNo = $"{PokeCode[0]}{PokeCode[1]}{PokeCode[2]}";
            int DexNo = Convert.ToInt32(HexDexNo, 16);
            if (DexNo > ReferenceData.Pokedex.PokemonDexList.Count)
                throw new Exception("Invalid Code");

            Dex.PokeTarget = ReferenceData.Pokedex.PokemonDexList[DexNo];

            string NatureDexNo = $"{PokeCode[3]}{PokeCode[4]}";
            int NatureNo = Convert.ToInt32(NatureDexNo, 16);
            if (NatureNo > ReferenceData.NatureDex.Natures.Count)
                throw new Exception("Invalid Code");

            Dex.Nature = ReferenceData.NatureDex.Natures[NatureNo];

            string LevelNo = $"{PokeCode[5]}{PokeCode[6]}";
            int Level = Convert.ToInt32(LevelNo, 16);
            if (Level > 201)
                throw new Exception("Invalid Code");

            if (Level < 101)
            { Dex.Level = Level; Dex.ShowLevel = true; }
            else
                Dex.Level = Level - 100;

            if (PokeCode[7].Equals(1))
                Dex.Shiny = true;

            string MoveDexNo = $"{PokeCode[8]}{PokeCode[9]}";
            int MoveNo = Convert.ToInt32(MoveDexNo, 16);
            if (MoveNo > 0)
            {
                List<LevelMoves> mvlist = new List<LevelMoves>();
                foreach (var move in Dex.PokeTarget.PossibleLevelupMoves)
                {
                    if (move.LevelLearned <= Dex.Level)
                        mvlist.Add(move);
                }
                foreach (var move in mvlist)
                {
                    if (Dex.MoveList.Count < MoveNo)
                        Dex.MoveList.Add(ReferenceData.MoveDex.MoveList[move.MoveID]);
                }
            }

            string CapabilityNo = $"{PokeCode[10]}";
            int Cap = Convert.ToInt32(CapabilityNo, 16);
            if (Cap > 0)
                Dex.Capability = true;

            string AttitudeNo = $"{PokeCode[11]}";
            int Attitude = Convert.ToInt32(AttitudeNo, 16);
            if (Attitude > 0)
                Dex.Attitude = Attitude;
        }

        private void PokeTextConverter(List<char> pokeCode)
        {
            StringBuilder PokeText = new StringBuilder();
            PokeText.Append($"{pokeCode[0]}");
            PokeText.Append($"{pokeCode[1]}");
            PokeText.Append($"{pokeCode[2]}");
            PokeText.Append($"{pokeCode[3]}");
            PokeText.Append($"-");
            PokeText.Append($"{pokeCode[4]}");
            PokeText.Append($"{pokeCode[5]}");
            PokeText.Append($"{pokeCode[6]}");
            PokeText.Append($"{pokeCode[7]}");
            PokeText.Append($"-");
            PokeText.Append($"{pokeCode[8]}");
            PokeText.Append($"{pokeCode[9]}");
            PokeText.Append($"{pokeCode[10]}");
            PokeText.Append($"{pokeCode[11]}");
            tb_PokemonCode.Text = PokeText.ToString();
        }

        private void btn_StyleToggle_Click(object sender, MouseButtonEventArgs e)
        {
            Style WindowStyle = new Style(typeof(Label));
            Setter setter = new Setter(Label.ForegroundProperty, Brushes.Black);
            WindowStyle.Setters.Add(setter);
            //this.Style = WindowStyle;
            this.Resources.Clear();
            this.Resources.Add("Label", WindowStyle);

            tb_PokemonCode.Foreground = Brushes.Black;
            lb_Capabilities.Foreground = Brushes.Black;
            lb_EggGroups.Foreground = Brushes.Black;
            lb_Habitats.Foreground = Brushes.Black;
            tbx_Desc.Foreground = Brushes.Black;
            lbl_PokemonCodeRequest.Foreground = Brushes.Black;
        }
    }

    public class DexResults
    {

        public bool StageOne { get; set; } // First Round, Display Primary Type only
        public bool StageTwo { get; set; } // Second Round, Description, Highest Base Stat?
        public bool StageThree { get; set; }
        public Pokemon PokeTarget { get; set; }
        public InherentNature Nature { get; set; }
        public int Level { get; set; }
        public bool ShowLevel { get; set; }
        public bool Shiny { get; set; }
        public List<PokemonMove> MoveList { get; set; }
        public bool Capability { get; set; }
        public int Attitude { get; set; }
        public DexResults()
        {
            PokeTarget = new Pokemon();
            Nature = new InherentNature();
            MoveList = new List<PokemonMove>();
        }
    }

}
