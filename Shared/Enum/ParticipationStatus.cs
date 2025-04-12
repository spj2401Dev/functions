using System.ComponentModel;

namespace Functions.Shared.Enum
{
    public enum ParticipationStatus
    {
        [Description("Unbekannt")] // da ist was ganz gewaltig schief gelaufen
        NotSet,
        [Description("Akzeptiert")]
        Accepted,
        [Description("Abgelehnt")]
        Declined,
        [Description("Noch unsicher")]
        Unsure
    }
}
