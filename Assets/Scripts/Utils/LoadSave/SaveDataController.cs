using System;
using System.Threading.Tasks;
using UnityEngine;

public class SaveDataController
{
    protected static OwnableSaveData mainData = new OwnableSaveData();


    private static string baseDirectoy = Application.persistentDataPath;
    protected static string localOwnableFileName = "OwnableData";
    protected static OwnableSaveData localData = new OwnableSaveData();

    protected static OwnableSaveData cloudData = new OwnableSaveData();

    protected static bool flaggedForUpdate;

    #region Load
    public static async Task LoadSavedData()
    {
        SerializeManager.SetBaseDirectoryPath(baseDirectoy);

        var tasks = new Task[2];

        tasks[0] = LoadLocalDataAsync();
        tasks[1] = LoadCloudDataAsync();

        await Task.WhenAll(tasks);

        await BuildMainDataAsync();

    }

    protected static async Task LoadLocalDataAsync()
    {
        var loadTask = SerializeManager.Load<OwnableSaveData>(localOwnableFileName);
        await loadTask;
        localData = loadTask.GetAwaiter().GetResult();
    }

    protected static async Task LoadCloudDataAsync()
    {
        await Task.Run(() => LoadCloudData());
    }

    protected static void LoadCloudData()
    {

    }

    protected static async Task BuildMainDataAsync()
    {
        await Task.Run(() => BuildMainData());
    }

    protected static void BuildMainData()
    {
        if (localData != null && localData.Data.Keys.Count > 0)
        {
            OwnableData ownableData = new OwnableData();
            foreach (var key in localData.Data.Keys)
            {
                ownableData = localData.Data[key];
                if (cloudData.Data.ContainsKey(key))
                {
                    if (DateTime.Compare(cloudData.Data[key].LastUpdateDate, localData.Data[key].LastUpdateDate) < 0) ownableData = cloudData.Data[key];

                    cloudData.Data.Remove(key);
                }

                mainData.Data[key] = ownableData;
            }

            if (cloudData.Data.Keys.Count > 0) TraceManager.WriteTrace(TraceChannel.Main, TraceType.warning, "Miss Match local and Cloud Data counts");
        }
        else if (cloudData != null && cloudData.Data.Keys.Count > 0)
        {
            OwnableData ownableData = new OwnableData();
            foreach (var key in cloudData.Data.Keys)
            {
                ownableData = cloudData.Data[key];
                if (localData.Data.ContainsKey(key))
                {
                    if (DateTime.Compare(localData.Data[key].LastUpdateDate, cloudData.Data[key].LastUpdateDate) < 0) ownableData = localData.Data[key];

                    localData.Data.Remove(key);
                }

                mainData.Data[key] = ownableData;
            }

            if (localData.Data.Keys.Count > 0) TraceManager.WriteTrace(TraceChannel.Main, TraceType.warning, "Miss Match local and Cloud Data counts");
        }
    }
    #endregion

    public static async void Save()
    {
        await SerializeManager.Save(localOwnableFileName, mainData);
    }

    public static OwnableData GetDataForId(long ID)
    {
        if (mainData.Data.ContainsKey(ID)) return mainData.Data[ID];

        return null;
    }

    public static void UpdateDataForId(long ID, OwnableData data)
    {
        mainData.Data[ID] = data;

        if (!flaggedForUpdate)
        {
            flaggedForUpdate = true;
            WaitForUpdate();
        }
    }
    protected static async void WaitForUpdate()
    {
        await Task.Delay(3000);

        Save();

        flaggedForUpdate = false;
    }
}
