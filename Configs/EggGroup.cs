namespace PokemonRPG.Configs
{
    public class EggGroup
    {
        public int UID { get; set; }
        public string Name { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}