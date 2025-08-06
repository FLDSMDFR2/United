using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.FilePathAttribute;

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
            if (modes.Count < CountDropDown.value + 1)
            {
                ErrorText(true, "Not Enough Modes Included");
                return;
            }

            for (int i = 0; i <= CountDropDown.value; i++)
            {
                var selected = modes[RandomGenerator.UnseededRange(0, modes.Count)];
                buildData.Mode.Add(selected);
                modes.Remove(selected);
            }
        }
        else if (selectedType == SingleItemGenerationType.Challenge)
        {
            var challenge = DataLoader.GetChallengesBySystem(GetActiveSystem());
            if (challenge.Count < CountDropDown.value + 1)
            {
                ErrorText(true, "Not Enough Challenges Included");
                return;
            }

            for (int i = 0; i <= CountDropDown.value; i++)
            {
                var selected = challenge[RandomGenerator.UnseededRange(0, challenge.Count)];
                buildData.Games[0].Challenges.Add(selected);
                challenge.Remove(selected);
            }
        }
        else if (selectedType == SingleItemGenerationType.Villain)
        {
            var villain = DataLoader.GetVillainsBySystem(GetActiveSystem());
            if (villain.Count < CountDropDown.value + 1)
            {
                ErrorText(true, "Not Enough Villains Included");
                return;
            }

            for (int i = 0; i <= CountDropDown.value; i++)
            {
                var selected = villain[RandomGenerator.UnseededRange(0, villain.Count)];
                buildData.Games[0].Villains.Add(selected);
                villain.Remove(selected);
            }
        }
        else if (selectedType == SingleItemGenerationType.Team)
        {
            var team = DataLoader.GetTeamsBySystem(GetActiveSystem());
            if (team.Count < CountDropDown.value + 1)
            {
                ErrorText(true, "Not Enough Teams Included");
                return;
            }

            for (int i = 0; i <= CountDropDown.value; i++)
            {
                var selected = team[RandomGenerator.UnseededRange(0, team.Count)];
                buildData.Games[0].Teams.Add(selected);
                team.Remove(selected);
            }
        }
        else if (selectedType == SingleItemGenerationType.Hero)
        {
            var hero = DataLoader.GetHerosBySystem(GetActiveSystem());
            if (hero.Count < CountDropDown.value + 1)
            {
                ErrorText(true, "Not Enough Heros Included");
                return;
            }

            for (int i = 0; i <= CountDropDown.value; i++)
            {
                var selected = hero[RandomGenerator.UnseededRange(0, hero.Count)];
                buildData.Games[0].Heros.Add(selected);
                hero.Remove(selected);
            }
        }
        else if (selectedType == SingleItemGenerationType.Companion)
        {
            var companion = DataLoader.GetCompanionsBySystem(GetActiveSystem());
            if (companion.Count < CountDropDown.value + 1)
            {
                ErrorText(true, "Not Enough Companions Included");
                return;
            }

            for (int i = 0; i <= CountDropDown.value; i++)
            {
                var selected = companion[RandomGenerator.UnseededRange(0, companion.Count)];
                buildData.Games[0].Companions.Add(selected);
                companion.Remove(selected);
            }
        }
        else if (selectedType == SingleItemGenerationType.Location)
        {
            var location = DataLoader.GetLocationsBySystem(GetActiveSystem());
            if (location.Count < CountDropDown.value + 1)
            {
                ErrorText(true, "Not Enough Locations Included");
                return;
            }

            for (int i = 0; i <= CountDropDown.value; i++)
            {
                var selected = location[RandomGenerator.UnseededRange(0, location.Count)];
                buildData.Games[0].Locations.Add(selected);
                location.Remove(selected);
            }
        }
        else if (selectedType == SingleItemGenerationType.Equipment)
        {
            var equipment = DataLoader.GetEquipmentBySystem(GetActiveSystem());
            if (equipment.Count < CountDropDown.value + 1)
            {
                ErrorText(true, "Not Enough Equipment Included");
                return;
            }

            for (int i = 0; i <= CountDropDown.value; i++)
            {
                var selected = equipment[RandomGenerator.UnseededRange(0, equipment.Count)];
                buildData.Games[0].Equipment.Add(selected);
                equipment.Remove(selected);
            }
        }
        else if (selectedType == SingleItemGenerationType.Campaign)
        {
            var campaign = DataLoader.GetCampaignsBySystem(GetActiveSystem());
            if (campaign.Count < CountDropDown.value + 1)
            {
                ErrorText(true, "Not Enough Campaigns Included");
                return;
            }

            for (int i = 0; i <= CountDropDown.value; i++)
            {
                var selected = campaign[RandomGenerator.UnseededRange(0, campaign.Count)];
                buildData.Games[0].Campaigns.Add(selected);
                campaign.Remove(selected);
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
