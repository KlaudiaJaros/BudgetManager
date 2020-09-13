using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetManagerLibrary
{
    public interface IDataConnection
    {
        Budget CreateBudget(Budget budget);
    }
}
