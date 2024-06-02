using static System.Runtime.InteropServices.JavaScript.JSType;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace E_recrutement.Models
{
    public enum TypeContrat { CDD, CDI, INTERN }
    public enum Profile { Deug, Licence, Master, Ingenieur }

    public class Offer
    {
        [Key]
        public int Id { get; set; }
        public string? rectuteurId { get; set; }

        [DisplayName("Contract Type")]
        public TypeContrat Type { get; set; }
        [DisplayName("Offer's Sector")]
        public string Secteur { get; set; }
        [DisplayName("Profile")]
        public Profile Profil { get; set; }
        [DisplayName("Poste")]
        public string Poste { get; set; }
        [DisplayName("Salary")]
        public string Remuneration { get; set; }
        [DisplayName("Offer Description")]
        public string Description { get; set; }
        [DisplayName("Offer Responsibilities")]
        public string Responsibilities { get; set; }
        [DisplayName("Offer Qualifications")]
        public string Qualifications { get; set; }
        [DisplayName("Offer Location")]
        public string Location { get; set; }
        public DateOnly DatePub { get; set; }
        [DisplayName("Offer Date Line")]
        public DateOnly DateLine { get; set; }
        [DisplayName("Company Name")]
        public string? Company { get; set; }
        public string? UrlCompanyLogo { get; set; }
        public int? minSalary { get; set; }  
        public int? maxSalary { get; set; }
    }
}
