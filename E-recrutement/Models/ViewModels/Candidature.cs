using System.ComponentModel.DataAnnotations;

namespace E_recrutement.Models.ViewModels
{
    public class Candidature
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string CandidatId { get; set; }
        [Required]
        public int OfferId { get; set;}
        public string? MotivationLettre { get; set; }
        public DateOnly? DateApplication { get; set; }
    }
}
