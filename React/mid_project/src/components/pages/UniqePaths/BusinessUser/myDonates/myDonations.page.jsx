import React, { useState, useEffect } from "react";
import { useAuth0 } from "@auth0/auth0-react";
import { ClockLoader } from "react-spinners";
import {
  getDonates,
  deleteProductFromDb,
} from "../../../../../services/businessService";
import { CardDonate } from "./cardDonate";

export const MyDonationsPage = () => {
  const [productList, setProductList] = useState([]);
  const [showClock, setShowClock] = useState(true);
  const { user } = useAuth0();

  const getProductsFromDB = async () => {
    let res = await getDonates(user.email, "");
    setProductList(res);
    console.log(res);
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

  const handleDelete = async (product) => {
    deleteProductFromDb(product);
    await getProductsFromDB();
  };

  return (
    <div className="cardlistContainer">
      <h1>My Donations</h1>
      <div className="cardlistContainer2">
        {productList && productList.length > 0 ? (
          productList.map((product) => {
            return (
              <>
                <CardDonate product={product} handleDelete={handleDelete} />
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
            {!showClock && <h1>sorry we didn't found any Donations</h1>}
          </>
        )}
      </div>
    </div>
  );
};
