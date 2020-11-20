import React, { useState, useEffect } from 'react';
import queryString from 'query-string';

import AuthService from '../../services/AuthService';
import ResetTokenIsInvalid from './ResetTokenIsInvalid';
import ResetTokenIsValid from './ResetTokenIsValid';

function Reset(props) {

    const [resetTokenIsValid, setResetTokenIsValid] = useState();
    const email = props.email;
    const onChangeEmail = props.onChangeEmail;

    function navigateToLogin(event) {
        event.preventDefault();
        props.history.push('/login');
    }

    function navigateToRegister(event) {
        event.preventDefault();
        props.history.push('/register');
    }

    function navigateToReset() {
        props.history.push('/user/reset');
    }

    function navigateToBudget(event) {
        event.preventDefault();
        props.history.push('/budget');
    }

    useEffect(() => {
        function getResetTokenFromUrl() {
            const urlParameters = queryString.parse(window.location.search);
            const resetTokenFromUrl = urlParameters.resetToken;
            if (resetTokenFromUrl !== undefined) {
                localStorage.setItem('resetToken', JSON.stringify(resetTokenFromUrl));
            }
        }

        function removeResetTokenFromUrl() {
            window.history.replaceState(null, "", "/user/reset");
        }

        async function verifyResetTokenFromUrl() {
            try {
                const email = JSON.parse(localStorage.getItem('email'));
                const resetToken = JSON.parse(localStorage.getItem('resetToken'));
                const response = await AuthService.verifyResetToken(email, resetToken);
                if (response === 200) {
                    setResetTokenIsValid(true);
                } else if (response === 400 || 401 || 404) {
                    setResetTokenIsValid(false);
                }
            } catch (error) {
                console.log(error);
            }
        }

        getResetTokenFromUrl();
        removeResetTokenFromUrl();
        verifyResetTokenFromUrl();

    }, []);

    return (
        <div>
            {resetTokenIsValid === false &&
                <ResetTokenIsInvalid email={email} onChangeEmail={onChangeEmail} navigateToLogin={navigateToLogin} navigateToRegister={navigateToRegister}>
                </ResetTokenIsInvalid>
            }
            {resetTokenIsValid === true &&
                <ResetTokenIsValid navigateToWelcome={navigateToBudget} navigateToReset={navigateToReset}>
                </ResetTokenIsValid>
            }
        </div>
    )
}

export default Reset;
