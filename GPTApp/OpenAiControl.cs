using GPTApp;
using OpenAI;
using OpenAI.Chat;

namespace Control;
public class OpenAiControl
{

    public async Task<string> GetSpeakAsync(IEnumerable<MainPageViewModel> viewModels)
    {
        try
        {
            string TokenUserApi = await SecureStorage.Default.GetAsync("token-api-user");
            var api = new OpenAIClient(new OpenAIAuthentication(TokenUserApi));

            var messages = AlternateRoles(viewModels);
            var chatRequest = new ChatRequest(messages);
            var resultChat = await api.ChatEndpoint.GetCompletionAsync(chatRequest);

            return resultChat.ToString();
        }
        catch (Exception)
        {
            return "erro, tente novamente mais tarde.";
        }

    }

    private List<Message> AlternateRoles(IEnumerable<MainPageViewModel> viewModels)
    {
        var result = new List<Message>();
        var i = 0;

        foreach (var viewModel in viewModels)
        {
            var newMessage = new Message(viewModel.role, viewModel.ListItems);
            result.Add(newMessage);
            i++;
        }

        return result;
    }

}
