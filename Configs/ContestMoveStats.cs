namespace PokemonRPG.Configs
{
    public class ContestMoveStats
    {
        public ContestMoveStats()
        {
            Appeal = new Dice();
        }

        public int UID { get; set; }
        public int Type { get; set; }
        public Dice Appeal { get; set; }
        public string Effect { get; set; }
    }
}