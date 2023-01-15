import React, { useState, useEffect } from "react";
import { getSpecificCampaigns } from "../../../../../services/serviceToAll";
import { ClockLoader } from "react-spinners";
import { useNavigate, useLocation } from "react-router-dom";
import "../css/organizationCard.css";
import { CardToCampagin } from "./cardToPresentCampaigns";
import { useAuth0 } from "@auth0/auth0-react";
import { signInForCampaign } from "../../../../../services/activistService";
import { ToastContainer, toast } from "react-toastify";

export const ActivistCampaignsPage = () => {
  const [campaignsList, setCampaignsList] = useState([]);
  const [showClock, setShowClock] = useState(true);
  const { user } = useAuth0();
  const navigate = useNavigate();
  const location = useLocation();
  const { organ, userEarnings, activeCampaignList } = location.state;

  const getCampaignsFromDB = async () => {
    let res = await getSpecificCampaigns(organ.email);
    setCampaignsList(res);
  };

  const handleBeActive = (cName) => {
    signInForCampaign(cName, user.email);
    toast.success("🦄 Congrats, we signed you to this campaign!", {
      position: "top-center",
      autoClose: 5000,
      hideProgressBar: false,
      closeOnClick: true,
      pauseOnHover: true,
      draggable: true,
      progress: undefined,
      theme: "light",
    });
  };

  const handleClaim = (cName, cHashtag) => {
    console.log(cName);
    navigate("/activist/campaignsProducts", {
      state: {
        cName,
        userEarnings,
        cHashtag,
      },
    });
  };
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
      <h3>Your earnings: {userEarnings}</h3>
      <div className="cardlistContainer2">
        {campaignsList && campaignsList.length > 0 ? (
          campaignsList.map((campaign) => {
            return (
              <>
                <CardToCampagin
                  campaign={campaign}
                  activeCampaignList={activeCampaignList}
                  handleBeActive={() => handleBeActive(campaign.campaignName)}
                  handleClaim={() =>
                    handleClaim(campaign.campaignName, campaign.campaignHashtag)
                  }
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
