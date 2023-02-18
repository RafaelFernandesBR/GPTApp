using Control;

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

    private async void OnCounterClicked(object sender, EventArgs e)
    {
        string userInput = UserInputEntry.Text;

        if (!string.IsNullOrEmpty(userInput))
        {
            ViewModel.MainPageViewModels.Add(new MainPageViewModel() { ListItems = userInput });
            UserInputEntry.Text = string.Empty;

            string itemsString = null;

            if (ViewModel.MainPageViewModels.Count > 10)
            {
                // obter apenas os últimos 10 itens da lista
                var lastTenItems = ViewModel.MainPageViewModels.Skip(Math.Max(0, ViewModel.MainPageViewModels.Count - 10)).Take(10);

                foreach (var item in lastTenItems)
                {
                    itemsString += item.ListItems + "\n";
                }
            }
            else
            {
                foreach (var item in ViewModel.MainPageViewModels)
                {
                    itemsString += item.ListItems + "\n";
                }
            }

            string response = await _openAiControl.GetSpeakAsync(itemsString);
            SemanticScreenReader.Announce(response);
            ViewModel.MainPageViewModels.Add(new MainPageViewModel() { ListItems = response });
        }
    }

    private async void OnToolbarItemClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new menu());
    }

}
