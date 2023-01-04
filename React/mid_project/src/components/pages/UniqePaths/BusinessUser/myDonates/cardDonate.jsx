import React from "react";

export const CardDonate = ({ product, handleDelete }) => {
  return (
    <div className="row">
      <div className="col-sm-6 cardDonateConteinar">
        <div className="card">
          <div className="card-body">
            <h5 className="card-title">{product.productName}</h5>
            <p className="card-text">{}</p>
            <button className="btnDonate" onClick={() => handleDelete(product)}>
              Delete{" "}
            </button>
          </div>
        </div>
      </div>
    </div>
  );
};
