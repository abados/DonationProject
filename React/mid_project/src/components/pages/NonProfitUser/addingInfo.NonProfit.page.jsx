import React, { useState } from "react";
import { addNonProfitUser } from "../../../services/nonProfitService";
import "./css/addingNInfo.css";

export const NonProfitUserForm = () => {
  const [fullName, setName] = useState("");
  const [email, setEmail] = useState("");
  const [cellPhone, setPhone] = useState("");
  const [organizationName, setOrganizationName] = useState("");
  const [organizationUrl, setOrganizationUrl] = useState("");
  //const [organizationLogo, setOrganizationLogo] = useState("");
  const [organizationDescription, setOrganizationDescription] = useState("");

  const handleSubmit = (event) => {
    event.preventDefault();
    const newNonProfitUser = {
      fullName,
      cellPhone,
      email,
      organizationName,
      organizationUrl,
      //organizationLogo,
      organizationDescription,
    };
    if (
      fullName === "" ||
      organizationName === "" ||
      cellPhone === "" ||
      email === "" ||
      organizationUrl === "" ||
      //organizationLogo === "" ||
      organizationDescription === ""
    ) {
      alert("Please enter all required");
    } else {
      addNonProfitUser(newNonProfitUser);
      alert("we recive your comment, thanks for it");
      //   setName("");
      //   setPhone("");
      //   setEmail("");
      //   setOrganizationName("");
      //   setOrganizationUrl("");
      //   //setOrganizationLogo("");
      //   setOrganizationDescription("");
    }
  };

  return (
    <div className="formContainer">
      <form onSubmit={handleSubmit} className="formNInfo">
        <h1>Please fill in you details</h1>
        <div className="form-group">
          <label className="formLbl" htmlFor="name">
            Activist Name:
          </label>
          <input
            type="text"
            className="form-control"
            id="name"
            value={fullName}
            onChange={(event) => setName(event.target.value)}
          />
        </div>
        <div className="form-group">
          <label className="formLbl" htmlFor="email">
            Email Address:
          </label>
          <input
            type="email"
            className="form-control"
            id="email"
            value={email}
            onChange={(event) => setEmail(event.target.value)}
          />
        </div>
        <div className="form-group">
          <label className="formLbl" htmlFor="phone">
            Phone Number:
          </label>
          <input
            type="tel"
            className="form-control"
            id="phone"
            value={cellPhone}
            onChange={(event) => setPhone(event.target.value)}
          />
        </div>
        <div className="form-group">
          <label className="formLbl" htmlFor="message">
            Organization Name:
          </label>
          <input
            type="text"
            className="form-control"
            id="name"
            value={organizationName}
            onChange={(event) => setOrganizationName(event.target.value)}
          />
        </div>
        <div className="form-group">
          <label className="formLbl" htmlFor="message">
            Organization Url:
          </label>
          <input
            type="text"
            className="form-control"
            id="name"
            value={organizationUrl}
            onChange={(event) => setOrganizationUrl(event.target.value)}
          />
        </div>
        {/* <div className="form-group">
          <label className="formLbl" htmlFor="message">
            Organization Logo:
          </label>
          <input
            type="file"
            className="form-control"
            id="name"
            value={organizationLogo}
            onChange={(event) => setOrganizationLogo(event.target.value)}
          />
        </div> */}
        <div className="form-group">
          <label className="formLbl" htmlFor="message">
            Organization Description:
          </label>
          <textarea
            className="form-control"
            id="message"
            value={organizationDescription}
            onChange={(event) => setOrganizationDescription(event.target.value)}
          />
        </div>

        <button type="submit" className="btn btn-primary">
          Submit
        </button>
      </form>
    </div>
  );
};
