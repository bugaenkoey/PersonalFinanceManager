using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinanceManager.Data
{
    class Note : INote
    {

        /// <summary>
        /// /
        /// </summary>
        public Note()
        {
            id = count++;
        }
        public Note(string description, ProfitCost profit, PlanDone plan) : this()
        {
          //  id = count++;
            Description = description;
            ProfitCost = profit;
            PlanDone = plan;
        }

        public Note(/*int id,*/ string description, decimal sum, DateTime time, ProfitCost profit, PlanDone plan) 
            : this(description, profit, plan)
        {
            Time = time;
            Sum = sum;
        }
        //public static int count = 1;
        //public int id;
        //public DateTime? Time;
        //public decimal? Sum;
        //public string Description = string.Empty;
        //public ProfitCost ProfitCost;
        //public PlanDone PlanDone;

        public static int count = 1;
        public int id { get; set; }
        public DateTime? Time { get; set; }
        public decimal? Sum { get; set; }
        public string Description { get; set; }
        public ProfitCost ProfitCost { get; set; }
        public PlanDone PlanDone { get; set; }



        //  private static List<INote> notes = new List<INote>();
        public override string ToString()
        {
            //  return $"id: {id}\tTime:{Time}\tSum: {Sum}\tDone: {Done}\tPlan: {Plan}\tDesc: {Description}\tPlan: {Plan}";
            return $"id: {id}  Desc: {Description}  Sum: {Sum}  Time:{Time}  Done: {ProfitCost}  Plan: {PlanDone}";
        }

        //public  Note Add(Note note)
        //{
        //    Console.Clear();
        //    Console.WriteLine("Add Method");
        //    Console.ReadKey();
        //    return new Note();
        //}

        //public  void Edit(Note note)
        //{
        //    Console.Clear();
        //    Console.WriteLine("Edit Method");
        //    Console.ReadKey();
        //}
        //public  void Del(Note note)
        //{
        //    Console.Clear();
        //    Console.WriteLine("Del Method");

        //    Console.ReadKey();
        //}
    }
}
