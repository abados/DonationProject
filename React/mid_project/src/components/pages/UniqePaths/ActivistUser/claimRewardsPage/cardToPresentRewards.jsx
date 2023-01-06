import React, { useEffect, useState } from "react";
import "../css/organizationCard.css";

export const CardToReward = ({ Product, handleBuy, isBought, namekey }) => {
  const [localIsBought, setLocalIsBought] = useState(isBought);

  useEffect(() => {
    setLocalIsBought(isBought);
  }, [isBought]);

  console.log(namekey);
  console.log(Product.productName);
  return (
    <div className="row">
      <div className="col-sm-6 cardDonateConteinar">
        <div className="card ">
          <div className="card-body ">
            <h5 className="card-title">{Product.productName}</h5>
            <p className="card-text">Product Price:{Product.price}</p>

            {localIsBought && Product.productName === namekey ? (
              <>
                <h5>Ordered</h5>
              </>
            ) : (
              <button
                className="btnDonate"
                onClick={() => handleBuy(Product.price)}
              >
                Buy Item
              </button>
            )}
          </div>
        </div>
      </div>
    </div>
  );
};
