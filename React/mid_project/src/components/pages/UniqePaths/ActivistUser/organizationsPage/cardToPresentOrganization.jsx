import React from "react";
import "../css/organizationCard.css";

export const CardToOrganization = ({ organ, handleChose }) => {
  return (
    <div className="row">
      <div className="col-sm-6 cardDonateConteinar">
        <div className="card ">
          <div className="card-body ">
            <h5 className="card-title">{organ.organizationName}</h5>
            <p className="card-text">{organ.organizationDescription}</p>
            <button className="btnDonate" onClick={() => handleChose(organ)}>
              Enter
            </button>
          </div>
        </div>
      </div>
    </div>
  );
};
