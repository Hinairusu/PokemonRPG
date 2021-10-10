using System.Collections.Generic;

namespace PokemonRPG.Configs
{
    public class Trainerdex
    {
        public Trainerdex()
        {
            Classes = new List<TrainerClass>();
        }

        public List<TrainerClass> Classes { get; set; }
    }
}