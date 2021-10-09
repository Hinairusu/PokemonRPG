using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonRPG.Configs
{
    public class Trainerdex
    {
        public List<TrainerClass> Classes { get; set; }
        public Trainerdex()
        {
            Classes = new List<TrainerClass>();
        }
    }
}
