using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinanceManager.Data
{
    class Note 
    {
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
            return $"id:{id} Time:{Time} Done:{ProfitCost}\t Plan:{PlanDone}\t Desc:{Description}\t Sum:{Sum}";
        }

    }
}
