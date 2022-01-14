namespace PokemonRPG.Configs
{
    public class PlayerStub
    {
        public int UID { get; set; } = -1;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PlayerName { get; set; }
        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Constitution { get; set; }
        public int Intelligence { get; set; }
        public int Wisdom { get; set; }
        public int Charisma { get; set; }
        public int Money { get; set; }

        public int CurrentHP { get; set; }
        public int MaxHP { get; set; }

        public string Description { get; set; }
        public string Notes { get; set; }
        public override string ToString()
        {
            return $"{FirstName} {LastName} - {PlayerName}";
        }
    }
}