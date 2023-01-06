import React, { useState, useEffect } from "react";
import { getPurches } from "../../../../../services/activistService";
import { ClockLoader } from "react-spinners";
import "../css/organizationCard.css";
import { CardOfPurches } from "./cardToPresentPurches";
import "react-toastify/dist/ReactToastify.css";
import { useAuth0 } from "@auth0/auth0-react";

export const PurchesOFActivist = () => {
  const [productList, setProductList] = useState([]);
  const [showClock, setShowClock] = useState(true);
  const { user } = useAuth0();

  const getProductsFromDB = async () => {
    let res = await getPurches(user.email);
    setProductList(res);
  };

  useEffect(() => {
    setTimeout(() => {
      getProductsFromDB();
    }, 3000);
  }, [productList.length]);

  useEffect(() => {
    const timeout = setTimeout(() => {
      setShowClock(false);
    }, 5000); // 5000 milliseconds = 5 seconds

    return () => {
      clearTimeout(timeout);
    };
  }, []);

  return (
    <div className="cardlistContainer">
      <h1>My Products</h1>

      <div className="cardlistContainer2">
        {productList && productList.length > 0 ? (
          productList.map((Product) => {
            return (
              <>
                <CardOfPurches Product={Product} />
              </>
            );
          })
        ) : (
          <>
            {showClock && (
              <tr className="clock--tr">
                <td className="clock--td" colSpan={4}>
                  <ClockLoader color="#36d7b7" size={86} />
                </td>
              </tr>
            )}
            {!showClock && (
              <h1>sorry we didn't found any Reward for this campaign</h1>
            )}
          </>
        )}
      </div>
    </div>
  );
};
