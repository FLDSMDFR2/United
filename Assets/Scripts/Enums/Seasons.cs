public enum Seasons
{
    None = 0,
    RetailOnly,
    MU_S1,
    MXU_S2,
    MMU_S3
}

public static class SeasonsExtensions
{
    public static string ToFriendlyString(this Seasons num)
    {
        switch (num)
        {
            case Seasons.None:
                return "NONE";
            case Seasons.RetailOnly:
                return "RETAIL";
            case Seasons.MU_S1:
                return "MU S1";
            case Seasons.MXU_S2:
                return "MXU S2";
            case Seasons.MMU_S3:
                return "MMU S3";
            default:
                return "";
        }
    }
}