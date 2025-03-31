using System.ComponentModel;

namespace Functions.Shared.Enum
{
    public enum SchoolType
    {
        [Description("Gymnasium")]
        HighSchool,
        [Description("Realschule")]
        SecondarySchool,
        [Description("Hauptschule")]
        MiddleSchool,
    }
}
