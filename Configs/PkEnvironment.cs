namespace PokemonRPG.Configs
{
    public class PkEnvironment
    {
        public int UID { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}