using System.Collections.Generic;

public class BuildGame
{
    public List<Character> Villains = new List<Character>();
    public List<Challenge> Challenges = new List<Challenge>();
    public List<Location> Locations = new List<Location>();
    public List<Team> Teams = new List<Team>();
    public List<Character> Heros = new List<Character>();
    public List<Character> AdditionalHeros = new List<Character>();
    public List<Character> Companions = new List<Character>();
    public List<Equipment> Equipment = new List<Equipment>();
    public List<Campaign> Campaigns = new List<Campaign>();
}

public class BuildGameData
{
    public List<Mode> Mode = new List<Mode>();
    public List<BuildGame> Games = new List<BuildGame>();
}
