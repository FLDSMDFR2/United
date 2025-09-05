using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_SearchDtlCharacter : UI_SearchDtl
{
    [Header("Background")]
    public Color HeroBackground;
    public Color VillainBackground;
    public Color AntiHeroBackground;
    public Color CompanionBackground;

    [Header("Team Tag")]
    public List<UI_Tag> TeamTags;
    public TextMeshProUGUI TeamOverFlow;

    [Header("Hero Win Lose")]
    public GameObject HeroWinLoseObject;
    public TextMeshProUGUI HeroWins;
    public TextMeshProUGUI HeroLosses;
    public UI_Rating HeroRating;

    [Header("Villain Win Lose")]
    public GameObject VillainWinLoseObject;
    public TextMeshProUGUI VillainWins;
    public TextMeshProUGUI VillainLosses;
    public UI_Rating VillainRating;

    [Header("Cards / Symbols")]
    public GameObject CardsAndSymbols;
    public TextMeshProUGUI MoveText;
    public TextMeshProUGUI HeroicText;
    public TextMeshProUGUI AttackText;
    public TextMeshProUGUI WildText;
    public TextMeshProUGUI SpecialCards;
    public TextMeshProUGUI StartingHandCards;

    protected Character getData => (Character)data;

    protected override void ApplyData()
    {
        base.ApplyData();

        Name.text = getData.CharacterName;
        TypeTag.SetTagDisplay(ColorManager.GetColor(getData.Type, out bool darkText), getData.Type.ToFriendlyString(), darkText);    
        SetTeamTags(getData.Teams);
        SetHeroData(getData);
        SetVillainData(getData);
        SetCardsAndSymbols(getData);
    }

    protected override void SetBackgroundColor()
    {
        if (getData.Type == CharacterType.Hero) Background.color = HeroBackground;
        else if (getData.Type == CharacterType.Villain) Background.color = VillainBackground;
        else if (getData.Type == CharacterType.AntiHero) Background.color = AntiHeroBackground;
        else if (getData.Type == CharacterType.Companion) Background.color = CompanionBackground;
    }

    protected virtual void SetTeamTags(List<Teams> teams)
    {
        foreach (var team in TeamTags)
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
            TeamTags[i].SetTagDisplay(team.TeamColor, team.DisplayNameWithClarifier(), false);
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
            HeroWinLoseObject.SetActive(false);
            return;
        }

        HeroWinLoseObject.SetActive(true);
        HeroWins.text = data.HeroWins.ToString();
        HeroLosses.text = data.HeroLosses.ToString();
        HeroRating.SetRating(data.HeroRating);
    }

    protected virtual void SetVillainData(Character data)
    {
        if (data.Type != CharacterType.Villain && data.Type != CharacterType.AntiHero)
        {
            VillainWinLoseObject.SetActive(false);
            return;
        }

        VillainWinLoseObject.SetActive(true);
        VillainWins.text = data.VillainWins.ToString();
        VillainLosses.text = data.VillainLosses.ToString();
        VillainRating.SetRating(data.VillainRating);
    }

    protected virtual void SetCardsAndSymbols(Character data)
    {
        if (data.Type == CharacterType.Villain)
        {
            CardsAndSymbols.SetActive(false);
            return;
        }

        MoveText.text = data.HeroSymblesMove.ToString();
        HeroicText.text = data.HeroSymblesHeroic.ToString();
        AttackText.text = data.HeroSymblesAttack.ToString();
        WildText.text = data.HeroSymblesWild.ToString();
        SpecialCards.text = data.HeroSpecialCards.ToString();
        StartingHandCards.text = data.HeroStartingHandCards.ToString();

        CardsAndSymbols.SetActive(true);
    }

    public override void Selected()
    {
        GameEventSystem.UI_OnCharacterSelected(getData);
    }
}
