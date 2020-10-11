import React from 'react';
import { makeStyles } from '@material-ui/core/styles';
import Button from '@material-ui/core/Button';
import Menu from '@material-ui/core/Menu';
import MenuItem from '@material-ui/core/MenuItem';
import Fade from '@material-ui/core/Fade';
import MenuIcon from '@material-ui/icons/Menu';
import Typography from '@material-ui/core/Typography';
import Box from '@material-ui/core/Box';

const useStyles = makeStyles({
    layout: {
        display: 'flex',
        flexDirection: 'row',
        alignItems: 'center',
        justifyContent: 'space-between',
        paddingTop: '18px',
        paddingLeft: '12px',
        paddingRight: '12px'
    },
    menuSize: {
        '& div': {
            width: '95%',
            borderRadius: '8px'
        }
    },
    menuIconButton: {
        borderRadius: 25,
        maxWidth: '48px',
        minWidth: '48px',
        maxHeight: '32px',
        minHeight: '32px',
        background: '#D3D3D3',
        opacity: '25%',
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
        <div className={classes.layout}>
            <Typography component="h1" variant="h5" >
                <Box fontWeight="fontWeightBold">
                    <a>
                        Budget App
                    </a>
                </Box>
            </Typography>
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
                className={classes.menuSize}
            >
                <MenuItem onClick={handleClose}>
                    <Typography variant="h6" >
                        <a>
                            Budget App
                        </a>
                    </Typography>
                </MenuItem>
                <MenuItem onClick={handleClose}>Budgets</MenuItem>
                <MenuItem onClick={handleClose}>Spending</MenuItem>
                <MenuItem onClick={handleClose}>Trends</MenuItem>
            </Menu>
        </div>
    );
}

export default NavigationBar;
