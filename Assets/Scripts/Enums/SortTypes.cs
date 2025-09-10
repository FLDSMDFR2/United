
public enum SortTypes
{
    None,
    Name,
    HeroWins,
    HeroLosses,
    HeroRating,
    VillainWins,
    VillainLosses,
    VillainRating,
    CompanionWins,
    CompanionLosses,
    CompanionRating,
    HeroMoveIcons,
    HeroAttackIcons,
    HeroHeroicIcons,
    HeroWildIcons, 
    HeroSpecailCards,
    HeroStartingHandCards,
    CompanionMoveIcons,
    CompanionAttackIcons,
    CompanionHeroicIcons,
    CompanionWildIcons,
    CompanionSpecailCards,
    CompanionStartingHandCards,
}

public static class SortTypesExtensions
{
    public static string ToFriendlyString(this SortTypes num)
    {
        switch (num)
        {
            case SortTypes.None:
                return "NONE";
            case SortTypes.Name:
                return "NAME";
            case SortTypes.HeroWins:
                return "HERO WINS";
            case SortTypes.HeroLosses:
                return "HERO LOSSES";
            case SortTypes.HeroRating:
                return "HERO RATING";
            case SortTypes.VillainWins:
                return "VILLAIN WINS";
            case SortTypes.VillainLosses:
                return "VILLAIN LOSSSES";
            case SortTypes.VillainRating:
                return "VILLAIN RATING";
            case SortTypes.CompanionWins:
                return "COMPANION WINS";
            case SortTypes.CompanionLosses:
                return "COMPANION LOSSSES";
            case SortTypes.CompanionRating:
                return "COMPANION RATING";
            case SortTypes.HeroMoveIcons:
                return "HERO MOVE ICONS";
            case SortTypes.HeroAttackIcons:
                return "HERO ATTACK ICONS";
            case SortTypes.HeroHeroicIcons:
                return "HERO HEROIC ICONS";
            case SortTypes.HeroWildIcons:
                return "HERO WILD ICONS";
            case SortTypes.HeroSpecailCards:
                return "HERO SPECAIL CARDS";
            case SortTypes.HeroStartingHandCards:
                return "HERO STARTING HAND CARDS";
            case SortTypes.CompanionMoveIcons:
                return "COMPANION MOVE ICONS";
            case SortTypes.CompanionAttackIcons:
                return "COMPANION ATTACK ICONS";
            case SortTypes.CompanionHeroicIcons:
                return "COMPANION HEROIC ICONS";
            case SortTypes.CompanionWildIcons:
                return "COMPANION WILD ICONS";
            case SortTypes.CompanionSpecailCards:
                return "COMPANION SPECAIL CARDS";
            case SortTypes.CompanionStartingHandCards:
                return "COMPANION STARTING HAND CARDS";
            default:
                return "";
        }
    }
}