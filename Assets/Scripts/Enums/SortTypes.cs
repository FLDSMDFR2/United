
public enum SortTypes
{
    None,
    Name,
    HeroMoveIcons,
    HeroAttackIcons,
    HeroHeroicIcons,
    HeroWildIcons, 
    HeroSpecailCards

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
            default:
                return "";
        }
    }
}