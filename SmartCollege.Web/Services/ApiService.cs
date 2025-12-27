using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace SmartCollege.Web.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContext;

        public ApiService(HttpClient httpClient, IHttpContextAccessor context)
        {
            _httpClient = httpClient;
            _httpContext = context;
        }

        private void AddJwtHeader()
        {
            var token = _httpContext.HttpContext?.Session.GetString("JWT");
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }
        }

        public async Task<T?> GetAsync<T>(string url)
        {
            AddJwtHeader();
            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode) return default;

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<T?> PostAsync<T>(string url, object data)
        {
            var content = new StringContent(
                JsonSerializer.Serialize(data),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PostAsync(url, content);
            if (!response.IsSuccessStatusCode) return default;

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}
