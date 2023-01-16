import React from "react";
import "./homePage.css";
import { useAuth0 } from "@auth0/auth0-react";

export const HomePage = () => {
  const { isLoading } = useAuth0();

  if (isLoading) {
    <div>Loading...</div>;
  } else {
    return (
      <div className="homePageContainer">
        <h1>Welcome to ProLobby home page</h1>
        <p>
          Welcome to the great donation site - ProLobby. Here you can take part
          in campaigns to promote things that are important to you, donate your
          time and money for those goals and promote your association's
          campaigns. Enjoy!
        </p>
      </div>
    );
  }
};
