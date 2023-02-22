using GPTApp;
using OpenAI;
using OpenAI.Models;

namespace Control;
public class OpenAiControl
{

    public async Task<string> GetSpeakAsync(string speak)
    {
        try
        {
            string TokenUserApi = await SecureStorage.Default.GetAsync("token-api-user");
            var api = new OpenAI.OpenAIClient(new OpenAIAuthentication(TokenUserApi));
            var result = await api.CompletionsEndpoint.CreateCompletionAsync(speak, temperature: 0.98, maxTokens: 3000, model: Model.Davinci);
            string n = result.ToString();

            return n;
        }
        catch (Exception ex)
        {
            return "erro, tente novamente mais tarde.";
        }

    }
}
