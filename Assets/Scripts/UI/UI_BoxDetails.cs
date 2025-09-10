using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_BoxDetails : Loadable, IDialog
{
    public ScrollRect ScrollRect;
    public GameObject BoxHeader;
    public Color HeaderColor;
    public GameObject BoxDtlPrefab;
    public GameObject GroupView;
    public GameObject GroupHeaderPrefab;

    [Header("Character")]
    public int CharacterMaxCount;
    [Header("Locations")]
    public int LocationsMaxCount;
    [Header("Challenges")]
    public int ChallengesMaxCount;
    [Header("Modes")]
    public int ModesMaxCount;
    [Header("Teams")]
    public int TeamsMaxCount;
    [Header("Equipment")]
    public int EquipmentMaxCount;
    [Header("Campaign")]
    public int CampaignMaxCount;

    protected Box Box;

    protected Dictionary<ItemTypes, List<UI_BoxDtl>> groupDictionary = new Dictionary<ItemTypes, List<UI_BoxDtl>>();
    protected Dictionary<ItemTypes, UI_DropDownHeader> groupHeader = new Dictionary<ItemTypes, UI_DropDownHeader>();

    protected UI_Header boxHeader;
    protected bool forGameBuild;

    protected Vector3 openPos;
    protected Vector3 closePos;
    protected float openTime = 0.2f;
    protected float closeTime = 0.2f;

    public override void LoadableStep1()
    {
        openPos = this.transform.position;
        closePos = this.transform.position - new Vector3(0, UIScreenSize.ScreenHeight(), 0);
        this.transform.position = closePos;

        boxHeader = Instantiate(BoxHeader, GroupView.transform).GetComponent<UI_Header>();

        PreBuildCharacterDisplay();
        PreBuildLocationsDisplay();
        PreBuildChallengesDisplay();
        PreBuildModesDisplay();
        PreBuildTeamDisplay();
        PreBuildEquipmentDisplay();
        PreBuildCampaignDisplay();
    }

    public virtual void SetData(Box box, bool isForGameBuild)
    {
        this.Box = box;
        forGameBuild = isForGameBuild;

        if (BoxDtlPrefab == null) return;
        if (boxHeader != null) boxHeader.SetData(box.BoxColor,Color.white,box.GetDisplayNameWithClarifier());
        HideAllDtls();

        BuildCharactersForBox();
        BuildLocationsForBox();
        BuildChallengesForBox();
        BuildModesForBox();
        BuildTeamsForBox();
        BuildEquipmentForBox();
        BuildCampaignForBox();
    }

    #region Characters
    protected virtual void PreBuildCharacterDisplay()
    {
        groupHeader[ItemTypes.Characters] = Instantiate(GroupHeaderPrefab, GroupView.transform).GetComponent<UI_DropDownHeader>();

        for (int i = 0; i < CharacterMaxCount; i++)
        {
            CreateNewDtl(ItemTypes.Characters);
        }
    }

    protected virtual void BuildCharactersForBox()
    {
        var index = 0;
        var group = new List<GameObject>();
        foreach (var character in Box.Characters)
        {
            groupDictionary[ItemTypes.Characters][index].gameObject.SetActive(true);
            groupDictionary[ItemTypes.Characters][index].SetData(character, this.Box, forGameBuild, ColorManager.GetColor(character.Type, out bool darkText));
            groupDictionary[ItemTypes.Characters][index].gameObject.SetActive(false);
            group.Add(groupDictionary[ItemTypes.Characters][index].gameObject);
            index++;
        }

        if (index <= 0)
        {
            groupHeader[ItemTypes.Characters].gameObject.SetActive(false);
        }
        else
        {
            groupHeader[ItemTypes.Characters].gameObject.SetActive(true);
            groupHeader[ItemTypes.Characters].SetData("Characters", group, HeaderColor, true, 65);
        }
    }
    #endregion

    #region Locations
    protected virtual void PreBuildLocationsDisplay()
    {
        groupHeader[ItemTypes.Locations] = Instantiate(GroupHeaderPrefab, GroupView.transform).GetComponent<UI_DropDownHeader>();

        for (int i = 0; i < LocationsMaxCount; i++)
        {
            CreateNewDtl(ItemTypes.Locations);
        }
    }

    protected virtual void BuildLocationsForBox()
    {
        var index = 0;
        var group = new List<GameObject>();
        foreach (var location in Box.Locations)
        {
            groupDictionary[ItemTypes.Locations][index].gameObject.SetActive(true);
            groupDictionary[ItemTypes.Locations][index].SetData(location, this.Box, forGameBuild, Color.gray);
            groupDictionary[ItemTypes.Locations][index].gameObject.SetActive(false);
            group.Add(groupDictionary[ItemTypes.Locations][index].gameObject);
            index++;
        }

        if (index <= 0)
        {
            groupHeader[ItemTypes.Locations].gameObject.SetActive(false);
        }
        else
        {
            groupHeader[ItemTypes.Locations].gameObject.SetActive(true);
            groupHeader[ItemTypes.Locations].SetData("Locations", group, HeaderColor, true, 65);
        }
    }
    #endregion

    #region Challenges
    protected virtual void PreBuildChallengesDisplay()
    {
        groupHeader[ItemTypes.Challenges] = Instantiate(GroupHeaderPrefab, GroupView.transform).GetComponent<UI_DropDownHeader>();

        for (int i = 0; i < ChallengesMaxCount; i++)
        {
            CreateNewDtl(ItemTypes.Challenges);
        }
    }

    protected virtual void BuildChallengesForBox()
    {
        var index = 0;
        var group = new List<GameObject>();
        foreach (var challenge in Box.Challenges)
        {
            groupDictionary[ItemTypes.Challenges][index].gameObject.SetActive(true);
            groupDictionary[ItemTypes.Challenges][index].SetData(challenge, this.Box, forGameBuild, Color.gray);
            groupDictionary[ItemTypes.Challenges][index].gameObject.SetActive(false);
            group.Add(groupDictionary[ItemTypes.Challenges][index].gameObject);
            index++;
        }

        if (index <= 0)
        {
            groupHeader[ItemTypes.Challenges].gameObject.SetActive(false);
        }
        else
        {
            groupHeader[ItemTypes.Challenges].gameObject.SetActive(true);
            groupHeader[ItemTypes.Challenges].SetData("Challenges", group, HeaderColor, true, 65);
        }
    }
    #endregion

    #region Modes
    protected virtual void PreBuildModesDisplay()
    {
        groupHeader[ItemTypes.Modes] = Instantiate(GroupHeaderPrefab, GroupView.transform).GetComponent<UI_DropDownHeader>();

        for (int i = 0; i < ModesMaxCount; i++)
        {
            CreateNewDtl(ItemTypes.Modes);
        }
    }

    protected virtual void BuildModesForBox()
    {
        var index = 0;
        var group = new List<GameObject>();
        foreach (var mode in Box.Modes)
        {
            groupDictionary[ItemTypes.Modes][index].gameObject.SetActive(true);
            groupDictionary[ItemTypes.Modes][index].SetData(mode, this.Box, forGameBuild, Color.gray);
            groupDictionary[ItemTypes.Modes][index].gameObject.SetActive(false);
            group.Add(groupDictionary[ItemTypes.Modes][index].gameObject);
            index++;
        }

        if (index <= 0)
        {
            groupHeader[ItemTypes.Modes].gameObject.SetActive(false);
        }
        else
        {
            groupHeader[ItemTypes.Modes].gameObject.SetActive(true);
            groupHeader[ItemTypes.Modes].SetData("Modes", group, HeaderColor, true, 65);
        }
    }
    #endregion

    #region Teams
    protected virtual void PreBuildTeamDisplay()
    {
        groupHeader[ItemTypes.Teams] = Instantiate(GroupHeaderPrefab, GroupView.transform).GetComponent<UI_DropDownHeader>();

        for (int i = 0; i < TeamsMaxCount; i++)
        {
            CreateNewDtl(ItemTypes.Teams);
        }
    }

    protected virtual void BuildTeamsForBox()
    {
        var index = 0;
        var group = new List<GameObject>();
        foreach (var team in Box.Teams)
        {
            groupDictionary[ItemTypes.Teams][index].gameObject.SetActive(true);
            groupDictionary[ItemTypes.Teams][index].SetData(team, this.Box, forGameBuild, Color.gray);
            groupDictionary[ItemTypes.Teams][index].gameObject.SetActive(false);
            group.Add(groupDictionary[ItemTypes.Teams][index].gameObject);
            index++;
        }

        if (index <= 0)
        {
            groupHeader[ItemTypes.Teams].gameObject.SetActive(false);
        }
        else
        {
            groupHeader[ItemTypes.Teams].gameObject.SetActive(true);
            groupHeader[ItemTypes.Teams].SetData("Teams", group, HeaderColor, true, 65);
        }
    }
    #endregion

    #region Equipment
    protected virtual void PreBuildEquipmentDisplay()
    {
        groupHeader[ItemTypes.Equipment] = Instantiate(GroupHeaderPrefab, GroupView.transform).GetComponent<UI_DropDownHeader>();

        for (int i = 0; i < EquipmentMaxCount; i++)
        {
            CreateNewDtl(ItemTypes.Equipment);
        }
    }

    protected virtual void BuildEquipmentForBox()
    {
        var index = 0;
        var group = new List<GameObject>();
        foreach (var equipment in Box.Equipment)
        {
            groupDictionary[ItemTypes.Equipment][index].gameObject.SetActive(true);
            groupDictionary[ItemTypes.Equipment][index].SetData(equipment, this.Box, forGameBuild, Color.gray);
            groupDictionary[ItemTypes.Equipment][index].gameObject.SetActive(false);
            group.Add(groupDictionary[ItemTypes.Equipment][index].gameObject);
            index++;
        }

        if (index <= 0)
        {
            groupHeader[ItemTypes.Equipment].gameObject.SetActive(false);
        }
        else
        {
            groupHeader[ItemTypes.Equipment].gameObject.SetActive(true);
            groupHeader[ItemTypes.Equipment].SetData("Equipment", group, HeaderColor, true, 65);
        }
    }
    #endregion

    #region Campaign
    protected virtual void PreBuildCampaignDisplay()
    {
        groupHeader[ItemTypes.Campaign] = Instantiate(GroupHeaderPrefab, GroupView.transform).GetComponent<UI_DropDownHeader>();

        for (int i = 0; i < CampaignMaxCount; i++)
        {
            CreateNewDtl(ItemTypes.Campaign);
        }
    }

    protected virtual void BuildCampaignForBox()
    {
        var index = 0;
        var group = new List<GameObject>();
        foreach (var campaign in Box.Campaigns)
        {
            groupDictionary[ItemTypes.Campaign][index].gameObject.SetActive(true);
            groupDictionary[ItemTypes.Campaign][index].SetData(campaign, this.Box, forGameBuild, Color.gray);
            groupDictionary[ItemTypes.Campaign][index].gameObject.SetActive(false);
            group.Add(groupDictionary[ItemTypes.Campaign][index].gameObject);
            index++;
        }

        if (index <= 0)
        {
            groupHeader[ItemTypes.Campaign].gameObject.SetActive(false);
        }
        else
        {
            groupHeader[ItemTypes.Campaign].gameObject.SetActive(true);
            groupHeader[ItemTypes.Campaign].SetData("Campaign", group, HeaderColor, true, 65);
        }
    }
    #endregion

    protected virtual void CreateNewDtl(ItemTypes group)
    {
        if (groupDictionary.ContainsKey(group)) groupDictionary[group].Add(Instantiate(BoxDtlPrefab, GroupView.transform).GetComponent<UI_BoxDtl>());
        else groupDictionary[group] = new List<UI_BoxDtl>() { Instantiate(BoxDtlPrefab, GroupView.transform).GetComponent<UI_BoxDtl>() };
    }

    protected virtual void HideAllDtls()
    {
        foreach (var key in groupDictionary.Keys)
        {
            foreach(var dtl in groupDictionary[key])
            {
                dtl.gameObject.SetActive(false);
            }
        }
    }

    public virtual void SelectAll()
    {
        foreach (var key in groupDictionary.Keys)
        {
            foreach (var collection in groupDictionary[key])
            {
                collection.SetSliderValue(true);
            }
        }
    }

    public virtual void UnSelectAll()
    {
        foreach (var key in groupDictionary.Keys)
        {
            foreach (var collection in groupDictionary[key])
            {
                collection.SetSliderValue(false);
            }
        }
    }

    #region IDialog
    public virtual void Open()
    {
        this.gameObject.SetActive(true);
        LeanTween.move(this.gameObject, openPos, openTime).setOnComplete(OpenComplete);
    }

    public virtual void OpenComplete()
    {
        ScrollRect.verticalNormalizedPosition = 1f;
    }
    public virtual void Close()
    {
        LeanTween.move(this.gameObject, closePos, closeTime).setOnComplete(CloseComplete);
    }

    public virtual void CloseComplete()
    {
        this.gameObject.SetActive(false);
    }
    #endregion
}
