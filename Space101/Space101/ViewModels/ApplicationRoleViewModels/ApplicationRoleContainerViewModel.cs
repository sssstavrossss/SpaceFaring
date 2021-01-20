using System.Collections.Generic;
using Space101.Models;

namespace Space101.ViewModels.ApplicationRoleViewModels
{
    public class ApplicationRoleContainerViewModel
    {
        public List<ApplicationRole> ApplicationRoles { get; set; }
        public ApplicationRoleForm ApplicationRoleForm { get; set; }

        public ApplicationRoleContainerViewModel()
        {
            ApplicationRoles = new List<ApplicationRole>();
            ApplicationRoleForm = new ApplicationRoleForm();
        }

        public ApplicationRoleContainerViewModel(List<ApplicationRole> applicationRoles)
        {
            ApplicationRoles = applicationRoles;
            ApplicationRoleForm = new ApplicationRoleForm();
        }
    }
}