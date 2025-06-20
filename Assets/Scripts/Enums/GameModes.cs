public enum GameModes
{
    None = 0,
    Normal,
    ThanosGauntlet,
    SinisterSix,
    GoldvsBlueTeam,
    TheHorsemenofApocalypse,
    PhoenixFive,
}

public static class GameModesExtensions
{
    public static string ToFriendlyString(this GameModes num)
    {
        switch (num)
        {
            case GameModes.None:
                return "NONE";
            case GameModes.Normal:
                return "Normal";
            case GameModes.ThanosGauntlet:
                return "Thanos Gauntlet";
            case GameModes.SinisterSix:
                return "Sinister Six";
            case GameModes.GoldvsBlueTeam:
                return "Gold vs Blue Team";
            case GameModes.TheHorsemenofApocalypse:
                return "The Horsemen of Apocalypse";
            case GameModes.PhoenixFive:
                return "Phoenix Five";
            default:
                return "";
        }
    }
}