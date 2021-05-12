using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonRPG.Configs
{
    public class Abilitydex
    {
        public List<Ability> AbilityList { get; set; }
        public List<Keywords> KeywordList { get; set; }
        public List<Capabilities> CapabilityList { get; set; }

        public Abilitydex()
        {
            AbilityList = new List<Ability>();
            KeywordList = new List<Keywords>();
            CapabilityList = new List<Capabilities>();
        }
    }

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

    public class Capabilities
    {
        public int UID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class Keywords
    {
        public int UID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
