﻿using System;

namespace Codell.Pies.Web.Models.Pie
{
    public class UpdateIngredientPercentageModel
    {
        public Guid Id { get; set; }

        public int Percent { get; set; }

        public Guid PieId { get; set; }
    }
}