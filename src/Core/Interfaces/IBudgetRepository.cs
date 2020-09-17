﻿using Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IBudgetRepository
    {
        Task<Budget> GetBudgetByIdAsync(int budgetId);
        Task<IReadOnlyList<Budget>> GetBudgetsAsync();
    }
}
