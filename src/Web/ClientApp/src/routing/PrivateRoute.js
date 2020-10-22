import React from "react";
import { Route, Redirect } from "react-router-dom";
import AuthHeader from "../services/AuthHeader";

function ProtectedRoute()
({
    component: Component,
    ...rest
}) => {
    return (
        <Route
            {...rest}
            render={props => {
                if (AuthHeader()) {
                    return <Component {...props} />;
                } else {
                    return (
                        <Redirect
                            to={{
                                pathname: "/",
                                state: {
                                    from: props.location
                                }
                            }}
                        />
                    );
                }
            }}
        />
    );
};

export default ProtectedRoute;
