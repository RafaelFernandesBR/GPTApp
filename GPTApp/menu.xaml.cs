namespace GPTApp;
public partial class menu : ContentPage
{
    public menu()
    {
        InitializeComponent();
        MenuList.ItemsSource = new[] { "Definir token da API" };
    }

    private async void OnMenuItemTapped(object sender, ItemTappedEventArgs e)
    {
        if (e.Item == null)
            return;
        var tokenFile = await Files.ReadState<UserData>() ?? new UserData();
        // Mostra um alerta para o usuário com um campo de edição e botões OK e Cancelar
        tokenFile.Tokem = await DisplayPromptAsync("Token da API", "Digite o token da API", initialValue: tokenFile.Tokem);

        if (!string.IsNullOrEmpty(token))
        {
            await Files.SaveState(tokenFile);
        }

        MenuList.SelectedItem = null;
    }

}