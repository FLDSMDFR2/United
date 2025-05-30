using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Character : ScriptableObject
{
    [Serializable]
    public class CharacterBoxDtl
    { 
        public Box Box;
        public bool Default; // default for this box
    }

    public delegate void CharacterUpdate();
    public event CharacterUpdate OnCharacterUpdate;

    public string CharacterName;
    public string CharacterClarifier;
    public Sprite ChracterImage;
    public Sprite ChracterDtlImage;
    public CharacterType Type;
    public CharacterSex Sex;
    public Seasons Season;
    public List<CharacterBoxDtl> Boxs = new List<CharacterBoxDtl>();
    public bool IsExclusive;
    public List<Teams> Teams = new List<Teams>();
    public List<GameModes> RequiredIfMode = new List<GameModes>();
    public bool Owned;

    public int HeroSymblesMove;
    public int HeroSymblesAttack;
    public int HeroSymblesHeroic;
    public int HeroSymblesWild;
    public int HeroSpecialCards;

    public int HeroWins;
    public int HeroLosses;
    public float HeroRating;

    public int VillainSymblesMove;
    public int VillainSymblesAttack;
    public int VillainSymblesHeroic;
    public int VillainSymblesWild;

    public int VillainWins;
    public int VillainLosses;
    public float VillainRating;

    public string Dtls;
    public string Comments;

    public virtual void Init()
    {
        GetChracterImage();
        GetChracterDtlImage();
    }

    protected virtual void GetChracterImage()
    {
        var filename = CharacterName.Replace(" ", string.Empty) + CharacterClarifier.Replace(" ", string.Empty);
        filename = filename.Replace("-", string.Empty);
        var image = Resources.Load<Sprite>("CharacterImage/" + filename);
        if (image != null) ChracterImage = image;
    }

    protected virtual void GetChracterDtlImage()
    {
        var filename = CharacterName.Replace(" ", string.Empty) + CharacterClarifier.Replace(" ", string.Empty);
        filename = filename.Replace("-", string.Empty);
        var image = Resources.Load<Sprite>("ChracterDtlImage/" + filename);
        if (image != null) ChracterDtlImage = image;
    }
    public virtual void SetOwned(bool owned)
    {
        Owned = owned;
        OnCharacterUpdate?.Invoke();
    }

    public virtual void SetHeroWins(int wins)
    {
        HeroWins = wins;
        OnCharacterUpdate?.Invoke();
    }

    public virtual void SetHeroLosses(int losses)
    {
        HeroLosses = losses;
        OnCharacterUpdate?.Invoke();
    }

    public virtual void SetHeroRating(float rating)
    {
        HeroRating = rating;
        OnCharacterUpdate?.Invoke();
    }

    public virtual void SetVillainWins(int wins)
    {
        VillainWins = wins;
        OnCharacterUpdate?.Invoke();
    }

    public virtual void SetVillainLosses(int losses)
    {
        VillainLosses = losses;
        OnCharacterUpdate?.Invoke();
    }
    public virtual void SetVillainRating(float rating)
    {
        VillainRating = rating;
        OnCharacterUpdate?.Invoke();
    }

}
