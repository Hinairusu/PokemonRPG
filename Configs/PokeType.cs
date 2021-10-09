﻿using System;
using System.Collections.Generic;

namespace PokemonRPG.Configs
{
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
}