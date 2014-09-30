using System.ComponentModel.DataAnnotations;

namespace TrainingPlanner.Models
{
    public class IstezanjePopisMetadata
    {
        [Required(ErrorMessage = "Unesite naziv")]
        [StringLength(15, ErrorMessage = "Maksimum 15 znakova")]
        public string Naziv { get; set; }

        [StringLength(50, ErrorMessage = "Maksimum 50 znakova")]
        public string Info { get; set; }
    }
}