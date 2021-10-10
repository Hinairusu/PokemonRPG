namespace PokemonRPG.Configs
{
    public class ContestMoveStats
    {
        public ContestMoveStats()
        {
            Appeal = new Dice();
        }

        public ContestType Type { get; set; }
        public Dice Appeal { get; set; }
        public string Effect { get; set; }
    }
}