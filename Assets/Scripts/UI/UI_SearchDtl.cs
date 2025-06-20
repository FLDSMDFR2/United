using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_SearchDtl : MonoBehaviour
{
    [Header("Background")]
    public Image Background;
    public Color DefaultBackground;

    [Header("Image")]
    public Image Image;

    [Header("Owned")]
    public GameObject OwnedImage;

    [Header("Character Name")]
    public TextMeshProUGUI Clarifier;
    public TextMeshProUGUI Name;

    [Header("Season Tag")]
    public List<UI_Tag> SeasonTags;
    public TextMeshProUGUI SeasonOverFlow;

    [Header("Box Tag")]
    public List<UI_Tag> BoxTags;
    public TextMeshProUGUI BoxOverFlow;

    public TextMeshProUGUI ExclusiveLabel;

    protected Searchable data;

    public virtual void SetData(Searchable searchable)
    {
        data = searchable;
        searchable.OnOwnableUpdate += ApplyData;
        ApplyData();
    }

    protected virtual void ApplyData()
    {
        SetBackgroundColor();
        OwnedImage.SetActive(data.Owned);
        Image.sprite = data.GetDisplayImage();
        Clarifier.text = data.Clarifier();
        Name.text = data.DisplayName();
        if (data is BoxOwnable) SetBoxTags(((BoxOwnable)data).Boxs);
        else SetBoxTags(new List<BoxAssociationDtl>());
        SetSeasonTags();
        ExclusiveLabel.gameObject.SetActive(false);
    }

    protected virtual void SetBackgroundColor()
    {
        Background.color = DefaultBackground;
    }

    protected virtual void SetBoxTags(List<BoxAssociationDtl> boxes)
    {
        foreach (var box in BoxTags)
        {
            box.gameObject.SetActive(false);
        }

        if (boxes == null || boxes.Count <= 0)
        {
            BoxOverFlow.text = "NONE";
            BoxOverFlow.gameObject.SetActive(true);
            return;
        }

        var lowCount = boxes.Count < BoxTags.Count ? boxes.Count : BoxTags.Count;

        for (int i = 0; i < lowCount; i++)
        {
            BoxTags[i].SetTagDisplay(ColorManager.GetColor(boxes[i].Box, out bool darkText), boxes[i].Box.ToFriendlyString(), darkText);
            BoxTags[i].gameObject.SetActive(true);
        }

        if (boxes.Count > BoxTags.Count)
        {
            BoxOverFlow.text = "+" + (boxes.Count - BoxTags.Count);
            BoxOverFlow.gameObject.SetActive(true);
        }
        else
        {
            BoxOverFlow.gameObject.SetActive(false);
        }
    }

    protected virtual void SetSeasonTags()
    {
        foreach (var season in SeasonTags)
        {
            season.gameObject.SetActive(false);
        }

        if (data.Season == null || data.Season.Count <= 0)
        {
            SeasonOverFlow.text = "NONE";
            SeasonOverFlow.gameObject.SetActive(true);
            return;
        }

        var lowCount = data.Season.Count < SeasonTags.Count ? data.Season.Count : SeasonTags.Count;

        for (int i = 0; i < lowCount; i++)
        {
            SeasonTags[i].SetTagDisplay(ColorManager.GetColor(data.Season[i], out bool darkText), data.Season[i].ToFriendlyString(), darkText);
            SeasonTags[i].gameObject.SetActive(true);
        }

        if (data.Season.Count > BoxTags.Count)
        {
            SeasonOverFlow.text = "+" + (data.Season.Count - BoxTags.Count);
            SeasonOverFlow.gameObject.SetActive(true);
        }
        else
        {
            SeasonOverFlow.gameObject.SetActive(false);
        }
    }

    public virtual void Selected()
    {
        GameEventSystem.UI_OnSearchSelected(data);
    }
}
