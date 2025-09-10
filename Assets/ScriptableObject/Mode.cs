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

        InitImage("ModeImages/", GetDisplayNameWithClarifier());
        InitDtlImage("ModeImages/", GetDisplayNameWithClarifier());
    }
    public override string GetDisplayName()
    {
        return ModeTag.ToFriendlyString();
    }
}
