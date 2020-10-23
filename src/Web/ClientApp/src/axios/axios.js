import axios from 'axios';

export default axios.create({
    baseURL: 'https://localhost:44387',
    headers: {
        'Content-Type': 'application/json',
    }
});