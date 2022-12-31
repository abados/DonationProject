import React, { useContext, useEffect, useState } from "react";
import { Link, useNavigate } from "react-router-dom";
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

export const NavBarComponent = () => {
  const { user, logout } = useAuth0();
  const [loading, setLoading] = useState(true);
  const { RoleContext, setRoleContext } = useContext(UserRoleContext);
  const { formSubmitted } = useContext(FormContext);
  const [isExists, setIsExists] = useState("");
  const navigate = useNavigate();
  const handleRole = async () => {
    setLoading(true); // set loading to true before making the request
    let userId = await user.sub;
    await setRoleContext(await getRole(userId));
    checkingExsits();

    setLoading(false); // set loading to false after the request is complete
  };

  const checkingExsits = async () => {
    await setIsExists(await checkIfExsits(user.email, "Activist"));
  };

  useEffect(() => {
    handleRole();
  }, []);

  useEffect(() => {
    handleRole();
  }, [formSubmitted]);

  if (loading) return <>loading</>;
  else if (RoleContext[0].name === "Owner") {
    return (
      <div className="container">
        <ul className="menu">
          <li>
            <label className="useNameLbl">
              Hello {RoleContext.map((r) => r.name)}{" "}
            </label>
          </li>
          {isExists === "false" ? (
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
          ) : (
            <>
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
            </>
          )}
        </ul>
      </div>
    );
  } else if (RoleContext[0].name === "Activist") {
    return (
      <div className="container">
        <ul className="menu">
          <li>
            <label className="useNameLbl">
              Hello {RoleContext.map((r) => r.name)}{" "}
            </label>
          </li>
          {isExists === "false" ? (
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
          ) : (
            <>
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
                  <label className="navLbl">Cart</label>
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
            </>
          )}
        </ul>
      </div>
    );
  } else if (RoleContext[0].name === "NonProfit") {
    return (
      <div className="container">
        <ul className="menu">
          <li>
            <label className="useNameLbl">
              Hello {RoleContext.map((r) => r.name)}{" "}
            </label>
          </li>
          {isExists === "false" ? (
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
          ) : (
            <>
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
            </>
          )}
        </ul>
      </div>
    );
  } else if (RoleContext[0].name === "Business") {
    return (
      <div className="container">
        <ul className="menu">
          <li>
            <label className="useNameLbl">
              Hello {RoleContext.map((r) => r.name)}{" "}
            </label>
          </li>
          {isExists === "false" ? (
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
          ) : (
            <>
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
            </>
          )}
        </ul>
      </div>
    );
  }
};
