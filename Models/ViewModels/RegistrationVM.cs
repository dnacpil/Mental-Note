using MentalNote.Areas.Identity.Pages.Account;
using MentalNote.Models;

namespace MentalNote.ViewModels{
    public class RegistrationVM{
        public List<RegisterModel>? Register {get; set;}
        public List<Provider>? RProvider {get; set;}
        public List<Individual>? RIndividual {get; set;}
    }
}