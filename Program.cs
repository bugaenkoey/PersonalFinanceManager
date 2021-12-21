using PersonalFinanceManager.Data;
using PersonalFinanceManager.Service;
using PersonalFinanceManager.View;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Reflection;
using System.Text;

namespace PersonalFinanceManager
{
    class Program
    {
        static void Main(string[] args)
        {
            // System.Console.InputEncoding = Encoding.GetEncoding(1251);



            //  ServeceNote service = new();
            //  DefaultData();

            _ = ServeceNote.LoadAsync();
            ServeceNote.Validator();

            List<Note> notes;
            Console.WriteLine("Personal Finance Manager");
            string itog;
            Note selectedNote;

            do
            {
                Console.Clear();

                notes = UI.Menu();
                if (notes.Count==0)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Не найдено");
                    Console.ResetColor();
                    Console.ReadLine();
                    Console.Clear();
                    continue;

                }
                itog = ServeceNote.Itog(notes);

                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Итог по выбранному: {itog}");
                Console.ResetColor();

                selectedNote = UI.Select(notes.ToArray(), 0, 3, "Сделайте выбор :");
                UI.SelectMethod(selectedNote);

                Console.Clear();
                Console.WriteLine("Хотите Выйти нажмите 'Esc'");

            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);


            ServeceNote.SaveAsync();
            Console.ReadLine();

            //var methods = Type.GetType("PersonalFinanceManager.Data.Note", false, true).GetMethods();
            //var v =  methods.Where(m => m.Name == "Add" || m.Name == "Edit" || m.Name == "Del").Select(m=> m.Name) ;

            //Console.Clear();

            //var sl = UI.DoSelect(v.ToArray(), 10, 10,"Select Method");

            //MethodInfo mI = selectedNote.GetType().GetMethod(sl.ToString());
            //var alls = mI.Invoke(selectedNote, null);


            //  Console.ReadLine();

            //     var allNote = service.GetAll();

            //           foreach (var s in allNote) Console.WriteLine(s.ToString());

            //var getId = service.GetId(0);
            ////          foreach (var s in allNote) Console.WriteLine(s.ToString());


            //var selectedNote = from t in allNote // определяем каждый объект из teams как t
            //                   where t.Sum < -13 //фильтрация по критерию
            //                   orderby t.Sum  // упорядочиваем по возрастанию
            //                   select t; // выбираем объект


            //          foreach (var s in selectedNote)  Console.WriteLine(s.ToString());

            //Console.WriteLine(allNote.Where(p => p.Sum < 0)Select p);
            // Console.WriteLine(selectedNote);

            //   var gB = service.GetBetween(dateTime2, DateTime.Now);

            //   foreach (var s in gB) Console.WriteLine(s.ToString());


            //    gB = service.GetBetween(dateTime, DateTime.Today);

            //   foreach (var s in gB) Console.WriteLine(s.ToString());

            //foreach (var item in allNote)
            //{
            //    //  Console.WriteLine(item.ToString());

            //    // return _context.Games.Where(_ => _.DateRelease < dateTime).ToList();
            //    // Console.WriteLine(allNote.Where(p => p.Sum<0).ToList());
            //}



            //Type myType = Type.GetType("PersonalFinanceManager.Data.Note", false, true);

            //Console.WriteLine("Методы:");
            //foreach (MethodInfo method in myType.GetMethods())
            //{
            //    Console.Write($"{method.ReturnType.Name} {method.Name}");
            //    //получаем все параметры

            //}
            //Console.WriteLine();

            //myType = Type.GetType("PersonalFinanceManager.Service.IService", false, true);

            //Console.WriteLine("Методы:");
            //foreach (MethodInfo method in myType.GetMethods())
            //{
            //    Console.Write($" {method.Name}");
            //    //получаем все параметры

            //}
            //  UI ui = new UI();

            //while (true)
            //{
            //    ui.AddNote();


            //   Console.ReadLine();

            //var methods = Type.GetType("PersonalFinanceManager.Service.IService", false, true).GetMethods();
            //var v = from m in methods
            //        select m.Name;
            //var sl = ui.Select(v.ToArray(), 10, 10);
            //// ServeceNote sN = new();
            //MethodInfo mI = service.GetType().GetMethod(sl.ToString());
            //var alls = mI.Invoke(service, null);

            //foreach (var s in (IEnumerable<INote>)alls) Console.WriteLine(s.ToString());

            //           Console.ReadLine();
            //           var fff = service.GetAll().ToArray();
            //           Console.Clear();
            //           { }
            ////           Console.WriteLine($" Vibor => {UI.Select((fff), 0, 0, "SDELSITE VIBOR").MyToString()}");

            //           Console.ReadLine();
            // }

        }

        private static void DefaultData()
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

            // sum = profitCost == ProfitCost.Расход ? Math.Abs(sum) * -1 : Math.Abs(sum);

            ServeceNote.Validator();
            ServeceNote.SaveAsync();

            //ProfitCost pc = UI.Select((ProfitCost[])Enum.GetValues(typeof(ProfitCost)), 10, 10, "Сделайте выбор :");

            //PlanDone pd = UI.Select((PlanDone[])Enum.GetValues(typeof(PlanDone)), 20, 20, "Сделайте выбор :");
        }
    }
}


