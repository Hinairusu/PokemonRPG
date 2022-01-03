using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;
using System.Xml.Serialization;
using PokemonRPG.Configs;

namespace PokemonRPG.Windows
{
    /// <summary>
    ///     Interaction logic for PC.xaml
    /// </summary>
    public partial class PC : Window
    {
        public PC()
        {
            
            InitializeComponent();
            DataBindings();
        }

        public void DataBindings()
        {
            DataBinding.BindThis(Lb_PartyPokemon, StaticData.PlayerData, "CurrentParty");
            DataBinding.BindThis(Lb_PokemonPC, StaticData.PlayerData, "OwnedPokemon");
            DataBinding.BindThis(cbx_BoxPokemon, StaticData.PlayerData, "OwnedPokemon");
        }

        private void btn_PartyAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<DataTableFeatures> Columns = new List<DataTableFeatures>();
                Columns.Add(new DataTableFeatures() {ColumnName = "Trainer UID", ColumnType = typeof(int)});
                Columns.Add(new DataTableFeatures() {ColumnName = "Party Slot", ColumnType = typeof(int)});
                Columns.Add(new DataTableFeatures() {ColumnName = "Pokemon UID", ColumnType = typeof(int)});


                DataTable DT = SQLData.MakeDT(Columns);

                DataRow row = DT.NewRow();
                row[Columns[0].ColumnName] = StaticData.PlayerData.UID;
                row[Columns[1].ColumnName] = cbx_PartySlot.SelectedIndex;
                row[Columns[2].ColumnName] = ((TrainerPokemon)cbx_BoxPokemon.SelectedItem).UID;
                DT.Rows.Add(row);

                SQLData.SQLInsert(DT, LoadDex.TrainerDB, FixedData.CurrentParty, LoadDex.tCnxn); ;
                MessageBox.Show("Operation Complete.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Exception thrown trying to Add Party:  {Environment.NewLine}{Environment.NewLine}{ex}");
            }
        }

        private void btn_PartyRemoval_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<DataTableFeatures> Columns = new List<DataTableFeatures>();
                Columns.Add(new DataTableFeatures() {ColumnName = "Trainer UID", ColumnType = typeof(int)});
                Columns.Add(new DataTableFeatures() {ColumnName = "Party Slot", ColumnType = typeof(int)});
                Columns.Add(new DataTableFeatures() {ColumnName = "Pokemon UID", ColumnType = typeof(int)});


                DataTable DT = SQLData.MakeDT(Columns);

                DataRow row = DT.NewRow();
                row[Columns[0].ColumnName] = StaticData.PlayerData.UID;
                row[Columns[1].ColumnName] = ((PartyPokemon)Lb_PartyPokemon.SelectedItem).Slot;
                row[Columns[2].ColumnName] = ((PartyPokemon)Lb_PartyPokemon.SelectedItem).PokemonUID;
                DT.Rows.Add(row);

                SQLData.SQLDelete(DT, LoadDex.TrainerDB, FixedData.CurrentParty, LoadDex.tCnxn); ;
                MessageBox.Show("Operation Complete.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Exception thrown trying to Remove Party:  {Environment.NewLine}{Environment.NewLine}{ex}");
            }
        }
    }
}