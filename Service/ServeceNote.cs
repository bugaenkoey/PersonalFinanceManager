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
        static string path = $"C:\\{ typeof(Note).Name}\\note.txt";

        public static List<Note> notes = new List<Note>();
        public ServeceNote()
        {

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

        public static IEnumerable<Note> GetBetweenDate(DateTime dateTimeStart, DateTime dateTimeEnd)
        {
            var result = from p in notes // определяем каждый объект из teams как t
                         where (p.Time >= dateTimeStart && p.Time <= dateTimeEnd)//фильтрация по критерию
                         orderby p.Time descending // упорядочиваем по возрастанию
                         select p; // выбираем объект
            return result;
        }

      public  static IEnumerable<Note> GetPlanDone(PlanDone planDone)
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
                         where (p.Description.ToLower().Contains(desc.ToLower(), StringComparison.InvariantCultureIgnoreCase) )//фильтрация по критерию
                         orderby p.Sum descending// упорядочиваем по возрастанию
                         select p; // выбираем объект
            return result;
        }

        public static IEnumerable<Note> GetBetweenSum(decimal max , decimal min)
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

        public void DeleteBetween(DateTime dateTimeStart, DateTime dateTimeEnd)
        {
          
        }

        internal static void Validator()
        {
            foreach (var item in notes)
            {
                item.Sum = item.ProfitCost == ProfitCost.Расход
                    ? Math.Abs((decimal)item.Sum) * -1
                    : Math.Abs((decimal)item.Sum);
            }
        }


        internal static string Itog(List<Note> notes)
        {
            string itog = string.Empty;
            itog = notes.Where(n => n.PlanDone == PlanDone.Выполнено).Sum(n => n.Sum).ToString();

            return itog;
        }

        public static async Task LoadAsync()
        {
            using FileStream openStream = File.OpenRead(path);
            notes = await JsonSerializer.DeserializeAsync<List<Note>>(openStream);
        }

        public static async Task SaveAsync()
        {
                using FileStream createStream = File.Create($"{path}");
                await JsonSerializer.SerializeAsync(createStream, notes);
            Console.WriteLine("Saved");
        }
    }
}
