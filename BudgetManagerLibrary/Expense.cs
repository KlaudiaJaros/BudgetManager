using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetManagerLibrary
{
    public class Expense
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Amount { get; set; }
        public string Category { get; set; }
        public DateTime? Date { get; set; }
        public int BudgetID { get; set; }

        public Expense(int id, string name, double amount, string category, DateTime? date, int budgetID)
        {
            Id = id;
            Name = name;
            Amount = amount;
            Category = category;
            Date = date;
            BudgetID = budgetID;
        }
        // constructor without id:
        public Expense(string name, double amount, string category, DateTime? date, int budgetID)
        {
            Name = name;
            Amount = amount;
            Category = category;
            Date = date;
            BudgetID = budgetID;
        }
        public Expense()
        {

        }
    }
}
