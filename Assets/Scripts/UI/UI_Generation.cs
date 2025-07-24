using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Generation : MonoBehaviour
{
    [Header("System")]
    public TMP_Dropdown GameSystemDropDown;
    public int GameSystemDefaultIndex;
    protected string selectedGameSystem;

    [Header("Error")]
    public GameObject ErrorGameObject;
    public TextMeshProUGUI ErrorMessageText;

    protected List<GameSystems> gameSystemsDropDownMap = new List<GameSystems>();

    protected virtual void Start()
    {
        GameEventSystem.UI_CloseGameIncludePopup += GameEventSystem_UI_CloseGameIncludePopup;

        BuildGameSystemsDropDown(Enum.GetValues(typeof(GameSystems)));

        UpdatedDropDowns();
    }

    protected virtual void UpdatedDropDowns()
    {

    }

    public virtual void GenerateGameSelected()
    {

    }

    protected virtual GameSystems GetActiveSystem()
    {
        return gameSystemsDropDownMap[GameSystemDropDown.value];
    }

    protected virtual void ErrorText(bool ShowError, string text = "")
    {
        if (!ShowError)
        {
            ErrorGameObject.SetActive(false);
            return;
        }

        ErrorGameObject.SetActive(true);
        ErrorMessageText.text = text;
    }

    public virtual void GameEventSystem_UI_CloseGameIncludePopup()
    {
        UpdatedDropDowns();
    }
    public virtual void UpdateIncludedSelected()
    {
        GameEventSystem.UI_OnShowGameBuildUpdatePopup(GetActiveSystem());
    }

    protected virtual void BuildGameSystemsDropDown(System.Array systems)
    {
        var displayList = new List<string>();
        foreach (GameSystems gameSystem in systems)
        {
            if (gameSystem == GameSystems.None) continue;

            gameSystemsDropDownMap.Add(gameSystem);
            displayList.Add(gameSystem.ToFriendlyString());
        }

        PopulateDropDown(GameSystemDropDown, displayList, GameSystemDefaultIndex, selectedGameSystem);
    }

    protected virtual void PopulateDropDown(TMP_Dropdown dropdown, List<string> options, int defaultIndex, string selected)
    {
        dropdown.options.Clear();
        if (options.Count <= 0)
        {
            dropdown.options.Add(new TMP_Dropdown.OptionData() { text = "NONE" });
        }
        else
        {
            var index = 0;
            foreach (var option in options)
            {
                if (option == selected) defaultIndex = index;

                dropdown.options.Add(new TMP_Dropdown.OptionData() { text = option });
                index++;

            }
        }

        if (dropdown.options.Count > 0) dropdown.SetValueWithoutNotify(1);

        if (dropdown.options.Count > defaultIndex) dropdown.SetValueWithoutNotify(defaultIndex);
        else dropdown.SetValueWithoutNotify(0);
    }
}
