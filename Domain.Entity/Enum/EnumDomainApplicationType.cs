using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Domain.Entity.Enum
{
    [Flags]
    public enum EnumDomainApplicationType
    {
        [EnumMember]
        [Description("İlk Başvuru")]
        Ilk_Basvuru = 1,

        [EnumMember]
        [Description("Yenileme")]
        Yenileme = 2
    }
}
