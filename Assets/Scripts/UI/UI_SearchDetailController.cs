using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UI_SearchDetailController : MonoBehaviour, IDialog
{
    [Header("Scrollview")]
    public GameObject CollectionView;

    [Header("Images")]
    public Image Image;
    public GameObject DtlImagePrefab;
    protected List<Image> dtlImages = new List<Image>();

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

    protected Searchable data;


    public virtual void SetData(Searchable searchable)
    {
        data = searchable;

        OwnedImage.SetActive(data.Owned);
        Image.sprite = data.GetDisplayImage();
        Clarifier.text = data.Clarifier();
        Name.text = data.SearchName();
        SetSeasonTags();
        if (data is BoxOwnable) SetBoxTags(((BoxOwnable)data).Boxs);
        else SetBoxTags(new List<BoxAssociationDtl>());
        SetDtlImages();
    }

    protected virtual void SetBoxTags(List<BoxAssociationDtl> boxes)
    {
        foreach (var team in BoxTags)
        {
            team.gameObject.SetActive(false);
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
    
    protected virtual void SetDtlImages()
    {
        HideAll();
        var index = 0;

        foreach (var images in data.GetDtlImages())
        {
            TryCreateDtlImages(index, images);
            index++;
        }
    }

    protected virtual void TryCreateDtlImages(int index, Sprite image)
    {
        if (index < dtlImages.Count)
        {
            dtlImages[index].gameObject.SetActive(true);
            dtlImages[index].sprite = image;
        }
        else
        {
            dtlImages.Add(Instantiate(DtlImagePrefab, CollectionView.transform).GetComponent<Image>());
            dtlImages[index].gameObject.SetActive(true);
            dtlImages[index].sprite = image;
        }
    }
    protected virtual void HideAll()
    {
        foreach (var collection in dtlImages)
        {
            collection.gameObject.SetActive(false);
        }
    }

    #region IDialog
    public virtual void Open()
    {
        this.gameObject.SetActive(true);
    }
    public virtual void Close()
    {
        this.gameObject.SetActive(false);
    }
    #endregion
}
