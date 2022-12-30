import React from "react";
import { LoginButton } from "./LoginButton";
import "./css/loginPage.css";

export const LoginPage = () => {
  return (
    <div className="loginPage">
      <h1>Welcome to ProLobby</h1>
      <LoginButton />
    </div>
  );
};
