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

        InitImage("ChallengeIcons/", DisplayName());
        InitDtlImage("ChallengeDtl/", DisplayName());
    }
    public override string DisplayName()
    {
        return ChallengeTag.ToFriendlyString();
    }
    public override string SearchName()
    {
        return ChallengeTag.ToFriendlyString();
    }
    public override string Clarifier()
    {
        return "(CHALLENGE)";
    }
}
