using System.Collections.ObjectModel;

namespace GPTApp;
public class MainPageViewModel
{
    public ObservableCollection<MainPageViewModel> MainPageViewModels { get; set; } = new ObservableCollection<MainPageViewModel>();
    public string ListItems { get; set; }
}
