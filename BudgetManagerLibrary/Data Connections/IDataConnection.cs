using BudgetManagerLibrary.Business_Objects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace BudgetManagerLibrary
{
    public interface IDataConnection
    {
        Budget SaveBudget(Budget budget);
        //Expense SaveExpense(Expense expense);
        //Income SaveIncome(Income income);

        //List<Expense> GetExpenses();
        //List<Income> GetIncomes();
        List<Budget> GetBudgets();
        List<Entry> GetEntries();
        List<Entry> GetEntriesByDate(int budgetId);
        Entry SaveEntry(Entry entry);
        void EditBudgetBalance(int budgetId, decimal budgetBalance);
        void DeleteBudget(int budgetId);
        decimal GetExpensesByMonth(int month, int budgetId);
        void deleteEntry(Entry entry);
    }
}
