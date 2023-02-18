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
        string tokenFile = await Files.ReadFile();
        // Mostra um alerta para o usuário com um campo de edição e botões OK e Cancelar
        string token = await DisplayPromptAsync("Token da API", "Digite o token da API", initialValue: tokenFile);

        if (!string.IsNullOrEmpty(token))
        {
            await Files.SaveState(token);
        }

        MenuList.SelectedItem = null;
    }

}