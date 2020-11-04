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
    resetText: {
        marginTop: theme.spacing(4),
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
    successIcon: {
        color: '#1EA672',
        fontSize: '16px',
        marginRight: '6px',
    },
    successText: {
        fontSize: '14px',
    },
    resendEmailText: {
        fontSize: '12px',
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

function Reset(props) {

    const classes = useStyles();

    const [emailExists, setEmailExists] = useState(false);
    const [emailSent, setEmailSent] = useState(false);
    const [email, setEmail] = useState(false);

    const { register, handleSubmit, errors } = useForm();

    function onChangeEmail(data) {
        setEmailExists(false);
        setEmail(data.target.value);
    }

    function navigateToLogin(event) {
        event.preventDefault();
        props.history.push('/login');
    }

    function navigateToRegister(event) {
        event.preventDefault();
        props.history.push('/register');
    }

    async function resendEmail(event) {
        event.preventDefault();
        try {
            const response = await AuthService.forgotPassword(email);
            if (response === 200) {
                setEmailExists(false);
                setEmailSent(true);
            } else if (response === 401 || 404) {
                setEmailExists(true);
                setEmailSent(false);
            }
        } catch (error) {
            console.log(error);
        }
    }

    async function onSubmit(data) {
        try {
            const response = await AuthService.forgotPassword(data.email);
            if (response === 200) {
                setEmailExists(false);
                setEmailSent(true);
            } else if (response === 401 || 404) {
                setEmailExists(true);
                setEmailSent(false);
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
                    Reset Your Password
                </Typography>
                <Typography variant='body1' className={classes.resetText}>
                    Enter the email address associated with your account and we'll send you a link to reset your password.
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
                    {emailExists &&
                        <Box className={classes.row}>
                            <WarningRoundedIcon className={classes.warningIcon} />
                            <Typography className={classes.warningText}>We couldn't find that email. Please try again.</Typography>
                        </Box>
                    }
                    {errors.email &&
                        <Box className={classes.row}>
                            <WarningRoundedIcon className={classes.warningIcon} />
                            <Typography className={classes.warningText}>{errors.email.message}</Typography>
                        </Box>
                    }
                    {emailSent &&
                        <Box>
                            <Box className={classes.row} mt={2}>
                                <Typography className={classes.successText}>Thanks, check your email for instructions to reset your password.</Typography>
                            </Box>
                            <Box className={classes.row} mt={2}>
                                <Box mr={0.5}>
                                    <Typography className={classes.resendEmailText}>{'Didn\'t get the email? Check your spam folder or'}</Typography>
                                </Box>
                                <Link href='' onClick={resendEmail} variant='body1' className={classes.resendEmailText}>
                                    {'resend'}.
                                </Link>
                            </Box>
                        </Box>
                    }
                    {!emailSent &&
                        <Box>
                            <Button
                                type='submit'
                                fullWidth
                                variant='contained'
                                color='primary'
                                className={classes.submit}
                            >
                                Continue
                            </Button>
                            <Box>
                                <Typography align='center' variant='body1'>
                                    <Link href='' variant='body1' onClick={navigateToLogin}>
                                        {'Return to sign in'}
                                    </Link>
                                </Typography>
                            </Box>
                            <Box>
                                <Typography align='center' variant='body1'>
                                    {'Don\'t have an account? '}
                                    <Link href='' variant='body1' onClick={navigateToRegister}>
                                        {'Sign Up'}
                                    </Link>
                                </Typography>
                            </Box>
                        </Box>
                    }
                </form>
            </div>
            <Box mt={8}>
                <Copyright />
            </Box>
        </Container>
    )
}

export default Reset;
