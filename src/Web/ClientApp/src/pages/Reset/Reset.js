import React, { useState } from 'react';
import queryString from 'query-string';

import ResetTokenIsInvalid from './ResetTokenIsInvalid';
import ResetTokenIsValid from './ResetTokenIsValid';

function Reset() {

    const [resetToken, setResetToken] = useState(getResetTokenFromUrl);
    const [resetTokenIsValid, setResetTokenIsValid] = useState(false);

    function getResetTokenFromUrl() {
        const urlParameters = queryString.parse(window.location.search);
        const resetTokenFromUrl = urlParameters.resetToken;
        console.log(resetTokenFromUrl);
    }

    function setResetTokenFromUrl(resetTokenFromUrl) {
        setResetToken(resetTokenFromUrl);
    }

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
        <div onLoad={getResetTokenFromUrl}>
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
