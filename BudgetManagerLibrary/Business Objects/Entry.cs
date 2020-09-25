using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetManagerLibrary.Business_Objects
{
    public class Entry
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Amount { get; set; }
        public DateTime? Date { get; set; }
        public string Category { get; set; }
        public int BudgetID { get; set; }

        public Entry(int id, string name, double amount, string category, DateTime? date, int budgetID)
        {
            Id = id;
            Name = name;
            Amount = amount;
            Category = category;
            Date = date;
            BudgetID = budgetID;
        }
        public Entry(string name, double amount, string category, DateTime? date, int budgetID)
        {
            Name = name;
            Amount = amount;
            Category = category;
            Date = date;
            BudgetID = budgetID;
        }
        public Entry()
        {

        }
    }
}
