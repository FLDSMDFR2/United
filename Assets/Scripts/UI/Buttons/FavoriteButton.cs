using UnityEngine;
using UnityEngine.UI;

public class FavoriteButton : MonoBehaviour
{
    public Image FavoriteIcon;
    public Color IsFavoriteColor;
    protected Color notFavoriteColor;

    protected virtual void Awake()
    {
        if (FavoriteIcon == null) return;

        notFavoriteColor = FavoriteIcon.color;
    }

    public virtual void SetIsFavorite(bool isFavorite)
    {
        if (isFavorite) FavoriteIcon.color = IsFavoriteColor;
        else FavoriteIcon.color = notFavoriteColor;
    }
}
