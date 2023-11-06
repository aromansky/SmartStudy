using MySql.Data.MySqlClient;
using System.Configuration;


namespace SmartStudy
{
    static class Client
    {
        static MySqlConnection connect = new MySqlConnection(ConfigurationManager.ConnectionStrings["TestDB"].ConnectionString);

        public static void Register(string FirstName, string LastName, string Email, string Password)
        {
            connect.Open();

            MySqlCommand command = new MySqlCommand(
                "INSERT INTO user (FirstName, LastName, Email, Password)" +
                $"VALUES (@FirstName, @LastName, @Email, @Password)", connect
                );

            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@Email", Email);
            command.Parameters.AddWithValue("@Password", Password);

            command.ExecuteNonQuery();


            connect.Close();
        }
    }
}
