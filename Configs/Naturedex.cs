using System.Collections.Generic;

namespace PokemonRPG.Configs
{
    public class Naturedex
    {
        public Naturedex()
        {
            Natures = new List<InherentNature>();
        }

        public List<InherentNature> Natures { get; set; }
    }
}