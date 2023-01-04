import React, { useState, useEffect } from "react";
import { getCampaings } from "../../../../../services/nonProfitService";
import { ClockLoader } from "react-spinners";
import { useNavigate } from "react-router-dom";

import "./organization.css";

import { CardToPresent } from "./cardToPresent";

export const BusinessCampaignPage = () => {
  const [campaingList, setCampaingList] = useState([]);
  const [showClock, setShowClock] = useState(true);
  const navigate = useNavigate();

  const getCampaingsFromDB = async () => {
    let res = await getCampaings();
    setCampaingList(res);
  };

  useEffect(() => {
    setTimeout(() => {
      getCampaingsFromDB();
    }, 3000);
  }, [campaingList]);

  useEffect(() => {
    const timeout = setTimeout(() => {
      setShowClock(false);
    }, 5000); // 5000 milliseconds = 5 seconds

    return () => {
      clearTimeout(timeout);
    };
  }, []);

  const handleDonate = (campaing) => {
    console.log(campaing);
    navigate("/donateProduct", {
      state: {
        campaing,
      },
    });
  };

  return (
    <div className="cardlistContainer">
      <h1>My Campaigns</h1>
      <div className="cardlistContainer2">
        {campaingList && campaingList.length > 0 ? (
          campaingList.map((campaing) => {
            return (
              <>
                <CardToPresent
                  campaing={campaing}
                  handleDonate={handleDonate}
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
            {!showClock && <h1>sorry we didn't found any campaign</h1>}
          </>
        )}
      </div>
    </div>
  );
};
