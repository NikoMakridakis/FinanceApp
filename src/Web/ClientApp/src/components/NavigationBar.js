import React, { useState } from 'react';
import { useHistory } from 'react-router-dom';
import { makeStyles } from '@material-ui/core/styles';
import AppBar from '@material-ui/core/AppBar';
import Toolbar from '@material-ui/core/Toolbar';
import Typography from '@material-ui/core/Typography';
import Button from '@material-ui/core/Button';
import IconButton from '@material-ui/core/IconButton';
import MenuIcon from '@material-ui/icons/Menu';
import NavigationDrawer from "./NavigationDrawer";

const useStyles = makeStyles({
    root: {
        flexGrow: 1,
    },
    bar: {
        backgroundColor: '#b8de6f',
    },
    menuButton: {
        marginRight: '2px',
    },
    title: {
        flexGrow: 1,
    },
});


function NavigationBar() {

    const classes = useStyles();
    const history = useHistory();
    const navigateToLogin = () => history.push('/login');

    const [drawerIsOpen, toggleDrawer] = useState(false);

    function handleToggle(bool) {
        toggleDrawer(bool);
    };

    return (
        <div className={classes.root}>
            <AppBar position="static" className={classes.bar}>
                <Toolbar>
                    <IconButton onClick={handleToggle} className={classes.menuButton} edge="start" color="inherit" aria-label="menu">
                        <MenuIcon />
                    </IconButton>
                    <Typography variant="h6" className={classes.title}>
                        BudgetApp
                    </Typography>
                    <Button onClick={navigateToLogin} color="inherit">Login</Button>
                </Toolbar>
            </AppBar>

            <NavigationDrawer
                drawerIsOpen={drawerIsOpen}
            />
        </div>
    );
}

export default NavigationBar;
