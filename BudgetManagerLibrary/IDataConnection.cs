using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetManagerLibrary
{
    public interface IDataConnection
    {
        Budget SaveBudget(Budget budget);
        Expense SaveExpense(Expense expense);
        Income SaveIncome(Income income);
    }
}
