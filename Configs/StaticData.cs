using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonRPG.Configs
{
    public static class StaticData
    {
        public static MasterReferenceClass ReferenceData { get; set; } = new MasterReferenceClass();
        public static Player PlayerData { get; set; } = new Player();
    }
}
