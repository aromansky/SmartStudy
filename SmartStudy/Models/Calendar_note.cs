using System.Collections.ObjectModel;
using System.Data;

namespace SmartStudy.Models
{
    //класс Note - близнец класса @event, оставил пока что его, т.к. потом надо будет
    //менять взаимодействие с event_id
    //у меня они генерируются для конкретного пользователя, а должны глобально где-то в бд
    //
    //если я неправильно понял, можно сразу заменить его на @event
    //с которым по идее должна быть интеграция у бд
    //
    //вообще можно создать функции в бд и вызывать их из моих, с уже переданными
    //параметрами(id, строки, даты), если это удобно 
    internal class Note
    {
        public long Id { get; set; }
        public string Name_note { get; set; }
        public string Text_note { get; set; }
        public DateTime Date_begin { get; set; }
        public DateTime Date_end { get; set; }
    }
    internal class Calendar_note
    {
        private string AppDataPath = FileSystem.AppDataDirectory;
        //коллекция всех событий пользователя
        public ObservableCollection<Note> Notes { get; set; } = new ObservableCollection<Note>();
        public void create_data()
        {
            DataSet usersSet = new DataSet("UsersSet");
            DataTable users = new DataTable("Users");
            usersSet.Tables.Add(users);

            DataColumn idColumn = new DataColumn("Id", Type.GetType("System.Int64"));
            idColumn.Unique = true; // столбец будет иметь уникальное значение
            idColumn.AllowDBNull = false; // не может принимать null
            idColumn.AutoIncrement = true; // будет автоинкрементироваться
            idColumn.AutoIncrementSeed = 0; // начальное значение
            idColumn.AutoIncrementStep = 1; // приращении при добавлении новой строки
            
            DataColumn nameColumn = new DataColumn("Name", Type.GetType("System.String"));
            DataColumn textColumn = new DataColumn("Text", Type.GetType("System.String"));
            DataColumn date_begin_Column = new DataColumn("Date_begin", Type.GetType("System.DateTime"));
            DataColumn date_end_Column = new DataColumn("Date_end", Type.GetType("System.DateTime"));
            textColumn.DefaultValue = ""; // значение по умолчанию

            users.Columns.Add(idColumn);
            users.Columns.Add(nameColumn);
            users.Columns.Add(textColumn);
            users.Columns.Add(date_begin_Column);
            users.Columns.Add(date_end_Column);

            users.PrimaryKey = new DataColumn[] { users.Columns["Id"] };

            users.WriteXml(AppDataPath+"local_calendar.xml", XmlWriteMode.WriteSchema);
        }
        //когда появится взаимодействие с бд, дублировать это же для класса @event
        //в связке с какой-то функцией добавления события в бд
        //и соответственно после добавления в бд запись в локальный файл моей функцией с переданным event_id
        public void add_data(string Name_note, string Text_note, DateTime Date_begin, DateTime Date_end)
        {
            
            DataSet usersSet = new DataSet("UsersSet");
            DataTable users = new DataTable("Users");
            usersSet.Tables.Add(users);
            if (!File.Exists(AppDataPath+"local_calendar.xml"))
                create_data();
            users.ReadXml(AppDataPath+"local_calendar.xml");

            users.Rows.Add(new object[] { null, Name_note, Text_note, Date_begin, Date_end });

            users.WriteXml(AppDataPath+"local_calendar.xml", XmlWriteMode.WriteSchema);
            sort_data();
        }
        
        //функция существует пока нет добавления события в события в бд
        //предназначена для сортировки по времени, так что можно
        //оставить для оффлайн версии, если такая будет, единственно заменять note_id на event_id
        private void sort_data()
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
                note.Name_note = r.ItemArray[1].ToString();
                note.Text_note = r.ItemArray[2].ToString();
                note.Date_begin = DateTime.ParseExact(r.ItemArray[3].ToString(), "G", null);
                note.Date_end = DateTime.ParseExact(r.ItemArray[4].ToString(), "G", null);
                Notes.Add(note);
            }
            var sortedNotes = Notes.OrderBy(p => p.Date_begin).ThenBy(p => p.Date_end);
            users.Rows.Clear();
            long i = 0;
            foreach (Note note in sortedNotes)
            {
                users.Rows.Add(new object[] { i, note.Name_note, note.Text_note, 
                    note.Date_begin, note.Date_end });
                i ++;
            }    
                
            users.WriteXml(AppDataPath+"local_calendar.xml", XmlWriteMode.WriteSchema);
        }
        public Calendar_note() =>
            Load_All_Notes();
        
        //загружает все события,
        //в неё можно поместить функцию из бд для притягивания всех событий пользователя
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
                note.Id = (long)r.ItemArray[0];
                note.Name_note = r.ItemArray[1].ToString();
                note.Text_note =  r.ItemArray[2].ToString();
                note.Date_begin = DateTime.ParseExact(r.ItemArray[3].ToString(), "G", null);
                note.Date_end = DateTime.ParseExact(r.ItemArray[4].ToString(), "G", null);
                Notes.Add(note);
            }
        }

        //сохраняет изменения в событии с id=editing_Id
        //аналогично можно воспользоваться передаваемыми параметрами для вызова функции из бд
        public void Save_edit_note(long editing_Id, string Name_note, string Text_note, DateTime Date_begin, DateTime Date_end)
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
                note_editing["Text"] = Text_note;
                note_editing["Date_begin"] = Date_begin;
                note_editing["Date_end"] = Date_end;
            }
            users.WriteXml(AppDataPath+"local_calendar.xml", XmlWriteMode.WriteSchema);
            sort_data();
        }
        //удаляет событие id=editing_Id
        public void Delete_note(long editing_Id)
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
        //считывает параметры события с id=editing_Id из списка всех событий
        public Note Get_Note_By_Id(long editing_Id)
        {
            return Notes.Where(x => x.Id == editing_Id).FirstOrDefault();
        }
    }
}
