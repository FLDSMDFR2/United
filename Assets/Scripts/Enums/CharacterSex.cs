public enum CharacterSex
{
    None = 0,
    Male,
    Female
}

public static class CharacterSexExtensions
{
    public static string ToFriendlyString(this CharacterSex num)
    {
        switch (num)
        {
            case CharacterSex.None:
                return "NONE";
            case CharacterSex.Male:
                return "MALE";
            case CharacterSex.Female:
                return "FEMALE";
            default:
                return "";
        }
    }
}


