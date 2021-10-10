using System;

namespace PokemonRPG.Configs
{
    [Serializable]
    public class BaseCharacteristics
    {
        public int HP { get; set; }
        public int Attack { get; set; }
        public int Defence { get; set; }
        public int SpecialAttack { get; set; }
        public int SpecialDefence { get; set; }
        public int Speed { get; set; }

        public static BaseCharacteristics operator +(BaseCharacteristics v1, BaseCharacteristics v2)
        {
            var newbase = new BaseCharacteristics();
            newbase.HP = v1.HP + v2.HP;
            newbase.Attack = v1.Attack + v2.Attack;
            newbase.Defence = v1.Defence + v2.Defence;
            newbase.Speed = v1.Speed + v2.Speed;
            newbase.SpecialDefence = v1.SpecialDefence + v2.SpecialDefence;
            newbase.SpecialAttack = v1.SpecialAttack + v2.SpecialAttack;
            return newbase;
        }
    }
}