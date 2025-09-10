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

        InitImage("LocationImages/", GetDisplayNameWithClarifier());
        InitDtlImage("LocationImages/", GetDisplayNameWithClarifier());
    }
}
