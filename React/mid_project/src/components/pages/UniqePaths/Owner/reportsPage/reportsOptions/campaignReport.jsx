import React, { useState } from "react";

export const CampaignReport = ({ Campaignlist, result }) => {
  const [countForTable] = useState(1);

  if (result === "All campaigns") {
    return (
      <div className="my-report-tbl">
        <table className="table table-striped">
          <thead>
            <tr>
              <th scope="col">#</th>
              <th scope="col">Campaign Name</th>
              <th scope="col">Campaign Info</th>
              <th scope="col">Campaign Hashtag</th>
              <th scope="col">Campaign Url</th>
              <th scope="col">Donation Amount</th>
            </tr>
          </thead>
          <tbody>
            {Campaignlist &&
              Campaignlist.map((campaign, index) => {
                return (
                  <>
                    <tr>
                      <th scope="row">{countForTable + index}</th>
                      <td>{campaign.campaignName}</td>
                      <td>{campaign.campaignInfo}</td>
                      <td>{campaign.campaignHashtag}</td>
                      <td>{campaign.campaignUrl}</td>
                      <td>{campaign.donationAmount}$</td>
                    </tr>
                  </>
                );
              })}
          </tbody>
        </table>
      </div>
    );
  } else {
    return (
      <div className="my-report-tbl">
        <table className="table table-striped">
          <thead>
            <tr>
              <th scope="col">#</th>
              <th scope="col">Campaign Name</th>
              <th scope="col">Campaign Hashtag</th>
              <th scope="col">Active User Id</th>
              <th scope="col">Active User Name</th>
              <th scope="col">Twitter Acount</th>
            </tr>
          </thead>
          <tbody>
            {Campaignlist &&
              Campaignlist.map((campaign, index) => {
                return (
                  <>
                    <tr>
                      <th scope="row">{countForTable + index}</th>
                      <td>{campaign.campaignName}</td>
                      <td>{campaign.campaignHashtag}</td>
                      <td>{campaign.activeUserId}</td>
                      <td>{campaign.ActiveUserName}</td>
                      <td>{campaign.TwitterAcount}</td>
                    </tr>
                  </>
                );
              })}
          </tbody>
        </table>
      </div>
    );
  }
};
