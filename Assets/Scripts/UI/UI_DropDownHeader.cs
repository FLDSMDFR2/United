using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_DropDownHeader : MonoBehaviour
{
    public Image DropDownImage;
    public Image Background;
    public TextMeshProUGUI DropDownName;

    protected bool isOpen;
    protected List<GameObject> controlledGroup;

    public virtual void SetData(string headerName, List<GameObject> grouped, Color? backgroundColor = null, bool showCount = false, float fontSize = 24)
    {
        if (showCount) DropDownName.text = headerName + " - " + grouped.Count.ToString();
        else DropDownName.text = headerName;

        DropDownName.fontSize = fontSize;

        controlledGroup = grouped;
        Background.color = backgroundColor ?? Color.gray;

        isOpen = true;
        OnSelected();
    }

    protected virtual void UpdateDisplay()
    {
        if (controlledGroup == null || controlledGroup.Count == 0) return;

        foreach (var control in controlledGroup)
        {
            control.SetActive(isOpen);
        }
    }

    public virtual void OnSelected()
    {
        isOpen = !isOpen;

        if (isOpen) LeanTween.rotateZ(DropDownImage.gameObject, 180f, 0.1f);
        else LeanTween.rotateZ(DropDownImage.gameObject, 270f, 0.1f);

        UpdateDisplay();
    }
}
