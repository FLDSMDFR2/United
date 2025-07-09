using System;
using System.Collections.Generic;
using UnityEngine;

public class Searchable : Ownable
{
    [Header("Searchable")]
    public Sprite MainImage;
    public bool AutoGetMainImage = true;
    public List<string> DtlImageNames;
    public List<Sprite> DtlImages = new List<Sprite>();
    public bool AutoGetDtlImage = true;
    public bool AutoGetDtlImages = true;
    public List<Seasons> Season;
    public bool IsExclusive;

    protected Dictionary<string, List<string>> filter = new Dictionary<string, List<string>>();
    protected Dictionary<SortTypes, string> sort = new Dictionary<SortTypes, string>();

    public virtual void Init()
    {
        InitFilter();
        InitSort();
    }

    protected virtual void InitImage(string path, string fileName)
    {
        if (!AutoGetMainImage) return;

        var image = Resources.Load<Sprite>(GetImageFilePath(path, fileName));
        if (image != null) MainImage = image;
        else
        {
            TraceManager.WriteTrace(TraceChannel.Fine, TraceType.warning, "Could Not Find Image [" + fileName + "]");
            MainImage = Resources.Load<Sprite>("Error/Missing");
        }
    }

    protected virtual void InitDtlImage(string path, string fileName)
    {
        DtlImages.Clear();

        if (AutoGetDtlImage)
        {
            var image = Resources.Load<Sprite>(GetImageFilePath(path, fileName));
            if (image != null) DtlImages.Add(image);
            else
            {
                TraceManager.WriteTrace(TraceChannel.Fine, TraceType.warning, "Could Not Find Image Dtl [" + fileName + "]");
                DtlImages.Add(Resources.Load<Sprite>("Error/Missing"));
            }
        }

        if (AutoGetDtlImages)
        {
            foreach (var file in DtlImageNames)
            {
                var image1 = Resources.Load<Sprite>(GetImageFilePath(path, file));
                if (image1 != null) DtlImages.Add(image1);
                else
                {
                    TraceManager.WriteTrace(TraceChannel.Fine, TraceType.warning, "Could Not Find Image Dtl [" + fileName + "]");
                    DtlImages.Add(Resources.Load<Sprite>("Error/Missing"));
                }
            }
        }
    }

    protected virtual string GetImageFilePath(string path, string fileName)
    {
        fileName = fileName.Replace(" ", string.Empty);
        //fileName = fileName.Replace("-", string.Empty);
        fileName = fileName.Replace(":", string.Empty);
        fileName = fileName.Replace("'", string.Empty);
        fileName = fileName.Replace(".", string.Empty);
        fileName = fileName.Replace(",", string.Empty);

        return path + fileName;
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

    protected virtual void InitSort()
    {
        sort[SortTypes.Name] = SearchName();
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
    public virtual string GetSortString(SortTypes sortTypes)
    {
        if (!sort.ContainsKey(sortTypes)) return "";

        return sort[sortTypes];
    }
    public virtual int GetSortInt(SortTypes sortTypes)
    {
        if (!sort.ContainsKey(sortTypes)) return 0;

        return int.Parse(sort[sortTypes]);
    }

    public virtual Sprite GetDisplayImage()
    {
        return MainImage;
    }
    public virtual List<Sprite> GetDtlImages()
    {
        return DtlImages;
    }

}
