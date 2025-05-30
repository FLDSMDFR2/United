using System.Collections.Generic;
using UnityEngine;
using static Collection;

public class UI_MainController : MonoBehaviour
{
    public GameObject MainDisplay;
    public GameObject CharacterDtlsDisplay;
    public GameObject BoxDtlsDisplay;

    protected Dictionary<GameObject, bool> displayStates = new Dictionary<GameObject, bool>();
    protected Inputs inputs;
    protected UI_CharacterDetailsController characterDetails;
    protected UI_BoxDetails boxDetails;

    protected virtual void Awake()
    {
        inputs = new Inputs();
        inputs.UserInputs.Enable();

        BuildDisplayStates();

        GameEventSystem.UI_ClosePopup += GameEventSystem_UI_ClosePopup;
        GameEventSystem.UI_CharacterSelected += GameEventSystem_UI_CharacterSelected;
        GameEventSystem.UI_BoxSelected += GameEventSystem_UI_BoxSelected;
        inputs.UserInputs.BackButton.performed += BackButton_performed;
    }

    protected virtual void Start()
    {
        UpdateDisplay();

        ShowMainDisplay();

        InitParams();
    }

    protected virtual void BuildDisplayStates()
    {
        if (MainDisplay != null) displayStates.Add(MainDisplay, true);
        if (CharacterDtlsDisplay != null) displayStates.Add(CharacterDtlsDisplay, true);
        if (BoxDtlsDisplay != null) displayStates.Add(BoxDtlsDisplay, true);
    }

    protected virtual void InitParams()
    {
        if (CharacterDtlsDisplay != null) characterDetails = CharacterDtlsDisplay.GetComponent<UI_CharacterDetailsController>();
        if (BoxDtlsDisplay != null) boxDetails = BoxDtlsDisplay.GetComponent<UI_BoxDetails>();
    }

    protected virtual void UpdateDisplay()
    {
        foreach (var key in displayStates.Keys)
        {
            key.SetActive(displayStates[key]);
        }
    }

    protected virtual void ShowMainDisplay()
    {
        if (displayStates.ContainsKey(MainDisplay))displayStates[MainDisplay] = true;
        if (displayStates.ContainsKey(CharacterDtlsDisplay)) displayStates[CharacterDtlsDisplay] = false;
        if (displayStates.ContainsKey(BoxDtlsDisplay)) displayStates[BoxDtlsDisplay] = true;

        UpdateDisplay();
    }

    protected virtual void ShowCharacterDtls(Character character)
    {
        if (characterDetails != null) characterDetails.SetData(character);
        if (displayStates.ContainsKey(CharacterDtlsDisplay)) displayStates[CharacterDtlsDisplay] = true;

        if (characterDetails != null && characterDetails is IDialog) characterDetails.Open();
        else UpdateDisplay();
    }

    protected virtual void ShowBoxDtls(BoxDtls box)
    {
        if (boxDetails != null) boxDetails.SetData(box);
        if (displayStates.ContainsKey(BoxDtlsDisplay)) displayStates[BoxDtlsDisplay] = true;

        if (boxDetails != null && boxDetails is IDialog) boxDetails.Open();
        else UpdateDisplay();
    }

    protected virtual void HideDtlsDisplay()
    {
        if (displayStates.ContainsKey(CharacterDtlsDisplay)) displayStates[CharacterDtlsDisplay] = false;
        if (displayStates.ContainsKey(BoxDtlsDisplay)) displayStates[BoxDtlsDisplay] = false;

        if (characterDetails != null && characterDetails is IDialog) characterDetails.Close();
        if (boxDetails != null && boxDetails is IDialog) boxDetails.Close();
        else UpdateDisplay();
    }

    #region Event Handlers

    protected virtual void GameEventSystem_UI_CharacterSelected(Character character)
    {
        ShowCharacterDtls(character);
    }
    protected virtual void GameEventSystem_UI_BoxSelected(BoxDtls box)
    {
        ShowBoxDtls(box);
    }

    protected virtual void BackButton_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        HideDtlsDisplay();
    }
    protected virtual void GameEventSystem_UI_ClosePopup()
    {
        HideDtlsDisplay();
    }
    #endregion

}
