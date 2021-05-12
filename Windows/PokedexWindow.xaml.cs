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
    /// Interaction logic for PokedexWindow.xaml
    /// </summary>
    public partial class PokedexWindow : Window
    {
        public PokedexWindow(MasterReferenceClass MRef, Player PRef)
        {
            InitializeComponent();
            ReferenceData = MRef;
            PlayerData = PRef;

        }

        public MasterReferenceClass ReferenceData { get; set; }
        public Player PlayerData { get; set; }

        public DexResults Dex { get; set; } = new DexResults();

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
                FetchDexData();
                if (Dex.StageOne)
                    lbl_Type.Content = ReferenceData.TypeDex.TypeList[Dex.PokeTarget.PrimaryTypeID].Name;
                if (Dex.StageTwo)
                {
                    lbl_Stat.Content = FindHighestStat();
                    lbl_Desc.Content = Dex.PokeTarget.PokedexResult.Description;
                }
                if (Dex.StageThree)
                    lbl_Pokemon.Content = Dex.PokeTarget.Name;

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

            if (PokeCode.Count == 4)
            {
                Dex.StageOne = true;
                for (int i = PokeCode.Count; i < 12; i++)
                    PokeCode.Add('0');
            }
            else if (PokeCode.Count == 8)
            {
                Dex.StageOne = true;
                Dex.StageTwo = true;
                for (int i = PokeCode.Count; i < 12; i++)
                    PokeCode.Add('0');
            }
            else if (PokeCode.Count == 12)
            {
                Dex.StageOne = true;
                Dex.StageTwo = true;
                Dex.StageThree = true;
            }

            if (PokeCode.Count != 12)
                throw new Exception("Invalid Code. You should enter 12 characters");

            

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
