public enum GameChallenges
{
    None = 0,
    Random,
    ModerateChallenge,
    HardChallenge,
    HeroicChallenge,
    EndangeredLocationsChallenge,
    PlanBChallenge,
    TraitorChallenge,
    SecretIdentityChallenge,
    AccelerateVillainChallenge,
    DeadpoolChaosChallenge,
    DangerRoomChallenge,
    HazardousLocationsChallenge,
    SentinelChallenge1,
    SentinelChallenge2,
    SentinelChallenge3,
    TakeoverChallenge,
}

public static class GameChallengesExtensions
{
    public static string ToFriendlyString(this GameChallenges num)
    {
        switch (num)
        {
            case GameChallenges.None:
                return "NONE";
            case GameChallenges.ModerateChallenge:
                return "Moderate Challenge";
            case GameChallenges.HardChallenge:
                return "Hard Challenge";
            case GameChallenges.HeroicChallenge:
                return "Heroic Challenge";
            case GameChallenges.EndangeredLocationsChallenge:
                return "Endangered Locations Challenge";
            case GameChallenges.PlanBChallenge:
                return "Plan B Challenge";
            case GameChallenges.TraitorChallenge:
                return "Traitor Challenge";
            case GameChallenges.SecretIdentityChallenge:
                return "Secret Identity Challenge";
            case GameChallenges.AccelerateVillainChallenge:
                return "Accelerate Villain Challenge";
            case GameChallenges.DeadpoolChaosChallenge:
                return "Deadpool Chaos Challenge";
            case GameChallenges.DangerRoomChallenge:
                return "Danger Room Challenge";
            case GameChallenges.HazardousLocationsChallenge:
                return "Hazardous Locations Challenge";
            case GameChallenges.SentinelChallenge1:
                return "Sentinel Challenge 1";
            case GameChallenges.SentinelChallenge2:
                return "Sentinel Challenge 2";
            case GameChallenges.SentinelChallenge3:
                return "Sentinel Challenge 3";
            case GameChallenges.TakeoverChallenge:
                return "Takeover Challenge";
            default:
                return "";
        }
    }
}