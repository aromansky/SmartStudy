using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace SmartStudy
{
    public static class Constants
    {
        public const string UserUrl = "https://localhost:7095/api/user";
        public const string GroupSettingsUrl = "https://<ввести номер своего хоста>/api/group_settings";
        public const string GroupUrl = "https://<ввести номер своего хоста>/api/group";
        public const string EventUrl = "https://<ввести номер своего хоста>/api/enent";
        public const string GroupEventUrl = "https://<ввести номер своего хоста>/api/group_event";
    }
}
