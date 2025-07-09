using System.Collections.Generic;
using UnityEngine;

public class BoxOwnable : Searchable
{
    [Header("OwnableBox")]
    public List<BoxAssociationDtl> Boxs = new List<BoxAssociationDtl>();

    public virtual bool GetOwned(Boxs boxTag)
    {
        if (Boxs.Count <= 0) return false;

        foreach (var box in Boxs)
        {
            if (boxTag == box.Box && DataLoader.GetBoxByTag(boxTag).Owned) return box.Owned;
        }

        return false;
    }

    public virtual bool GetIncludeInBuild(Boxs boxTag)
    {
        if (Boxs.Count <= 0) return false;

        foreach (var box in Boxs)
        {
            if (boxTag == box.Box && DataLoader.GetBoxByTag(boxTag).IncludeInGameBuild) return box.IncludeInGameBuild;
        }

        return false;
    }

    public virtual void SetOwned(Boxs boxTag, bool owned)
    {
        base.SetOwned(UpdateOwned(boxTag, owned));  
    }

    public virtual void SetIncludeInBuild(Boxs boxTag, bool includeInBuild)
    {
        base.SetIncludeInBuild(UpdateIncludeInBuild(boxTag, includeInBuild));
    }

    protected virtual bool UpdateOwned(Boxs boxTag, bool owned)
    {
        var retVal = false;

        if (Boxs.Count <= 0) return retVal;

        foreach(var box in Boxs)
        {
            if (boxTag == box.Box) box.Owned = owned;

            if (box.Owned) retVal = true;
        }

        return retVal;
    }

    protected virtual bool UpdateIncludeInBuild(Boxs boxTag, bool includeInBuild)
    {
        var retVal = false;

        if (Boxs.Count <= 0) return retVal;

        foreach (var box in Boxs)
        {
            if (boxTag == box.Box) box.IncludeInGameBuild = includeInBuild;

            if (box.IncludeInGameBuild) retVal = true;
        }

        return retVal;
    }
}
