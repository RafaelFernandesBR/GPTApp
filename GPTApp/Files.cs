using Newtonsoft.Json;
using System.IO;

namespace GPTApp;
public static class Files
{
    private static string mainDir = FileSystem.Current.AppDataDirectory;
    private static string fileName = "data.json";
    private static string filePath = Path.Combine(mainDir, fileName);

    public static async Task SaveState(object data)
    {
        string json = JsonConvert.SerializeObject(data);
        using FileStream outputStream = File.OpenWrite(filePath);
        using StreamWriter streamWriter = new StreamWriter(outputStream);
        await streamWriter.WriteAsync(json);
    }

    public static async Task<T> ReadState<T>()
    {
        try
        {
            using Stream fileStream = File.OpenRead(filePath);
            using StreamReader reader = new StreamReader(fileStream);
            string json = await reader.ReadToEndAsync();
            return JsonConvert.DeserializeObject<T>(json);
        }
        catch (Exception)
        {
            // tratamento de erro
            return default(T);
        }
    }
}
