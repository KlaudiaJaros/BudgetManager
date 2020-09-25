using BudgetManagerLibrary.Business_Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetManagerLibrary
{
    public class Expense : Entry
    {
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
        public override string ToString()
        {
            string editedName = Name.TrimEnd();
            return editedName;
        }
    }
}
