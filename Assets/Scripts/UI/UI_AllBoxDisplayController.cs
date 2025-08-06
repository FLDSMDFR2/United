using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UI_AllBoxDisplayController : Loadable, IDialog
{
    public GameObject BoxUIPrefab;
    public GameObject CollectionView;
    public ScrollRect SearchScrollRect;
    public bool IsForGameBuild;
    public int PreBuildTotal;

    protected Vector3 openPos;
    protected Vector3 closePos;
    protected float openTime = 0.2f;
    protected float closeTime = 0.2f;

    protected List<UI_CollectionDtl> collectionDtls = new List<UI_CollectionDtl>();

    public override void LoadableStep2()
    {
        openPos = this.transform.position;
        closePos = this.transform.position - new Vector3(0, UIScreenSize.ScreenHeight(), 0);

        for (int i = 0; i < PreBuildTotal; i++)
        {
            collectionDtls.Add(Instantiate(BoxUIPrefab, CollectionView.transform).GetComponent<UI_CollectionDtl>());
        }

        if (!IsForGameBuild)
        {
            SetData(GameSystems.All);
        }
    }

    public virtual void SetData(GameSystems gameSystems)
    {
        HideAll();
        var boxes = DataLoader.GetBoxsBySystem(gameSystems);
        var display = boxes.OrderBy(b => b.DisplayNameWithClarifier());
        var index = 0;

        foreach (var box in display)
        {
            TryCreateCollection(index).SetData(box, IsForGameBuild);
            index++;
        }
    }

    protected virtual UI_CollectionDtl TryCreateCollection(int index)
    {
        if (index < collectionDtls.Count)
        {
            collectionDtls[index].gameObject.SetActive(true);
            return collectionDtls[index];
        }
        else
        {
            collectionDtls.Add(Instantiate(BoxUIPrefab, CollectionView.transform).GetComponent<UI_CollectionDtl>());
            collectionDtls[index].gameObject.SetActive(true);
            return collectionDtls[index];
        }
    }

    protected virtual void HideAll()
    {
        foreach (var collection in collectionDtls)
        {
            collection.gameObject.SetActive(false);
        }
    }

    public virtual void SelectAll()
    {
        foreach(var collection in collectionDtls)
        {
            collection.SetSliderValue(true);
        }
    }

    public virtual void UnSelectAll()
    {
        foreach (var collection in collectionDtls)
        {
            collection.SetSliderValue(false);
        }
    }

    #region IDialog
    public virtual void Open()
    {
        LeanTween.move(this.gameObject, openPos, openTime);
        StartCoroutine(OpenDelay());
        //this.gameObject.SetActive(true);
    }

    protected virtual IEnumerator OpenDelay()
    {
        yield return new WaitForSeconds(openTime);
        SearchScrollRect.verticalNormalizedPosition = 1f;
    }

    public virtual void Close()
    {
        if (IsForGameBuild) GameEventSystem.UI_OnCloseGameIncludePopup();
        LeanTween.move(this.gameObject, closePos, closeTime);
        //this.gameObject.SetActive(false);
    }
    #endregion
}
