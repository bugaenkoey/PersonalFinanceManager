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
        public UI()
        {
        }

        public static List<Note> Menu()
        {
            List<Note> selectedNotes = new List<Note>();

            string[] menu = new string[] { "Все", "По Дате", "Планируемые/Выполненые", "Доход/Расход", "По сумме", "На Букву" };

            string slc = UI.Select(menu, 0, 3, "Сделайте выбор :");
            switch (slc)
            {
                case "Все":
                    selectedNotes = ServeceNote.GetAll();
                    break;
                case "По Дате":
                    selectedNotes = ServeceNote.GetBetweenDate(
                        GetDateTime(25, 0, "Выберите меньшую дату"),
                        GetDateTime(25, 0, "Выберите большую дату")
                        ).ToList();
                    break;
                case "Планируемые/Выполненые":
                    selectedNotes = ServeceNote.GetPlanDone(EditPlanDone()).ToList();
                    break;
                case "Доход/Расход":
                    selectedNotes = ServeceNote.GetProfitCost(EditProfitCost()).ToList();
                    break;
                case "По сумме":
                    selectedNotes = ServeceNote.GetBetweenSum(
                        EditSum("Max"),
                        EditSum("Min")
                        ).ToList();
                    break;
                case "На Букву":
                    string desc = EditDesc();
                    selectedNotes = ServeceNote.GetDescription(desc).ToList();
                    break;

                default:
                    selectedNotes = ServeceNote.GetAll();
                    break;
            }
            return selectedNotes;
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

        internal static void SelectMethod(Note note)
        {
            Console.Clear();
            if (note == null) Add();
            string[] methods = new string[] { "Menu", "Add", "Edit", "Delete" };
            string method = Select(methods, 0, 0, "Выберите действие :");

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
                case "Menu":
                    return;
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
            ConsoleKey key;
            decimal sum;
            do
            {
                Console.Clear();

                Console.SetCursorPosition(25, 0);
                Console.WriteLine($"Отредактируйте эту запись:\n{note}");

                note.Description = EditDesc();
                note.Time = EditDateTime();
                note.Sum = sum = EditSum();
                note.ProfitCost = EditProfitCost();

                note.Sum = note.ProfitCost == ProfitCost.Расход ? Math.Abs(sum) * -1 : Math.Abs(sum);

                note.PlanDone = EditPlanDone();

                Console.Clear();
                Console.SetCursorPosition(0, 0);
                Console.WriteLine($"{note}\nНажмите 'Y' если согласны с изменениями:");

                key = Console.ReadKey(true).Key;
            } while (key != ConsoleKey.Y && key != ConsoleKey.Escape);
        }


        private static PlanDone EditPlanDone()
        {
           // Console.Clear();
            return UI.Select(
                (PlanDone[])Enum.GetValues(typeof(PlanDone)),
                25, 12, "Сделайте выбор :");
        }

        private static ProfitCost EditProfitCost()
        {
          //  Console.Clear();
            return UI.Select(
                (ProfitCost[])Enum.GetValues(typeof(ProfitCost)),
                25, 10, "Сделайте выбор :");
        }

        private static decimal EditSum(string? txt = "Введите сумму")
        {
          //  Console.Clear();

            Console.SetCursorPosition(25, 8);
            Console.WriteLine(txt);
            decimal.TryParse(Console.ReadLine(), out decimal result);
            return result;
        }

        private static DateTime EditDateTime()
        {

            return GetDateTime(25, 4, "Выберите дату");
        }

        private static string EditDesc()
        {
          //  Console.Clear();
            Console.SetCursorPosition(25, 2);
            Console.WriteLine("Введите описание");
            //  Console.OutputEncoding = System.Text.Encoding.UTF8;
            //  System.Console.InputEncoding = Encoding.GetEncoding(1251);

            var v = Console.ReadLine();
            v ??= "";//if is null
            return v;
        }


        public delegate void ChangeDate(int n);
        public static DateTime GetDateTime(int x = 0, int y = 0, string text = "")
        {
            DateTime dateTime = DateTime.Now;
            Console.SetCursorPosition(x, y);

            ChangeDate changeDays = AddDays;
            ChangeDate changeMonths = AddMonths;
            ChangeDate changeYears = AddYears;

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

            void AddDays(int n = 0)
            {
                dateTime = dateTime.AddDays(n);
            }
            void AddMonths(int n = 0)
            {
                dateTime = dateTime.AddMonths(n);
            }
            void AddYears(int n = 0)
            {
                dateTime = dateTime.AddYears(n);
            }
        }
    }
}
