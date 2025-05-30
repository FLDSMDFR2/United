using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Rating : MonoBehaviour
{
    public Color FillColor;
    public List<Slider> FillObjects;
    public List<Image> Fills;

    protected virtual void Awake()
    {
        SetFillColor(FillColor);
    }

    protected virtual void SetFillColor(Color fillColor)
    {
        foreach (var obj in Fills)
        {
            obj.color = FillColor;
        }
    }

    public virtual void SetRating(float rating)
    {
        if (FillObjects == null || FillObjects.Count <= 0 || rating > FillObjects.Count) return;

        foreach (var obj in FillObjects)
        {
            obj.value = 0;
        }

        int full = (int)(rating / 1);
        var percentFull = rating % 1;

        int i = 0;
        for (i = 0; i < full; i++)
        {
            FillObjects[i].value = 1;
        }

        if (percentFull > 0) FillObjects[i].value = percentFull;
    }
}
