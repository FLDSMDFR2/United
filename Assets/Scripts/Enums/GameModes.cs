public enum GameModes
{
    None = 0,
    ModerateChallenge,
    HardChallenge,
    HeroicChallenge,
    ThanosGauntlet,
    EndangeredLocationsChallenge,
    HiddenIdenityChallenge,
    PlanBChallenge,
    TraitorMechanic,
    SinisterSixMode,
    DeadpoolChaosChallenge,
    SentinalChallenge,
    TakeoverChallenge,
    HazardousLocationChallenge
}

public static class GameModesExtensions
{
    public static string ToFriendlyString(this GameModes num)
    {
        switch (num)
        {
            case GameModes.None:
                return "NONE";
            case GameModes.ModerateChallenge:
                return "Moderate Challenge";
            case GameModes.HardChallenge:
                return "Hard Challenge";
            case GameModes.HeroicChallenge:
                return "Heroic Challenge";
            default:
                return "";
        }
    }
}