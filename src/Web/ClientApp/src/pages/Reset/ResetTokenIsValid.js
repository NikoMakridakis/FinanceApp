import React, { useState } from 'react';
import { useForm } from 'react-hook-form';
import { makeStyles } from '@material-ui/core/styles';
import Avatar from '@material-ui/core/Avatar';
import Button from '@material-ui/core/Button';
import CssBaseline from '@material-ui/core/CssBaseline';
import TextField from '@material-ui/core/TextField';
import Box from '@material-ui/core/Box';
import LockOutlinedIcon from '@material-ui/icons/LockOutlined';
import Typography from '@material-ui/core/Typography';
import Container from '@material-ui/core/Container';
import WarningRoundedIcon from '@material-ui/icons/WarningRounded';
import Link from '@material-ui/core/Link';
import CircularProgress from '@material-ui/core/CircularProgress';
import { createMuiTheme } from '@material-ui/core/styles'
import { ThemeProvider } from '@material-ui/styles';

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
    rowCenter: {
        display: 'flex',
        flexDirection: 'row',
        alignItems: 'center',
        justifyContent: 'center',
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
        fontSize: '14px',
    },
    buttonProgress: {
        color: '#FDFDFE',
    },
}))

const disabledButton = createMuiTheme({
    palette: {
        action: {
            disabledBackground: '#9EA7E2',
            disabled: '#FDFDFE',
        }
    }
});

function ResetTokenIsValid(props) {

    const [password, setPassword] = useState('');
    const [confirmPassword, setConfirmPassword] = useState('');
    const [passwordIsTooShort, setPasswordIsTooShort] = useState();
    const [passwordsDoNotMatch, setPasswordsDoNotMatch] = useState();
    const [buttonIsDisabled, setButtonIsDisabled] = useState(true);
    const [tokenIsValid, setTokenIsValid] = useState();
    const [hideInputTextFields, setHideInputTextFields] = useState(false);
    const [isLoading, setIsLoading] = useState(false);

    const { register, handleSubmit } = useForm();
    const delay = ms => new Promise(res => setTimeout(res, ms));
    const classes = useStyles();

    async function onChangePassword(input) {
        const password = input.target.value;
        setPassword(password);
        await delay(500);

        if (password.length === 0) {
            setPasswordIsTooShort(false);
            setButtonIsDisabled(true);
        } else if (password.length < 6) {
            setPasswordIsTooShort(true);
            setButtonIsDisabled(true);
        } else if (password.length === 6) {
            setPasswordIsTooShort(false);
            if (password === confirmPassword && confirmPassword !== '') {
                setButtonIsDisabled(false);
                setPasswordsDoNotMatch(false);
            } else if (password !== confirmPassword && confirmPassword !== '') {
                setButtonIsDisabled(true);
                setPasswordsDoNotMatch(true);
            }
        } else {
            setPasswordIsTooShort(false);
            if (password === confirmPassword && confirmPassword !== '') {
                setButtonIsDisabled(false);
                setPasswordsDoNotMatch(false);
            } else if (password !== confirmPassword && confirmPassword !== '') {
                setButtonIsDisabled(true);
                setPasswordsDoNotMatch(true);
            }
        }
    }

    async function onChangeConfirmPassword(input) {
        const confirmPassword = input.target.value;
        setConfirmPassword(confirmPassword);
        await delay(500);
        if (confirmPassword !== password) {
            setPasswordsDoNotMatch(true);
            setButtonIsDisabled(true);
        } else {
            setPasswordsDoNotMatch(false);
            setButtonIsDisabled(false);
        }
    }

    async function onSubmit(data) {
        setIsLoading(true);
        try {
            const email = await JSON.parse(localStorage.getItem('email'));
            const resetToken = await JSON.parse(localStorage.getItem('resetToken'));
            const response = await AuthService.resetPassword(email, data.password, data.confirmPassword, resetToken);
            if (response === 200) {
                await AuthService.loginForStaySignedIn(email, data.password);
                localStorage.removeItem('email');
                localStorage.removeItem('resetToken');
                setTokenIsValid(true);
                setHideInputTextFields(true);
                setIsLoading(false);
            } else if (response === 400 || 401 || 404) {
                localStorage.removeItem('email');
                localStorage.removeItem('resetToken');
                setTokenIsValid(false);
                setHideInputTextFields(true);
                setIsLoading(false);
            }
        } catch (error) {
            console.log(error);
        }
    }

    let button;
    if (tokenIsValid === true) {
        button =
            <Box mt={5}>
                <Typography align="center">You've successfully changed your password</Typography>
                <Button type='submit' color='primary' fullWidth variant='contained' className={classes.submit} onClick={props.navigateToWelcome}>
                    {isLoading === true &&
                        <CircularProgress size={24} className={classes.buttonProgress} />
                    }
                    {isLoading === false &&
                        <Typography>
                            Continue to Home
                        </Typography>
                    }
                </Button>
            </Box>
    } else if (tokenIsValid === false) {
        button =
            <Box mt={5} align="center">
                <Box className={classes.rowCenter}>
                    <WarningRoundedIcon className={classes.warningIcon} />
                    <Typography>The passwords reset link has expired.</Typography>
                </Box>
                <Link href='' variant='body1' onClick={props.navigateToReset}>
                    {'Try resetting your password again.'}
                </Link>
            </Box>
    } else if (buttonIsDisabled === true) {
        button =
           <ThemeProvider theme={disabledButton}>
                <Button type='submit' fullWidth variant='contained' className={classes.submit} disabled>
                    Continue
                </Button>
           </ThemeProvider>
    } else {
        button =
           <Button type='submit' fullWidth variant='contained' color='primary' className={classes.submit}>
                {isLoading === true &&
                    <CircularProgress size={24} className={classes.buttonProgress} />
                }
                {isLoading === false &&
                    <Typography>
                        Continue
                    </Typography>
                }
           </Button>
    }

    return (
        <Container component='main' maxWidth='xs'>
            <CssBaseline />
            <div className={classes.paper}>
                <Avatar className={classes.avatar}>
                    <LockOutlinedIcon />
                </Avatar>
                <Typography component='h1' variant='h5'>
                    Reset your password
                </Typography>
                <form onSubmit={handleSubmit(onSubmit)} className={classes.form} noValidate>
                    {hideInputTextFields === false &&
                        <div>
                            <TextField
                                inputRef={register({
                                    required: true,
                                    minLength: 6,
                                })}
                                onChange={onChangePassword}
                                name='password'
                                variant='outlined'
                                margin='normal'
                                fullWidth
                                label='New password'
                                type='password'
                                id='password'
                                autoFocus
                            />
                            {passwordIsTooShort === true &&
                                <Box className={classes.row}>
                                    <WarningRoundedIcon className={classes.warningIcon} />
                                    <Typography className={classes.warningText}>Your password must be at least 6 characters.</Typography>
                                </Box>
                            }
                            <TextField
                                inputRef={register({
                                    required: true,
                                    minLength: 6,
                                })}
                                onChange={onChangeConfirmPassword}
                                name='confirmPassword'
                                variant='outlined'
                                margin='normal'
                                fullWidth
                                label='Confirm your password'
                                type='password'
                                id='confirmPassword'
                            />
                            {passwordsDoNotMatch === true &&
                                <Box className={classes.row}>
                                    <WarningRoundedIcon className={classes.warningIcon} />
                                    <Typography className={classes.warningText}>The passwords don't match. Please try again.</Typography>
                                </Box>
                            }
                        </div>
                    }
                    {button}
                </form>
            </div>
            <Box mt={8}>
                <Copyright />
            </Box>
        </Container>
    )
}

export default ResetTokenIsValid;