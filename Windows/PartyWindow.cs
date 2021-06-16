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
            SetAssets();
        }

       private void SetAssets()
        {
            string DefaultTypePath = "Resources/Image/Types/Unknown.png";
            ImageSourceConverter c = new ImageSourceConverter();
            Pkmn_One_Primary_Type.Source = (ImageSource)c.ConvertFrom(DefaultTypePath);
            Pkmn_One_Secondary_Type.Source = (ImageSource)c.ConvertFrom(DefaultTypePath);
        }

        public MasterReferenceClass ReferenceData { get; set; }
        public Player PlayerData { get; set; }

 
    }
}
