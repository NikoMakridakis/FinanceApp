import React, { useState } from 'react';
import { useForm } from 'react-hook-form';
import { makeStyles } from '@material-ui/core/styles';
import Avatar from '@material-ui/core/Avatar';
import Button from '@material-ui/core/Button';
import CssBaseline from '@material-ui/core/CssBaseline';
import TextField from '@material-ui/core/TextField';
import Link from '@material-ui/core/Link';
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

function Register(props) {

    const [passwordIsTooShort, setPasswordIsTooShort] = useState(false);

    const delay = ms => new Promise(res => setTimeout(res, ms));
    const { register, handleSubmit, errors } = useForm();
    const classes = useStyles();

    async function onChangePassword(input) {
        const password = input.target.value;
        await delay(500);
        if (password.length < 6) {
            setPasswordIsTooShort(true);
        } else if (password.length === 6) {
            setPasswordIsTooShort(false);
        } else {
            setPasswordIsTooShort(false);
        }
    }

    function navigateToLogin(event) {
        event.preventDefault();
        props.history.push('/login');
    }

    async function onSubmit(data, props) {
        try {
            const response = await AuthService.register(data.email, data.password);
            if (response === 200) {
                props.setEmailExists(false);
                props.history.push('/welcome');
            } else if (response === 401) {
                props.setEmailExists(true);
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
                    Register
                </Typography>
                <form onSubmit={handleSubmit(onSubmit)} className={classes.form} noValidate>
                    <TextField
                        inputRef={register({
                            required: 'Email is required',
                            pattern: {
                                // regex pattern below verifies email as per RFC2822 standards
                                // source: https://regexr.com/5em0n
                                value: /[a-zA-Z0-9!#$ %& '*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&' * +/=?^_`{|}~-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?/,
                                message: 'Please enter a valid email.'
                            }
                        })}
                        onChange={props.onChangeEmail}
                        defaultValue={props.email}
                        name='email'
                        variant='outlined'
                        margin='normal'
                        fullWidth
                        id='email'
                        label='Email Address'
                        autoComplete='email'
                        autoFocus
                    />
                    {props.emailExists &&
                        <Box className={classes.row}>
                            <WarningRoundedIcon className={classes.warningIcon} />
                            <Typography className={classes.warningText}>An account already exists with this email.</Typography>
                        </Box>
                    }
                    {errors.email &&
                        <Box className={classes.row}>
                            <WarningRoundedIcon className={classes.warningIcon} />
                            <Typography className={classes.warningText}>{errors.email.message}</Typography>
                        </Box>
                    }
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
                        label='Password'
                        type='password'
                        id='password'
                    />
                    {passwordIsTooShort &&
                        <Box className={classes.row}>
                            <WarningRoundedIcon className={classes.warningIcon} />
                            <Typography className={classes.warningText}>Your password must be at least 6 characters.</Typography>
                        </Box>
                    }
                    <Button
                        type='submit'
                        fullWidth
                        variant='contained'
                        color='primary'
                        className={classes.submit}
                    >
                        Register
                    </Button>
                    <Box>
                        <Typography align='center' variant='body1'>
                            {'Have an account? '}
                            <Link href='' variant='body1' onClick={navigateToLogin}>
                                {'Sign In'}
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

export default Register;
