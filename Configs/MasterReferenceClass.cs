﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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

    public static class SQLData
    {
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
}