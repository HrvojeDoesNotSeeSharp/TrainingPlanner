using System.ComponentModel.DataAnnotations;
using TrainingPlanner.Helpers;

namespace TrainingPlanner.Models
{
    public class AmnezaMetadata
    {
        [Required(ErrorMessage = "Unesite ime")]
        [StringLength(30, ErrorMessage = "Maksimum 30 znakova")]
        public string Ime { get; set; }

        [StringLength(1000, ErrorMessage = "Maksimum 1000 znakova")]
        public string Opis { get; set; }
    }
}