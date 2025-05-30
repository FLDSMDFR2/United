using System.Collections.Generic;
using UnityEngine;

public class DataLoader : MonoBehaviour
{
    protected static List<Character> allCharacters = new List<Character>();

    protected static Dictionary<Box, List<Character>> characterToBoxMap = new Dictionary<Box, List<Character>>();

    protected virtual void Awake()
    {
        LoadCharacters();
    }

    protected virtual void LoadCharacters()
    {
        allCharacters.AddRange(Resources.LoadAll<Character>("Characters/"));

        foreach(var character in allCharacters)
        {
            character.Init();

            AddCharacterToBox(character);
        }
    }

    public static List<Character> GetAllCharacters()
    {
        return allCharacters;
    }

    public static List<Character> GetAllCharactersByBox(Box box)
    {
        if (characterToBoxMap.ContainsKey(box)) return characterToBoxMap[box];
        return new List<Character>();
    }

    protected static void AddCharacterToBox(Character character)
    {
        foreach (var box in character.Boxs)
        {
            if (characterToBoxMap.ContainsKey(box.Box)) characterToBoxMap[box.Box].Add(character);
            else characterToBoxMap[box.Box] = new List<Character>() { character };
        }
    }
}

