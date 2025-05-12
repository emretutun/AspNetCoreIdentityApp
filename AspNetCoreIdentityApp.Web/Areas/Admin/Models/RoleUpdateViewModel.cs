using System.ComponentModel.DataAnnotations;

namespace AspNetCoreIdentityApp.Web.Areas.Admin.Models
{
    public class RoleUpdateViewModel
    {

        
        public string Id { get; set; } = null!;
        
        [Required(ErrorMessage = "Rol ismi zorunludur.")]
        [Display(Name = "Rol ismi")]
        public string Name { get; set; }

    }
}
