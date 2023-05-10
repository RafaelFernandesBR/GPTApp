namespace GPTApp;
public partial class menu : ContentPage
{
    public menu()
    {
        InitializeComponent();
        bool clearList = Preferences.Default.Get("Vibration-Clear-List", true);
        VibrationCheckbox.IsChecked = clearList;
    }

    private async void OnDefineTokenClicked(object sender, EventArgs e)
    {
        string TokenUserApi = await SecureStorage.Default.GetAsync("token-api-user");
        // Mostra um alerta para o usuário com um campo de edição e botões OK e Cancelar
        string Tokem = await DisplayPromptAsync("Token da API", "Digite o token da API", initialValue: TokenUserApi);

        if (!string.IsNullOrEmpty(Tokem))
        {
            await SecureStorage.Default.SetAsync("token-api-user", Tokem);
        }
    }

    private async void OnDefineChatSizeClicked(object sender, EventArgs e)
    {
        string ChatSizeActual = Preferences.Default.Get("Chat-Size", 10).ToString();

        // Mostra um alerta para o usuário com um campo de edição e botões OK e Cancelar
        string ChatSize = await DisplayPromptAsync("Tamanho do chat", "Defina o tamanho da lista a ser enviada para o chat", keyboard: Keyboard.Telephone, initialValue: ChatSizeActual);

        if (!string.IsNullOrEmpty(ChatSize))
        {
            bool isNumeric = int.TryParse(ChatSize, out int result);
            if (isNumeric && result > 10)
            {
                Preferences.Default.Set("Chat-Size", result);
            }
            else
            {
                await DisplayAlert("Tamanho do chat inválido", "O tamanho do chat deve ser um número válido e maior que 10", "OK");
            }
        }
    }

    private async void OnViewLogsClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ViewLogs());
    }

    private void OnVibrationCheckboxCheckedChanged(object sender, EventArgs e)
    {
        bool isChecked = VibrationCheckbox.IsChecked;

        Preferences.Default.Set("Vibration-Clear-List", isChecked);
    }

}