using DisapeardPeople.MVC.Data.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DisapeardPeople.MVC.Models
{
    public class DisappearPerson
    {
        public int Id { get; set; }
       
        [DisplayName("Imie")]
        public string FirstName { get; set; }

        [DisplayName("Nazwisko")]
        public string Surname { get; set; }

        [DisplayName("Wiek")]
        public int Age { get; set; }

        [DisplayName("Wzrost")]
        public int Height { get; set; }

        [DisplayName("Ostatnie miejsce pobytu")]
        public string City { get; set; }

        [DisplayName("Zdjęcie")]
        public string? Image { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dddd, dd MMMM yyyy}", ApplyFormatInEditMode = false)]
        [DisplayName("Data zaginięcia")]
        public DateTime DisappearDate { get; set; }
        
        [DisplayName("Województwo")]
        public Province Province { get; set; }

        [DisplayName("Płeć")]
        public Gender Gender { get; set; }

        [NotMapped]
        [DisplayName("Załącz zdjęcie zaginionego")]
        [BindNever]
        public IFormFile? ImageFile { get; set; }
    }
}
