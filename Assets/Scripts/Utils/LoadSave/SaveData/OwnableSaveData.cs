using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class OwnableSaveData
{
    [SerializeField]
    public Dictionary<long, OwnableData> Data = new Dictionary<long, OwnableData>();
}
