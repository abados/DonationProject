import React from "react";
import ReactDOM from "react-dom/client";
import "./index.css";
import App from "./App";
import "../node_modules/bootstrap/dist/css/bootstrap.css";
import { BrowserRouter } from "react-router-dom";
import { Auth0Provider } from "@auth0/auth0-react";
import { RecoilRoot } from "recoil";

const root = ReactDOM.createRoot(document.getElementById("root"));
root.render(
  <BrowserRouter>
    <Auth0Provider
      domain="dev-qjf7hgqeu16fymem.us.auth0.com"
      clientId="OjebAoLsXnn5cKbkxJeYjQ1UaJRcLEUi"
      redirectUri={window.location.origin}
    >
      <RecoilRoot>
        <App />
      </RecoilRoot>
    </Auth0Provider>
  </BrowserRouter>
);
