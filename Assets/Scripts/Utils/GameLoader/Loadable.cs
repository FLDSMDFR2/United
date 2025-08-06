using UnityEngine;

public class Loadable : MonoBehaviour, ILoadable
{
    protected virtual void Awake()
    {
        Register();
    }

    protected virtual void Register()
    {
        GameLoader.Register(this);
    }

    public virtual void LoadableStep1(){ }
    public virtual void LoadableStep2(){ }
}
