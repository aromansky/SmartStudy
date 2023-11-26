using System.Collections.ObjectModel;
using System.Data;
using System.Windows.Input;
using SmartStudy.ModelsDB;

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
    public class Calendar_note
    {
        private string AppDataPath = FileSystem.CacheDirectory;

        //коллекция всех событий пользователя
        public ObservableCollection<Event> Events { get; set; } = new ObservableCollection<Event>();
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
        public void add_data(string Title, string Description, DateTime date_begin, DateTime date_end)
        {
            Client.CreateEvent(new Event(Title, Serializer.DeserializeUser().user_id, Description, date_begin, date_end));
            Load_All_Events();

            //DataSet usersSet = new DataSet("UsersSet");
            //DataTable users = new DataTable("Users");
            //usersSet.Tables.Add(users);
            //if (!File.Exists(AppDataPath+"local_calendar.xml"))
            //    create_data();
            //users.ReadXml(AppDataPath+"local_calendar.xml");

            //users.Rows.Add(new object[] { null, Name_note, Text_note, Date_begin, Date_end });

            //users.WriteXml(AppDataPath+"local_calendar.xml", XmlWriteMode.WriteSchema);
            sort_data();
        }
        
        //функция существует пока нет добавления события в события в бд
        //предназначена для сортировки по времени, так что можно
        //оставить для оффлайн версии, если такая будет, единственно заменять note_id на event_id
        private void sort_data()
        {
            Events.Clear();

            DataSet usersSet = new DataSet("UsersSet");
            DataTable users = new DataTable("Users");
            usersSet.Tables.Add(users);
            if (!File.Exists(AppDataPath+"local_calendar.xml"))
                create_data();
            users.ReadXml(AppDataPath+"local_calendar.xml");
            foreach (DataRow r in users.Rows)
            {
                Event @event = new Event();
                @event.Title = r.ItemArray[1].ToString();
                @event.Description = r.ItemArray[2].ToString();
                @event.date_begin = DateTime.ParseExact(r.ItemArray[3].ToString(), "G", null);
                @event.date_end = DateTime.ParseExact(r.ItemArray[4].ToString(), "G", null);
                Events.Add(@event);
            }
            var sortedNotes = Events.OrderBy(p => p.date_begin).ThenBy(p => p.date_end);
            users.Rows.Clear();
            long i = 0;
            foreach (Event @event in sortedNotes)
            {
                users.Rows.Add(new object[] { i, @event.Title, @event.Description,
                    @event.date_begin, @event.date_end });
                i ++;
            }    
                
            users.WriteXml(AppDataPath+"local_calendar.xml", XmlWriteMode.WriteSchema);
        }
        public Calendar_note() =>
            Load_All_Events();

        //загружает все события,
        //в неё можно поместить функцию из бд для притягивания всех событий пользователя
        public async void Load_All_Events()
        {
            List<Event> evs = await Client.GetEventsWithUser(Serializer.DeserializeUser());
            Events.Clear();
            foreach (Event ev in evs.DistinctBy(x => x.event_id))
                Events.Add(ev);

            //DataSet usersSet = new DataSet("UsersSet");
            //DataTable users = new DataTable("Users");
            //usersSet.Tables.Add(users);
            //if (!File.Exists(AppDataPath+"local_calendar.xml"))
            //    create_data();
            //users.ReadXml(AppDataPath+"local_calendar.xml");
            //foreach (DataRow r in users.Rows)
            //{
            //    Event @event = new Event();
            //    @event.Title = r.ItemArray[1].ToString();
            //    @event.Description = r.ItemArray[2].ToString();
            //    @event.date_begin = DateTime.ParseExact(r.ItemArray[3].ToString(), "G", null);
            //    @event.date_end = DateTime.ParseExact(r.ItemArray[4].ToString(), "G", null);
            //    Events.Add(@event);
            //}
        }

        //сохраняет изменения в событии с id=editing_Id
        //аналогично можно воспользоваться передаваемыми параметрами для вызова функции из бд
        public void Save_edit_note(long editing_Id, string Title, string Description, DateTime date_begin, DateTime date_end)
        {
            Client.EditEventFromId(editing_Id, new Event(Title, Serializer.DeserializeUser().user_id, Description, date_begin, date_end));

            //DataSet usersSet = new DataSet("UsersSet");
            //DataTable users = new DataTable("Users");
            //usersSet.Tables.Add(users);
            //if (!File.Exists(AppDataPath+"local_calendar.xml"))
            //    create_data();
            //users.ReadXml(AppDataPath+"local_calendar.xml");

            //var selectedUsers = users.Select($"Id = {editing_Id}");
            //foreach (var note_editing in selectedUsers)
            //{
            //    note_editing["Name"] = Name_note;
            //    note_editing["Text"] = Text_note;
            //    note_editing["Date_begin"] = Date_begin;
            //    note_editing["Date_end"] = Date_end;
            //}
            //users.WriteXml(AppDataPath+"local_calendar.xml", XmlWriteMode.WriteSchema);
            sort_data();
        }
        //удаляет событие id=editing_Id
        public void Delete_note(long editing_Id)
        {
            Client.DeleteEventFromId(editing_Id);

            //DataSet usersSet = new DataSet("UsersSet");
            //DataTable users = new DataTable("Users");
            //usersSet.Tables.Add(users);
            //if (!File.Exists(AppDataPath+"local_calendar.xml"))
            //    create_data();
            //users.ReadXml(AppDataPath+"local_calendar.xml");

            //var selectedUsers = users.Select($"Id = {editing_Id}");
            //foreach (var note_editing in selectedUsers)
            //    note_editing.Delete();
            //users.WriteXml(AppDataPath+"local_calendar.xml", XmlWriteMode.WriteSchema);
        }
        //считывает параметры события с id=editing_Id из списка всех событий
        public Event Get_Event_By_Id(long editing_Id)
        {
            return Events.Where(x => x.event_id == editing_Id).FirstOrDefault();
        }
    }
}
