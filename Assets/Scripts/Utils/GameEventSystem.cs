using UnityEngine;

public class GameEventSystem : MonoBehaviour
{
    #region UI Events
    public delegate void UIEvent();
    public delegate void UIEvent_SearchSelected(Searchable searchable);
    public delegate void UIEvent_CharacterSelected(Character character);
    public delegate void UIEvent_BoxSelected(Box box, bool isForGameBuild);
    public delegate void UIEvent_GameBuildUpdatePopup(GameSystems gameSystems);
    public delegate void UIEvent_BuildGamePopup(BuildGameData data);

    public static event UIEvent UI_ClosePopup;
    public static event UIEvent UI_ShowFilterPopup;
    public static event UIEvent UI_CloseFilterPopup;
    public static event UIEvent UI_CloseGameIncludePopup;
    public static event UIEvent_GameBuildUpdatePopup UI_ShowGameBuildUpdatePopup;
    public static event UIEvent_SearchSelected UI_SearchSelected;
    public static event UIEvent_CharacterSelected UI_CharacterSelected;

    public static event UIEvent_BoxSelected UI_BoxSelected;
    public static event UIEvent_BuildGamePopup UI_ShowBuiltGame;

    public static void UI_OnSearchSelected(Searchable searchable)
    {
        UI_SearchSelected?.Invoke(searchable);
    }
    public static void UI_OnCharacterSelected(Character character)
    {
        UI_CharacterSelected?.Invoke(character);
    }
    public static void UI_OnBoxSelected(Box box, bool isForGameBuild)
    {
        UI_BoxSelected?.Invoke(box, isForGameBuild);
    }
    public static void UI_OnShowGameBuildUpdatePopup(GameSystems gameSystems)
    {
        UI_ShowGameBuildUpdatePopup?.Invoke(gameSystems);
    }
    public static void UI_OnShowBuiltGame(BuildGameData data)
    {
        UI_ShowBuiltGame?.Invoke(data);
    }
    public static void UI_OnShowFilterPopup()
    {
        UI_ShowFilterPopup?.Invoke();
    }
    public static void UI_OnCloseFilterPopup()
    {
        UI_CloseFilterPopup?.Invoke();
    }
    public static void UI_OnClosePopup()
    {
        UI_ClosePopup?.Invoke();
    }
    public static void UI_OnCloseGameIncludePopup()
    {
        UI_CloseGameIncludePopup?.Invoke();
    }
    #endregion
}
