namespace PokemonRPG.Configs
{
    public class AdvancementTypes
    {
        public int UID { get; set; }
        public string ChangeType { get; set; }
        public override string ToString()
        {
            return ChangeType;
        }
    }
}