import React, { useState, useEffect } from "react";
import "./products.css";
import { ClockLoader } from "react-spinners";
import { useNavigate } from "react-router-dom";
import { useAuth0 } from "@auth0/auth0-react";
import {
  getDonates,
  sendTheItem,
} from "../../../../../services/businessService";

export const BoughtProductsPage = () => {
  const [productsData, setProductsData] = useState([]);
  const [countForTable, setCountForTable] = useState(1);
  const [render, setRender] = useState(false);

  const { user } = useAuth0();

  const getProductsFromDB = async () => {
    let res = await getDonates(user.email, "trackShipment");
    setProductsData(res);
    setRender(false);
  };

  useEffect(() => {
    setTimeout(() => {
      getProductsFromDB();
    }, 3000);
  }, [render]);

  // const [expanded, setExpanded] = useState(false);
  // const [key, setKey] = useState();

  function handleClick(productKey) {
    sendTheItem(productKey);
    getProductsFromDB();
    setRender(true);

    // setExpanded(!expanded);
    // setKey(productKey);
  }

  return (
    <div className="my-tbl">
      <table className="table table-striped">
        <thead>
          <tr>
            <th scope="col">#</th>
            <th scope="col">Product Name</th>
            <th scope="col">Price</th>
            <th scope="col">The name of the orderer</th>
            <th scope="col">address</th>
            <th scope="col"></th>
          </tr>
        </thead>
        <tbody>
          {productsData && productsData.length > 0 ? (
            productsData.map((product, index) => {
              return (
                <>
                  <tr>
                    <th scope="row">{countForTable + index}</th>
                    <td>{product.productName}</td>
                    <td>{product.price}$</td>
                    <td>{product.fullName}</td>
                    <td>{product.addressToShip}</td>
                    <td>
                      {!product.IsDelivered ? (
                        <button
                          className="btnShipThis"
                          onClick={() => handleClick(product.Id)}
                        >
                          <div className="svg-wrapper-1">
                            <div className="svg-wrapper3">
                              <svg
                                className="svhShip"
                                xmlns="http://www.w3.org/2000/svg"
                                viewBox="0 0 24 24"
                                width="24"
                                height="24"
                              >
                                <path fill="none" d="M0 0h24v24H0z"></path>
                                <path
                                  fill="currentColor"
                                  d="M1.946 9.315c-.522-.174-.527-.455.01-.634l19.087-6.362c.529-.176.832.12.684.638l-5.454 19.086c-.15.529-.455.547-.679.045L12 14l6-8-8 6-8.054-2.685z"
                                ></path>
                              </svg>
                            </div>
                          </div>
                          <span className="spanSend">Send</span>
                        </button>
                      ) : (
                        <p>The product shipped</p>
                      )}
                    </td>
                    <td></td>
                  </tr>
                </>
              );
            })
          ) : (
            <tr className="clock--tr">
              <td className="clock--td2" colSpan={4}>
                <ClockLoader color="#36d7b7" size={86} />
              </td>
            </tr>
          )}
        </tbody>{" "}
      </table>
    </div>
  );
};
