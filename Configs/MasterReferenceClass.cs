using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Documents;
using System.Xml;
using AutoMapper;

namespace PokemonRPG.Configs
{
    public class MasterReferenceClass
    {
        public MasterReferenceClass()
        {
            Pokedex = new Pokedex();
            ItemDex = new Itemdex();
            NatureDex = new Naturedex();
            TrainerDex = new Trainerdex();
            MoveDex = new Movedex();
            TypeDex = new Typedex();
            AbilityDex = new Abilitydex();
        }

        public Random RandomGenerator { get; set; } = new Random();
        public Pokedex Pokedex { get; set; }
        public Itemdex ItemDex { get; set; }
        public Naturedex NatureDex { get; set; }
        public Trainerdex TrainerDex { get; set; }
        public Movedex MoveDex { get; set; }
        public Typedex TypeDex { get; set; }


        public Abilitydex AbilityDex { get; set; }

        #region Generation of Pokemon



        private readonly MapperConfiguration BasePktoTrainterPk =
            new MapperConfiguration(cfg => cfg.CreateMap<Pokemon, TrainerPokemon>());
        
        
        public TrainerPokemon GenerateTrainerPokemon(Pokemon Poke)
        {
            return GenerateRandomTrainerPokemon(Poke.UID);
        }
        public TrainerPokemon GenerateTrainerPokemon(int UID)
        {
            var mapper = BasePktoTrainterPk.CreateMapper();
            var poke = mapper.Map<TrainerPokemon>(GenerateWildPokemon(UID));
            poke.Level = 1;

            return poke;
        }

        public Advancements GenerateRandomAdvancement()
        {
            Advancements advance = new Advancements();
            advance.LevelUpUID = StaticData.ReferenceData.RandomGenerator.Next(2,7);
            advance.Modifier = 1;
            advance.ValueAdd = true;
            advance.DateAdded = DateTime.Now;
            advance.Notes = "Random Advance Generated";

            return advance;

        }
        public TrainerPokemon GenerateRandomTrainerPokemon(int UID, int level = 1)
        {
            var mapper = BasePktoTrainterPk.CreateMapper();
            var poke = mapper.Map<TrainerPokemon>(GenerateWildPokemon(UID));

            poke.Level = level;


            for (var i = 1; i < level; i++)
            {
                poke.LevelUps.Add(GenerateRandomAdvancement());
                if (level > 50)
                    poke.LevelUps.Add(GenerateRandomAdvancement());
                if (level > 75)
                    poke.LevelUps.Add(GenerateRandomAdvancement());
            }

            var Ratio = StaticData.ReferenceData.RandomGenerator.NextDouble();
            if (poke.Sex.FemaleRatio > decimal.Parse(Ratio.ToString()) && poke.Sex.Male)
                poke.ActualSex = "Male";
            else if (poke.Sex.Female)
                poke.ActualSex = "Female";
            else
                poke.ActualSex = "Genderless";

            poke.Nature =
                StaticData.ReferenceData.NatureDex.Natures[
                    StaticData.ReferenceData.RandomGenerator.Next(0, StaticData.ReferenceData.NatureDex.Natures.Count)];
            poke.Advance();
            return poke;
        }
        

        public TrainerPokemon GenerateWildPokemon(int UID, int level = 0)
        {
            var pk = new Pokemon();
            var mapper = BasePktoTrainterPk.CreateMapper();
            var poke = mapper.Map<TrainerPokemon>(Pokedex.PokemonDexList.Find(s => s.UID.Equals(UID)));
            if (level > 0)
            {
                poke.Level = level;
                for (var i = 1; i < level; i++)
                    poke.LevelUps.Add(GenerateRandomAdvancement());
            }

            var Ratio = StaticData.ReferenceData.RandomGenerator.NextDouble();
            if (poke.Sex.FemaleRatio > decimal.Parse(Ratio.ToString()) && poke.Sex.Male)
                poke.ActualSex = "Male";
            else if (poke.Sex.Female)
                poke.ActualSex = "Female";
            else
                poke.ActualSex = "Genderless";

            poke.Nature =
                StaticData.ReferenceData.NatureDex.Natures[
                    StaticData.ReferenceData.RandomGenerator.Next(0, StaticData.ReferenceData.NatureDex.Natures.Count)];
            poke.Advance();
            return poke;
        }


        public void AdvancePokemon(int PokemonUID, List<Advancements> Advances)
        {

        }
        #endregion
    }

    public class DataTableFeatures
    {
        public string ColumnName { get; set; }
        public Type ColumnType { get; set; }
    }

    public static class SQLData
    {
        public static DataTable MakeDT(List<DataTableFeatures> Columns)
        {
            DataTable DT = new DataTable();
            foreach (var col in Columns)
            {
                DataColumn column = new DataColumn();
                column.ColumnName = col.ColumnName;
                column.DataType = col.ColumnType;
                DT.Columns.Add(column);
            }

            return DT;
        }
        public static string GenerateSQLConString(string DatabaseName)
        {
            return
                $"Server={ConfigurationManager.AppSettings["SQLIP"]}; Database={DatabaseName}; User Id={ConfigurationManager.AppSettings["Username"]}; Password={ConfigurationManager.AppSettings["Password"]}";
        }
        public static DataTable DatatableFill(string DatabaseName, string TableName, string ConString, string Where = null)
        {
            #region preset strings
            DataTable dbDATA = new DataTable();
            SqlConnection conn;
            #endregion

            try
            {
                //make MySQL Cnnection
                conn = new SqlConnection(ConString);
                // Make MySQL COmmand
                string query = $"SELECT * FROM [{DatabaseName}].[dbo].[{TableName}]"; // +" WHERE HERE IF NEEDED";
                if (!string.IsNullOrWhiteSpace(Where))
                    query += $" WHERE {Where}";

                SqlCommand cmd = new SqlCommand(query, conn);
                //from this point the connection to the DB is live
                conn.Open();
                // create data adapter
                using (var da = new SqlDataAdapter(cmd))
                {
                    //Fill the table!
                    da.Fill(dbDATA);
                }
                //close afer we are done
                conn.Close();
                //empty the buffer of the command we created to prevent interception
                cmd.Dispose();
            }
            catch (SqlException ex)
            {
                return null;
            }
            return dbDATA;
        }

        public static void SQLInsert(DataTable DT, string DatabaseName, string TableName, string ConString)
        {
            StringBuilder SQLSTR = new StringBuilder();
            StringBuilder colNames = new StringBuilder();
            StringBuilder colValues = new StringBuilder();
            SQLSTR.Append($"INSERT INTO [{DatabaseName}].[dbo].[{TableName}] ");
            colNames.Append("(");
            colValues.Append("VALUES (");
            int ParameterCount = 1;
            foreach (DataColumn col in DT.Columns)
            {

                colNames.Append($"[{col.ColumnName}]");
                colValues.Append($"@Val{ParameterCount}");


                if (ParameterCount != DT.Columns.Count)
                {
                    colNames.Append(",");
                    colValues.Append(",");
                }
                else
                {
                    colNames.Append(") ");
                    colValues.Append(")");
                }
                ParameterCount++;
            }
            using (var conn = new SqlConnection(ConString))
            {
                conn.Open();
                var SQLString = $"{SQLSTR}{colNames}{colValues}";

                using (var Command = new SqlCommand(SQLString, conn))
                {
                    for (int i = 1; i < ParameterCount; i++)
                    {
                        SqlParameter Param = new SqlParameter
                        {
                            ParameterName = $"@Val{i}",
                            SqlDbType = GetSqlType(DT.Columns[i - 1].DataType),
                            Size = DT.Columns[i - 1].MaxLength
                        };

                        Command.Parameters.Add(Param);
                    }
                    

                    foreach (DataRow row in DT.Rows)
                    {
                        try
                        {
                            for (int i = 1; i < ParameterCount; i++)
                            {
                                Command.Parameters[$"@Val{i}"].Value = row[i - 1];
                            }
                            Command.ExecuteNonQuery();
                            
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error: {ex}");
                            break;
                        }

                    }
                }
            }
        }

          public static void SQLDelete(DataTable DT, string DatabaseName, string TableName, string ConString, bool OutputRowDebug = false)
            {
                StringBuilder SQLSTR = new StringBuilder();
                SQLSTR.Append($"DELETE FROM [{DatabaseName}].[dbo].[{TableName}] WHERE ");
                int ParameterCount = 1;
                foreach (DataColumn col in DT.Columns)
                {
                    SQLSTR.Append($"[{col.ColumnName}] = @Val{ParameterCount}");

                    if (ParameterCount != DT.Columns.Count)
                        SQLSTR.Append(" AND ");
                    ParameterCount++;
                }

                using (var conn = new SqlConnection(ConString))
                {
                    conn.Open();
                    var SQLString = SQLSTR.ToString();

                    using (var Command = new SqlCommand(SQLString, conn))
                    {
                        for (int i = 1; i < ParameterCount; i++)
                            Command.Parameters.Add($"@Val{i}", GetSqlType(DT.Columns[i - 1].DataType), DT.Columns[i - 1].MaxLength);
                        foreach (DataRow row in DT.Rows)
                        {
                            try
                            {
                                for (int i = 1; i < ParameterCount; i++)
                                {
                                    Command.Parameters[$"@Val{i}"].Value = row[i - 1];
                                }
                                Command.ExecuteNonQuery();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"Error: {ex}");
                                break;
                            }

                        }
                    }
                }
            }


          public static SqlDbType GetSqlType(Type type)
          {
              if (type == typeof(string))
                  return SqlDbType.NVarChar;
              if (type == typeof(XmlDocument))
                  return SqlDbType.Xml;

              if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                  type = Nullable.GetUnderlyingType(type);

              var param = new SqlParameter("", Activator.CreateInstance(type));
              return param.SqlDbType;

          }
    }

    public static class LoadDex
    {
        public static string PokemonDB = ConfigurationManager.AppSettings["PokemonDatabase"];
        public static string TrainerDB = ConfigurationManager.AppSettings["PlayerDatabase"];
        public static string pCnxn = SQLData.GenerateSQLConString(PokemonDB);
        public static string tCnxn = SQLData.GenerateSQLConString(TrainerDB);

        public static void LoadSQLData()
        {
            LoadTypeDex();
            LoadNatureDex();
            LoadAbilityDex();
            LoadMoveDex();
            LoadItemDex();
            LoadPokeDex();
            LoadTrainerDex();
        }

        public static void LoadTypeDex()
        {
             #region Typedex

            #region Basic Types
            try
            {
                StaticData.ReferenceData.TypeDex.TypeList.Clear();
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
                StaticData.ReferenceData.TypeDex.ContestTypeList.Clear();
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
        }

        public static void LoadNatureDex()
        {
            
            #region Naturedex

            #region Flavours
            try
            {
                StaticData.ReferenceData.NatureDex.Flavours.Clear();
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
                StaticData.ReferenceData.NatureDex.Natures.Clear();
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
                StaticData.ReferenceData.NatureDex.Enviroments.Clear();
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
                StaticData.ReferenceData.NatureDex.EggGroups.Clear();
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

        }

        public static void LoadAbilityDex()
        {
            
            #region AbilityDex
            #region Keywords
            try
            {
                StaticData.ReferenceData.AbilityDex.KeywordList.Clear();
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
                StaticData.ReferenceData.AbilityDex.AbilityList.Clear();
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
                StaticData.ReferenceData.AbilityDex.CapabilityList.Clear();
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
        }

        public static void LoadMoveDex()
        {

            #region MoveDex

            #region ContestMoves

            try
            {
                StaticData.ReferenceData.MoveDex.ContestMoveList.Clear();
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
                StaticData.ReferenceData.MoveDex.MoveList.Clear();
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

        }
        public static void LoadItemDex()
        {

            #region ItemDex

            #region Item Categories
            try
            {
                StaticData.ReferenceData.ItemDex.ItemCategories.Clear();
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
                StaticData.ReferenceData.ItemDex.Items.Clear();
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
                StaticData.ReferenceData.ItemDex.MoveLearningItems.Clear();
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

        }
        public static void LoadPokeDex()
        {
            
            #region PokeDex

            #region Sex
            try
            {
                StaticData.ReferenceData.Pokedex.SexList.Clear();
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
                StaticData.ReferenceData.Pokedex.PokemonDexList.Clear();
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

            #region Fetching Moveset

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
                StaticData.ReferenceData.Pokedex.EvolutionList.Clear();
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
            
        }
        public static void LoadTrainerDex()
        {
            
            #region TrainerDex

            #region Trainer Stubs
            try
            {
                StaticData.ReferenceData.TrainerDex.Trainers.Clear();
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
                List<int> LevelUpUids = new List<int>() {68, 69, 70, 71, 72, 73};
                
                
                StaticData.ReferenceData.TrainerDex.Advances.Clear();
                DataTable advancementTable = SQLData.DatatableFill(PokemonDB,FixedData.LevelUpList, tCnxn);

                foreach (DataRow row in advancementTable.Rows)
                {
                    AdvancementTypes value = new AdvancementTypes();
                    
                    value.UID = (int) row["UID"];
                    value.ChangeType = (string) row["Change Type"];
                    if(LevelUpUids.Contains(value.UID))
                        StaticData.ReferenceData.TrainerDex.LevelUpAdvancements.Add(value);
                    else
                        StaticData.ReferenceData.TrainerDex.Advances.Add(value);
                    if(value.UID >23 && value.UID < 52)
                        StaticData.ReferenceData.Pokedex.MoveSlots.Add(value);
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Trainer Stubs threw exception: {Environment.NewLine}{Environment.NewLine}{ex}");
            }
            #endregion

            StaticData.ReferenceData.TrainerDex.LoadedTrainers.Clear();
            StaticData.PlayerData = new Player();

            #endregion

        }
    }
}