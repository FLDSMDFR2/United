using System.Collections.Generic;
using UnityEngine;
using static Collection;

public class UI_BoxDetails : MonoBehaviour, IDialog
{
    public GameObject BoxDtlPrefab;
    public GameObject BoxDtlView;

    protected BoxDtls Box;

    protected Vector3 openPos;
    protected Vector3 closePos;

    protected List<UI_BoxDtl> dtlsList = new List<UI_BoxDtl>();

    protected virtual void Awake()
    {
        openPos = this.transform.position;
        closePos = this.transform.position - new Vector3(0, UIScreenSize.ScreenHeight(), 0);
        this.transform.position = closePos;

        PreBuildDisplay();
    }

    protected virtual void PreBuildDisplay()
    {
        for(int i = 0; i < 40; i ++)
        {
            CreateNewDtl();
        }

        HideAllDtls();
    }

    public virtual void SetData(BoxDtls box)
    {
        this.Box = box;

        if (BoxDtlPrefab == null || BoxDtlView == null) return;

        HideAllDtls();

        var index = 0;
        foreach (var character in DataLoader.GetAllCharactersByBox(this.Box.BoxTag))
        {
            UpdateAndShowDtl(character, index++);
        }
    }

    protected virtual void CreateNewDtl()
    {
        dtlsList.Add(Instantiate(BoxDtlPrefab, BoxDtlView.transform).GetComponent<UI_BoxDtl>());
    }

    protected virtual void UpdateAndShowDtl(Character character, int index)
    {
        if (index >= dtlsList.Count) CreateNewDtl();

        dtlsList[index].gameObject.SetActive(true);
        dtlsList[index].SetData(character, this.Box);
    }

    protected virtual void HideAllDtls()
    {
        foreach (var dtl in dtlsList)
        {
            dtl.gameObject.SetActive(false);
        }
    }

    #region IDialog
    public virtual void Open()
    {
        LeanTween.move(this.gameObject, openPos, 0.2f);
    }
    public virtual void Close()
    {
        LeanTween.move(this.gameObject, closePos, 0.2f);
    }
    #endregion
}
