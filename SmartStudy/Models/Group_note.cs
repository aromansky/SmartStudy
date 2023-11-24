using SmartStudy.ModelsDB;
using System.Collections.ObjectModel;

namespace SmartStudy.Models
{
    class Group_note
    {
        public ObservableCollection<group_settings> Groups { get; set; } = new ObservableCollection<group_settings>();
        public ObservableCollection<object> SelectedGroups { get; set; } = new ObservableCollection<object>();

        public Group_note() => Load_All_Groups();
        public async void Load_All_Groups()
        {
            List<group_settings> grps = await Client.GetGroupList();
            Groups.Clear();
            foreach (group_settings grp in grps)
                Groups.Add(grp);
        }
    }
}
