import axios from '../axios/axios';
import addAuthHeader from './AuthHeader';

const getBudgetGroups = () => {
    return axios.get('/api/BudgetGroup', { headers: addAuthHeader() });
};

export default {
    getBudgetGroups
};