using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_CollectionDtl : MonoBehaviour
{
    public Image BoxImage;
    public TextMeshProUGUI BoxName;
    public ToggleSwitch OwnedSlider;

    protected Box data;
    protected bool forGameBuild;

    public virtual void SetData(Box box, bool isForGameBuild)
    {
        data = box;
        forGameBuild = isForGameBuild;

        BoxImage.sprite = data.GetDisplayImage();
        BoxName.text = data.BoxTag.ToFriendlyString();

        if (forGameBuild) OwnedSlider.ToggleByGroupManager(data.GetIncludeInBuild(data.BoxTag), false);
        else OwnedSlider.ToggleByGroupManager(data.GetOwned(data.BoxTag), false);
    }

    public virtual void SetSliderValue(bool sliderValue)
    {
        if (data == null) return;

        OwnedSlider.ToggleByGroupManager(sliderValue);
    }

    public virtual void SliderToggled()
    {
        if (forGameBuild)
        {
            data.SetIncludeInBuild(data.BoxTag, OwnedSlider.CurrentValue);
            UpdateBoxContent(data.GetIncludeInBuild(data.BoxTag));
        }
        else
        {
            data.SetOwned(data.BoxTag, OwnedSlider.CurrentValue);
            UpdateBoxContent(data.GetOwned(data.BoxTag));
        }
    }

    protected virtual void UpdateBoxContent(bool owned)
    {
        foreach (var item in data.GetAllBoxItems())
        {
            if (forGameBuild)
            {
                UpdateContentDtls(item, owned);
            }
            else
            {
                foreach (var box in item.Boxs)
                {
                    if (box.Box == data.BoxTag && box.Default)
                    {
                        UpdateContentDtls(item, owned);
                        break;
                    }
                }
            }
        }
    }

    protected virtual void UpdateContentDtls(BoxOwnable boxOwnable, bool owned)
    {
        if (forGameBuild) boxOwnable.SetIncludeInBuild(data.BoxTag, owned);
        else boxOwnable.SetOwned(data.BoxTag, owned);
    }

    public virtual void BoxSelected()
    {
        GameEventSystem.UI_OnBoxSelected(data, forGameBuild);
    }
}
