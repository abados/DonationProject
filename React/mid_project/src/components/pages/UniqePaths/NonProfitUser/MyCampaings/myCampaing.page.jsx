import React, { useState, useEffect } from "react";
import { getCampaings } from "../../../../../services/nonProfitService";
import { ClockLoader } from "react-spinners";
import { useNavigate } from "react-router-dom";
import { deleteCampaignFromDb } from "../../../../../services/nonProfitService";
import "./myCampaings.css";
import { CampaignCardComponent } from "./campaingsCardComponent/campaign.component";
import { getSpecificCampaigns } from "../../../../../services/serviceToAll";
import { useAuth0 } from "@auth0/auth0-react";

export const CampaingsPage = () => {
  const [campaingList, setCampaingList] = useState([]);
  const [showClock, setShowClock] = useState(true);
  const navigate = useNavigate();
  const { user } = useAuth0();

  const getCampaingsFromDB = async () => {
    let res = await getSpecificCampaigns(user.email);
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

  const handleUpadate = (campaing) => {
    navigate("/editCampaing", {
      state: {
        campaing,
      },
    });
  };

  const handleDelete = async (campaignName) => {
    deleteCampaignFromDb(campaignName);
    await getCampaingsFromDB();
  };

  return (
    <div className="cardlistContainer">
      <h1>My Campaigns</h1>
      {campaingList && campaingList.length > 0 ? (
        campaingList.map((campaing) => {
          return (
            <>
              <CampaignCardComponent
                campaing={campaing}
                handleUpadate={handleUpadate}
                handleDelete={handleDelete}
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
  );
};
