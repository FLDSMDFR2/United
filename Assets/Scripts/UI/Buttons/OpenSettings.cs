using UnityEngine;

public class OpenSettings : MonoBehaviour
{
    public virtual void ButtonPress()
    {
        GameEventSystem.UI_OnShowSettings();
    }
}
