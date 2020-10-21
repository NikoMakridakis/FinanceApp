import React, { useState } from 'react';
import { useForm, Controller } from 'react-hook-form';
import Avatar from '@material-ui/core/Avatar';
import Button from '@material-ui/core/Button';
import CssBaseline from '@material-ui/core/CssBaseline';
import TextField from '@material-ui/core/TextField';
import FormControlLabel from '@material-ui/core/FormControlLabel';
import Checkbox from '@material-ui/core/Checkbox';
import Link from '@material-ui/core/Link';
import Grid from '@material-ui/core/Grid';
import Box from '@material-ui/core/Box';
import LockOutlinedIcon from '@material-ui/icons/LockOutlined';
import Typography from '@material-ui/core/Typography';
import { makeStyles } from '@material-ui/core/styles';
import Container from '@material-ui/core/Container';
import WarningRoundedIcon from '@material-ui/icons/WarningRounded';

import Copyright from './Copyright';
import AuthService from '../services/AuthService';

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
    error: {
        display: 'flex',
        flexDirection: 'row',
        alignItems: 'center',
    },
    warningIcon: {
        color: '#DC004E',
        fontSize: '16px',
        marginRight: '6px',

    },
    warningText: {
        color: '#DC004E',
    },
}));

function Login(props) {

    const classes = useStyles();

    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');

    const { register, handleSubmit, errors, control } = useForm();

    function onChangeEmail(input) {
        const email = input.target.value;
        setEmail(email);
    };

    function onChangePassword(input) {
        const password = input.target.value;
        setPassword(password);
    };

    function onSubmit(data) {
        console.log(data);

        //data.preventDefault();

        //handleLogin(data);
    };

    function handleLogin(data) {
        AuthService.login(data.email, data.password).then(
            () => {
                props.history.push('/profile');
                window.location.reload();
            }
        );
    };

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
                                // regex pattern below checks for the following:
                                // contains at least one character before '@'
                                // contains only one '@' character
                                // contains only one '.' after '@'
                                value: /^[^@s]+@[^@s.]+.[^@.s]+$/,
                                message: 'Invalid email address'
                            }
                        })}
                        onChange={onChangeEmail}
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
                        <Box className={classes.error}>
                            <WarningRoundedIcon className={classes.warningIcon} />
                            <Typography className={classes.warningText}>{errors.email.message}</Typography>
                        </Box>
                    }
                    <TextField
                        inputRef={register}
                        onChange={onChangePassword}
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
                        <Grid item xs>
                            <FormControlLabel
                                control={
                                    <Controller as={Checkbox} control={control} name='remember' color='primary' defaultValue={false} />
                                }
                                label='Remember me'
                            />
                        </Grid>
                        <Grid item xs className={classes.forgotPassword}>
                            <Link href='#' variant='body2'>
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
                            <Link href='#' variant='body1'>
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
    );
}

export default Login;
