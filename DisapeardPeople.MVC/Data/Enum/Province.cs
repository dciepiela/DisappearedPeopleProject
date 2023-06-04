using System.ComponentModel.DataAnnotations;

namespace DisapeardPeople.MVC.Data.Enum
{
    public enum Province
    {
        Dolnośląskie,
        [Display(Name = "Kujawsko-pomorskie")]
        KujawskoPomorskie,
        Lubelskie,
        Lubuskie,
        Łódzkie,
        Małopolskie,
        Mazowieckie,
        Opolskie,
        Podkarpackie,
        Podlaskie,
        Pomorskie,
        Śląskie,
        Świętokrzyskie,
        [Display(Name = "Warmińsko-Mazurskie")]
        WarmińskoMazurskie,
        Wielkopolskie,
        Zachodniopomorskie
    }
}
