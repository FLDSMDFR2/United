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
        data = ownable;
        this.box = box;
        forGameBuild = isForGameBuild;
        referanceIndex = -1;

        BoxImage.sprite = data.GetDisplayImage();
        BoxName.text = data.DisplayName();
        BackGround.color = BackgroundColor;

        var isOn = false;
        if (isForGameBuild)
        {
            isOn = ownable.IncludeInGameBuild;
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
    }

    public virtual void SetSliderValue(bool sliderValue)
    {
        if (data == null) return;

        OwnedSlider.ToggleByGroupManager(sliderValue);
    }

    public virtual void SliderToggleOn()
    {
        if (forGameBuild)
        {
            data.IncludeInGameBuild = true;
        }
        else
        {
            if (data != null && referanceIndex >= 0) data.Boxs[referanceIndex].Default = true;
            if (box.Owned) data.SetOwned(true);
        }
    }

    public virtual void SliderToggleOff()
    {
        if (forGameBuild)
        {
            data.IncludeInGameBuild = false;
        }
        else
        {
            if (data != null && referanceIndex >= 0) data.Boxs[referanceIndex].Default = false;
            if (box.Owned) data.SetOwned(false);
        }
    }
}

