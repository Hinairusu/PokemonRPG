using System.Collections.Generic;

namespace PokemonRPG.Configs
{
    public class Ability
    {
        public int UID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Frequency { get; set; }
        public List<string> Keywords { get; set; }
        public string Effect { get; set; }
        public Ability()
        {
            Keywords = new List<string>();
        }
    }
}