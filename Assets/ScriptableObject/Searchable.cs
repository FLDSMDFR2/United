using System;
using System.Collections.Generic;
using UnityEngine;

public class Searchable : Ownable
{
    [Header("Searchable")]
    public Sprite MainImage;
    public Sprite DtlImage;
    public List<Seasons> Season;

    protected Dictionary<string, List<string>> filter = new Dictionary<string, List<string>>();

    public virtual void Init()
    {
        InitFilter();
    }

    protected virtual void InitImage(string path, string fileName)
    {
        fileName = fileName.Replace(" ", string.Empty);
        //fileName = fileName.Replace("-", string.Empty);
        fileName = fileName.Replace(":", string.Empty);
        fileName = fileName.Replace("'", string.Empty);
        fileName = fileName.Replace(".", string.Empty);
        fileName = fileName.Replace(",", string.Empty);
        var image = Resources.Load<Sprite>(path + fileName);
        if (image != null) MainImage = image;
        else
        {
            TraceManager.WriteTrace(TraceChannel.Fine, TraceType.warning, "Could Not Find Image [" + fileName + "]");
            DtlImage = Resources.Load<Sprite>("Error/Missing");
        }
    }

    protected virtual void InitDtlImage(string path, string fileName)
    {
        fileName = fileName.Replace(" ", string.Empty);
        //fileName = fileName.Replace("-", string.Empty);
        fileName = fileName.Replace(":", string.Empty);
        fileName = fileName.Replace("'", string.Empty);
        fileName = fileName.Replace(".", string.Empty);
        fileName = fileName.Replace(",", string.Empty);
        var image = Resources.Load<Sprite>(path + fileName);
        if (image != null) DtlImage = image;
        else
        {
            TraceManager.WriteTrace(TraceChannel.Fine, TraceType.warning, "Could Not Find Image Dtl [" + fileName + "]");
            DtlImage = Resources.Load<Sprite>("Error/Missing");
        }
    }

    protected virtual void InitFilter()
    {
        filter[typeof(GameSystems).Name] = new List<string>() { Enum.GetName(typeof(GameSystems), GameSystem) };
        filter["Owned"] = new List<string>() { Owned.ToString() };
        filter[typeof(Seasons).Name] = new List<string>();
        foreach(var season in Season)
        {
            filter[typeof(Seasons).Name].Add(Enum.GetName(typeof(Seasons), season));
        }
    }

    public override void SetOwned(bool owned)
    {
        base.SetOwned(owned);
        filter["Owned"][0] = Owned.ToString();
    }

    public virtual string DisplayName()
    {
        return "";
    }
    public virtual string SearchName()
    {
        return "";
    }
    public virtual string Clarifier()
    {
        return "";
    }
    public virtual Dictionary<string, List<string>> Filter()
    {
        return filter;
    }

    public virtual Sprite GetDisplayImage()
    {
        return MainImage;
    }
    public virtual Sprite GetDtlImage()
    {
        return DtlImage;
    }

}
