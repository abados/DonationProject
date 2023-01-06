import React, { useState, useEffect } from "react";
import {
  getOrganizations,
  getUserEarnings,
} from "../../../../../services/activistService";
import { ClockLoader } from "react-spinners";
import { useNavigate } from "react-router-dom";
import "../css/organizationCard.css";
import { CardToOrganization } from "./cardToPresentOrganization";
import { useAuth0 } from "@auth0/auth0-react";

export const ActivistOrganizationsPage = () => {
  const [organizaionList, setOrganizaionList] = useState([]);
  const [showClock, setShowClock] = useState(true);
  const [userEarnings, setUserEarnings] = useState(true);
  const navigate = useNavigate();
  const { user } = useAuth0();

  const getOrganizaionsFromDB = async () => {
    let res = await getOrganizations();
    setOrganizaionList(res);
    let earnings = await getUserEarnings(user.email);
    console.log(earnings);
    setUserEarnings(parseFloat(earnings.join("")));
    console.log(userEarnings);
  };

  useEffect(() => {
    setTimeout(() => {
      getOrganizaionsFromDB();
    }, 3000);
  }, [organizaionList.length]);

  useEffect(() => {
    const timeout = setTimeout(() => {
      setShowClock(false);
    }, 5000); // 5000 milliseconds = 5 seconds

    return () => {
      clearTimeout(timeout);
    };
  }, []);

  const handleChose = (organ) => {
    console.log(organ);
    navigate("/activist/specificCampaigns", {
      state: {
        organ,
        userEarnings,
      },
    });
  };

  return (
    <div className="cardlistContainer">
      <h1>Organizations</h1>
      <h3>Your earnings: {userEarnings}</h3>
      <div className="cardlistContainer2">
        {organizaionList && organizaionList.length > 0 ? (
          organizaionList.map((organ) => {
            return (
              <>
                <CardToOrganization
                  organ={organ}
                  handleChose={() => handleChose(organ)}
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
