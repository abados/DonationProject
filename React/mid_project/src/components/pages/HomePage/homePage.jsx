import React from "react";
import "./homePage.css";
import { useAuth0 } from "@auth0/auth0-react";
import ReactPlayer from "react-player";
import video from "../../../videos/Snowflakes.mp4";

export const HomePage = () => {
  const { isLoading } = useAuth0();

  if (isLoading) {
    <div>Loading...</div>;
  } else {
    return (
      <div className="homePageContainer">
        <ReactPlayer
          className="ReactPlayer"
          url={video}
          playing
          loop
          muted
          width="100%"
          height="98%"
          style={{ position: "absolute", zIndex: -1 }}
        />
        <h1 className="homePageH1">Welcome to ProLobby home page</h1>
        <p className="homePageP">
          Welcome to the great donation site - ProLobby. Here you can take part
          in campaigns to promote things that are important to you, donate your
          time and money for those goals and promote your association's
          campaigns. Enjoy!
        </p>
      </div>
    );
  }
};
