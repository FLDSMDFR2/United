using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UI_AllBoxDisplayController : MonoBehaviour, IDialog
{
    public GameObject BoxUIPrefab;
    public GameObject CollectionView;
    public bool IsForGameBuild;
    public int PreBuildTotal;

    protected List<UI_CollectionDtl> collectionDtls = new List<UI_CollectionDtl>();

    protected virtual void Start()
    {
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
        var display = boxes.OrderBy(b => b.DisplayName());
        var index = 0;

        foreach (var box in display)
        {
            TryCreateCollection(index).SetData(box, IsForGameBuild);
            index++;
        }
    }

    protected virtual UI_CollectionDtl TryCreateCollection(int index)
    {
        if (index <= collectionDtls.Count)
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
        this.gameObject.SetActive(true);
    }
    public virtual void Close()
    {
        this.gameObject.SetActive(false);
    }
    #endregion
}
