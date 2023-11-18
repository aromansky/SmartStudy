using System.Collections.ObjectModel;
using System.Data;

namespace SmartStudy.Models
{
    internal class Note
    {
        public int Id { get; set; }
        public string Name_note { get; set; }
        public string Date_note { get; set; }
        public string Time_note { get; set; }
        public string Text_note { get; set; }
    }

    internal class Calendar_note
    {
        private string AppDataPath = FileSystem.AppDataDirectory;
        public void create_data()
        {
            DataSet usersSet = new DataSet("UsersSet");
            DataTable users = new DataTable("Users");
            usersSet.Tables.Add(users);

            DataColumn idColumn = new DataColumn("Id", Type.GetType("System.Int32"));
            idColumn.Unique = true; // столбец будет иметь уникальное значение
            idColumn.AllowDBNull = false; // не может принимать null
            idColumn.AutoIncrement = true; // будет автоинкрементироваться
            idColumn.AutoIncrementSeed = 0; // начальное значение
            idColumn.AutoIncrementStep = 1; // приращении при добавлении новой строки

            DataColumn nameColumn = new DataColumn("Name", Type.GetType("System.String"));
            DataColumn dateColumn = new DataColumn("Date", Type.GetType("System.String"));
            DataColumn timeColumn = new DataColumn("Time", Type.GetType("System.String"));
            DataColumn textColumn = new DataColumn("Text", Type.GetType("System.String"));
            textColumn.DefaultValue = ""; // значение по умолчанию

            users.Columns.Add(idColumn);
            users.Columns.Add(nameColumn);
            users.Columns.Add(dateColumn);
            users.Columns.Add(timeColumn);
            users.Columns.Add(textColumn);

            users.PrimaryKey = new DataColumn[] { users.Columns["Id"] };

            users.WriteXml(AppDataPath+"local_calendar.xml", XmlWriteMode.WriteSchema);
        }
        public void add_data(string Name_note, string Date_note, string Time_note, string Text_note)
        {
            DataSet usersSet = new DataSet("UsersSet");
            DataTable users = new DataTable("Users");
            usersSet.Tables.Add(users);
            if (!File.Exists(AppDataPath+"local_calendar.xml"))
                create_data();
            users.ReadXml(AppDataPath+"local_calendar.xml");

            users.Rows.Add(new object[] { null, Name_note, Date_note, Time_note, Text_note });

            users.WriteXml(AppDataPath+"local_calendar.xml", XmlWriteMode.WriteSchema);
        }
        public ObservableCollection<Note> Notes { get; set; } = new ObservableCollection<Note>();
        public Calendar_note() =>
            Load_All_Notes();

        public void Load_All_Notes()
        {
            Notes.Clear();

            DataSet usersSet = new DataSet("UsersSet");
            DataTable users = new DataTable("Users");
            usersSet.Tables.Add(users);
            if (!File.Exists(AppDataPath+"local_calendar.xml"))
                create_data();
            users.ReadXml(AppDataPath+"local_calendar.xml");
            foreach (DataRow r in users.Rows)
            {
                Note note = new Note();
                note.Id = (int)r.ItemArray[0];
                note.Name_note = r.ItemArray[1].ToString();
                note.Date_note = r.ItemArray[2].ToString();
                note.Time_note = r.ItemArray[3].ToString();
                note.Text_note = r.ItemArray[4].ToString();
                Notes.Add(note);
            }
        }
        public void Save_edit_note(int editing_Id, string Name_note, string Date_note, string Time_note, string Text_note)
        {
            DataSet usersSet = new DataSet("UsersSet");
            DataTable users = new DataTable("Users");
            usersSet.Tables.Add(users);
            if (!File.Exists(AppDataPath+"local_calendar.xml"))
                create_data();
            users.ReadXml(AppDataPath+"local_calendar.xml");

            var selectedUsers = users.Select($"Id = {editing_Id}");
            foreach (var note_editing in selectedUsers)
            {
                note_editing["Name"] = Name_note;
                note_editing["Date"] = Date_note;
                note_editing["Time"] = Time_note;
                note_editing["Text"] = Text_note;
            }
            users.WriteXml(AppDataPath+"local_calendar.xml", XmlWriteMode.WriteSchema);
        }
        public void Delete_note(int editing_Id)
        {
            DataSet usersSet = new DataSet("UsersSet");
            DataTable users = new DataTable("Users");
            usersSet.Tables.Add(users);
            if (!File.Exists(AppDataPath+"local_calendar.xml"))
                create_data();
            users.ReadXml(AppDataPath+"local_calendar.xml");

            var selectedUsers = users.Select($"Id = {editing_Id}");
            foreach (var note_editing in selectedUsers)
                note_editing.Delete();
            users.WriteXml(AppDataPath+"local_calendar.xml", XmlWriteMode.WriteSchema);
        }
    }
}
