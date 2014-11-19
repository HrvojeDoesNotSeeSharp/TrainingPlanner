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

        [StringLength(1000, ErrorMessage = "Maksimum 1000 znakova")]
        public string FunkcionalneSposobnosti { get; set; }

        [StringLength(1000, ErrorMessage = "Maksimum 1000 znakova")]
        public string MotorickeSposobnosti { get; set; }

        [StringLength(1000, ErrorMessage = "Maksimum 1000 znakova")]
        public string NapomenaFunkcionalneSposobnosti { get; set; }

        [StringLength(1000, ErrorMessage = "Maksimum 1000 znakova")]
        public string NapomenaMotorickeSposobnosti { get; set; }
    }
}