public enum GameSystems
{
    None = 0,
    All,
    Marvel,
    DC
}

public static class GameSystemsExtensions
{
    public static string ToFriendlyString(this GameSystems num)
    {
        switch (num)
        {
            case GameSystems.None:
                return "NONE";
            case GameSystems.All:
                return "ALL";
            case GameSystems.Marvel:
                return "MARVEL";
            case GameSystems.DC:
                return "DC";
            default:
                return "";
        }
    }
}