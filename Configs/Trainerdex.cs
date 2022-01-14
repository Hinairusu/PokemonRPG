using System.Collections.Generic;

namespace PokemonRPG.Configs
{
    public class Trainerdex
    {
        public Trainerdex()
        {
            Classes = new List<TrainerClass>();
            LoadedTrainers = new List<Player>();
            Trainers = new List<PlayerStub>();
            Advances = new List<AdvancementTypes>();
            LevelUpAdvancements = new List<AdvancementTypes>();
        }
        public List<Player> LoadedTrainers { get; set; }
        public List<TrainerClass> Classes { get; set; }
        public List<PlayerStub> Trainers { get; set; }
        public List<AdvancementTypes> Advances { get; set; }
        public List<AdvancementTypes> LevelUpAdvancements { get; set; }
    }
}