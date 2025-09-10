using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Character : BoxOwnable
{
    [Header("Character")]
    public string CharacterName;
    public string CharacterClarifier;
    public CharacterType Type;
    public CharacterSex Sex;
    public List<Teams> Teams = new List<Teams>();

    [Header("HERO")]
    public int HeroSymblesMove;
    public int HeroSymblesHeroic;
    public int HeroSymblesAttack;
    public int HeroSymblesWild;
    public int HeroSpecialCards;
    public int HeroStartingHandCards;

    public int HeroWins;
    public int HeroLosses;
    public float HeroRating;

    [Header("VILLAIN")]
    public bool IsStandAloneVillain = true;
    public bool IsSuperVillainModeAllowed = true;
    public int VillainSymblesMove;
    public int VillainSymblesAttack;
    public int VillainSymblesHeroic;
    public int VillainSymblesWild;

    public int VillainWins;
    public int VillainLosses;
    public float VillainRating;

    [Header("COMPANION")]
    public int CompanionSymblesMove;
    public int CompanionSymblesHeroic;
    public int CompanionSymblesAttack;
    public int CompanionSymblesWild;
    public int CompanionSpecialCards;
    public int CompanionStartingHandCards;

    public int CompanionWins;
    public int CompanionLosses;
    public float CompanionRating;

    public string Dtls;
    public string Comments;
    public GameDtl GameDtl;

    public List<Equipment> Equipment = new List<Equipment>();

    public override void Init()
    {
        base.Init();

        InitImage("CharacterImages/", GetDisplayNameWithClarifier());
        InitDtlImage("ChracterDtlImages/", GetDisplayNameWithClarifier());
    }

    protected override void InitFilter()
    {
        base.InitFilter();

        filter[typeof(CharacterType).Name] = new List<string>() { Enum.GetName(typeof(CharacterType), Type) };
        filter[typeof(CharacterSex).Name] = new List<string>() { Enum.GetName(typeof(CharacterSex), Sex) };
    }

    protected override void InitSort()
    {
        base.InitSort();

        sort[SortTypes.VillainWins] = VillainWins.ToString();
        sort[SortTypes.VillainLosses] = VillainLosses.ToString();
        sort[SortTypes.VillainRating] = VillainRating.ToString();

        sort[SortTypes.HeroWins] = HeroWins.ToString();
        sort[SortTypes.HeroLosses] = HeroLosses.ToString();
        sort[SortTypes.HeroRating] = HeroRating.ToString();

        sort[SortTypes.HeroMoveIcons] = HeroSymblesMove.ToString();
        sort[SortTypes.HeroAttackIcons] = HeroSymblesAttack.ToString();
        sort[SortTypes.HeroHeroicIcons] = HeroSymblesHeroic.ToString();
        sort[SortTypes.HeroWildIcons] = HeroSymblesWild.ToString();
        sort[SortTypes.HeroSpecailCards] = HeroSpecialCards.ToString();
        sort[SortTypes.HeroStartingHandCards] = HeroStartingHandCards.ToString();

        sort[SortTypes.CompanionWins] = CompanionWins.ToString();
        sort[SortTypes.CompanionLosses] = CompanionLosses.ToString();
        sort[SortTypes.CompanionRating] = CompanionRating.ToString();

        sort[SortTypes.CompanionMoveIcons] = CompanionSymblesMove.ToString();
        sort[SortTypes.CompanionAttackIcons] = CompanionSymblesAttack.ToString();
        sort[SortTypes.CompanionHeroicIcons] = CompanionSymblesHeroic.ToString();
        sort[SortTypes.CompanionWildIcons] = CompanionSymblesWild.ToString();
        sort[SortTypes.CompanionSpecailCards] = CompanionSpecialCards.ToString();
        sort[SortTypes.CompanionStartingHandCards] = CompanionStartingHandCards.ToString();
    }

    public virtual void SetHeroWins(int wins)
    {
        HeroWins = wins;
        UpdateAndSaveData();
        RaiseOnOwnableUpdate();
    }

    public virtual void SetHeroLosses(int losses)
    {
        HeroLosses = losses;
        UpdateAndSaveData();
        RaiseOnOwnableUpdate();
    }

    public virtual void SetHeroRating(float rating)
    {
        HeroRating = rating;
        UpdateAndSaveData();
        RaiseOnOwnableUpdate();
    }

    public virtual void SetVillainWins(int wins)
    {
        VillainWins = wins;
        UpdateAndSaveData();
        RaiseOnOwnableUpdate();
    }

    public virtual void SetVillainLosses(int losses)
    {
        VillainLosses = losses;
        UpdateAndSaveData();
        RaiseOnOwnableUpdate();
    }
    public virtual void SetVillainRating(float rating)
    {
        VillainRating = rating;
        UpdateAndSaveData();
        RaiseOnOwnableUpdate();
    }

    public virtual void SetCompanionWins(int wins)
    {
        CompanionWins = wins;
        UpdateAndSaveData();
        RaiseOnOwnableUpdate();
    }

    public virtual void SetCompanionLosses(int losses)
    {
        CompanionLosses = losses;
        UpdateAndSaveData();
        RaiseOnOwnableUpdate();
    }
    public virtual void SetCompanionRating(float rating)
    {
        CompanionRating = rating;
        UpdateAndSaveData();
        RaiseOnOwnableUpdate();
    }

    public override void SetOwnableData(OwnableData data)
    {
        base.SetOwnableData(data);

        HeroWins = data.HeroWins;
        HeroLosses = data.HeroLosses;
        HeroRating = data.HeroRating;

        VillainWins = data.VillainWins;
        VillainLosses = data.VillainLosses;
        VillainRating = data.VillainRating;

        CompanionWins = data.CompanionWins;
        CompanionLosses = data.CompanionLosses;
        CompanionRating = data.CompanionRating;
    }

    public override OwnableData GetOwnableData()
    {
        var data = base.GetOwnableData();
        data.HeroWins = HeroWins;
        data.HeroLosses = HeroLosses;
        data.HeroRating = HeroRating;

        data.VillainWins = VillainWins;
        data.VillainLosses = VillainLosses;
        data.VillainRating = VillainRating;

        data.CompanionWins = CompanionWins;
        data.CompanionLosses = CompanionLosses;
        data.CompanionRating = CompanionRating;

        return data;
    }

}
