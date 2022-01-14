namespace PokemonRPG.Configs
{
    public class Flavours
    {
        public int UID { get; set; }
        public string Flavour { get; set; }

        public override string ToString()
        {
            return Flavour;
        }
    }
}