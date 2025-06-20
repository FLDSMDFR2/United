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
    public bool IsExclusive;
    public List<Teams> Teams = new List<Teams>();

    public int HeroSymblesMove;
    public int HeroSymblesHeroic;
    public int HeroSymblesAttack;
    public int HeroSymblesWild;
    public int HeroSpecialCards;

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

    public override void Init()
    {
        base.Init();

        InitImage("CharacterImages/", DisplayName());
        InitDtlImage("ChracterDtlImages/", DisplayName());
    }

    protected override void InitFilter()
    {
        base.InitFilter();

        filter[typeof(CharacterType).Name] = new List<string>() { Enum.GetName(typeof(CharacterType), Type) };
        filter[typeof(CharacterSex).Name] = new List<string>() { Enum.GetName(typeof(CharacterSex), Sex) };
    }

    public override string DisplayName()
    {
        return CharacterName + " " + CharacterClarifier;
    }
    public override string SearchName()
    {
        return CharacterName + " " + CharacterClarifier;
    }
    public override string Clarifier()
    {
        return CharacterClarifier;
    }

    public virtual void SetHeroWins(int wins)
    {
        HeroWins = wins;
        RaiseOnOwnableUpdate();
    }

    public virtual void SetHeroLosses(int losses)
    {
        HeroLosses = losses;
        RaiseOnOwnableUpdate();
    }

    public virtual void SetHeroRating(float rating)
    {
        HeroRating = rating;
        RaiseOnOwnableUpdate();
    }

    public virtual void SetVillainWins(int wins)
    {
        VillainWins = wins;
        RaiseOnOwnableUpdate();
    }

    public virtual void SetVillainLosses(int losses)
    {
        VillainLosses = losses;
        RaiseOnOwnableUpdate();
    }
    public virtual void SetVillainRating(float rating)
    {
        VillainRating = rating;
        RaiseOnOwnableUpdate();
    }

}
