using System.Numerics;

public enum Teams
{
    None = 0,
    UnitedHeroes,
    Avengers,
    NewAvengers,
    WestCoastAvengers,
    SavageAvengers,
    DarkAvengers,
    YoungAvengers,
    TeamCaptainAmericaSecretAvengers,
    TeamIronManProRegistration,
    SHIELD,
    ForceWorks,
    Defenders,
    DefendersManhattan,
    Wakandans,
    SpiderArmy,
    AsgardiansAllies,
    RedHulksThunderbolts,
    GuardiansOfTheGalaxy,
    InfinityWatch,
    Champions,
    MarvelKnights,
    MidnightSons,
    Illuminati,
    XMen,
    XForce,
    UncannyXForce,
    XFactor,
    AlphaFlight,
    NewMutants,
    ResistanceAgainstApocalypse,
    AForce,
    FantasticFour,
    DeadpoolTeamUp,
    Inhumans,
    Excalibur,
    GenX,
    Starjammers,
    SwordbearersOfKrakoa
}

public static class TeamsExtensions
{
    public static string ToFriendlyString(this Teams num)
    {
        switch (num)
        {
            case Teams.None:
                return "NONE";
            case Teams.UnitedHeroes:
                return "United Heroes";
            case Teams.Avengers:
                return "Avengers";
            case Teams.NewAvengers:
                return "New Avengers";
            case Teams.WestCoastAvengers:
                return "West Coast Avengers";
            case Teams.SavageAvengers:
                return "Savage Avengers";
            case Teams.DarkAvengers:
                return "Dark Avengers";
            case Teams.YoungAvengers:
                return "Young Avengers";
            case Teams.TeamCaptainAmericaSecretAvengers:
                return "Team Captain America Secret Avengers";
            case Teams.TeamIronManProRegistration:
                return "Team Iron Man Pro-Registration";
            case Teams.SHIELD:
                return "S.H.I.E.L.D.";
            case Teams.ForceWorks:
                return "Force Works";
            case Teams.Defenders:
                return "Defenders";
            case Teams.DefendersManhattan:
                return "Defenders (Manhattan)";
            case Teams.Wakandans:
                return "Wakandans";
            case Teams.SpiderArmy:
                return "Spider-Army";
            case Teams.AsgardiansAllies:
                return "Asgardians & Allies";
            case Teams.RedHulksThunderbolts:
                return "Red Hulk's Thunderbolts";
            case Teams.GuardiansOfTheGalaxy:
                return "Guardians of the Galaxy";
            case Teams.InfinityWatch:
                return "Infinity Watch";
            case Teams.Champions:
                return "Champions";
            case Teams.MarvelKnights:
                return "Marvel Knights";
            case Teams.MidnightSons:
                return "Midnight Sons";
            case Teams.Illuminati:
                return "Illuminati";
            case Teams.XMen:
                return "XMen";
            case Teams.XForce:
                return "X-Force";
            case Teams.UncannyXForce:
                return "Uncanny X-Force";
            case Teams.XFactor:
                return "X-Factor";
            case Teams.AlphaFlight:
                return "Alpha Flight";
            case Teams.NewMutants:
                return "New Mutants";
            case Teams.ResistanceAgainstApocalypse:
                return "Resistance Against Apocalypse";
            case Teams.AForce:
                return "A-Force";
            case Teams.FantasticFour:
                return "Fantastic Four";
            case Teams.DeadpoolTeamUp:
                return "Deadpool Team-Up";
            case Teams.Inhumans:
                return "Inhumans";
            case Teams.Excalibur:
                return "Excalibur";
            case Teams.GenX:
                return "Gen-X";
            case Teams.Starjammers:
                return "Starjammers";
            case Teams.SwordbearersOfKrakoa:
                return "Swordbearers of Krakoa";


            default:
                return "";
        }
    }
}