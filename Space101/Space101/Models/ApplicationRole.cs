using Microsoft.AspNet.Identity.EntityFramework;

namespace Space101.Models
{
    public class ApplicationRole : IdentityRole
    {
        public bool Active { get; private set; }

        public void ToggleActivity() {
            if (Active)
                Active = false;
            else
                Active = true;
        }

        public void MakeActive()
        {
            Active = true;
        }

    }
}