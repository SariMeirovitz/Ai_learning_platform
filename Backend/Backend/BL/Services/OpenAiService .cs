using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BL.Services
{
    public class OpenAiService
    {
        private readonly string _apiKey;
        private readonly HttpClient _httpClient;

        public OpenAiService(string apiKey)
        {
            _apiKey = apiKey;
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://api.openai.com/v1/");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
        }

        public async Task<string> GetChatCompletionAsync(string prompt)
        {
            var requestBody = new
            {
                model = "gpt-3.5-turbo",
                messages = new[]
                {
                    new { role = "system", content = "You are a helpful teacher." },
                    new { role = "user", content = prompt }
                },
                max_tokens = 512,
                temperature = 0.7
            };

            var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

            try
            {
                var response = await _httpClient.PostAsync("chat/completions", content);

                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    throw new Exception($"OpenAI API error: {response.StatusCode} - {error}");
                }

                var responseString = await response.Content.ReadAsStringAsync();
                using var doc = JsonDocument.Parse(responseString);
                var message = doc.RootElement
                    .GetProperty("choices")[0]
                    .GetProperty("message")
                    .GetProperty("content")
                    .GetString();

                return message?.Trim() ?? string.Empty;
            }
            catch (Exception ex)
            {
                return $"[OpenAI Error] {ex.Message}";
            }
        }
    }
}
