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

    private void OnVibrationCheckboxCheckedChanged(object sender, EventArgs e)
    {
        bool isChecked = VibrationCheckbox.IsChecked;

        Preferences.Default.Set("Vibration-Clear-List", isChecked);
    }

}