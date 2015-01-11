using System;
using System.ComponentModel.DataAnnotations;
using TrainingPlanner.Helpers;

namespace TrainingPlanner.Models
{
    public class IstezanjeTreningTemplateMetadata
    {
        [Required(ErrorMessage = "Unesite naziv")]
        [StringLength(50, ErrorMessage = "Maksimum 50 znakova")]
        public string NazivPredloska { get; set; }
    }
}