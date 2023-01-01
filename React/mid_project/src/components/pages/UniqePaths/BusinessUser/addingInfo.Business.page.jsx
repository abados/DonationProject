import React, { useState, useContext } from "react";
import "./css/addingBInfo.css";
import { addBusinessUser } from "../../../../services/businessService";
import { FormContext } from "../../../../context/context";
import { useNavigate } from "react-router-dom";

export const BusinessUserForm = () => {
  const [fullName, setFullName] = useState("");
  const [email, setEmail] = useState("");
  const [cellPhone, setPhone] = useState("");
  const [businessName, setBusinessName] = useState("");
  const { setFormSubmitted } = useContext(FormContext);

  const navigate = useNavigate();

  const handleSubmit = (event) => {
    event.preventDefault();
    const newOwnerToAdd = {
      fullName,
      cellPhone,
      email,
      businessName,
    };
    if (
      fullName === "" ||
      cellPhone === "" ||
      email === "" ||
      businessName === ""
    ) {
      alert("Please enter all required");
    } else {
      addBusinessUser(newOwnerToAdd);
      setFormSubmitted(true);
      alert("we recive your comment, thanks for it");
      navigate("/thankYou");
      // setName("");
      // setBusinessID("");
      // setPhone("");
      // setEmail("");
      // setBusinessName("");
    }
  };

  return (
    <form onSubmit={handleSubmit} className="formBInfo">
      <h1>Please fill in you details</h1>
      <div className="form-group">
        <label className="formLbl" htmlFor="name">
          Business user full name:
        </label>
        <input
          type="text"
          className="form-control"
          id="name"
          value={fullName}
          onChange={(event) => setFullName(event.target.value)}
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
          Business Name:
        </label>
        <textarea
          className="form-control"
          id="message"
          value={businessName}
          onChange={(event) => setBusinessName(event.target.value)}
        />
      </div>

      <button type="submit" className="btn btn-primary">
        Submit
      </button>
    </form>
  );
};
