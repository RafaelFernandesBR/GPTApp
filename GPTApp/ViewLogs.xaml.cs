namespace GPTApp;

public partial class ViewLogs : ContentPage
{
    public ViewLogs()
    {
        InitializeComponent();
        DefineLogs();
    }

    private void DefineLogs()
    {
        var logsLabel = (Label)FindByName("logs");
        string logFilePath = Path.Combine(FileSystem.AppDataDirectory, "gpt-android.log");

        // Verifica se o arquivo de log existe antes de tentar abri-lo
        if (File.Exists(logFilePath))
        {
            // Lê o conteúdo do arquivo de log e salva em uma string
            string logText = File.ReadAllText(logFilePath);
            logsLabel.Text = logText;
        }
    }

}
