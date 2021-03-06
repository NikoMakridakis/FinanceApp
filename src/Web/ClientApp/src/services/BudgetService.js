﻿import axios from '../axios/axios';
import AuthService from './AuthService';

async function getBudgetGroups() {
    try {
        const response = await axios.get('/api/BudgetGroup', { headers: AuthService.addAuthHeader() });
        const responseStatusCode = response.status;
        if (responseStatusCode === 200) {
            localStorage.setItem('isAuthenticated', true);
            console.log('BudgetService.getBudgetGroups response:');
            console.log(response);
            return response;
        }
    } catch (error) {
        const errorStatusCode = error.response.status;
        if (errorStatusCode === 400 || 401 || 404) {
            localStorage.removeItem('user');
            localStorage.removeItem('isAuthenticated');
            console.log('BudgetService.getBudgetGroups response catch error status code:');
            console.log(error.response);
            return error.response;
        }
    }
}

async function postBudgetGroup(budgetGroup) {
    try {
        const response = await axios.post('/api/BudgetGroup',
            {
                headers: AuthService.addAuthHeader(),
                data: budgetGroup
            });
            
        const responseStatusCode = response.status;
        if (responseStatusCode === 200) {
            localStorage.setItem('isAuthenticated', true);
            console.log('BudgetService.getBudgetGroups response:');
            console.log(response);
            return response;
        }
    } catch (error) {
        const errorStatusCode = error.response.status;
        if (errorStatusCode === 400 || 401 || 404) {
            localStorage.removeItem('user');
            localStorage.removeItem('isAuthenticated');
            console.log('BudgetService.getBudgetGroups response catch error status code:');
            console.log(error.response);
            return error.response;
        }
    }
}

export default {
    getBudgetGroups,
    postBudgetGroup,
}
