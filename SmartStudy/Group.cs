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
        public long group_id { get; set; }
        public long group_settings_id { get; set; }
        public long user_id { get; set; }

        public Group(long group_id, long group_settings_id, long user_id)
        {
            this.group_id = group_id;
            this.group_settings_id = group_settings_id;
            this.user_id = user_id;
        }   
        
        public override string ToString()
        {
            return $"group_id: {group_id}, group_settings_id: {group_settings_id}, user_id: {user_id}";
        }
    }
}
