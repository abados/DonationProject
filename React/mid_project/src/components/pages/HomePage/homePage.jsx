import React from "react";
import "./homePage.css";
import { useAuth0 } from "@auth0/auth0-react";

export const HomePage = () => {
  const { user, isLoading } = useAuth0();

  if (isLoading) {
    <div>Loading...</div>;
  } else {
    return (
      <div className="homePageContainer">
        <h1>Welcome to ProLobby home page</h1>
        <p>
          Lorem ipsum dolor sit amet consectetur adipisicing elit. Repellat
          quisquam qui nostrum. Qui eum amet ullam, reprehenderit adipisci
          laudantium perspiciatis?
        </p>
      </div>
    );
  }
};
