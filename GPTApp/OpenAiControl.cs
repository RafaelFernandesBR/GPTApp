using OpenAI;
using OpenAI.Models;

namespace Control;
public class OpenAiControl
{

    public async Task<string> GetSpeakAsync(string speak)
    {
        try
        {
            string tokenFile = await GPTApp.Files.ReadFile();
            var api = new OpenAI.OpenAIClient(new OpenAIAuthentication(tokenFile));
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
