using UnityEngine;

public class UI_CollectionController : MonoBehaviour
{
    public Collection collection;
    public GameObject BoxUIPrefab;
    public GameObject CollectionView;

    protected virtual void Start()
    {
        if (BoxUIPrefab == null || collection == null) return;

        foreach (var box in collection.BoxList)
        {
            var prefab = Instantiate(BoxUIPrefab, CollectionView.transform);
            var uiScript = prefab.GetComponent<UI_CollectionDtl>();
            if (uiScript != null) uiScript.SetData(box);
        }
    }
}
