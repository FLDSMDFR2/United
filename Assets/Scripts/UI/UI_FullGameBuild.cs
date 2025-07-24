using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_FullGameBuild : UI_Generation
{
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

    [Header("Companions")]
    public TMP_Dropdown NumberOfCompanionsDropDown;
    public int NumberOfCompanionsDefaultIndex;
    protected string selectedCompanions;
    public int MaxNumberOfCompanions;

    protected int numberOfLocations = 6;

    protected Dictionary<int, Mode> modesDropDownMap = new Dictionary<int, Mode>();
    protected Dictionary<int, Challenge> challengesDropDownMap = new Dictionary<int, Challenge>();
    protected Dictionary<int, Character> villainsDropDownMap = new Dictionary<int, Character>();
    protected Dictionary<int, Team> teamsDropDownMap = new Dictionary<int, Team>();

    // get all data
    protected List<Mode> availableModes = new List<Mode>();
    protected List<Challenge> availableChalleneges = new List<Challenge>();
    protected List<Character> availableVillains = new List<Character>();
    protected List<Team> availableTeams = new List<Team>();
    protected List<Location> availableLocations = new List<Location>();
    protected List<Character> availableHeros = new List<Character>();
    protected List<Character> availableCompanions = new List<Character>();

    protected bool updatingDropDowns;

    protected override void Start()
    {
        BuildNumberOfHerosDropDown(MaxNumberOfHeros);
        BuildNumberOfCompanionsDropDown(MaxNumberOfCompanions);

        base.Start();
    }

    protected override void UpdatedDropDowns()
    {
        if (updatingDropDowns) return;
        updatingDropDowns = true;

        UpdateAvailableMode();
        BuildModesDropDown(availableModes);

        UpdateAvailableChallenges();
        BuildChallengesDropDown(availableChalleneges);

        UpdateAvailableVillain();
        BuildVillainsDropDown(availableVillains);

        UpdateAvailableTeams();
        BuildTeamsDropDown(availableTeams);

        updatingDropDowns = false;
    }

    #region Update Available

    #region Update Available Mode
    protected virtual void UpdateAvailableMode()
    {
        availableModes.Clear();
        availableModes.AddRange(DataLoader.GetModesBySystem(GetActiveSystem()));
        var removeModes = new List<Mode>();
        foreach (Mode mode in availableModes)
        {
            if (!mode.IncludeInGameBuild)
            {
                removeModes.Add(mode);
                continue;
            }
            if (VillainsDropDown.value != 0 && !IsModeValidForVillain(mode, villainsDropDownMap[VillainsDropDown.value]))
            {
                removeModes.Add(mode);
                continue;
            }
            if (ChallengeDropDown.value != 0 && ChallengeDropDown.value != 1 && !IsModeValidForChallenge(mode, challengesDropDownMap[ChallengeDropDown.value]))
            {
                removeModes.Add(mode);
                continue;
            }
        }

        foreach(var mode in removeModes)
        {
            availableModes.Remove(mode);
        }    
    }

    protected virtual bool IsModeValidForVillain(Mode mode, Character villain)
    {
        foreach (var game in mode.Games)
        {
            if (game.ExcludedVillains.Count > 0 && game.ExcludedVillains.Contains(villain)) return false;
            if (game.RequiaredVillains.Count > 0 && game.RequiaredVillainsExclusively)
            {
                var found = false;
                foreach(var vill in game.RequiaredVillains)
                {
                    if (vill.RequiredObjects.Contains(villain))
                    {
                        found = true;
                        break;
                    }
                }
                if (!found) return false;
            }
            if (!mode.AllowVillainSelection) return false;
        }

        return true;
    }

    protected virtual bool IsModeValidForChallenge(Mode mode, Challenge challenge)
    {
        foreach (var game in mode.Games)
        {
            if (game.ExcludedChallenges.Count > 0 && game.ExcludedChallenges.Contains(challenge)) return false;
            if (game.RequiaredChallenges.Count > 0)
            {
                var found = false;
                foreach (var chall in game.RequiaredChallenges)
                {
                    if (chall.RequiredObjects.Contains(challenge))
                    {
                        found = true;
                        break;
                    }
                }
                if (!found) return false;
            }
            if (!mode.AllowChallengeSelection) return false;
        }

        return true;
    }
    #endregion

    #region Update Available Challenges
    protected virtual void UpdateAvailableChallenges()
    {
        availableChalleneges.Clear();
        availableChalleneges.AddRange(DataLoader.GetChallengesBySystem(GetActiveSystem()));

        // if the game mode random
        if (GameModesDropDown.value == 1 || modesDropDownMap[GameModesDropDown.value].AllowChallengeSelection)
        {
            if (GameModesDropDown.value != 1) UpdateAvailableChallengesForMode(modesDropDownMap[GameModesDropDown.value]);
            ChallengeObject.SetActive(true);
        }
        else
        {
            // if no villain selection allowed hide selection
            ChallengeObject.SetActive(false);
            return;
        }

        ChallengesExlusions();
    }

    protected virtual void UpdateAvailableChallengesForMode(Mode mode)
    {
        foreach (var game in mode.Games)
        {
            foreach (var challenge in game.ExcludedChallenges)
            {
                if (availableChalleneges.Contains(challenge)) availableChalleneges.Remove(challenge);
            }
        }
    }

    protected virtual void ChallengesExlusions()
    {
        var removeModes = new List<Challenge>();
        foreach (var challenge in availableChalleneges)
        {
            if (!challenge.IncludeInGameBuild)
            {
                removeModes.Add(challenge);
                continue;
            }

            if (VillainsDropDown.value != 0 && challenge.GameDtl.ExcludedVillains.Contains(villainsDropDownMap[VillainsDropDown.value]))
            {
                removeModes.Add(challenge);
                continue;
            }
        }

        foreach (var challenge in removeModes)
        {
            availableChalleneges.Remove(challenge);
        }
    }
    #endregion

    #region Update Available Villain
    protected virtual void UpdateAvailableVillain()
    {
        availableVillains.Clear();
        availableVillains.AddRange(DataLoader.GetVillainsBySystem(GetActiveSystem()));

        // if the game mode random then allow full villain selection
        if (GameModesDropDown.value <= 1 ||  modesDropDownMap[GameModesDropDown.value].AllowVillainSelection)
        {
            if (GameModesDropDown.value != 1) UpdateAvailableVillainsForMode(modesDropDownMap[GameModesDropDown.value]);
            VillainsObject.SetActive(true);
        }
        else
        {
            // if no villain selection allowed hide selection
            VillainsObject.SetActive(false);
            return;
        }

        VillianExlusions();

        if (ChallengeDropDown.value != 0 && ChallengeDropDown.value != 1)
        {
            UpdateAvailableVillainsForChallenge(challengesDropDownMap[ChallengeDropDown.value]);
        }    
    }

    protected virtual void VillianExlusions()
    {
        var removeList = new List<Character>();
        foreach (var villain in availableVillains)
        {
            if (!villain.IncludeInGameBuild)
            {
                removeList.Add(villain);
                continue;
            }
            if (!villain.IsStandAloneVillain)
            {
                removeList.Add(villain);
                continue;
            }
        }

        foreach (var villain in removeList)
        {
            availableVillains.Remove(villain);
        }
    }

    protected virtual void UpdateAvailableVillainsForMode(Mode mode)
    {
        // this should only get called if we allow selection
        // we should only remove what is not allowed
        foreach (var game in mode.Games)
        {
            foreach(var villain in game.ExcludedVillains)
            {
                if (availableVillains.Contains(villain)) availableVillains.Remove(villain);
            }
        }
    }

    protected virtual void UpdateAvailableVillainsForChallenge(Challenge challenge)
    {
        foreach (var villain in challenge.GameDtl.ExcludedVillains)
        {
            if (availableVillains.Contains(villain)) availableVillains.Remove(villain);
        }
    }
    #endregion

    #region Update Available Teams
    protected virtual void UpdateAvailableTeams()
    {
        availableTeams.Clear();
        availableTeams.AddRange(DataLoader.GetTeamsBySystem(GetActiveSystem()));

        TeamsExlusions();
    }

    protected virtual void TeamsExlusions()
    {
        var removeModes = new List<Team>();
        foreach (var team in availableTeams)
        {
            if (!team.IncludeInGameBuild)
            {
                removeModes.Add(team);
                continue;
            }
        }

        foreach (var team in removeModes)
        {
            availableTeams.Remove(team);
        }
    }
    #endregion

    #region Update Available Heros
    protected virtual void UpdateAvailableHeros()
    {
        availableHeros.Clear();
        availableHeros.AddRange(DataLoader.GetHerosBySystem(GetActiveSystem()));

        HeroExlusions();
    }

    protected virtual void HeroExlusions()
    {
        var removeModes = new List<Character>();
        foreach (var hero in availableHeros)
        {
            if (!hero.IncludeInGameBuild)
            {
                removeModes.Add(hero);
                continue;
            }
        }

        foreach (var hero in removeModes)
        {
            availableHeros.Remove(hero);
        }
    }
    #endregion

    #region Update Available Locations
    protected virtual void UpdateAvailableLocations()
    {
        availableLocations.Clear();
        availableLocations.AddRange(DataLoader.GetLocationsBySystem(GetActiveSystem()));

        LocationExlusions();
    }

    protected virtual void LocationExlusions()
    {
        var removeModes = new List<Location>();
        foreach (var location in availableLocations)
        {
            if (!location.IncludeInGameBuild)
            {
                removeModes.Add(location);
                continue;
            }
        }

        foreach (var location in removeModes)
        {
            availableLocations.Remove(location);
        }
    }
    #endregion

    #region Update Available Companions
    protected virtual void UpdateAvailableCompanions()
    {
        availableCompanions.Clear();
        availableCompanions.AddRange(DataLoader.GetCompanionsBySystem(GetActiveSystem()));
    }
    #endregion
    
    #endregion

    #region GenerateGame
    public override void GenerateGameSelected()
    {
        UpdateAvailableHeros();
        UpdateAvailableLocations();
        UpdateAvailableCompanions();

        if (!ValidateAvailableData())
        {
            UpdatedDropDowns();
            return;
        }

        var buildData = new BuildGameData();

        buildData.Mode.Add(GenerateGameMode());

        buildData = GenerateChallenges(buildData);
        if (buildData == null || !ValidateChallenges(buildData))
        {
            UpdatedDropDowns();
            return;
        }

        buildData = GenerateVillains(buildData);
        if (buildData == null || !ValidateVillains(buildData))
        {
            UpdatedDropDowns();
            return;
        }

        buildData = GenerateLocations(buildData);
        if (buildData == null || !ValidateLocations(buildData))
        {
            UpdatedDropDowns();
            return;
        }

        buildData = GenerateHeros(buildData);
        if (buildData == null || !ValidateHeros(buildData))
        {
            UpdatedDropDowns();
            return;
        }

        buildData = GenerateCompanions(buildData);
        if (buildData == null || !ValidateCompanions(buildData))
        {
            UpdatedDropDowns();
            return;
        }

        UpdatedDropDowns();
        ErrorText(false);
        GameEventSystem.UI_OnShowBuiltGame(buildData);
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
            return availableModes[RandomGenerator.UnseededRange(0, availableModes.Count)];
        }
        else
        {
            return modesDropDownMap[GameModesDropDown.value];
        }
    }

    protected virtual BuildGameData GenerateChallenges(BuildGameData gameData)
    {
        UpdateAvailableChallengesForMode(gameData.Mode[0]);

        var required = new List<Required>();

        for (int i = 0; i < gameData.Mode[0].Games.Count; i++)
        {
            if (gameData.Games.Count == i) gameData.Games.Add(new BuildGame());

            if (!gameData.Mode[0].AllowChallengeSelection) continue;

            required.Clear();
            required.AddRange(BuildRequiaredList(gameData.Mode[0].Games[i].RequiaredChallenges));

            // if the mode doesnt requiare any challenges and NONE was selected then skip adding Challenges
            if (required.Count <= 0 && ChallengeDropDown.value == 0) continue;

            if (required.Count > 0 && required[0].NumberRequired > required[0].RequiredObjects.Count)
            {
                ErrorText(true, "Requaired Challenge for mode not available");
                return null;
            }

            // try and select a Challenge
            Challenge selected;
            if (ChallengeDropDown.value == 1)
            {
                // random 
                if (required.Count > 0) selected = (Challenge)required[0].RequiredObjects[RandomGenerator.UnseededRange(0, required[0].RequiredObjects.Count)];
                else selected = availableChalleneges[RandomGenerator.UnseededRange(0, availableChalleneges.Count)];
            }
            else
            {
                selected = challengesDropDownMap[ChallengeDropDown.value];
                if (!availableChalleneges.Contains(selected))
                {
                    ErrorText(true, "Selected Challenge not valid");
                    return null;
                }
            }

            if (!availableChalleneges.Contains(selected))
            {
                if (required.Count > 0) UpdateRequired(required, selected, false);
                i--;
                continue;
            }

            // this is a good Challenge we will use it and remove it from available
            availableChalleneges.Remove(selected);
            gameData.Games[i].Challenges.Add(selected);
            if (required.Count > 0) UpdateRequired(required, selected, true);
        }

        return gameData;
    }

    protected virtual BuildGameData GenerateVillains(BuildGameData gameData)
    {
        UpdateVillainsForModeAndChallenege(gameData);

        var availableVillainsForGame = new List<Character>();
        availableVillainsForGame.AddRange(availableVillains);

        var required = new List<Required>();

        for (int i = 0; i < gameData.Mode[0].Games.Count; i++)
        {
            required.Clear();
            required.AddRange(BuildRequiaredList(gameData.Mode[0].Games[i].RequiaredVillains));
            if (gameData.Games[i].Challenges != null && gameData.Games[i].Challenges.Count > 0 &&
                gameData.Games[i].Challenges[0] != null) required.AddRange(BuildRequiaredList(gameData.Games[i].Challenges[0].GameDtl.RequiaredVillains));

            var numberOfVillainsForGame = gameData.Mode[0].Games[i].NumberOfVillains;


            for (int j = 0; j < numberOfVillainsForGame; j++)
            {
                if (required.Count > 0 && required[0].NumberRequired > required[0].RequiredObjects.Count)
                {
                    ErrorText(true, "Requaired villains for mode not available");
                    return null;
                }

                // try and select a villain
                Character selected;
                if (VillainsDropDown.value == 0  || !VillainsObject.activeSelf)
                {
                    // random villain
                    if (required.Count > 0) selected = (Character)required[0].RequiredObjects[RandomGenerator.UnseededRange(0, required[0].RequiredObjects.Count)];
                    else selected = availableVillainsForGame[RandomGenerator.UnseededRange(0, availableVillainsForGame.Count)];
                }
                else
                {
                    selected = villainsDropDownMap[VillainsDropDown.value];
                    if (!availableVillainsForGame.Contains(selected) && gameData.Games[i].Villains.Contains(selected) && required.Count > 0)
                    {
                        selected = (Character)required[0].RequiredObjects[RandomGenerator.UnseededRange(0, required[0].RequiredObjects.Count)];
                    }
                    else if (!availableVillainsForGame.Contains(selected))
                    {
                        ErrorText(true, "Selected Villain not valid");
                        return null;
                    }
                }

                if ((!IsVillainValid(gameData.Mode[0].Games[i], gameData.Games[i], selected)) || (!availableVillainsForGame.Contains(selected)))
                {
                    availableVillainsForGame.Remove(selected);
                    if (required.Count > 0) UpdateRequired(required, selected, false);
                    j--;
                    continue;
                }

                // this is a good villain we will use it and remove it from available
                availableVillainsForGame.Remove(selected);
                if (availableHeros.Contains(selected)) availableHeros.Remove(selected);
                gameData.Games[i].Villains.Add(selected);
                if (required.Count > 0) UpdateRequired(required, selected, true);

                // update for required after we select it
                numberOfVillainsForGame += selected.GameDtl.NumberOfVillains;
                required.AddRange(BuildRequiaredList(selected.GameDtl.RequiaredVillains));
                foreach (var vill in selected.GameDtl.RequiaredVillains)
                {
                    foreach (Character obj in vill.RequiredObjects)
                    {
                        if (!availableVillainsForGame.Contains(obj)) availableVillainsForGame.Add(obj);
                    }
                }
            }
        }

        return gameData;
    }

    protected virtual void UpdateVillainsForModeAndChallenege(BuildGameData gameData)
    {
        for (int i = 0; i < gameData.Mode[0].Games.Count; i++)
        {
            if (gameData.Mode[0].Games[i].RequiaredVillains.Count > 0)
            {
                foreach(var vill in gameData.Mode[0].Games[i].RequiaredVillains)
                {
                    foreach (Character obj in vill.RequiredObjects)
                    {
                        if (!availableVillains.Contains(obj)) availableVillains.Add(obj);
                    }
                }
            }

            if (gameData.Games[i].Challenges != null && gameData.Games[i].Challenges.Count > 0 &&
                gameData.Games[i].Challenges[0] != null && gameData.Games[i].Challenges[0].GameDtl.RequiaredVillains.Count > 0)
            {
                foreach (var vill in gameData.Games[i].Challenges[0].GameDtl.RequiaredVillains)
                {
                    foreach (Character obj in vill.RequiredObjects)
                    {
                        if (!availableVillains.Contains(obj)) availableVillains.Add(obj);
                    }
                }
            }
        }
    }

    protected virtual bool IsVillainValid(GameDtl modeDtl, BuildGame gameData, Character villain)
    {
        //check if valid for mode
        if (modeDtl.ExcludedVillains.Contains(villain)) return false;

        //check if valid for challenege
        if (gameData.Challenges != null && gameData.Challenges.Count > 0 &&
            gameData.Challenges[0] != null && gameData.Challenges[0].GameDtl.ExcludedVillains.Contains(villain)) return false;

        return true;
    }

    protected virtual BuildGameData GenerateLocations(BuildGameData gameData)
    {
        var availableLocationsForGame = new List<Location>();
        var required = new List<Required>();
        for (int i = 0; i < gameData.Mode[0].Games.Count; i++)
        {
            availableLocationsForGame.Clear();
            availableLocationsForGame.AddRange(availableLocations);

            required.Clear();
            required.AddRange(BuildRequiaredList(gameData.Mode[0].Games[i].RequiaredLocations));
            if (gameData.Games[i].Challenges != null && gameData.Games[i].Challenges.Count > 0 &&
                gameData.Games[i].Challenges[0] != null) required.AddRange(BuildRequiaredList(gameData.Games[i].Challenges[0].GameDtl.RequiaredLocations));

            foreach (var villain in gameData.Games[i].Villains)
            {
                required.AddRange(BuildRequiaredList(villain.GameDtl.RequiaredLocations));
            }

            if (TotalRequired(required) > numberOfLocations)
            {
                ErrorText(true, "To many requiared locations needed");
                return null;
            }

            for (int j = 0; j < numberOfLocations; j++)
            {
                if (required.Count > 0 && required[0].NumberRequired > required[0].RequiredObjects.Count)
                {
                    ErrorText(true, "Requaired location for mode not available");
                    return null;
                }

                if (availableLocationsForGame.Count <= 0)
                {
                    ErrorText(true, "Locations not available");
                    return null;
                }

                // try and select a locations
                Location selected;
                if (required.Count > 0)
                {
                    selected = (Location)required[0].RequiredObjects[RandomGenerator.UnseededRange(0, required[0].RequiredObjects.Count)];
                }
                else
                {
                    selected = availableLocationsForGame[RandomGenerator.UnseededRange(0, availableLocationsForGame.Count)];
                }

                // if this location is exlusive to a mode and its not this one
                // or if this location is exlusive to a mode and it is this one but not this game
                if ((selected.ExclusiveForMode.Count > 0 && !selected.ExclusiveForMode.Contains(gameData.Mode[0].ModeTag)) || 
                    (selected.ExclusiveForMode.Count > 0 && selected.ExclusiveForMode.Contains(gameData.Mode[0].ModeTag)  && !RequiredContains(required, selected)))
                {
                    // reset and try and find another location this one is not elegiable for this game type
                    availableLocationsForGame.Remove(selected);
                    j--;
                    continue;
                }

                if (!IsLocationValid(gameData.Mode[0].Games[i], gameData.Games[i], selected) || (!availableLocationsForGame.Contains(selected)) || gameData.Games[i].Locations.Contains(selected))
                {
                    availableLocationsForGame.Remove(selected);
                    if (required.Count > 0) UpdateRequired(required, selected, false);
                    j--;
                    continue;
                }

                // this is a good location we will use it and remove it from available
                gameData.Games[i].Locations.Add(selected);
                availableLocationsForGame.Remove(selected);
                if (required.Count > 0) UpdateRequired(required, selected, true);
            }
        }

        return gameData;
    }

    protected virtual bool IsLocationValid(GameDtl modeDtl, BuildGame gameData, Location location)
    {
        //check if valid for mode
        if (modeDtl.ExcludedLocations.Contains(location)) return false;

        //check if valid for challenege
        if (gameData.Challenges != null && gameData.Challenges.Count > 0 &&
            gameData.Challenges[0] != null && gameData.Challenges[0].GameDtl.ExcludedLocations.Contains(location)) return false;

        //check if valid for all villians
        foreach(var villain in gameData.Villains)
        {
            if (villain.GameDtl.ExcludedLocations.Contains(location)) return false;
        }

        return true;
    }

    protected virtual BuildGameData GenerateHeros(BuildGameData gameData)
    {
        var team = UpdateHerosForTeam();

        var availableHerosForGame = new List<Character>();
        var required = new List<Required>();
        for (int i = 0; i < gameData.Mode[0].Games.Count; i++)
        {
            if (team != null) gameData.Games[i].Teams.Add(team);

            // if we are going to use the same heros for all games just copy heros from first game to the rest
            if (i > 0 && gameData.Mode[0].UseSameHerosForAllGames)
            {
                gameData.Games[i].Heros.AddRange(gameData.Games[0].Heros);
                gameData.Games[i].AdditionalHeros.AddRange(gameData.Games[0].AdditionalHeros);
                continue;
            }

            availableHerosForGame.Clear();
            availableHerosForGame.AddRange(availableHeros);

            required.Clear();
            required.AddRange(BuildRequiaredList(gameData.Mode[0].Games[i].RequiredHeros));
            if (gameData.Games[i].Challenges != null && gameData.Games[i].Challenges.Count > 0 &&
                gameData.Games[i].Challenges[0] != null) required.AddRange(BuildRequiaredList(gameData.Games[i].Challenges[0].GameDtl.RequiredHeros));

            foreach (var villain in gameData.Games[i].Villains)
            {
                required.AddRange(BuildRequiaredList(villain.GameDtl.RequiredHeros));
            }

            for (int j = 0; j <= NumberOfHerosDropDown.value; j++)
            {
                if (required.Count > 0 && required[0].NumberRequired > required[0].RequiredObjects.Count)
                {
                    ErrorText(true, "Requaired heros for mode not available");
                    return null;
                }

                if (availableHerosForGame.Count <= 0)
                {
                    ErrorText(true, "Not enough heros available");
                    return null;
                }

                // try and select a Character
                Character selected;
                if (required.Count > 0)
                {
                    selected = (Character)required[0].RequiredObjects[RandomGenerator.UnseededRange(0, required[0].RequiredObjects.Count)];
                }
                else
                {
                    selected = availableHerosForGame[RandomGenerator.UnseededRange(0, availableHerosForGame.Count)];
                }

                if ((!IsHeroValid(gameData.Mode[0].Games[i], gameData.Games[i], selected)) || (!availableHerosForGame.Contains(selected)))
                {
                    availableHerosForGame.Remove(selected);
                    if (required.Count > 0) UpdateRequired(required, selected, false);
                    j--;
                    continue;
                }

                // this is a good hero we will use it
                gameData.Games[i].Heros.Add(selected);
                availableHerosForGame.Remove(selected);
                if (required.Count > 0) UpdateRequired(required, selected, true);
            }

            if (gameData.Mode[0].Games[i].AdditionalHeros != 0)
            {
                var AdditionalHerosCount = gameData.Mode[0].Games[i].AdditionalHeros;
                if (gameData.Mode[0].Games[i].AdditionalHerosReferenceNumHeros) AdditionalHerosCount = (NumberOfHerosDropDown.value + 1) + gameData.Mode[0].Games[i].AdditionalHeros;

                for (int j = 0; j < AdditionalHerosCount; j++)
                {
                    if (required.Count > 0 && required[0].NumberRequired > required[0].RequiredObjects.Count)
                    {
                        ErrorText(true, "Not enough required heros available for Additional");
                        return null;
                    }

                    // try and select a Character
                    Character selected;
                    if (required.Count > 0)
                    {
                        selected = (Character)required[0].RequiredObjects[RandomGenerator.UnseededRange(0, required[0].RequiredObjects.Count)];
                    }
                    else
                    {
                        selected = availableHerosForGame[RandomGenerator.UnseededRange(0, availableHerosForGame.Count)];
                    }

                    if (!IsHeroValid(gameData.Mode[0].Games[i], gameData.Games[i], selected) || (!availableHerosForGame.Contains(selected)))
                    {
                        availableHerosForGame.Remove(selected);
                        if (required.Count > 0) UpdateRequired(required, selected, false);
                        j--;
                        continue;
                    }

                    // this is a good hero we will use it
                    gameData.Games[i].AdditionalHeros.Add(selected);
                    availableHerosForGame.Remove(selected);
                    if (required.Count > 0) UpdateRequired(required, selected, true);
                }
            }
        }

        return gameData;
    }

    protected virtual Team UpdateHerosForTeam()
    {
        if (TeamsDropDown.value == 0) return null;

        Team selected;
        if (TeamsDropDown.value == 1)
        {
            selected = availableTeams[RandomGenerator.UnseededRange(0, availableTeams.Count)];
        }
        else
        {
            selected = teamsDropDownMap[TeamsDropDown.value];
        }

        var removeList = new List<Character>();
        foreach (var hero in availableHeros) 
        {
            if (!hero.Teams.Contains(selected.TeamTag)) removeList.Add(hero);
        }

        foreach (var hero in removeList)
        {
            availableHeros.Remove(hero);
        }

        return selected;
    }

    protected virtual bool IsHeroValid(GameDtl modeDtl, BuildGame gameData, Character hero)
    {
        //check if valid for mode
        if (modeDtl.ExcludedHeros.Contains(hero)) return false;

        //check if valid for challenege
        if (gameData.Challenges != null && gameData.Challenges.Count > 0 &&
            gameData.Challenges[0] != null && gameData.Challenges[0].GameDtl.ExcludedHeros.Contains(hero)) return false;

        //check if valid for all villians
        foreach (var villain in gameData.Villains)
        {
            if (villain.GameDtl.ExcludedHeros.Contains(hero)) return false;
        }

        return true;
    }

    protected virtual BuildGameData GenerateCompanions(BuildGameData gameData)
    {
        var availableCompanionsForGame = new List<Character>();
        var required = new List<Required>();
        for (int i = 0; i < gameData.Mode[0].Games.Count; i++)
        {
            // if we are going to use the same heros for all games just copy Companions from first game to the rest
            if (i > 0 && gameData.Mode[0].UseSameHerosForAllGames)
            {
                gameData.Games[i].Companions.AddRange(gameData.Games[0].Companions);
                continue;
            }

            availableCompanionsForGame.Clear();
            availableCompanionsForGame.AddRange(availableCompanions);

            required.Clear();
            //required.AddRange(BuildRequiaredList(gameData.Mode.Games[i].RequiredHeros));
            //if (gameData.Games[i].Challenges != null && gameData.Games[i].Challenges.Count > 0 &&
            //gameData.Games[i].Challenges[0] != null) required.AddRange(BuildRequiaredList(gameData.Games[i].Challenge.GameDtl.RequiredHeros));

            for (int j = 0; j < NumberOfCompanionsDropDown.value; j++)
            {
                if (required.Count > 0 && required[0].NumberRequired > required[0].RequiredObjects.Count)
                {
                    ErrorText(true, "Requaired Companions for mode not available");
                    return null;
                }

                if (availableCompanionsForGame.Count <= 0)
                {
                    ErrorText(true, "Not enough Companions available");
                    return null;
                }

                // try and select a Character
                Character selected;
                if (required.Count > 0)
                {
                    selected = (Character)required[0].RequiredObjects[RandomGenerator.UnseededRange(0, required[0].RequiredObjects.Count)];
                }
                else
                {
                    selected = availableCompanionsForGame[RandomGenerator.UnseededRange(0, availableCompanionsForGame.Count)];
                }

                if ((!IsCompanionValid(gameData.Mode[0].Games[i], gameData.Games[i], selected)) || (!availableCompanionsForGame.Contains(selected)))
                {
                    availableCompanionsForGame.Remove(selected);
                    if (required.Count > 0) UpdateRequired(required, selected, false);
                    j--;
                    continue;
                }

                // this is a good Companions we will use it
                gameData.Games[i].Companions.Add(selected);
                availableCompanionsForGame.Remove(selected);
                if (required.Count > 0) UpdateRequired(required, selected, true);
            }
        }

        return gameData;
    }

    protected virtual bool IsCompanionValid(GameDtl modeDtl, BuildGame gameData, Character hero)
    {
        return true;
    }

    #region Requiared Helpers
    protected virtual List<Required> BuildRequiaredList(List<Required> required)
    {
        List <Required> retval = new List <Required>();

        if (required == null || required.Count <= 0) return retval;

        foreach (var requiared in required)
        {
            AddToRequiredList(retval, requiared);
        }

        return retval;
    }

    protected virtual void AddToRequiredList(List<Required> required, Required requiredToAdd)
    {
        if (requiredToAdd == null || requiredToAdd.NumberRequired <= 0 || 
            requiredToAdd.RequiredObjects == null || requiredToAdd.RequiredObjects.Count <= 0) return;

        Required req = new Required();
        req.NumberRequired = requiredToAdd.NumberRequired;
        req.RequiredObjects.AddRange(requiredToAdd.RequiredObjects);

        required.Add(req);
    }

    protected virtual int TotalRequired(List<Required> required)
    {
        if (required == null || required.Count <= 0) return 0;

        var sum = 0;
        foreach (var req in required)
        {
            sum += req.NumberRequired;
        }
        return sum;
    }

    protected virtual void UpdateRequired(List<Required> required, BoxOwnable selected, bool isSuccess)
    {
        if (required == null || required.Count <= 0) return;

        if (required[0].NumberRequired < 1) required.RemoveAt(0);

        if (required.Count <= 0) return;

        if (required[0].RequiredObjects.Contains(selected)) required[0].RequiredObjects.Remove(selected);
        if (isSuccess) required[0].NumberRequired -= 1;
        if (required[0].NumberRequired < 1) required.RemoveAt(0);
    }

    protected virtual bool RequiredContains(List<Required> required, BoxOwnable selected)
    {
        if (required == null || required.Count <= 0) return false;

        foreach (var req in required)
        {
            if (req.RequiredObjects.Contains(selected)) return true;
        }

        return false;
    }

    #endregion

    #endregion

    #region Validation

    protected virtual bool ValidateAvailableData()
    {
        if (availableModes.Count <= 0)
        {
            ErrorText(true, "No Available Modes Included");
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
        if (gameData.Games.Count != gameData.Mode[0].Games.Count)
        {
            ErrorText(true, "Mode Game count miss match");
            return false;
        }

        //for (int i = 0; i < gameData.Games.Count; i ++)
        //{
        //    if (gameData.Games[i].Villains.Count != gameData.Mode.Games[i].NumberOfVillains)
        //    {
        //        ErrorText(true, "Villain to game count miss match");
        //        return false;
        //    }
        //}

        return true;
    }

    protected virtual bool ValidateLocations(BuildGameData gameData)
    {
        if (gameData.Games.Count != gameData.Mode[0].Games.Count)
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
        if (gameData.Games.Count != gameData.Mode[0].Games.Count)
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

            if (gameData.Mode[0].Games[i].AdditionalHeros > 0)
            {
                var AdditionalHerosCount = gameData.Mode[0].Games[i].AdditionalHeros;
                if (gameData.Mode[0].Games[i].AdditionalHerosReferenceNumHeros) AdditionalHerosCount = (NumberOfHerosDropDown.value + 1) + gameData.Mode[0].Games[i].AdditionalHeros;

                if (gameData.Games[i].AdditionalHeros.Count != AdditionalHerosCount)
                {
                    ErrorText(true, "Additional Heros to game count miss match");
                    return false;
                }
            }
    
        }

        return true;
    }

    protected virtual bool ValidateCompanions(BuildGameData gameData)
    {
        if (NumberOfCompanionsDropDown.value == 0)
        {
            // none selected
            return true;
        }
        if (gameData.Games.Count != gameData.Mode[0].Games.Count)
        {
            ErrorText(true, "Mode Game count miss match");
            return false;
        }

        for (int i = 0; i < gameData.Games.Count; i++)
        {
            if (gameData.Games[i].Companions.Count != (NumberOfCompanionsDropDown.value))
            {
                ErrorText(true, "Companions to game count miss match");
                return false;
            }
        }

        return true;
    }

    protected virtual bool ValidateChallenges(BuildGameData gameData)
    {
        return true;
    }

    #endregion

    #region Handlers
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

    public virtual void CompanionsSelected()
    {
        selectedCompanions = NumberOfCompanionsDropDown.options[NumberOfCompanionsDropDown.value].text;
        UpdatedDropDowns();
    }
    #endregion

    #region Populate Drop Down Helpers
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
                displayList.Add(gameMode.DisplayNameWithClarifier());
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
            displayList.Add(gameChallenge.DisplayNameWithClarifier());
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
            displayList.Add(villain.DisplayNameWithClarifier());
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
            displayList.Add(team.DisplayNameWithClarifier());
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

    protected virtual void BuildNumberOfCompanionsDropDown(int numberOfCompanions)
    {
        var displayList = new List<string>();
        displayList.Add("None");

        for (int i = 1; i <= numberOfCompanions; i++)
        {
            displayList.Add(i.ToString());
        }

        PopulateDropDown(NumberOfCompanionsDropDown, displayList, NumberOfCompanionsDefaultIndex, selectedCompanions);
    }
    #endregion
}
