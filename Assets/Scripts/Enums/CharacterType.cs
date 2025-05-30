public enum CharacterType
{
    None = 0,
    Hero,
    Villain,
    AntiHero
}

public static class CharacterTypeExtensions
{
    public static string ToFriendlyString(this CharacterType num)
    {
        switch (num)
        {
            case CharacterType.None:
                return "NONE";
            case CharacterType.Hero:
                return "HERO";
            case CharacterType.Villain:
                return "VILLAIN";
            case CharacterType.AntiHero:
                return "ANTI HERO";
            default:
                return "";
        }
    }
}
    