using System.Xml.Serialization;
using SmartStudy.ModelsDB;

namespace SmartStudy.Models
{
    static class Serializer
    {
        static string path = FileSystem.Current.AppDataDirectory;
        static string directory = Path.Combine(path, "Events");

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
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
            XmlSerializer serializer = new XmlSerializer(typeof(Event));
            using (FileStream fs = new FileStream(Path.Combine(directory, "Event" + @event.event_id + ".xml"), FileMode.Create))
                serializer.Serialize(fs, @event);
        }


        /// <summary>
        /// Десериализует объект класса Event из файла Event[num].xml
        /// </summary>
        /// <returns></returns>
        public static List<Event> DeserializeEvents()
        {
            try
            {
                List<Event> events = new List<Event>();
                XmlSerializer serializer = new XmlSerializer(typeof(Event));
                foreach (string dir in Directory.GetFiles(directory))
                    using (FileStream fs = new FileStream(dir, FileMode.Open))
                        events.Add((Event)serializer.Deserialize(fs));
                return events;
            }
            // надо чем-нибудь запомнить
            catch
            {
                return null;
            }

        }
    }
}
