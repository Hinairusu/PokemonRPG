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
}
