using System;
using System.Collections.Generic;

[Serializable]
public class GameDtl
{
    public bool AdditionalHerosReferenceNumHeros;
    public int AdditionalHeros;
    public int NumberOfVillains;

    public bool RequiaredHerosExclusively;
    public List<Character> RequiaredHeros = new List<Character>();
    public List<Character> ExcludedHeros = new List<Character>();

    public bool RequiaredVillainsExclusively;
    public List<Character> RequiaredVillains = new List<Character>();
    public List<Character> ExcludedVillains = new List<Character>();

    public bool RequiaredLocationsExclusively;
    public int NumberRequiaredLocations;
    public List<Location> RequiaredLocations = new List<Location>();
    public List<Location> ExcludedLocations = new List<Location>();

    public List<Challenge> RequiaredChallenges = new List<Challenge>();
    public List<Challenge> ExcludedChallenges = new List<Challenge>();
}
