using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Domain.Entity.Enum
{
    [Flags]
    public enum EnumAnonymousUser
    {
        [EnumMember]
        [Description("Anonim")]
        Anonim = 5
    }
}
