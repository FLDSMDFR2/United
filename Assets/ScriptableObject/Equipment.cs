using UnityEngine;

[CreateAssetMenu]
public class Equipment : BoxOwnable
{
    [Header("Equipment")]
    public string Name;

    public override void Init()
    {
        base.Init();

        InitImage("EquipmentImages/", DisplayName());
        InitDtlImage("EquipmentImages/", DisplayName());
    }

    public override string DisplayName()
    {
        return Name;
    }

    public override string SearchName()
    {
        return Name;
    }
    public override string Clarifier()
    {
        return "(EQUIPMENT)";
    }
}
