import React, { useState } from "react";

export const ProductReport = ({ Productlist, result }) => {
  const [countForTable] = useState(1);
  console.log(Productlist);
  return (
    <div className="my-report-tbl">
      <table className="table table-striped">
        <thead>
          <tr>
            <th scope="col">#</th>
            <th scope="col">Product Name</th>
            <th scope="col">Price</th>
            <th scope="col">Donor ID</th>
            <th scope="col">Donate ID</th>
            <th scope="col">Is Bought</th>
            <th scope="col">Is Deliverd</th>
            <th scope="col">Buyer ID</th>
          </tr>
        </thead>
        <tbody>
          {Productlist &&
            Productlist.map((Product, index) => {
              console.log(Product);
              return (
                <>
                  <tr>
                    <th scope="row">{countForTable + index}</th>
                    <td>{Product.productName}</td>
                    <td>{Product.price}$</td>
                    <td>{Product.businessID}</td>
                    <td>{Product.campaignID}</td>
                    <td>{Product.IsBought ? "Yes" : "No"}</td>
                    <td>{Product.IsDelivered ? "Yes" : "No"}</td>
                    <td>{Product.ActivistBuyerID}</td>
                  </tr>
                </>
              );
            })}
        </tbody>
      </table>
    </div>
  );
};
