public enum Seasons
{
    None = 0,
    MarvelRetailOnly,
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
            case Seasons.MarvelRetailOnly:
                return "MARVEL RETAIL";
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

    public static string ToLongFriendlyString(this Seasons num)
    {
        switch (num)
        {
            case Seasons.None:
                return "NONE";
            case Seasons.MarvelRetailOnly:
                return "MARVEL RETAIL";
            case Seasons.MU_S1:
                return "MARVEL UNITED SEASON 1";
            case Seasons.MXU_S2:
                return "MARVEL UNITED SEASON 2";
            case Seasons.MMU_S3:
                return "MARVEL UNITED SEASON 3";
            default:
                return "";
        }
    }
}