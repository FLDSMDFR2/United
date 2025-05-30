using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static Character;

public class UI_Character : MonoBehaviour
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
    public GameObject HeroWinLoseObject;
    public TextMeshProUGUI HeroWins;
    public TextMeshProUGUI HeroLosses;
    public UI_Rating HeroRating;

    [Header("Villain Win Lose")]
    public GameObject VillainWinLoseObject;
    public TextMeshProUGUI VillainWins;
    public TextMeshProUGUI VillainLosses;
    public UI_Rating VillainRating;

    protected Character data;

    public virtual void SetData(Character character)
    {
        data = character;
        data.OnCharacterUpdate += ApplyData;
        ApplyData();
    }

    protected virtual void ApplyData()
    {
        SetBackgroundColor(data);
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
        if (data.Type != CharacterType.Hero && data.Type != CharacterType.AntiHero)
        {
            HeroWinLoseObject.SetActive(false);
            return;
        }

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

        VillainWins.text = data.VillainWins.ToString();
        VillainLosses.text = data.VillainLosses.ToString();
        VillainRating.SetRating(data.VillainRating);
    }

    public virtual void CharaterSelected()
    {
        GameEventSystem.UI_OnCharacterSelected(data);
    }
}
