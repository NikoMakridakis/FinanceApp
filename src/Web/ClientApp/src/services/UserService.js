import axios from 'axios';
import AuthHeader from './AuthHeader';

const API_URL = 'https://localhost:44387'

const getBudgetGroups = () => {
    return axios.get(API_URL + '/api/BudgetGroup', { headers: AuthHeader() });
};

export default {
    getBudgetGroups
};