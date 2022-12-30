import React, { useContext, useEffect, useState } from "react";
import { Link } from "react-router-dom";
import HomeIcon from "@mui/icons-material/Home";
import InfoIcon from "@mui/icons-material/Info";
import CallIcon from "@mui/icons-material/Call";
import MeetingRoomIcon from "@mui/icons-material/MeetingRoom";
import RedeemIcon from "@mui/icons-material/Redeem";
import ContactMailIcon from "@mui/icons-material/ContactMail";
import { useAuth0 } from "@auth0/auth0-react";
import { getRole } from "../../services/serviceToAll";
import "./navbar.css";
import { UserRoleContext } from "../../context/context";

export const NavBarComponent = (props) => {
  const { user, logout } = useAuth0();
  const [role, setRole] = useState([]);
  const [loading, setLoading] = useState(true);
  const { RoleContext, setRoleContext } = useContext(UserRoleContext);

  const handleRole = async () => {
    setLoading(true); // set loading to true before making the request
    let userId = await user.sub;
    let userRole = await getRole(userId);
    setRole(userRole);
    setRoleContext(userRole);
    setLoading(false); // set loading to false after the request is complete
  };

  useEffect(() => {
    handleRole();
  }, []);

  if (loading) return <>loading</>;
  else if (role[0].name === "Admin") {
    return (
      <div className="container">
        <ul className="menu">
          <li>
            <label className="useNameLbl">
              Hello {role.map((r) => r.name)}{" "}
            </label>
          </li>
          <li>
            <Link to="/Admin/addingOInfo">
              <ContactMailIcon />
              <label className="navLbl">Sign IN</label>
            </Link>
          </li>
          <li>
            <Link to="/">
              <HomeIcon />
              <label className="navLbl">Home</label>
            </Link>
          </li>
          <li>
            <Link to="/about">
              <InfoIcon />
              <label className="navLbl">Organizations</label>
            </Link>
          </li>
          <li>
            <Link to="/contactus">
              <CallIcon />
              <label className="navLbl">Reports</label>
            </Link>
          </li>
          <li className="user-li">
            <Link to="/profile">
              <img className="img" src={user.picture} alt={user.name} />
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
        </ul>
      </div>
    );
  } else if (role[0].name === "Non-Profit User") {
    return (
      <div className="container">
        <ul className="menu">
          <li>
            <label className="useNameLbl">
              Hello {role.map((r) => r.name)}{" "}
            </label>
          </li>
          <li>
            <Link to="/NonProfit/addingNInfo">
              <ContactMailIcon />
              <label className="navLbl">Sign In</label>
            </Link>
          </li>
          <li>
            <Link to="/">
              <HomeIcon />
              <label className="navLbl">Home</label>
            </Link>
          </li>
          <li>
            <Link to="/contactus">
              <CallIcon />
              <label className="navLbl">Contact Us</label>
            </Link>
          </li>
          <li className="user-li">
            <Link to="/profile">
              <img className="img" src={user.picture} alt={user.name} />
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
        </ul>
      </div>
    );
  } else if (role[0].name === "Activist") {
    return (
      <div className="container">
        <ul className="menu">
          <li>
            <label className="useNameLbl">
              Hello {role.map((r) => r.name)}{" "}
            </label>
          </li>
          <li>
            <Link to="/Activist/addingAInfo">
              <ContactMailIcon />
              <label className="navLbl">Sign In</label>
            </Link>
          </li>
          <li className="user-li">
            <Link to="/profile">
              <img className="img" src={user.picture} alt={user.name} />
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
        </ul>
      </div>
    );
  } else if (role[0].name === "Business User") {
    return (
      <div className="container">
        <ul className="menu">
          <li>
            <label className="useNameLbl">
              Hello {role.map((r) => r.name)}{" "}
            </label>
          </li>
          <li>
            <Link to="/Business/addingBInfo">
              <ContactMailIcon />
              <label className="navLbl">Sign In</label>
            </Link>
          </li>
          <li className="user-li">
            <Link to="/profile">
              <img className="img" src={user.picture} alt={user.name} />
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
        </ul>
      </div>
    );
  }
};
