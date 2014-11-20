using System.ComponentModel.DataAnnotations;
using TrainingPlanner.Helpers;

namespace TrainingPlanner.Models
{
    public class ClanMetadata
    {
        [Required(ErrorMessage = "Unesite ime")]
        [StringLength(30, ErrorMessage = "Maksimum 30 znakova")]
        public string Ime { get; set; }

        [Required(ErrorMessage = "Unesite prezime")]
        [StringLength(30, ErrorMessage = "Maksimum 30 znakova")]
        public string Prezime { get; set; }

        [Required(ErrorMessage = "Unesite datum rođenja")]
        [DateFormatValidator(ErrorMessage = "Unesite tocan format datuma - dd/mm/yyyy.")]
        public System.DateTime GodinaRodenja { get; set; }

        [Required(ErrorMessage = "Unesite visinu")]
        [StringLength(15, ErrorMessage = "Maksimum 15 znakova")]
        public string Visina { get; set; }

        [Required(ErrorMessage = "Unesite težinu")]
        [StringLength(15, ErrorMessage = "Maksimum 15 znakova")]
        public string Tezina { get; set; }        

        [StringLength(1000, ErrorMessage = "Maksimum 1000 znakova")]
        public string Napomena { get; set; }

        [StringLength(50, ErrorMessage = "Maksimum 50 znakova")]
        public string Sport { get; set; }
    }
}