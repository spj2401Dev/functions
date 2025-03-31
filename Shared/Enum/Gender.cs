using System.ComponentModel;

namespace Functions.Shared.Enum
{
    public enum Gender
    {
        [Description("Frau")]
        Female,
        [Description("Mann")]
        Male,
        [Description("Divers")]
        Other,
        [Description("Lieber nichts angeben")]
        PreferNotToSay
    }
}
