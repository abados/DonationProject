import React from "react";
import "../css/organizationCard.css";

export const CardToCampagin = ({
  campaign,
  handleBeActive,
  handleClaim,
  activeCampaignList,
}) => {
  const campaignNames = [...activeCampaignList].map(
    (item) => item.campaignName
  );

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
            {activeCampaignList.filter(
              (item) => item.campaignName === campaign.campaignName
            ).length > 0 ? (
              <h2>Great Job Promoting Me</h2>
            ) : (
              <button className="btnDonate" onClick={() => handleBeActive()}>
                Promote
              </button>
            )}
            {activeCampaignList.filter(
              (item) => item.campaignName === campaign.campaignName
            ).length > 0 ? (
              <button
                className="btnDonate"
                onClick={() => handleClaim(campaign.campaignName)}
              >
                Claim Earnings
              </button>
            ) : (
              <></>
            )}
          </div>
        </div>
      </div>
    </div>
  );
};
