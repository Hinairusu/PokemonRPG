using System;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Windows;
using PokemonRPG.Configs;

namespace PokemonRPG.Windows
{
    /// <summary>
    ///     Interaction logic for Trainer.xaml
    /// </summary>
    public partial class TrainerPage : Window
    {
        public static bool Reload = false;
        public TrainerPage()
        {
            InitializeComponent();
            DataBindings();
        }

        public void DataBindings()
        {
            DataBinding.BindThis(cbx_Players, StaticData.ReferenceData.TrainerDex, "LoadedTrainers");
            DataBinding.BindThis(cbx_Active_Player, StaticData.ReferenceData.TrainerDex, "LoadedTrainers");
            DataBinding.BindThis(cbx_PlayerStubs, StaticData.ReferenceData.TrainerDex, "Trainers");
        }

        private void btn_Unload_Click(object sender, RoutedEventArgs e)
        {
            if (cbx_Players.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a loaded character to unload");
                return;
            }

            StaticData.ReferenceData.TrainerDex.LoadedTrainers.RemoveAt(cbx_Players.SelectedIndex);
            
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
                DataTable CurrentPartyID = SQLData.DatatableFill(MainWindow.TrainerDB,FixedData.CurrentParty, MainWindow.tCnxn,$"[Trainer UID] = {player.UID}");

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
                DataTable PokemonList = SQLData.DatatableFill(MainWindow.PokemonDB, FixedData.TrainerPokemon,
                    MainWindow.tCnxn, $"[Trainer UID] = {player.UID}");

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
                        DataTable advancementTable = SQLData.DatatableFill(MainWindow.PokemonDB, FixedData.LevelUp,
                            MainWindow.tCnxn, $"[Base UID] = {pk.UID}");

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
            if (cbx_Active_Player.SelectedIndex > -1)
            {
                StaticData.PlayerData =
                    StaticData.ReferenceData.TrainerDex.LoadedTrainers[cbx_Active_Player.SelectedIndex];
                if(!Reload)
                    MessageBox.Show("Please right click on the Trainer card to update the main page");
                Reload = true;
            }
        }
    }
}