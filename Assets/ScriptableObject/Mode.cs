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

        InitImage("ModeImages/", DisplayName());
        InitDtlImage("ModeImages/", DisplayName());
    }
    public override string DisplayName()
    {
        return ModeTag.ToFriendlyString();
    }
    public override string SearchName()
    {
        return ModeTag.ToFriendlyString();
    }
    public override string Clarifier()
    {
        return "(MODE)";
    }
}
