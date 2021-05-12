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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Serialization;
using PokemonRPG.Configs;

namespace PokemonRPG
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {

            InitializeComponent();
            LoadBaseData();
        }

        MasterReferenceClass ReferenceData = new MasterReferenceClass();

        public void LoadBaseData()
        {
            string FilePath = $"{Environment.CurrentDirectory}\\Resources\\Data\\";
            try
            {
                string dexpath = "Pokedex.xml";

                XmlSerializer serializer = new XmlSerializer(typeof(Pokedex));

                StreamReader reader = new StreamReader($"{FilePath}{dexpath}");
                ReferenceData.Pokedex = (Pokedex)serializer.Deserialize(reader);
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading Pokedex");
            }

            try
            {
                string dexpath = "Itemdex.xml";

                XmlSerializer serializer = new XmlSerializer(typeof(Itemdex));

                StreamReader reader = new StreamReader($"{FilePath}{dexpath}");
                ReferenceData.ItemDex = (Itemdex)serializer.Deserialize(reader);
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading Itemdex");
            }

            try
            {
                string dexpath = "Naturedex.xml";

                XmlSerializer serializer = new XmlSerializer(typeof(Naturedex));

                StreamReader reader = new StreamReader($"{FilePath}{dexpath}");
                ReferenceData.NatureDex = (Naturedex)serializer.Deserialize(reader);
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading Naturedex");
            }

            try
            {
                string dexpath = "Trainerdex.xml";

                XmlSerializer serializer = new XmlSerializer(typeof(Trainerdex));

                StreamReader reader = new StreamReader($"{FilePath}{dexpath}");
                ReferenceData.TrainerDex = (Trainerdex)serializer.Deserialize(reader);
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading Trainerdex");
            }

            try
            {
                string dexpath = "Movedex.xml";

                XmlSerializer serializer = new XmlSerializer(typeof(Movedex));

                StreamReader reader = new StreamReader($"{FilePath}{dexpath}");
                ReferenceData.MoveDex = (Movedex)serializer.Deserialize(reader);
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading Movedex");
            }

            try
            {
                string dexpath = "Abilitydex.xml";

                XmlSerializer serializer = new XmlSerializer(typeof(Abilitydex));

                StreamReader reader = new StreamReader($"{FilePath}{dexpath}");
                ReferenceData.AbilityDex = (Abilitydex)serializer.Deserialize(reader);
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading Abilitydex");
            }


            try
            {
                string dexpath = "Typedex.xml";

                XmlSerializer serializer = new XmlSerializer(typeof(Typedex));

                StreamReader reader = new StreamReader($"{FilePath}{dexpath}");
                ReferenceData.TypeDex = (Typedex)serializer.Deserialize(reader);
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading Abilitydex");
            }
        }


        
        private void LoadCSV()
        {



            List<string> Data = new List<string>();
            using (var sr = new StreamReader("pkmnCapabilities.csv"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    Data.Add(line);
                }
            }

            int j = 0;
            foreach (string line in Data)
            {
                string[] splitvals = line.Split(',');

               
 
            }

        }


        private void btnTest_Click(object sender, RoutedEventArgs e)
        {
            var basepk = new WildPokemon();
            var Trainerpk = new TrainerPokemon();


        }

        private void btn_Load_Click(object sender, RoutedEventArgs e)
        {

        }

        public Player DoWork()
        {
            return null;
        }




        private void btn_Save_Click(object sender, RoutedEventArgs e)
        {
             var foo = DoWork();

            XmlSerializer xsSubmit = new XmlSerializer(typeof(Player));
            var xml = "";

            using (var sww = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sww))
                {
                    xsSubmit.Serialize(writer, foo);
                    xml = sww.ToString(); // Your XML
                }
            }
        }


        private void btn_Trainer_MouseWork(object sender, MouseEventArgs e)
        {
            ButtonHighlightChange(btn_Trainer, btn_TrainerHighlight);
        }

        private void btn_Pokedex_MouseWork(object sender, MouseEventArgs e)
        {
            ButtonHighlightChange(btn_Pokedex, btn_PokedexHighlight);
        }

        private void btn_PC_MouseWork(object sender, MouseEventArgs e)
        {
            ButtonHighlightChange(btn_PC, btn_PCHighlight);
        }

        private void btn_Party_MouseWork(object sender, MouseEventArgs e)
        {
            ButtonHighlightChange(btn_Party, btn_PartyHighlight);
        }

        private void ButtonHighlightChange (Image image, Image image1)
        {
            if (image.Visibility == Visibility.Visible)
            {
                image.Visibility = Visibility.Hidden;
                image1.Visibility = Visibility.Visible;
            }
            else
            {
                image.Visibility = Visibility.Visible;
                image1.Visibility = Visibility.Hidden;
            }
        }

        private void btn_Encyclopedia_MouseWork(object sender, MouseEventArgs e)
        {
            ButtonHighlightChange(btn_Encyclopedia, btn_EncyclopediaHighlight);
        }

        private void btn_Bag_MouseWork(object sender, MouseEventArgs e)
        {
            ButtonHighlightChange(btn_Bag, btn_BagHighlight);
        }

        private void btn_Load_MouseWork(object sender, MouseEventArgs e)
        {
            ButtonHighlightChange(btn_Load, btn_LoadHighlight);
        }

        private void btn_Save_MouseWork(object sender, MouseEventArgs e)
        {
            ButtonHighlightChange(btn_Save, btn_SaveHighlight);
        }
    }
}
