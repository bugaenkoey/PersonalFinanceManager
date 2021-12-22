using PersonalFinanceManager.Data;
using PersonalFinanceManager.Service;
using PersonalFinanceManager.View;
using System;
using System.Collections.Generic;

namespace PersonalFinanceManager
{
    class Program
    {
        static void Main(string[] args)
        {
            ServeceNote.FileCreate();
            // ServeceNote.DefaultData();

            _ = ServeceNote.LoadAsync();
            new ServeceNote();
            ServeceNote.Validator();

            List<Note> notes;
            Console.WriteLine("Personal Finance Manager");
            string totalSum;
            Note selectedNote;

            do
            {
                Console.Clear();

                notes = UI.Menu();
                if (notes.Count == 0)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Не найдено");
                    Console.ResetColor();
                    Console.ReadLine();
                    Console.Clear();
                    continue;
                }
                totalSum = ServeceNote.TotalSum(notes);

                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Итог по выбранному: {totalSum}");
                Console.ResetColor();

                selectedNote = UI.Select(notes, 0, 3, "Сделайте выбор :");
                UI.SelectMethod(selectedNote);

                Console.Clear();
                Console.WriteLine("Хотите Выйти нажмите 'Esc'");

            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);

            _ = ServeceNote.SaveAsync();
            Console.ReadLine();
            // NewMethod();
        }

        //private static void NewMethod()
        //{
        //    Note note = new();
        //    var methods = Type.GetType("PersonalFinanceManager.Data.Note", false, true).GetMethods();
        //    List<string> v = methods.Where(m => m.Name == "Add" || m.Name == "Edit" || m.Name == "Del").Select(m => m.Name).ToList();

        //    Console.Clear();

        //    var sl = UI.Select(v, 10, 10, "Select Method");

        //    MethodInfo mI = note.GetType().GetMethod(sl.ToString());
        //    var alls = mI.Invoke(note, null);
        //}
    }
}


