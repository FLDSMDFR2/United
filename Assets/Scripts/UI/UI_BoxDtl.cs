using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_BoxDtl : MonoBehaviour
{
    public Image BoxImage;
    public Image BackGround;
    public TextMeshProUGUI BoxName;
    public ToggleSwitch OwnedSlider;

    protected BoxOwnable data;
    protected Box box;
    protected int referanceIndex;
    protected bool forGameBuild;

    public virtual void SetData(BoxOwnable ownable, Box box, bool isForGameBuild, Color BackgroundColor)
    {

        OwnedSlider.ToggleSwitch_On -= OwnedSlider_Toggled;
        OwnedSlider.ToggleSwitch_Off -= OwnedSlider_Toggled;

        data = ownable;
        this.box = box;
        forGameBuild = isForGameBuild;
        referanceIndex = -1;

        BoxImage.sprite = data.GetDisplayImage();
        BoxName.text = data.GetDisplayNameWithClarifier();
        BackGround.color = BackgroundColor;

        var isOn = false;
        if (isForGameBuild)
        {
            isOn = ownable.GetIncludeInBuild(box.BoxTag);
        }
        else
        {
            for (int i = 0; i < data.Boxs.Count; i++)
            {
                if (data.Boxs[i].Box == this.box.BoxTag)
                {
                    isOn = data.Boxs[i].Default;
                    referanceIndex = i;
                    break;
                }
            }
        }

        OwnedSlider.ToggleByGroupManager(isOn, false);


        OwnedSlider.ToggleSwitch_On += OwnedSlider_Toggled;
        OwnedSlider.ToggleSwitch_Off += OwnedSlider_Toggled;
    }

    public virtual void SetSliderValue(bool sliderValue)
    {
        if (data == null) return;

        OwnedSlider.ToggleByGroupManager(sliderValue);
    }

    protected virtual void OwnedSlider_Toggled()
    {
        if (forGameBuild)
        {
            data.SetIncludeInBuild(box.BoxTag, OwnedSlider.CurrentValue);
        }
        else
        {
            if (data != null && referanceIndex >= 0) data.Boxs[referanceIndex].Default = OwnedSlider.CurrentValue;
            if (box.Owned) data.SetOwned(box.BoxTag, OwnedSlider.CurrentValue);
        }
    }
}

