import React from "react";
import { useAuth0 } from "@auth0/auth0-react";
import "./css/loginBtn.css";

export const LoginButton = () => {
  const { loginWithRedirect } = useAuth0();

  return (
    <button
      className="loginBtn"
      onClick={() => loginWithRedirect("http://localhost:3000")}
    >
      <span className="btnSpan">Login</span>
    </button>
  );
};
