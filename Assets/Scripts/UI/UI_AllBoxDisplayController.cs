using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UI_AllBoxDisplayController : Loadable, IDialog
{
    public GameObject GroupHeaderPrefab;
    public GameObject BoxUIPrefab;
    public GameObject CollectionView;
    public ScrollRect SearchScrollRect;
    public bool IsForGameBuild;

    protected Vector3 openPos;
    protected Vector3 closePos;
    protected float openTime = 0.2f;
    protected float closeTime = 0.2f;

    protected Dictionary<Seasons, DropDownGroup> dropDownGroup = new Dictionary<Seasons, DropDownGroup>();

    public override void LoadableStep2()
    {
        openPos = this.transform.position;
        closePos = this.transform.position - new Vector3(0, UIScreenSize.ScreenHeight(), 0);

        if (!IsForGameBuild)
        {
            SetData(GameSystems.All);
        }
    }

    public virtual void SetData(GameSystems gameSystems)
    {
        if (IsForGameBuild)
        {
            ClearGameObjectChildren(CollectionView);
            dropDownGroup.Clear();
        }

        var boxes = DataLoader.GetBoxsBySystem(gameSystems);
        var display = boxes.OrderBy(b => b.GetDisplayNameWithClarifier());

        var seasonDic = new Dictionary<Seasons, List<Box>>();
        var sortList = new List<Seasons>();

        foreach (var box in display)
        {
            if (box.Season.Count < 0) continue;
            if (!seasonDic.ContainsKey(box.Season[0]))
            {
                seasonDic[box.Season[0]] = new List<Box>();
                sortList.Add(box.Season[0]);
            }
            seasonDic[box.Season[0]].Add(box);
        }

        var sortKeyList = sortList.OrderBy(b => b.ToLongFriendlyString());
        foreach (var key in sortKeyList)
        {
            if (!dropDownGroup.ContainsKey(key))
            {
                dropDownGroup[key] = new DropDownGroup();
                dropDownGroup[key].Header = Instantiate(GroupHeaderPrefab, CollectionView.transform).GetComponent<UI_DropDownHeader>();
            }

            foreach (var box in seasonDic[key])
            {
                CreateNewDtl(key, box);
            }

            dropDownGroup[key].Header.gameObject.SetActive(true);
            dropDownGroup[key].Header.SetData(key.ToLongFriendlyString(), dropDownGroup[key].GetItemGameObjects(), ColorManager.GetColor(key, out bool darkText), true, 65);
        }
    }

    protected virtual void CreateNewDtl(Seasons Key, Box box)
    {
        if (dropDownGroup[Key].Items != null) dropDownGroup[Key].Items.Add(Instantiate(BoxUIPrefab, CollectionView.transform).GetComponent<UI_CollectionDtl>());
        else dropDownGroup[Key].Items = new List<UI_CollectionDtl>() { Instantiate(BoxUIPrefab, CollectionView.transform).GetComponent<UI_CollectionDtl>() };

        dropDownGroup[Key].Items[dropDownGroup[Key].Items.Count - 1].gameObject.SetActive(true);
        dropDownGroup[Key].Items[dropDownGroup[Key].Items.Count - 1].SetData(box, IsForGameBuild);
    }

    public virtual void SelectAll()
    {
        foreach(var key in dropDownGroup.Keys)
        {
            foreach(var collection in dropDownGroup[key].Items)
            {
                collection.SetSliderValue(true);
            }
        }
    }

    public virtual void UnSelectAll()
    {
        foreach (var key in dropDownGroup.Keys)
        {
            foreach (var collection in dropDownGroup[key].Items)
            {
                collection.SetSliderValue(false);
            }
        }
    }

    protected virtual void ClearGameObjectChildren(GameObject gameObject)
    {
        foreach (Transform child in gameObject.transform)
        {
            Destroy(child.gameObject);
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
        SearchScrollRect.verticalNormalizedPosition = 1f;
    }

    public virtual void Close()
    {
        if (IsForGameBuild) GameEventSystem.UI_OnCloseGameIncludePopup();
        LeanTween.move(this.gameObject, closePos, closeTime).setOnComplete(CloseComplete);
    }
    public virtual void CloseComplete()
    {
        this.gameObject.SetActive(false);
    }
    #endregion
}
