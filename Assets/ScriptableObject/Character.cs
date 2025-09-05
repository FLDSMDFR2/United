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

    public int HeroSymblesMove;
    public int HeroSymblesHeroic;
    public int HeroSymblesAttack;
    public int HeroSymblesWild;
    public int HeroSpecialCards;
    public int HeroStartingHandCards;

    public int HeroWins;
    public int HeroLosses;
    public float HeroRating;

    public bool IsStandAloneVillain = true;
    public bool IsSuperVillainModeAllowed = true;
    public int VillainSymblesMove;
    public int VillainSymblesAttack;
    public int VillainSymblesHeroic;
    public int VillainSymblesWild;

    public int VillainWins;
    public int VillainLosses;
    public float VillainRating;

    public string Dtls;
    public string Comments;
    public GameDtl GameDtl;

    public List<Equipment> Equipment = new List<Equipment>();

    public override void Init()
    {
        base.Init();

        InitImage("CharacterImages/", DisplayNameWithClarifier());
        InitDtlImage("ChracterDtlImages/", DisplayNameWithClarifier());
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

        sort[SortTypes.HeroWins] = HeroWins.ToString();
        sort[SortTypes.HeroLosses] = HeroLosses.ToString();
        sort[SortTypes.HeroRating] = HeroRating.ToString();

        sort[SortTypes.VillainWins] = VillainWins.ToString();
        sort[SortTypes.VillainLosses] = VillainLosses.ToString();
        sort[SortTypes.VillainRating] = VillainRating.ToString();

        sort[SortTypes.HeroMoveIcons] = HeroSymblesMove.ToString();
        sort[SortTypes.HeroAttackIcons] = HeroSymblesAttack.ToString();
        sort[SortTypes.HeroHeroicIcons] = HeroSymblesHeroic.ToString();
        sort[SortTypes.HeroWildIcons] = HeroSymblesWild.ToString();
        sort[SortTypes.HeroSpecailCards] = HeroSpecialCards.ToString();
        sort[SortTypes.HeroStartingHandCards] = HeroStartingHandCards.ToString();
    }

    public override string DisplayName()
    {
        return CharacterName;
    }
    public override string DisplayNameWithClarifier()
    {
        return DisplayName() + " " + Clarifier();
    }
    public override string SearchName()
    {
        return DisplayNameWithClarifier();
    }
    public override string Clarifier()
    {
        return CharacterClarifier;
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

    public override void SetOwnableData(OwnableData data)
    {
        base.SetOwnableData(data);

        HeroWins = data.HeroWins;
        HeroLosses = data.HeroLosses;
        HeroRating = data.HeroRating;

        VillainWins = data.VillainWins;
        VillainLosses = data.VillainLosses;
        VillainRating = data.VillainRating;
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

        return data;
    }

}
