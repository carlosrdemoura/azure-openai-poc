using Azure.AI.OpenAI;

namespace AzureOpenAI.POC.Services
{
    public class OpenAICompletionsService
    {
        private readonly OpenAIClient _openAIClient;
        private readonly IConfiguration _configuration;

        public OpenAICompletionsService(OpenAIClient openAIClient, IConfiguration configuration)
        {
            _openAIClient = openAIClient;
            _configuration = configuration;
        }

        public async Task<string> Handle(string userMessage)
        {
            ChatCompletionsOptions chatCompletionOptions = new()
            {
                Temperature = (float)0.7,
                MaxTokens = 800,
                NucleusSamplingFactor = (float)0.95,
                FrequencyPenalty = 0,
                PresencePenalty = 0,
                Messages =
                {
                    new ChatMessage(ChatRole.System, "The following is a conversation with an AI assistant. The assistant is helpful, creative, clever, and very friendly."),
                    new ChatMessage(ChatRole.User, userMessage),
                }
            };

            var chatCompletionsResponse = await _openAIClient.GetChatCompletionsAsync(_configuration["AzureOpenAI:ModelOrDeploymentName"], chatCompletionOptions);

            return chatCompletionsResponse.Value.Choices[0].Message.Content;
        }
    }
}
