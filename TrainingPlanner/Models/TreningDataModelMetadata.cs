using System.ComponentModel.DataAnnotations;
using TrainingPlanner.Helpers;

namespace TrainingPlanner.Models
{
    public class TreningDataModelMetadata
    {
        [Required(ErrorMessage = "Unesite ime treninga")]
        [StringLength(15, ErrorMessage = "Maksimum 15 znakova")]
        public string TreningImeTreninga { get; set; }

        [Required(ErrorMessage = "Unesite datum treninga")]
        [DateFormatValidator(ErrorMessage = "Unesite tocan format datuma - dd/mm/yyyy.")]
        public string TreningDatum { get; set; }
    }
}