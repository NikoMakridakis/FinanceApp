import React from 'react';
import { makeStyles } from '@material-ui/core/styles';
import Button from '@material-ui/core/Button';
import CssBaseline from '@material-ui/core/CssBaseline';
import Menu from '@material-ui/core/Menu';
import MenuItem from '@material-ui/core/MenuItem';
import Fade from '@material-ui/core/Fade';
import MenuIcon from '@material-ui/icons/Menu';
import Typography from '@material-ui/core/Typography';
import Box from '@material-ui/core/Box';
import Link from '@material-ui/core/Link';
import AccountBalanceIcon from '@material-ui/icons/AccountBalance';


const useStyles = makeStyles({
    layout: {
        display: 'flex',
        flexDirection: 'row',
        alignItems: 'center',
        justifyContent: 'space-between',
        paddingTop: '20px',
        paddingLeft: '30px',
        paddingRight: '30px'
    },
    logo: {
        color: '#3a3a3a',

        '&:hover': {
            textDecoration: 'none',
            opacity: '80%'
        }
    },
    customMenu: {
        '& div': {
            width: '95%',
            borderRadius: '8px'
        }
    },
    menuTitle: {
        fontSize: '1.1rem',
        color: '#8898AA'
    },
    menuIconButton: {
        borderRadius: 25,
        maxWidth: '48px',
        minWidth: '48px',
        maxHeight: '32px',
        minHeight: '32px',
        background: '#D3D3D3',
        opacity: '25%'
    },
    menuIconSpace: {
        marginLeft: '5px',
        paddingRight: '8px',
    },
});

function NavigationBar() {

    const classes = useStyles();

    const [anchorEl, setAnchorEl] = React.useState(null);
    const open = Boolean(anchorEl);

    const handleClick = (event) => {
        setAnchorEl(event.currentTarget);
    };

    const handleClose = () => {
        setAnchorEl(null);
    };

    return (
        <div>
            <CssBaseline />
            <div className={classes.layout}>
                <Link className={classes.logo} href="#" component="button" variant="h5" disableUnderline={true}>
                    <Typography variant="h5" >
                        <Box fontWeight="fontWeightBold">
                            BudgetApp
                        </Box>
                    </Typography>
                </Link>
                
                <Button className={classes.menuIconButton} aria-controls="fade-menu" aria-haspopup="true" onClick={handleClick}>
                    <MenuIcon />
                </Button>
                <Menu
                    id="fade-menu"
                    anchorEl={anchorEl}
                    keepMounted
                    open={open}
                    onClose={handleClose}
                    TransitionComponent={Fade}
                    className={classes.customMenu}
                >
                    <MenuItem onClick={handleClose}>
                        <Typography className={classes.menuTitle} variant="subtitle2">
                            <Box fontWeight="fontWeightBold">
                                SERVICES
                            </Box>
                        </Typography>
                    </MenuItem>
                    <MenuItem onClick={handleClose}>
                        <AccountBalanceIcon className={classes.menuIconSpace} />
                        <Box fontWeight="fontWeightBold" color="#0A2540">
                            Budget
                        </Box>
                    </MenuItem>
                    <MenuItem onClick={handleClose}>Spending</MenuItem>
                    <MenuItem onClick={handleClose}>Trends</MenuItem>
                </Menu>
            </div>
        </div>

    );
}

export default NavigationBar;
