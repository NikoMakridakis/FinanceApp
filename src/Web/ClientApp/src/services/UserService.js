import axios from '../axios/axios';
import AuthService from './AuthService';

function getBudgetGroups() {
    return axios
        .get('/api/BudgetGroup', { headers: AuthService.addAuthHeader() })
        .then((response) => {
            console.log('UserService getBudgetGroups response:');
            console.log(response);
            return response.data;
        })
        .catch((error) => {
            const errorStatusCode = error.response.status;
            if (errorStatusCode === 401 || 404) {
                console.log('login response catch error status code: ');
                console.log(errorStatusCode);
                return errorStatusCode
            }
        })
};

export default getBudgetGroups;
