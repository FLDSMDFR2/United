using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Header : MonoBehaviour
{
    public Image Background;
    public TextMeshProUGUI Text;

    public virtual void SetData(Color background, Color textColor, string text)
    {
        Background.color = background;
        Text.text = text;
        Text.color = textColor;
    }
}
