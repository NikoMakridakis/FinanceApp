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
        fontSize: '14px',
    },
}))

function ResetTokenIsValid(props) {

    const classes = useStyles();

    const delay = ms => new Promise(res => setTimeout(res, ms));

    const [password, setPassword] = useState('');
    const [passwordIsTooShort, setPasswordIsTooShort] = useState();
    const [passwordsDoNotMatch, setPasswordsDoNotMatch] = useState();

    const { register, handleSubmit } = useForm();

    async function onChangePassword(input) {
        const password = input.target.value;
        setPassword(password);
        await delay(500);
        if (password.length < 6) {
            setPasswordIsTooShort(true);
        } else if (password.length === 6) {
            setPasswordIsTooShort(false);
        } else {
            setPasswordIsTooShort(false);
        }
    }

    async function onChangeConfirmPassword(input) {
        const confirmPassword = input.target.value;
        await delay(500);
        if (confirmPassword !== password) {
            setPasswordsDoNotMatch(true);
        } else {
            setPasswordsDoNotMatch(false);
        }
    }

    async function onSubmit(data) {
        try {
            const email = await JSON.parse(localStorage.getItem('email'));
            const resetToken = await JSON.parse(localStorage.getItem('resetToken'));
            const response = await AuthService.resetPassword(email, data.password, data.confirmPassword, resetToken);
            if (response === 200) {
                await AuthService.loginForStaySignedIn(email, data.password);
                localStorage.removeItem('email');
                localStorage.removeItem('resetToken');
            } else if (response === 400 || 401 || 404) {
                localStorage.removeItem('email');
                localStorage.removeItem('resetToken');
            }
        } catch (error) {
            console.log(error);
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
                    Reset your password
                </Typography>
                <form onSubmit={handleSubmit(onSubmit)} className={classes.form} noValidate>
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
                            <Typography className={classes.warningText}>The password doesn't match. Try again.</Typography>
                        </Box>
                    }
                    {passwordIsTooShort === false && passwordsDoNotMatch === false &&
                        <Button
                            type='submit'
                            fullWidth
                            variant='contained'
                            color='primary'
                            className={classes.submit}
                            onClick={props.navigateToWelcome}
                        >
                            Continue
                        </Button>
                    }
                    <Button
                        type='submit'
                        fullWidth
                        variant='contained'
                        color='primary'
                        className={classes.submit}
                        disabled
                    >
                        Continue
                    </Button>
                </form>
            </div>
            <Box mt={8}>
                <Copyright />
            </Box>
        </Container>
    )
}

export default ResetTokenIsValid;