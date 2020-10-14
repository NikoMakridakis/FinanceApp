import React from 'react';
import { connect } from 'react-redux';
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

function createData(title, amount) {
    return { title, amount };
}

const rows = [
    createData('Housing', 1000),
    createData('Food', 500),
    createData('Travel', 200),
];


function BudgetGroupList() {

    let budgetGroups = [];

    const classes = useStyles();

    return (
        <TableContainer component={Paper}>
            <Table className={classes.table} aria-label="simple table">
                <TableHead>
                    <TableRow>
                        <TableCell align="center">Title</TableCell>
                        <TableCell align="center">Total</TableCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                    {rows.map((row) => (
                        <TableRow key={row.name}>
                            <TableCell align="center">{row.title}</TableCell>
                            <TableCell align="center">{row.amount}</TableCell>
                        </TableRow>
                    ))}
                </TableBody>
            </Table>
        </TableContainer>
    );
}


// The mapStateToProps function is mapping the data property from the repositoryReducer's initialState object to
// the data property inside the BudgetGroupList component. The data can be accessed with props.data.

const mapStateToProps = (state) => {
    return {
        data: state.data
    }
}


// The mapDispatchToProps function is creating an additional property onGetData, which can be called with props.onGetData.
// Then, the action is dispatched inside the repositoryAction.js file which will fetch the data from the server.

const mapDispatchToProps = (dispatch) => {
    return {
        onGetData: (url, props) => dispatch(repositoryActions.getData(url, props))
    }
}
export default connect(mapStateToProps, mapDispatchToProps)(OwnerList);

export default BudgetGroupList;