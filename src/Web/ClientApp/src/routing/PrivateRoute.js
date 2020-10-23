import React from "react";
import { Route, Redirect } from "react-router-dom";
import AuthService from "../services/AuthService";

const PrivateRoute = ({ component: Component, ...rest }) => (
    <Route {...rest} render={(props) => (
        AuthService.isAuthenticated === true
            ? <Component {...props} />
            : <Redirect to='/login' />
    )} />
)

export default PrivateRoute;
