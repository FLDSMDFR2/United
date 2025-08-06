using System;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    [Serializable]
    public class CharacterTypeColorMap
    {
        public CharacterType Name;
        public Color Color;
        public bool IsDarkText;
    }
    [Serializable]
    public class GameModesColorMap
    {
        public GameModes Name;
        public Color Color;
        public bool IsDarkText;
    }
    [Serializable]
    public class SeasonsColorMap
    {
        public Seasons Name;
        public Color Color;
        public bool IsDarkText;
    }
    [Serializable]
    public class TeamsColorMap
    {
        public Teams Name;
        public Color Color;
        public bool IsDarkText;
    }

    [SerializeField]
    [Header("Character Type")]
    public List<CharacterTypeColorMap> CharacterType = new List<CharacterTypeColorMap>();
    [SerializeField]
    [Header("Game Mode")]
    public List<GameModesColorMap> GameModes = new List<GameModesColorMap>();
    [SerializeField]
    [Header("Seasons")]
    public List<SeasonsColorMap> Seasons = new List<SeasonsColorMap>();
    [SerializeField]
    [Header("Teams")]
    public List<TeamsColorMap> Teams = new List<TeamsColorMap>();

    protected static Dictionary<CharacterType, CharacterTypeColorMap> characterType = new Dictionary<CharacterType, CharacterTypeColorMap>();
    protected static Dictionary<GameModes, GameModesColorMap> gameModes = new Dictionary<GameModes, GameModesColorMap>();
    protected static Dictionary<Seasons, SeasonsColorMap> seasons = new Dictionary<Seasons, SeasonsColorMap>();
    protected static Dictionary<Teams, TeamsColorMap> teams = new Dictionary<Teams, TeamsColorMap>();

    protected virtual void Start()
    {
        foreach (var c in CharacterType)
        {
            characterType[c.Name] = c;
        }
        foreach (var g in GameModes)
        {
            gameModes[g.Name] = g;
        }
        foreach (var s in Seasons)
        {
            seasons[s.Name] = s;
        }
        foreach (var t in Teams)
        {
            teams[t.Name] = t;
        }
    }

    public static Color GetColor(CharacterType name, out bool isDarkText)
    {
        isDarkText = false;
        if (characterType.ContainsKey(name))
        {
            isDarkText = characterType[name].IsDarkText;
            return characterType[name].Color;
        }
        return Color.red;
    }

    public static Color GetColor(GameModes name, out bool isDarkText)
    {
        isDarkText = false;
        if (gameModes.ContainsKey(name))
        {
            isDarkText = gameModes[name].IsDarkText;
            return gameModes[name].Color;
        }
        return Color.red;
    }
    public static Color GetColor(Seasons name, out bool isDarkText)
    {
        isDarkText = false;
        if (seasons.ContainsKey(name))
        {
            isDarkText = seasons[name].IsDarkText;
            return seasons[name].Color;
        }
        return Color.red;
    }
    public static Color GetColor(Teams name, out bool isDarkText)
    {
        isDarkText = false;
        if (teams.ContainsKey(name))
        {
            isDarkText = teams[name].IsDarkText;
            return teams[name].Color;
        }
        return Color.red;
    }
}
