using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class UI_SearchController : MonoBehaviour
{
    public GameObject ChracterUIPrefab;
    public GameObject SearchView;
    public TMP_InputField SearchBox;
    public int MaxDisplay = 100;

    protected List<UI_Character> displayList = new List<UI_Character>();
    protected List<Character> charactersToDisplay = new List<Character>();

    protected virtual void Start()
    {
        if (ChracterUIPrefab == null) return;

        DisplayAll();
        PreBuildDisplay();
        DisplayCharacters();
    }

    protected virtual void DisplayAll()
    {
        charactersToDisplay.AddRange(DataLoader.GetAllCharacters());
    }

    protected virtual void PreBuildDisplay()
    {
        for (int i = 0; i < MaxDisplay; i++)
        {
            CreateNewDtl();
        }

        HideAllDtls();
    }

    protected virtual void DisplayCharacters()
    {
        HideAllDtls();

        if (charactersToDisplay.Count > MaxDisplay || charactersToDisplay.Count <= 0)
        {
            //show text
        }

        for (int i = 0; i < charactersToDisplay.Count; i++)
        {
            displayList[i].SetData(charactersToDisplay[i]);
            displayList[i].gameObject.SetActive(true);
        }
    }

    public virtual void Search()
    {
        var searchText = SearchBox.text;
        charactersToDisplay.Clear();

        if (string.IsNullOrEmpty(searchText))
        {
            DisplayAll();
        }
        else
        {
            foreach (var character in DataLoader.GetAllCharacters())
            {
                if ((character.CharacterClarifier+character.CharacterName).ToLower().Contains(searchText.ToLower())) charactersToDisplay.Add(character);
            }
        }
        
        DisplayCharacters();
    }

    protected virtual void CreateNewDtl()
    {
        displayList.Add(Instantiate(ChracterUIPrefab, SearchView.transform).GetComponent<UI_Character>());
    }

    protected virtual void HideAllDtls()
    {
        foreach (var dtl in displayList)
        {
            dtl.gameObject.SetActive(false);
        }
    }
}
