using System.Collections.Generic;

namespace PokemonRPG.Configs
{
    public class Typedex
    {
        public Typedex()
        {
            TypeList = new List<PokeType>();
        }

        public List<PokeType> TypeList { get; set; }

        public decimal CheckTypeOutcome(int MoveType, int PkmnPrimaryType, int PkmnSecondaryType)
        {
            if (MoveType > TypeList.Count)
                return -100;
            if (PkmnPrimaryType > TypeList.Count)
                return -100;
            if (PkmnSecondaryType > TypeList.Count)
                return -100;
            try
            {
                var type = TypeList.Find(s => s.UID.Equals(PkmnPrimaryType));
                var TypeOneMultipler = type.TypeInteraction.Find(s => s.TypeID.Equals(MoveType)).Multiplier;

                var typeTwo = TypeList.Find(s => s.UID.Equals(PkmnSecondaryType));
                var TypeTwoMultipler = typeTwo.TypeInteraction.Find(s => s.TypeID.Equals(MoveType)).Multiplier;

                return TypeOneMultipler * TypeTwoMultipler;
            }
            catch
            {
                return -1;
            }
        }
    }
}