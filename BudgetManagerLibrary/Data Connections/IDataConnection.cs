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
        List<Budget> GetBudgets();
        List<Entry> GetEntries();
        List<Entry> GetEntriesByDate(int budgetId);
        Entry SaveEntry(Entry entry);
        void EditBudgetBalance(int budgetId, decimal budgetBalance);
        void DeleteBudget(int budgetId);
        decimal GetSpendByMonth(int month, int budgetId);
        void DeleteEntry(Entry entry);
    }
}
