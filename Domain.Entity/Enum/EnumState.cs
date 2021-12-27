using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Domain.Entity.Enum
{
    [Flags]
    public enum EnumState
    {
        [EnumMember]
        [Description("Pasif")]
        InActive = 0,

        [EnumMember]
        [Description("Aktif")]
        Active = 1
    }
}
