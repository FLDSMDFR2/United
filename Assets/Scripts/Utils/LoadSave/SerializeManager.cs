using System;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using Newtonsoft.Json;

public class SerializeManager
{
    private static string baseDirectoy;
    private static string mainDirectory =  "/SaveData";
    private static string fileExtention = ".bam";

    public static void SetBaseDirectoryPath(string path)
    {
        baseDirectoy = path;
    }

    protected static string ConfigFilePath(string fileName)
    {
        return baseDirectoy + mainDirectory + "/" + fileName + fileExtention;
    }

    protected static void FilePathCheck(string path)
    {
        System.IO.FileInfo file = new System.IO.FileInfo(path);      
        if (File.Exists(path))

        {
            // do nothing
            //File.Delete(path);
        }
        else
        {
            file.Directory.Create();
        } 
    }

    public static async Task Save(string fileName, object obj)
    {
        var path = ConfigFilePath(fileName);
        FilePathCheck(path);

        try
        {
            TraceManager.WriteTrace(TraceChannel.Main, TraceType.info, $"Saving value to path {path}");

            var jsonString = JsonConvert.SerializeObject(obj);

            await File.WriteAllTextAsync(path, jsonString);
        }
        catch (Exception ex)
        {
            TraceManager.WriteTrace(TraceChannel.Main, TraceType.error, "Error while attempting to file.");
            TraceManager.WriteTrace(TraceChannel.Main, TraceType.error, $"Path={path}");
            TraceManager.WriteTrace(TraceChannel.Main, TraceType.error, $"Error={ex.Message}");
        }    
    }

    public static async Task<T> Load<T>(string fileName)
    {
        var path = ConfigFilePath(fileName);
        if (File.Exists(path))
        {
            try
            {
                TraceManager.WriteTrace(TraceChannel.Main, TraceType.info, $"Loading values from path {path}");

                var readTask = File.ReadAllTextAsync(path);
                await readTask;
                string jsonString = readTask.GetAwaiter().GetResult();

                T retval = JsonConvert.DeserializeObject<T>(jsonString);
                if (retval == null)
                {
                    TraceManager.WriteTrace(TraceChannel.Main, TraceType.error, $"DataLoader {fileName} Failed to load");
                    retval = default;
                }
                return retval;
            }
            catch (Exception ex)
            {
                TraceManager.WriteTrace(TraceChannel.Main, TraceType.error, "Error while attempting to file.");
                TraceManager.WriteTrace(TraceChannel.Main, TraceType.error, $"Path={path}");
                TraceManager.WriteTrace(TraceChannel.Main, TraceType.error, $"Error={ex.Message}");
                return default(T);

            }
        }
        else
        {
            TraceManager.WriteTrace(TraceChannel.Main, TraceType.info, "No Data to Load");
            return default(T);
        }
    }
}
