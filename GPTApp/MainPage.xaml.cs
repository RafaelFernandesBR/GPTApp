using Control;
using OpenAI.Chat;
using Serilog;

namespace GPTApp;
public partial class MainPage : ContentPage
{
    public MainPageViewModel ViewModel { get; set; }
    private OpenAiControl _openAiControl;
    private readonly ILogger _logger;

    public MainPage()
    {
        _logger = LoggerConfig.CreateLogger();
        InitializeComponent();
        ViewModel = new MainPageViewModel();
        BindingContext = ViewModel;
        _openAiControl = new OpenAiControl(_logger);
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
                //obter apenas os últimos 10 itens da lista, ou o que o usuário definir como tamanho
                int ChatSize = Preferences.Default.Get("Chat-Size", 10);

                var lastTenItems = ViewModel.MainPageViewModels.Skip(Math.Max(0, ViewModel.MainPageViewModels.Count - ChatSize)).Take(ChatSize);
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
            string action = await DisplayActionSheet("Opções", "Cancelar", null, "Copiar item", "Editar item");
            if (action == "Copiar item")
            {
                await Clipboard.Default.SetTextAsync(selectedItem.ListItems);
                SemanticScreenReader.Announce("Item copiado!");
            }
            else if (action == "Editar item")
            {
                // Aqui você pode adicionar a lógica para editar o item selecionado
                // Pode abrir uma nova página para edição ou exibir um diálogo, por exemplo
                EditItem(selectedItem);
            }
        }
    }

    private async void EditItem(MainPageViewModel selectedItem)
    {
        // Encontre o índice do item na lista
        int selectedIndex = ViewModel.MainPageViewModels.IndexOf(selectedItem);

        // Exibir um diálogo de edição com um campo de texto
        string editedText = await DisplayPromptAsync("Editar item", "Digite o novo texto", initialValue: selectedItem.ListItems);

        // Verificar se o diálogo foi confirmado ou cancelado
        if (editedText != null)
        {
            // Atualizar o texto do item na lista
            ViewModel.MainPageViewModels[selectedIndex].ListItems = editedText;

            // Forçar uma atualização da ListView
            MyList.ItemsSource = null;
            MyList.ItemsSource = ViewModel.MainPageViewModels;
        }
    }

    private async void OnToolbarItemClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new menu());
    }

}
