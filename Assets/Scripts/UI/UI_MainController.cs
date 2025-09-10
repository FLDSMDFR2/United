using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class UI_MainController : MonoBehaviour
{
    protected class DisplayData
    {
        public bool CurrentState;
        public bool NewState;
        public bool IsDialog;
        public IDialog Dialog;

        public DisplayData(bool currentState, bool newState, bool isDialog, IDialog dialog)
        {
            CurrentState = newState;
            NewState = currentState;
            IsDialog = isDialog;
            Dialog = dialog;
        }
    }

    public UI_LoadScreen LoadingScreen; // LOADING SCREEN

    public GameObject MainDisplay; // TAB WINDOW

    public GameObject SearchDtlsDisplay; // SERACH SELECT POPUP
    public GameObject CharacterDtlsDisplay; // SERACH SELECT POPUP

    public GameObject BoxDtlsDisplay; // BOX DTL POPUP

    public GameObject BuildIncludsDisplay; // BUILD INCLUDE POPUP
    public GameObject GeneratedBuildDisplay; // GENERATED BUILD POPUP

    public GameObject FilterDisplay; // FILTER POPUP

    public GameObject CharacterWinLose; // FILTER POPUP

    public GameObject Settings; // SETTINGS POPUP

    protected Dictionary<GameObject, DisplayData> displayStates = new Dictionary<GameObject, DisplayData>();
    protected Stack<GameObject> displayStack = new Stack<GameObject>();
    protected Inputs inputs;

    protected virtual void Awake()
    {
        LoadingScreen.Show();
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
        GameEventSystem.UI_CharacterWinLoseSelected += GameEventSystem_UI_CharacterWinLoseSelected;
        GameEventSystem.UI_ShowSettings += GameEventSystem_UI_ShowSettings;
        inputs.UserInputs.BackButton.performed += BackButton_performed;
    }

    protected virtual void Start()
    {
        InitDisplay();

        StartGameLoad();
    }

    protected async void StartGameLoad()
    {
        await GameLoader.LoadStep1Async();
        await GameLoader.LoadStep2Async();

        ShowMainDisplay();

        await Task.Delay(1000);

        LoadingScreen.Close();
    }

    protected virtual void BuildDisplayStates()
    {
        if (MainDisplay != null) displayStates.Add(MainDisplay, new DisplayData(true, true, false, null));
        if (SearchDtlsDisplay != null) displayStates.Add(SearchDtlsDisplay, new DisplayData(true, true, true, SearchDtlsDisplay.GetComponent<UI_SearchDetailController>()));
        if (CharacterDtlsDisplay != null) displayStates.Add(CharacterDtlsDisplay, new DisplayData(true, true, true, CharacterDtlsDisplay.GetComponent<UI_CharacterDetailsController>()));  
        if (BoxDtlsDisplay != null) displayStates.Add(BoxDtlsDisplay, new DisplayData(true, true, true, BoxDtlsDisplay.GetComponent<UI_BoxDetails>()));
        if (BuildIncludsDisplay != null) displayStates.Add(BuildIncludsDisplay, new DisplayData(true, true, true, BuildIncludsDisplay.GetComponent<UI_AllBoxDisplayController>()));
        if (GeneratedBuildDisplay != null) displayStates.Add(GeneratedBuildDisplay, new DisplayData(true, true, true, GeneratedBuildDisplay.GetComponent<UI_GeneratedGameDtl>()));
        if (FilterDisplay != null) displayStates.Add(FilterDisplay, new DisplayData(true, true, true, FilterDisplay.GetComponent<UI_Filter>()));
        if (CharacterWinLose != null) displayStates.Add(CharacterWinLose, new DisplayData(true, true, true, CharacterWinLose.GetComponent<UI_CharacterWinLose>()));
        if (Settings != null) displayStates.Add(Settings, new DisplayData(true, true, true, Settings.GetComponent<UI_Settings>()));
    }
    
    protected virtual void InitDisplay()
    {
        foreach (var key in displayStates.Keys)
        {
            key.SetActive(displayStates[key].CurrentState);
        }
    }

    protected virtual void UpdateDisplay()
    {
        foreach (var key in displayStates.Keys)
        {
            if (displayStates[key].CurrentState == displayStates[key].NewState) continue;

            displayStates[key].CurrentState = displayStates[key].NewState;

            if (displayStates[key] != null && displayStates[key].Dialog != null && displayStates[key].CurrentState) displayStates[key].Dialog.Open();
            else if (displayStates[key] != null && displayStates[key].Dialog != null && !displayStates[key].CurrentState) displayStates[key].Dialog.Close();
            else key.SetActive(displayStates[key].CurrentState);
        }
    }

    protected virtual void ShowAll()
    {
        if (displayStates.ContainsKey(MainDisplay)) displayStates[MainDisplay].NewState = true;
        if (displayStates.ContainsKey(SearchDtlsDisplay)) displayStates[SearchDtlsDisplay].NewState = true;
        if (displayStates.ContainsKey(CharacterDtlsDisplay)) displayStates[CharacterDtlsDisplay].NewState = true;
        if (displayStates.ContainsKey(BoxDtlsDisplay)) displayStates[BoxDtlsDisplay].NewState = true;
        if (displayStates.ContainsKey(BuildIncludsDisplay)) displayStates[BuildIncludsDisplay].NewState = true;
        if (displayStates.ContainsKey(GeneratedBuildDisplay)) displayStates[GeneratedBuildDisplay].NewState = true;
        if (displayStates.ContainsKey(FilterDisplay)) displayStates[FilterDisplay].NewState = true;
        if (displayStates.ContainsKey(CharacterWinLose)) displayStates[CharacterWinLose].NewState = true;
        if (displayStates.ContainsKey(Settings)) displayStates[Settings].NewState = true;

        UpdateDisplay();
    }

    protected virtual void HideAll(bool withUpdate = false)
    {
        if (displayStates.ContainsKey(MainDisplay)) displayStates[MainDisplay].NewState = false;
        if (displayStates.ContainsKey(SearchDtlsDisplay)) displayStates[SearchDtlsDisplay].NewState = false;
        if (displayStates.ContainsKey(CharacterDtlsDisplay)) displayStates[CharacterDtlsDisplay].NewState = false;
        if (displayStates.ContainsKey(BoxDtlsDisplay)) displayStates[BoxDtlsDisplay].NewState = false;
        if (displayStates.ContainsKey(BuildIncludsDisplay)) displayStates[BuildIncludsDisplay].NewState = false;
        if (displayStates.ContainsKey(GeneratedBuildDisplay)) displayStates[GeneratedBuildDisplay].NewState = false;
        if (displayStates.ContainsKey(FilterDisplay)) displayStates[FilterDisplay].NewState = false;
        if (displayStates.ContainsKey(CharacterWinLose)) displayStates[CharacterWinLose].NewState = false;
        if (displayStates.ContainsKey(Settings)) displayStates[Settings].NewState = false;

        if (withUpdate) UpdateDisplay();
    }

    protected virtual void ShowMainDisplay()
    {
        HideAll();

        if (displayStates.ContainsKey(MainDisplay))displayStates[MainDisplay].NewState = true;

        UpdateDisplay();
    }

    protected virtual void ShowBuildIncludsDisplay(GameSystems gameSystems)
    {
        if (displayStates.ContainsKey(BuildIncludsDisplay)) displayStates[BuildIncludsDisplay].NewState = true;

        displayStack.Push(BuildIncludsDisplay);

        UpdateDisplay();

        if (displayStates[BuildIncludsDisplay].Dialog != null && displayStates[BuildIncludsDisplay].Dialog is UI_AllBoxDisplayController)
            ((UI_AllBoxDisplayController)displayStates[BuildIncludsDisplay].Dialog).SetData(gameSystems);
    }

    protected virtual void ShowGeneratedBuildDisplay(BuildGameData data)
    {
        if (displayStates[GeneratedBuildDisplay].Dialog != null && displayStates[GeneratedBuildDisplay].Dialog is UI_GeneratedGameDtl)
            ((UI_GeneratedGameDtl)displayStates[GeneratedBuildDisplay].Dialog).SetData(data);

        if (displayStates.ContainsKey(GeneratedBuildDisplay)) displayStates[GeneratedBuildDisplay].NewState = true;

        displayStack.Push(GeneratedBuildDisplay);

        UpdateDisplay();
    }

    protected virtual void ShowBoxDtls(Box box, bool isForGameBuild)
    {
        if (displayStates[BoxDtlsDisplay].Dialog != null && displayStates[BoxDtlsDisplay].Dialog is UI_BoxDetails) 
            ((UI_BoxDetails)displayStates[BoxDtlsDisplay].Dialog).SetData(box, isForGameBuild);

        if (displayStates.ContainsKey(BoxDtlsDisplay)) displayStates[BoxDtlsDisplay].NewState = true;

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
                displayStates[CharacterDtlsDisplay].NewState = true;
                displayStack.Push(CharacterDtlsDisplay);
            }
        }
        else
        {
            if (displayStates[SearchDtlsDisplay].Dialog != null && displayStates[SearchDtlsDisplay].Dialog is UI_SearchDetailController)
            {
                ((UI_SearchDetailController)displayStates[SearchDtlsDisplay].Dialog).SetData(searchable);
                displayStates[SearchDtlsDisplay].NewState = true;
                displayStack.Push(SearchDtlsDisplay);
            }
        }

        UpdateDisplay();
    }

    protected virtual void ShowFilter()
    {
        if (displayStates.ContainsKey(FilterDisplay)) displayStates[FilterDisplay].NewState = true;

        displayStack.Push(FilterDisplay);

        UpdateDisplay();
    }

    protected virtual void ShowWinLose(Character character, CharacterType type)
    {
        if (displayStates[CharacterWinLose].Dialog != null && displayStates[CharacterWinLose].Dialog is UI_CharacterWinLose)
            ((UI_CharacterWinLose)displayStates[CharacterWinLose].Dialog).SetData(character, type);

        if (displayStates.ContainsKey(CharacterWinLose)) displayStates[CharacterWinLose].NewState = true;

        displayStack.Push(CharacterWinLose);

        UpdateDisplay();
    }

    protected virtual void ShowSettings()
    {
        if (displayStates[Settings].Dialog != null && displayStates[Settings].Dialog is UI_Settings)
            ((UI_Settings)displayStates[Settings].Dialog).SetData();

        if (displayStates.ContainsKey(Settings)) displayStates[Settings].NewState = true;

        displayStack.Push(Settings);

        UpdateDisplay();
    }

    protected virtual void HideDialog()
    {
        if (displayStack.Count > 0)
        {
            displayStates[displayStack.Pop()].NewState = false;

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
    private void GameEventSystem_UI_CharacterWinLoseSelected(Character character, CharacterType type)
    {
        ShowWinLose(character, type);
    }
    protected virtual void GameEventSystem_UI_ShowFilterPopup()
    {
        ShowFilter();
    }
    protected virtual void GameEventSystem_UI_ShowSettings()
    {
        ShowSettings();
    }
    protected virtual void BackButton_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        HideDialog();
    }
    protected virtual void GameEventSystem_UI_ClosePopup()
    {
        HideDialog();
    }
    #endregion

}
