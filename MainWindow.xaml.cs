using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Microsoft.Win32;
using PokemonRPG.Configs;
using PokemonRPG.Windows;

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

#if DEBUG
            LoadTestData();
#endif
            DataContext = this;
            BindData();
        }
        public void LoadBaseData()
        {
            string FilePath = $"{Environment.CurrentDirectory}\\Resources\\Data\\";
            try
            {
                string dexpath = "Pokedex.xml";

                XmlSerializer serializer = new XmlSerializer(typeof(Pokedex));

                StreamReader reader = new StreamReader($"{FilePath}{dexpath}");
                StaticData.ReferenceData.Pokedex = (Pokedex)serializer.Deserialize(reader);
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
                StaticData.ReferenceData.ItemDex = (Itemdex)serializer.Deserialize(reader);
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
                StaticData.ReferenceData.NatureDex = (Naturedex)serializer.Deserialize(reader);
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
                StaticData.ReferenceData.TrainerDex = (Trainerdex)serializer.Deserialize(reader);
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
                StaticData.ReferenceData.MoveDex = (Movedex)serializer.Deserialize(reader);
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
                StaticData.ReferenceData.AbilityDex = (Abilitydex)serializer.Deserialize(reader);
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
                StaticData.ReferenceData.TypeDex = (Typedex)serializer.Deserialize(reader);
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading Abilitydex");
            }
        }

        public void LoadTestData()
        {
            StaticData.PlayerData = new Player()
            {
                Name = "Joe Bloggs",
                Strength = 12,
                Intelligence = 15,
                Dexterity = 13,
                Constitution = 11,
                Wisdom = 6,
                Charisma = 16,
                Description = "A Partygoing mad scientist",
                Notes = "Loves the Macarena"
            };
            TrainerPokemon Starter = StaticData.ReferenceData.GenerateTrainerPokemon(212);
            Starter.Nickname = "The IV Bag";
            Starter.Level = 25;
            Starter.Sex = new Gender() { Female = true };
            Starter.Enhancements.Defence = 100;
            Starter.Enhancements.SpecialDefence = 100;
            Starter.Enhancements.Attack = 12;
            Starter.Enhancements.SpecialAttack = 12;
            Starter.Enhancements.Speed = 35;
            Starter.Enhancements.HP = 80;

            StaticData.PlayerData.CurrentParty.Add(Starter);
            StaticData.PlayerData.TotalOwnedPokemon = 1;
            StaticData.PlayerData.Money = 9001;
            StaticData.PlayerData.MaxHP = 10;
            StaticData.PlayerData.CurrentHP = 1;

            foreach (var pkmn in StaticData.PlayerData.CurrentParty)
                StaticData.PlayerData.Pkmnlist.Add(pkmn);
        }

        public void BindData()
        {
            DataBinding.BindThis(lbl_Str, StaticData.PlayerData, "Strength");
            DataBinding.BindThis(lbl_Dex, StaticData.PlayerData, "Dexterity");
            DataBinding.BindThis(lbl_Con, StaticData.PlayerData, "Constitution");
            DataBinding.BindThis(lbl_Int, StaticData.PlayerData, "Intelligence");
            DataBinding.BindThis(lbl_Wis, StaticData.PlayerData, "Wisdom");
            DataBinding.BindThis(lbl_Cha, StaticData.PlayerData, "Charisma");
            DataBinding.BindThis(lbl_Name, StaticData.PlayerData, "Name");
            DataBinding.BindThis(lbl_Money, StaticData.PlayerData, "Money");
            DataBinding.BindThis(lbl_MaxHP, StaticData.PlayerData, "MaxHP");
            DataBinding.BindThis(lbl_CurrentHP, StaticData.PlayerData, "CurrentHP");
            DataBinding.BindThis(tb_User_Notes, StaticData.PlayerData, "Notes");
            DataBinding.BindThis(tb_Description, StaticData.PlayerData, "Description");
            DataBinding.BindThis(Lb_PokemonTeam, StaticData.PlayerData, "Pkmnlist");
        }
        
        private void LoadCSV()
        {

            // Legacy block to allow speed input of missing data when it's ready

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

            StaticData.PlayerData.CurrentParty[0].ActualStats = StaticData.PlayerData.CurrentParty[0].BaseStats + StaticData.PlayerData.CurrentParty[0].Enhancements + StaticData.PlayerData.CurrentParty[0].IVs + StaticData.PlayerData.CurrentParty[0].EVs;

        }

        private void btn_Load_Click(object sender, RoutedEventArgs e)
        {
            string file;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            openFileDialog.Filter = "Pkmn Characters (*.PkmnChr)|*.PkmnChr";
            openFileDialog.DefaultExt = ".PkmnChr";
            if (openFileDialog.ShowDialog() == true)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Player));
                StreamReader reader = new StreamReader(openFileDialog.FileName);
                StaticData.PlayerData = (Player)serializer.Deserialize(reader);
                reader.Close();
            }
            BindData();
        }


        private void btn_Save_Click(object sender, RoutedEventArgs e)
        {

            XmlSerializer xsSubmit = new XmlSerializer(typeof(Player));
            var xml = "";

            using (var sww = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sww))
                {
                    xsSubmit.Serialize(writer, StaticData.PlayerData);
                    xml = sww.ToString(); // Your XML
                }
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.DefaultExt = ".PkmnChr";
            saveFileDialog.Filter = "Pkmn Characters (*.PkmnChr)|*.PkmnChr";
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (saveFileDialog.ShowDialog() == true)
                File.WriteAllText(saveFileDialog.FileName, xml);
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
        private void btn_PC_Click(object sender, MouseEventArgs e)
        {
            PC pcWindow = new PC(StaticData.ReferenceData,StaticData.PlayerData);
            pcWindow.Show();
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

        private void btn_GM_MouseWork(object sender, MouseEventArgs e)
        {
            ButtonHighlightChange(btn_GM, btn_GMHighlight);
        }

        private void btn_PokedexHighlight_Click(object sender, MouseButtonEventArgs e)
        {
            PokedexWindow PkWin = new PokedexWindow();

            PkWin.Show();
        }

        private void btn_Party_Click(object sender, MouseButtonEventArgs e)
        {
            PartyWindow party = new PartyWindow();

            party.Show();
        }
}
}
