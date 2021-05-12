using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonRPG.Configs
{
    public class Typedex
    {
        public List<PokeType> TypeList { get; set; }
        public Typedex()
        {
            TypeList = new List<PokeType>();
        }

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
                decimal TypeOneMultipler = type.TypeInteraction.Find(s => s.TypeID.Equals(MoveType)).Multiplier;

                var typeTwo = TypeList.Find(s => s.UID.Equals(PkmnSecondaryType));
                decimal TypeTwoMultipler = typeTwo.TypeInteraction.Find(s => s.TypeID.Equals(MoveType)).Multiplier;

                return TypeOneMultipler * TypeTwoMultipler;
            }
            catch
            {
                return -1;
            }
        }
    }

    [Serializable]
    public class PokeType
    {
        public int UID { get; set; }
        public string Name { get; set; }
        public List<TypeMultiplier> TypeInteraction { get; set; }
        public PokeType()
        {
            TypeInteraction = new List<TypeMultiplier>();
        }
    }

    public class TypeMultiplier
    {
        public int TypeID { get; set; }
        public decimal Multiplier { get; set; }
    }



}
