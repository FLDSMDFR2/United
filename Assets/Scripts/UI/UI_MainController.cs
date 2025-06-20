using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UI_MainController : MonoBehaviour
{
    protected class DisplayData
    {
        public bool IsActive;
        public bool IsDialog;
        public IDialog Dialog;

        public DisplayData(bool isActive, bool isDialog, IDialog dialog)
        {
            IsActive = isActive;
            IsDialog = isDialog;
            Dialog = dialog;
        }
    }

    public GameObject LoadingScreen; // LOADING SCREEN

    public GameObject MainDisplay; // TAB WINDOW

    public GameObject SearchDtlsDisplay; // SERACH SELECT POPUP
    public GameObject CharacterDtlsDisplay; // SERACH SELECT POPUP

    public GameObject BoxDtlsDisplay; // BOX DTL POPUP

    public GameObject BuildIncludsDisplay; // BUILD INCLUDE POPUP
    public GameObject GeneratedBuildDisplay; // GENERATED BUILD POPUP

    public GameObject FilterDisplay; // FILTER POPUP

    protected Dictionary<GameObject, DisplayData> displayStates = new Dictionary<GameObject, DisplayData>();
    protected Stack<GameObject> displayStack = new Stack<GameObject>();
    protected Inputs inputs;

    protected virtual void Awake()
    {
        LoadingScreen.SetActive(true);
        inputs = new Inputs();
        inputs.UserInputs.Enable();

        BuildDisplayStates();

        GameEventSystem.UI_ClosePopup += GameEventSystem_UI_ClosePopup;
        GameEventSystem.UI_SearchSelected += GameEventSystem_UI_SearchSelected;
        GameEventSystem.UI_CharacterSelected += GameEventSystem_UI_CharacterSelected;
        GameEventSystem.UI_BoxSelected += GameEventSystem_UI_BoxSelected;
        GameEventSystem.UI_ShowGameBuildUpdatePopup += GameEventSystem_UI_ShowGameBuildUpdatePopup;
        GameEventSystem.UI_ShowBuiltGame += GameEventSystem_UI_ShowBuiltGame;
        GameEventSystem.UI_ShowFilterPopup += GameEventSystem_UI_ShowFilterPopup;
        inputs.UserInputs.BackButton.performed += BackButton_performed;
    }

    protected virtual void Start()
    {
        InitDisplay();

        StartCoroutine(StartDelay());
    }

    protected virtual IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(0.2f);

        ShowMainDisplay();

        yield return new WaitForSeconds(0.5f);

        LoadingScreen.SetActive(false);
    }

    protected virtual void BuildDisplayStates()
    {
        if (MainDisplay != null) displayStates.Add(MainDisplay, new DisplayData(true, false, null));
        if (SearchDtlsDisplay != null) displayStates.Add(SearchDtlsDisplay, new DisplayData(true, true, SearchDtlsDisplay.GetComponent<UI_SearchDetailController>()));
        if (CharacterDtlsDisplay != null) displayStates.Add(CharacterDtlsDisplay, new DisplayData(true, true, CharacterDtlsDisplay.GetComponent<UI_CharacterDetailsController>()));  
        if (BoxDtlsDisplay != null) displayStates.Add(BoxDtlsDisplay, new DisplayData(true, true, BoxDtlsDisplay.GetComponent<UI_BoxDetails>()));
        if (BuildIncludsDisplay != null) displayStates.Add(BuildIncludsDisplay, new DisplayData(true, true, BuildIncludsDisplay.GetComponent<UI_AllBoxDisplayController>()));
        if (GeneratedBuildDisplay != null) displayStates.Add(GeneratedBuildDisplay, new DisplayData(true, true, GeneratedBuildDisplay.GetComponent<UI_GeneratedGameDtl>()));
        if (FilterDisplay != null) displayStates.Add(FilterDisplay, new DisplayData(true, true, FilterDisplay.GetComponent<UI_Filter>()));
    }
    
    protected virtual void InitDisplay()
    {
        foreach (var key in displayStates.Keys)
        {
            key.SetActive(displayStates[key].IsActive);
        }
    }

    protected virtual void UpdateDisplay()
    {
        foreach (var key in displayStates.Keys)
        {
            if (displayStates[key] != null && displayStates[key].Dialog != null && displayStates[key].IsActive) displayStates[key].Dialog.Open();
            else if (displayStates[key] != null && displayStates[key].Dialog != null && !displayStates[key].IsActive) displayStates[key].Dialog.Close();
            else key.SetActive(displayStates[key].IsActive);       
        }
    }

    protected virtual void ShowAll()
    {
        if (displayStates.ContainsKey(MainDisplay)) displayStates[MainDisplay].IsActive = true;
        if (displayStates.ContainsKey(SearchDtlsDisplay)) displayStates[SearchDtlsDisplay].IsActive = true;
        if (displayStates.ContainsKey(CharacterDtlsDisplay)) displayStates[CharacterDtlsDisplay].IsActive = true;
        if (displayStates.ContainsKey(BoxDtlsDisplay)) displayStates[BoxDtlsDisplay].IsActive = true;
        if (displayStates.ContainsKey(BuildIncludsDisplay)) displayStates[BuildIncludsDisplay].IsActive = true;
        if (displayStates.ContainsKey(GeneratedBuildDisplay)) displayStates[GeneratedBuildDisplay].IsActive = true;
        if (displayStates.ContainsKey(FilterDisplay)) displayStates[FilterDisplay].IsActive = true;

        UpdateDisplay();
    }

    protected virtual void HideAll(bool withUpdate = false)
    {
        if (displayStates.ContainsKey(MainDisplay)) displayStates[MainDisplay].IsActive = false;
        if (displayStates.ContainsKey(SearchDtlsDisplay)) displayStates[SearchDtlsDisplay].IsActive = false;
        if (displayStates.ContainsKey(CharacterDtlsDisplay)) displayStates[CharacterDtlsDisplay].IsActive = false;
        if (displayStates.ContainsKey(BoxDtlsDisplay)) displayStates[BoxDtlsDisplay].IsActive = false;
        if (displayStates.ContainsKey(BuildIncludsDisplay)) displayStates[BuildIncludsDisplay].IsActive = false;
        if (displayStates.ContainsKey(GeneratedBuildDisplay)) displayStates[GeneratedBuildDisplay].IsActive = false;
        if (displayStates.ContainsKey(FilterDisplay)) displayStates[FilterDisplay].IsActive = false;

        if (withUpdate) UpdateDisplay();
    }

    protected virtual void ShowMainDisplay()
    {
        HideAll();

        if (displayStates.ContainsKey(MainDisplay))displayStates[MainDisplay].IsActive = true;

        UpdateDisplay();
    }

    protected virtual void ShowBuildIncludsDisplay(GameSystems gameSystems)
    {
        if (displayStates[BuildIncludsDisplay].Dialog != null && displayStates[BuildIncludsDisplay].Dialog is UI_AllBoxDisplayController)
            ((UI_AllBoxDisplayController)displayStates[BuildIncludsDisplay].Dialog).SetData(gameSystems);

        if (displayStates.ContainsKey(BuildIncludsDisplay)) displayStates[BuildIncludsDisplay].IsActive = true;

        displayStack.Push(BuildIncludsDisplay);

        UpdateDisplay();
    }

    protected virtual void ShowGeneratedBuildDisplay(BuildGameData data)
    {
        if (displayStates[GeneratedBuildDisplay].Dialog != null && displayStates[GeneratedBuildDisplay].Dialog is UI_GeneratedGameDtl)
            ((UI_GeneratedGameDtl)displayStates[GeneratedBuildDisplay].Dialog).SetData(data);

        if (displayStates.ContainsKey(GeneratedBuildDisplay)) displayStates[GeneratedBuildDisplay].IsActive = true;

        displayStack.Push(GeneratedBuildDisplay);

        UpdateDisplay();
    }

    protected virtual void ShowBoxDtls(Box box, bool isForGameBuild)
    {
        if (displayStates[BoxDtlsDisplay].Dialog != null && displayStates[BoxDtlsDisplay].Dialog is UI_BoxDetails) 
            ((UI_BoxDetails)displayStates[BoxDtlsDisplay].Dialog).SetData(box, isForGameBuild);

        if (displayStates.ContainsKey(BoxDtlsDisplay)) displayStates[BoxDtlsDisplay].IsActive = true;

        displayStack.Push(BoxDtlsDisplay);

        UpdateDisplay();
    }

    protected virtual void ShowSearchableDtls(Searchable searchable)
    {
        if (searchable is Character)
        {
            if (displayStates[CharacterDtlsDisplay].Dialog != null && displayStates[CharacterDtlsDisplay].Dialog is UI_CharacterDetailsController)
            {
                ((UI_CharacterDetailsController)displayStates[CharacterDtlsDisplay].Dialog).SetData(searchable);
                displayStates[CharacterDtlsDisplay].IsActive = true;
                displayStack.Push(CharacterDtlsDisplay);
            }
        }
        else
        {
            if (displayStates[SearchDtlsDisplay].Dialog != null && displayStates[SearchDtlsDisplay].Dialog is UI_SearchDetailController)
            {
                ((UI_SearchDetailController)displayStates[SearchDtlsDisplay].Dialog).SetData(searchable);
                displayStates[SearchDtlsDisplay].IsActive = true;
                displayStack.Push(SearchDtlsDisplay);
            }
        }

        UpdateDisplay();
    }

    protected virtual void ShowFilter()
    {
        if (displayStates.ContainsKey(FilterDisplay)) displayStates[FilterDisplay].IsActive = true;

        displayStack.Push(FilterDisplay);

        UpdateDisplay();
    }

    protected virtual void HideDialog()
    {
        if (displayStack.Count > 0)
        {
            displayStates[displayStack.Pop()].IsActive = false;

            UpdateDisplay();
        }
    }

    #region Event Handlers
    protected virtual void GameEventSystem_UI_SearchSelected(Searchable searchable)
    {
        ShowSearchableDtls(searchable);
    }
    protected virtual void GameEventSystem_UI_CharacterSelected(Character character)
    {
        ShowSearchableDtls(character);
    }

    protected virtual void GameEventSystem_UI_BoxSelected(Box box, bool isForGameBuild)
    {
        ShowBoxDtls(box, isForGameBuild);
    }

    protected virtual void GameEventSystem_UI_ShowGameBuildUpdatePopup(GameSystems gameSystems)
    {
        ShowBuildIncludsDisplay(gameSystems);
    }
    protected virtual void GameEventSystem_UI_ShowBuiltGame(BuildGameData data)
    {
        ShowGeneratedBuildDisplay(data);
    }

    protected virtual void BackButton_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        HideDialog();
    }
    protected virtual void GameEventSystem_UI_ShowFilterPopup()
    {
        ShowFilter();
    }
    protected virtual void GameEventSystem_UI_ClosePopup()
    {
        HideDialog();
    }
    #endregion

}
