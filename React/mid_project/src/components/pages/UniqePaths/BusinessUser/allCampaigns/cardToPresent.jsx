import React from "react";
import "./card.css";

export const CardToPresent = ({ campaing, handleDonate }) => {
  return (
    <div className="row">
      <div className="col-sm-6 cardDonateConteinar">
        <div className="card ">
          <div className="card-body ">
            <h5 className="card-title">{campaing.campaignHashtag}</h5>
            <p className="card-text">{campaing.campaignHashtag}</p>
            <button
              className="btnDonate"
              onClick={() => handleDonate(campaing.campaignName)}
            >
              Donate{" "}
            </button>
          </div>
        </div>
      </div>
    </div>
  );
};
