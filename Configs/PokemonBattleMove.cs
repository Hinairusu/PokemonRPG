namespace PokemonRPG.Configs
{
    public class PokemonBattleMove
    {
        public int Slot { get; set; }

        public PokemonMove Move { get; set; }
        public override string ToString()
        {
            return $"{Move}";
        }
    }
}