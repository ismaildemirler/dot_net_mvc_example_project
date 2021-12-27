
using System.ComponentModel.DataAnnotations;

namespace Domain.Web.Models.Domain
{
    public class DomainSearchViewModel
    {
        [StringLength(250, ErrorMessage = "{0} alanına maksimum {1} karakter girilebilir")]
        public string DomainName { get; set; }
    }
}