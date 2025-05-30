using System;
using System.Collections.Generic;
using System.Xml.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;
using static Character;

public class UI_CharacterDetailsController : MonoBehaviour, IDialog
{
    [Header("Background")]
    public Image Background;
    public Color HeroBackground;
    public Color VillainBackground;
    public Color AntiHeroBackground;

    [Header("Type Tag")]
    public UI_Tag TypeTag;

    [Header("Character Image")]
    public Image CharacterImage;

    [Header("Owned")]
    public GameObject OwnedImage;

    [Header("Character Name")]
    public TextMeshProUGUI Clarifier;
    public TextMeshProUGUI Name;

    [Header("Season Tag")]
    public UI_Tag SeasonTag;

    [Header("Box Tag")]
    public List<UI_Tag> BoxTags;
    public TextMeshProUGUI BoxOverFlow;

    [Header("Team Tag")]
    public List<UI_Tag> TeamTags;
    public TextMeshProUGUI TeamOverFlow;
    public TextMeshProUGUI ExclusiveLabel;

    [Header("Hero Win Lose")]
    public TMP_InputField HeroWins;
    public TMP_InputField HeroLosses;
    public UI_Rating HeroRating;

    [Header("Hero Symbols")]
    public GameObject HeroSymbles;
    public TextMeshProUGUI HeroMoveSymble;
    public TextMeshProUGUI HeroHeroicSymble;
    public TextMeshProUGUI HeroAttackSymble;
    public TextMeshProUGUI HeroWildSymble;
    public TextMeshProUGUI HeroSpecialCards;

    [Header("Villain Win Lose")]
    public TMP_InputField VillainWins;
    public TMP_InputField VillainLosses;
    public UI_Rating VillainRating;

    [Header("Villain Symbols")]
    public GameObject VillianSymbles;
    public TextMeshProUGUI VillainMoveSymble;
    public TextMeshProUGUI VillainHeroicSymble;
    public TextMeshProUGUI VillainAttackSymble;
    public TextMeshProUGUI VillainWildSymble;

    [Header("Character Dtl Image")]
    public Image CharacterDtlImage;

    protected Character data;

    public virtual void SetData(Character character)
    {
        data = character;

        //SetBackgroundColor(data);
        OwnedImage.SetActive(data.Owned);
        TypeTag.SetTagDisplay(ColorManager.GetColor(data.Type, out bool darkText), data.Type.ToFriendlyString(), darkText);
        CharacterImage.sprite = data.ChracterImage;
        Clarifier.text = data.CharacterClarifier;
        Name.text = data.CharacterName;
        SeasonTag.SetTagDisplay(ColorManager.GetColor(data.Season, out darkText), data.Season.ToFriendlyString(), darkText);
        SetBoxTags(data.Boxs);
        ExclusiveLabel.gameObject.SetActive(data.IsExclusive);
        SetTeamTags(data.Teams);
        SetHeroData(data);
        SetVillainData(data);
        SetSymbleDispaly(data);

        CharacterDtlImage.sprite = data.ChracterDtlImage;
    }

    protected virtual void SetBackgroundColor(Character data)
    {
        if (data.Type == CharacterType.Hero) Background.color = HeroBackground;
        else if (data.Type == CharacterType.Villain) Background.color = VillainBackground;
        else if (data.Type == CharacterType.AntiHero) Background.color = AntiHeroBackground;
    }
    protected virtual void SetBoxTags(List<CharacterBoxDtl> boxes)
    {
        foreach (var team in BoxTags)
        {
            team.gameObject.SetActive(false);
        }

        if (boxes == null || boxes.Count <= 0)
        {
            BoxOverFlow.text = "NONE";
            BoxOverFlow.gameObject.SetActive(true);
            return;
        }

        var lowCount = boxes.Count < BoxTags.Count ? boxes.Count : BoxTags.Count;

        for (int i = 0; i < lowCount; i++)
        {
            BoxTags[i].SetTagDisplay(ColorManager.GetColor(boxes[i].Box, out bool darkText), boxes[i].Box.ToFriendlyString(), darkText);
            BoxTags[i].gameObject.SetActive(true);
        }

        if (boxes.Count > BoxTags.Count)
        {
            BoxOverFlow.text = "+" + (boxes.Count - BoxTags.Count);
            BoxOverFlow.gameObject.SetActive(true);
        }
        else
        {
            BoxOverFlow.gameObject.SetActive(false);
        }
    }

    protected virtual void SetTeamTags(List<Teams> teams)
    {
        foreach(var team in TeamTags)
        {
            team.gameObject.SetActive(false);
        }

        if (teams == null || teams.Count <= 0)
        {
            TeamOverFlow.text = "NONE";
            TeamOverFlow.gameObject.SetActive(true);
            return;
        }

        var lowCount = teams.Count < TeamTags.Count ? teams.Count : TeamTags.Count;

        for (int i = 0; i < lowCount; i++)
        {
            TeamTags[i].SetTagDisplay(ColorManager.GetColor(teams[i], out bool darkText), teams[i].ToFriendlyString(), darkText);
            TeamTags[i].gameObject.SetActive(true);
        }

        if (teams.Count > TeamTags.Count)
        {
            TeamOverFlow.text = "+" + (teams.Count - TeamTags.Count);
            TeamOverFlow.gameObject.SetActive(true);
        }
        else
        {
            TeamOverFlow.gameObject.SetActive(false);
        }
    }

    protected virtual void SetHeroData(Character data)
    {
        if (data.Type != CharacterType.Hero && data.Type != CharacterType.AntiHero) return;

        HeroWins.text = data.HeroWins.ToString();
        HeroLosses.text = data.HeroLosses.ToString();
        HeroRating.SetRating(data.HeroRating);
    }
    protected virtual void SetVillainData(Character data)
    {
        if (data.Type != CharacterType.Villain && data.Type != CharacterType.AntiHero) return;

        VillainWins.text = data.VillainWins.ToString();
        VillainLosses.text = data.VillainLosses.ToString();
        VillainRating.SetRating(data.VillainRating);
    }

    protected virtual void SetSymbleDispaly(Character data)
    {
        if (data.Type == CharacterType.Hero || data.Type == CharacterType.AntiHero)
        {
            HeroSymbles.SetActive(true);
            HeroMoveSymble.text = data.HeroSymblesMove.ToString();
            HeroHeroicSymble.text = data.HeroSymblesHeroic.ToString();
            HeroAttackSymble.text = data.HeroSymblesAttack.ToString();
            HeroWildSymble.text = data.HeroSymblesWild.ToString();
            HeroSpecialCards.text = data.HeroSpecialCards.ToString();
        }
        else
        {
            HeroSymbles.SetActive(false);
        }

        if (data.Type == CharacterType.Villain || data.Type == CharacterType.AntiHero)
        {
            VillianSymbles.SetActive(true);
            VillainMoveSymble.text = data.VillainSymblesMove.ToString();
            VillainHeroicSymble.text = data.VillainSymblesHeroic.ToString();
            VillainAttackSymble.text = data.VillainSymblesAttack.ToString();
            VillainWildSymble.text = data.VillainSymblesWild.ToString();
        }
        else
        {
            VillianSymbles.SetActive(false);
        }
    }

    public virtual void OnValueChanged(TMP_InputField field)
    {
        if (!int.TryParse(field.text, out int value))
        {
            field.text = string.Empty;
            return;
        }

        var numberBoxValue = Mathf.Clamp(value, 0, 999);
        field.text = numberBoxValue.ToString();

        if (field == HeroWins)
        {
            data.SetHeroWins(numberBoxValue);   
        }
        else if (field == HeroLosses)
        {
            data.SetHeroLosses(numberBoxValue);
        }
        else if(field == VillainWins)
        {
            data.SetVillainWins(numberBoxValue);
        }
        else if (field == VillainLosses)
        {
            data.SetVillainLosses(numberBoxValue);
        }
    }


    #region IDialog
    public virtual void Open()
    {
        this.gameObject.SetActive(true);
    }
    public virtual void Close()
    {
        this.gameObject.SetActive(false);
    }
    #endregion

}
