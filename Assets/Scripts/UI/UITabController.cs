using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UITabController : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IInitializePotentialDragHandler
{
    [Serializable]
    public class TabControllerData
    {
        public Graphic SelectedGraphic;
        public Button TabButton;
        public GameObject ContentPanel;
        public Vector3 TabOffset;
    }

    [Header("Default")]
    public int DefaultIndex;
    public Color NormalColor;
    public Color SelectedColor;
    public List<TabControllerData> Tabs;
    protected int currentIndex;

    [Header("Swip")]
    public bool IsSwip;
    public float SwipThreshold = 0.3f;
    public float TransitionSecondsPerTab;
    public Transform TabPanelParent;
    protected Vector3 tabpanelLocation;

    protected bool inTransition = false;

    protected virtual void Awake() 
    {
        if (Tabs != null && Tabs.Count > 0)
        {
            var count = 0;
            foreach (var tab in Tabs) 
            {
                if (IsSwip) tab.ContentPanel.transform.position += new Vector3(UIScreenSize.ScreenWidth() * count, 0f, 0f);
                tab.ContentPanel.SetActive(true);
                count++;
            }

            currentIndex = DefaultIndex;
            OnTabSelected(Tabs[currentIndex].TabButton);
        }

        tabpanelLocation = TabPanelParent.position;
    }

    public virtual void OnTabSelected(Button tab)
    {
        if (tab == null || Tabs == null || Tabs.Count <= 0 || inTransition) return;

        var data = Tabs.Find(x => x.TabButton == tab);

        if (data == null) return;

        foreach(var t in Tabs)
        {
            t.SelectedGraphic.color = NormalColor;
            if (!IsSwip) t.ContentPanel.SetActive(false);
        }

        var nextIndex = Tabs.IndexOf(data);

        if (IsSwip)
        {
            data.SelectedGraphic.color = SelectedColor;
            StartCoroutine(TransitionToNewTab(nextIndex));
        }
        else
        {
            data.SelectedGraphic.color = SelectedColor;
            data.ContentPanel.SetActive(true);
            currentIndex = nextIndex;
        }
    }

    protected virtual IEnumerator TransitionToNewTab(int endIndex)
    {
        inTransition = true;
        if (IsIndexOutOfBounds(endIndex)) endIndex = currentIndex;

        var time = 0f;
        var tabDif = currentIndex - endIndex;
        var seconds = Mathf.Clamp(Mathf.Abs(tabDif), 1, 99) * TransitionSecondsPerTab;
        var startPos = TabPanelParent.transform.position;
        var endPos = TabPanelParent.transform.position += new Vector3(UIScreenSize.ScreenWidth() * tabDif, 0, 0);

        while (time < 1.0f)
        {
            time += Time.deltaTime / seconds;
            TabPanelParent.transform.position = Vector3.Lerp(startPos, endPos, Mathf.SmoothStep(0f,1f, time));
            yield return null;
        }

        tabpanelLocation = TabPanelParent.transform.position;
        currentIndex = endIndex;
        inTransition = false;
    }

    protected virtual bool IsIndexOutOfBounds(int index)
    {
        return index < 0 || index >= Tabs.Count;
    }

    #region Drag Handlers
    public virtual void OnInitializePotentialDrag(PointerEventData eventData)
    {
        //eventData.useDragThreshold = false;
    }
    public virtual void OnBeginDrag(PointerEventData eventData)
    {

    }

    public virtual void OnDrag(PointerEventData data)
    {
        //var difference = data.pressPosition.x - data.position.x;
        //TabPanelParent.position = tabpanelLocation - new Vector3 (difference, 0f,0f);
    }

    public virtual void OnEndDrag(PointerEventData data)
    {
        //var percentage = (data.pressPosition.x - data.position.x) / ScreenWidth();

        //if (MathF.Abs(percentage) >= SwipThreshold)
        //{
        //    //var newLocation = tabpanelLocation;
        //    //if (percentage > 0) newLocation += new Vector3(-ScreenWidth(), 0, 0);
        //    //else if (percentage <= 0) newLocation += new Vector3(ScreenWidth(), 0, 0);
        //    //TabPanelParent.transform.position = newLocation;
        //    //tabpanelLocation = newLocation;

        //    var newIndex = 0;
        //    if (percentage > 0) newIndex = 1;
        //    else if (percentage <= 0) newIndex = -1;

        //    StartCoroutine(TransitionToNewTab(currentIndex + newIndex));  
        //}
        //else
        //{
        //    StartCoroutine(TransitionToNewTab(currentIndex));
        //    //TabPanelParent.transform.position = tabpanelLocation;
        //}
    }

    #endregion
}
