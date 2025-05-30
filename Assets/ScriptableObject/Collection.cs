using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Collection : ScriptableObject
{
    [Serializable]
    public class BoxDtls
    {
        public Sprite Image;
        public Box BoxTag;
        public bool Owned;
        public Color BoxColor;
        public bool IsDarkText;
    }

    public List<BoxDtls> BoxList = new List<BoxDtls>();
}
