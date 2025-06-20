using System.Collections.Generic;

public class BuildGame
{
    public List<Character> Villains = new List<Character>();
    public Challenge Challenge;
    public List<Location> Locations = new List<Location>();
    public List<Character> Heros = new List<Character>();
    public List<Character> AdditionalHeros = new List<Character>();
}

public class BuildGameData
{
    public Mode Mode;
    public List<BuildGame> Games = new List<BuildGame>();
}
