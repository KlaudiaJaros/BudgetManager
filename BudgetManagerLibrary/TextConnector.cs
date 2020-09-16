using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;

namespace BudgetManagerLibrary 
{
    public class TextConnector : IDataConnection
    {
        // csv file :
        private string fileName;

        public List<Budget> GetBudgets()
        {
            throw new NotImplementedException();
        }

        public List<Expense> GetExpenses()
        {
            throw new NotImplementedException();
        }

        public Budget SaveBudget(Budget budget)
        {
            fileName = "List of all budgets.csv";
            // save the csv file path:
            string path = $"{ ConfigurationManager.AppSettings["filePath"]}\\{fileName}";

            // convert budget into csv list :
            List<string> lines = new List<string>();
            lines.Add($"BUDGET,{ budget.Id },{ budget.Name }");

            File.AppendAllLines(path, lines);

        return budget;
        }

        public Expense SaveExpense(Expense expense)
        {
            fileName = "Budget no " + expense.BudgetID + ".csv";
            
            // save the csv file path:
            string path = $"{ ConfigurationManager.AppSettings["filePath"]}\\{fileName}";

            // convert expense into csv list :
            List<string> lines = new List<string>();
            lines.Add($"EXPENSE,{ expense.Id },{ expense.Name },{ expense.Amount},{ expense.Category},{ expense.Date},{ expense.BudgetID}");

            // save to file:
            File.AppendAllLines(path, lines);

            
            return expense;
        }

        public Income SaveIncome(Income income)
        {
            // TODO: implement SaveIncome in TextConnector
            fileName = "Budget no " + income.BudgetID + ".csv";
            // save the csv file path:
            string path = $"{ ConfigurationManager.AppSettings["filePath"]}\\{fileName}";

            // convert income into csv list :
            List<string> lines = new List<string>();
            lines.Add($"INCOME,{ income.Id },{ income.Name },{ income.Amount},{ income.Date},{ income.BudgetID}");

            // save to file:
            File.AppendAllLines(path, lines);

            return income;
        }
    }
}
