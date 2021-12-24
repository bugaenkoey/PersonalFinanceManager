using PersonalFinanceManager.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace PersonalFinanceManager.Service
{
    class ServeceNote //: IService
    {
        static string currentDirectory = Directory.GetCurrentDirectory();
        static string dir = $"{currentDirectory}\\{ typeof(Note).Name}";
        static string path = $"{dir}\\note.txt";

        public static List<Note> notes = new List<Note>();
        public ServeceNote()
        {
        }
        public static void FileCreate()
        {

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            if (!File.Exists(path))
            {
                File.Create(path);
            }
        }

        public static void Add(Note note)
        {
            //if (!notes.Any(n => n.id == note.id))
                notes.Add(note);
        }
        public static List<Note> GetAll()
        {
            return notes;
        }

        public static IEnumerable<Note> GetBetweenDate(DateTime dateTimeStart, DateTime dateTimeEnd)
        {
            var result = from p in notes // определяем каждый объект из teams как t
                         where (p.Time >= dateTimeStart && p.Time <= dateTimeEnd)//фильтрация по критерию
                         orderby p.Time descending // упорядочиваем по возрастанию
                         select p; // выбираем объект
            return result;
        }

        public static IEnumerable<Note> GetPlanDone(PlanDone planDone)
        {
            var result = from p in notes // определяем каждый объект из teams как t
                         where (p.PlanDone == planDone)//фильтрация по критерию
                         orderby p.Time descending// упорядочиваем по возрастанию
                         select p; // выбираем объект
            return result;
        }

        public static IEnumerable<Note> GetDescription(string desc)
        {
            var result = from p in notes // определяем каждый объект из teams как t
                         where (p.Description.ToLower().Contains(desc.ToLower()))//фильтрация по критерию

                         //where (p.Description.ToLower().Contains(desc.ToLower(),
                         //StringComparison.InvariantCultureIgnoreCase))//фильтрация по критерию

                         orderby p.Description descending// упорядочиваем по возрастанию
                         select p; // выбираем объект
            return result;
        }

        public static IEnumerable<Note> GetBetweenSum(decimal max, decimal min)
        {
            var result = from p in notes // определяем каждый объект из teams как t
                         where (p.Sum < max && p.Sum > min)//фильтрация по критерию
                         orderby p.Sum descending// упорядочиваем по возрастанию
                         select p; // выбираем объект
            return result;
        }

        public static IEnumerable<Note> GetProfitCost(ProfitCost profitCost)
        {
            var result = from p in notes // определяем каждый объект из teams как t
                         where (p.ProfitCost == profitCost)//фильтрация по критерию
                         orderby p.Time descending // упорядочиваем по возрастанию
                         select p; // выбираем объект
            return result;
        }

        //public void DeleteBetween(DateTime dateTimeStart, DateTime dateTimeEnd)
        //{

        //}

        internal static void Validator()
        {
            foreach (var item in notes)
            {
                item.Sum = item.ProfitCost == ProfitCost.Расход
                    ? Math.Abs((decimal)item.Sum) * -1
                    : Math.Abs((decimal)item.Sum);
            }
        }


        internal static string TotalSum(List<Note> notes)
        {
            string totalSum = string.Empty;
            totalSum = notes.Where(n => n.PlanDone == PlanDone.Выполнено).Sum(n => n.Sum).ToString();

            return totalSum;
        }

        public static async Task LoadAsync()
        {
            using FileStream openStream = File.OpenRead(path);
            notes = await JsonSerializer.DeserializeAsync<List<Note>>(openStream);

            //int id=notes.Max(n => n.id);
            //Note.count = id;
        }

        public static async Task SaveAsync()
        {
            using FileStream createStream = File.Create(path);
            await JsonSerializer.SerializeAsync(createStream, notes);
            Console.WriteLine("Saved");
        }
        public static void DefaultData()
        {
            //DateTime dateTime1 =  UI.GetDateTime();
            //Console.WriteLine(dateTime1);

            DateTime dateTime = new DateTime(1980, 05, 15);
            DateTime dateTime2 = new DateTime(2021, 11, 14);

            ServeceNote.Add(new Note("Popcorn", -35.50M, DateTime.Now, ProfitCost.Расход, PlanDone.Выполнено));
            ServeceNote.Add(new Note("Шампанское", -150.00M, DateTime.Now, ProfitCost.Расход, PlanDone.План));
            ServeceNote.Add(new Note("Хлеб", -12.0M, DateTime.Today, ProfitCost.Расход, PlanDone.Выполнено));
            ServeceNote.Add(new Note("Масло", -32.0M, dateTime, ProfitCost.Доход, PlanDone.Выполнено));
            ServeceNote.Add(new Note("Продал кресло", 1000.0M, dateTime2, ProfitCost.Доход, PlanDone.План));

            ServeceNote.Add(new Note("Капуста", -15.12M, DateTime.Now, ProfitCost.Доход, PlanDone.Выполнено));
            ServeceNote.Add(new Note("Кнэка", -12345.12M, DateTime.Now, ProfitCost.Расход, PlanDone.План));
            ServeceNote.Add(new Note("Сахар", -270.00M, DateTime.Today, ProfitCost.Доход, PlanDone.План));
            ServeceNote.Add(new Note("Чай", -43.0M, dateTime, ProfitCost.Доход, PlanDone.Выполнено));
            ServeceNote.Add(new Note("Продал огурци", 1000.0M, dateTime2, ProfitCost.Доход, PlanDone.Выполнено));
            ServeceNote.Add(new Note("Продал программу", 10000.0M, new DateTime(), ProfitCost.Доход, PlanDone.Выполнено));

            ServeceNote.Validator();
            _ = ServeceNote.SaveAsync();

        }
    }
}
