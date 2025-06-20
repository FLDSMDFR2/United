using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using TMPro;
using UnityEngine;

public class UI_GameBuildController : MonoBehaviour
{
    [Header("System")]
    public TMP_Dropdown GameSystemDropDown;
    public int GameSystemDefaultIndex;
    protected string selectedGameSystem;

    [Header("Modes")]
    public GameObject GameModesObject;
    public TMP_Dropdown GameModesDropDown;
    public int GameModesDefaultIndex;
    protected string selectedMode;

    [Header("Challenges")]
    public GameObject ChallengeObject;
    public TMP_Dropdown ChallengeDropDown;
    public int ChallengeDefaultIndex;
    protected string selectedChallenge;

    [Header("Villains")]
    public GameObject VillainsObject;
    public TMP_Dropdown VillainsDropDown;
    public int VillainsDefaultIndex;
    protected string selectedVillain;

    [Header("Teams")]
    public GameObject GameTeamsObject;
    public TMP_Dropdown TeamsDropDown;
    public int TeamsDefaultIndex;
    protected string selectedTeam;

    [Header("Heros")]
    public TMP_Dropdown NumberOfHerosDropDown;
    public int NumberOfHerosDefaultIndex;
    protected string selectedHeros;

    public int MaxNumberOfHeros;
    protected int numberOfLocations = 6;

    [Header("Error")]
    public GameObject ErrorGameObject;
    public TextMeshProUGUI ErrorMessageText;

    protected List<GameSystems> gameSystemsDropDownMap = new List<GameSystems>();
    protected Dictionary<int, Mode> modesDropDownMap = new Dictionary<int, Mode>();
    protected Dictionary<int, Challenge> challengesDropDownMap = new Dictionary<int, Challenge>();
    protected Dictionary<int, Character> villainsDropDownMap = new Dictionary<int, Character>();
    protected Dictionary<int, Team> teamsDropDownMap = new Dictionary<int, Team>();

    // display data for dropdowns
    protected List<Character> displayVillains = new List<Character>();
    protected List<Mode> displayModes = new List<Mode>();
    protected List<Challenge> displayChalleneges = new List<Challenge>();
    protected List<Team> displayTeams = new List<Team>();

    // get all data
    protected List<Box> avaialbleBoxs;
    protected List<Character> availableVillains;
    protected List<Mode> availableModes;
    protected List<Challenge> availableChalleneges;
    protected List<Location> availableLocations;
    protected List<Character> availableHeros;
    protected List<Team> availableTeams;

    protected bool updatingDropDowns;

    protected virtual void Start()
    {
        BuildGameSystemsDropDown(Enum.GetValues(typeof(GameSystems)));
        BuildNumberOfHerosDropDown(MaxNumberOfHeros);

        UpdatedDropDowns();
    }

    public virtual void UpdatedDropDowns()
    {
        if (updatingDropDowns) return;
        updatingDropDowns = true;

        UpdateModeDropDown();
        UpdateChallengesDropDown();
        UpdateVillainDropDown();
        BuildTeamsDropDown(DataLoader.GetTeamsBySystem(GetActiveSystem()));

        updatingDropDowns = false;
    }

    #region UpdateDropDowns

    #region Mode DropDown
    protected virtual void UpdateModeDropDown()
    {
        displayModes.Clear();
        displayModes.AddRange(DataLoader.GetModesBySystem(GetActiveSystem()));
        var removeModes = new List<Mode>();
        foreach (Mode mode in displayModes)
        {
            if (VillainsDropDown.value != 0 && !IsModeValidForVillain(mode, villainsDropDownMap[VillainsDropDown.value])) removeModes.Add(mode);
            if (ChallengeDropDown.value != 0 && ChallengeDropDown.value != 1 && !IsModeValidForChallenge(mode, challengesDropDownMap[ChallengeDropDown.value])) removeModes.Add(mode);
        }

        foreach(var mode in removeModes)
        {
            displayModes.Remove(mode);
        }

        BuildModesDropDown(displayModes);
    }

    protected virtual bool IsModeValidForVillain(Mode mode, Character villain)
    {
        foreach (var game in mode.Games)
        {
            if (game.ExcludedVillains.Count > 0 && game.ExcludedVillains.Contains(villain)) return false;
            if (game.RequiaredVillains.Count > 0 && !game.RequiaredVillains.Contains(villain) && game.RequiaredVillainsExclusively) return false;
        }

        return true;
    }

    protected virtual bool IsModeValidForChallenge(Mode mode, Challenge challenge)
    {
        foreach (var game in mode.Games)
        {
            if (game.ExcludedChallenges.Count > 0 && game.ExcludedChallenges.Contains(challenge)) return false;
            if (game.RequiaredChallenges.Count > 0 && !game.RequiaredChallenges.Contains(challenge)) return false;
        }

        return true;
    }
    #endregion

    #region Villain DropDown
    protected virtual void UpdateVillainDropDown()
    {
        displayVillains.Clear();
        displayVillains.AddRange(DataLoader.GetVillainsBySystem(GetActiveSystem()));

        VillianExlusions();

        // if the game mode random then allow full villain selection
        if (GameModesDropDown.value == 1 || modesDropDownMap[GameModesDropDown.value].AllowVillainSelection)
        {
            if (GameModesDropDown.value != 1) UpdateVillainDropDownForMode(modesDropDownMap[GameModesDropDown.value]);
            VillainsObject.SetActive(true);
        }
        else
        {
            // if no villain selection allowed hide selection
            VillainsObject.SetActive(false);
            return;
        }

        BuildVillainsDropDown(displayVillains);
    }

    protected virtual void VillianExlusions()
    {
        var removeList = new List<Character>();
        foreach (var villain in displayVillains)
        {
            if (!villain.IsStandAloneVillain) removeList.Add(villain);
        }

        foreach (var villain in removeList)
        {
            displayVillains.Remove(villain);
        }
    }

    protected virtual void UpdateVillainDropDownForMode(Mode mode)
    {
        // this should only get called if we allow selection
        // we should only remove what is not allowed
        foreach (var game in mode.Games)
        {
            foreach(var villain in game.ExcludedVillains)
            {
                if (displayVillains.Contains(villain)) displayVillains.Remove(villain);
            }
        }
    }
    #endregion

    #region Challenges DropDown
    protected virtual void UpdateChallengesDropDown()
    {
        displayChalleneges.Clear();
        displayChalleneges.AddRange(DataLoader.GetChallengesBySystem(GetActiveSystem()));

        // if the game mode random
        if (GameModesDropDown.value == 1 || modesDropDownMap[GameModesDropDown.value].AllowChallengeSelection)
        {
            if (GameModesDropDown.value != 1) UpdateChallengesDropDownForMode(modesDropDownMap[GameModesDropDown.value]);
            ChallengeObject.SetActive(true);
        }
        else
        {
            // if no villain selection allowed hide selection
            ChallengeObject.SetActive(false);
            return;
        }

        BuildChallengesDropDown(displayChalleneges);
    }

    protected virtual void UpdateChallengesDropDownForMode(Mode mode)
    {
        foreach (var game in mode.Games)
        {
            foreach (var challenge in game.ExcludedChallenges)
            {
                if (displayChalleneges.Contains(challenge)) displayChalleneges.Remove(challenge);
            }
        }
    }
    #endregion

    #endregion

    #region GenerateGame
    public virtual void GenerateGameSelected()
    {
        var system = gameSystemsDropDownMap[GameSystemDropDown.value];
        // if we are set to None this means Random so pick a random system
        if (gameSystemsDropDownMap[GameSystemDropDown.value] == GameSystems.None)
        {
            // start at index 2 so we ignor the none and all values
            system = gameSystemsDropDownMap[RandomGenerator.UnseededRange(2, gameSystemsDropDownMap.Count)];
        }

        BuildAvailableData(system);

        if (!ValidateAvailableData()) return;

        var buildData = new BuildGameData();

        buildData.Mode = GenerateGameMode();

        buildData = GenerateVillains(buildData);
        if (buildData == null || !ValidateVillains(buildData)) return;

        buildData = GenerateLocations(buildData);
        if (buildData == null || !ValidateLocations(buildData)) return;

        buildData = GenerateHeros(buildData);
        if (buildData == null || !ValidateHeros(buildData)) return;

        buildData = GenerateChallenges(buildData);
        if (buildData == null || !ValidateChallenges(buildData)) return;

        ErrorText(false);
        GameEventSystem.UI_OnShowBuiltGame(buildData);
    }

    protected virtual void BuildAvailableData(GameSystems system)
    {
        avaialbleBoxs = DataLoader.GetBoxsBySystem(system);

        availableVillains = new List<Character>();
        availableModes = new List<Mode>();
        availableChalleneges = new List<Challenge>();
        availableLocations = new List<Location>();
        availableHeros = new List<Character>();
        availableTeams = new List<Team>();

        //loop over data to build list of each object type
        foreach (var data in avaialbleBoxs)
        {
            //characters
            foreach (var character in data.Characters)
            {
                if (character.IncludeInGameBuild && (character.Type == CharacterType.Villain || character.Type == CharacterType.AntiHero)) availableVillains.Add(character);
                if (character.IncludeInGameBuild && (character.Type == CharacterType.Hero || character.Type == CharacterType.AntiHero)) availableHeros.Add(character);
            }
            //modes
            foreach (var mode in data.Modes)
            {
                if (mode.IncludeInGameBuild) availableModes.Add(mode);
            }
            //Challenges
            foreach (var challenge in data.Challenges)
            {
                if (challenge.IncludeInGameBuild) availableChalleneges.Add(challenge);
            }
            //Locations
            foreach (var location in data.Locations)
            {
                if (location.IncludeInGameBuild) availableLocations.Add(location);
            }
            //Teams
            foreach (var team in data.Teams)
            {
                if (team.IncludeInGameBuild) availableTeams.Add(team);
            }
        }
    }

    protected virtual Mode GenerateGameMode()
    {
        // we have no mode option or we are set to normal generate a normal game
        if (!GameModesObject.activeSelf || GameModesDropDown.value <= 0)
        {
            return modesDropDownMap[0];
        }
        else if (GameModesDropDown.value == 1)
        {
            // random mode
            return modesDropDownMap[RandomGenerator.UnseededRange(2, modesDropDownMap.Count)];
        }
        else
        {
            return modesDropDownMap[GameModesDropDown.value];
        }
    }

    protected virtual BuildGameData GenerateVillains(BuildGameData gameData)
    {
        var gameCount = 0;
        foreach(var game in gameData.Mode.Games)
        {
            gameData.Games.Add(new BuildGame());
            var requiared = new List<Character>();
            requiared.AddRange(game.RequiaredVillains);
            var isRequiared = requiared != null && requiared.Count > 0;

            for (int i = 0; i < game.NumberOfVillains; i++)
            {
                //ERROR we need specific villains and there are none left
                if (game.RequiaredVillainsExclusively && isRequiared && requiared.Count <= 0)
                {
                    ErrorText(true, "Requaired villains for mode not available");
                    return null;
                }

                // try and select a villain
                Character selected;      
                if (VillainsDropDown.value == 0  || !VillainsObject.activeSelf)
                {
                    // random villain
                    if (isRequiared && requiared.Count > 0) selected = requiared[RandomGenerator.UnseededRange(0, requiared.Count)];
                    else selected = availableVillains[RandomGenerator.UnseededRange(0, availableVillains.Count)];
                }
                else
                {
                    selected = villainsDropDownMap[VillainsDropDown.value];
                }

                if (game.ExcludedVillains.Contains(selected))
                {
                    i--;
                    continue;
                }

                // Update data for selected
                if (availableVillains.Contains(selected))
                {
                    // this is a good villain we will use it and remove it from available
                    availableVillains.Remove(selected);
                    if (availableHeros.Contains(selected)) availableHeros.Remove(selected);
                    gameData.Games[gameCount].Villains.Add(selected);
                }
                else
                {
                    // we are not using this selection so reset the counter
                    i--;
                }

                if (isRequiared) requiared.Remove(selected);
                
            }
            gameCount++;
        }

        return gameData;
    }

    protected virtual BuildGameData GenerateLocations(BuildGameData gameData)
    {
        var gameCount = 0;
        foreach (var game in gameData.Mode.Games)
        {
            var requiared = new List<Location>();
            requiared.AddRange(game.RequiaredLocations);
            var isRequiared = requiared != null && requiared.Count > 0;

            for (int i = 0; i < numberOfLocations; i++)
            {
                //ERROR we need specific locations and there are none left
                if (game.RequiaredLocationsExclusively && isRequiared && requiared.Count <= 0)
                {
                    ErrorText(true, "Requaired locations for mode not available");
                    return null;
                }

                // try and select a locations
                Location selected;
                if (isRequiared && requiared.Count > 0) selected = requiared[RandomGenerator.UnseededRange(0, requiared.Count)];
                else selected = availableLocations[RandomGenerator.UnseededRange(0, availableLocations.Count)];

                // if this location is exlusive to a mode and its not this one
                // or if this location is exlusive to a mode and it is this one but not this game
                if ((selected.ExclusiveForMode.Count > 0 && !selected.ExclusiveForMode.Contains(gameData.Mode.ModeTag)) || 
                    (selected.ExclusiveForMode.Count > 0 && selected.ExclusiveForMode.Contains(gameData.Mode.ModeTag)  && !requiared.Contains(selected)))
                {
                    // reset and try and find another location this one is not elegiable for this game type
                    i--;
                    continue;
                }

                if (game.ExcludedLocations.Contains(selected))
                {
                    i--;
                    continue;
                }

                // Update data for selected
                if (availableLocations.Contains(selected) && !gameData.Games[gameCount].Locations.Contains(selected))
                {
                    // this is a good location we will use it and remove it from available
                    gameData.Games[gameCount].Locations.Add(selected);
                }
                else
                {
                    // we are not using this selection so reset the counter
                    i--;
                }

                if (isRequiared) requiared.Remove(selected);
            }

            gameCount++;
        }

        return gameData;
    }

    protected virtual BuildGameData GenerateHeros(BuildGameData gameData)
    {
        var gameCount = 0;
        foreach (var game in gameData.Mode.Games)
        {
            // if we are going to use the same heros for all games just copy heros from first game to the rest
            if (gameCount > 0 && gameData.Mode.UseSameHerosForAllGames)
            {
                gameData.Games[gameCount].Heros.AddRange(gameData.Games[0].Heros);
                gameData.Games[gameCount].AdditionalHeros.AddRange(gameData.Games[0].AdditionalHeros);

                gameCount++;
                continue;
            }

            var requiared = new List<Character>();
            requiared.AddRange(game.RequiaredHeros);
            var isRequiared = requiared != null && requiared.Count > 0;

            for (int i = 0; i <= NumberOfHerosDropDown.value; i++)
            {
                //ERROR we need specific Heros and there are none left
                if (game.RequiaredHerosExclusively && isRequiared && requiared.Count <= 0)
                {
                    ErrorText(true, "Requaired Heros for mode not available");
                    return null;
                }

                // try and select a Character
                Character selected;
                if (isRequiared && requiared.Count > 0) selected = requiared[RandomGenerator.UnseededRange(0, requiared.Count)];
                else selected = availableHeros[RandomGenerator.UnseededRange(0, availableHeros.Count)];

                if (game.ExcludedHeros.Contains(selected))
                {
                    i--;
                    continue;
                }

                // Update data for selected
                if (availableHeros.Contains(selected) && !gameData.Games[gameCount].Heros.Contains(selected))
                {
                    // this is a good hero we will use it
                    gameData.Games[gameCount].Heros.Add(selected);
                }
                else
                {
                    // we are not using this selection so reset the counter
                    i--;
                }

                if (isRequiared) requiared.Remove(selected);
            }

            if (game.AdditionalHeros != 0)
            {
                var AdditionalHerosCount = game.AdditionalHeros;
                if (game.AdditionalHerosReferenceNumHeros) AdditionalHerosCount = (NumberOfHerosDropDown.value + 1) + game.AdditionalHeros;

                for (int i = 0; i < AdditionalHerosCount; i++)
                {
                    //ERROR we need specific Heros and there are none left
                    if (game.RequiaredHerosExclusively && isRequiared && requiared.Count <= 0)
                    {
                        ErrorText(true, "Requaired Additional Heros for mode not available");
                        return null;
                    }

                    // try and select a Character
                    Character selected;
                    if (isRequiared && requiared.Count > 0) selected = requiared[RandomGenerator.UnseededRange(0, requiared.Count)];
                    else selected = availableHeros[RandomGenerator.UnseededRange(0, availableHeros.Count)];

                    if (game.ExcludedHeros.Contains(selected))
                    {
                        i--;
                        continue;
                    }

                    // Update data for selected
                    if (availableHeros.Contains(selected) && !gameData.Games[gameCount].Heros.Contains(selected) &&
                        !gameData.Games[gameCount].AdditionalHeros.Contains(selected))
                    {
                        // this is a good location we will use it
                        gameData.Games[gameCount].AdditionalHeros.Add(selected);
                    }
                    else
                    {
                        // we are not using this selection so reset the counter
                        i--;
                    }

                    if (isRequiared) requiared.Remove(selected);
                }
            }

            gameCount++;
        }

        return gameData;
    }

    protected virtual BuildGameData GenerateChallenges(BuildGameData gameData)
    {
        var gameCount = 0;
        foreach (var game in gameData.Mode.Games)
        {
            if (ChallengeDropDown.value == 1)
            {
                // random 
                gameData.Games[gameCount].Challenge = challengesDropDownMap[RandomGenerator.UnseededRange(2, challengesDropDownMap.Count)];
            }
            else if (ChallengeDropDown.value > 1)
            {
                gameData.Games[gameCount].Challenge = challengesDropDownMap[ChallengeDropDown.value];
            }

            gameCount++;
        }


        return gameData;
    }
    #endregion

    #region Validation

    protected virtual bool ValidateAvailableData()
    {
        if (avaialbleBoxs.Count <= 0)
        {
            ErrorText(true, "No Available Data Included");
            return false;
        }
        if (availableVillains.Count <= 0)
        {
            ErrorText(true, "No Available Villains Included");
            return false;
        }
        if (availableLocations.Count < numberOfLocations)
        {
            ErrorText(true, "Not Enough Locations Included");
            return false;
        }
        if (availableHeros.Count < NumberOfHerosDropDown.value + 1)
        {
            ErrorText(true, "Not Enough Heros Included");
            return false;
        }

        return true;
    }

    protected virtual bool ValidateVillains(BuildGameData gameData)
    {
        if (gameData.Games.Count != gameData.Mode.Games.Count)
        {
            ErrorText(true, "Mode Game count miss match");
            return false;
        }

        for (int i = 0; i < gameData.Games.Count; i ++)
        {
            if (gameData.Games[i].Villains.Count != gameData.Mode.Games[i].NumberOfVillains)
            {
                ErrorText(true, "Villain to game count miss match");
                return false;
            }
        }

        return true;
    }

    protected virtual bool ValidateLocations(BuildGameData gameData)
    {
        if (gameData.Games.Count != gameData.Mode.Games.Count)
        {
            ErrorText(true, "Mode Game count miss match");
            return false;
        }

        for (int i = 0; i < gameData.Games.Count; i++)
        {
            if (gameData.Games[i].Locations.Count != numberOfLocations)
            {
                ErrorText(true, "Location to game count miss match");
                return false;
            }
        }

        return true;
    }

    protected virtual bool ValidateHeros(BuildGameData gameData)
    {
        if (gameData.Games.Count != gameData.Mode.Games.Count)
        {
            ErrorText(true, "Mode Game count miss match");
            return false;
        }

        for (int i = 0; i < gameData.Games.Count; i++)
        {
            if (gameData.Games[i].Heros.Count != (NumberOfHerosDropDown.value+1))
            {
                ErrorText(true, "Hero to game count miss match");
                return false;
            }

            if (gameData.Mode.Games[i].AdditionalHeros > 0)
            {
                var AdditionalHerosCount = gameData.Mode.Games[i].AdditionalHeros;
                if (gameData.Mode.Games[i].AdditionalHerosReferenceNumHeros) AdditionalHerosCount = (NumberOfHerosDropDown.value + 1) + gameData.Mode.Games[i].AdditionalHeros;

                if (gameData.Games[i].AdditionalHeros.Count != AdditionalHerosCount)
                {
                    ErrorText(true, "Additional Heros to game count miss match");
                    return false;
                }
            }
    
        }

        return true;
    }

    protected virtual bool ValidateChallenges(BuildGameData gameData)
    {
        return true;
    }

    #endregion

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

    protected virtual GameSystems GetActiveSystem()
    {
        var system = GameSystems.All;
        // if we are set to None this means Random so keep system as all so we can pick between all systems
        if (gameSystemsDropDownMap[GameSystemDropDown.value] != GameSystems.None) system = gameSystemsDropDownMap[GameSystemDropDown.value];

        return system;
    }

    #region Handlers
    public virtual void UpdateIncludedSelected()
    {
        GameEventSystem.UI_OnShowGameBuildUpdatePopup(GetActiveSystem());
    }

    public virtual void GameSystemSelected()
    {
        selectedGameSystem = GameSystemDropDown.options[GameSystemDropDown.value].text;
        UpdatedDropDowns();
    }

    public virtual void GameModeSelected()
    {
        selectedMode = GameModesDropDown.options[GameModesDropDown.value].text;
        UpdatedDropDowns();
    }

    public virtual void ChallengeSelected()
    {
        selectedChallenge = ChallengeDropDown.options[ChallengeDropDown.value].text;
        UpdatedDropDowns();
    }

    public virtual void VillainSelected()
    {
        selectedVillain= VillainsDropDown.options[VillainsDropDown.value].text;
        UpdatedDropDowns();
    }

    public virtual void TeamSelected()
    {
        selectedTeam = TeamsDropDown.options[TeamsDropDown.value].text;
        UpdatedDropDowns();
    }
    public virtual void HerosSelected()
    {
        selectedHeros = NumberOfHerosDropDown.options[NumberOfHerosDropDown.value].text;
        UpdatedDropDowns();
    }

    #endregion

    #region Populate Drop Down Helpers
    protected virtual void BuildGameSystemsDropDown(System.Array systems)
    {
        var displayList = new List<string>();
        foreach (GameSystems gameSystem in systems)
        {
            gameSystemsDropDownMap.Add(gameSystem);

            if (gameSystem == global::GameSystems.None)
            {
                displayList.Add("Random");
                continue;
            }
            displayList.Add(gameSystem.ToFriendlyString());
        }

        PopulateDropDown(GameSystemDropDown, displayList, GameSystemDefaultIndex, selectedGameSystem);
    }

    protected virtual void BuildModesDropDown(List<Mode> modes)
    {
        modesDropDownMap.Clear();
        var displayList = new List<string>();
        var index = 0;
        modesDropDownMap[index++] = null;
        displayList.Add("Normal");
        modesDropDownMap[index++] = null;
        displayList.Add("Random");


        foreach (var gameMode in modes)
        {
            if (gameMode.ModeTag == GameModes.Normal)
            {
                // if this is the normal mode then add it to the normal index
                modesDropDownMap[0] = gameMode;
            }
            else
            {
                modesDropDownMap[index] = gameMode;
                displayList.Add(gameMode.DisplayName());
                index++;
            }
        }

        PopulateDropDown(GameModesDropDown, displayList, GameModesDefaultIndex, selectedMode);
    }

    protected virtual void BuildChallengesDropDown(List<Challenge> challenges)
    {
        challengesDropDownMap.Clear();
        var displayList = new List<string>();
        var index = 0;
        challengesDropDownMap[index++] = null;
        displayList.Add("None");
        challengesDropDownMap[index++] = null;
        displayList.Add("Random");

        foreach (var gameChallenge in challenges)
        {
            challengesDropDownMap[index] = gameChallenge;
            displayList.Add(gameChallenge.DisplayName());
            index++;
        }

        PopulateDropDown(ChallengeDropDown, displayList, ChallengeDefaultIndex, selectedChallenge);
    }

    protected virtual void BuildVillainsDropDown(List<Character> villains)
    {
        villainsDropDownMap.Clear();
        var displayList = new List<string>();
        var index = 0;
        villainsDropDownMap[index++] = null;
        displayList.Add("Random");

        foreach (var villain in villains)
        {
            villainsDropDownMap[index] = villain;
            displayList.Add(villain.DisplayName());
            index++;
        }

        PopulateDropDown(VillainsDropDown, displayList, VillainsDefaultIndex, selectedVillain);
    }

    protected virtual void BuildTeamsDropDown(List<Team> teams)
    {
        teamsDropDownMap.Clear();
        var displayList = new List<string>();
        var index = 0;
        teamsDropDownMap[index++] = null;
        displayList.Add("None");
        teamsDropDownMap[index++] = null;
        displayList.Add("Random");

        foreach (var team in teams)
        {
            teamsDropDownMap[index] = team;
            displayList.Add(team.DisplayName());
            index++;
        }

        PopulateDropDown(TeamsDropDown, displayList, TeamsDefaultIndex, selectedTeam);
    }

    protected virtual void BuildNumberOfHerosDropDown(int numberOfHeros)
    {
        var displayList = new List<string>();
        for (int i = 1; i <= numberOfHeros; i++)
        {
            displayList.Add(i.ToString());
        }

        PopulateDropDown(NumberOfHerosDropDown, displayList, NumberOfHerosDefaultIndex, selectedHeros);
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
    #endregion
}
