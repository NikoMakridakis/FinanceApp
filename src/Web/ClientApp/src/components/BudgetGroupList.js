import React, { useState, useEffect } from 'react';
import axios from 'axios';

const API_URL = 'https://localhost:44387'

function BudgetGroupList() {

    const budgetGroupId = 1;

    const [budgetGroups, setBudgetGroups] = useState([]);

    useEffect(() => {
        async function fetchBudgetGroups() {
            axios.get(API_URL + budgetGroupId)
                .then(response => {
                    console.log(response);
                    setBudgetGroups(response.data);
                })
                .catch(error => {
                    console.log(error);
                })
        }
        fetchBudgetGroups();
    }, []);

    return (
        <p>
            {budgetGroups.map(item => (
                <li key={item.budgetGroupId}>
                    {item.budgetGroupTitle}
                </li>
            ))}
        </p>
    );
}

export default BudgetGroupList;

