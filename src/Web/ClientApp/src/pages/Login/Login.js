import React, { useState } from 'react';
import { useForm } from 'react-hook-form';
import { makeStyles } from '@material-ui/core/styles';
import { FormControlLabel } from '@material-ui/core';
import Avatar from '@material-ui/core/Avatar';
import Button from '@material-ui/core/Button';
import CssBaseline from '@material-ui/core/CssBaseline';
import TextField from '@material-ui/core/TextField';
import Checkbox from '@material-ui/core/Checkbox';
import Link from '@material-ui/core/Link';
import Grid from '@material-ui/core/Grid';
import Box from '@material-ui/core/Box';
import LockOutlinedIcon from '@material-ui/icons/LockOutlined';
import Typography from '@material-ui/core/Typography';
import Container from '@material-ui/core/Container';
import WarningRoundedIcon from '@material-ui/icons/WarningRounded';

import Copyright from '../../components/Copyright';
import AuthService from '../../services/AuthService';

const useStyles = makeStyles((theme) => ({
    paper: {
        marginTop: theme.spacing(8),
        display: 'flex',
        flexDirection: 'column',
        alignItems: 'center',
    },
    avatar: {
        margin: theme.spacing(1),
        backgroundColor: theme.palette.secondary.main,
    },
    form: {
        width: '100%',
        marginTop: theme.spacing(1),
    },
    submit: {
        margin: theme.spacing(3, 0, 2),
    },
    forgotPassword: {
        textAlign: 'right',
        margin: 'auto',
    },
    row: {
        display: 'flex',
        flexDirection: 'row',
        alignItems: 'center',
    },
    check: {
        marginLeft: '-12px',
    },
    warningIcon: {
        color: '#DC004E',
        fontSize: '16px',
        marginRight: '6px',

    },
    warningText: {
        color: '#DC004E',
    },
}))

function Login(props) {

    const classes = useStyles();

    const [isLoginError, setIsLoginError] = useState(false);
    const [isLockedOut, setIsLockedOut] = useState(false);
    const [countdownIsActive, setCountdownIsActive] = useState(false);
    const [lockoutTimeLeft, setLockoutTimeLeft] = useState(0);
    const [checkBox, setCheckBox] = useState(true);

    const { register, handleSubmit, errors } = useForm();

    function onChangeCheckBox() {
        setCheckBox(!checkBox);
    }

    function navigateToRegister(event) {
        event.preventDefault();
        props.history.push('/register');
    }

    function navigateToReset(event) {
        event.preventDefault();
        props.history.push('/reset');
    }

    function lockoutCountdown(time) {
        const newTime = time - 1;
        setLockoutTimeLeft(newTime);
        console.log(newTime);

        setTimeout(() => {
            if (newTime === 1 || newTime < 1) {
                setIsLockedOut(false);
                setCountdownIsActive(false);
                return;
            }
            lockoutCountdown(newTime)
        }, 1000);
    }

    async function onSubmit(data) {
        console.log(data);
        const staySignedIn = data.staySignedIn;
        if (staySignedIn === true) {
            try {
                if (!isLockedOut) {
                    const response = await AuthService.loginForStaySignedIn(data.email, data.password);
                    if (response === 200) {
                        setIsLoginError(false);
                        setIsLockedOut(false);
                        props.history.push('/budget');
                    } if (response === 401 || 404) {
                        setIsLoginError(true);
                        setIsLockedOut(false);
                    } if (response.isLockedOut) {
                        setIsLoginError(false);
                        setIsLockedOut(true);
                        if (!countdownIsActive) {
                            setCountdownIsActive(true);
                            if (response.lockoutSeconds > 0) {
                                lockoutCountdown(response.lockoutSeconds);
                            }
                        }
                    }
                }
            } catch (error) {
                console.log(error);
            }
        } else if (staySignedIn === false) {
            try {
                const response = await AuthService.loginForNotStaySignedIn(data.email, data.password);
                if (response === 200) {
                    setIsLoginError(false);
                    setIsLockedOut(false);
                    props.history.push('/budget');
                } else if (response === 401 || 404) {
                    setIsLoginError(true);
                    setIsLockedOut(false);
                } else if (response.isLockedOut) {
                    setIsLoginError(true);
                    setIsLockedOut(true);
                }
            } catch (error) {
                console.log(error);
            }
        }
    }

    return (
        <Container component='main' maxWidth='xs'>
            <CssBaseline />
            <div className={classes.paper}>
                <Avatar className={classes.avatar}>
                    <LockOutlinedIcon />
                </Avatar>
                <Typography component='h1' variant='h5'>
                    Sign in
                </Typography>
                <form onSubmit={handleSubmit(onSubmit)} className={classes.form} noValidate>
                    <TextField
                        inputRef={register({
                            required: 'Email is required',
                            pattern: {
                                // regex pattern below verifies email as per RFC2822 standards
                                // source: https://regexr.com/5em0n
                                value: /[a-zA-Z0-9!#$ %& '*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&' * +/=?^_`{|}~-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?/,
                                message: 'Invalid email address'
                            }
                        })}
                        name='email'
                        variant='outlined'
                        margin='normal'
                        fullWidth
                        id='email'
                        label='Email Address'
                        autoComplete='email'
                        autoFocus
                    />
                    {errors.email &&
                        <Box className={classes.row}>
                            <WarningRoundedIcon className={classes.warningIcon} />
                            <Typography className={classes.warningText}>{errors.email.message}</Typography>
                        </Box>
                    }
                    {isLoginError &&
                        <Box className={classes.row}>
                            <WarningRoundedIcon className={classes.warningIcon} />
                            <Typography className={classes.warningText}>Incorrect email or password.</Typography>
                        </Box>
                    }
                    {isLockedOut &&
                        <Box className={classes.row}>
                            <WarningRoundedIcon className={classes.warningIcon} />
                            <Typography className={classes.warningText}>Please try again in {lockoutTimeLeft} seconds.</Typography>
                        </Box>
                    }
                    <TextField
                        inputRef={register}
                        name='password'
                        variant='outlined'
                        margin='normal'
                        fullWidth
                        label='Password'
                        type='password'
                        id='password'
                        autoComplete='current-password'
                    />
                    <Grid container>
                        <Grid item xs className={classes.row}>
                            <FormControlLabel
                                control={
                                    <Checkbox
                                        inputRef={register}
                                        name='staySignedIn'
                                        color='primary'
                                        checked={checkBox}
                                        onChange={onChangeCheckBox}
                                    />}
                                label='Stay signed in'
                            />
                        </Grid>
                        <Grid item xs className={classes.forgotPassword}>
                            <Link href='' variant='body2' onClick={navigateToReset}>
                                <Typography>
                                    Forgot password?
                                </Typography>
                            </Link>
                        </Grid>
                    </Grid>
                    <Button
                        type='submit'
                        fullWidth
                        variant='contained'
                        color='primary'
                        className={classes.submit}
                    >
                        Sign In
                    </Button>
                    <Box>
                        <Typography align='center' variant='body1'>
                            { 'Don\'t have an account? ' }
                            <Link href='' variant='body1' onClick={navigateToRegister}>
                                    {'Sign Up'}
                            </Link>
                        </Typography>
                    </Box>
                </form>
            </div>
            <Box mt={8}>
                <Copyright />
            </Box>
        </Container>
    )
}

export default Login;
