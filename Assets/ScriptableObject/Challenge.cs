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

        InitImage("ChallengeIcons/", GetDisplayNameWithClarifier());
        InitDtlImage("ChallengeDtl/", GetDisplayNameWithClarifier());
    }
    public override string GetDisplayName()
    {
        return ChallengeTag.ToFriendlyString();
    }
}
