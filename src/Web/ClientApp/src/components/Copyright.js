import React from 'react';
import { useHistory } from "react-router-dom";
import { makeStyles } from '@material-ui/core/styles';
import Typography from '@material-ui/core/Typography';
import Link from '@material-ui/core/Link';

const useStyles = makeStyles((theme) => ({
    websiteLink: {
        cursor: 'pointer'
    }
}));

function Copyright() {

    const classes = useStyles();

    const history = useHistory();

    const navigateToHome = () => history.push('/home');

    return (
        <Typography variant="body2" color="textSecondary" align="center">
            {'Copyright © '}
            <Link color="inherit" onClick={navigateToHome} className={classes.websiteLink}>
                StarBudget
            </Link>{' '}
            {new Date().getFullYear()}
        </Typography>
    );
}

export default Copyright;