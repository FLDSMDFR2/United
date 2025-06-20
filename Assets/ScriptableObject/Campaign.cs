using UnityEngine;

[CreateAssetMenu]
public class Campaign : BoxOwnable
{
    [Header("Campaign")]
    public string Name;

    public override void Init()
    {
        base.Init();

        InitImage("CampaignImages/", DisplayName());
        InitDtlImage("CampaignImages/", DisplayName());
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
        return "(CAMPAIGN)";
    }
}
