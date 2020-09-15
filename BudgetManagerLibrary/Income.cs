using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetManagerLibrary
{
    public class Income
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Amount { get; set; }
        public DateTime? Date { get; set; }
        public int BudgetID { get; set; }

        public Income(string name, double amount, DateTime? date, int budget)
        {
            Name = name;
            Amount = amount;
            Date= date;
            BudgetID = budget;
        }
        public Income()
        {

        }
    }
}
