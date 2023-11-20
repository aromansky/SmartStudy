using System.Text.Json;
using System.Text;
using System.Diagnostics;
using System.Net.Http.Json;
using SmartStudy.Models;
using SmartStudy.ModelsDB;

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

        public static async void Register(User user)
        {
            Uri uri = new Uri(string.Format(Constants.UserUrl, string.Empty));

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

        public static async Task<bool> Login(string email, string password, string role)
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync(Constants.UserUrl);
                if (response.IsSuccessStatusCode)
                {
                    // Читаем содержимое ответа
                    List<User> users = await response.Content.ReadFromJsonAsync<List<User>>();
                    foreach (var user in users)
                        if (user.Email == email && user.Password == password)
                        {
                            user.Role = role;
                            Serializer.SerializeUser(user);
                            return true;
                        }
                            
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
            List<string> recommendations = new List<string>();
            if (password is null)
                password = "";  
            if (password.Length < 8)
                recommendations.Add("Увеличьте длину пароля (минимум 8 символов).");
            if (!password.Any(char.IsDigit))
                recommendations.Add("\nДобавьте цифры в пароль.");
            if (!password.Any(char.IsUpper))
                recommendations.Add("\nДобавьте буквы в верхнем регистре в пароль.");
            if (!password.Any(char.IsLower))
                recommendations.Add("\nДобавьте буквы в нижнем регистре в пароль.");
            return String.Join(" ", recommendations);
        }

        public static async void CreateGroupSettings(group_settings settings)
        {
            Uri uri = new Uri(string.Format(Constants.GroupSettingsUrl, string.Empty));

            try
            {
                string json = JsonSerializer.Serialize<group_settings>(settings, _serializerOptions);
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

        public static async Task<List<User>> GetUsersList()
        {
            List<User> users = new List<User>();
            try
            {
                HttpResponseMessage response = await _client.GetAsync(Constants.UserUrl);
                if (response.IsSuccessStatusCode)
                    users = await response.Content.ReadFromJsonAsync<List<User>>();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
            return users;
        }
        public static async void CreateGroup(ModelsDB.Group group)
        {
            Uri uri = new Uri(string.Format(Constants.GroupUrl, string.Empty));

            try
            {
                string json = JsonSerializer.Serialize<ModelsDB.Group>(group, _serializerOptions);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                Debug.WriteLine(json);

                HttpResponseMessage response = await _client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                    Debug.WriteLine("Ok");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
        }

        public static async void AddUserToGroup(group_settings g_s, params User[] users)
        {
            Uri uri = new Uri(string.Format(Constants.GroupUrl, string.Empty));
            foreach(User user in users)
            {
                try
                {
                    string json = JsonSerializer.Serialize<ModelsDB.Group>(new ModelsDB.Group(g_s.group_settings_id, user.user_id),
                        _serializerOptions);
                    StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                    Debug.WriteLine(json);

                    HttpResponseMessage response = await _client.PostAsync(uri, content);

                    if (response.IsSuccessStatusCode)
                        Debug.WriteLine("Ok");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(@"\tERROR {0}", ex.Message);
                }

            }
        }
    }
}
