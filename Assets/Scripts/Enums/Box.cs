public enum Box 
{
    None = 0,
    CoreSet_MU_S1,
    StretchGoals_MU_S1,
    TalesOfAsgard_MU_S1,
    GuardiansOfTheGalaxyRemix_MU_S1,
    RiseOfTheBlackPanther_MU_S1,
    EnterTheSpiderVerse_MU_S1,
    TheInfinitGauntlet_MU_S1,
    ReturnOfTheSinisterSix_MU_S1,
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

    SpiderGeddon_RO = 70,
    WitchingHour_RO
}

public static class BoxExtensions
{
    public static string ToFriendlyString(this Box num)
    {
        switch (num)
        {
            case Box.None:
                return "NONE";
            case Box.CoreSet_MU_S1:
                return "Marvel United Core Set";
            case Box.StretchGoals_MU_S1:
                return "S1 Stretch Goals";
            case Box.TalesOfAsgard_MU_S1:
                return "Tales of Asgard";
            case Box.GuardiansOfTheGalaxyRemix_MU_S1:
                return "Guardians of the Galaxy Remix";
            case Box.RiseOfTheBlackPanther_MU_S1:
                return "Rise of the Black Panther";
            case Box.EnterTheSpiderVerse_MU_S1:
                return "Enter the Spider-Verse";
            case Box.TheInfinitGauntlet_MU_S1:
                return "The Infinity Gauntlet";
            case Box.ReturnOfTheSinisterSix_MU_S1:
                return "Return of the Sinister Six";
            case Box.AdamWarlock_MU_S1:
                return "Adam Warlock";
            case Box.CoreSet_MXU_S2:
                return "X-Men United Core Set";
            case Box.StretchGoals_MXU_S2:
                return "S2 Stretch Goals";
            case Box.GoldTeam_MXU_S2:
                return "Gold Team";
            case Box.BlueTeam_MXU_S2:
                return "Blue Team";
            case Box.Deadpool_MXU_S2:
                return "Deadpool";
            case Box.TheHorsemenOfApocalypse_MXU_S2:
                return "The Horsemen of Apocalypse";
            case Box.XMenFirstClass_MXU_S2:
                return "X-Men: First Class";
            case Box.XMenXForce_MXU_S2:
                return "X-Men: X-Force";
            case Box.PhoenixFive_MXU_S2:
                return "Phoenix Five";
            case Box.DaysOfFuturePast_MXU_S2:
                return "Days of Future Past";
            case Box.FantasticFour_MXU_S2:
                return "Fantastic Four";
            case Box.CoreSet_MMU_S3:
                return "Multiverse Core Box";
            case Box.StretchGoals_MMU_S3:
                return "S3 Stretch Goals";
            case Box.CivilWar_MMU_S3:
                return "Civil War";
            case Box.WorldWarHulk_MMU_S3:
                return "World War Hulk";
            case Box.MaximumCarnage_MMU_S3:
                return "Maximum Carnage";
            case Box.FantasticFourTheComingOfGalactus_MMU_S3:
                return "Fantastic Four: The Coming of Galactus";
            case Box.SecretInvasion_MMU_S3:
                return "Secret Invasion";
            case Box.WarOfKings_MMU_S3:
                return "War of Kings";
            case Box.XMenTheAgeOfApocalypse_MMU_S3:
                return "X-Men: The Age of Apocalypse";
            case Box.Annihilation_MMU_S3:
                return "Annihilation";
            case Box.SpiderGeddon_RO:
                return "Marvel United: Spider-Geddon";
            case Box.WitchingHour_RO:
                return "Witching Hour";
            default:
                return "";
        }
    }
}
