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

        InitImage("CampaignImages/", GetDisplayNameWithClarifier());
        InitDtlImage("CampaignImages/", GetDisplayNameWithClarifier());

        AddDtlItems("REQUIARED BOXS", RequiaredBoxs);
    }
}
