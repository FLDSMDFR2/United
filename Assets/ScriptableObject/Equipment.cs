using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Equipment : BoxOwnable
{
    [Header("Equipment")]
    public string EquipmentName;
    public string EquipmentClarifier;

    public List<Character> Characters = new List<Character>();
    public bool AnyHero;

    public override void Init()
    {
        base.Init();

        InitImage("EquipmentImages/", GetDisplayNameWithClarifier());
        InitDtlImage("EquipmentImages/", GetDisplayNameWithClarifier());
    }
}
