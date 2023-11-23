using System.Text.Json;
using System.Text;
using System.Diagnostics;
using System.Net.Http.Json;
using SmartStudy.ModelsDB;
using SmartStudy.Models;
using Microsoft.Extensions.Logging;

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

         /// <summary>
         /// Выполняет регистрацию пользователя. Делает соответствующую запись в таблице user 
         /// </summary>
         /// <param name="user">Объект класса User</param>
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

        /// <summary>
        /// Выполняет вход в аккаунт пользователя.
        /// </summary>
        /// <param name="email">EMail пользователя</param>
        /// <param name="password">Пароль пользователя</param>
        /// <param name="role">Роль пользователя</param>
        /// <returns>True, если вход возможен. False - иначе</returns>
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

        /// <summary>
        /// Возвращает строку с рекомендациями по улучшению пароля
        /// </summary>
        /// <param name="password">Переданный пароль</param>
        /// <returns></returns>
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


        /// <summary>
        /// Создаёт в таблице group_settings запись, соответствующую объекту класса group_settings
        /// </summary>
        /// <param name="settings">Объект класса group_settings</param>
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

        /// <summary>
        /// Создаёт в таблице event запись, соответствующую объекту класса Event
        /// </summary>
        /// <param name="event">Объект класса Event</param>
        public static async void CreateEvent(Event @event)
        {
            Uri uri = new Uri(string.Format(Constants.EventUrl, string.Empty));

            try
            {
                string json = JsonSerializer.Serialize<Event>(@event, _serializerOptions);
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

        /// <summary>
        /// Получает объект Event по его event_id
        /// </summary>
        /// <param name="id">id Event-а</param>
        public static async Task<Event> GetEventFromId(long id)
        {
            Event @event = null;
            try
            {
                HttpResponseMessage response = await _client.GetAsync(Constants.EventUrl + $"/{id}");
                @event = await response.Content.ReadFromJsonAsync<Event>();
            }
            catch (Exception ex)
            {
                //
            }
            return @event;
        }

        /// <summary>
        /// Удаляет запись об Event-е из БД
        /// </summary>
        /// <param name="id">id Event-а</param>
        public static async void DeleteEventFromId(long event_id)
        {
            User user = Serializer.DeserializeUser();
            Event @event = await GetEventFromId(event_id);
            if (/*!user.IsTutor() ||*/ (user.user_id != @event.author_id))
                return;
            try
            {
                HttpResponseMessage response = await _client.DeleteAsync(Constants.EventUrl + $"/{@event.event_id}");
            }
            catch (Exception ex)
            {
                //
            }
        }

        /// <summary>
        /// Редактирует запись об Event-е из БД
        /// </summary>
        /// <param name="id">id Event-а</param>
        public static async void EditEventFromId(long editing_Id, Event @event)
        {
            User user = Serializer.DeserializeUser();
            @event.event_id = editing_Id;
            if (/*!user.IsTutor() ||*/ (user.user_id != @event.author_id))
                return;
            try
            {
                string json = JsonSerializer.Serialize<Event>(@event, _serializerOptions);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _client.PutAsync(Constants.EventUrl + $"/{editing_Id}", content);
            }
            catch (Exception ex)
            {
                //
            }
        }

        /// <summary>
        /// Добавляет event к указанной группе. Создаёт об этом запись в таблицу group_event
        /// </summary>
        /// <param name="event">Объект класса Event</param>
        /// <param name="g_s">Объект класса group_settings</param>
        public static async void AddEventToGroup(Event @event, group_settings g_s)
        {
            Uri uri = new Uri(string.Format(Constants.GroupEventUrl, string.Empty));

            try
            {
                string json = JsonSerializer.Serialize<group_event>(new group_event(@event.event_id, g_s.group_settings_id), _serializerOptions);
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

        /// <summary>
        /// Возвращает список объектов group_event
        /// </summary>
        public static async Task<List<group_event>> GetGroupEventList()
        {
            List<group_event> events = new List<group_event>();
            try
            {
                HttpResponseMessage response = await _client.GetAsync(Constants.GroupEventUrl);
                if (response.IsSuccessStatusCode)
                    events = await response.Content.ReadFromJsonAsync<List<group_event>>();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
            return events;
        }

        /// <summary>
        /// Возвращает списко объектов Event, в которых учавствует пользователь
        /// </summary>
        /// <param name="user">Пользвователь</param>
        /// <returns>Список event-ов</returns>
        public static async Task<List<Event>> GetEventList(User user)
        {
            var eventUsers = new List<EventUser>();
            List<Event> events = new List<Event>();
            try
            {
                HttpResponseMessage response = await _client.GetAsync(Constants.EventUserUrl + $"/{user.user_id}");
                if (response.IsSuccessStatusCode)
                    eventUsers = await response.Content.ReadFromJsonAsync<List<EventUser>>();

                response = await _client.GetAsync(Constants.EventUrl);
                if (response.IsSuccessStatusCode)
                    events = await response.Content.ReadFromJsonAsync<List<Event>>();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
            return events.Where(x => eventUsers.Select(x => x.event_id).Contains(x.event_id)).ToList();
        }

        /// <summary>
        /// Возвращает список с информацией о зарегистрированных пользователях
        /// </summary>
        /// <returns>Список List<User></returns>
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


        /// <summary>
        ///  Возвращает список объектов group_settings, в которых состоит user
        /// </summary>
        /// <param name="user">User</param>
        /// <returns>Список объектов group_settings, в которых состоит user</returns>
        public static async Task<List<group_settings>> GetGroupListWithUser(User user)
        {
            List<Group> groups = new List<Group>();
            List<group_settings> groupsSettings = new List<group_settings>();
            try
            {
                HttpResponseMessage responseGroup = await _client.GetAsync(Constants.GroupUrl);
                if (responseGroup.IsSuccessStatusCode)
                    groups = await responseGroup.Content.ReadFromJsonAsync<List<Group>>();

                HttpResponseMessage responsegroupsSettings = await _client.GetAsync(Constants.GroupSettingsUrl);
                if (responsegroupsSettings.IsSuccessStatusCode)
                    groupsSettings = await responsegroupsSettings.Content.ReadFromJsonAsync<List<group_settings>>();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
            return groupsSettings.Where(x => groups.Where(x => x.user_id == user.user_id).Select(x => x.group_settings_id).Distinct().Contains(x.group_settings_id)).ToList();
        }


        /// <summary>
        /// Возвращает список с информацией о созданных пользователем группах
        /// </summary>
        /// <returns>Список List<group_settings></returns>
        public static async Task<List<group_settings>> GetGroupList()
        {
            Uri uri = new Uri(string.Format(Constants.GroupUrl, string.Empty));
            try
            {
                List<group_settings> res = new List<group_settings>();
                HttpResponseMessage response = await _client.GetAsync(Constants.GroupSettingsUrl);

                if (response.IsSuccessStatusCode)
                    res = await response.Content.ReadFromJsonAsync<List<group_settings>>();
                return res.Where(x => x.Tutor_id == Serializer.DeserializeUser().user_id).ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Добавляет пользователей в указанную группу. Делает соответствующую запись в таблице group_event
        /// </summary>
        /// <param name="g_s">Объект класса group_settings</param>
        /// <param name="users">Массив пользователей</param>
        public static async void AddUserToGroup(group_settings g_s, params User[] users)
        {
            Uri uri = new Uri(string.Format(Constants.GroupUrl, string.Empty));
            foreach(User user in users)
            {
                try
                {
                    string json = JsonSerializer.Serialize<Group>(new Group(g_s.group_settings_id, user.user_id),
                        _serializerOptions);
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
        }
    }
}
