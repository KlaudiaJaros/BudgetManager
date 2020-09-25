using BudgetManagerLibrary.Business_Objects;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace BudgetManagerLibrary 
{
    public class TextConnector : IDataConnection
    {
        // csv/txt file name :
        private string fileName;

        public void DeleteBudget(int budgetId)
        {
            // delete budget's text file with expenses and incomes:
            fileName = "Budget no " + budgetId + ".csv";
            string path = $"{ ConfigurationManager.AppSettings["filePath"]}\\{fileName}";
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            
            // delete budget entry from all budgets txt file:
            fileName = "List of all budgets.csv";
            path = $"{ ConfigurationManager.AppSettings["filePath"]}\\{fileName}";
            
            if (File.Exists(path))
            {
                string targetLine = "BUDGET," + budgetId.ToString();
                string[] lines = File.ReadAllLines(path);
                List<string> newLines = new List<string>();

                foreach (string line in lines)
                {
                    if (!line.Contains(targetLine))
                    {
                        newLines.Add(line);
                    }
                }
                File.Delete(path);
                File.AppendAllLines(path, newLines);
            }
        }

        public void EditBudgetBalance(int budgetId, decimal budgetBalance)
        {
            fileName = "List of all budgets.csv";
            string path = $"{ ConfigurationManager.AppSettings["filePath"]}\\{fileName}";

            if (File.Exists(path))
            {
                string search = "BUDGET," + budgetId.ToString();
                string[] lines = File.ReadAllLines(path);
                List<string> newLines = new List<string>();

                foreach (string line in lines)
                {
                    if (!line.Contains(search))
                    {
                        newLines.Add(line);
                    }
                    else if (line.Contains(search))
                    {
                        string[] words = line.Split(',');
                        newLines.Add($"BUDGET,{budgetId},{words[2]},{budgetBalance}");
                    }
                }
                File.Delete(path);
                File.AppendAllLines(path, newLines);
            }
        }

        public List<Budget> GetBudgets()
        {
            throw new NotImplementedException();
        }

        public List<Entry> GetEntries()
        {
            throw new NotImplementedException();
        }

        public List<Entry> GetEntriesByDate(int budgetId)
        {
            throw new NotImplementedException();
        }

        public List<Expense> GetExpenses()
        {
            throw new NotImplementedException();
        }

        public List<Income> GetIncomes()
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
            lines.Add($"BUDGET,{ budget.Id },{ budget.Name },{budget.Balance}");

            File.AppendAllLines(path, lines);

        return budget;
        }

        public Entry SaveEntry(Entry entry)
        {
            fileName = "Budget no " + entry.BudgetID + ".csv";

            // save the csv file path:
            string path = $"{ ConfigurationManager.AppSettings["filePath"]}\\{fileName}";

            // convert expense into csv list :
            List<string> lines = new List<string>();
            lines.Add($"EXPENSE,{ entry.Id },{ entry.Name },{ entry.Amount},{ entry.Category},{ entry.Date},{ entry.BudgetID}");

            // save to file:
            File.AppendAllLines(path, lines);

            return entry;
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
