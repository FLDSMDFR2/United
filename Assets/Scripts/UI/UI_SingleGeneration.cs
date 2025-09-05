using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_SingleGeneration : UI_Generation
{
    [Header("Type")]
    public TMP_Dropdown TyperopDown;
    public int TypeDefaultIndex;
    protected string selectedType;

    [Header("Count")]
    public TMP_Dropdown CountDropDown;
    public int CountDefaultIndex;
    protected string selectedCount;
    public int MaxCount;

    protected List<SingleItemGenerationType> typeDropDownMap = new List<SingleItemGenerationType>();

    public override void LoadableStep2()
    {
        BuildTypesDropDown(Enum.GetValues(typeof(SingleItemGenerationType)));
        BuildCountDropDown(MaxCount);

        base.LoadableStep2();
    }

    public override void GenerateGameSelected()
    {
        var buildData = new BuildGameData();
        buildData.Games.Add(new BuildGame());

        var selectedType = typeDropDownMap[TyperopDown.value];
        if (selectedType == SingleItemGenerationType.Mode)
        {
            var modes = DataLoader.GetModesBySystem(GetActiveSystem());
            List<Mode> availableModes = new List<Mode>();
            foreach(var mode in modes)
            {
                if (mode.IncludeInGameBuild) availableModes.Add(mode);
            }

            if (availableModes.Count < CountDropDown.value + 1)
            {
                ErrorText(true, "Not Enough Modes Included");
                return;
            }

            for (int i = 0; i <= CountDropDown.value; i++)
            {
                var selected = availableModes[RandomGenerator.UnseededRange(0, availableModes.Count)];
                buildData.Mode.Add(selected);
                availableModes.Remove(selected);
            }
        }
        else if (selectedType == SingleItemGenerationType.Challenge)
        {
            var challenges = DataLoader.GetChallengesBySystem(GetActiveSystem());
            List<Challenge> availableChallenges = new List<Challenge>();
            foreach (var challenge in challenges)
            {
                if (challenge.IncludeInGameBuild) availableChallenges.Add(challenge);
            }

            if (availableChallenges.Count < CountDropDown.value + 1)
            {
                ErrorText(true, "Not Enough Challenges Included");
                return;
            }

            for (int i = 0; i <= CountDropDown.value; i++)
            {
                var selected = availableChallenges[RandomGenerator.UnseededRange(0, availableChallenges.Count)];
                buildData.Games[0].Challenges.Add(selected);
                availableChallenges.Remove(selected);
            }
        }
        else if (selectedType == SingleItemGenerationType.Villain)
        {
            var villains = DataLoader.GetVillainsBySystem(GetActiveSystem());
            List<Character> availableVillains = new List<Character>();
            foreach (var villain in villains)
            {
                if (villain.IncludeInGameBuild) availableVillains.Add(villain);
            }

            if (availableVillains.Count < CountDropDown.value + 1)
            {
                ErrorText(true, "Not Enough Villains Included");
                return;
            }

            for (int i = 0; i <= CountDropDown.value; i++)
            {
                var selected = availableVillains[RandomGenerator.UnseededRange(0, availableVillains.Count)];
                buildData.Games[0].Villains.Add(selected);
                availableVillains.Remove(selected);
            }
        }
        else if (selectedType == SingleItemGenerationType.Team)
        {
            var teams = DataLoader.GetTeamsBySystem(GetActiveSystem());
            List<Team> availableTeams = new List<Team>();
            foreach (var team in teams)
            {
                if (team.IncludeInGameBuild) availableTeams.Add(team);
            }

            if (availableTeams.Count < CountDropDown.value + 1)
            {
                ErrorText(true, "Not Enough Teams Included");
                return;
            }

            for (int i = 0; i <= CountDropDown.value; i++)
            {
                var selected = availableTeams[RandomGenerator.UnseededRange(0, availableTeams.Count)];
                buildData.Games[0].Teams.Add(selected);
                availableTeams.Remove(selected);
            }
        }
        else if (selectedType == SingleItemGenerationType.Hero)
        {
            var heros = DataLoader.GetHerosBySystem(GetActiveSystem());
            List<Character> availableHeros = new List<Character>();
            foreach (var hero in heros)
            {
                if (hero.IncludeInGameBuild) availableHeros.Add(hero);
            }

            if (availableHeros.Count < CountDropDown.value + 1)
            {
                ErrorText(true, "Not Enough Heros Included");
                return;
            }

            for (int i = 0; i <= CountDropDown.value; i++)
            {
                var selected = availableHeros[RandomGenerator.UnseededRange(0, availableHeros.Count)];
                buildData.Games[0].Heros.Add(selected);
                availableHeros.Remove(selected);
            }
        }
        else if (selectedType == SingleItemGenerationType.Companion)
        {
            var companions = DataLoader.GetCompanionsBySystem(GetActiveSystem());
            List<Character> availableCompanions = new List<Character>();
            foreach (var companion in companions)
            {
                if (companion.IncludeInGameBuild) availableCompanions.Add(companion);
            }

            if (availableCompanions.Count < CountDropDown.value + 1)
            {
                ErrorText(true, "Not Enough Companions Included");
                return;
            }

            for (int i = 0; i <= CountDropDown.value; i++)
            {
                var selected = availableCompanions[RandomGenerator.UnseededRange(0, availableCompanions.Count)];
                buildData.Games[0].Companions.Add(selected);
                availableCompanions.Remove(selected);
            }
        }
        else if (selectedType == SingleItemGenerationType.Location)
        {
            var locations = DataLoader.GetLocationsBySystem(GetActiveSystem());
            List<Location> availableLocations = new List<Location>();
            foreach (var location in locations)
            {
                if (location.IncludeInGameBuild) availableLocations.Add(location);
            }

            if (availableLocations.Count < CountDropDown.value + 1)
            {
                ErrorText(true, "Not Enough Locations Included");
                return;
            }

            for (int i = 0; i <= CountDropDown.value; i++)
            {
                var selected = availableLocations[RandomGenerator.UnseededRange(0, availableLocations.Count)];
                buildData.Games[0].Locations.Add(selected);
                availableLocations.Remove(selected);
            }
        }
        else if (selectedType == SingleItemGenerationType.Equipment)
        {
            var equipments = DataLoader.GetEquipmentBySystem(GetActiveSystem());
            List<Equipment> availableEquipments = new List<Equipment>();
            foreach (var equipment in equipments)
            {
                if (equipment.IncludeInGameBuild) availableEquipments.Add(equipment);
            }

            if (availableEquipments.Count < CountDropDown.value + 1)
            {
                ErrorText(true, "Not Enough Equipment Included");
                return;
            }

            for (int i = 0; i <= CountDropDown.value; i++)
            {
                var selected = availableEquipments[RandomGenerator.UnseededRange(0, availableEquipments.Count)];
                buildData.Games[0].Equipment.Add(selected);
                availableEquipments.Remove(selected);
            }
        }
        else if (selectedType == SingleItemGenerationType.Campaign)
        {
            var campaigns = DataLoader.GetCampaignsBySystem(GetActiveSystem());
            List<Campaign> availableCampaigns = new List<Campaign>();
            foreach (var campaign in campaigns)
            {
                if (campaign.IncludeInGameBuild) availableCampaigns.Add(campaign);
            }

            if (availableCampaigns.Count < CountDropDown.value + 1)
            {
                ErrorText(true, "Not Enough Campaigns Included");
                return;
            }

            for (int i = 0; i <= CountDropDown.value; i++)
            {
                var selected = availableCampaigns[RandomGenerator.UnseededRange(0, availableCampaigns.Count)];
                buildData.Games[0].Campaigns.Add(selected);
                availableCampaigns.Remove(selected);
            }
        }

        UpdatedDropDowns();
        ErrorText(false);
        GameEventSystem.UI_OnShowBuiltGame(buildData);
    }

    #region Build
    protected virtual void BuildTypesDropDown(System.Array types)
    {
        var displayList = new List<string>();
        foreach (SingleItemGenerationType type in types)
        {
            if (type == SingleItemGenerationType.None) continue;

            typeDropDownMap.Add(type);
            displayList.Add(type.ToFriendlyString());
        }

        PopulateDropDown(TyperopDown, displayList, TypeDefaultIndex, selectedType);
    }

    protected virtual void BuildCountDropDown(int count)
    {
        var displayList = new List<string>();

        for (int i = 1; i <= count; i++)
        {
            displayList.Add(i.ToString());
        }

        PopulateDropDown(CountDropDown, displayList, CountDefaultIndex, selectedCount);
    }
    #endregion

    #region Handler
    public virtual void TypeSelected()
    {
        selectedType = TyperopDown.options[TyperopDown.value].text;
        UpdatedDropDowns();
    }

    public virtual void CountSelected()
    {
        selectedCount = CountDropDown.options[CountDropDown.value].text;
        UpdatedDropDowns();
    }
    #endregion

}
