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
        private bool PCEnabled = false;
        private bool EncyclopediaEnabled = false;
        private bool SaveEnabled = false;
        private bool LoadEnabled = false;

        public static string PokemonDB = ConfigurationManager.AppSettings["PokemonDatabase"];
        public static string TrainerDB = ConfigurationManager.AppSettings["PlayerDatabase"];
        public static string pCnxn = SQLData.GenerateSQLConString(PokemonDB);
        public static string tCnxn = SQLData.GenerateSQLConString(TrainerDB);
        public MainWindow()
        {
            InitializeComponent();
            //LoadBaseData();
            LoadSQLData();
            //LoadTestData();
            DataContext = this;
            BindData();
            
        }

        public void LoadSQLData()
        {
            

            #region Typedex

            #region Basic Types
            try
            {
                DataTable typeTable = SQLData.DatatableFill(PokemonDB,FixedData.TypeList, pCnxn);
                DataTable interactionTable = SQLData.DatatableFill(PokemonDB,FixedData.TypeInteractions, pCnxn);

                foreach (DataRow typeRow in typeTable.Rows)
                {
                    PokeType type = new PokeType();
                    type.Name = (string) typeRow["Name"];
                    type.UID = (int) typeRow["UID"];
                    
                    foreach (var interaction in interactionTable.Select($"[CurrentTypeID] = {type.UID}"))
                    {
                        TypeMultiplier Multiplier = new TypeMultiplier();
                        Multiplier.TypeID = (int) interaction["TargetTypeID"];
                        Multiplier.Multiplier = (decimal) interaction["Multiplier"];
                        type.TypeInteraction.Add(Multiplier);
                    }
                    StaticData.ReferenceData.TypeDex.TypeList.Add(type);
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Type Dex threw exception: {Environment.NewLine}{Environment.NewLine}{ex}");
            }

            #endregion

            #region Contest Types
            try
            {
                DataTable contestTypeTable = SQLData.DatatableFill(PokemonDB,FixedData.ContestTypes, pCnxn);

                foreach (DataRow row in contestTypeTable.Rows)
                {
                    ContestType value = new ContestType();
                    value.Name = (string)row["Name"];
                    value.UID = (int) row["UID"];

                    StaticData.ReferenceData.TypeDex.ContestTypeList.Add(value);
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Contest Type Dex threw exception: {Environment.NewLine}{Environment.NewLine}{ex}");
            }
            #endregion

            #endregion

            #region Naturedex

            #region Flavours
            try
            {
                DataTable FlavourTable = SQLData.DatatableFill(PokemonDB, FixedData.FlavourList, pCnxn);

                foreach (DataRow flavourRow in FlavourTable.Rows)
                {
                    Flavours flavour = new Flavours();
                    flavour.Flavour = (string) flavourRow["Name"];
                    flavour.UID = (int) flavourRow["UID"];
                    StaticData.ReferenceData.NatureDex.Flavours.Add(flavour);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Flavour Dex threw exception: {Environment.NewLine}{Environment.NewLine}{ex}");
            }
            #endregion

            #region Natures
            try
            {
                DataTable natureTable = SQLData.DatatableFill(PokemonDB,FixedData.NatureList, pCnxn);
                

                foreach (DataRow row in natureTable.Rows)
                {
                    InherentNature nature = new InherentNature();
                    nature.Name = (string) row["Name"];
                    nature.UID = (int) row["UID"];
                    nature.Attack = (int) row["Attack"];
                    nature.SpecialAttack = (int) row["Special Attack"];
                    nature.Defence = (int) row["Defence"];
                    nature.SpecialDefence = (int) row["Special Defence"];
                    nature.Speed = (int) row["Speed"];
                    nature.HP = (int) row["HP"];
                    nature.FlavourHate = StaticData.ReferenceData.NatureDex.Flavours
                        .Single(s => s.UID.Equals(row["Flavour Hate"])).Flavour;
                    nature.FlavourLike = StaticData.ReferenceData.NatureDex.Flavours
                        .Single(s => s.UID.Equals(row["Flavour Like"])).Flavour;
                    
                    
                    StaticData.ReferenceData.NatureDex.Natures.Add(nature);
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Nature Dex threw exception: {Environment.NewLine}{Environment.NewLine}{ex}");
            }
            #endregion

            #region Habitats
            
            try
            {
                DataTable habitatsTable = SQLData.DatatableFill(PokemonDB,FixedData.EnvironmentList, pCnxn);

                foreach (DataRow row in habitatsTable.Rows)
                {
                    PkEnvironment value = new PkEnvironment();
                    value.Name = (string)row["Name"];
                    if (!row.IsNull("Notes"))
                        value.Notes = (string)row["Notes"];
                    value.UID = (int) row["UID"];

                    StaticData.ReferenceData.NatureDex.Enviroments.Add(value);
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Environment Dex threw exception: {Environment.NewLine}{Environment.NewLine}{ex}");
            }
            #endregion

            #region Egg Groups
            try
            {
                DataTable eggListTable = SQLData.DatatableFill(PokemonDB,FixedData.EggList, pCnxn);
                foreach (DataRow env in eggListTable.Rows)
                {
                    EggGroup egg = new EggGroup();
                    egg.Name = (string) env["Name"];

                    egg.UID = (int) env["UID"];
                    StaticData.ReferenceData.NatureDex.EggGroups.Add(egg);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Egg Dex threw exception: {Environment.NewLine}{Environment.NewLine}{ex}");
            }
            #endregion

            #endregion

            #region AbilityDex
            #region Keywords
            try
            {
                DataTable KeywordTable = SQLData.DatatableFill(PokemonDB,FixedData.KeywordList, pCnxn);

                foreach (DataRow row in KeywordTable.Rows)
                {
                    Keywords value = new Keywords();
                    value.Name = (string)row["Name"];
                    value.Description = (string)row["Description"];
                    value.UID = (int) row["UID"];

                    StaticData.ReferenceData.AbilityDex.KeywordList.Add(value);
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Keyword Dex threw exception: {Environment.NewLine}{Environment.NewLine}{ex}");
            }
            #endregion

            #region Abilities
            try
            {
                DataTable AbilityTable = SQLData.DatatableFill(PokemonDB,FixedData.AbilityList, pCnxn);
                DataTable keywordLinkTable = SQLData.DatatableFill(PokemonDB,FixedData.AbilityKeywordLink, pCnxn);

                foreach (DataRow row in AbilityTable.Rows)
                {
                    Ability value = new Ability();
                    value.Name = (string)row["Name"];
                    value.Effect = (string)row["Effect"];
                    value.Type = (string)row["Type"];
                    value.Frequency = (string)row["Frequency"];
                    value.UID = (int) row["UID"];

                    foreach (DataRow Keyword in keywordLinkTable.Select($"[Ability UID] = {value.UID}"))
                    {
                        value.Keywords.Add(StaticData.ReferenceData.AbilityDex.KeywordList.Single(s => s.UID.Equals(Keyword["Keyword UID"])).Name);
                    }

                    StaticData.ReferenceData.AbilityDex.AbilityList.Add(value);
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ability Dex threw exception: {Environment.NewLine}{Environment.NewLine}{ex}");
            }
            #endregion

            #region Capabilties
            try
            {
                DataTable capabilityTable = SQLData.DatatableFill(PokemonDB,FixedData.CapabilityList, pCnxn);

                foreach (DataRow typeRow in capabilityTable.Rows)
                {
                    Capabilities value = new Capabilities();
                    value.Name = (string) typeRow["Name"];
                    value.UID = (int) typeRow["UID"];
                    value.Description = (string) typeRow["Description"];
                    
                    
                    StaticData.ReferenceData.AbilityDex.CapabilityList.Add(value);
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Capability Dex threw exception: {Environment.NewLine}{Environment.NewLine}{ex}");
            }
            #endregion

            #endregion

            #region MoveDex

            #region ContestMoves

            try
            {
                DataTable contestMoveTable = SQLData.DatatableFill(PokemonDB,FixedData.ContestMoves, pCnxn);

                foreach (DataRow row in contestMoveTable.Rows)
                {
                    ContestMoveStats value = new ContestMoveStats();
                    value.Type = (int)row["Type UID"];
                    value.Effect = (string)row["Effect"];
                    value.Appeal.DiceSize = (int) row["DiceSize"];
                    value.Appeal.DiceMod = (int) row["DiceModifier"];
                    value.Appeal.DiceCount = (int) row["DiceCount"];
                    value.UID = (int) row["UID"];

                    StaticData.ReferenceData.MoveDex.ContestMoveList.Add(value);
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Contest Move Dex threw exception: {Environment.NewLine}{Environment.NewLine}{ex}");
            }

            #endregion
            
            #region BaseMoves

            try
            {
                DataTable baseMoveTable = SQLData.DatatableFill(PokemonDB,FixedData.MoveList, pCnxn);

                foreach (DataRow row in baseMoveTable.Rows)
                {

                    Dice dice = new Dice()
                    {
                        DiceSize = (int) row["DiceSize"],
                        DiceMod = (int) row["DiceModifier"],
                        DiceCount = (int) row["DiceCount"]
                    };
                    PokemonMove value = new PokemonMove()
                    {
                        Accuracy = (int) row["Accuracy"],
                        AttackStat = (string) row["AttackStat"],
                        TypeID = (int)row["Type UID"],
                        ContestStats = (int) row["Contest UID"],
                        Damage = dice,
                        Frequency = (string) row["Frequency"],
                        MoveID = (int) row["UID"],
                        Name = (string) row["Name"],
                        Notes = (string) row["Notes"],
                        Range = (int) row["Range"],
                        Target = (string) row["Target"]
                    };
                    
                    StaticData.ReferenceData.MoveDex.MoveList.Add(value);
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Contest Move Dex threw exception: {Environment.NewLine}{Environment.NewLine}{ex}");
            }

            #endregion

            #endregion

            #region ItemDex

            #region Item Categories
            try
            {
                DataTable itemCategoryTable = SQLData.DatatableFill(PokemonDB,FixedData.ItemCategories, pCnxn);

                foreach (DataRow row in itemCategoryTable.Rows)
                {
                    ItemCategory value = new ItemCategory();
                    value.Name = (string)row["Name"];
                    value.UID = (int) row["UID"];

                    StaticData.ReferenceData.ItemDex.ItemCategories.Add(value);
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Item Category Dex threw exception: {Environment.NewLine}{Environment.NewLine}{ex}");
            }
            #endregion

            #region Item List
            try
            {
                DataTable itemTable = SQLData.DatatableFill(PokemonDB,FixedData.ItemList, pCnxn);
                DataTable itemFlavTable = SQLData.DatatableFill(PokemonDB,FixedData.ItemFlavourList, pCnxn);

                foreach (DataRow row in itemTable.Rows)
                {
                    Item value = new Item();
                    value.Name = (string)row["Name"];
                    value.UID = (int) row["UID"];
                    value.Category = (int) row["Category"];
                    value.Frequency = (string) row["Frequency"];
                    value.Effect = (string) row["Effect"];
                    value.UnitValue = (int) row["Unit Value"];
                    value.Notes = (string) row["Notes"];
                    value.Rarity = (int) row["Rarity"];


                    foreach (var flavour in itemFlavTable.Select($"[Item UID] = {value.UID}"))
                    {
                        value.Flavours.Add((int)flavour["Flavour UID"]);
                    }


                    StaticData.ReferenceData.ItemDex.Items.Add(value);
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Item Dex threw exception: {Environment.NewLine}{Environment.NewLine}{ex}");
            }
            #endregion

            #region TM Items
            try
            {
                DataTable itemTMTable = SQLData.DatatableFill(PokemonDB,FixedData.TMList, pCnxn);

                foreach (DataRow row in itemTMTable.Rows)
                {
                    MoveLearningItem value = new MoveLearningItem();
                    value.MoveUID = (int)row["Move UID"];
                    value.UID = (int) row["UID"];
                    value.ItemUID = (int) row["Item UID"];
                    value.TMNumber = (string) row["TM Number"];
                    value.IsHM = (bool)row["HM"];

                    StaticData.ReferenceData.ItemDex.MoveLearningItems.Add(value);
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Item Category Dex threw exception: {Environment.NewLine}{Environment.NewLine}{ex}");
            }
            #endregion

            #endregion

            #region PokeDex

            #region Sex
            try
            {
                DataTable sexTable = SQLData.DatatableFill(PokemonDB,FixedData.Sex, pCnxn);

                foreach (DataRow row in sexTable.Rows)
                {
                    Gender value = new Gender();
                    value.Male = (bool)row["Male"];
                    value.Female = (bool)row["Female"];
                    value.Genderless = (bool) row["Genderless"];
                    value.FemaleRatio = (decimal) row["FemaleRatio"];
                    value.UID = (int) row["UID"];

                    StaticData.ReferenceData.Pokedex.SexList.Add(value);
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Sex Ratio Dex threw exception: {Environment.NewLine}{Environment.NewLine}{ex}");
            }
            #endregion

            #region Pokemon
            try
            {
                DataTable movementTable = SQLData.DatatableFill(PokemonDB,FixedData.Movements, pCnxn);
                DataTable pokeTable = SQLData.DatatableFill(PokemonDB,FixedData.PokemonList, pCnxn);
                

                foreach (DataRow row in pokeTable.Rows)
                {
                    Pokemon value = new Pokemon();
                   
                    value.UID = (int) row["UID"];
                    value.Name = (string) row["Name"];
                    value.Size = (string) row["Size"];
                    value.WeightClass = (int) row["Weight Class"];
                    value.ExperinceValue = (int) row["Base Experience"];
                    value.Sex = StaticData.ReferenceData.Pokedex.SexList.Single(s => s.UID.Equals(row["Sex"]));
                    value.PrimaryTypeID = (int) row["Primary Type"];
                    value.SecondaryTypeID = (int) row["Secondary Type"];
                    value.TertiaryTypeID = (int) row["Tertiary Type"];
                    value.Power = (int) row["Power"];
                    value.Intellegence = (int) row["Intelligence"];
                    value.PokedexResult.Description = (string) row["Dex Description"];
                    value.PokedexResult.Number = (int) row["Dex Number"];
                    value.PokedexResult.SpeciesType = (string) row["Dex Species"];
                    value.HatchRate = (int) row["Hatch Rate"];
                    value.CatchRate = (int) row["Catch Rate"];
                    value.BaseStats.HP = (int) row["Base HP"];
                    value.BaseStats.Attack = (int) row["Base Attack"];
                    value.BaseStats.SpecialAttack = (int) row["Base Special Attack"];
                    value.BaseStats.Defence = (int) row["Base Defence"];
                    value.BaseStats.SpecialDefence = (int) row["Base Special Defence"];
                    value.BaseStats.Speed = (int) row["Base Speed"];
                    value.PokemonFamily = (string) row["Pokemon Family"];

                    #region Fetching Movements

                    try
                    {
                        var Movementoptions = movementTable.Select($"UID = {row["Movements"]}").ToList();
                        if (Movementoptions.Count != 1)
                            throw new Exception($"Too many movements for pokemon UID {value.UID}");
                        foreach (DataRow movementRow in Movementoptions)
                        {
                            Movement move = new Movement()
                            {
                                Burrow = (int)movementRow["Burrow"],
                                Jump = (int)movementRow["Jump"],
                                Overland = (int)movementRow["Overland"],
                                Sky = (int)movementRow["Sky"],
                                Surface = (int)movementRow["Surface"],
                                Underwater = (int)movementRow["Underwater"]
                            };
                            value.Movements = move;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"PokeDex {value.Name} threw exception fetching Movements: {Environment.NewLine}{Environment.NewLine}{ex}");
                    }

                    #endregion

                    StaticData.ReferenceData.Pokedex.PokemonDexList.Add(value);
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"PokeDex threw exception: {Environment.NewLine}{Environment.NewLine}{ex}");
            }
            #endregion

            #region Fetching Habitats

            try
            {
                DataTable habitatTable = SQLData.DatatableFill(PokemonDB,FixedData.Environments, pCnxn);
                foreach (DataRow row in habitatTable.Rows)
                {
                    Habitat hab = new Habitat();
                    hab.EnvironmentUID = (int) row["Habitat UID"];
                    if (!row.IsNull("Notes"))
                        hab.Notes = (string) row["Notes"];
                    

                    hab.Rarity = (decimal) row["Rarity"];
                    hab.UID = (int) row["UID"];
                    StaticData.ReferenceData.Pokedex.PokemonDexList.Single(s => s.UID.Equals((int)row["Base UID"])).Habitats.Add(hab);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Habitat Assignment threw exception: {Environment.NewLine}{Environment.NewLine}{ex}");
            }
                    
            #endregion

            #region Fetching Egg Groups

            try
            {
                DataTable eggGroupsTable = SQLData.DatatableFill(PokemonDB,FixedData.EggGroups, pCnxn);
                foreach (DataRow row in eggGroupsTable.Rows)
                {
                    StaticData.ReferenceData.Pokedex.PokemonDexList.Single(s => s.UID.Equals((int)row["Base UID"])).EggGroup.Add((int) row["Egg UID"]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Egg Assignment threw exception: {Environment.NewLine}{Environment.NewLine}{ex}");
            }
                    
            #endregion

            #region Fetching Capabilties

            try
            {
                DataTable capabilitiesTable = SQLData.DatatableFill(PokemonDB,FixedData.Capabilities, pCnxn);
                foreach (DataRow row in capabilitiesTable.Rows)
                {
                    StaticData.ReferenceData.Pokedex.PokemonDexList.Single(s => s.UID.Equals((int)row["Base UID"])).Capability.Add((int) row["Capability UID"]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Egg Assignment threw exception: {Environment.NewLine}{Environment.NewLine}{ex}");
            }
                    
            #endregion

            #region FetchingMoveset

            try
            {
                DataTable movesTable = SQLData.DatatableFill(PokemonDB,FixedData.Moves, pCnxn);
                foreach (DataRow row in movesTable.Rows)
                {
                    int MoveID = (int)row["Move UID"];
                    
                    if (!row.IsNull("Level Learned"))
                    {
                        LevelMoves move = new LevelMoves();
                        move.MoveID = MoveID;
                        move.LevelLearned = (int) row["Level Learned"];
                        StaticData.ReferenceData.Pokedex.PokemonDexList.Single(s => s.UID.Equals((int)row["Base UID"])).PossibleLevelupMoves.Add(move);
                    }
                    
                    if((bool) row["Tutor Move"])
                        StaticData.ReferenceData.Pokedex.PokemonDexList.Single(s => s.UID.Equals((int)row["Base UID"])).PossibleTutorMoves.Add(MoveID);
                    
                    if((bool) row["Egg Move"])
                        StaticData.ReferenceData.Pokedex.PokemonDexList.Single(s => s.UID.Equals((int)row["Base UID"])).PossibleEggMoves.Add(MoveID);
                    
                    if((bool) row["TM Move"])
                        StaticData.ReferenceData.Pokedex.PokemonDexList.Single(s => s.UID.Equals((int)row["Base UID"])).PossibleTMMoves.Add(MoveID);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Move Assignment threw exception: {Environment.NewLine}{Environment.NewLine}{ex}");
            }
                    
            #endregion

            #region Fetching Evolutions

            try
            {
                DataTable evolistTable = SQLData.DatatableFill(PokemonDB,FixedData.Evolutions, pCnxn);
                foreach (DataRow row in evolistTable.Rows)
                {
                    EvolutionData Evo = new EvolutionData();
                    Evo.UID = (int) row["UID"];
                    if(!row.IsNull("Conditions"))
                        Evo.EvolutionConditions = (string) row["Conditions"];
                    Evo.EvolutionStage = (int) row["Stage"];
                    Evo.EvolutionID = (int) row["Evolved UID"];
                    StaticData.ReferenceData.Pokedex.EvolutionList.Add(Evo);
                    StaticData.ReferenceData.Pokedex.PokemonDexList.Single(s => s.UID.Equals((int)row["Base UID"])).EvolutionIDs.Add(Evo.UID);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Evolution Assignment threw exception: {Environment.NewLine}{Environment.NewLine}{ex}");
            }
                    
            #endregion

            #endregion

            #region TrainerDex

            #region Trainer Stubs
            try
            {
                DataTable stubTable = SQLData.DatatableFill(TrainerDB,FixedData.PlayerSummary, tCnxn);

                foreach (DataRow row in stubTable.Rows)
                {
                    PlayerStub value = new PlayerStub();
                    //Player value = new Player();
                    var Name = ((string)row["Character_Name"]).Split(' ');
                    value.FirstName = Name[0];
                    if(Name.Length > 1)
                        value.LastName = Name[1];
                    value.Description = (string)row["Description"];
                    value.UID = (int) row["ID"];
                    value.PlayerName = (string)row["Player_Name"];
                    value.Money = (int) row["Money"];
                    value.Strength = (int) row["Strength"];
                    value.Dexterity = (int) row["Dexterity"];
                    value.Constitution = (int) row["Constitution"];
                    value.Intelligence = (int) row["Intelligence"];
                    value.Wisdom = (int) row["Wisdom"];
                    value.Charisma = (int) row["Charisma"];
                    value.CurrentHP = (int) row["Current_HP"];
                    value.MaxHP = (int) row["Max_HP"];
                    value.Notes = (string) row["Notes"];
                    StaticData.ReferenceData.TrainerDex.Trainers.Add(value);
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Trainer Stubs threw exception: {Environment.NewLine}{Environment.NewLine}{ex}");
            }
            #endregion

            #region Advancement Types
            try
            {
                DataTable advancementTable = SQLData.DatatableFill(PokemonDB,FixedData.LevelUpList, tCnxn);

                foreach (DataRow row in advancementTable.Rows)
                {
                    AdvancementTypes value = new AdvancementTypes();
                    
                    value.UID = (int) row["UID"];
                    value.ChangeType = (string) row["Change Type"];
                    StaticData.ReferenceData.TrainerDex.Advances.Add(value);
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Trainer Stubs threw exception: {Environment.NewLine}{Environment.NewLine}{ex}");
            }
            #endregion

            #endregion

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
            DataBinding.BindThis(Lb_PokemonTeam, StaticData.PlayerData, "Pkmnlist");
        }

        private void btnTest_Click(object sender, RoutedEventArgs e)
        {
            DebugMode = !DebugMode;
        }

        private void btn_Trainer_Click(object sender, MouseButtonEventArgs e)
        {
            if (!TrainerEnabled && !DebugMode) return;
            var Trainer = new TrainerPage();

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
            var pcWindow = new PC(StaticData.ReferenceData, StaticData.PlayerData);
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