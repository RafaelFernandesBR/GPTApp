using OpenAI;
using OpenAI.Chat;

namespace Control;
public class OpenAiControl
{

    public async Task<string> GetSpeakAsync(string speak)
    {
        try
        {
            string TokenUserApi = await SecureStorage.Default.GetAsync("token-api-user");
            var api = new OpenAIClient(new OpenAIAuthentication(TokenUserApi));

            var messages = new List<Message>
{
    new Message(Role.User, speak),
};
            var chatRequest = new ChatRequest(messages);
            var resultChat = await api.ChatEndpoint.GetCompletionAsync(chatRequest);

            return resultChat.ToString();
        }
        catch (Exception ex)
        {
            return "erro, tente novamente mais tarde.";
        }

    }
}
