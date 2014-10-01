using System;
using System.ComponentModel.DataAnnotations;

namespace TrainingPlanner.Models
{
    public class TestMetadata
    {
        //popraviti validaciju datuma
        [Required(ErrorMessage = "Unesite datum")]
        [DataType(DataType.Date, ErrorMessage = "Unesite ispravan format datuma npr. - 28/08/1984")]
        public DateTime DatumTesta { get; set; }

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
        [StringLength(50, ErrorMessage = "Maksimum 50 znakova")]
        public string Name { get; set; }
    }
}