using System;
using System.ComponentModel.DataAnnotations;
using TrainingPlanner.Helpers;

namespace TrainingPlanner.Models
{
    public class TestMetadata
    {
        [Required(ErrorMessage = "Unesite datum")]
        [DateFormatValidator(ErrorMessage = "Unesite tocan format datuma - dd/mm/yyyy.")]
        public DateTime DatumTesta { get; set; }

        [Required(ErrorMessage = "Unesite ime")]
        [StringLength(50, ErrorMessage = "Maksimum 50 znakova")]
        public string Name { get; set; }

        [StringLength(15, ErrorMessage = "Maksimum 15 znakova")]
        public string Visina { get; set; }

        [StringLength(15, ErrorMessage = "Maksimum 15 znakova")]
        public string Tezina { get; set; }

        [StringLength(15, ErrorMessage = "Maksimum 15 znakova")]
        public string Antropometrija { get; set; }

        [StringLength(15, ErrorMessage = "Maksimum 15 znakova")]
        public string PotkoznoMasnoTkivo { get; set; }

        [StringLength(15, ErrorMessage = "Maksimum 15 znakova")]
        public string BezmasnaMasa { get; set; }

        [StringLength(1000, ErrorMessage = "Maksimum 1000 znakova")]
        public string FunkcionalneSposobnosti { get; set; }

        [StringLength(1000, ErrorMessage = "Maksimum 1000 znakova")]
        public string MotorickeSposobnosti { get; set; }
    }
}