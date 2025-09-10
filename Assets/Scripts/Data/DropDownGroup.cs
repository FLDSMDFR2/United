using System.Collections.Generic;
using UnityEngine;

public class DropDownGroup
{
    public UI_DropDownHeader Header;
    public List<UI_CollectionDtl> Items;

    public List<GameObject> GetItemGameObjects()
    {
        var list = new List<GameObject>();
        foreach (var item in Items)
        {
            list.Add(item.gameObject);
        }
        return list;
    }
}
