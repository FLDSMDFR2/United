public enum GameModes
{
    None = 0,
    Normal,
    ThanosGauntlet,
    SinisterSix,
    GoldvsBlueTeam,
    TheHorsemenOfApocalypse,
    PhoenixFive,
    ClashOfHeroes_PvP,
    RegistrationClash_PvP,
    SinisterSixAssembled,
    TheComingOfGalactus,
    TheHeraldsOfGalactus,
    WinterGuard
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
                return "Gold vs Blue (Team)";
            case GameModes.TheHorsemenOfApocalypse:
                return "The Horsemen of Apocalypse";
            case GameModes.PhoenixFive:
                return "Phoenix Five";
            case GameModes.ClashOfHeroes_PvP:
                return "Clash of Heroes (PvP)";
            case GameModes.RegistrationClash_PvP:
                return "Registration Clash (PvP)";
            case GameModes.SinisterSixAssembled:
                return "Sinister Six Assembled";
            case GameModes.TheComingOfGalactus:
                return "The Coming of Galactus";
            case GameModes.TheHeraldsOfGalactus:
                return "The Heralds of Galactus";
            case GameModes.WinterGuard:
                return "Winter Guard";
            default:
                return "";
        }
    }
}