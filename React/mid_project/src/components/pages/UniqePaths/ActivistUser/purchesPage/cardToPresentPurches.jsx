import React, { useEffect, useState } from "react";
import "../css/organizationCard.css";

export const CardOfPurches = ({ Product }) => {
  return (
    <div className="row">
      <div className="col-sm-6 cardDonateConteinar">
        <div className="card ">
          <div className="card-body ">
            <h5 className="card-title">{Product.productName}</h5>
            <p className="card-text">Product Price:{Product.price}</p>
          </div>
        </div>
      </div>
    </div>
  );
};
