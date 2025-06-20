using UnityEngine;

public class Ownable : ScriptableObject
{
    public delegate void OwnableUpdate();
    public event OwnableUpdate OnOwnableUpdate;

    [Header("Ownable")]
    public GameSystems GameSystem;
    public bool Owned;
    public bool IncludeInGameBuild = true;

    public virtual void SetOwned(bool owned)
    {
        Owned = owned;
        RaiseOnOwnableUpdate();
    }

    protected virtual void RaiseOnOwnableUpdate()
    {
        OnOwnableUpdate?.Invoke();
    }
}
