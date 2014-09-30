using System.ComponentModel.DataAnnotations;

namespace TrainingPlanner.Models
{
    public class ZagrijavanjePopisMetadata
    {
        [Required(ErrorMessage = "Unesite naziv")]
        [StringLength(50, ErrorMessage = "Maksimum 50 znakova")]
        public string Naziv { get; set; }

        [StringLength(1000, ErrorMessage = "Maksimum 1000 znakova")]
        public string Info { get; set; }
    }
}