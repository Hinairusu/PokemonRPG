using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Win32;
using PokemonRPG.Configs;
using PokemonRPG.Windows;
using Xceed.Wpf.Toolkit.Primitives;

namespace PokemonRPG
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool DebugMode = false;
        private bool TrainerEnabled = true;
        private bool PokedexEnabled = true;
        private bool PartyEnabled = true;
        private bool BagEnabled = false;
        private bool GMModeEnabled = true;
        private bool PCEnabled = true;
        private bool EncyclopediaEnabled = false;
        private bool SaveEnabled = false;
        private bool LoadEnabled = false;


        public MainWindow()
        {
            InitializeComponent();
            //LoadBaseData();
            LoadDex.LoadSQLData();
            //LoadTestData();
            DataContext = this;
            BindData();
            
        }

       
        //public void LoadBaseData()
        //{
        //    var FilePath = $"{Environment.CurrentDirectory}\\Resources\\Data\\";
        //    try
        //    {
        //        var dexpath = "Pokedex.xml";

        //        var serializer = new XmlSerializer(typeof(Pokedex));

        //        var reader = new StreamReader($"{FilePath}{dexpath}");
        //        StaticData.ReferenceData.Pokedex = (Pokedex) serializer.Deserialize(reader);
        //        reader.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error loading Pokedex");
        //    } //Pokedex DONE

        //    try
        //    {
        //        var dexpath = "Itemdex.xml";

        //        var serializer = new XmlSerializer(typeof(Itemdex));

        //        var reader = new StreamReader($"{FilePath}{dexpath}");
        //        StaticData.ReferenceData.ItemDex = (Itemdex) serializer.Deserialize(reader);
        //        reader.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error loading Itemdex");
        //    } //Itemdex DONE

        //    try
        //    {
        //        var dexpath = "Naturedex.xml";

        //        var serializer = new XmlSerializer(typeof(Naturedex));

        //        var reader = new StreamReader($"{FilePath}{dexpath}");
        //        StaticData.ReferenceData.NatureDex = (Naturedex) serializer.Deserialize(reader);
        //        reader.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error loading Naturedex");
        //    } //Naturedex DONE

        //    try
        //    {
        //        var dexpath = "Trainerdex.xml";

        //        var serializer = new XmlSerializer(typeof(Trainerdex));

        //        var reader = new StreamReader($"{FilePath}{dexpath}");
        //        StaticData.ReferenceData.TrainerDex = (Trainerdex) serializer.Deserialize(reader);
        //        reader.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error loading Trainerdex");
        //    } //Trainerdex

        //    try
        //    {
        //        var dexpath = "Movedex.xml";

        //        var serializer = new XmlSerializer(typeof(Movedex));

        //        var reader = new StreamReader($"{FilePath}{dexpath}");
        //        StaticData.ReferenceData.MoveDex = (Movedex) serializer.Deserialize(reader);
        //        reader.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error loading Movedex");
        //    } //Movedex DONE

        //    try
        //    {
        //        var dexpath = "Abilitydex.xml";

        //        var serializer = new XmlSerializer(typeof(Abilitydex));

        //        var reader = new StreamReader($"{FilePath}{dexpath}");
        //        StaticData.ReferenceData.AbilityDex = (Abilitydex) serializer.Deserialize(reader);
        //        reader.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error loading Abilitydex");
        //    } //Abilitydex DONE


        //    try
        //    {
        //        var dexpath = "Typedex.xml";

        //        var serializer = new XmlSerializer(typeof(Typedex));

        //        var reader = new StreamReader($"{FilePath}{dexpath}");
        //        StaticData.ReferenceData.TypeDex = (Typedex) serializer.Deserialize(reader);
        //        reader.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error loading Abilitydex");
        //    } //Typedex DONE
        //}

        

        public void BindData()
        {
            DataBinding.BindThis(lbl_Str, StaticData.PlayerData, "Strength");
            DataBinding.BindThis(lbl_Dex, StaticData.PlayerData, "Dexterity");
            DataBinding.BindThis(lbl_Con, StaticData.PlayerData, "Constitution");
            DataBinding.BindThis(lbl_Int, StaticData.PlayerData, "Intelligence");
            DataBinding.BindThis(lbl_Wis, StaticData.PlayerData, "Wisdom");
            DataBinding.BindThis(lbl_Cha, StaticData.PlayerData, "Charisma");
            DataBinding.BindThis(lbl_Name, StaticData.PlayerData, "FirstName");
            DataBinding.BindThis(lbl_Money, StaticData.PlayerData, "Money");
            DataBinding.BindThis(lbl_MaxHP, StaticData.PlayerData, "MaxHP");
            DataBinding.BindThis(lbl_CurrentHP, StaticData.PlayerData, "CurrentHP");
            DataBinding.BindThis(tb_User_Notes, StaticData.PlayerData, "Notes");
            DataBinding.BindThis(tb_Description, StaticData.PlayerData, "Description");
            DataBinding.BindThis(Lb_PokemonTeam, StaticData.PlayerData, "CurrentParty");
        }

        private void btnTest_Click(object sender, RoutedEventArgs e)
        {
            DebugMode = !DebugMode;
        }

        private void btn_Trainer_Click(object sender, MouseButtonEventArgs e)
        {
            if (!TrainerEnabled && !DebugMode) return;
            bool loaded = !(StaticData.PlayerData.UID < 0);
            var Trainer = new TrainerPage(loaded);

            Trainer.Show();
        }

        private void btn_GM_Click(object sender, RoutedEventArgs e)
        {
            if (!GMModeEnabled && !DebugMode) return;
            GMPage gmpage = new GMPage();
            gmpage.Show();
        }

        private void btn_Load_Click(object sender, RoutedEventArgs e)
        {
            if (!LoadEnabled && !DebugMode) return;
            string file;
            var openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            openFileDialog.Filter = "Pkmn Characters (*.PkmnChr)|*.PkmnChr";
            openFileDialog.DefaultExt = ".PkmnChr";
            if (openFileDialog.ShowDialog() == true)
            {
                var serializer = new XmlSerializer(typeof(Player));
                var reader = new StreamReader(openFileDialog.FileName);
                StaticData.PlayerData = (Player) serializer.Deserialize(reader);
                reader.Close();
            }

            BindData();
        }

        private void btn_Save_Click(object sender, RoutedEventArgs e)
        {
            if (!SaveEnabled && !DebugMode) return;
            var xsSubmit = new XmlSerializer(typeof(Player));
            var xml = "";

            using (var sww = new StringWriter())
            {
                using (var writer = XmlWriter.Create(sww))
                {
                    xsSubmit.Serialize(writer, StaticData.PlayerData);
                    xml = sww.ToString(); // Your XML
                }
            }

            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.DefaultExt = ".PkmnChr";
            saveFileDialog.Filter = "Pkmn Characters (*.PkmnChr)|*.PkmnChr";
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (saveFileDialog.ShowDialog() == true)
                File.WriteAllText(saveFileDialog.FileName, xml);
        }

        private void btn_PC_Click(object sender, MouseEventArgs e)
        {
            if (!PCEnabled && !DebugMode) return;
            var pcWindow = new PC();
            pcWindow.Show();
        }

        private void btn_Pokedex_Click(object sender, MouseButtonEventArgs e)
        {
            if (!PokedexEnabled && !DebugMode) return;
            var PkWin = new PokedexWindow();

            PkWin.Show();
        }

        private void btn_Party_Click(object sender, MouseButtonEventArgs e)
        {
            if (!PartyEnabled && !DebugMode) return;
            var party = new PartyWindow();

            party.Show();
        }

        private void btn_Bag_Click(object sender, MouseButtonEventArgs e)
        {
            if (!BagEnabled && !DebugMode) return;
            var Inventory = new Bag();
            
            Inventory.Show();
        }

        private void btn_Encyclopedia_Click(object sender, MouseButtonEventArgs e)
        {
            if (!EncyclopediaEnabled && !DebugMode) return;
            var Lexicon = new Encyclopedia();

            Lexicon.Show();
        }


        private void Window_Closing(object sender, CancelEventArgs e)
        {
            //HANDLE SAVEING HERE
            Environment.Exit(1);
        }

        #region MouseHighlights
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

        private void ButtonHighlightChange(Image image, Image image1)
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

        #endregion

        #region TestData
        //public void LoadTestData()
        //{
        //    StaticData.PlayerData = new Player()
        //    {
        //        Charisma = 16,
        //        Constitution = 13,
        //        CurrentHP = 20,
        //        Description = "A Damn Fool",
        //        CurrentParty = new List<int>(),
        //        Dexterity = 14,
        //        Intelligence = 17,
        //        Inventory = new List<Item>(),
        //        ItemPC = new List<ItemBox>(),
        //        MaxHP = 20,
        //        Strength = 10,
        //        Money = 1337,
        //        //Name = "Joe Doe",
        //        Notes = "Dear god why is he still alive",
        //        OwnedPokemon = new List<TrainerPokemon>(),
        //        Pkmnlist = new ObservableCollection<TrainerPokemon>(),
        //        Wisdom = 6
        //    };

        //    for (int i = 0; i < 30; i++)
        //    {
        //        var pkmn = StaticData.ReferenceData.GenerateTrainerPokemon(
        //            StaticData.ReferenceData.RandomGenerator.Next(0,
        //                StaticData.ReferenceData.Pokedex.PokemonDexList.Count),
        //            StaticData.ReferenceData.RandomGenerator.Next(1, 101));
        //        pkmn.TrainerID = i;
        //        pkmn.PokemonTID = i;
        //        pkmn.Nickname = $"Totally Pikachu #{i}";
        //        StaticData.PlayerData.Pkmnlist.Add(pkmn);
        //    }

        //    while (StaticData.PlayerData.CurrentParty.Count < 6)
        //    {
        //        int Rand = StaticData.ReferenceData.RandomGenerator.Next(0, 30);
        //        if(!StaticData.PlayerData.CurrentParty.Contains(Rand))
        //            StaticData.PlayerData.CurrentParty.Add(Rand);
        //    }

        //}

        
        private void LoadCSV()
        {
            // Legacy block to allow speed input of missing data when it's ready

            var Data = new List<string>();
            using (var sr = new StreamReader("tmmoves.csv"))
            {
                string line;
                while ((line = sr.ReadLine()) != null) Data.Add(line);
            }

            var j = 0;
            foreach (var line in Data)
            {
                var splitvals = line.Split(',');
                try
                {
                    var PokeName = splitvals[0];

                    if (PokeName.Equals("Nidoran (M)"))
                        PokeName = "Nidoran M";
                    else if (PokeName.Equals("Nidoran (F)"))
                        PokeName = "Nidoran F";
                    else if (PokeName.Equals("Mr Mime"))
                        PokeName = "Mr. Mime";
                    else if (PokeName.Equals("Mime Jr"))
                        PokeName = "Mime Jr.";
                    else if (PokeName.Equals("Ryhorn"))
                        PokeName = "Rhyhorn";
                    else if (PokeName.Equals("Wobuffet"))
                        PokeName = "Wobbuffet";
                    else if (PokeName.Equals("Apiom"))
                        PokeName = "Aipom";
                    else if (PokeName.Equals("Farfetche'd"))
                        PokeName = "Farfetch'd";
                    else if (PokeName.Equals("Proygon"))
                        PokeName = "Porygon";
                    else if (PokeName.Equals("Proygon2"))
                        PokeName = "Porygon2";
                    else if (PokeName.Equals("Proygon Z"))
                        PokeName = "Porygon-Z";
                    else if (PokeName.Equals("Tyrnaitar"))
                        PokeName = "Tyranitar";
                    else if (PokeName.Equals("Pickachu"))
                        PokeName = "Pikachu";
                    else if (PokeName.Equals("Slackoth"))
                        PokeName = "Slakoth";


                    //var PokeID =
                    //    StaticData.ReferenceData.Pokedex.PokemonDexList.FindIndex(s =>
                    //        s.Name.Equals(PokeName, StringComparison.OrdinalIgnoreCase));
                    //var MoveID = StaticData.ReferenceData.ItemDex.MoveLearningItems.Single(s =>
                    //    s.MoveUID.ToString().Equals(splitvals[1], StringComparison.OrdinalIgnoreCase));

                    //var MoveNo = StaticData.ReferenceData.MoveDex.MoveList.Single(s => s.Name.Equals(MoveID.MoveName))
                    //    .MoveID;
                  
                    //StaticData.ReferenceData.Pokedex.PokemonDexList[PokeID].PossibleTMMoves.Add(MoveNo);
                }
                catch
                {
                }

                //Thread.Sleep(1);
            }

            var FukedPkmn = string.Empty;
            foreach (var pk in StaticData.ReferenceData.Pokedex.PokemonDexList)
                if (pk.PossibleTMMoves.Count < 1 && pk.EvolutionIDs.Count > 0 && StaticData.ReferenceData.Pokedex
                    .EvolutionList[pk.EvolutionIDs.First()].EvolutionStage == 1)
                    FukedPkmn += $"{pk.Name}, ";

            var BaseFukedPkmn = StaticData.ReferenceData.Pokedex.PokemonDexList
                .Where(pk => pk.PossibleTMMoves.Count < 1)
                .Aggregate(string.Empty,
                    (current,
                        pk) => current + $"{pk.Name}, ");

            var xsSubmit = new XmlSerializer(typeof(Pokedex));
            var xml = "";

            using (var sww = new StringWriter())
            {
                using (var writer = XmlWriter.Create(sww))
                {
                    xsSubmit.Serialize(writer, StaticData.ReferenceData.Pokedex);
                    xml = sww.ToString(); // Your XML
                }
            }
        }

        #endregion

        private void Image_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            BindData();
        }
    }
}