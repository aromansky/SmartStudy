using System.Xml.Serialization;
using SmartStudy.ModelsDB;

namespace SmartStudy.Models
{
    static class Serializer
    {
        static string path = FileSystem.Current.AppDataDirectory;
        static string directory = Path.Combine(path, "Events");
        public static void SerializeUser(User user)
        {
            if (user == null)
                return;

                XmlSerializer serializer = new XmlSerializer(typeof(User));
                using (FileStream fs = new FileStream(Path.Combine(path, "UserData.xml"), FileMode.Create))
                    serializer.Serialize(fs, user);
        }

        public static User DeserializeUser()
        {
            //try
            //{
                XmlSerializer serializer = new XmlSerializer(typeof(User));
                using (FileStream fs = new FileStream(Path.Combine(path, "UserData.xml"), FileMode.Open))
                    return (User)serializer.Deserialize(fs);
            //}
            // надо чем-нибудь запомнить
            //catch
            //{
            //    return null;
            //}

        }

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

        public static List<Event> DeserializeEvents()
        {
            try
            {
                List<Event> events = new List<Event>();
                XmlSerializer serializer = new XmlSerializer(typeof(Event));
                foreach(string dir in Directory.GetFiles(directory))
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
