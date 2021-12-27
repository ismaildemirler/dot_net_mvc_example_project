using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Domain.Entity.Container.Response.Services
{
    public class ResponseService
    {
        public int ServiceID { get; set; }
        public byte[] Icon { get; set; }
        public byte[] ServiceImage { get; set; }

        [Required]
        [StringLength(250)]
        [Display(Name = "Başlık")]
        public string Header { get; set; }
        
        [Required]
        [StringLength(1000)]
        [Display(Name = "Açıklama")]
        public string DescriptionText { get; set; }
        
        [Required]
        [Display(Name = "İçerik")]
        public string ContentText { get; set; }
        
        public string Buttons { get; set; }
        public HttpPostedFileBase FileIcon { get; set; }
        public HttpPostedFileBase FileImage { get; set; }
        public bool ImageDeleted { get; set; }
        public bool IconDeleted { get; set; }
    }
}
