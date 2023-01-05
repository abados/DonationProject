import React, { useState, useEffect } from "react";
import { getSpecificCampaigns } from "../../../../../services/serviceToAll";
import { ClockLoader } from "react-spinners";
import { useNavigate, useLocation } from "react-router-dom";
import "../css/organizationCard.css";
import { CardToCampagin } from "./cardToPresentCampaigns";

export const ActivistCampaignsPage = () => {
  const [campaignsList, setCampaignsList] = useState([]);
  const [showClock, setShowClock] = useState(true);
  const navigate = useNavigate();
  const location = useLocation();
  const { organ } = location.state;

  const getCampaignsFromDB = async () => {
    let res = await getSpecificCampaigns(organ.email);
    setCampaignsList(res);
  };

  const handleBeActive = () => {};

  const handleClaim = () => {};

  useEffect(() => {
    setTimeout(() => {
      getCampaignsFromDB();
    }, 3000);
  }, [campaignsList.length]);

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
      <h1>Campaigns</h1>
      <div className="cardlistContainer2">
        {campaignsList && campaignsList.length > 0 ? (
          campaignsList.map((campaign) => {
            return (
              <>
                <CardToCampagin
                  campaign={campaign}
                  handleBeActive={() => handleBeActive()}
                  handleClaim={() => handleClaim()}
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
            {!showClock && <h1>sorry we didn't found any Organizations</h1>}
          </>
        )}
      </div>
    </div>
  );
};
