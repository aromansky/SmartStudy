//using MySql.Data.MySqlClient;
using System.Configuration;
using System.Text.Json;
using SmartStudy.Models;
using System.Text;
using System.Diagnostics;
using System.Net.Http.Json;
using MySqlX.XDevAPI;
using System.Text.RegularExpressions;

namespace SmartStudy
{
    static class Client
    {
        private static HttpClient _client = new HttpClient();
        private static JsonSerializerOptions _serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };

        public static async void Register(string FirstName, string LastName, string Email, string Password)
        {

            User user = new User(FirstName, LastName, Email, Password);
            Uri uri = new Uri(string.Format(Constants.RestUrl, string.Empty));

            try
            {
                string json = JsonSerializer.Serialize<User>(user, _serializerOptions);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                    Debug.WriteLine("Ok");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

        }

        public static async Task<bool> Login(string email, string password)
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync(Constants.RestUrl);
                if (response.IsSuccessStatusCode)
                {
                    // Читаем содержимое ответа
                    List<User> users = await response.Content.ReadFromJsonAsync<List<User>>();
                    foreach (var user in users)
                        if (user.Email == email && user.Password == password)
                            return true;
                }
                else
                {
                    Debug.WriteLine($"HTTP Error: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
            return false;
        }

        public static string IsGoodPassword(string password)
        {
            if(password is null)
                password = "";  
                List<string> recommendations = new List<string>();
            // Рекомендовать повысить длину пароля, если он менее 8 символов
            if (password.Length < 8)
                recommendations.Add("Увеличьте длину пароля (минимум 8 символов).");
            // Рекомендовать добавить цифры, если их нет в пароле
            if (!password.Any(char.IsDigit))
                recommendations.Add("Добавьте цифры в пароль.");
            // Рекомендовать добавить буквы в верхнем регистре, если их нет в пароле
            if (!password.Any(char.IsUpper))
                recommendations.Add("Добавьте буквы в верхнем регистре в пароль.");
            // Рекомендовать добавить буквы в нижнем регистре, если их нет в пароле
            if (!password.Any(char.IsLower))
                recommendations.Add("Добавьте буквы в нижнем регистре в пароль.");
            return String.Join(" ", recommendations);
        }

    }
}
