using System;
using System.Diagnostics;
using System.Xml.Serialization;
using SmartStudy.ModelsDB;

namespace SmartStudy.Models
{
    static class Serializer
    {
        static string path = FileSystem.Current.AppDataDirectory;

        public static void SerializeUser(User user)
        {
            if (user == null)
                return;

                XmlSerializer serializer = new XmlSerializer(typeof(User));
                Debug.WriteLine(Path.Combine(path, "UserData.xml"));
                using (FileStream fs = new FileStream(Path.Combine(path, "UserData.xml"), FileMode.Create))
                    serializer.Serialize(fs, user);

            
        }

        public static User DeserializeUser()
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(User));
                using (FileStream fs = new FileStream(Path.Combine(path, "UserData.xml"), FileMode.Open))
                    return (User)serializer.Deserialize(fs);
            }
            // надо чем-нибудь запомнить
            catch
            {
                return null;
            }

        }
    }
}
