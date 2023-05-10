namespace GPTApp;

public partial class ViewLogs : ContentPage
{
    public ViewLogs()
    {
        InitializeComponent();
        DefineLogs();
    }

    private async void OnCopyLogsClicked(object sender, EventArgs e)
    {
        string logText = GetLogsFile();
        var logsLabel = (Label)FindByName("logs");

        if (string.IsNullOrEmpty(logText))
        {
            await Clipboard.Default.SetTextAsync(logText);
            SemanticScreenReader.Announce("Copiado!");
        }
    }

    private async void OnClearLogsClicked(object sender, EventArgs e)
    {
        string logFilePath = Path.Combine(FileSystem.AppDataDirectory, "gpt-android.log");

        if (File.Exists(logFilePath))
        {
            var logsLabel = (Label)FindByName("logs");

            File.Delete(logFilePath);
            SemanticScreenReader.Announce("Logs limpos");

            logsLabel.Text = null;
        }
    }

    private void DefineLogs()
    {
        string logText = GetLogsFile();
        var logsLabel = (Label)FindByName("logs");

        logsLabel.Text = logText;
    }

    private string GetLogsFile()
    {
        string logFilePath = Path.Combine(FileSystem.AppDataDirectory, "gpt-android.log");

        // Verifica se o arquivo de log existe antes de tentar abri-lo
        if (File.Exists(logFilePath))
        {
            // Lê o conteúdo do arquivo de log e salva em uma string
            string logText = File.ReadAllText(logFilePath);
            return logText;
        }

        return null;
    }

}
