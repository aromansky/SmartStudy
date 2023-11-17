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
        private const string your_address = "";
        public const string UserUrl = $"https://{your_address}/api/user";
        public const string GroupSettingsUrl = $"https://{your_address}/api/group_settings";
        public const string GroupUrl = $"https://{your_address}/api/group";
        public const string EventUrl = $"https://{your_address}/api/event";
        public const string GroupEventUrl = $"https://{your_address}/api/group_event";
    }
}
