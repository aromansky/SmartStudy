using SmartStudy.ModelsDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartStudy
{
    public class Group
    {
        public int Id { get; }
        public int Group_settings_id { get; set; }
        public List<User> Users { get; set; } 
        public List<Event> Events { get; set; }
        
        public Group(int id, int group_settings_id)
        {
            Id = id;
            Group_settings_id = group_settings_id;
            Users = new List<User>();
            Events = new List<Event>();
        }   

        public void AddUser(User user)
        {
            Users.Add(user);
        }

        public void AddEvent(Event _event)
        {
            Events.Add(_event);
        }


        public override string ToString()
        {
            string res = $"Id: {Id}\nGroup_settings_id: {Group_settings_id}\nUsers:";
            for(int i = 0; i < Users.Count; i++)
            {
                res += Users[i].ToString() + '\n';
            }
            res += "Events:\n";
            for(int i = 0; i < Events.Count; i++)
            {
                res += Events[i].ToString() + '\n';
            }   

            return res;
        }
    }
}
