using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using NugetNotify.Core.Attributes;

namespace NugetNotify.Core.ViewModels
{
    public class HomeIndexViewModel
    {
        [DisplayName("Package Name")]
        [Required]
        public string PackageName { get; set; }

        [AtLeastOneRequired("Email", "Twitter", "Telephone", ErrorMessage = "Please enter at least an Email, Twitter or Telephone notification")]
        public string Email { get; set; }

        public string Twitter { get; set; }
        
        public string Telephone { get; set; }
    }
}