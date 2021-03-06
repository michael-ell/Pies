﻿using System;

namespace Codell.Pies.Core.ReadModels
{
    [Serializable]
    public class Ingredient
    {
        public Guid Id { get; set; }

        public string Description { get; set; }

        public int Percent { get; set; }

        public string Color { get; set; }
    }
}