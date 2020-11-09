import React, { useState, useEffect } from 'react';
import queryString from 'query-string';

import AuthService from '../../services/AuthService';
import ResetTokenIsInvalid from './ResetTokenIsInvalid';
import ResetTokenIsValid from './ResetTokenIsValid';

function Reset(props) {

    const [resetTokenIsValid, setResetTokenIsValid] = useState(false);

    async function verifyResetTokenFromUrl(props, resetTokenFromUrl) {
        try {
            const response = await AuthService.verifyResetToken(props.email, resetTokenFromUrl);
            console.log(response);
            if (response === 200) {
                setResetTokenIsValid(true);
            } else if (response === 400 || 401 || 404) {
                setResetTokenIsValid(false);
            }
        } catch (error) {
            console.log(error);
        }
    }

    useEffect(() => {
        function getResetTokenFromUrl() {
            const urlParameters = queryString.parse(window.location.search);
            const resetTokenFromUrl = urlParameters.resetToken;
            verifyResetTokenFromUrl(resetTokenFromUrl);
        }

        getResetTokenFromUrl();
    }, []);



    //function navigateToLogin(event) {
    //    event.preventDefault();
    //    props.history.push('/login');
    //}

    //async function onSubmit(data) {
    //    try {
    //        const response = await AuthService.register(data.email, data.password);
    //        console.log(response);
    //        if (response === 200) {

    //            props.history.push('/welcome');
    //        } else if (response === 401) {

    //        }
    //    } catch (error) {
    //        console.log(error);
    //    }
    //}

    return (
        <div>
            {resetTokenIsValid === false &&
                <ResetTokenIsInvalid>
                </ResetTokenIsInvalid>
            }
            {resetTokenIsValid === true &&
                <ResetTokenIsValid>
                </ResetTokenIsValid>
            }
        </div>
    )
}

export default Reset;
