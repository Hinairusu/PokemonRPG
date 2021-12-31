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
                if (StaticData.PlayerData.Pkmnlist.Count > 0)
                    StaticData.PlayerData.Pkmnlist.RemoveAt(0);
                StaticData.PlayerData.Pkmnlist.Add(StaticData.ReferenceData.GenerateTrainerPokemon(
                    StaticData.ReferenceData.RandomGenerator.Next(0,
                        StaticData.ReferenceData.Pokedex.PokemonDexList.Count),
                    StaticData.ReferenceData.RandomGenerator.Next(1, PkmnLevel + 1)));
                StaticData.PlayerData.Pkmnlist[0].CurrentHP = StaticData.PlayerData.Pkmnlist[0].MaxHP;
                var page = new PokemonPage(0);
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
                col.ColumnName = "Effect";
                col.DataType = typeof(string);
                DT1.Columns.Add(col);
            }
            catch { }
            try
            {
                DataColumn col = new DataColumn();
                col.ColumnName = "DiceCount";
                col.DataType = typeof(int);
                DT1.Columns.Add(col);
            }
            catch { }

            try
            {
                DataColumn col = new DataColumn();
                col.ColumnName = "DiceSize";
                col.DataType = typeof(int);
                DT1.Columns.Add(col);
            }
            catch { }

            try
            {
                DataColumn col = new DataColumn();
                col.ColumnName = "DiceModifier";
                col.DataType = typeof(int);
                DT1.Columns.Add(col);
            }
            catch { }


            try
            {
                DataColumn col = new DataColumn();
                col.ColumnName = "Type UID";
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

            List<string> Types = new List<string>();
            foreach (var ability in StaticData.ReferenceData.MoveDex.MoveList)
            {
                Types.Add(ability.ContestStats.Type.Name);
            }

            Types = Types.Distinct().ToList();
            Dictionary<string, int> Tdictionary = new Dictionary<string, int>();
            for (int i = 0; i < Types.Count; i++)
            {
                Tdictionary.Add(Types[i],i);
                DataRow row = DT2.NewRow();
                row["Name"] = Types[i];
                DT2.Rows.Add(row);
            }



            foreach (var ability in StaticData.ReferenceData.MoveDex.MoveList)
            {
                try
                {
                    var move = ability.ContestStats;
                    DataRow row = DT1.NewRow();
                    row["Effect"] = move.Effect;
                    row["Type UID"] = Tdictionary[move.Type.Name];
                    row["DiceCount"] = move.Appeal.DiceCount;
                    row["DiceSize"] = move.Appeal.DiceSize;
                    row["DiceModifier"] = move.Appeal.DiceMod;
                    DT1.Rows.Add(row); }
                catch { }
                
                
            }
            
            DataView view = new DataView(DT1);
            DataTable distinctValues = view.ToTable(true, "Effect", "Type UID", "DiceCount","DiceSize","DiceModifier"); 


            #region DataTable 3

            DataTable DT3 = new DataTable();
            try
            {
                DataColumn col = new DataColumn();
                col.ColumnName = "Name";
                col.DataType = typeof(string);
                DT3.Columns.Add(col);
            }
            catch { }
            
            try
            {
                DataColumn col = new DataColumn();
                col.ColumnName = "Type UID";
                col.DataType = typeof(int);
                DT3.Columns.Add(col);
            }
            catch { }

            try
            {
                DataColumn col = new DataColumn();
                col.ColumnName = "Frequency";
                col.DataType = typeof(string);
                DT3.Columns.Add(col);
            }
            catch { }

            try
            {
                DataColumn col = new DataColumn();
                col.ColumnName = "Range";
                col.DataType = typeof(int);
                DT3.Columns.Add(col);
            }
            catch { }

            try
            {
                DataColumn col = new DataColumn();
                col.ColumnName = "Accuracy";
                col.DataType = typeof(int);
                DT3.Columns.Add(col);
            }
            catch { }

            try
            {
                DataColumn col = new DataColumn();
                col.ColumnName = "Notes";
                col.DataType = typeof(string);
                DT3.Columns.Add(col);
            }
            catch { }

            try
            {
                DataColumn col = new DataColumn();
                col.ColumnName = "Name";
                col.DataType = typeof(string);
                DT3.Columns.Add(col);
            }
            catch { }

            try
            {
                DataColumn col = new DataColumn();
                col.ColumnName = "Target";
                col.DataType = typeof(string);
                DT3.Columns.Add(col);
            }
            catch { }

            try
            {
                DataColumn col = new DataColumn();
                col.ColumnName = "AttackStat";
                col.DataType = typeof(string);
                DT3.Columns.Add(col);
            }
            catch { }

            try
            {
                DataColumn col = new DataColumn();
                col.ColumnName = "Contest UID";
                col.DataType = typeof(int);
                DT3.Columns.Add(col);
            }
            catch { }

            try
            {
                DataColumn col = new DataColumn();
                col.ColumnName = "DiceCount";
                col.DataType = typeof(int);
                DT3.Columns.Add(col);
            }
            catch { }

            try
            {
                DataColumn col = new DataColumn();
                col.ColumnName = "DiceSize";
                col.DataType = typeof(int);
                DT3.Columns.Add(col);
            }
            catch { }

            try
            {
                DataColumn col = new DataColumn();
                col.ColumnName = "DiceModifier";
                col.DataType = typeof(int);
                DT3.Columns.Add(col);
            }
            catch { }


            #endregion

            foreach (var move in StaticData.ReferenceData.MoveDex.MoveList)
            {
                DataRow row = DT3.NewRow();

                row["Name"] = move.Name;
                row["Type UID"] = move.TypeID;
                row["Frequency"] = move.Frequency;
                row["Range"] = move.Range;
                row["Accuracy"] = move.Accuracy;
                row["Notes"] = move.Notes;
                row["Target"] = move.Target;
                row["AttackStat"] = move.AttackStat;
                
                row["DiceCount"] = move.Damage.DiceCount;
                row["DiceSize"] = move.Damage.DiceSize;
                row["DiceModifier"] = move.Damage.DiceMod;

                for (var i = 0; i < distinctValues.Rows.Count; i++)
                {
                    if (!distinctValues.Rows[i]["Effect"].Equals(move.ContestStats.Effect) ||
                        !distinctValues.Rows[i]["Type UID"].Equals(Tdictionary[move.ContestStats.Type.Name]) ||
                        !distinctValues.Rows[i]["DiceCount"].Equals(move.ContestStats.Appeal.DiceCount) ||
                        !distinctValues.Rows[i]["DiceSize"].Equals(move.ContestStats.Appeal.DiceSize) ||
                        !distinctValues.Rows[i]["DiceModifier"].Equals(move.ContestStats.Appeal.DiceMod)) continue;
                    row["Contest UID"] = i;
                    break;
                }

                DT3.Rows.Add(row);
            }


            //if(DT2.Rows.Count > 0)
            //    SQLData.SQLInsert(DT2, ConfigurationManager.AppSettings["PokemonDatabase"], "ContestTypes", cnxn);
            //if(DT1.Rows.Count > 0)
            //    SQLData.SQLInsert(distinctValues, ConfigurationManager.AppSettings["PokemonDatabase"], "ContestMoves", cnxn);
            if(DT3.Rows.Count > 0)
               SQLData.SQLInsert(DT3, ConfigurationManager.AppSettings["PokemonDatabase"], "MoveList", cnxn);


            MessageBox.Show("Done!");
        }
    }


}
