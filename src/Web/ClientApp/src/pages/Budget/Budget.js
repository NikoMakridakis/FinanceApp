import React, { useState, useEffect } from 'react';
//import clamp from 'lodash-es/clamp'
//import swap from 'lodash-move'
//import { useGesture } from 'react-use-gesture'
//import { useSprings, animated, interpolate } from 'react-spring'

import BudgetService from '../../services/BudgetService';

//const fn = (order, down, originalIndex, curIndex, y) => index =>
//    down && index === originalIndex
//        ? { y: curIndex * 100 + y, scale: 1.1, zIndex: '1', shadow: 15, immediate: n => n === 'y' || n === 'zIndex' }
//        : { y: order.indexOf(index) * 100, scale: 1, zIndex: '0', shadow: 1, immediate: false }

function Budget() {

    const [budgetGroups, setBudgetGroups] = useState([]);

    //function navigateToLogin() {
    //    props.history.push('/login');
    //}

    useEffect(() => {
        async function fetchData() {
            const data = await fetchBudgetGroups();
            console.log(data);
            setBudgetGroups(data);
        }
        fetchData();
    }, []);

    async function fetchBudgetGroups() {
        try {
            const response = await BudgetService.getBudgetGroups();
            if (response.status === 200) {
                return response.data;
            }
            //if (response.status === 400 || 401 || 404) {
            //    navigateToLogin();
            //}
        } catch (error) {
            console.log(error);
        }
    }

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