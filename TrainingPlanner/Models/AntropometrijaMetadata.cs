using System;
using System.ComponentModel.DataAnnotations;
using TrainingPlanner.Helpers;

namespace TrainingPlanner.Models
{
    public class AntropometrijaMetadata
    {
        [Required(ErrorMessage = "Unesite ime")]
        [StringLength(50, ErrorMessage = "Maksimum 50 znakova")]
        public string Ime { get; set; }

        [StringLength(15, ErrorMessage = "Maksimum 15 znakova")]
        public string Visina { get; set; }

        [StringLength(15, ErrorMessage = "Maksimum 15 znakova")]
        public string Tezina { get; set; }

        [StringLength(15, ErrorMessage = "Maksimum 15 znakova")]
        public string PotkoznoMasnoTkivo { get; set; }

        [StringLength(15, ErrorMessage = "Maksimum 15 znakova")]
        public string BezmasnaMasa { get; set; }
    }
}