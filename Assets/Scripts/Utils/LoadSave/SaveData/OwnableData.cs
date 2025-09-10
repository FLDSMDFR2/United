using System;
using UnityEngine;

[Serializable]
public class OwnableData
{
    [SerializeField]
    public bool Owned;
    [SerializeField]
    public bool IncludeInGameBuild;
    [SerializeField]
    public DateTime LastUpdateDate;

    //Searchable
    [SerializeField]
    public bool Favorite;

    //Character data
    [SerializeField]
    public int HeroWins;
    [SerializeField]
    public int HeroLosses;
    [SerializeField]
    public float HeroRating;

    [SerializeField]
    public int VillainWins;
    [SerializeField]
    public int VillainLosses;
    [SerializeField]
    public float VillainRating;

    [SerializeField]
    public int CompanionWins;
    [SerializeField]
    public int CompanionLosses;
    [SerializeField]
    public float CompanionRating;
}
