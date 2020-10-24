import axios from '../axios/axios';
import AuthHeader from './AuthHeader';

const getBudgetGroups = () => {
    return axios.get('/api/BudgetGroup', { headers: AuthHeader() });
};

export default {
    getBudgetGroups
};