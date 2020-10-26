import axios from '../axios/axios';
import AuthService from './AuthService';

function getBudgetGroups() {
    return axios.get('/api/BudgetGroup', { headers: AuthService.addAuthHeader() })
        .then((response) => {
            console.log('UserService getBudgetGroups response:');
            console.log(response);
            return response.data;
        }, (error) => {
        console.log(error);
        });
};

export default {
    getBudgetGroups
};