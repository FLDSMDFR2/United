using UnityEngine;

[CreateAssetMenu]
public class Challenge : BoxOwnable
{
    [Header("Challenge")]
    public GameChallenges ChallengeTag;
    public GameDtl GameDtl;

    public override void Init()
    {
        base.Init();

        InitImage("ChallengeIcons/", DisplayNameWithClarifier());
        InitDtlImage("ChallengeDtl/", DisplayNameWithClarifier());
    }
    public override string DisplayName()
    {
        return ChallengeTag.ToFriendlyString();
    }
    public override string DisplayNameWithClarifier()
    {
        return DisplayName() + " " + Clarifier();
    }
    public override string SearchName()
    {
        return DisplayNameWithClarifier();
    }

    public override string Clarifier()
    {
        return "";
    }
}
