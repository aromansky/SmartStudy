﻿using SmartStudy.ModelsDB;
using System.Collections.ObjectModel;

namespace SmartStudy.Models
{
    class Group_note
    {
        public ObservableCollection<group_settings> Groups { get; set; } = new ObservableCollection<group_settings>();
        public ObservableCollection<object> SelectedGroups { get; set; } = new ObservableCollection<object>();
        public ObservableCollection<group_settings> GroupsWithEvent { get; set; } = new ObservableCollection<group_settings>();
        public ObservableCollection<group_settings> GroupsWithUser { get; set; } = new ObservableCollection<group_settings>();

        public Group_note() => Load_All_Groups();
        public async void Load_All_Groups()
        {
            List<group_event> group_with_event = await Client.GetGroupEventList();
            List<group_settings> grps = await Client.GetGroupsWithTutor(Serializer.DeserializeUser().user_id);
            Groups.Clear();
            foreach (group_settings grp in grps.Where(x => !group_with_event.Select(x => x.group_settings_id).Contains(x.group_settings_id)))
                Groups.Add(grp);
            Groups.OrderBy(x => x.Title);
        }
        public async void Load_Groups_With_Event(long event_id)
        {
            GroupsWithEvent.Clear();
            List<group_settings> g_s = await Client.GetGroupsWithEvent(event_id);
            foreach (group_settings g in g_s.OrderBy(x => x.Title))
                GroupsWithEvent.Add(g);
        }

        public async void Load_Groups_With_User()
        {
            User user = Serializer.DeserializeUser();
            if (user.IsTutor())
            {
                List<group_settings> g_s = await Client.GetGroupsWithTutor(user.user_id);
                GroupsWithUser.Clear();
                foreach (group_settings g in g_s.OrderBy(x => x.Title))
                    GroupsWithUser.Add(g);
            }
            else
            {
                List<group_settings> g_s = await Client.GetGroupListWithUser(user.user_id);
                GroupsWithUser.Clear();
                foreach (group_settings g in g_s.OrderBy(x => x.Title))
                    GroupsWithUser.Add(g);
            }
        }
    }
}
