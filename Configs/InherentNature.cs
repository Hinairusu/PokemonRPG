namespace PokemonRPG.Configs
{
    public class InherentNature : BaseCharacteristics
    {
        public int UID { get; set; }
        public string Name { get; set; }
        public string FlavourLike { get; set; }
        public string FlavourHate { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}