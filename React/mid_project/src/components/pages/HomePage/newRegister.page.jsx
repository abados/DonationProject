import React from "react";
import "./homePage.css";
import { useAuth0 } from "@auth0/auth0-react";

export const NewRegisterPage = () => {
  const { logout } = useAuth0();
  return (
    <div className="homePageContainer">
      <h1>
        Thank you for registering, please wait for an administrator to grant you
        permissions
      </h1>
      <label
        className="navLbl"
        onClick={() => logout({ returnTo: window.location.origin })}
      >
        Logout
      </label>
    </div>
  );
};
