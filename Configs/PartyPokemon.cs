using System;
using System.Linq;

namespace PokemonRPG.Configs
{
    public class PartyPokemon
    {
        public int Slot { get; set; }
        public int PokemonUID { get; set; }

        public override string ToString()
        {
            var pkmn = StaticData.PlayerData.OwnedPokemon.Single(s => s.UID.Equals(PokemonUID));
            

            return $"{pkmn.Nickname} ({pkmn.Name}) {Environment.NewLine} - Level {pkmn.Level} {Environment.NewLine} HP: {pkmn.CurrentHP} / {pkmn.MaxHP}";
        }
    }
}