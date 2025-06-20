using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Team : BoxOwnable
{
    [Header("Team")]
    public Teams TeamTag;
    public List<Character> Characters = new List<Character>();
    public bool HerosOnly;
    public bool VillainOnly;

    public override void Init()
    {
        base.Init();

        InitImage("TeamImages/", DisplayName());
        InitDtlImage("TeamImages/", DisplayName());
    }

    public override string DisplayName()
    {
        return TeamTag.ToFriendlyString();
    }
    public override string SearchName()
    {
        return TeamTag.ToFriendlyString();
    }
    public override string Clarifier()
    {
        return "(TEAM)";
    }
}
