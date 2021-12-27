using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Domain.Entity.Enum
{
    [Flags]
    public enum EnumFirmStatusType
    {
        [EnumMember]
        [Description("Seçimsiz")]
        Secimsiz = 0,

        [EnumMember]
        [Description("Limited")]
        Limited = 1,

        [EnumMember]
        [Description("Anonim")]
        Anonim = 2,

        [EnumMember]
        [Description("Şahıs")]
        Sahis = 3,

        [EnumMember]
        [Description("Aktif")]
        Kollektif = 4,

        [EnumMember]
        [Description("Komandit")]
        Komandit = 5,

        [EnumMember]
        [Description("Diğer")]
        Diger = 6,

        [EnumMember]
        [Description("Adi Ortaklık")]
        AdiOrtaklik = 7,

        [EnumMember]
        [Description("Yurtdisi")]
        Yurtdisi = 8,
    }
}
