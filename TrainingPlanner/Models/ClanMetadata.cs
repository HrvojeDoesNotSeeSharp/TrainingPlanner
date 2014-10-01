using System.ComponentModel.DataAnnotations;

namespace TrainingPlanner.Models
{
    public class ClanMetadata
    {
        [Required(ErrorMessage = "Unesite ime")]
        [StringLength(15, ErrorMessage = "Maksimum 15 znakova")]
        public string Ime { get; set; }

        [Required(ErrorMessage = "Unesite prezime")]
        [StringLength(15, ErrorMessage = "Maksimum 15 znakova")]
        public string Prezime { get; set; }

        [Required(ErrorMessage = "Unesite datum rođenja")]
        [DataType(DataType.Date, ErrorMessage = "Unesite ispravan format datuma npr. - 28/08/1984")]
        public System.DateTime GodinaRodenja { get; set; }

        [Required(ErrorMessage = "Unesite visinu")]
        [RegularExpression(@"^-*[0-9,\.]+$", ErrorMessage = "Unesite ispravan broj")]
        public string Visina { get; set; }

        [Required(ErrorMessage = "Unesite težinu")]
        [RegularExpression(@"^-*[0-9,\.]+$", ErrorMessage = "Unesite ispravan broj")]
        public string Tezina { get; set; }

        [StringLength(1000, ErrorMessage = "Maksimum 1000 znakova")]
        public string Amneza { get; set; }

        [StringLength(1000, ErrorMessage = "Maksimum 1000 znakova")]
        public string Napomena { get; set; }
    }
}