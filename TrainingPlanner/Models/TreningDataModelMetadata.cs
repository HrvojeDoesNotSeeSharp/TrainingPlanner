﻿using System.ComponentModel.DataAnnotations;
using TrainingPlanner.Helpers;

namespace TrainingPlanner.Models
{
    public class TreningDataModelMetadata
    {
        [Required(ErrorMessage = "Unesite ime treninga")]
        [StringLength(50, ErrorMessage = "Maksimum 50 znakova")]
        public string TreningImeTreninga { get; set; }

        [Required(ErrorMessage = "Unesite datum treninga")]
        [DateFormatValidator(ErrorMessage = "Unesite tocan format datuma - dd/mm/yyyy.")]
        public string TreningDatum { get; set; }

        [StringLength(50, ErrorMessage = "Maksimum 50 znakova")]
        public string TreningTip { get; set; }

        [StringLength(1000, ErrorMessage = "Maksimum 1000 znakova")]
        public string Napomena { get; set; }
    }
}