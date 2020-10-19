import axios from 'axios';
import AuthHeaderService from './AuthHeaderService';

const API_URL = 'https://localhost:44387'

const getPublicContent = () => {
    return axios.get(API_URL + 'all');
};

const getBudgetList = () => {
    return axios.get(API_URL + '/api/BudgetGroup', { headers: AuthHeaderService() });
};

export default {
    getPublicContent,
    getBudgetList
};