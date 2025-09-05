using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.TextCore.Text;
using static UnityEditor.FilePathAttribute;

public class DataLoader : Loadable
{
    protected static List<Character> allCharacters = new List<Character>();
    protected static List<Box> allBoxes = new List<Box>();
    protected static List<Location> allLocations = new List<Location>();
    protected static List<Challenge> allChallenges = new List<Challenge>();
    protected static List<Mode> allModes = new List<Mode>();
    protected static List<Team> allTeams = new List<Team>();
    protected static List<Equipment> allEquipment = new List<Equipment>();
    protected static List<Campaign> allCampaigns = new List<Campaign>();

    protected static Dictionary<Boxs, Box> boxMap = new Dictionary<Boxs, Box>();
    protected static Dictionary<Teams, Team> teamMap = new Dictionary<Teams, Team>();

    protected static Dictionary<GameSystems, List<Box>> boxsInGameSystem = new Dictionary<GameSystems, List<Box>>();
    protected static Dictionary<GameSystems, List<Character>> charactersInGameSystem = new Dictionary<GameSystems, List<Character>>();
    protected static Dictionary<GameSystems, List<Character>> herosInGameSystem = new Dictionary<GameSystems, List<Character>>();
    protected static Dictionary<GameSystems, List<Character>> villainsInGameSystem = new Dictionary<GameSystems, List<Character>>();
    protected static Dictionary<GameSystems, List<Character>> companionsInGameSystem = new Dictionary<GameSystems, List<Character>>();
    protected static Dictionary<GameSystems, List<Location>> locationsInGameSystem = new Dictionary<GameSystems, List<Location>>();
    protected static Dictionary<GameSystems, List<Challenge>> challengeInGameSystem = new Dictionary<GameSystems, List<Challenge>>();
    protected static Dictionary<GameSystems, List<Mode>> modesInGameSystem = new Dictionary<GameSystems, List<Mode>>();
    protected static Dictionary<GameSystems, List<Team>> teamsInGameSystem = new Dictionary<GameSystems, List<Team>>();
    protected static Dictionary<GameSystems, List<Equipment>> equipmentInGameSystem = new Dictionary<GameSystems, List<Equipment>>();
    protected static Dictionary<GameSystems, List<Campaign>> campaignsInGameSystem = new Dictionary<GameSystems, List<Campaign>>();

    #region GenerateIds
    [ButtonInspectorAttribute(nameof(GenerateIDs))]
    public bool GenerateIdsButton;
    public virtual void GenerateIDs()
    {
        foreach (var character in allCharacters)
        {
            character.ID = GameIdGenerator.GetNextId();
            EditorUtility.SetDirty(character);
        }
        foreach (var box in allBoxes)
        {
            box.ID = GameIdGenerator.GetNextId();
            EditorUtility.SetDirty(box);
        }
        foreach (var location in allLocations)
        {
            location.ID = GameIdGenerator.GetNextId();
            EditorUtility.SetDirty(location);
        }
        foreach (var challenge in allChallenges)
        {
            challenge.ID = GameIdGenerator.GetNextId();
            EditorUtility.SetDirty(challenge);
        }
        foreach (var mode in allModes)
        {
            mode.ID = GameIdGenerator.GetNextId();
            EditorUtility.SetDirty(mode);
        }
        foreach (var team in allTeams)
        {
            team.ID = GameIdGenerator.GetNextId();
            team.TeamColor = ColorManager.GetColor(team.TeamTag, out bool darkText);
            EditorUtility.SetDirty(team);
        }
        foreach (var equipment in allEquipment)
        {
            equipment.ID = GameIdGenerator.GetNextId();
            EditorUtility.SetDirty(equipment);
        }
        foreach (var campaign in allCampaigns)
        {
            campaign.ID = GameIdGenerator.GetNextId();
            EditorUtility.SetDirty(campaign);
        }
    }
    #endregion

    public override void LoadableStep1()
    {
        Load();
    }

    public async void Load()
    {
        await SaveDataController.LoadSavedData();

        LoadData();
    }       

    protected virtual void LoadData()
    {
        allCharacters.AddRange(Resources.LoadAll<Character>("Characters/"));
        allBoxes.AddRange(Resources.LoadAll<Box>("Boxes/"));
        allLocations.AddRange(Resources.LoadAll<Location>("Locations/"));
        allChallenges.AddRange(Resources.LoadAll<Challenge>("Challenges/"));
        allModes.AddRange(Resources.LoadAll<Mode>("Modes/"));
        allTeams.AddRange(Resources.LoadAll<Team>("Teams/"));
        allEquipment.AddRange(Resources.LoadAll<Equipment>("Equipment/"));
        allCampaigns.AddRange(Resources.LoadAll<Campaign>("Campaigns/"));

        foreach (var box in allBoxes)
        {
            if (box.ID == 0) TraceManager.WriteTrace(TraceChannel.Main, TraceType.error, box.DisplayNameWithClarifier() + "Missing Id");

            box.Init();

            UpdateSavedData(box);

            box.Boxs[0].Owned = box.Owned;
            box.Boxs[0].IncludeInGameBuild = box.IncludeInGameBuild;

            boxMap[box.BoxTag] = box;

            AddToAllDictionary<Box>(box.GameSystem, boxsInGameSystem, box);
        }

        foreach (var character in allCharacters)
        {
            if (character.ID == 0) TraceManager.WriteTrace(TraceChannel.Main, TraceType.error, character.DisplayNameWithClarifier() + "Missing Id");

            character.Init();

            UpdateSavedData(character);
            UpdateOwnableAndInclude(character);

            character.Teams.Clear();
            character.Equipment.Clear();

            AddToAllDictionary<Character>(character.GameSystem, charactersInGameSystem, character);

            if (character.Type == CharacterType.Villain || character.Type == CharacterType.AntiHero)
            {
                AddToAllDictionary<Character>(character.GameSystem, villainsInGameSystem, character);
            }

            if (character.Type == CharacterType.Hero || character.Type == CharacterType.AntiHero)
            {
                AddToAllDictionary<Character>(character.GameSystem, herosInGameSystem, character);
            }

            if (character.Type == CharacterType.Companion)
            {
                AddToAllDictionary<Character>(character.GameSystem, companionsInGameSystem, character);
            }

            foreach (var team in allTeams)
            {
                // if this is not for the same game system skip or if the character is already assigned to the team skip
                if (team.GameSystem != character.GameSystem || character.Teams.Contains(team.TeamTag)) continue;

                if (team.HerosOnly && character.Type == CharacterType.Villain) continue;
                if (team.VillainOnly && character.Type == CharacterType.Hero) continue;

                // if there are no characters in this team it means all characters are apart of the team
                if (team.Characters.Count <= 0)
                {
                    character.Teams.Add(team.TeamTag);
                    continue;
                }

                if (team.Characters.Contains(character)) character.Teams.Add(team.TeamTag);
            }

            foreach (var equipment in allEquipment)
            {
                //skip if not for this system
                if (equipment.GameSystem != character.GameSystem) continue;

                // skip not for villain
                if (equipment.AnyHero && character.Type == CharacterType.Villain) continue;

                // if for any hero add it
                if (equipment.Characters.Count <= 0 && equipment.AnyHero)
                {
                    character.AddDtlItem("EQUIPMENT", equipment);
                    continue;
                }

                // if its for this character add it
                if (equipment.Characters.Contains(character)) character.AddDtlItem("EQUIPMENT", equipment);
            }

            AddToBox(character);
        }

        foreach (var location in allLocations)
        {
            if (location.ID == 0) TraceManager.WriteTrace(TraceChannel.Main, TraceType.error, location.DisplayNameWithClarifier() + "Missing Id");

            location.Init();

            UpdateSavedData(location);
            UpdateOwnableAndInclude(location);

            AddToAllDictionary<Location>(location.GameSystem, locationsInGameSystem, location);

            AddToBox(location);
        }

        foreach (var challenges in allChallenges)
        {
            if (challenges.ID == 0) TraceManager.WriteTrace(TraceChannel.Main, TraceType.error, challenges.DisplayNameWithClarifier() + "Missing Id");

            challenges.Init();

            UpdateSavedData(challenges);
            UpdateOwnableAndInclude(challenges);

            AddToAllDictionary<Challenge>(challenges.GameSystem, challengeInGameSystem, challenges);

            AddToBox(challenges);
        }

        foreach (var mode in allModes)
        {
            if (mode.ID == 0) TraceManager.WriteTrace(TraceChannel.Main, TraceType.error, mode.DisplayNameWithClarifier() + "Missing Id");

            mode.Init();

            UpdateSavedData(mode);
            UpdateOwnableAndInclude(mode);

            AddToAllDictionary<Mode>(mode.GameSystem, modesInGameSystem, mode);

            AddToBox(mode);
        }

        foreach (var team in allTeams)
        {
            if (team.ID == 0) TraceManager.WriteTrace(TraceChannel.Main, TraceType.error, team.DisplayNameWithClarifier() + "Missing Id");

            team.Init();

            UpdateSavedData(team);
            UpdateOwnableAndInclude(team);

            teamMap[team.TeamTag] = team;

            AddToAllDictionary<Team>(team.GameSystem, teamsInGameSystem, team);

            AddToBox(team);
        }

        foreach (var equipment in allEquipment)
        {
            if (equipment.ID == 0) TraceManager.WriteTrace(TraceChannel.Main, TraceType.error, equipment.DisplayNameWithClarifier() + "Missing Id");

            equipment.Init();

            UpdateSavedData(equipment);
            UpdateOwnableAndInclude(equipment);

            AddToAllDictionary<Equipment>(equipment.GameSystem, equipmentInGameSystem, equipment);

            AddToBox(equipment);
        }

        foreach (var campaigns in allCampaigns)
        {
            if (campaigns.ID == 0) TraceManager.WriteTrace(TraceChannel.Main, TraceType.error, campaigns.DisplayNameWithClarifier() + "Missing Id");

            campaigns.Init();

            UpdateSavedData(campaigns);
            UpdateOwnableAndInclude(campaigns);

            AddToAllDictionary<Campaign>(campaigns.GameSystem, campaignsInGameSystem, campaigns);

            AddToBox(campaigns);
        }

        SaveDataController.Save();
    }

    protected virtual void UpdateSavedData(BoxOwnable ownable)
    {
        var savedData = SaveDataController.GetDataForId(ownable.ID);

        if (savedData != null)
        {
            ownable.SetOwnableData(savedData);
        }
        else
        {
            savedData = ownable.GetOwnableData();
            savedData.LastUpdateDate = DateTime.Now;
            SaveDataController.UpdateDataForId(ownable.ID, savedData);
        }
    }

    protected virtual void UpdateOwnableAndInclude(BoxOwnable ownable)
    {
        foreach (var box in ownable.Boxs)
        {
            if (GetBoxByTag(box.Box).Owned) box.Owned = true;
            else box.Owned = false;

            if (GetBoxByTag(box.Box).IncludeInGameBuild) box.IncludeInGameBuild = true;
            else box.IncludeInGameBuild = false;
        }
    }

    protected virtual void AddToAllDictionary<T>(GameSystems system, Dictionary<GameSystems, List<T>> dictionary, T valueToAdd)
    {
        AddToDictionary<T>(GameSystems.All, dictionary, valueToAdd);
        if (system != GameSystems.All)
        {
            AddToDictionary<T>(system, dictionary, valueToAdd);
        }
        else
        {
            foreach (GameSystems s in Enum.GetValues(typeof(GameSystems)))
            {
                if (s == GameSystems.None || s == GameSystems.All) continue;

                AddToDictionary<T>(s, dictionary, valueToAdd);
            }
        }
    }
    protected virtual void AddToDictionary<T>(GameSystems system, Dictionary<GameSystems, List<T>> dictionary, T valueToAdd)
    {
        if (!dictionary.ContainsKey(system)) dictionary[system] = new List<T>() { valueToAdd };
        else dictionary[system].Add(valueToAdd);
    }
    protected static void AddToBox(BoxOwnable ownableBox)
    {
        foreach (var box in ownableBox.Boxs)
        {
            if (!boxMap.ContainsKey(box.Box)) continue;

            if (ownableBox is Character) boxMap[box.Box].Characters.Add((Character)ownableBox);
            else if (ownableBox is Location) boxMap[box.Box].Locations.Add((Location)ownableBox);
            else if (ownableBox is Challenge) boxMap[box.Box].Challenges.Add((Challenge)ownableBox);
            else if (ownableBox is Mode) boxMap[box.Box].Modes.Add((Mode)ownableBox);
            else if (ownableBox is Team) boxMap[box.Box].Teams.Add((Team)ownableBox);
            else if (ownableBox is Equipment) boxMap[box.Box].Equipment.Add((Equipment)ownableBox);
            else if (ownableBox is Campaign) boxMap[box.Box].Campaigns.Add((Campaign)ownableBox);
        }
    }

    public static List<Character> GetCharactersBySystem(GameSystems gameSystems = GameSystems.All)
    {
        if (charactersInGameSystem.ContainsKey(gameSystems)) return charactersInGameSystem[gameSystems];
        return new List<Character>();
    }
    public static List<Character> GetHerosBySystem(GameSystems gameSystems = GameSystems.All)
    {
        if (herosInGameSystem.ContainsKey(gameSystems)) return herosInGameSystem[gameSystems];
        return new List<Character>();
    }
    public static List<Character> GetVillainsBySystem(GameSystems gameSystems = GameSystems.All)
    {
        if (villainsInGameSystem.ContainsKey(gameSystems)) return villainsInGameSystem[gameSystems];
        return new List<Character>();
    }
    public static List<Character> GetCompanionsBySystem(GameSystems gameSystems = GameSystems.All)
    {
        if (companionsInGameSystem.ContainsKey(gameSystems)) return companionsInGameSystem[gameSystems];
        return new List<Character>();
    }
    public static List<Location> GetLocationsBySystem(GameSystems gameSystems = GameSystems.All)
    {
        if (locationsInGameSystem.ContainsKey(gameSystems)) return locationsInGameSystem[gameSystems];
        return new List<Location>();
    }
    public static List<Challenge> GetChallengesBySystem(GameSystems gameSystems = GameSystems.All)
    {
        if (challengeInGameSystem.ContainsKey(gameSystems)) return challengeInGameSystem[gameSystems];
        return new List<Challenge>();
    }
    public static List<Mode> GetModesBySystem(GameSystems gameSystems = GameSystems.All)
    {
        if (modesInGameSystem.ContainsKey(gameSystems)) return modesInGameSystem[gameSystems];
        return new List<Mode>();
    }
    public static List<Team> GetTeamsBySystem(GameSystems gameSystems = GameSystems.All)
    {
        if (teamsInGameSystem.ContainsKey(gameSystems)) return teamsInGameSystem[gameSystems];
        return new List<Team>();
    }
    public static List<Equipment> GetEquipmentBySystem(GameSystems gameSystems = GameSystems.All)
    {
        if (equipmentInGameSystem.ContainsKey(gameSystems)) return equipmentInGameSystem[gameSystems];
        return new List<Equipment>();
    }
    public static List<Campaign> GetCampaignsBySystem(GameSystems gameSystems = GameSystems.All)
    {
        if (campaignsInGameSystem.ContainsKey(gameSystems)) return campaignsInGameSystem[gameSystems];
        return new List<Campaign>();
    }
    public static List<Box> GetBoxsBySystem(GameSystems gameSystems = GameSystems.All)
    {
        if (boxsInGameSystem.ContainsKey(gameSystems)) return boxsInGameSystem[gameSystems];
        return new List<Box>();
    }
    public static Box GetBoxByTag(Boxs boxTag)
    {
        if (!boxMap.ContainsKey(boxTag))
        {
            return new Box();
        }

        return boxMap[boxTag];
    }
    public static Team GetTeamByTag(Teams teamTag)
    {
        if (!teamMap.ContainsKey(teamTag)) return new Team();

        return teamMap[teamTag];
    }
}

