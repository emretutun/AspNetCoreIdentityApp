using System.ComponentModel.DataAnnotations;

namespace AspNetCoreIdentityApp.Web.Areas.Admin.Models
{
    public class RoleCreateViewModel
    {
        [Required(ErrorMessage = "Rol ismi zorunludur.")]
        [Display(Name = "Rol ismi")]
        public string Name { get; set; }

    }
}
