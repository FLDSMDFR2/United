using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Tag : MonoBehaviour
{
    public Image TagBackground;
    public TextMeshProUGUI TagText;
    public bool IsVertical;
    protected string isVerticalText = "<rotate=90>";

    public virtual void SetTagDisplay(Color tagColor, string tagText, bool isDarkText)
    {
        TagBackground.color = tagColor;
        if (IsVertical) tagText = isVerticalText + tagText;
        TagText.text = tagText;
        if (isDarkText) TagText.color = Color.black;
    }
}
