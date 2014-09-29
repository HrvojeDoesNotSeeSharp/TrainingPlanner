using System.ComponentModel.DataAnnotations;

namespace TrainingPlanner.Models
{
    public class TestMetadata
    {
        [Required(ErrorMessage = "Unesite datum")]
        [DataType(DataType.Date, ErrorMessage = "Unesite ispravan datum")]
        public System.DateTime DatumTesta { get; set; }

        [RegularExpression(@"^\s*-?[0-9]{1,5}\s*$", ErrorMessage = "Unesite ispravan broj")]
        public short Ergometar { get; set; }

        [RegularExpression(@"^\s*-?[0-9]{1,5}\s*$", ErrorMessage = "Unesite ispravan broj")]
        public short Zgibovi { get; set; }

        [RegularExpression(@"^\s*-?[0-9]{1,5}\s*$", ErrorMessage = "Unesite ispravan broj")]
        public short Sklekovi { get; set; }

        [RegularExpression(@"^\s*-?[0-9]{1,5}\s*$", ErrorMessage = "Unesite ispravan broj")]
        public short Trbusnjaci { get; set; }

        [RegularExpression(@"^\s*-?[0-9]{1,5}\s*$", ErrorMessage = "Unesite ispravan broj")]
        public short Cucnjevi { get; set; }

        [Required(ErrorMessage = "Unesite ime")]
        [StringLength(15, ErrorMessage = "Maksimum 15 znakova")]
        public string Name { get; set; }
    }
}