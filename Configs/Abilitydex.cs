using System.Collections.Generic;

namespace PokemonRPG.Configs
{
    public class Abilitydex
    {
        public Abilitydex()
        {
            AbilityList = new List<Ability>();
            KeywordList = new List<Keywords>();
            CapabilityList = new List<Capabilities>();
        }

        public List<Ability> AbilityList { get; set; }
        public List<Keywords> KeywordList { get; set; }
        public List<Capabilities> CapabilityList { get; set; }
    }
}