using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_CharacterDetailsController : UI_SearchDetailController
{
    [Header("Character Details")]

    [Header("Character Sex")]
    public UI_Tag SexTag;

    [Header("Team Tag")]
    public List<UI_Tag> TeamTags;
    public TextMeshProUGUI TeamOverFlow;

    [Header("Hero Win Lose")]
    public TextMeshProUGUI HeroWins;
    public TextMeshProUGUI HeroLosses;
    public UI_Rating HeroRating;

    [Header("Hero Symbols")]
    public Image HeroSymblesHeader;
    public Color HeroSymblesHeaderColor;
    public Image HeroSymblesBackground;
    public Color HeroSymblesBackgroundColor;
    public TextMeshProUGUI HeroSymbleHeaderText;

    public GameObject HeroSymbles;
    public TextMeshProUGUI HeroMoveSymble;
    public TextMeshProUGUI HeroHeroicSymble;
    public TextMeshProUGUI HeroAttackSymble;
    public TextMeshProUGUI HeroWildSymble;
    public TextMeshProUGUI HeroSpecialCards;
    public TextMeshProUGUI HeroStartingHandCards;

    [Header("Villain Win Lose")]
    public TextMeshProUGUI VillainWins;
    public TextMeshProUGUI VillainLosses;
    public UI_Rating VillainRating;

    [Header("Villain Symbols")]
    public Image VillainSymblesHeader;
    public Color VillainSymblesHeaderColor;
    public Image VillainSymblesBackground;
    public Color VillainSymblesBackgroundColor;
    public TextMeshProUGUI VillainSymbleHeaderText;

    public GameObject VillianData;
    public GameObject VillianSymbles;
    public TextMeshProUGUI VillainMoveSymble;
    public TextMeshProUGUI VillainHeroicSymble;
    public TextMeshProUGUI VillainAttackSymble;
    public TextMeshProUGUI VillainWildSymble;

    [Header("Companion Win Lose")]
    public TextMeshProUGUI CompanionWins;
    public TextMeshProUGUI CompanionLosses;
    public UI_Rating CompanionRating;

    [Header("Companion Symbols")]
    public Image CompanionSymblesHeader;
    public Color CompanionSymblesHeaderColor;
    public Image CompanionSymblesBackground;
    public Color CompanionSymblesBackgroundColor;
    public TextMeshProUGUI CompanionSymbleHeaderText;

    public GameObject CompanionSymbles;
    public TextMeshProUGUI CompanionMoveSymble;
    public TextMeshProUGUI CompanionHeroicSymble;
    public TextMeshProUGUI CompanionAttackSymble;
    public TextMeshProUGUI CompanionWildSymble;
    public TextMeshProUGUI CompanionSpecialCards;
    public TextMeshProUGUI CompanionStartingHandCards;

    protected Character getData => (Character)data;

    public override void SetData(Searchable searchable)
    {
        searchable.OnOwnableUpdate += ApplyData;

        base.SetData(searchable);
    }

    public override void ResetData()
    {
        if (data != null) data.OnOwnableUpdate -= ApplyData;
    }

    public override void ApplyData()
    {
        base.ApplyData();
        SexTag.SetTagDisplay(Color.gray, getData.Sex.ToFriendlyString(), false);
        TypeTag.SetTagDisplay(ColorManager.GetColor(getData.Type, out bool darkText), getData.Type.ToFriendlyString(), darkText);
        SetTeamTags(getData.Teams);
        SetHeroData(getData);
        SetVillainData(getData);
        SetCompanionData(getData);
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
            var team = DataLoader.GetTeamByTag(teams[i]);
            TeamTags[i].SetTagDisplay(team.TeamColor, team.GetDisplayNameWithClarifier(), false);
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
        if (data.Type != CharacterType.Hero && data.Type != CharacterType.AntiHero)
        {
            HeroSymbles.SetActive(false);
            return;
        }

        HeroWins.text = data.HeroWins.ToString();
        HeroLosses.text = data.HeroLosses.ToString();
        HeroRating.SetRating(data.HeroRating);

        HeroSymbleHeaderText.text = CharacterType.Hero.ToFriendlyString();
        HeroSymblesHeader.color = HeroSymblesHeaderColor;
        HeroSymblesBackground.color = HeroSymblesBackgroundColor;

        HeroMoveSymble.text = data.HeroSymblesMove.ToString();
        HeroHeroicSymble.text = data.HeroSymblesHeroic.ToString();
        HeroAttackSymble.text = data.HeroSymblesAttack.ToString();
        HeroWildSymble.text = data.HeroSymblesWild.ToString();
        HeroSpecialCards.text = data.HeroSpecialCards.ToString();
        HeroStartingHandCards.text = data.HeroStartingHandCards.ToString();

        HeroSymbles.SetActive(true);
    }

    protected virtual void SetVillainData(Character data)
    {
        if (data.Type != CharacterType.Villain && data.Type != CharacterType.AntiHero)
        {
            VillianData.SetActive(false);
            VillianSymbles.SetActive(false);
            return;
        }

        VillainWins.text = data.VillainWins.ToString();
        VillainLosses.text = data.VillainLosses.ToString();
        VillainRating.SetRating(data.VillainRating);

        VillainSymbleHeaderText.text = CharacterType.Villain.ToFriendlyString();
        VillainSymblesHeader.color = VillainSymblesHeaderColor;
        VillainSymblesBackground.color = VillainSymblesBackgroundColor;

        VillianData.SetActive(true);
        if (data.IsSuperVillainModeAllowed)
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

    protected virtual void SetCompanionData(Character data)
    {
        if (data.Type != CharacterType.Companion)
        {
            CompanionSymbles.SetActive(false);
            return;
        }

        CompanionWins.text = data.CompanionWins.ToString();
        CompanionLosses.text = data.CompanionLosses.ToString();
        CompanionRating.SetRating(data.CompanionRating);

        CompanionSymbleHeaderText.text = data.Type.ToFriendlyString();
        CompanionSymblesHeader.color = CompanionSymblesHeaderColor;
        CompanionSymblesBackground.color = CompanionSymblesBackgroundColor;

        CompanionMoveSymble.text = data.CompanionSymblesMove.ToString();
        CompanionHeroicSymble.text = data.CompanionSymblesHeroic.ToString();
        CompanionAttackSymble.text = data.CompanionSymblesAttack.ToString();
        CompanionWildSymble.text = data.CompanionSymblesWild.ToString();
        CompanionSpecialCards.text = data.CompanionSpecialCards.ToString();
        CompanionStartingHandCards.text = data.CompanionStartingHandCards.ToString();

        CompanionSymbles.SetActive(true);
    }

    public virtual void HeroDataSelected()
    {
        GameEventSystem.UI_OnCharacterWinLoseSelected(getData, CharacterType.Hero);
    }

    public virtual void VillainDataSelected()
    {
        GameEventSystem.UI_OnCharacterWinLoseSelected(getData, CharacterType.Villain);
    }
    public virtual void CompanionDataSelected()
    {
        GameEventSystem.UI_OnCharacterWinLoseSelected(getData, getData.Type);
    }
}
