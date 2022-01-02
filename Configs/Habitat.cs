using System;
using System.Linq;

namespace PokemonRPG.Configs
{
    [Serializable]
    public class Habitat
    {
        public int UID { get; set; }
        public int EnvironmentUID { get; set; }
        public decimal Rarity { get; set; }
        public string Notes { get; set; }

        public override string ToString()
        {
            return $"{Rarity} chance in {StaticData.ReferenceData.NatureDex.Enviroments.Single(s => s.UID.Equals(EnvironmentUID))}";
        }
    }
    public class PkEnvironment
    {
        public int UID { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}