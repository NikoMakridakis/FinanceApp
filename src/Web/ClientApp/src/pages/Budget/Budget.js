import React, { useState, useEffect } from 'react';

import getBudgetGroups from '../../services/BudgetService';

function Budget() {

    const [budgetGroups, setBudgetGroups] = useState([]);

    useEffect(() => {
        let isLoading = true;
        async function fetchBudgetGroups() {
            if (isLoading === true) {
                const response = await getBudgetGroups();
                try {
                    setBudgetGroups(response);

                } catch (error) {
                    if (error === 400 || 401 || 404) {
                        console.log('BudgetService.getBudgetGroups error response:');
                        console.log(error);
                    }
                }
                return () => {
                    isLoading = false;
                };
            }
        }
        fetchBudgetGroups();
    }, []);

    

    return (
        <div>
            <h1>
                Private Budget
            </h1>
            <ul>
                {budgetGroups &&
                    budgetGroups.map((budget) => (
                        <li key={budget.budgetGroupId}>
                            {budget.budgetGroupTitle}
                        </li>
                    ))}
            </ul>
        </div>
    )
}

export default Budget;