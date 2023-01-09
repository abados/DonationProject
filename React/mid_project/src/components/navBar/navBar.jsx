import React, { useContext, useEffect, useState } from "react";
import { Link } from "react-router-dom";
import HomeIcon from "@mui/icons-material/Home";
import InfoIcon from "@mui/icons-material/Info";
import CallIcon from "@mui/icons-material/Call";
import MeetingRoomIcon from "@mui/icons-material/MeetingRoom";
import ContactMailIcon from "@mui/icons-material/ContactMail";
import { useAuth0 } from "@auth0/auth0-react";
import { getRole } from "../../services/serviceToAll";
import "./navbar.css";
import { UserRoleContext } from "../../context/context";
import { checkIfExsits } from "../../services/serviceToAll";
import { FormContext } from "../../context/context";
import LocalShippingIcon from "@mui/icons-material/LocalShipping";
import { NewRegisterPage } from "../pages/HomePage/newRegister.page";
import ShoppingCartIcon from "@mui/icons-material/ShoppingCart";

export const NavBarComponent = () => {
  const { user, logout, isLoading } = useAuth0();
  const [loading, setLoading] = useState(true);
  const { RoleContext, setRoleContext } = useContext(UserRoleContext);
  const { formSubmitted } = useContext(FormContext);
  const [isExists, setIsExists] = useState("false");

  //bring the user Role
  const handleRole = async () => {
    setLoading(true); // set loading to true before making the request
    let userId = user.sub;
    await setRoleContext(await getRole(userId));
    setLoading(false); // set loading to false after the request is complete
  };

  //chack if the user is in the Sql DB
  const checkingExsits = async () => {
    if (RoleContext && RoleContext[0] && RoleContext[0].name) {
      setIsExists(await checkIfExsits(user.email, RoleContext[0].name));
    }
  };

  //Call the funciton in specific order of execution
  const orderFunctions = async () => {
    await handleRole();
    await checkingExsits();
  };

  useEffect(() => {
    // declare the data fetching function
    const fetchData = async () => {
      await orderFunctions();
    };

    // call the function
    fetchData()
      // make sure to catch any error
      .catch(console.error);
  }, []);

  //after the user signUp completes, i want the navigation bar to Re-render.
  useEffect(() => {
    checkingExsits();
  }, [formSubmitted, isExists]);

  if (loading) return <>loading</>;
  else if (RoleContext === undefined && isLoading) {
    return <NewRegisterPage />;
  } else if (
    RoleContext[0] ? RoleContext[0].name === "Owner" : RoleContext[0] === null
  ) {
    return (
      <div className="container">
        <ul className="menu">
          <li>
            <label className="useNameLbl">
              Hello {RoleContext.map((r) => r.name)}{" "}
            </label>
          </li>
          {formSubmitted ? (
            <>
              <li>
                <Link to="/">
                  <HomeIcon />
                  <label className="navLbl">Home</label>
                </Link>
              </li>
              <li>
                <Link to="/checkTweets">
                  <InfoIcon />
                  <label className="navLbl">Checking tweets</label>
                </Link>
              </li>
              <li>
                <Link to="/Admin/Reports">
                  <CallIcon />
                  <label className="navLbl">Reports</label>
                </Link>
              </li>
              <li className="user-li">
                <Link to="/profile">
                  <img className="img" src={user.picture} alt="" />
                  <label className="navLbl">{user.name}</label>
                </Link>
              </li>
              <li className="loguot">
                <Link to="/logout">
                  <MeetingRoomIcon />
                  <label
                    className="navLbl"
                    onClick={() => logout({ returnTo: window.location.origin })}
                  >
                    Logout
                  </label>
                </Link>
              </li>
            </>
          ) : (
            <>
              <li>
                <label className="useNameRbl"></label>
                <Link to="/Admin/addingOInfo">
                  <ContactMailIcon />
                  <label className="navLbl">Sign IN</label>
                </Link>
              </li>
              <li className="loguot">
                <Link to="/logout">
                  <MeetingRoomIcon />
                  <label
                    className="navLbl"
                    onClick={() => logout({ returnTo: window.location.origin })}
                  >
                    Logout
                  </label>
                </Link>
              </li>
            </>
          )}
        </ul>
      </div>
    );
  } else if (
    RoleContext[0]
      ? RoleContext[0].name === "Activist"
      : RoleContext[0] === null
  ) {
    return (
      <div className="container">
        <ul className="menu">
          <li>
            <label className="useNameLbl">
              Hello {RoleContext.map((r) => r.name)}{" "}
            </label>
          </li>
          {formSubmitted ? (
            <>
              <li>
                <Link to="/">
                  <HomeIcon />
                  <label className="navLbl">Home</label>
                </Link>
              </li>
              <li>
                <Link to="/activist/listOfOrganization">
                  <InfoIcon />
                  <label className="navLbl">Organizations</label>
                </Link>
              </li>
              <li>
                <Link to="/activist/Purches">
                  <ShoppingCartIcon />
                  <label className="navLbl">My Products</label>
                </Link>
              </li>
              <li className="user-li">
                <Link to="/profile">
                  <img className="img" src={user.picture} alt="" />
                  <label className="navLbl">{user.name}</label>
                </Link>
              </li>
              <li className="loguot">
                <Link to="/logout">
                  <MeetingRoomIcon />
                  <label
                    className="navLbl"
                    onClick={() => logout({ returnTo: window.location.origin })}
                  >
                    Logout
                  </label>
                </Link>
              </li>
            </>
          ) : (
            <>
              <li>
                <label className="useNameRbl"></label>
                <Link to="/Activist/addingAInfo">
                  <ContactMailIcon />
                  <label className="navLbl">Sign IN</label>
                </Link>
              </li>
              <li className="loguot">
                <Link to="/logout">
                  <MeetingRoomIcon />
                  <label
                    className="navLbl"
                    onClick={() => logout({ returnTo: window.location.origin })}
                  >
                    Logout
                  </label>
                </Link>
              </li>
            </>
          )}
        </ul>
      </div>
    );
  } else if (
    RoleContext[0]
      ? RoleContext[0].name === "NonProfit"
      : RoleContext[0] === null
  ) {
    return (
      <div className="container">
        <ul className="menu">
          <li>
            <label className="useNameLbl">
              Hello {RoleContext.map((r) => r.name)}{" "}
            </label>
          </li>
          {formSubmitted ? (
            <>
              <li>
                <Link to="/">
                  <HomeIcon />
                  <label className="navLbl">Home</label>
                </Link>
              </li>
              <li>
                <Link to="/MyCampagins">
                  <InfoIcon />
                  <label className="navLbl">My Campaigns</label>
                </Link>
              </li>
              <li>
                <Link to="/addCampagin">
                  <ShoppingCartIcon />
                  <label className="navLbl">Add Campagin</label>
                </Link>
              </li>
              <li className="user-li">
                <Link to="/profile">
                  <img className="img" src={user.picture} alt="" />
                  <label className="navLbl">{user.name}</label>
                </Link>
              </li>
              <li className="loguot">
                <Link to="/logout">
                  <MeetingRoomIcon />
                  <label
                    className="navLbl"
                    onClick={() => logout({ returnTo: window.location.origin })}
                  >
                    Logout
                  </label>
                </Link>
              </li>
            </>
          ) : (
            <>
              <li>
                <label className="useNameRbl"></label>
                <Link to="/NonProfit/addingNInfo">
                  <ContactMailIcon />
                  <label className="navLbl">Sign IN</label>
                </Link>
              </li>
              <li className="loguot">
                <Link to="/logout">
                  <MeetingRoomIcon />
                  <label
                    className="navLbl"
                    onClick={() => logout({ returnTo: window.location.origin })}
                  >
                    Logout
                  </label>
                </Link>
              </li>
            </>
          )}
        </ul>
      </div>
    );
  } else if (
    RoleContext[0]
      ? RoleContext[0].name === "Business"
      : RoleContext[0] === null
  ) {
    return (
      <div className="container">
        <ul className="menu">
          <li>
            <label className="useNameLbl">
              Hello {RoleContext.map((r) => r.name)}{" "}
            </label>
          </li>
          {formSubmitted ? (
            <>
              <li>
                <Link to="/">
                  <HomeIcon />
                  <label className="navLbl">Home</label>
                </Link>
              </li>
              <li>
                <Link to="/business/Campaigns">
                  <InfoIcon />
                  <label className="navLbl">Campaigns</label>
                </Link>
              </li>

              <li>
                <Link to="/MyDonates">
                  <InfoIcon />
                  <label className="navLbl">My Donates</label>
                </Link>
              </li>
              <li>
                <Link to="/business/ShipmentPage">
                  <LocalShippingIcon />
                  <label className="navLbl">Shipment tracking </label>
                </Link>
              </li>
              <li className="user-li">
                <Link to="/profile">
                  <img className="img" src={user.picture} alt="" />

                  <label className="navLbl">{user.name}</label>
                </Link>
              </li>
              <li className="loguot">
                <Link to="/logout">
                  <MeetingRoomIcon />
                  <label
                    className="navLbl"
                    onClick={() => logout({ returnTo: window.location.origin })}
                  >
                    Logout
                  </label>
                </Link>
              </li>
            </>
          ) : (
            <>
              <li>
                <label className="useNameRbl"></label>
                <Link to="/Business/addingBInfo">
                  <ContactMailIcon />
                  <label className="navLbl">Sign IN</label>
                </Link>
              </li>
              <li className="loguot">
                <Link to="/logout">
                  <MeetingRoomIcon />
                  <label
                    className="navLbl"
                    onClick={() => logout({ returnTo: window.location.origin })}
                  >
                    Logout
                  </label>
                </Link>
              </li>
            </>
          )}
        </ul>
      </div>
    );
  }
};
