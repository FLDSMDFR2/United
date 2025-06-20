using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Header : MonoBehaviour
{
    public Image Background;
    public TextMeshProUGUI Text;

    public virtual void SetData(Box box)
    {
        Background.color = ColorManager.GetColor(box.BoxTag, out bool darkText);
        Text.text = box.DisplayName();
        if (darkText) Text.color = Color.black;
        else Text.color = Color.white;
    }
    public virtual void SetData(Color background, Color textColor, string text)
    {
        Background.color = background;
        Text.text = text;
        Text.color = textColor;
    }
}
