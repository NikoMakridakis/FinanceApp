import React, { useState } from 'react';
import { useForm } from "react-hook-form";
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

import Copyright from './Copyright';
import AuthService from "../services/AuthService";

//https://codesandbox.io/s/o766kp4z05

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
}));

function Login(props) {

    const classes = useStyles();

    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");

    const { register, handleSubmit } = useForm();

    function onChangeEmail(e) {
        const email = e.target.value;
        setEmail(email);
    };

    function onChangePassword(e) {
        const password = e.target.value;
        setPassword(password);
    };

    function handleLogin(e) {
        e.preventDefault();
        
        AuthService.login(email, password).then(
            () => {
                props.history.push("/profile");
                window.location.reload();
            }
        );
    }

    return (
        <Container component="main" maxWidth="xs">
            <CssBaseline />
            <div className={classes.paper}>
                <Avatar className={classes.avatar}>
                    <LockOutlinedIcon />
                </Avatar>
                <Typography component="h1" variant="h5">
                    Sign in
                </Typography>
                <form onSubmit={handleLogin} className={classes.form}>
                    <TextField
                        onChange={onChangeEmail}
                        value={email}
                        name="email"
                        variant="outlined"
                        margin="normal"
                        fullWidth
                        id="email"
                        label="Email Address"
                        autoComplete="email"
                        autoFocus
                        ref={register({
                            required: "required",
                            pattern: {
                                value: /S+@S+.S+/,
                                message: "Entered value does not match email format"
                            }
                        })}
                    />
                    <TextField
                        ref={register}
                        onChange={onChangePassword}
                        value={password}
                        name="password"
                        variant="outlined"
                        margin="normal"
                        fullWidth
                        label="Password"
                        type="password"
                        id="password"
                        autoComplete="current-password"
                    />
                    <Grid container>
                        <Grid item xs>
                            <FormControlLabel
                                control={<Checkbox value="remember" color="primary" />}
                                label="Remember me"
                            />
                        </Grid>
                        <Grid item xs className={classes.forgotPassword}>
                            <Link href="#" variant="body2">
                                <Typography>
                                    Forgot password?
                                </Typography>
                            </Link>
                        </Grid>
                    </Grid>
                    <Button
                        type="submit"
                        fullWidth
                        variant="contained"
                        color="primary"
                        className={classes.submit}
                    >
                        Sign In
                    </Button>
                    <Box>
                        <Typography align="center" variant="body1">
                            { "Don't have an account? " }
                            <Link href="#" variant="body1">
                                    {"Sign Up"}
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
