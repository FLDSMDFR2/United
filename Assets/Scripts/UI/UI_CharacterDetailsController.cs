using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_CharacterDetailsController : UI_SearchDetailController
{
    [Header("Type Tag")]
    public UI_Tag TypeTag;

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

    protected Character getData => (Character)data;

    public override void SetData(Searchable searchable)
    {
        base.SetData(searchable);

        TypeTag.SetTagDisplay(ColorManager.GetColor(getData.Type, out bool darkText), getData.Type.ToFriendlyString(), darkText);
        Clarifier.text = getData.Clarifier();
        ExclusiveLabel.gameObject.SetActive(getData.IsExclusive);
        SetTeamTags(getData.Teams);
        SetHeroData(getData);
        SetVillainData(getData);
        SetSymbleDispaly(getData);

        DtlImage.sprite = getData.GetDtlImage();
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

        if (data.IsSuperVillainModeAllowed && (data.Type == CharacterType.Villain || data.Type == CharacterType.AntiHero))
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
            getData.SetHeroWins(numberBoxValue);   
        }
        else if (field == HeroLosses)
        {
            getData.SetHeroLosses(numberBoxValue);
        }
        else if(field == VillainWins)
        {
            getData.SetVillainWins(numberBoxValue);
        }
        else if (field == VillainLosses)
        {
            getData.SetVillainLosses(numberBoxValue);
        }
    }
}
