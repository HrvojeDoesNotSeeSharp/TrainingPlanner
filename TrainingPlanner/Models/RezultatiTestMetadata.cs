using System.ComponentModel.DataAnnotations;
using TrainingPlanner.Helpers;

namespace TrainingPlanner.Models
{
    public class RezultatiTestMetadata
    {
        [DateFormatValidator(ErrorMessage = "Unesite tocan format datuma - dd/mm/yyyy.")]
        public System.DateTime Datum { get; set; }

        [StringLength(50, ErrorMessage = "Maksimum 50 znakova")]
        public string Rezultat { get; set; }
    }
}