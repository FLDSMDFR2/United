using UnityEngine;

public class ClosePopupButton : MonoBehaviour
{
    public virtual void ButtonPress()
    {
        GameEventSystem.UI_OnClosePopup();
    }
}

