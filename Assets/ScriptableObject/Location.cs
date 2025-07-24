using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Location : BoxOwnable
{
    [Header("Location")]
    public string LocationName;
    public List<GameModes> ExclusiveForMode = new List<GameModes>();

    public override void Init()
    {
        base.Init();

        InitImage("LocationImages/", DisplayNameWithClarifier());
        InitDtlImage("LocationImages/", DisplayNameWithClarifier());
    }

    public override string DisplayName()
    {
        return LocationName;
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
