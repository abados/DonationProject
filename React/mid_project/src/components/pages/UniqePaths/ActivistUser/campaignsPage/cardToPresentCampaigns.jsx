import React from "react";
import "../css/organizationCard.css";

export const CardToCampagin = ({ campaign, handleDonate }) => {
  return (
    <div className="row">
      <div className="col-sm-6 cardDonateConteinar">
        <div className="card ">
          <div className="card-body ">
            <h5 className="card-title">{campaign.campaignName}</h5>
            <p className="card-text">{campaign.campaignInfo}</p>
            <button className="btnDonate" onClick={() => handleDonate()}>
              {campaign.campaignHashtag}
            </button>
          </div>
        </div>
      </div>
    </div>
  );
};
