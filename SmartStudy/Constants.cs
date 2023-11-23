﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace SmartStudy
{
    public static class Constants
    {
        // a22268-0fcc.b.d-f.pw
        private const string your_address = "localhost:7095";
        public const string UserUrl = $"https://{your_address}/api/user";
        public const string GroupSettingsUrl = $"https://{your_address}/api/group_settings";
        public const string GroupUrl = $"https://{your_address}/api/group";
        public const string EventUrl = $"https://{your_address}/api/event";
        public const string GroupEventUrl = $"https://{your_address}/api/group_event";
        public const string EventUserUrl = $"https://{your_address}/api/event_user";
    }
}