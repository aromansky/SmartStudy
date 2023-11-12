//using MySql.Data.MySqlClient;
using System.Configuration;
using System.Text.Json;
using SmartStudy.Models;
using System.Text;
using System.Diagnostics;

namespace SmartStudy
{
    static class Client
    {
       //static MySqlConnection _connect = new MySqlConnection(ConfigurationManager.ConnectionStrings["TestDB"].ConnectionString);
        private static HttpClient _client = new HttpClient();
        private static JsonSerializerOptions _serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };

        public static async void Register(string FirstName, string LastName, string Email, string Password)
        {

            User user = new User(FirstName, LastName, Email, Password);
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
            

            //_connect.Open();

            //MySqlCommand command = new MySqlCommand(
            //    "INSERT INTO user (FirstName, LastName, Email, Password)" +
            //    $"VALUES (@FirstName, @LastName, @Email, @Password)", _connect
            //    );

            //command.Parameters.AddWithValue("@FirstName", FirstName);
            //command.Parameters.AddWithValue("@LastName", LastName);
            //command.Parameters.AddWithValue("@Email", Email);
            //command.Parameters.AddWithValue("@Password", Password);

            //command.ExecuteNonQuery();


            //_connect.Close();
        }

    }
}
