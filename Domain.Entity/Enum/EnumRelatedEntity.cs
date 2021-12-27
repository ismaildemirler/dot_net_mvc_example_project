
using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Domain.Entity.Enum
{
    [Flags]
    public enum EnumRelatedEntity
    {
        [EnumMember]
        [Description("Seçimsiz")]
        Nothing = 0,

        [EnumMember]
        [Description("Marka Başvurusu")]
        BrandApplicationDetail = 1,

        [EnumMember]
        [Description("Patent Başvurusu")]
        PatentApplicationDetail = 2,

        [EnumMember]
        [Description("Alan Adı Başvurusu")]
        DomainApplicationDetail = 3,

        [EnumMember]
        [Description("Endüstriyel Tasarım Başvurusu")]
        IndustrialDesignApplicationDetail = 4,

        [EnumMember]
        [Description("Endüstriyel Tasarım Başvurusu Çoklu Fotoğraf")]
        IndustrialDesignApplicationPhotographs = 5
    }
}
