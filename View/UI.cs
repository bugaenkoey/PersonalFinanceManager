using PersonalFinanceManager.Data;
using PersonalFinanceManager.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinanceManager.View
{
    class UI
    {
        ServeceNote service;
        public UI()
        {
            service = new();
        }

        public static T Select<T>(T[] lictOption, int x = 0, int y = 0, string text = "")
        {
            ConsoleKey Key;
            Console.SetCursorPosition(x, y);
            Console.Write(text);
            for (int i = 0; i < lictOption.Length; i++)
            {
                Console.SetCursorPosition(x, y + i + 1);
                Console.Write($"[ ]{lictOption[i]}");

            }
            int j = 0;
            do
            {
                if (j == lictOption.Length)
                {
                    j = 0;
                }
                if (j < 0)
                {
                    j = lictOption.Length - 1;
                }

                Console.SetCursorPosition(x + 1, y + j + 1);
                Console.Write("*");

                Key = Console.ReadKey(true).Key;

                Console.SetCursorPosition(x + 1, y + j + 1);
                Console.Write(" ");

                switch (Key)
                {
                    case ConsoleKey.Enter:
                        return lictOption[j];

                    case ConsoleKey.Escape:
                        return default(T);

                    case ConsoleKey.Spacebar: j--; break;
                    case ConsoleKey.UpArrow: j--; break;
                    case ConsoleKey.DownArrow: j++; break;
                    case ConsoleKey.LeftArrow: j--; break;
                    case ConsoleKey.RightArrow: j++; break;
                }
            } while (true);
        }



        //public void AddNote()
        //{
        //    Console.Clear();
        //  Console.WriteLine(" Select Now and Plan");
        //  string[] nowPlan = { "Now", "Plan" };
        //string selectNowPlan = Select(nowPlan,5,5);
        //  Console.WriteLine($"your selected {selectNowPlan}");

        //  switch (selectNowPlan)
        //  {
        //      case "Now":
        //          Edit(false);
        //          break;
        //      case "Plan":
        //          Edit(true);
        //          break;
        //      default:
        //          break;
        //  }
        //  Edit();

        // }

        internal static void SelectMethod(Note note)
        {
            if (note == null) Add();
            string[] methods = new string[] { "Add", "Edit", "Delete" };
            string method = Select(methods, 0, 20, "Выберите действие :");

            switch (method)
            {
                case "Add":
                    Add();
                    break;
                case "Edit":
                    Edit(note);
                    break;
                case "Delete":
                    Delete(note);
                    break;
                default:
                    break;
            }

        }

        private static void Delete(Note note)
        {
            Console.Clear();
            Console.WriteLine($"Запись: {note}");
            Console.Write(ServeceNote.notes.Remove(note) ? "Удалена !!!" : "Не получилось удалить.");
            //Console.ReadKey();
            Console.ReadLine();
            Console.Clear();
        }

        private static void Add()
        {
            Console.Clear();
            Console.WriteLine("Добавьте новую запись.");
            //string desc = EditDesc();
            //decimal sum = EditSum();
            //DateTime dateTime = EditDateTime();
            //ProfitCost profitCost = EditProfitCost();

            //sum = profitCost == ProfitCost.Расход ? Math.Abs(sum) * -1 : Math.Abs(sum);

            //PlanDone planDone = EditPlanDone();
            //Note note = new(desc, sum, dateTime, profitCost, planDone);

            Note note = new();
            Edit(note);
            ServeceNote.Add(note);

        }

        private static void Edit(Note note)
        {
            ConsoleKey key;
            decimal sum;
            do
            {

                Console.Clear();
                Console.WriteLine($"Отредактируйте эту запись:\n{note}");

                note.Description = EditDesc();
                note.Time = EditDateTime();
                note.Sum = sum = EditSum();
                note.ProfitCost = EditProfitCost();

                note.Sum = note.ProfitCost == ProfitCost.Расход ? Math.Abs(sum) * -1 : Math.Abs(sum);

                note.PlanDone = EditPlanDone();

                Console.WriteLine($"{note}\nНажмите 'Y' если согласны с изменениями:");

                key = Console.ReadKey(true).Key;
            } while (key != ConsoleKey.Y || key != ConsoleKey.Escape);

        }


        private static PlanDone EditPlanDone()
        {

            return UI.Select((PlanDone[])Enum.GetValues(typeof(PlanDone)), 20, 20, "Сделайте выбор :");

            //Console.WriteLine("Пожалуйста, введите\n 1 - если это запланировано 0 - если это уже сбылось");
            //int.TryParse(Console.ReadLine(), out int result);
            //return (PlanDone)result;
        }

        private static ProfitCost EditProfitCost()
        {
            return UI.Select((ProfitCost[])Enum.GetValues(typeof(ProfitCost)), 10, 10, "Сделайте выбор :");

            //Console.WriteLine("Пожалуйста, введите  1 - если это уже выполнено 0 - если это еще не выполнено");
            //int.TryParse(Console.ReadLine(), out int result);
            //return (ProfitCost)result;
        }

        private static decimal EditSum()
        {
            Console.Clear();
            Console.WriteLine("Введите сумму");
            decimal.TryParse(Console.ReadLine(), out decimal result);
            return result;
        }

        private static DateTime EditDateTime()
        {

            // TO DO
            Console.Clear();
            Console.WriteLine("Установлена текущая дата");
            return DateTime.Now;
        }

        private static string EditDesc()
        {
            Console.WriteLine("Введите описание");
            return Console.ReadLine();
        }


        public delegate void ChangeDate( int n);
        public static DateTime GetDateTime(int x = 0, int y = 0, string text = "")
        {
            DateTime dateTime = DateTime.Now;
            Console.SetCursorPosition(x, y);

            
            ChangeDate changeDays;
            changeDays = AddDays;
            ChangeDate changeMonths;
            changeMonths = AddMonths;
            ChangeDate changeYears;
            changeYears = AddYears;

            ChangeDate[] delegats = new ChangeDate[] { changeDays, changeMonths, changeYears };

            ConsoleKey Key;
            int j = 0;

            Console.SetCursorPosition(x, y);
            Console.Write(text);

            do
            {
                if (j == delegats.Length) j = 0;
                if (j < 0) j = delegats.Length - 1;

                ChangeDate changeDate = delegats[j];

                Console.SetCursorPosition(x + 1, y + 1);
                Console.Write(dateTime.ToString("d"));
                Console.SetCursorPosition(x + 1 + (j * 3), y + 2);
                Console.Write("^^");

                Key = Console.ReadKey(true).Key;

                Console.SetCursorPosition(x + 1 + (j * 3), y + 2);
                Console.Write("  ");


                switch (Key)
                {
                    case ConsoleKey.Enter:
                    case ConsoleKey.Spacebar:
                        return dateTime;

                    case ConsoleKey.Escape:
                        dateTime = DateTime.Now;
                        break;

                    case ConsoleKey.UpArrow: changeDate( 1); break;
                    case ConsoleKey.DownArrow: changeDate( -1); break;
                    case ConsoleKey.LeftArrow: j--; break;
                    case ConsoleKey.RightArrow: j++; break;
                }

            } while (true);
            void AddDays( int n = 0)
            {
                dateTime = dateTime.AddDays(n);
            }
            void AddMonths( int n = 0)
            {
                dateTime = dateTime.AddMonths(n);
            }
            void AddYears( int n = 0)
            {
                dateTime = dateTime.AddYears(n);
            }
        }




    }
}
