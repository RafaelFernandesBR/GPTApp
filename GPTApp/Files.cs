namespace GPTApp;
public static class Files
{
    private static string mainDir = FileSystem.Current.AppDataDirectory;
    private static string fileName = "token.txt";
    private static string filePath = System.IO.Path.Combine(mainDir, fileName);

    public static async Task SaveState(string str)
    {
        using FileStream outputStream = System.IO.File.OpenWrite(filePath);
        using StreamWriter streamWriter = new StreamWriter(outputStream);

        await streamWriter.WriteAsync(str);
    }

    public static async Task<string> ReadFile()
    {
        try
        {
            using Stream fileStream = System.IO.File.OpenRead(filePath);
            using StreamReader reader = new StreamReader(fileStream);

            var c = await reader.ReadToEndAsync();

            return c;
        }
        catch (Exception ex)
        {
            return null;
        }

        return String.Empty;
    }
}
