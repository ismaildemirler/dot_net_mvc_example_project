using Domain.Infrastructure.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity.Concrete
{
    [Table("SystemUser")]
    public class SystemUser : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int SystemUserId { get; set; }
       
        [MaxLength(100)]
        [Required]
        public string FirstName { get; set; }
        
        [MaxLength(100)]
        [Required]
        public string LastName { get; set; }
        
        [MaxLength(50)]
        [Required]
        public string Password { get; set; }
       
        [MaxLength(250)]
        [Required]
        public string Email { get; set; }
        
        [MaxLength(50)]
        [Required]
        public string PhoneNumber { get; set; } 
       
        [Required]
        public byte StateId { get; set; }
        
        [Required]
        public byte UserTypeId { get; set; }

        public byte? Gender { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }
        
        public DateTime? UpdateDate { get; set; }
        public DateTime? LastLoginDate { get; set; }
    }
}
