using UnityEngine;

public class UIScreenSize : MonoBehaviour
{
    public RectTransform UITransform;

    protected static RectTransform myTransform;

    protected virtual void Awake()
    {
        myTransform = UITransform;
    }

    public static float ScreenWidth()
    {
        //return myTransform.sizeDelta.x * 2;
        return myTransform.position.x * 2;
    }
    public static float ScreenHeight()
    {
        //return myTransform.sizeDelta.y * 2;
        return myTransform.position.y * 2;
    }
}
