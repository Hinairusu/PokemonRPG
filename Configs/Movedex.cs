using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonRPG.Configs
{
    [Serializable]
    public class Movedex
    {
        public List<PokemonMove> MoveList { get; set; }
        public Movedex()
        {
            MoveList = new List<PokemonMove>();
        }
    }
}
