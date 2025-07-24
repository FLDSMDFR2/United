using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Mode : BoxOwnable
{
    [Header("Mode")]
    public GameModes ModeTag;

    public bool UseSameHerosForAllGames;
    public bool AllowVillainSelection = true;
    public bool AllowChallengeSelection = true;
    public List<GameDtl> Games = new List<GameDtl>();

    public override void Init()
    {
        base.Init();

        InitImage("ModeImages/", DisplayNameWithClarifier());
        InitDtlImage("ModeImages/", DisplayNameWithClarifier());
    }
    public override string DisplayName()
    {
        return ModeTag.ToFriendlyString();
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
