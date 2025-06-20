using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_FilterOption : MonoBehaviour
{
    public Image Image;
    public Image BackGround;
    public TextMeshProUGUI Name;
    public ToggleSwitch OwnedSlider;

    protected Filter data;

    public virtual void SetData(Filter filter)
    {
        data = filter;

        Name.text = data.Name;
        Image.gameObject.SetActive(false);
        OwnedSlider.gameObject.SetActive(true);
        OwnedSlider.ToggleByGroupManager(data.FilterValue, false);
    }

    public virtual void SliderToggled()
    {
        data.FilterValue = OwnedSlider.CurrentValue;
    }
}
