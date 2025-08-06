using System;
using UnityEngine;

public class Ownable : ScriptableObject
{
    public delegate void OwnableUpdate();
    public event OwnableUpdate OnOwnableUpdate;

    [Header("Save ID")]
    [ReadOnlyInspector]
    public long ID;

    [Header("Ownable")]
    public GameSystems GameSystem;
    public bool Owned;
    public bool IncludeInGameBuild = true;

    protected DateTime lastUpdateDate;

    public virtual void Init()
    {

    }

    public virtual void SetOwned(bool owned)
    {
        Owned = owned;
        UpdateAndSaveData();
        RaiseOnOwnableUpdate();
    }

    public virtual void SetIncludeInBuild(bool includeInBuild)
    {
        IncludeInGameBuild = includeInBuild;
        UpdateAndSaveData();
    }
    
    protected virtual void RaiseOnOwnableUpdate()
    {
        OnOwnableUpdate?.Invoke();
    }

    public virtual void SetOwnableData(OwnableData data)
    {
        Owned = data.Owned;
        IncludeInGameBuild = data.IncludeInGameBuild;
        lastUpdateDate = data.LastUpdateDate;
    }

    public virtual OwnableData GetOwnableData()
    {
        var data = new OwnableData();
        data.Owned = Owned;
        data.IncludeInGameBuild = IncludeInGameBuild;
        data.LastUpdateDate = lastUpdateDate;
        return data;
    }

    protected virtual void UpdateAndSaveData()
    {
        lastUpdateDate = DateTime.Now;

        SaveDataController.UpdateDataForId(ID, GetOwnableData());
        //SaveDataController.Save();
    }
}
