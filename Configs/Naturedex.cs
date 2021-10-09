using System.Collections.Generic;

namespace PokemonRPG.Configs
{
    public class Naturedex
    {
        public List<InherentNature> Natures { get; set; }
        public Naturedex()
        {
            Natures = new List<InherentNature>();
        }
    }
}