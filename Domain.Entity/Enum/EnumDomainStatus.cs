using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Domain.Entity.Enum
{
    [Flags]
    public enum EnumDomainStatus
    {
        [EnumMember]
        [Display(Name = "Belirsiz")]
        Undefined = 0,

        [EnumMember]
        [Display(Name = "Uygun")]
        Available = 1,

        [EnumMember]
        [Display(Name = "Uygun Değil")]
        Not_Available = 2
    }
}
