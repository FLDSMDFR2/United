using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_SearchDetailController : MonoBehaviour, IDialog
{
    [Header("Scrollview")]
    public GameObject CollectionView;
    public ScrollRect ScrollRect;

    [Header("Type Tag")]
    public UI_Tag TypeTag;

    [Header("Images")]
    public Image Image;
    public GameObject DtlImagePrefab;
    public GameObject ImageView;
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

    [Header("Dtl Item")]
    public GameObject DtlItemPrefab;
    public GameObject DtlItemHeaderPrefab;
    public Color DtlItemHeaderColor;

    protected List<UI_DtlItem> displayedDtlItems = new List<UI_DtlItem>();
    protected List<GameObject> displayedDtlItemsHeaders = new List<GameObject>();

    protected Searchable data;

    protected float openTime = 0.1f;
    protected float closeTime = 0.05f;

    public virtual void SetData(Searchable searchable)
    {
        data = searchable;
        ApplyData();
    }

    public virtual void ResetData()
    {

    }

    public virtual void ApplyData()
    {
        TypeTag.SetTagDisplay(Color.gray, data.GetType().ToString(), false);
        OwnedImage.SetActive(data.Owned);
        Image.sprite = data.GetDisplayImage();
        Clarifier.text = data.Clarifier();
        Name.text = data.DisplayName();
        SetSeasonTags();
        if (data is BoxOwnable) SetBoxTags(((BoxOwnable)data).Boxs);
        else SetBoxTags(new List<BoxAssociationDtl>());
        SetDtlImages();
        SetDtlItems(data.DtlItems());
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
            var imageObj = Instantiate(DtlImagePrefab, ImageView.transform).GetComponent<Image>();
            var rectTrans = imageObj.GetComponent<RectTransform>();
            rectTrans.anchoredPosition = new Vector2(0.5f, 1f);
            rectTrans.anchorMin = new Vector2(0.5f, 1f);
            rectTrans.anchorMax = new Vector2(0.5f, 1f);
            rectTrans.pivot = new Vector2(0.5f, 1f);

            dtlImages.Add(imageObj);
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

    protected virtual void SetDtlItems(Dictionary<string, List<Searchable>> ownables)
    {
        HideDtlItems();
        if (ownables == null || ownables.Keys.Count <= 0) return;

        foreach(var key in ownables.Keys)
        {
            var header = Instantiate(DtlItemHeaderPrefab, CollectionView.transform);
            header.GetComponent<UI_Header>().SetData(DtlItemHeaderColor, Color.white, key);
            displayedDtlItemsHeaders.Add(header);

            foreach (var item in ownables[key])
            {
                var dtl = Instantiate(DtlItemPrefab, CollectionView.transform).GetComponent<UI_DtlItem>();
                dtl.SetData(item.GetDisplayImage(), item.DisplayNameWithClarifier());
                displayedDtlItems.Add(dtl);
            }
        }
    }

    protected virtual void HideDtlItems()
    {
        foreach (var header in displayedDtlItemsHeaders)
        {
            //header.gameObject.SetActive(false);
            Destroy(header.gameObject);
        }
        displayedDtlItemsHeaders.Clear();

        foreach (var item in displayedDtlItems)
        {
            //item.gameObject.SetActive(false);
            Destroy(item.gameObject);
        }
        displayedDtlItems.Clear();
    }

    #region IDialog
    public virtual void Open()
    {
        LeanTween.scale(this.gameObject, new Vector3(1f, 1f, 1f), openTime);
        StartCoroutine(OpenDelay());
        //this.gameObject.SetActive(true);
    }

    protected virtual IEnumerator OpenDelay()
    {
        yield return new WaitForSeconds(openTime);
        ScrollRect.verticalNormalizedPosition = 1f;
    }

    public virtual void Close()
    {
        ResetData();
        LeanTween.scale(this.gameObject, new Vector3(0f, 0f, 0f), closeTime);
        //this.gameObject.SetActive(false);
    }
    #endregion
}
