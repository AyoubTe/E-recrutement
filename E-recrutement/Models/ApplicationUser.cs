using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace E_recrutement.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage = "Please write your Family name")]
        public string FamilyName { get; set; }
        [Required(ErrorMessage = "Please write your First name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please write your age")]
        public int Age { get; set; }

        public string? Titre { get; set; }
        public string? Diplome { get; set; }
        public Profile? Profil { get; set; } 
        public int? NbAnsExp { get; set; }
        public string? CV { get; set; }
        public string? Company { get; set; }
        public string? urlImageCompany { get; set; }
    }
}
