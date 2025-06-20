using UnityEngine;

public class FilterButton : MonoBehaviour
{
    public virtual void ButtonPress()
    {
        GameEventSystem.UI_OnShowFilterPopup();
    }
}

