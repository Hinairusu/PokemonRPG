using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using PokemonRPG.Configs;

namespace PokemonRPG.Windows
{
    /// <summary>
    ///     Interaction logic for Trainer.xaml
    /// </summary>
    public partial class TrainerPage : Window
    {
        private bool Load = false;
        public TrainerPage(bool Loaded)
        {
            Load = Loaded;
            InitializeComponent();
            DataBindings();
        }

        public void DataBindings()
        {
            DataBinding.BindThis(cbx_Players, StaticData.ReferenceData.TrainerDex, "LoadedTrainers");
            DataBinding.BindThis(cbx_Active_Player, StaticData.ReferenceData.TrainerDex, "LoadedTrainers");
            DataBinding.BindThis(cbx_PlayerStubs, StaticData.ReferenceData.TrainerDex, "Trainers");
            DataBinding.BindThis(cbx_BasePokemon, StaticData.ReferenceData.Pokedex, "PokemonDexList");
            DataBinding.BindThis(cbx_Breeder, StaticData.ReferenceData.TrainerDex, "Trainers");
            DataBinding.BindThis(cbx_Nature, StaticData.ReferenceData.NatureDex, "Natures");
            DataBinding.BindThis(cbx_Parent1, StaticData.PlayerData, "OwnedPokemon");
            DataBinding.BindThis(cbx_Parent2, StaticData.PlayerData, "OwnedPokemon");
            try
            {
                cbx_Active_Player.SelectedIndex =
                    StaticData.ReferenceData.TrainerDex.LoadedTrainers.FindIndex(s =>
                        s.UID.Equals(StaticData.PlayerData.UID));
            }
            catch{}
        }

        private void btn_Unload_Click(object sender, RoutedEventArgs e)
        {
            if (cbx_Players.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a loaded character to unload");
                return;
            }

            StaticData.ReferenceData.TrainerDex.LoadedTrainers.RemoveAt(cbx_Players.SelectedIndex);
            DataBindings();
            
        }

        private void btn_Load_Click(object sender, RoutedEventArgs e)
        {
            if (cbx_PlayerStubs.SelectedIndex == -1)
            {
                MessageBox.Show("Please select an unloaded character to load");
                return;
            }
            
            Player player = new Player(StaticData.ReferenceData.TrainerDex.Trainers[cbx_PlayerStubs.SelectedIndex]);

            try
            {
                DataTable CurrentPartyID = SQLData.DatatableFill(LoadDex.TrainerDB,FixedData.CurrentParty, LoadDex.tCnxn,$"[Trainer UID] = {player.UID}");

                foreach (DataRow row in CurrentPartyID.Rows)
                {
                    PartyPokemon pk = new PartyPokemon();
                    pk.PokemonUID = (int) row["Pokemon UID"];
                    pk.Slot = (int) row["Party Slot"];
                    player.CurrentParty.Add(pk);
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show($"Current Party Fetch threw exception: {Environment.NewLine}{Environment.NewLine}{ex}");
            }

            int PkID = -1;
            try
            {
                DataTable PokemonList = SQLData.DatatableFill(LoadDex.PokemonDB, FixedData.TrainerPokemon,
                    LoadDex.tCnxn, $"[Trainer UID] = {player.UID}");

                foreach (DataRow row in PokemonList.Rows)
                {
                    PkID = (int) row["UID"];
                    
                    TrainerPokemon pk = StaticData.ReferenceData.GenerateTrainerPokemon((int) row["Base Pokemon UID"]);
                    pk.UID = PkID;
                    pk.TrainerID = player.UID;
                    pk.Nickname = (string) row["Nickname"];
                    pk.ActualSex = (string) row["Actual Sex"];
                    pk.Nature = StaticData.ReferenceData.NatureDex.Natures.Single(s =>
                        s.UID.Equals((int) row["Nature UID"]));
                    if(!row.IsNull("Breeder UID"))
                        pk.BreederID = (int) row["Breeder UID"];
                    if (!row.IsNull("Parent 1 UID"))
                        pk.Parent1UID = (int) row["Parent 1 UID"];
                    if (!row.IsNull("Parent 2 UID"))
                        pk.Parent2UID = (int) row["Parent 2 UID"];
                    pk.CurrentHP = (int) row["Current HP"];
                    pk.Alive = (bool) row["Alive"];
                    if (!row.IsNull("Notes"))
                        pk.Notes = (string) row["Notes"];

                    #region Advancement Fetch

                    try
                    {
                        DataTable advancementTable = SQLData.DatatableFill(LoadDex.PokemonDB, FixedData.LevelUp,
                            LoadDex.tCnxn, $"[Base UID] = {pk.UID}");

                        foreach (DataRow aRow in advancementTable.Rows)
                        {
                            Advancements advance = new Advancements();
                            advance.LevelUpUID = (int) aRow["Level Up UID"];
                            advance.Modifier = (int) aRow["Modifier"];
                            if(!aRow.IsNull("Notes"))
                                advance.Notes = (string) aRow["Notes"];
                            advance.UID = (int) aRow["UID"];
                            advance.ValueAdd = (bool) aRow["Add Value"];
                            advance.DateAdded = (DateTime) aRow["Date Added"];
                            pk.LevelUps.Add(advance);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Pokemon Advancement fetch threw exception for UID {PkID}: {Environment.NewLine}{Environment.NewLine}{ex}");
                    }

                    #endregion



                    player.OwnedPokemon.Add(pk);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Owned Pokemon Fetch threw exception for UID {PkID}: {Environment.NewLine}{Environment.NewLine}{ex}");
            }

            foreach(var pkmn in player.OwnedPokemon)
                pkmn.Advance();

            StaticData.ReferenceData.TrainerDex.LoadedTrainers.Add(player);


        }

        private void cbx_Active_Player_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (!Load)
            {
                if (cbx_Active_Player.SelectedIndex > -1)
                {
                    StaticData.PlayerData =
                        StaticData.ReferenceData.TrainerDex.LoadedTrainers[cbx_Active_Player.SelectedIndex];

                    MessageBox.Show("Please right click on the Trainer card to update the main page");

                    this.Close();
                }
            }
            else
                Load = false;
        }

        private void btn_AddNewPlayer_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                List<DataTableFeatures> Columns = new List<DataTableFeatures>();
                Columns.Add(new DataTableFeatures() {ColumnName = "Player_Name", ColumnType = typeof(string)});
                Columns.Add(new DataTableFeatures() {ColumnName = "Character_Name", ColumnType = typeof(string)});
                Columns.Add(new DataTableFeatures() {ColumnName = "Money", ColumnType = typeof(int)});
                Columns.Add(new DataTableFeatures() {ColumnName = "Strength", ColumnType = typeof(int)});
                Columns.Add(new DataTableFeatures() {ColumnName = "Dexterity", ColumnType = typeof(int)});
                Columns.Add(new DataTableFeatures() {ColumnName = "Constitution", ColumnType = typeof(int)});
                Columns.Add(new DataTableFeatures() {ColumnName = "Intelligence", ColumnType = typeof(int)});
                Columns.Add(new DataTableFeatures() {ColumnName = "Wisdom", ColumnType = typeof(int)});
                Columns.Add(new DataTableFeatures() {ColumnName = "Charisma", ColumnType = typeof(int)});
                Columns.Add(new DataTableFeatures() {ColumnName = "Max_HP", ColumnType = typeof(int)});
                Columns.Add(new DataTableFeatures() {ColumnName = "Current_HP", ColumnType = typeof(int)});
                Columns.Add(new DataTableFeatures() {ColumnName = "Description", ColumnType = typeof(string)});
                Columns.Add(new DataTableFeatures() {ColumnName = "Notes", ColumnType = typeof(string)});


                DataTable DT = SQLData.MakeDT(Columns);

                DataRow row = DT.NewRow();
                row[Columns[0].ColumnName] = tb_PlayerName.Text;
                row[Columns[1].ColumnName] = tb_CharacterName.Text;
                row[Columns[2].ColumnName] = 2000;
                row[Columns[3].ColumnName] = Inud_Strength.Value.Value;
                row[Columns[4].ColumnName] = Inud_Dexterity.Value.Value;
                row[Columns[5].ColumnName] = Inud_Constitution.Value.Value;
                row[Columns[6].ColumnName] = Inud_Intelligence.Value.Value;
                row[Columns[7].ColumnName] = Inud_Wisdom.Value.Value;
                row[Columns[8].ColumnName] = Inud_Charisma.Value.Value;
                row[Columns[9].ColumnName] = (Inud_Constitution.Value.Value * 4) + 4;
                row[Columns[10].ColumnName] = (Inud_Constitution.Value.Value * 4) + 4;
                row[Columns[11].ColumnName] = tb_Description.Text;
                row[Columns[12].ColumnName] = tb_Notes.Text;
                DT.Rows.Add(row);

                SQLData.SQLInsert(DT, LoadDex.TrainerDB, FixedData.PlayerSummary, LoadDex.tCnxn);
                LoadDex.LoadTrainerDex();
                MessageBox.Show("Operation Complete.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Exception thrown trying to make new Trainer:  {Environment.NewLine}{Environment.NewLine}{ex}");
            }
        }

        private void btn_AddNewPokemon_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<DataTableFeatures> Columns = new List<DataTableFeatures>();
                Columns.Add(new DataTableFeatures() {ColumnName = "Base Pokemon UID", ColumnType = typeof(int)});
                Columns.Add(new DataTableFeatures() {ColumnName = "Trainer UID", ColumnType = typeof(int)});
                Columns.Add(new DataTableFeatures() {ColumnName = "Actual Sex", ColumnType = typeof(string)});
                Columns.Add(new DataTableFeatures() {ColumnName = "Capture Trainer UID", ColumnType = typeof(int)});
                Columns.Add(new DataTableFeatures() {ColumnName = "Breeder UID", ColumnType = typeof(int)});
                Columns.Add(new DataTableFeatures() {ColumnName = "Parent 1 UID", ColumnType = typeof(int)});
                Columns.Add(new DataTableFeatures() {ColumnName = "Parent 2 UID", ColumnType = typeof(int)});
                Columns.Add(new DataTableFeatures() {ColumnName = "Current HP", ColumnType = typeof(int)});
                Columns.Add(new DataTableFeatures() {ColumnName = "Alive", ColumnType = typeof(bool)});
                Columns.Add(new DataTableFeatures() {ColumnName = "Nature UID", ColumnType = typeof(int)});
                Columns.Add(new DataTableFeatures() {ColumnName = "Nickname", ColumnType = typeof(string)});
                Columns.Add(new DataTableFeatures() {ColumnName = "Notes", ColumnType = typeof(string)});


                DataTable DT = SQLData.MakeDT(Columns);

                DataRow row = DT.NewRow();
                row[Columns[0].ColumnName] = ((Pokemon) cbx_BasePokemon.SelectionBoxItem).UID;
                row[Columns[1].ColumnName] = StaticData.PlayerData.UID;
                row[Columns[2].ColumnName] = ((ComboBoxItem)cbx_Sex.SelectedItem).Content;
                row[Columns[3].ColumnName] = StaticData.PlayerData.UID;
                row[Columns[4].ColumnName] = ((PlayerStub)cbx_Breeder.SelectedItem).UID;
                row[Columns[5].ColumnName] = ((TrainerPokemon) cbx_Parent1.SelectionBoxItem).UID;
                row[Columns[6].ColumnName] = ((TrainerPokemon) cbx_Parent2.SelectionBoxItem).UID;
                row[Columns[7].ColumnName] = 1;
                row[Columns[8].ColumnName] = true;
                row[Columns[9].ColumnName] = ((InherentNature) cbx_Nature.SelectedItem).UID;
                row[Columns[10].ColumnName] = tb_PokemonNickname.Text;
                row[Columns[11].ColumnName] = tb_PokemonNotes.Text;
                DT.Rows.Add(row);

                SQLData.SQLInsert(DT, LoadDex.PokemonDB, FixedData.TrainerPokemon, LoadDex.pCnxn);
                LoadDex.LoadTrainerDex();
                MessageBox.Show("Operation Complete.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Exception thrown trying to make new Pokemon:  {Environment.NewLine}{Environment.NewLine}{ex}");
            }
        }

        private void btn_Reload_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Warning, this may take a moment.");
            LoadDex.LoadSQLData();
            DataBindings();
            MessageBox.Show("Operation Complete.");
        }
    }

    
}