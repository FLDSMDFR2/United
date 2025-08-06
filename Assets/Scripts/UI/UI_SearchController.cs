using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_SearchController : Loadable
{
    [Header("Base Search")]
    public GameObject BaseSearchUIPrefab;
    [Header("Chracter")]
    public GameObject ChracterUIPrefab;

    public ScrollRect SearchScrollRect;
    public GameObject SearchView;
    public TMP_InputField SearchBox;
    public int MaxDisplay = 100;


    public TextMeshProUGUI CurrentPageNumber;
    public TextMeshProUGUI MaxPageNumber;
    public UI_Filter Filter;

    protected int currentPage;
    protected int maxPage;

    protected Dictionary<ItemTypes, List<UI_SearchDtl>> displayList = new Dictionary<ItemTypes, List<UI_SearchDtl>>();
    protected int workingIndex;

    protected List<Searchable> fullSearchContent = new List<Searchable>();
    protected List<Searchable> searchToDisplay = new List<Searchable>();
    protected Searchable[] sortedDisplay;

    public override void LoadableStep2()
    {
        BuildSearchableContent();
        PreBuildDisplay();
        Search();

        GameEventSystem.UI_CloseFilterPopup += GameEventSystem_UI_CloseFilterPopup;
    }

    protected virtual void BuildSearchableContent()
    {
        fullSearchContent.AddRange(DataLoader.GetBoxsBySystem());
        fullSearchContent.AddRange(DataLoader.GetCharactersBySystem());
        fullSearchContent.AddRange(DataLoader.GetLocationsBySystem());
        fullSearchContent.AddRange(DataLoader.GetChallengesBySystem());
        fullSearchContent.AddRange(DataLoader.GetModesBySystem());
        fullSearchContent.AddRange(DataLoader.GetTeamsBySystem());
        fullSearchContent.AddRange(DataLoader.GetCampaignsBySystem());
        fullSearchContent.AddRange(DataLoader.GetEquipmentBySystem());
    }

    protected virtual void PreBuildDisplay()
    {
        for (int i = 0; i < MaxDisplay; i++)
        {
            CreateNewDtl();
        }

        HideAllDtls();
    }

    protected virtual void DisplaySearchPage(int page)
    {
        SearchScrollRect.verticalNormalizedPosition = 1f;

        if (sortedDisplay == null || sortedDisplay.Count() <= 0)
        {
            HideAllDtls();
            return;
        }

        maxPage = (sortedDisplay.Count() / MaxDisplay);
        if (sortedDisplay.Count() % MaxDisplay != 0) maxPage++;

        if (page <= 0 || page > maxPage) return;

        HideAllDtls();

        currentPage = page;

        CurrentPageNumber.text = currentPage.ToString();
        MaxPageNumber.text = "-" + maxPage.ToString();

        var startIndex = (currentPage - 1) * MaxDisplay;
        var endIndex = 0;
        if (currentPage == maxPage) endIndex = startIndex + (sortedDisplay.Count() - startIndex) - 1;
        else endIndex = (startIndex + MaxDisplay) - 1;


        for (int i = startIndex; i <= endIndex; i ++)
        {
            if (sortedDisplay[i] is Character)
            {
                displayList[ItemTypes.Characters][workingIndex].SetData(sortedDisplay[i]);
                displayList[ItemTypes.Characters][workingIndex].gameObject.SetActive(true);
                workingIndex++;
            }
            else if (sortedDisplay[i] is Location || sortedDisplay[i] is Challenge || sortedDisplay[i] is Mode || sortedDisplay[i] is Team || sortedDisplay[i] is Box || sortedDisplay[i] is Campaign || sortedDisplay[i] is Equipment)
            {
                displayList[ItemTypes.Generic][workingIndex].SetData(sortedDisplay[i]);
                displayList[ItemTypes.Generic][workingIndex].gameObject.SetActive(true);
                workingIndex++;
            }
        }
    }

    public virtual void Search()
    {
        searchToDisplay.Clear();

        PerformSearch(SearchBox.text);

        PerformSort();

        DisplaySearchPage(1);
    }

    protected virtual void PerformSearch(string searchText)
    {
        foreach (var searchObj in fullSearchContent)
        {
            if (!string.IsNullOrEmpty(searchText))
                if (!(searchObj.SearchName()).ToLower().Contains(searchText.ToLower())) continue;

            if (!Filter.CheckFilter(searchObj)) continue;

            searchToDisplay.Add(searchObj);
        }
    }
    protected virtual void PerformSort()
    {
        var sort = Filter.GetSort();

        if (sort.IsAscending)
        {
            if (sort.Type == SortTypes.Name) sortedDisplay = searchToDisplay.OrderBy(s => s.GetSortString(sort.Type)).ToArray();
            else if (sort.Type == SortTypes.HeroMoveIcons || sort.Type == SortTypes.HeroAttackIcons || sort.Type == SortTypes.HeroHeroicIcons ||
                sort.Type == SortTypes.HeroWildIcons || sort.Type == SortTypes.HeroSpecailCards || sort.Type == SortTypes.HeroWins || sort.Type == SortTypes.HeroLosses
                || sort.Type == SortTypes.VillainWins || sort.Type == SortTypes.VillainLosses) sortedDisplay = searchToDisplay.OrderBy(s => s.GetSortInt(sort.Type)).ToArray();
            else if (sort.Type == SortTypes.HeroRating || sort.Type == SortTypes.VillainRating) sortedDisplay = searchToDisplay.OrderBy(s => s.GetSortFloat(sort.Type)).ToArray();
        }
        else
        {
            if (sort.Type == SortTypes.Name) sortedDisplay = searchToDisplay.OrderByDescending(s => s.GetSortString(sort.Type)).ToArray();
            else if (sort.Type == SortTypes.HeroMoveIcons || sort.Type == SortTypes.HeroAttackIcons || sort.Type == SortTypes.HeroHeroicIcons ||
                sort.Type == SortTypes.HeroWildIcons || sort.Type == SortTypes.HeroSpecailCards || sort.Type == SortTypes.HeroWins || sort.Type == SortTypes.HeroLosses
                || sort.Type == SortTypes.VillainWins || sort.Type == SortTypes.VillainLosses) sortedDisplay = searchToDisplay.OrderByDescending(s => s.GetSortInt(sort.Type)).ToArray();
            else if (sort.Type == SortTypes.HeroRating || sort.Type == SortTypes.VillainRating) sortedDisplay = searchToDisplay.OrderByDescending(s => s.GetSortFloat(sort.Type)).ToArray();
        }
    }

    protected virtual void CreateNewDtl()
    {
        if (!displayList.ContainsKey(ItemTypes.Characters)) displayList[ItemTypes.Characters] = new List<UI_SearchDtl>();
        displayList[ItemTypes.Characters].Add(Instantiate(ChracterUIPrefab, SearchView.transform).GetComponent<UI_SearchDtlCharacter>());

        if (!displayList.ContainsKey(ItemTypes.Generic)) displayList[ItemTypes.Generic] = new List<UI_SearchDtl>();
        displayList[ItemTypes.Generic].Add(Instantiate(BaseSearchUIPrefab, SearchView.transform).GetComponent<UI_SearchDtl>());
    }

    protected virtual void HideAllDtls()
    {
        foreach (var key in displayList.Keys)
        {
            workingIndex = 0;
            foreach (var item in displayList[key])
            {
                item.ResetData();
                item.gameObject.SetActive(false);
            }
        }
    }

    public virtual void PageUp()
    {
        DisplaySearchPage(currentPage + 1);
    }
    public virtual void PageDown()
    {
        DisplaySearchPage(currentPage - 1);
    }
    protected virtual void GameEventSystem_UI_CloseFilterPopup()
    {
        Search();
    }
}
