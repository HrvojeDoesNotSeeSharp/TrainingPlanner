﻿using System;
using System.ComponentModel.DataAnnotations;
using TrainingPlanner.Helpers;

namespace TrainingPlanner.Models
{
    public class TestMetadata
    {
        [Required(ErrorMessage = "Unesite ime")]
        [StringLength(50, ErrorMessage = "Maksimum 50 znakova")]
        public string Name { get; set; }

        [StringLength(1000, ErrorMessage = "Maksimum 1000 znakova")]
        public string FunkcionalneSposobnosti { get; set; }

        [StringLength(1000, ErrorMessage = "Maksimum 1000 znakova")]
        public string MotorickeSposobnosti { get; set; }
    }
}