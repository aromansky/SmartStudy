using System.Xml.Serialization;
using SmartStudy.ModelsDB;

namespace SmartStudy.Models
{
    static class Serializer
    {
        static string path = FileSystem.Current.AppDataDirectory;

        /// <summary>
        /// Сериализует объект класса User в файл UserData.xml
        /// </summary>
        /// <param name="user">Объект класса User</param>
        public static void SerializeUser(User user)
        {
            if (user == null)
                return;

            XmlSerializer serializer = new XmlSerializer(typeof(User));
            using (FileStream fs = new FileStream(Path.Combine(path, "UserData.xml"), FileMode.Create))
                serializer.Serialize(fs, user);
        }

        /// <summary>
        /// Десериализует объект класса User из файла UserData.xml
        /// </summary>
        /// <returns></returns>
        public static User DeserializeUser()
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(User));
                using (FileStream fs = new FileStream(Path.Combine(path, "UserData.xml"), FileMode.Open))
                    return (User)serializer.Deserialize(fs);
            }
            //надо чем-нибудь запомнить
            catch
            {
                return null;
            }

        }


        /// <summary>
        /// Сериализует объект класса Event в файл Event[num].xml
        /// </summary>
        /// <param name="@event">Объект класса Event</param>
        public static void SerializeEvent(Event @event)
        {
            if (@event == null)
                return;
            XmlSerializer serializer = new XmlSerializer(typeof(Event));
            using (FileStream fs = new FileStream(Path.Combine(path, "Event.xml"), FileMode.Create))
                serializer.Serialize(fs, @event);
        }


        /// <summary>
        /// Десериализует объект класса Event из файла Event[num].xml
        /// </summary>
        /// <returns></returns>
        public static Event DeserializeEvent()
        {
            try
            {
                Event @event;
                XmlSerializer serializer = new XmlSerializer(typeof(Event));
                using (FileStream fs = new FileStream(Path.Combine(path, "Event.xml"), FileMode.Open))
                    @event = (Event)serializer.Deserialize(fs);
                //File.Delete(path);
                return @event;
            }
            // надо чем-нибудь запомнить
            catch
            {
                return null;
            }

        }
        /// <summary>
        /// Удаление данных о предыдущем входе из памяти
        /// </summary>
        public static void DeleteUserData()
        {
            try
            {
                File.Delete(Path.Combine(path, "UserData.xml"));
            }
            catch
            {
                throw new FileNotFoundException();
            }
            
        }
    }
}
