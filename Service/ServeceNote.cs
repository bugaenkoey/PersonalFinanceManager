using PersonalFinanceManager.Data;
using PersonalFinanceManager.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
//using System.Text.Json;
using System.Threading.Tasks;

namespace PersonalFinanceManager.Service
{
    class ServeceNote //: IService
    {
        // private  DateTime dateNow;

        public static List<Note> notes = new List<Note>();
        public ServeceNote()
        {
            // notes = new List<INote>();
            // dateNow = DateTime.Now;
        }


        public static void Add(Note note)
        {
            if (!notes.Any(n => n.id == note.id))
                notes.Add(note);
        }
        public static List<Note> GetAll()
        {
            return notes;
        }

        public IEnumerable<Note> GetBetween(DateTime dateTimeStart, DateTime dateTimeEnd)
        {

            return from p in notes // определяем каждый объект из teams как t
                   where (p.Time >= dateTimeStart && p.Time <= dateTimeStart)//фильтрация по критерию
                   orderby p.Time  // упорядочиваем по возрастанию
                   select p; // выбираем объект

        }
        public IEnumerable<Note> GetId(int id)
        {
            return from note in notes
                   where note.id == id
                   select note;
        }

        public void Edit(int id, INote note)
        {


        }
        public void DeleteBetween(DateTime dateTimeStart, DateTime dateTimeEnd)
        {


        }

        public void Delete(int id)
        {


        }


        //public IEnumerable<INote> Load()
        //{
        //    throw new NotImplementedException();
        //}

        public static async Task LoadAsync()
        {
           // List<Note> v1;
            string path = $"C:\\{ typeof(Note).Name}";

            {
                using FileStream openStream = File.OpenRead($"{path}\\note.txt");
                notes = await JsonSerializer.DeserializeAsync<List<Note>>(openStream);
            }



            //string textFromFile;
            //// чтение из файла
            //using (FileStream fstream = File.OpenRead($"{path}\\note.txt"))
            //{
            //    // преобразуем строку в байты
            //    byte[] array = new byte[fstream.Length];
            //    // считываем данные
            //    fstream.Read(array, 0, array.Length);
            //    // декодируем байты в строку
            //    textFromFile = System.Text.Encoding.Default.GetString(array);
            //    Console.WriteLine($"Текст из файла: {textFromFile}");
            //}
            //List<Note> njs = new List<Note>();
            //var v = JsonSerializer.Deserialize<List<Note>>(textFromFile);
            //Console.WriteLine($"{notes}");
            Console.Clear();

            //         UI.Select(notes.ToArray());
        }

        internal static void Validator()
        {
            foreach (var item in notes)
            {
                item.Sum = item.ProfitCost == ProfitCost.Расход ? Math.Abs((decimal)item.Sum) * -1 : Math.Abs((decimal)item.Sum);
            }
        }

        //public void Save(IEnumerable<INote> notes)
        //{
        //    string json = JsonSerializer.Serialize< IEnumerable<INote>>(notes);
        //    Console.WriteLine(json);

        //}


        internal static string Itog(Note[] notes)
        {
            string itog = string.Empty;
            //  itog = notes.Sum(n => n.Sum).ToString();
            itog = notes.Where(n => n.PlanDone == PlanDone.Выполнено).Sum(n => n.Sum).ToString();

            return itog;
        }

        public static async Task SaveAsync()
        {
            Note nnt = new Note("Popcorn", -35.50M, DateTime.Now, ProfitCost.Расход, PlanDone.Выполнено);
            string js = JsonSerializer.Serialize(nnt);
            Console.WriteLine(js);

            string json = JsonSerializer.Serialize(notes);
            //  string json = JsonSerializer.Serialize<IEnumerable<INote>>((IEnumerable<INote>)notes);
            Console.WriteLine(json);





            // Console.WriteLine($"this.path = {this.path}");

            string path = $"C:\\{ typeof(Note).Name}";
            ///  Console.WriteLine($"Description:{this.Description}\nSum:{this.Sum}\nTime:{this.Time}");

            //DirectoryInfo dirInfo = new DirectoryInfo(path);
            //if (!dirInfo.Exists)
            //{
            //    dirInfo.Create();
            //}

            //try
            //{
            //    using (StreamWriter sw = new StreamWriter($"{path}\\note.txt", true, Encoding.Default))
            //    {
            //        sw.WriteLine(json);
            //    }

            //    Console.ForegroundColor = ConsoleColor.Magenta;
            //    Console.WriteLine("Запись выполнена");
            //    Console.ResetColor();
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e.Message);
            //}

            Console.WriteLine(json);
            Console.ReadLine();

            {
                using FileStream createStream = File.Create($"{path}\\note.txt");
                await JsonSerializer.SerializeAsync(createStream, notes);
            }
            
          //  var v1 = JsonSerializer.Deserialize<List<Note>>(json);
            //Console.WriteLine($"{notes}");
        }
    }
}
