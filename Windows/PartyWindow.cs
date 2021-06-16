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
        public PartyWindow(MasterReferenceClass MRef, Player PRef)
        {
            InitializeComponent();
            ReferenceData = MRef;
            PlayerData = PRef;
            LoadChart();
        }

        public MasterReferenceClass ReferenceData { get; set; }
        public Player PlayerData { get; set; }

        public void LoadChart()
        {
            //((BarSeries)Chart_Pkmn_One.Series[0]).ItemsSource = new KeyValuePair<string, int>[] { new KeyValuePair<string, int>("Project Manager", 12) };
        }
    }
}
