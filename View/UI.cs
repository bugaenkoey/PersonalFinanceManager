using PersonalFinanceManager.Data;
using PersonalFinanceManager.Service;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace PersonalFinanceManager.View
{
    class UI
    {
        public UI()
        {
        }

        public static List<Note> Menu()
        {
            List<Note> selectedNotes = new List<Note>();

            const string ADD = "Добавить запись";
            const string ALL = "Все";
            const string BETWEEN_DATE = "По Дате";
            const string PLAN_DONE = "Планируемые/Выполненые";
            const string PROFIT_COST = "Доход/Расход";
            const string BETWEEN_SUM = "По сумме";
            const string DESCRIPTION = "По описанию";

            List<string> menu = new List<string> { ADD, ALL, BETWEEN_DATE, PLAN_DONE, PROFIT_COST, BETWEEN_SUM, DESCRIPTION };


            string slc = UI.Select(menu, 0, 3, "Сделайте выбор :");
            switch (slc)
            {
                case ADD:
                    Add();
                    break;
                case ALL:
                    selectedNotes = ServeceNote.GetAll();
                    break;
                case BETWEEN_DATE:
                    selectedNotes = ServeceNote.GetBetweenDate(
                        GetDateTime(25, 0, "Выберите меньшую дату"),
                        GetDateTime(25, 0, "Выберите большую дату")
                        ).ToList();
                    break;
                case PLAN_DONE:
                    selectedNotes = ServeceNote.GetPlanDone(EditPlanDone()).ToList();
                    break;
                case PROFIT_COST:
                    selectedNotes = ServeceNote.GetProfitCost(EditProfitCost()).ToList();
                    break;
                case BETWEEN_SUM:
                    selectedNotes = ServeceNote.GetBetweenSum(
                        EditSum("Max"),
                        EditSum("Min")
                        ).ToList();
                    break;
                case DESCRIPTION:
                    string desc = EditDescription();
                    selectedNotes = ServeceNote.GetDescription(desc).ToList();
                    break;
                default:
                    selectedNotes = ServeceNote.GetAll();
                    break;
            }
            return selectedNotes;
        }



        public static T Select<T>(List<T> contents, int x = 0, int y = 0, string text = "")
        {
            Console.SetCursorPosition(x, y);
            Console.Write(text);
            for (int i = 0; i < contents.Count; i++)
            {
                Console.SetCursorPosition(x, y + i + 1);
                Console.Write($"[ ]{contents[i]}");
            }

            ConsoleKey Key;
            int j = 0;
            do
            {
                if (j == contents.Count) { j = 0; }
                if (j < 0) { j = contents.Count - 1; }

                Console.SetCursorPosition(x + 1, y + j + 1);
                Console.Write("*");

                Key = Console.ReadKey(true).Key;

                Console.SetCursorPosition(x + 1, y + j + 1);
                Console.Write(" ");

                switch (Key)
                {
                    case ConsoleKey.Enter:
                        return contents[j];

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

        internal static void SelectMethod(Note note)
        {
            Console.Clear();
            if (note == null) Add();

            const string ADD = "Add";
            const string MENU = "Menu";
            const string EDIT = "Edit";
            const string DELETE = "Delete";

            List<string> methods = new List<string> { MENU, ADD, EDIT, DELETE };

            string method = Select(methods, 0, 0, "Выберите действие :");

            switch (method)
            {
                case ADD:
                    Add();
                    break;
                case MENU:
                    return;
                case EDIT:
                    Edit(note);
                    break;
                case DELETE:
                    Delete(note);
                    break;
                default:
                    break;
            }
        }

        private static void Delete(Note note)
        {
            Console.Clear();
            Console.SetCursorPosition(25, 0);
            Console.WriteLine($"Запись: {note}");
            Console.Write(ServeceNote.notes.Remove(note) ? "Удалена !!!" : "Не получилось удалить.");
            //Console.ReadKey();
            Console.ReadLine();
            Console.Clear();
        }

        private static void Add()
        {
            Console.Clear();
            Console.SetCursorPosition(25, 0);
            Console.WriteLine("Добавьте новую запись.");
            Note note = new();
            Edit(note);
            ServeceNote.Add(note);
        }


        private static void Edit(Note note)
        {
            Console.WriteLine(note);
            const string DESCRPTION = "Изменить описание";
            const string TIME = "Изменить дату";
            const string SUM = "Изменить сумму";
            const string PROFIT_COST = "Расход Доход";
            const string PLAN_DONE = "План Выполнено";
            const string EXIT = "Выход";
            List<string> editors = new List<string> { DESCRPTION, TIME, SUM, PROFIT_COST, PLAN_DONE , EXIT };

            ConsoleKey key;

                decimal sum = 0;
            do
            {
                Console.Clear();
                Console.WriteLine($"Отредактируйте эту запись:");
                string method = Select(editors, 0, 2, $"{note}");
                switch (method)
                {
                    case DESCRPTION:
                        note.Description = EditDescription();
                        break;
                    case TIME:
                        note.Time = EditDateTime();
                        break;
                    case SUM:
                        sum = EditSum();
                        break;
                    case PROFIT_COST:
                        note.ProfitCost = EditProfitCost();
                        break;
                    case PLAN_DONE:
                        note.PlanDone = EditPlanDone();
                        break;
                    case EXIT:
                        break;
                    default:
                        break;
                }
                note.Sum = note.ProfitCost == ProfitCost.Расход ? Math.Abs(sum) * -1 : Math.Abs(sum);

                Console.Clear();
                Console.SetCursorPosition(0, 0);
                Console.WriteLine($"{note}\nНажмите 'Y' если согласны с изменениями:");

                key = Console.ReadKey(true).Key;
            } while (key != ConsoleKey.Y && key != ConsoleKey.Escape);
        }

        //private static void Edit2(Note note)
        //{
        //    ConsoleKey key;
        //    decimal sum;
        //    do
        //    {
        //        Console.Clear();

        //        Console.SetCursorPosition(25, 0);
        //        Console.WriteLine($"Отредактируйте эту запись:\n{note}");

        //        note.Description = EditDescription();
        //        note.Time = EditDateTime();
        //        note.Sum = sum = EditSum();
        //        note.ProfitCost = EditProfitCost();

        //        note.Sum = note.ProfitCost == ProfitCost.Расход ? Math.Abs(sum) * -1 : Math.Abs(sum);

        //        note.PlanDone = EditPlanDone();

        //        Console.Clear();
        //        Console.SetCursorPosition(0, 0);
        //        Console.WriteLine($"{note}\nНажмите 'Y' если согласны с изменениями:");

        //        key = Console.ReadKey(true).Key;
        //    } while (key != ConsoleKey.Y && key != ConsoleKey.Escape);
        //}

        private static string EditDescription()
        {
            Console.Clear();
            Console.SetCursorPosition(25, 2);
            Console.WriteLine("Введите описание");
            //  Console.OutputEncoding = System.Text.Encoding.UTF8;
            //  System.Console.InputEncoding = Encoding.GetEncoding(1251);

            var v = Console.ReadLine();
            v ??= "";//if is null
            return v;
        }



        private static PlanDone EditPlanDone()
        {
            var listOfEnums = Enum.GetValues(typeof(PlanDone)).Cast<PlanDone>().ToList();
            //  Console.Clear();
            return UI.Select(listOfEnums, 25, 12, "Сделайте выбор:");
        }

        private static ProfitCost EditProfitCost()
        {
            var listOfEnums = Enum.GetValues(typeof(ProfitCost)).Cast<ProfitCost>().ToList();
            //  Console.Clear();
            return UI.Select(listOfEnums, 25, 10, "Сделайте выбор:");
        }

        private static decimal EditSum(string? txt = "Введите сумму:")
        {
            //  Console.Clear();

            Console.SetCursorPosition(25, 8);
            Console.Write(txt);
            decimal.TryParse(Console.ReadLine(), out decimal result);
            return result;
        }

        private static DateTime EditDateTime()
        {
            return GetDateTime(25, 4, "Выберите дату");
        }

        public delegate void ChangeDate(int n); // DELEGATE
        public static DateTime GetDateTime(int x = 0, int y = 0, string text = "")
        {
            DateTime dateTime = DateTime.Now;

            ChangeDate changeDays = (n) => dateTime = dateTime.AddDays(n);
            ChangeDate changeMonths = (n) => dateTime = dateTime.AddMonths(n);
            ChangeDate changeYears = (n) => dateTime = dateTime.AddYears(n);

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

                    case ConsoleKey.UpArrow: changeDate(1); break;
                    case ConsoleKey.DownArrow: changeDate(-1); break;
                    case ConsoleKey.LeftArrow: j--; break;
                    case ConsoleKey.RightArrow: j++; break;
                }
            } while (true);
        }
    }
}
