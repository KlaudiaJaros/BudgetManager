using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetManagerLibrary
{
     public class Budget
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
        
        private List<Expense> Expenses = new List<Expense>();

        public Budget(System.Int32 budgetId, System.String name, System.Decimal balance)
        {
            Id = budgetId;
            Name = name;
            Balance = balance;
        }
        public Budget(string name, decimal balance)
        {
            Name = name;
            Balance = balance;
        }

        public override string ToString()
        {
            return Name;
        }


    }
}
