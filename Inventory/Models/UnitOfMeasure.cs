﻿using System.ComponentModel.DataAnnotations;

namespace Inventory.Models
{
    public class UnitOfMeasure
    {
        public int UnitOfMeasureId { get; set; }
        [Required]
        public string UnitOfMeasureName { get; set; }
        public string Description { get; set; }
    }
}
