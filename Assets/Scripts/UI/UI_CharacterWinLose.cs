using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_CharacterWinLose : MonoBehaviour, IDialog
{
    public Image Background;
    public Sprite BackgroundHero;
    public Sprite BackgroundVillain;
    public Sprite BackgroundCompanion;

    public TMP_InputField Wins;
    public TMP_InputField Losses;
    public TMP_InputField Rating;

    protected Character data;
    protected CharacterType type;

    protected float openTime = 0.1f;
    protected float closeTime = 0.05f;

    public virtual void SetData(Character character, CharacterType type)
    {
        data = character;
        this.type = type;

        if (this.type == CharacterType.Hero)
        {
            Background.sprite = BackgroundHero;
            Wins.text = data.HeroWins.ToString();
            Losses.text = data.HeroLosses.ToString();
            Rating.text = data.HeroRating.ToString();
        }
        else if (this.type == CharacterType.Villain)
        {
            Background.sprite = BackgroundVillain;
            Wins.text = data.VillainWins.ToString();
            Losses.text = data.VillainLosses.ToString();
            Rating.text = data.VillainRating.ToString();
        }
        else if (this.type == CharacterType.Companion)
        {
            Background.sprite = BackgroundCompanion;
            Wins.text = data.CompanionWins.ToString();
            Losses.text = data.CompanionLosses.ToString();
            Rating.text = data.CompanionRating.ToString();
        }
    }

    public virtual void ConfirmPress()
    {
        if (type == CharacterType.Hero)
        {
            if (!int.TryParse(Wins.text, out int value1))
            {
                Wins.text = "0";
            }

            var numberBoxValue1 = Mathf.Clamp(value1, 0, 9999);
            Wins.text = numberBoxValue1.ToString();
            data.SetHeroWins(numberBoxValue1);

            if (!int.TryParse(Losses.text, out int value2))
            {
                Losses.text = "0";
            }

            var numberBoxValue2 = Mathf.Clamp(value2, 0, 9999);
            Losses.text = numberBoxValue2.ToString();
            data.SetHeroLosses(numberBoxValue2);

            if (!float.TryParse(Rating.text, out float value3))
            {
                Rating.text = "0.0";
                return;
            }
            var numberBoxValue3 = Mathf.Clamp(value3, 0, 5);
            Rating.text = numberBoxValue3.ToString();
            data.SetHeroRating(numberBoxValue3);
        }
        else if (type == CharacterType.Villain)
        {
            if (!int.TryParse(Wins.text, out int value1))
            {
                Wins.text = "0";
            }

            var numberBoxValue1 = Mathf.Clamp(value1, 0, 9999);
            Wins.text = numberBoxValue1.ToString();
            data.SetVillainWins(numberBoxValue1);

            if (!int.TryParse(Losses.text, out int value2))
            {
                Losses.text = "0";
            }

            var numberBoxValue2 = Mathf.Clamp(value2, 0, 9999);
            Losses.text = numberBoxValue2.ToString();
            data.SetVillainLosses(numberBoxValue2);

            if (!float.TryParse(Rating.text, out float value3))
            {
                Rating.text = "0.0";
                return;
            }
            var numberBoxValue3 = Mathf.Clamp(value3, 0, 5);
            Rating.text = numberBoxValue3.ToString();
            data.SetVillainRating(numberBoxValue3);
        }
        else if (type == CharacterType.Companion)
        {
            if (!int.TryParse(Wins.text, out int value1))
            {
                Wins.text = "0";
            }

            var numberBoxValue1 = Mathf.Clamp(value1, 0, 9999);
            Wins.text = numberBoxValue1.ToString();
            data.SetCompanionWins(numberBoxValue1);

            if (!int.TryParse(Losses.text, out int value2))
            {
                Losses.text = "0";
            }

            var numberBoxValue2 = Mathf.Clamp(value2, 0, 9999);
            Losses.text = numberBoxValue2.ToString();
            data.SetCompanionLosses(numberBoxValue2);

            if (!float.TryParse(Rating.text, out float value3))
            {
                Rating.text = "0.0";
                return;
            }
            var numberBoxValue3 = Mathf.Clamp(value3, 0, 5);
            Rating.text = numberBoxValue3.ToString();
            data.SetCompanionRating(numberBoxValue3);
        }
    }

    #region IDialog
    public virtual void Open()
    {
        this.gameObject.SetActive(true);
        LeanTween.scale(this.gameObject, new Vector3(1f, 1f, 1f), openTime);
    }

    public virtual void Close()
    {
        LeanTween.scale(this.gameObject, new Vector3(0f, 0f, 0f), closeTime).setOnComplete(CloseComplete);
    }
    public virtual void CloseComplete()
    {
        this.gameObject.SetActive(false);
    }
    #endregion
}
