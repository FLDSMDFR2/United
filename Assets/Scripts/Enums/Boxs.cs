using UnityEngine.UIElements;

public enum Boxs
{
    None = 0,
    CoreSet_MU_S1,
    StretchGoals_MU_S1,
    TalesOfAsgard_MU_S1,
    GuardiansOfTheGalaxyRemix_MU_S1,
    RiseOfTheBlackPanther_MU_S1,
    EnterTheSpiderVerse_MU_S1,
    TheInfinityGauntlet_MU_S1,
    ReturnOfTheSinisterSix_MU_S1,
    Yondu_MU_S1,
    AdamWarlock_MU_S1,

    CoreSet_MXU_S2 = 20,
    StretchGoals_MXU_S2,
    GoldTeam_MXU_S2,
    BlueTeam_MXU_S2,
    Deadpool_MXU_S2,
    TheHorsemenOfApocalypse_MXU_S2,
    XMenFirstClass_MXU_S2,
    XMenXForce_MXU_S2,
    PhoenixFive_MXU_S2,
    DaysOfFuturePast_MXU_S2,
    FantasticFour_MXU_S2,
    StormMohawk_MXU_S2,
    OldManLogan_MXU_S2,

    CoreSet_MMU_S3 = 50,
    StretchGoals_MMU_S3,
    CivilWar_MMU_S3,
    WorldWarHulk_MMU_S3,
    MaximumCarnage_MMU_S3,
    FantasticFourTheComingOfGalactus_MMU_S3,
    SecretInvasion_MMU_S3,
    WarOfKings_MMU_S3,
    XMenTheAgeOfApocalypse_MMU_S3,
    Annihilation_MMU_S3,
    TeamDecks_MMU_S3,
    CampaignDecks_MMU_S3,
    PetCompanions_MMU_S3,
    FinFangFoom_MMU_S3,

    SpiderGeddon_RO = 70,
    WitchingHour_RO
}

public static class BoxsExtensions
{
    public static string ToFriendlyString(this Boxs num)
    {
        switch (num)
        {
            case Boxs.None:
                return "NONE";
            case Boxs.CoreSet_MU_S1:
                return "Marvel United Core Set";
            case Boxs.StretchGoals_MU_S1:
                return "S1 Stretch Goals";
            case Boxs.TalesOfAsgard_MU_S1:
                return "Tales of Asgard";
            case Boxs.GuardiansOfTheGalaxyRemix_MU_S1:
                return "Guardians of the Galaxy Remix";
            case Boxs.RiseOfTheBlackPanther_MU_S1:
                return "Rise of the Black Panther";
            case Boxs.EnterTheSpiderVerse_MU_S1:
                return "Enter the Spider-Verse";
            case Boxs.TheInfinityGauntlet_MU_S1:
                return "The Infinity Gauntlet";
            case Boxs.ReturnOfTheSinisterSix_MU_S1:
                return "Return of the Sinister Six";
            case Boxs.Yondu_MU_S1:
                return "Yondu";
            case Boxs.AdamWarlock_MU_S1:
                return "Adam Warlock";

            case Boxs.CoreSet_MXU_S2:
                return "X-Men United Core Set";
            case Boxs.StretchGoals_MXU_S2:
                return "S2 Stretch Goals";
            case Boxs.GoldTeam_MXU_S2:
                return "Gold Team";
            case Boxs.BlueTeam_MXU_S2:
                return "Blue Team";
            case Boxs.Deadpool_MXU_S2:
                return "Deadpool";
            case Boxs.TheHorsemenOfApocalypse_MXU_S2:
                return "The Horsemen of Apocalypse";
            case Boxs.XMenFirstClass_MXU_S2:
                return "X-Men: First Class";
            case Boxs.XMenXForce_MXU_S2:
                return "X-Men: X-Force";
            case Boxs.PhoenixFive_MXU_S2:
                return "Phoenix Five";
            case Boxs.DaysOfFuturePast_MXU_S2:
                return "Days of Future Past";
            case Boxs.FantasticFour_MXU_S2:
                return "Fantastic Four";
            case Boxs.StormMohawk_MXU_S2:
                return "Storm (Mohawk)";
            case Boxs.OldManLogan_MXU_S2:
                return "Old Man Logan";

            case Boxs.CoreSet_MMU_S3:
                return "Multiverse Core Set";
            case Boxs.StretchGoals_MMU_S3:
                return "S3 Stretch Goals";
            case Boxs.CivilWar_MMU_S3:
                return "Civil War";
            case Boxs.WorldWarHulk_MMU_S3:
                return "World War Hulk";
            case Boxs.MaximumCarnage_MMU_S3:
                return "Maximum Carnage";
            case Boxs.FantasticFourTheComingOfGalactus_MMU_S3:
                return "Fantastic Four: The Coming of Galactus";
            case Boxs.SecretInvasion_MMU_S3:
                return "Secret Invasion";
            case Boxs.WarOfKings_MMU_S3:
                return "War of Kings";
            case Boxs.XMenTheAgeOfApocalypse_MMU_S3:
                return "X-Men: The Age of Apocalypse";
            case Boxs.Annihilation_MMU_S3:
                return "Annihilation";
            case Boxs.TeamDecks_MMU_S3:
                return "S3 Team Decks";
            case Boxs.CampaignDecks_MMU_S3:
                return "S3 Campaign Decks";
            case Boxs.PetCompanions_MMU_S3:
                return "S3 Pet Companions";
            case Boxs.FinFangFoom_MMU_S3:
                return "Fin Fang Foom";

            case Boxs.SpiderGeddon_RO:
                return "Spider-Geddon";
            case Boxs.WitchingHour_RO:
                return "Witching Hour";
            default:
                return "";
        }
    }
}
