import axios from '../axios/axios';
import AuthService from './AuthService';

async function getBudgetGroups() {
    try {
        const response = await axios.get('/api/BudgetGroup', { headers: AuthService.addAuthHeader() });
        const responseStatusCode = response.status;
        if (responseStatusCode === 200) {
            localStorage.setItem('isAuthenticated', true);
            console.log('BudgetService.getBudgetGroups response:');
            console.log(response);
            return responseStatusCode;
        }
    } catch (error) {
        const errorStatusCode = error.response.status;
        if (errorStatusCode === 400 || 401 || 404) {
            localStorage.setItem('isAuthenticated', false);
            console.log('BudgetService.getBudgetGroups response catch error status code:');
            console.log(errorStatusCode);
            return errorStatusCode
        }
    }
}

export default getBudgetGroups;
