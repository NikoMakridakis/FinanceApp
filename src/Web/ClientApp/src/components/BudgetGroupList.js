import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { makeStyles } from '@material-ui/core/styles';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableContainer from '@material-ui/core/TableContainer';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import Paper from '@material-ui/core/Paper';

const useStyles = makeStyles({
    table: {
        minWidth: 350,
    },
});

function BudgetGroupList() {

    const classes = useStyles();

    const [data, setData] = useState({ budgetGroups: [] });

    useEffect(() => {
        const fetchData = async () => {
            const result = await axios(
                'https://localhost:44387/api/BudgetGroup?UserId=1',
            );

            setData(result.data);
        };

        fetchData();
    }, []);

    return (
        <ul>
            {data.budgetGroups.map(item => (
                <li key={item.budgetGroupId}>
                    <p>{item.budgetGroupTitle}</p>
                </li>
            ))}
        </ul>
    );
}

export default BudgetGroupList;