import React, { useState, useEffect } from 'react';

import getBudgetGroups from '../../services/UserService';

function Budget() {

    const [budgetGroups, setBudgetGroups] = useState([]);


    useEffect(() => {
        async function fetchBudgetGroups() {
            try {
                const response = await getBudgetGroups();
                setBudgetGroups(response);

            } catch (error) {

                if (error === 401 || 404) {
                    console.log('UserService.getBudgetGroups error response:');
                    console.log(error);
                }

                console.log(error);
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