public enum SingleItemGenerationType
{
    None,
    Mode,
    Challenge,
    Villain,
    Team,
    Hero,
    Companion,
    Location,
    Equipment,
    Campaign,
}

public static class SingleItemGenerationTypeExtensions
{
    public static string ToFriendlyString(this SingleItemGenerationType type)
    {
        switch (type)
        {
            case SingleItemGenerationType.None:
                return "NONE";
            case SingleItemGenerationType.Mode:
                return "MODE";
            case SingleItemGenerationType.Challenge:
                return "CHALLENGE";
            case SingleItemGenerationType.Villain:
                return "VILLAIN";
            case SingleItemGenerationType.Team:
                return "TEAM";
            case SingleItemGenerationType.Hero:
                return "HERO";
            case SingleItemGenerationType.Companion:
                return "COMPANION";
            case SingleItemGenerationType.Location:
                return "LOCATION";
            case SingleItemGenerationType.Equipment:
                return "EQUIPMENT";
            case SingleItemGenerationType.Campaign:
                return "CAMPAIGN";
            default:
                return "";
        }
    }
}