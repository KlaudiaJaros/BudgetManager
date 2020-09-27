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
            string editedName = Name.TrimEnd();
            return editedName;
        }
    }
}
