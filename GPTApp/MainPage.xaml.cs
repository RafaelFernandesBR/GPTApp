using Control;
using OpenAI.Chat;
using static Android.App.LauncherActivity;

namespace GPTApp;
public partial class MainPage : ContentPage
{
    public MainPageViewModel ViewModel { get; set; }
    private OpenAiControl _openAiControl;

    public MainPage()
    {
        InitializeComponent();
        ViewModel = new MainPageViewModel();
        BindingContext = ViewModel;
        _openAiControl = new OpenAiControl();
    }

    private async void OnUserGetSpeak(object sender, EventArgs e)
    {
        string userInput = UserInputEntry.Text;
        UserInputEntry.Unfocus();
        string TokenUserApi = await SecureStorage.Default.GetAsync("token-api-user");

        if (TokenUserApi == null)
        {
            await DisplayAlert("Token da API", "Por favor, defina o token da API nas configurações.", "OK");
            return;
        }

        if (!string.IsNullOrEmpty(userInput))
        {
            ViewModel.MainPageViewModels.Add(new MainPageViewModel() { ListItems = userInput, role = Role.User });
            UserInputEntry.Text = string.Empty;

            string response = null;

            if (ViewModel.MainPageViewModels.Count > 10)
            {
                // obter apenas os últimos 10 itens da lista
                var lastTenItems = ViewModel.MainPageViewModels.Skip(Math.Max(0, ViewModel.MainPageViewModels.Count - 10)).Take(10);
                response = await _openAiControl.GetSpeakAsync(lastTenItems);
            }
            else
            {
                response = await _openAiControl.GetSpeakAsync(ViewModel.MainPageViewModels);
            }

            SemanticScreenReader.Announce(response);

            // Criar um novo objeto MainPageViewModel com a resposta e adicioná-lo à lista
            MainPageViewModel responseViewModel = new MainPageViewModel() { ListItems = response, role = Role.Assistant };
            ViewModel.MainPageViewModels.Add(responseViewModel);
        }
    }

    private void OnClearList(object sender, EventArgs e)
    {
        if (ViewModel.MainPageViewModels.Count > 0)
        {
            bool vibrateClearList = Preferences.Default.Get("Vibration-Clear-List", true);
            ViewModel.MainPageViewModels.Clear();

            if (vibrateClearList)
            {
                Vibration.Default.Vibrate(20);
            }
        }
    }

    private async void OnItemClicked(object sender, EventArgs e)
    {
        var listView = sender as ListView;
        var selectedItem = listView.SelectedItem as MainPageViewModel;
        if (selectedItem != null)
        {
            string action = await DisplayActionSheet("Quer copiar o item?", "Cancelar", null, "Copiar item");
            if (action == "Copiar item")
            {
                await Clipboard.Default.SetTextAsync(selectedItem.ListItems);
                SemanticScreenReader.Announce("Item copiado!");
            }
        }
    }

    private async void OnToolbarItemClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new menu());
    }

}
