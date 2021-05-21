﻿using System;
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

        MasterReferenceClass ReferenceData { get; set; } = new MasterReferenceClass();
        Player PlayerData { get; set; } = new Player();
        
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

        public void LoadTestData()
        {
            PlayerData = new Player()
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
            TrainerPokemon Starter = ReferenceData.GenerateTrainerPokemon(212);
            Starter.Nickname = "The IV Bag";
            Starter.Level = 25;
            Starter.Sex = new Gender() { Female = true };

            PlayerData.CurrentParty.Add(Starter);
            PlayerData.TotalOwnedPokemon = 1;
            PlayerData.Money = 9001;
            PlayerData.MaxHP = 10;
            PlayerData.CurrentHP = 1;

            foreach (var pkmn in PlayerData.CurrentParty)
                PlayerData.Pkmnlist.Add(pkmn);
        }

        public void BindData()
        {
            DataBinding.BindThis(lbl_Str, PlayerData, "Strength");
            DataBinding.BindThis(lbl_Dex, PlayerData, "Dexterity");
            DataBinding.BindThis(lbl_Con, PlayerData, "Constitution");
            DataBinding.BindThis(lbl_Int, PlayerData, "Intelligence");
            DataBinding.BindThis(lbl_Wis, PlayerData, "Wisdom");
            DataBinding.BindThis(lbl_Cha, PlayerData, "Charisma");
            DataBinding.BindThis(lbl_Name, PlayerData, "Name");
            DataBinding.BindThis(lbl_Money, PlayerData, "Money");
            DataBinding.BindThis(lbl_MaxHP, PlayerData, "MaxHP");
            DataBinding.BindThis(lbl_CurrentHP, PlayerData, "CurrentHP");
            DataBinding.BindThis(tb_User_Notes, PlayerData, "Notes");
            DataBinding.BindThis(tb_Description, PlayerData, "Description");
            DataBinding.BindThis(Lb_PokemonTeam, PlayerData, "Pkmnlist");
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
            string file;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            openFileDialog.Filter = "Pkmn Characters (*.PkmnChr)|*.PkmnChr";
            openFileDialog.DefaultExt = ".PkmnChr";
            if (openFileDialog.ShowDialog() == true)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Player));
                StreamReader reader = new StreamReader(openFileDialog.FileName);
                PlayerData = (Player)serializer.Deserialize(reader);
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
                    xsSubmit.Serialize(writer, PlayerData);
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
            PokedexWindow PkWin = new PokedexWindow(ReferenceData,PlayerData);

            PkWin.Show();
        }
    }
}
