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

        InitImage("TeamImages/", DisplayNameWithClarifier());
        InitDtlImage("TeamImages/", DisplayNameWithClarifier());

        AddDtlItems("CHARACTERS", Characters);
    }

    public override string DisplayName()
    {
        return TeamTag.ToFriendlyString();
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
