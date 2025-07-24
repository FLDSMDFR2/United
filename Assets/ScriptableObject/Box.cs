using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Box : BoxOwnable
{
    [Header("Box")]
    public Boxs BoxTag;
    public Color BoxColor;
    public bool IsDarkText;

    public List<Character> Characters = new List<Character>();
    public List<Location> Locations = new List<Location>();
    public List<Challenge> Challenges = new List<Challenge>();
    public List<Mode> Modes = new List<Mode>();
    public List<Team> Teams = new List<Team>();
    public List<Equipment> Equipment = new List<Equipment>();
    public List<Campaign> Campaigns = new List<Campaign>();

    public override void Init()
    {
        base.Init();

        InitImage("BoxImages/", DisplayNameWithClarifier());
        InitDtlImage("BoxImages/", DisplayNameWithClarifier());
        ResetLists();
    }
    public override string DisplayName()
    {
        return BoxTag.ToFriendlyString();
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
    public virtual void ResetLists()
    {
        Characters.Clear();
        Locations.Clear();
        Challenges.Clear();
        Modes.Clear();
        Teams.Clear();
        Equipment.Clear();
        Campaigns.Clear();
    }

    public virtual List<BoxOwnable> GetAllBoxItems()
    {
        var retval = new List<BoxOwnable>();
        retval.AddRange(Characters);
        retval.AddRange(Locations);
        retval.AddRange(Challenges);
        retval.AddRange(Modes);
        retval.AddRange(Teams);
        retval.AddRange(Equipment);
        retval.AddRange(Campaigns);

        return retval;
    }
}
