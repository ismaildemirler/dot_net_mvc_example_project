using System;
using System.ComponentModel;

namespace Domain.Entity.Enum
{
    [Flags]
    public enum EnumDurationType
    {
        [Description("Yok")]
        unselected = 0,
        [Description("/Ay")]
        month = 1,
        [Description("/Yıl")]
        year = 2
    }
}
