using System.Collections.Generic;

namespace PokemonRPG.Configs
{
    public class Naturedex
    {
        public Naturedex()
        {
            Natures = new List<InherentNature>();
            Flavours = new List<Flavours>();
            Enviroments = new List<PkEnvironment>();
            EggGroups = new List<EggGroup>();
        }

        public List<InherentNature> Natures { get; set; }
        public List<Flavours> Flavours { get; set; }
        public List<PkEnvironment> Enviroments { get; set; }
        public List<EggGroup> EggGroups { get; set; }
    }
}