using System.Text.Json;
using System.Text;
using System.Diagnostics;
using System.Net.Http.Json;
using SmartStudy.ModelsDB;
using SmartStudy.Models;
using Microsoft.Extensions.Logging;
using SmartStudy.Views.Student;

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
        public static async Task<bool> CreateGroupSettings(group_settings settings)
        {
            Uri uri = new Uri(string.Format(Constants.GroupSettingsUrl, string.Empty));

            try
            {
                string json = JsonSerializer.Serialize<group_settings>(settings, _serializerOptions);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                    Debug.WriteLine("Ok");

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                return false;
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
        }


        /// <summary>
        /// Удаляет запись group_settings из БД
        /// </summary>
        /// <param name="group_settings_id">Id группы</param>
        public static async void DeleteGroupSettings(long group_settings_id)
        {
            try
            {
                HttpResponseMessage response = await _client.DeleteAsync(Constants.GroupSettingsUrl + $"/{group_settings_id}");
            }
            catch (Exception ex)
            {
                //
            }
        }

        /// <summary>
        /// Редактирует запись об group_settings-е из БД
        /// </summary>
        /// <param name="id">id Event-а</param>
        public static async void EditGroupFromId(long editing_Id, group_settings g_s)
        {
            try
            {
                string json = JsonSerializer.Serialize<group_settings>(g_s, _serializerOptions);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _client.PutAsync(Constants.GroupSettingsUrl + $"/{editing_Id}", content);
            }
            catch (Exception ex)
            {
                //
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
                string url = Constants.EventUrl + $"/{id}";
                HttpResponseMessage response = await _client.GetAsync(url);
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
            if (!user.IsTutor() || (user.user_id != @event.author_id))
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
        public static async void AddEventToGroup(Event @event, params group_settings[] g_s)
        {
            Uri uri = new Uri(string.Format(Constants.GroupEventUrl, string.Empty));

            try
            {
                foreach (var i in g_s)
                {
                    string json = JsonSerializer.Serialize<group_event>(new group_event(@event.event_id, i.group_settings_id), _serializerOptions);
                    StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await _client.PostAsync(uri, content);

                    if (response.IsSuccessStatusCode)
                        Debug.WriteLine("Ok");
                }
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
        /// Возвращает список групп с указанным event-ом
        /// </summary>
        public static async Task<List<group_settings>> GetGroupsWithEvent(long event_id)
        {
            List<group_settings> group_Settings = new List<group_settings>();
            try
            {
                HttpResponseMessage response = await _client.GetAsync(Constants.GroupSettingsUrl + $"/event-{event_id}");
                if (response.IsSuccessStatusCode)
                    group_Settings = await response.Content.ReadFromJsonAsync<List<group_settings>>();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
            return group_Settings;
        }

        /// <summary>
        /// Возвращает группу с указанным id
        /// </summary>
        public static async Task<group_settings> GetGroupWithId(long group_settings_id)
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync(Constants.GroupSettingsUrl + $"/{group_settings_id}");
                if (response.IsSuccessStatusCode)
                    return await response.Content.ReadFromJsonAsync<group_settings>();
                else 
                    return null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Возвращает список групп с указанным tutor-ом
        /// </summary>
        public static async Task<List<group_settings>> GetGroupsWithTutor(long tutor_id)
        {
            List<group_settings> group_Settings = new List<group_settings>();
            try
            {
                HttpResponseMessage response = await _client.GetAsync(Constants.GroupSettingsUrl + $"/tutor-{tutor_id}");
                if (response.IsSuccessStatusCode)
                    group_Settings = await response.Content.ReadFromJsonAsync<List<group_settings>>();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
            return group_Settings;
        }

        /// <summary>
        /// Возвращает список объектов Event, в которых учавствует пользователь
        /// </summary>
        /// <param name="user">Пользвователь</param>
        /// <returns>Список event-ов</returns>
        public static async Task<List<Event>> GetEventsWithUser(User user)
        {
            List<Event> events = new List<Event>();
            try
            {
                HttpResponseMessage response;
                if (user.IsTutor())
                {
                    response = await _client.GetAsync(Constants.EventUrl + $"/author-{user.user_id}");
                    if (response.IsSuccessStatusCode)
                        events = await response.Content.ReadFromJsonAsync<List<Event>>();
                }
                else
                {
                    response = await _client.GetAsync(Constants.EventUrl + $"/user-{user.user_id}");
                    if (response.IsSuccessStatusCode)
                        events = await response.Content.ReadFromJsonAsync<List<Event>>();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
            return events;
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
        /// Возвращает список пользователей, которым доступно дз
        /// </summary>
        /// <param name="homework_id">id дз</param>
        /// <returns></returns>
        public static async Task<List<User>> GetUsersWithHomework(long homework_id)
        {
            List<User> users = new List<User>();
            try
            {
                HttpResponseMessage response = await _client.GetAsync(Constants.UserUrl + $"/users_with_hw-{homework_id}");
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
        /// Возвращает список с информацией о зарегистрированных пользователях, состоящих в указанной группе
        /// </summary>
        /// <param name="group_settings_id">id группы</param>
        /// <returns></returns>
        public static async Task<List<User>> GetUsersFromGroup(long group_settings_id)
        {
            List<User> users = new List<User>();
            try
            {
                HttpResponseMessage response = await _client.GetAsync(Constants.UserUrl + $"/group-{group_settings_id}");
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
        /// Удаляет пользователя из группы
        /// </summary>
        /// <param name="group_settings_id">id группы</param>
        /// <param name="user_id">id пользователя</param>
        public static async Task<bool> RemoveUserFromGroup(long group_settings_id, long user_id)
        {
            try
            {
                HttpResponseMessage response = await _client.DeleteAsync(Constants.GroupUrl + $"/{group_settings_id}_{user_id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        /// <summary>
        ///  Возвращает список объектов group_settings, в которых состоит user
        /// </summary>
        /// <param name="user">User</param>
        /// <returns>Список объектов group_settings, в которых состоит user</returns>
        public static async Task<List<group_settings>> GetGroupListWithUser(long user_id)
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
            return groupsSettings.Where(x => groups.Where(x => x.user_id == user_id).Select(x => x.group_settings_id).Distinct().Contains(x.group_settings_id)).ToList();
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
        public static async Task<bool> AddUsersToGroup(long group_settings_id, params long[] users_id)
        {
            Uri uri = new Uri(string.Format(Constants.GroupUrl, string.Empty));
            foreach (long user_id in users_id)
            {
                try
                {
                    string json = JsonSerializer.Serialize<Group>(new Group(group_settings_id, user_id),
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
            return true;
        }


        /// <summary>
        /// Возвращает дз, доступное пользователю
        /// </summary>
        /// <param name="user_id">id пользователя</param>
        /// <returns></returns>
        public static async Task<List<homework>> GetUserHomework(long user_id)
        {
            List<homework> homeworks = new List<homework>();
            try
            {
                HttpResponseMessage response = await _client.GetAsync(Constants.UserUrl + $"/homework_user-{user_id}");
                if (response.IsSuccessStatusCode)
                    homeworks = await response.Content.ReadFromJsonAsync<List<homework>>();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
            return homeworks;
        }


        /// <summary>
        /// Возвращает дз, доступное группе
        /// </summary>
        /// <param name="group_settings_id">id группы</param>
        /// <returns></returns>
        public static async Task<List<homework>> GetGroupHomework(long group_settings_id)
        {
            List<homework> homeworks = new List<homework>();
            try
            {
                HttpResponseMessage response = await _client.GetAsync(Constants.GroupSettingsUrl + $"/homework-{group_settings_id}");
                if (response.IsSuccessStatusCode)
                    homeworks = await response.Content.ReadFromJsonAsync<List<homework>>();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
            return homeworks;
        }


        /// <summary>
        /// Возвращает дз, созданное пользователем
        /// </summary>
        /// <param name="author_id">id автора</param>
        /// <returns></returns>
        public static async Task<List<homework>> GetHomeworkForAuthor(long author_id)
        {
            List<homework> homeworks = new List<homework>();
            try
            {
                HttpResponseMessage response = await _client.GetAsync(Constants.UserUrl + $"/homework_author-{author_id}");
                if (response.IsSuccessStatusCode)
                    homeworks = await response.Content.ReadFromJsonAsync<List<homework>>();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
            return homeworks;
        }



        /// <summary>
        /// Возвращает группы, которым доступно дз
        /// </summary>
        /// <param name="homerwork_id">id домашнего задания</param>
        /// <returns></returns>
        public static async Task<List<group_settings>> GetGroupsWithHomework(long homerwork_id)
        {
            List<group_settings> group_settings = new List<group_settings>();
            try
            {
                HttpResponseMessage response = await _client.GetAsync(Constants.GroupUrl + $"/groups_with_homework-{homerwork_id}");
                if (response.IsSuccessStatusCode)
                    group_settings = await response.Content.ReadFromJsonAsync<List<group_settings>>();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
            return group_settings;
        }


        /// <summary>
        /// Получает объект homework по его homework_id
        /// </summary>
        /// <param name="id">id homework-а</param>
        public static async Task<homework> GetHomeworkFromId(long id)
        {
            homework homework = null;
            try
            {
                string url = Constants.HomeworkUrl + $"/{id}";
                HttpResponseMessage response = await _client.GetAsync(url);
                homework = await response.Content.ReadFromJsonAsync<homework>();
            }
            catch (Exception ex)
            {
                //
            }
            return homework;
        }

        /// <summary>
        /// Создаёт в таблице homework запись, соответствующую объекту класса homework
        /// </summary>
        /// <param name="homework">Объект класса homework</param>
        public static async void CreateHomework(homework homework)
        {
            Uri uri = new Uri(string.Format(Constants.HomeworkUrl, string.Empty));

            try
            {
                string json = JsonSerializer.Serialize<homework>(homework, _serializerOptions);
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
        /// Редактирует запись о homework из БД
        /// </summary>
        /// <param name="editing_Id">id homework-а</param>
        public static async void EditHomeworkFromId(long editing_Id, homework homework)
        {
            User user = Serializer.DeserializeUser();
            homework.homework_id = editing_Id;
            if (!user.IsTutor() || (user.user_id != homework.author_id))
                return;
            try
            {
                string json = JsonSerializer.Serialize<homework>(homework, _serializerOptions);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _client.PutAsync(Constants.HomeworkUrl + $"/{editing_Id}", content);
            }
            catch (Exception ex)
            {
                //
            }
        }


        /// <summary>
        /// Удаляет запись homework из БД
        /// </summary>
        /// <param name="homework_id">Id домашнего задания</param>
        public static async Task<bool> DeleteHomework(long homework_id)
        {
            try
            {
                HttpResponseMessage response = await _client.DeleteAsync(Constants.HomeworkUrl + $"/{homework_id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                return false;
                //
            }
        }

        /// <summary>
        /// Открепляет дз от группы
        /// </summary>
        /// <param name="homework_id">id группы</param>
        /// <param name="group_settings_id">id домашнего задания</param>
        public static async Task<bool> DeleteGroupHomework(long homework_id, long group_settings_id)
        {
            try
            {
                HttpResponseMessage response = await _client.DeleteAsync(Constants.GroupHomeworkUrl + $"/{homework_id}_{group_settings_id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Открепляет дз от пользователя
        /// </summary>
        /// <param name="homework_id">id группы</param>
        /// <param name="user_id">id пользователя</param>
        public static async Task<bool> DeleteUserHomework(long homework_id, long user_id)
        {
            try
            {
                HttpResponseMessage response = await _client.DeleteAsync(Constants.UserHomeworkUrl + $"/{homework_id}_{user_id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        /// <summary>
        /// Отправляет homework пользователям
        /// </summary>
        /// <param name="homework_id">Id домашнего задания</param>
        /// <param name="users_id">Id пользователей, котрым отсылается дз</param>
        public static async Task<bool> CreateUserHomework(long homework_id, params long[] users_id)
        {
            Uri uri = new Uri(string.Format(Constants.UserHomeworkUrl, string.Empty));

            try
            {
                foreach(long u_id in users_id)
                {
                    user_homework user_homework = new user_homework(homework_id, u_id);
                    string json = JsonSerializer.Serialize<user_homework>(user_homework, _serializerOptions);
                    StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await _client.PostAsync(uri, content);

                    if (response.IsSuccessStatusCode)
                        Debug.WriteLine("Ok");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
            return true;
        }

        /// <summary>
        /// Отправляет homework в группу
        /// </summary>
        /// <param name="homework_id">Id домашнего задания</param>
        /// <param name="group_settings_id">Id групп, котрым отсылается дз</param>
        public static async Task<bool> CreateGroupHomework(long homework_id, params long[] group_settings_id)
        {
            Uri uri = new Uri(string.Format(Constants.GroupHomeworkUrl, string.Empty));

            try
            {
                foreach (long gs_id in group_settings_id)
                {
                    group_homework group_homework = new group_homework(homework_id, gs_id);
                    string json = JsonSerializer.Serialize<group_homework>(group_homework, _serializerOptions);
                    StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await _client.PostAsync(uri, content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
            return true;
        }

        /// <summary>
        /// Создаёт в таблице feedback запись, соответствующую объекту класса feedback
        /// </summary>
        /// <param name="feedback">Объект класса feedback</param>
        public static async void CreateFeedback(feedback feedback)
        {
            Uri uri = new Uri(string.Format(Constants.FeedbackUrl, string.Empty));

            try
            {
                string json = JsonSerializer.Serialize<feedback>(feedback, _serializerOptions);
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
        /// Редактирует запись о feedback из БД
        /// </summary>
        /// <param name="editing_Id">id feedback-а</param>
        public static async void EditFeedbackFromId(long editing_Id, feedback feedback)
        {
            User user = Serializer.DeserializeUser();
            feedback.feedback_id = editing_Id;
            if (!user.IsTutor() || (user.user_id != feedback.author_id))
                return;
            try
            {
                string json = JsonSerializer.Serialize<feedback>(feedback, _serializerOptions);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _client.PutAsync(Constants.FeedbackUrl + $"/{editing_Id}", content);
            }
            catch (Exception ex)
            {
                //
            }
        }

        /// <summary>
        /// Удаляет запись feedback из БД
        /// </summary>
        /// <param name="feedback_id">Id фидбека</param>
        public static async Task<bool> DeleteFeedback(long feedback_id)
        {
            try
            {
                HttpResponseMessage response = await _client.DeleteAsync(Constants.FeedbackUrl + $"/{feedback_id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                return false;
                //
            }
        }

        /// <summary>
        /// Отправляет фидбек пользователям
        /// </summary>
        /// <param name="feedback_id">Id фидбека</param>
        /// <param name="users_id">Id пользователей, котрым отсылается фидбек</param>
        public static async void SendFeedbackToUsers(long feedback_id, params long[] users_id)
        {
            Uri uri = new Uri(string.Format(Constants.UserFeedbackUrl, string.Empty));

            try
            {
                foreach (long u_id in users_id)
                {
                    user_feedback user_feedback = new user_feedback(feedback_id, u_id);
                    string json = JsonSerializer.Serialize<user_feedback>(user_feedback, _serializerOptions);
                    StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await _client.PostAsync(uri, content);

                    if (response.IsSuccessStatusCode)
                        Debug.WriteLine("Ok");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
        }

        /// <summary>
        /// Отправляет фидбек пользователям группы
        /// </summary>
        /// <param name="feedback_id">Id домашнего задания</param>
        /// <param name="group_settings_id">Id группы</param>
        public static async void SendFeedbackToGroup(long feedback_id, long group_settings_id)
        {
            List<User> usersInGroup = await GetUsersFromGroup(group_settings_id);
            SendFeedbackToUsers(feedback_id, usersInGroup.Select(x => x.user_id).ToArray());
        }


        /// <summary>
        /// Возвращает фидбек, созданный пользователем
        /// </summary>
        /// <param name="author_id">id автора</param>
        /// <returns></returns>
        public static async Task<List<feedback>> GetFeedbackForAuthor(long author_id)
        {
            List<feedback> feedbacks = new List<feedback>();
            try
            {
                HttpResponseMessage response = await _client.GetAsync(Constants.UserUrl + $"/feedback_author-{author_id}");
                if (response.IsSuccessStatusCode)
                    feedbacks = await response.Content.ReadFromJsonAsync<List<feedback>>();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
            return feedbacks;
        }

        /// <summary>
        /// Возвращает фидбек, доступный пользователю
        /// </summary>
        /// <param name="user_id">id автора</param>
        /// <returns></returns>
        public static async Task<List<feedback>> GetFeedbackForUser(long user_id)
        {
            List<feedback> feedbacks = new List<feedback>();
            try
            {
                HttpResponseMessage response = await _client.GetAsync(Constants.UserUrl + $"/feedback_user-{user_id}");
                if (response.IsSuccessStatusCode)
                    feedbacks = await response.Content.ReadFromJsonAsync<List<feedback>>();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
            return feedbacks;
        }


        /// <summary>
        /// Возвращает id последней созданной группы
        /// </summary>
        /// <returns>id группы</returns>
        public static async Task<long> GetLastCreatedGroupId()
        {
            List<group_settings> g_s = await GetGroupsWithTutor(Serializer.DeserializeUser().user_id);
            return g_s.Last().group_settings_id;
        }
    }
}
