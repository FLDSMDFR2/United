using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GameLoader
{
    protected static List<ILoadable> loadables = new List<ILoadable>();

    public static void Register(ILoadable loadable)
    {
        if (!loadables.Contains(loadable)) loadables.Add(loadable);
    }

    public static async Task LoadStep1Async()
    {
        foreach (var loadable in loadables)
        {
            loadable.LoadableStep1();
            await Task.Yield();
        }
    }

    public static async Task LoadStep2Async()
    {
        foreach (var loadable in loadables)
        {
            loadable.LoadableStep2();
            await Task.Yield();
        }
    }
}
