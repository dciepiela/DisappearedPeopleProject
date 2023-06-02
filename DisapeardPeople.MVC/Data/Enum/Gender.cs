using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace DisapeardPeople.MVC.Data.Enum
{
    public enum Gender
    {
        [Display(Name = "Mężczyzna")]
        Male,
        [Display(Name = "Kobieta")]
        Female,
        [Display(Name = "Inna")]
        Other
    }
}
