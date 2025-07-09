using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UI_Filter : MonoBehaviour, IDialog
{
    public GameObject GroupHeaderPrefab;
    public Color HeaderColor;
    public GameObject Content;
    public GameObject FilterPrefab;

    protected Vector3 openPos;
    protected Vector3 closePos;

    protected Dictionary<string, Dictionary<string, Filter>> filterDictionary = new Dictionary<string, Dictionary<string, Filter>>();
    protected Dictionary<SortTypes, Filter> sortDictionary = new Dictionary<SortTypes, Filter>();

    protected Filter sortIsAscending;

    protected virtual void Awake()
    {
        BuildSort();
        BuildFilterGameSystems();
        BuildFilterSeasons();
        BuildFilterOwned();
        BuildFilterType();
        BuildFilterCharacterType();
        BuildFilterCharacterSex();
    }

    protected virtual void Start()
    {
        openPos = this.transform.position;
        closePos = this.transform.position - new Vector3(-UIScreenSize.ScreenWidth(), 0, 0);
    }

    #region Build Filters
    protected virtual void BuildSort()
    {
        var dropdown = Instantiate(GroupHeaderPrefab, Content.transform).GetComponent<UI_DropDownHeader>();
        var groupManager = dropdown.AddComponent<ToggleSwitchGroupManager>();
        groupManager.AllCanBeToggleOff = false;
        groupManager.AllCanBeToggleOn = false;
        groupManager.CanMultipleGroupsBeToggleOn = false;

        var group = new List<GameObject>();

        sortIsAscending = new Filter();
        sortIsAscending.Name = "Ascending";
        sortIsAscending.FilterValue = true;

        var filterObj = Instantiate(FilterPrefab, Content.transform).GetComponent<UI_FilterOption>();
        filterObj.SetData(sortIsAscending);
        group.Add(filterObj.gameObject);
        filterObj.gameObject.SetActive(false);

        foreach (SortTypes enumValue in Enum.GetValues(typeof(SortTypes)))
        {
            if (enumValue == SortTypes.None) continue;

            var filter = new Filter();
            filter.Name = enumValue.ToFriendlyString();
            if (enumValue == SortTypes.Name) filter.FilterValue = true;
            else filter.FilterValue = false;

            sortDictionary[enumValue] = filter;
            var filterObj1 = Instantiate(FilterPrefab, Content.transform).GetComponent<UI_FilterOption>();
            filterObj1.SetData(filter);
            group.Add(filterObj1.gameObject);
            filterObj1.gameObject.SetActive(false);

            var groupDtls = new ToggleSwitchGroupManager.ToggleGroupDtls();
            groupDtls.Toggle = filterObj1.OwnedSlider;
            groupDtls.GroupNumber = 1;
            groupManager.ToggleSwitches.Add(groupDtls);
        }

        groupManager.RegisterToggleButtonsToGroup();
        groupManager.StartUpConfig();
        dropdown.SetData("Sort", group, HeaderColor, false, 55);
    }

    protected virtual void BuildFilterGameSystems()
    {
        var dropdown = Instantiate(GroupHeaderPrefab, Content.transform).GetComponent<UI_DropDownHeader>();
        filterDictionary[typeof(GameSystems).Name] = new Dictionary<string, Filter>();

        var group = new List<GameObject>();
        foreach (GameSystems enumValue in Enum.GetValues(typeof(GameSystems)))
        {
            if (enumValue == GameSystems.None || enumValue == GameSystems.All) continue;

            var filter = new Filter();
            filter.Name = enumValue.ToFriendlyString();
            filter.FilterValue = true;

            filterDictionary[typeof(GameSystems).Name][Enum.GetName(typeof(GameSystems), enumValue)] = filter;
            var filterObj = Instantiate(FilterPrefab, Content.transform).GetComponent<UI_FilterOption>();
            filterObj.SetData(filter);
            group.Add(filterObj.gameObject);
            filterObj.gameObject.SetActive(false);
        }

        dropdown.SetData("Game Systems", group, HeaderColor, false, 55);
    }

    protected virtual void BuildFilterSeasons()
    {
        var dropdown = Instantiate(GroupHeaderPrefab, Content.transform).GetComponent<UI_DropDownHeader>();
        filterDictionary[typeof(Seasons).Name] = new Dictionary<string, Filter>();

        var group = new List<GameObject>();
        foreach (Seasons enumValue in Enum.GetValues(typeof(Seasons)))
        {
            if (enumValue == Seasons.None) continue;

            var filter = new Filter();
            filter.Name = enumValue.ToFriendlyString();
            filter.FilterValue = true;

            filterDictionary[typeof(Seasons).Name][Enum.GetName(typeof(Seasons), enumValue)] = filter;
            var filterObj = Instantiate(FilterPrefab, Content.transform).GetComponent<UI_FilterOption>();
            filterObj.SetData(filter);
            group.Add(filterObj.gameObject);
            filterObj.gameObject.SetActive(false);
        }

        dropdown.SetData("Seasons", group, HeaderColor, false, 55);
    }

    protected virtual void BuildFilterOwned()
    {
        var dropdown = Instantiate(GroupHeaderPrefab, Content.transform).GetComponent<UI_DropDownHeader>();
        filterDictionary["Owned"] = new Dictionary<string, Filter>();

        var group = new List<GameObject>();

        var filter = new Filter();
        filter.Name = "Yes";
        filter.FilterValue = true;

        filterDictionary["Owned"][true.ToString()] = filter;
        var filterObj = Instantiate(FilterPrefab, Content.transform).GetComponent<UI_FilterOption>();
        filterObj.SetData(filter);
        group.Add(filterObj.gameObject);
        filterObj.gameObject.SetActive(false);

        var filter1 = new Filter();
        filter1.Name = "No";
        filter1.FilterValue = true;

        filterDictionary["Owned"][false.ToString()] = filter1;
        var filterObj1 = Instantiate(FilterPrefab, Content.transform).GetComponent<UI_FilterOption>();
        filterObj1.SetData(filter1);
        group.Add(filterObj1.gameObject);
        filterObj1.gameObject.SetActive(false);


        dropdown.SetData("Owned", group, HeaderColor, false, 55);
    }

    protected virtual void BuildFilterType()
    {
        var dropdown = Instantiate(GroupHeaderPrefab, Content.transform).GetComponent<UI_DropDownHeader>();
        filterDictionary["Type"] = new Dictionary<string, Filter>();

        var typeList = new List<string>() { typeof(Character).ToString(), typeof(Location).ToString(), typeof(Challenge).ToString(), typeof(Mode).ToString(), typeof(Team).ToString(), typeof(Box).ToString() };

        var group = new List<GameObject>();
        foreach (var type in typeList)
        {
            var filter = new Filter();
            filter.Name = type;
            filter.FilterValue = true;

            filterDictionary["Type"][type] = filter;
            var filterObj = Instantiate(FilterPrefab, Content.transform).GetComponent<UI_FilterOption>();
            filterObj.SetData(filter);
            group.Add(filterObj.gameObject);
            filterObj.gameObject.SetActive(false);
        }

        dropdown.SetData("Type", group, HeaderColor, false, 55);
    }

    protected virtual void BuildFilterCharacterType()
    {
        var dropdown = Instantiate(GroupHeaderPrefab, Content.transform).GetComponent<UI_DropDownHeader>();
        filterDictionary[typeof(CharacterType).Name] = new Dictionary<string, Filter>();

        var group = new List<GameObject>();
        foreach (CharacterType enumValue in Enum.GetValues(typeof(CharacterType)))
        {
            if (enumValue == CharacterType.None) continue;

            var filter = new Filter();
            filter.Name = enumValue.ToFriendlyString();
            filter.FilterValue = true;

            filterDictionary[typeof(CharacterType).Name][Enum.GetName(typeof(CharacterType), enumValue)] = filter;
            var filterObj = Instantiate(FilterPrefab, Content.transform).GetComponent<UI_FilterOption>();
            filterObj.SetData(filter);
            group.Add(filterObj.gameObject);
            filterObj.gameObject.SetActive(false);
        }

        dropdown.SetData("Character Type", group, HeaderColor, false, 55);
    }

    protected virtual void BuildFilterCharacterSex()
    {
        var dropdown = Instantiate(GroupHeaderPrefab, Content.transform).GetComponent<UI_DropDownHeader>();
        filterDictionary[typeof(CharacterSex).Name] = new Dictionary<string, Filter>();

        var group = new List<GameObject>();
        foreach (CharacterSex enumValue in Enum.GetValues(typeof(CharacterSex)))
        {
            if (enumValue == CharacterSex.None || enumValue == CharacterSex.Both) continue;

            var filter = new Filter();
            filter.Name = enumValue.ToFriendlyString();
            filter.FilterValue = true;

            filterDictionary[typeof(CharacterSex).Name][Enum.GetName(typeof(CharacterSex), enumValue)] = filter;
            var filterObj = Instantiate(FilterPrefab, Content.transform).GetComponent<UI_FilterOption>();
            filterObj.SetData(filter);
            group.Add(filterObj.gameObject);
            filterObj.gameObject.SetActive(false);
        }

        dropdown.SetData("Character Sex", group, HeaderColor, false, 55);
    }
    #endregion

    public virtual Sort GetSort()
    {
        return new Sort(sortIsAscending.FilterValue, GetSortType());
    }

    protected virtual SortTypes GetSortType()
    {
        foreach(var key in sortDictionary.Keys)
        {
            if (sortDictionary[key].FilterValue) return key;
        }

        return SortTypes.Name;
    }

    // return true if included in filter
    public virtual bool CheckFilter(Searchable searchable)
    {
        // not the right type no need to check further
        if (!filterDictionary["Type"][searchable.GetType().ToString()].FilterValue) return false;

        var isExcluded = true;
        foreach(var key in searchable.Filter().Keys)
        {
            isExcluded = true;
            foreach (var specific in searchable.Filter()[key])
            {
                if (specific == "All" || specific == "Both")
                {
                    foreach(var value in filterDictionary[key].Keys)
                    {
                        if (filterDictionary[key][value].FilterValue)
                        {
                            isExcluded = false;
                            break;
                        }
                    }
                }
                else if (filterDictionary[key][specific].FilterValue) isExcluded = false;

                if (!isExcluded) break;
            }

            if (isExcluded) return false;
        }
        return true;
    }

    #region IDialog
    public virtual void Open()
    {
        LeanTween.move(this.gameObject, openPos, 0.2f);
    }
    public virtual void Close()
    {
        GameEventSystem.UI_OnCloseFilterPopup();
        LeanTween.move(this.gameObject, closePos, 0.2f);
    }
    #endregion
}