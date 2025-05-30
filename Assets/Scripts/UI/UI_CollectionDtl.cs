using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Collection;

public class UI_CollectionDtl : MonoBehaviour
{
    public Image BoxImage;
    public TextMeshProUGUI BoxName;
    public ToggleSwitch OwnedSlider;

    protected BoxDtls data;

    public virtual void SetData(BoxDtls box)
    {
        data = box;

        BoxImage.sprite = data.Image;
        BoxName.text = data.BoxTag.ToFriendlyString();
        OwnedSlider.ToggleByGroupManager(data.Owned);
    }

    public virtual void SliderToggled()
    {
        data.Owned = OwnedSlider.CurrentValue;
        UpdateBoxCharacters(data.Owned);
    }
    protected virtual void UpdateBoxCharacters(bool owned)
    {
        foreach (var character in DataLoader.GetAllCharactersByBox(data.BoxTag))
        {
            foreach(var box in character.Boxs)
            {
                if (box.Box == data.BoxTag && box.Default)
                {
                    character.SetOwned(owned);
                    break;
                }
            }
        }
    }

    public virtual void BoxSelected()
    {
        GameEventSystem.UI_OnBoxSelected(data);
    }
}
