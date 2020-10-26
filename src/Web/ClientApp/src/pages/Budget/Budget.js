import React, { useState, useEffect } from 'react';

import UserService from '../../services/UserService';

function Budget() {

    const [budgetGroups, setBudgetGroups] = useState([]);

    useEffect(() => {
        fetchBudgetGroups();
    }, []);

    function fetchBudgetGroups() {
        UserService.getBudgetGroups()
            .then(response => {
                setBudgetGroups(response);
                console.log('getBudgetGroups from Budget.js');
                console.log(response);
            })
            .catch(error => {
                console.log(error);
            });
    };

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
    );
};

export default Budget;