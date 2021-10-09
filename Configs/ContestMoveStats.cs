namespace PokemonRPG.Configs
{
    public class ContestMoveStats
    {
        public ContestType Type { get; set; }
        public Dice Appeal { get; set; }
        public string Effect { get; set; }
        public ContestMoveStats()
        {
            Appeal = new Dice();
        }
    }
}