using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using PokemonRPG.Configs;

namespace PokemonRPG.Windows
{
    /// <summary>
    /// Interaction logic for GMPage.xaml
    /// </summary>
    public partial class GMPage : Window
    {
        public GMPage()
        {
            InitializeComponent();
            cb_PokemonList.ItemsSource = StaticData.ReferenceData.Pokedex.PokemonDexList;
            cb_NatureList.ItemsSource = StaticData.ReferenceData.NatureDex.Natures;
        }
        
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void btn_Pkmn_Encounter_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(tb_PokemonLevel.Text, out int PkmnLevel) && PkmnLevel > 0 && PkmnLevel < 151)
            {
                if (StaticData.PlayerData.CurrentParty.Count > 0)
                    StaticData.PlayerData.CurrentParty.Clear();

                var RandomEncounter = StaticData.ReferenceData.GenerateRandomTrainerPokemon(
                    StaticData.ReferenceData.RandomGenerator.Next(0,
                        StaticData.ReferenceData.Pokedex.PokemonDexList.Count),
                    PkmnLevel);
                RandomEncounter.CurrentHP = RandomEncounter.MaxHP;
                StaticData.PlayerData.OwnedPokemon.Add(RandomEncounter);
                StaticData.PlayerData.CurrentParty.Add(new PartyPokemon(){PokemonUID = RandomEncounter.UID, Slot = 0});
                
                var page = new PokemonPage(RandomEncounter.UID);
                page.Show();
            }
            else
            {
                MessageBox.Show("Invalid Level in box");
            }
        }

        private void btn_Generate_Dex_Code_Click(object sender, RoutedEventArgs e)
        {
            
            StringBuilder HexValue = new StringBuilder();
            if (cb_PokemonList.SelectedIndex < 0)
            {
                MessageBox.Show("Error, no Pokemon selected!");
                return;
            }
            if(cb_NatureList.SelectedIndex < 0)
            {
                MessageBox.Show("Error, no Nature selected!");
                return;
            }

            Pokemon pkmn = cb_PokemonList.SelectedItem as Pokemon;
            HexValue.Append(pkmn.UID.ToString("X3")); // [0], [1], [2]
            
            InherentNature nature = cb_NatureList.SelectedItem as InherentNature;
            HexValue.Append(nature.UID.ToString("X2")); // [3], [4],

            HexValue.Append(int.Parse(tb_level.Text).ToString("X2")); // [5], [6]


            if (cbx_Shiny.IsChecked.HasValue && cbx_Shiny.IsChecked.Value == true) // [7]
                HexValue.Append(1);
            else
                HexValue.Append(0);

            if (cbx_Moves.IsChecked.HasValue && cbx_Moves.IsChecked.Value == true) // [8,9]
                HexValue.Append("01");
            else
                HexValue.Append("00");
            
            if (cbx_Capabilities.IsChecked.HasValue && cbx_Capabilities.IsChecked.Value == true) // [10]
                HexValue.Append(1);
            else
                HexValue.Append(0);

            if (cbx_Attitude.IsChecked.HasValue && cbx_Attitude.IsChecked.Value == true) // [11]
                HexValue.Append(1);
            else
                HexValue.Append(0);


            tb_DexCode.Text = PokeTextConverter(HexValue.ToString().ToCharArray().ToList());


        }

        private string PokeTextConverter(List<char> pokeCode)
        {
            if (!cbx_Stage_1.IsChecked.HasValue || cbx_Stage_1.IsChecked.Value != true) return "0000-0000-0000";
            var PokeText = new StringBuilder();
            PokeText.Append($"{pokeCode[0]}");
            PokeText.Append($"{pokeCode[1]}");
            PokeText.Append($"{pokeCode[2]}");
            PokeText.Append($"{pokeCode[3]}");
            if (!cbx_Stage_2.IsChecked.HasValue || cbx_Stage_2.IsChecked.Value != true) return PokeText.ToString();
            PokeText.Append("-");
            PokeText.Append($"{pokeCode[4]}");
            PokeText.Append($"{pokeCode[5]}");
            PokeText.Append($"{pokeCode[6]}");
            PokeText.Append($"{pokeCode[7]}");
            if (!cbx_Stage_3.IsChecked.HasValue || cbx_Stage_3.IsChecked.Value != true) return PokeText.ToString();
            PokeText.Append("-");
            PokeText.Append($"{pokeCode[8]}");
            PokeText.Append($"{pokeCode[9]}");
            PokeText.Append($"{pokeCode[10]}");
            PokeText.Append($"{pokeCode[11]}");

            return PokeText.ToString();
        }

        private void TestingButton_Click(object sender, RoutedEventArgs e)
        {
            
            
            var cnxn = SQLData.GenerateSQLConString(ConfigurationManager.AppSettings["PokemonDatabase"]);

            #region DataTable 1

            DataTable DT1 = new DataTable();
            //try
            //{
            //    DataColumn col = new DataColumn();
            //    col.ColumnName = "Name";
            //    col.DataType = typeof(string);
            //    DT1.Columns.Add(col);
            //}
            //catch { }
            try
            {
                DataColumn col = new DataColumn();
                col.ColumnName = "Base UID";
                col.DataType = typeof(int);
                DT1.Columns.Add(col);
            }
            catch { }
            try
            {
                DataColumn col = new DataColumn();
                col.ColumnName = "Move UID";
                col.DataType = typeof(int);
                DT1.Columns.Add(col);
            }
            catch { }
           
            try
            {
                DataColumn col = new DataColumn();
                col.ColumnName = "Level Learned";
                col.DataType = typeof(int);
                DT1.Columns.Add(col);
            }
            catch { }

            try
            {
                DataColumn col = new DataColumn();
                col.ColumnName = "Tutor Move";
                col.DataType = typeof(int);
                DT1.Columns.Add(col);
            }
            catch { }

            try
            {
                DataColumn col = new DataColumn();
                col.ColumnName = "Egg Move";
                col.DataType = typeof(int);
                DT1.Columns.Add(col);
            }
            catch { }

            try
            {
                DataColumn col = new DataColumn();
                col.ColumnName = "TM Move";
                col.DataType = typeof(int);
                DT1.Columns.Add(col);
            }
            catch { }

            #endregion
            #region DataTable 2

            DataTable DT2 = new DataTable();
            try
            {
                DataColumn col = new DataColumn();
                col.ColumnName = "Name";
                col.DataType = typeof(string);
                DT2.Columns.Add(col);
            }
            catch { }
            

            #endregion
            


            foreach (var pkmn in StaticData.ReferenceData.Pokedex.PokemonDexList)
            {

                
                foreach (var value in pkmn.PossibleTMMoves)
                {
                    try
                    {
                        if (value == -1)
                            continue;
                        DataRow row = DT1.NewRow();
                        row["Base UID"] = pkmn.UID;
                        row["Move UID"] = value;
                        row["TM Move"] = 1;
                        row["Egg Move"] = 0;
                        row["Tutor Move"] = 0;
                        DT1.Rows.Add(row);
                    }
                    catch { }
                }

            }

                //if(DT2.Rows.Count > 0)
                //    SQLData.SQLInsert(DT2, ConfigurationManager.AppSettings["PokemonDatabase"], "ContestTypes", cnxn);
                //if (DT1.Rows.Count > 0)
                //SQLData.SQLInsert(DT1, ConfigurationManager.AppSettings["PokemonDatabase"], "Moves", cnxn);
            


            MessageBox.Show("Done!");
        }
    }


}
