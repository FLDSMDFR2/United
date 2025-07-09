using System;
using System.Collections.Generic;

[Serializable]
public class GameDtl
{
    public bool AdditionalHerosReferenceNumHeros;
    public int AdditionalHeros;
    public int NumberOfVillains;

    public bool RequiredHerosExclusively;
    public List<Required> RequiredHeros = new List<Required>();
    public List<Character> ExcludedHeros = new List<Character>();

    public bool RequiaredVillainsExclusively;
    public List<Required> RequiaredVillains = new List<Required>();
    public List<Character> ExcludedVillains = new List<Character>();

    public List<Required> RequiaredLocations = new List<Required>();
    public List<Location> ExcludedLocations = new List<Location>();

    public List<Required> RequiaredChallenges = new List<Required>();
    public List<Challenge> ExcludedChallenges = new List<Challenge>();
}

[Serializable]
public class Required
{
    public int NumberRequired;
    public List<BoxOwnable> RequiredObjects = new List<BoxOwnable>();
}

