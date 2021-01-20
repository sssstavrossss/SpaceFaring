using Space101.Models;
using System.ComponentModel.DataAnnotations;

namespace Space101.ViewModels.ApplicationUserViewModels
{
    public class ApplicationUserContainerViewModel
    {
        [Display(Name = "User Id")]
        public string ID { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public string NickName { get; set; }
        public string Email { get; set; }

        [DisplayFormat(NullDisplayText = "No Race assigned")]
        public string Race { get; set; }

        [Display(Name = "Home Planet")]
        [DisplayFormat(NullDisplayText = "No Home Planet assigned")]
        public string HomePlanet { get; set; }

        public ApplicationUserContainerViewModel() { }

        private ApplicationUserContainerViewModel(ApplicationUser user)
        {
            ID = user.Id;
            if (user.Race != null)
                Race = user.Race.Name;
            if (user.Planet != null)
                HomePlanet = user.Planet.Name;
            Email = user.Email;
            FirstName = user.FirstName;
            LastName = user.LastName;
            NickName = user.NickName;
        }

        public static ApplicationUserContainerViewModel CreateNew(ApplicationUser user)
        {
            return new ApplicationUserContainerViewModel(user);
        }

    }
}