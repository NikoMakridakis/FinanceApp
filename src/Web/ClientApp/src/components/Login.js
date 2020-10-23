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
}));

function Login(props) {

    const classes = useStyles();

    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');

    const { register, handleSubmit, errors } = useForm();

    function onChangeEmail(input) {
        const email = input.target.value;
        setEmail(email);
    };

    function onChangePassword(input) {
        const password = input.target.value;
        setPassword(password);
    };

    function onSubmit(data) {
        handleLogin(data);
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
                                // regex pattern below verifies email as per RFC2822 standards
                                // source: https://regexr.com/5em0n
                                value: /[a-zA-Z0-9!#$ %& '*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&' * +/=?^_`{|}~-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?/,
                                message: 'Invalid email address'
                            }
                        })}
                        onChange={onChangeEmail}
                        value={email}
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
                        value={password}
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
                                        name='remember'
                                        color='primary'
                                        defaultValue={false}
                                    />}
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
