import React, { useState } from 'react';
import Copyright from './Copyright';
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

import AuthService from '../services/AuthService';

const useStyles = makeStyles({
    paper: {
            marginTop: '35%',
            display: 'flex',
            flexDirection: 'column',
            alignItems: 'center',
    },
    avatar: {
        margin: '1px',
        backgroundColor: '#DC004E',
    },
    form: {
        width: '100%',
        marginTop: '1px',
    },
    submit: {
        margin: '3px, 0px, 2px',
    },
});

function Login(props) {

    const classes = useStyles();

    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [loading, setLoading] = useState(false);

    function onChangeEmail(event) {
        const email = event.target.value;
        setEmail(email);
    };

    function onChangePassword(event) {
        const password = event.target.value;
        setPassword(password);
    };

    function handleLogin() {
        setLoading(true);

        AuthService.login(email, password).then(
            () => {
                props.history.push('/Budget');
                window.location.reload();
            },
            (error) => {
                setLoading(false);
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
                <form onSubmit={handleLogin} className={classes.form} noValidate>
                    <TextField
                        value={email}
                        onChange={onChangeEmail}
                        variant='outlined'
                        margin='normal'
                        required
                        fullWidth
                        id='email'
                        label='Email Address'
                        name='email'
                        autoComplete='email'
                        autoFocus
                    />
                    <TextField
                        value={password}
                        onChange={onChangePassword}
                        variant='outlined'
                        margin='normal'
                        required
                        fullWidth
                        name='password'
                        label='Password'
                        type='password'
                        id='password'
                        autoComplete='current-password'
                    />
                    <FormControlLabel
                        control={<Checkbox value='remember' color='primary' />}
                        label='Remember me'
                    />
                    <Button
                        type='submit'
                        fullWidth
                        variant='contained'
                        color='primary'
                        className={classes.submit}
                    >
                        Sign In
                    </Button>
                    <Grid container>
                        <Grid item xs>
                            <Link href='#' variant='body2'>
                                Forgot password?
                            </Link>
                        </Grid>
                        <Grid item>
                            <Link href='#' variant='body2'>
                                {'Do not have an account? Sign Up'}
                            </Link>
                        </Grid>
                    </Grid>
                </form>
            </div>
            <Box mt={8}>
                <Copyright />
            </Box>
        </Container>

    );
}

export default Login;
