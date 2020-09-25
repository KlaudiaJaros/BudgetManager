using BudgetManagerLibrary.Business_Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetManagerLibrary
{
    public class Income : Entry
    {
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
        public override string ToString()
        {
            string editedName = Name.TrimEnd();
            return editedName;
        }
    }
}
