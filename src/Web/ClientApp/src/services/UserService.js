import axios from '../axios/axios';
import AuthService from './AuthService';

async function getBudgetGroups() {
    try {
        console.log('getBudgetGroups request sent from UserService.getBudgetGroups');
        const response = await axios
            .get('/api/BudgetGroup',
                {
                    headers: AuthService.addAuthHeader()
                })
        console.log(`UserService getBudgetGroups response: ${response.data}`);
        return response.data;
    } catch (error) {
        console.log(error);
    }
}

export default {
    getBudgetGroups
};