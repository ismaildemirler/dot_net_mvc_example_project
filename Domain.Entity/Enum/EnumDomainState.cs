using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Domain.Entity.Enum
{
    [Flags]
    public enum EnumDomainState
    {
        [EnumMember]
        [Description("Aktif")]
        Active = 1,

        [EnumMember]
        [Description("Pasif")]
        Inactive = 2,

        [EnumMember]
        [Description("Süresi Dolmuş")]
        Expired = 3,

        [EnumMember]
        [Description("Transfer Edilmiş")]
        Transferred = 4
    }
}
