using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Campaign : BoxOwnable
{
    [Header("Campaign")]
    public string Name;

    public List<Box> RequiaredBoxs = new List<Box>();

    public override void Init()
    {
        base.Init();

        InitImage("CampaignImages/", DisplayNameWithClarifier());
        InitDtlImage("CampaignImages/", DisplayNameWithClarifier());

        AddDtlItems("REQUIARED BOXS", RequiaredBoxs);
    }

    public override string DisplayName()
    {
        return Name;
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
