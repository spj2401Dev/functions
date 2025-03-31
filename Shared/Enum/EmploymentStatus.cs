using System.ComponentModel;

namespace Functions.Shared.Enum
{
    public enum EmploymentStatus
    {
        [Description("Schüler")]
        Pupil,
        [Description("Student")]
        Student,
        [Description("Unbeschäftigt")]
        Unemployed,
    }
}
