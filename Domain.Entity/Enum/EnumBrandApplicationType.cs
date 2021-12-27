using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Domain.Entity.Enum
{
    [Flags]
    public enum EnumBrandApplicationType
    {
        [EnumMember]
        [Description("Şahıs")]
        Sahis = 1,

        [EnumMember]
        [Description("Firma")]
        Firma = 2
    }
}
