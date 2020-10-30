import React, { useState, useEffect } from 'react';

import getBudgetGroups from '../../services/UserService';

function Budget(props) {

    const [budgetGroups, setBudgetGroups] = useState([]);
    const [isAuthorized, setIsAuthorized] = useState(false);


    useEffect(() => {
        async function fetchBudgetGroups() {
            try {
                const response = await getBudgetGroups();
                setBudgetGroups(response);
                setIsAuthorized(true);

            } catch (error) {

                if (error === 401 || 404) {
                    console.log('UserService.getBudgetGroups error response:');
                    console.log(error);
                    window.location.href = '/login';
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
                {budgetGroups && isAuthorized &&
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