using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Collection;

public class UI_BoxDtl : MonoBehaviour
{
    public Image BoxImage;
    public TextMeshProUGUI BoxName;
    public ToggleSwitch OwnedSlider;

    protected Character data;
    protected BoxDtls box;
    protected int referanceIndex;

    public virtual void SetData(Character character, BoxDtls box)
    {
        data = character;
        this.box = box;
        referanceIndex = -1;

        BoxImage.sprite = data.ChracterImage;
        BoxName.text = data.CharacterClarifier + " " + data.CharacterName;

        var isOn = false;
        for (int i = 0; i < data.Boxs.Count; i++)
        {
            if (data.Boxs[i].Box == this.box.BoxTag)
            {
                isOn = data.Boxs[i].Default;
                referanceIndex = i;
                break;
            }
        }


        OwnedSlider.ToggleByGroupManager(isOn);
    }

    public virtual void SliderToggleOn()
    {
        if (data != null && referanceIndex >= 0) data.Boxs[referanceIndex].Default = true;
        if (box.Owned) data.SetOwned(true);
    }
    public virtual void SliderToggleOff()
    {
        if (data != null && referanceIndex >= 0) data.Boxs[referanceIndex].Default = false;
        if (box.Owned) data.SetOwned(false);
    }
}

