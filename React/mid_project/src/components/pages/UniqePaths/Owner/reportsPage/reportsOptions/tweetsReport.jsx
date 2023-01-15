import React, { useState } from "react";
import { TwitterShareButton } from "react-twitter-embed";

export const TweetsReport = ({ Tweetslist }) => {
  const [countForTable] = useState(1);

  return (
    <div className="my-report-tbl">
      <table className="table table-striped">
        <thead>
          <tr>
            <th scope="col">#</th>
            <th scope="col">Campaign Name</th>
            <th scope="col">Tweet Hashtag</th>
            <th scope="col">Tweet Text</th>
            <th scope="col">Active User Name</th>
            <th scope="col">Date</th>
            <th scope="col">Time</th>
          </tr>
        </thead>
        <tbody>
          {Tweetslist &&
            Tweetslist.map((Tweet, index) => {
              return (
                <>
                  <tr>
                    <th scope="row">{countForTable + index}</th>
                    <td>{Tweet.campaignName}</td>
                    <td>{Tweet.TweetHashtag}</td>
                    <td>{Tweet.TweetText}</td>
                    <td>{Tweet.ActiveUserName}</td>
                    <td>{Tweet.Date.split(" ")[0]}</td>
                    <td>{Tweet.Time}</td>
                    <td>
                      <TwitterShareButton
                        options={{
                          text: `I just saw the activity on my donation site and it's amazing, I suggest you also log in and start donating to the community! you can donate to ${Tweet.campaignName} and even earn money. #Donor #Community ${Tweet.TweetHashtag}`,
                        }}
                        url={"www.PromoteIt.com"}
                      >
                        Tweet it
                      </TwitterShareButton>
                    </td>
                  </tr>
                </>
              );
            })}
        </tbody>
      </table>
    </div>
  );
};
