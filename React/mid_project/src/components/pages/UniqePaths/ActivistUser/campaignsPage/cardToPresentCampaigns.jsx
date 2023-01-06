import React from "react";
import "../css/organizationCard.css";

export const CardToCampagin = ({ campaign, handleBeActive, handleClaim }) => {
  return (
    <div className="row">
      <div className="col-sm-6 cardDonateConteinar">
        <div className="card ">
          <div className="card-body ">
            <h5 className="card-title">{campaign.campaignName}</h5>
            <p className="card-text">{campaign.campaignInfo}</p>
            <p className="card-text">
              Potential Profit:{campaign.donationAmount}
            </p>
            <button className="btnDonate" onClick={() => handleBeActive()}>
              Promote
            </button>
            <button
              className="btnDonate"
              onClick={() => handleClaim(campaign.campaignName)}
            >
              Claim Earnings
            </button>
          </div>
        </div>
      </div>
    </div>
  );
};
