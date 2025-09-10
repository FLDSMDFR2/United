using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Team : BoxOwnable
{
    [Header("Team")]
    public Teams TeamTag;
    public Color TeamColor;
    public List<Character> Characters = new List<Character>();
    public bool HerosOnly;
    public bool VillainOnly;

    public override void Init()
    {
        base.Init();

        InitImage("TeamImages/", GetDisplayNameWithClarifier());
        InitDtlImage("TeamImages/", GetDisplayNameWithClarifier());

        AddDtlItems("CHARACTERS", Characters);
    }

    public override string GetDisplayName()
    {
        return TeamTag.ToFriendlyString();
    }
}
