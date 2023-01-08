import React, { useState, useEffect } from "react";
import { getDonates } from "../../../../../services/businessService";
import { makeAPurchase } from "../../../../../services/activistService";
import { ClockLoader } from "react-spinners";
import { useLocation } from "react-router-dom";
import "../css/organizationCard.css";
import { CardToReward } from "./cardToPresentRewards";
import { useAuth0 } from "@auth0/auth0-react";
import { ToastContainer, toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";

export const ClaimRwardsForActivist = () => {
  const [productList, setProductList] = useState([]);
  const [showClock, setShowClock] = useState(true);
  const [isBought, setIsBought] = useState(false);
  const [namekey, setNameKey] = useState();
  const [earnings, setEarnings] = useState(0);

  const { user } = useAuth0();
  const location = useLocation();
  const { cName, userEarnings } = location.state;

  const getProductsFromDB = async () => {
    let res = await getDonates(cName);
    setProductList(res);
    console.log(res);
  };

  const handleBuy = (ProductPrice, productName) => {
    console.log(productName);

    if (ProductPrice > earnings) {
      toast.warn("🛒 insufficient funds!", {
        position: "top-center",
        autoClose: 5000,
        hideProgressBar: false,
        closeOnClick: true,
        pauseOnHover: true,
        draggable: true,
        progress: undefined,
        theme: "light",
      });
    } else {
      setNameKey(productName);
      setIsBought(true);
      setEarnings(earnings - ProductPrice);
      toast.success("🦄 Congrats!", {
        position: "top-center",
        autoClose: 5000,
        hideProgressBar: false,
        closeOnClick: true,
        pauseOnHover: true,
        draggable: true,
        progress: undefined,
        theme: "light",
      });
      makeAPurchase(productName, ProductPrice, user.email);
      getProductsFromDB();

      //by this order:
      ////1.lower activist income (i need product price) - DONE
      ////2.change IsBought status of the product(i need product name) - DONE
      //3. make it not visible to other activists users(add a condition to the getDonates sql query that ignore those which are bought)
      //4. add it to the trackings list of the business user(nre page that take just product that is bought)
      //
    }
  };

  useEffect(() => {
    setTimeout(() => {
      getProductsFromDB();
      setEarnings(userEarnings);
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
      <h1>Products</h1>
      <h3>Your earnings: {earnings}</h3>
      <div className="cardlistContainer2">
        {productList && productList.length > 0 ? (
          productList.map((Product) => {
            return (
              <>
                <CardToReward
                  Product={Product}
                  handleBuy={() =>
                    handleBuy(Product.price, Product.productName)
                  }
                  isBought={isBought}
                  namekey={namekey}
                />
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
      <ToastContainer
        position="top-center"
        autoClose={5000}
        hideProgressBar={false}
        newestOnTop={false}
        closeOnClick
        rtl={false}
        pauseOnFocusLoss
        draggable
        pauseOnHover
        theme="light"
      />
    </div>
  );
};
