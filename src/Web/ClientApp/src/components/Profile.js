import React from "react";
import AuthService from "../services/AuthService";

function Profile() {
    const currentUser = AuthService.getCurrentUser();

    return (
        <div>
            <h1>
                <h3>
                    <strong>{currentUser.email}</strong> Profile
                </h3>
            </h1>
            <p>
                <strong>Token:</strong> {currentUser.accessToken.substring(0, 20)} ...{" "}
                {currentUser.accessToken.substr(currentUser.accessToken.length - 20)}
            </p>
            <p>
                <strong>Id:</strong> {currentUser.id}
            </p>
        </div>
    );
};

export default Profile;