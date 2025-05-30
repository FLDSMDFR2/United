using UnityEngine;
using static Collection;

public class GameEventSystem : MonoBehaviour
{
    #region UI Events
    public delegate void UIEvent();
    public delegate void UIEvent_CharacterSelected(Character character);
    public delegate void UIEvent_BoxSelected(BoxDtls box);

    public static event UIEvent UI_ClosePopup;
    public static event UIEvent_CharacterSelected UI_CharacterSelected;
    public static event UIEvent_BoxSelected UI_BoxSelected;
    public static void UI_OnCharacterSelected(Character character)
    {
        UI_CharacterSelected?.Invoke(character);
    }
    public static void UI_OnBoxSelected(BoxDtls box)
    {
        UI_BoxSelected?.Invoke(box);
    }
    public static void UI_OnClosePopup()
    {
        UI_ClosePopup?.Invoke();
    }
    #endregion
}
