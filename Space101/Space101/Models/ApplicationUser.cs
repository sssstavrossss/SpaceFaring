using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Space101.ViewModels.IdentityViewModels;

namespace Space101.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, 
    // please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Homeplanet")]
        public int? PlanetID { get; set; }

        [Display(Name = "Race")]
        public int? RaceID { get; set; }


        [Required]
        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "Nickame")]
        public string NickName { get; set; }

        public Planet Planet { get; set; }

        public Race Race { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            userIdentity.AddClaim(new Claim("NickName",NickName.ToString()));
            return userIdentity;
        }

        //FOR NOW ITS PUBLIC BECAUSE IT IS USED IN ACCOUNT CONTROLLER BY MICROSOFT
        public ApplicationUser() { }

        private ApplicationUser(string email, string firstName, string lastName, string nickName, int? raceID, int? planetID)
        {
            UserName = email;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            NickName = nickName;
            if (raceID != null)
                RaceID = raceID.Value;
            else
                RaceID = null;
            if (planetID != null)
                PlanetID = planetID.Value;
            else
                PlanetID = null;
        }

        public static ApplicationUser ApplicationUserRegistration(RegisterViewModel viewModel)
        {
            return new ApplicationUser(viewModel.Email, viewModel.FirstName, viewModel.LastName, 
                viewModel.NickName, viewModel.PlanetID, viewModel.RaceID);
        }

        public static ApplicationUser AdminSeed(string email, string names)
        {
            return new ApplicationUser(email, names, names, names, null, null);
        }

    }
}