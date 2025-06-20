using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class UI_SearchController : MonoBehaviour
{
    [Header("Chracter")]
    public GameObject ChracterUIPrefab;
    [Header("Location")]
    public GameObject LocationUIPrefab;

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

    protected virtual void Start()
    {
        if (ChracterUIPrefab == null) return;

        BuildSearchableContent();
        PreBuildDisplay();
        Search();
    }

    protected virtual void BuildSearchableContent()
    {
        fullSearchContent.AddRange(DataLoader.GetBoxsBySystem());
        fullSearchContent.AddRange(DataLoader.GetCharactersBySystem());
        fullSearchContent.AddRange(DataLoader.GetLocationsBySystem());
        fullSearchContent.AddRange(DataLoader.GetChallengesBySystem());
        fullSearchContent.AddRange(DataLoader.GetModesBySystem());
        fullSearchContent.AddRange(DataLoader.GetTeamsBySystem());
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
            else if (sortedDisplay[i] is Location || sortedDisplay[i] is Challenge || sortedDisplay[i] is Mode || sortedDisplay[i] is Team || sortedDisplay[i] is Box)
            {
                displayList[ItemTypes.Locations][workingIndex].SetData(sortedDisplay[i]);
                displayList[ItemTypes.Locations][workingIndex].gameObject.SetActive(true);
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
         sortedDisplay = searchToDisplay.OrderBy(s => s.SearchName()).ToArray();
    }

    protected virtual void CreateNewDtl()
    {
        if (!displayList.ContainsKey(ItemTypes.Characters)) displayList[ItemTypes.Characters] = new List<UI_SearchDtl>();
        displayList[ItemTypes.Characters].Add(Instantiate(ChracterUIPrefab, SearchView.transform).GetComponent<UI_Character>());

        if (!displayList.ContainsKey(ItemTypes.Locations)) displayList[ItemTypes.Locations] = new List<UI_SearchDtl>();
        displayList[ItemTypes.Locations].Add(Instantiate(LocationUIPrefab, SearchView.transform).GetComponent<UI_Location>());
    }

    protected virtual void HideAllDtls()
    {
        foreach (var key in displayList.Keys)
        {
            workingIndex = 0;
            foreach (var item in displayList[key])
            {
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
}
