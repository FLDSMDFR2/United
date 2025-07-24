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

        InitImage("EquipmentImages/", DisplayNameWithClarifier());
        InitDtlImage("EquipmentImages/", DisplayNameWithClarifier());
    }

    public override string DisplayName()
    {
        return EquipmentName;
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
        return EquipmentClarifier;
    }
}
