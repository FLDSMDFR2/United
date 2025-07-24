using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_DtlItem : MonoBehaviour
{
    public Image Image;
    public TextMeshProUGUI Name;

    public virtual void SetData(Sprite image, string name)
    {
        Image.sprite = image;
        Name.text = name;
    }
}
